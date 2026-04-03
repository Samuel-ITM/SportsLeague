using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Services;

public interface ISponsorService
{
    Task<IEnumerable<Sponsor>> GetAllAsync();
    Task<Sponsor?> GetByIdAsync(int id);
    Task<Sponsor> CreateAsync(Sponsor sponsor);
    Task UpdateAsync(int id, Sponsor sponsor);
    Task DeleteAsync(int id);
    Task<TournamentSponsor> RegisterSponsorToTournamentAsync(int sponsorId, int tournamentId, decimal contractAmount);
    Task<IEnumerable<Tournament>> GetTournamentsBySponsorAsync(int sponsorId);
    Task<IEnumerable<TournamentSponsor>> GetTournamentSponsorshipsBySponsorAsync(int sponsorId);
    Task UnregisterSponsorFromTournamentAsync(int sponsorId, int tournamentId);
}