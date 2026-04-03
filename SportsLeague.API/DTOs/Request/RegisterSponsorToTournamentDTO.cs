namespace SportsLeague.API.DTOs.Request;

public class RegisterSponsorToTournamentDTO
{
    public int TournamentId { get; set; }
    public decimal ContractAmount { get; set; }
}