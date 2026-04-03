using SportsLeague.Domain.Enums;

namespace SportsLeague.Domain.Entities;

public class Sponsor : AuditBase
{
    public string Name {get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty;
    public string? Phone { get; set; } = string.Empty;
    public string? WebsiteUrl { get; set; } = string.Empty;
    public SponsorCategory Category { get; set; }
    
    //relacion n:m
    public ICollection<TournamentSponsor> TournamentSponsors { get; set; } = new List<TournamentSponsor>();
    
}