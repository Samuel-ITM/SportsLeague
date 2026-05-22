using SportsLeague.Domain.Enums;

namespace SportsLeague.API.DTOs.Response;

public class MatchLineupResponseDTO
{
    public int Id { get; set; }
    public int MatchId { get; set; }
    public int PlayerId { get; set; }
    public string PlayerName { get; set; } = string.Empty;
    public string TeamName { get; set; } = string.Empty;
    public bool IsStarter { get; set; }
    public PlayerPosition Position { get; set; }
}