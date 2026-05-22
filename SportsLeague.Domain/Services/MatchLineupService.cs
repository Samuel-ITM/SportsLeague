using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Enums;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services;
using SportsLeague.Domain.Helpers;

namespace SportsLeague.Domain.Services;

public class MatchLineupService : IMatchLineupService
{
    private readonly IMatchLineupRepository _lineupRepo;
    private readonly IMatchRepository _matchRepo;
    private readonly IPlayerRepository _playerRepo;
    private readonly MatchValidationHelper _validationHelper;

    public MatchLineupService(
        IMatchLineupRepository lineupRepo,
        IMatchRepository matchRepo,
        IPlayerRepository playerRepo,
        MatchValidationHelper validationHelper)
    {
        _lineupRepo = lineupRepo;
        _matchRepo = matchRepo;
        _playerRepo = playerRepo;
        _validationHelper = validationHelper;
    }

    public async Task<MatchLineup> AddPlayerToLineupAsync(int matchId, MatchLineup lineup)
    {
        // V1: El partido debe existir
        var match = await _matchRepo.GetByIdAsync(matchId);
        if (match == null)
            throw new KeyNotFoundException($"No se encontró el partido con ID {matchId}");

        // V2: El jugador debe existir y V3: El jugador debe pertenecer al HomeTeam o AwayTeam
        await _validationHelper.ValidatePlayerInMatchAsync(lineup.PlayerId, match); 

        // V4: El jugador no puede estar duplicado
        var exists = await _lineupRepo.ExistsByMatchAndPlayerAsync(matchId, lineup.PlayerId);
        if (exists)
            throw new InvalidOperationException("El jugador ya está registrado en la alineación de este partido");

        // V5: Máximo 11 titulares por equipo
        var player = await _playerRepo.GetByIdAsync(lineup.PlayerId);
        if (lineup.IsStarter)
        {
            var starterCount = await _lineupRepo.CountStartersByMatchAndTeamAsync(matchId, player.TeamId);
            if (starterCount >= 11)
                throw new InvalidOperationException("El equipo ya tiene 11 titulares registrados en este partido");
        }

        // V6: El partido debe estar en estado Scheduled
        if (match.Status != MatchStatus.Scheduled)
            throw new InvalidOperationException("Solo se pueden registrar alineaciones en partidos Scheduled");

        lineup.MatchId = matchId;
        return await _lineupRepo.CreateAsync(lineup);
    }

    public async Task<IEnumerable<MatchLineup>> GetLineupByMatchAsync(int matchId)
    {
        var match = await _matchRepo.GetByIdAsync(matchId);
        if (match == null)
            throw new KeyNotFoundException($"No se encontró el partido con ID {matchId}");

        return await _lineupRepo.GetByMatchAsync(matchId);
    }

    public async Task<IEnumerable<MatchLineup>> GetLineupByMatchAndTeamAsync(int matchId, int teamId)
    {
        var match = await _matchRepo.GetByIdAsync(matchId);
        if (match == null)
            throw new KeyNotFoundException($"No se encontró el partido con ID {matchId}");

        var team = match.HomeTeamId == teamId || match.AwayTeamId == teamId;
        if (!team)
            throw new InvalidOperationException($"El equipo con ID {teamId} no participa en el partido con ID {matchId}");

        return await _lineupRepo.GetByMatchAndTeamAsync(matchId, teamId);
    }

    public async Task DeleteFromLineupAsync(int matchId, int lineupId)
    {
        var lineup = await _lineupRepo.GetByIdAsync(lineupId);
        if (lineup == null || lineup.MatchId != matchId)
            throw new KeyNotFoundException($"No se encontró el registro con ID {lineupId} en la alineación del partido {matchId}");

        await _lineupRepo.DeleteAsync(lineupId);
    }
}