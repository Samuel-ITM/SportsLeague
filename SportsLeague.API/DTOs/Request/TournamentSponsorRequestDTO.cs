namespace SportsLeague.API.DTOs.Request;

public class TournamentSponsorRequestDTO
{
    public int TournamentId { get; set; }
    public int SponsorId { get; set; }
    public decimal ContractAmount { get; set; }
    public DateTime JoinedAt { get; set; }
}