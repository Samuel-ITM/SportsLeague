using Microsoft.Extensions.Logging;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.Domain.Services;

public class SponsorService : ISponsorService
{
    private readonly ISponsorRepository _sponsorRepository;
    private readonly ITournamentSponsorRepository _tournamentSponsorRepository;
    private readonly ILogger<SponsorService> _logger;

    public SponsorService(
        ISponsorRepository sponsorRepository,
        ITournamentSponsorRepository tournamentSponsorRepository,
        ILogger<SponsorService> logger)
    {
        _sponsorRepository = sponsorRepository;
        _tournamentSponsorRepository = tournamentSponsorRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Sponsor>> GetAllAsync()
    {
        _logger.LogInformation("Retrieving all sponsors");
        return await _sponsorRepository.GetAllAsync();
    }

    public async Task<Sponsor?> GetByIdAsync(int id)
    {
        _logger.LogInformation("Retrieving sponsor with ID: {SponsorId}", id);
        var sponsor = await _sponsorRepository.GetByIdAsync(id);
        if (sponsor == null)
            _logger.LogWarning("Sponsor with ID {SponsorId} not found", id);
        return sponsor;
    }

    public async Task<Sponsor> CreateAsync(Sponsor sponsor)
    {
        //validacion de negocio: nombre unico
        var existingSponsor = await _sponsorRepository.GetByNameAsync(sponsor.Name);
        if (existingSponsor != null)
        {
            _logger.LogWarning("Sponsor with name '{SponsorName}' already exists", sponsor.Name);
            throw new InvalidOperationException(
            $"Ya existe un patrocinador con el nombre '{sponsor.Name}'");
        }

        // Validación de negocio: formato de email
        if (!IsValidEmail(sponsor.ContactEmail))
        {
            _logger.LogWarning("Invalid email format for sponsor: {Email}", sponsor.ContactEmail);
            throw new InvalidOperationException(
            $"El formato del correo electrónico '{sponsor.ContactEmail}' no es válido");
        }

        _logger.LogInformation("Creating sponsor: {Name}",sponsor.Name);
        return await _sponsorRepository.CreateAsync(sponsor);
    }

    private bool IsValidEmail(string email)
    {
        return new System.ComponentModel.DataAnnotations.EmailAddressAttribute()
        .IsValid(email);
    }

    public async Task UpdateAsync(int id, Sponsor sponsor)
    {
        var existingSponsor = await _sponsorRepository.GetByIdAsync(id);
        if (existingSponsor == null)
            throw new KeyNotFoundException($"No se encontró el patrocinador con ID {id}");

        // Validación de negocio: formato de email
        if (!IsValidEmail(sponsor.ContactEmail))
        {
            _logger.LogWarning("Invalid email format for sponsor: {Email}", sponsor.ContactEmail);
            throw new InvalidOperationException(
            $"El formato del correo electrónico '{sponsor.ContactEmail}' no es válido");
        }

        // Validar nombre único (si cambió)
        if (existingSponsor.Name != sponsor.Name)
        {
            var sponsorWithSameName = await _sponsorRepository.GetByNameAsync(sponsor.Name);
            if (sponsorWithSameName != null)
            {
                throw new InvalidOperationException(
                $"Ya existe un patrocinador con el nombre '{sponsor.Name}'");
            }
        }

        existingSponsor.Name = sponsor.Name;
        existingSponsor.ContactEmail = sponsor.ContactEmail;
        existingSponsor.Phone = sponsor.Phone;
        existingSponsor.WebsiteUrl = sponsor.WebsiteUrl;
        existingSponsor.Category = sponsor.Category;

        _logger.LogInformation("Updating sponsor with ID: {SponsorId}", id);
        await _sponsorRepository.UpdateAsync(existingSponsor);
    }

    public async Task DeleteAsync(int id)
    {
        var existingSponsor = await _sponsorRepository.GetByIdAsync(id);
        if (existingSponsor == null)
            throw new KeyNotFoundException($"No se encontró el patrocinador con ID {id}");

        // Validar que no esté asociado a ningún torneo
        var associatedTournaments = await _tournamentSponsorRepository.GetBySponsorIdAsync(id);
        if (associatedTournaments.Any())
        {
            throw new InvalidOperationException(
            "No se puede eliminar el patrocinador porque está asociado a uno o más torneos");
        }

        _logger.LogInformation("Deleting sponsor with ID: {SponsorId}", id);
        await _sponsorRepository.DeleteAsync(id);
    }

    public async Task RegisterSponsorToTournamentAsync(int sponsorId, int tournamentId, decimal contractAmount)
    {
        // Validar que el patrocinador existe
        var sponsor = await _sponsorRepository.GetByIdAsync(sponsorId);
        if (sponsor == null)
            throw new KeyNotFoundException($"No se encontró el patrocinador con ID {sponsorId}");

        // Validar que el torneo existe
        var tournament = await _tournamentSponsorRepository.GetByTournamentIdAsync(tournamentId);
        if (tournament == null)
            throw new KeyNotFoundException($"No se encontró el torneo con ID {tournamentId}");

        // Validar que no esté ya asignado
        var existingAssignment = await _tournamentSponsorRepository.GetByTournamentAndSponsorAsync(tournamentId, sponsorId);
        if (existingAssignment != null)
        {
            throw new InvalidOperationException(
            "El patrocinador ya está asignado a este torneo");
        }
        // validar el monto del contrato
        if (contractAmount < 1)
        {
            throw new InvalidOperationException(
            "El monto del contrato debe ser mayor a cero");
        }

        var tournamentSponsor = new TournamentSponsor
        {
            SponsorId = sponsorId,
            TournamentId = tournamentId,
            ContractAmount = contractAmount,
            JoinedAt = DateTime.UtcNow
        };

        _logger.LogInformation("Registering sponsor {SponsorId} to tournament {TournamentId} with contract amount {ContractAmount}",
        sponsorId, tournamentId, contractAmount);
        await _tournamentSponsorRepository.CreateAsync(tournamentSponsor);
    }

    public async Task<IEnumerable<Tournament>> GetTournamentsBySponsorAsync(int sponsorId)
    {
        var sponsor = await _sponsorRepository.GetByIdAsync(sponsorId);
        if (sponsor == null)
            throw new KeyNotFoundException($"No se encontró el patrocinador con ID {sponsorId}");

        var tournamentSponsors = await _tournamentSponsorRepository.GetBySponsorIdAsync(sponsorId);
        return tournamentSponsors.Select(ts => ts.Tournament);
    }

    //desvincular patrocinador de torneo
    public async Task UnregisterSponsorFromTournamentAsync(int sponsorId, int tournamentId)
    {
        var existingAssignment = await _tournamentSponsorRepository.GetByTournamentAndSponsorAsync(tournamentId, sponsorId);
        if (existingAssignment == null)
        {
            throw new KeyNotFoundException(
            "El patrocinador no está asignado a este torneo");
        }

        await _tournamentSponsorRepository.DeleteAsync(existingAssignment.Id);
        _logger.LogInformation("Unregistered sponsor {SponsorId} from tournament {TournamentId}", sponsorId, tournamentId);
    }       
}