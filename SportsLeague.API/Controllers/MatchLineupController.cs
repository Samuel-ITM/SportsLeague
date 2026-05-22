using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportsLeague.API.DTOs.Request;
using SportsLeague.API.DTOs.Response;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.API.Controllers;

[ApiController]
[Route("api/match/{matchId}/lineup")]
public class MatchLineupController : ControllerBase
{
    private readonly IMatchLineupService _service;
    private readonly IMapper _mapper;

    public MatchLineupController(IMatchLineupService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetLineup(int matchId)
    {
        try
        {
            var lineups = await _service.GetLineupByMatchAsync(matchId);
            var response = _mapper.Map<IEnumerable<MatchLineupResponseDTO>>(lineups);
            return Ok(response);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpGet("team/{teamId}")]
    public async Task<IActionResult> GetLineupByTeam(int matchId, int teamId)
    {
        try
        {
            var lineups = await _service.GetLineupByMatchAndTeamAsync(matchId, teamId);
            var response = _mapper.Map<IEnumerable<MatchLineupResponseDTO>>(lineups);
            return Ok(response);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddToLineup(int matchId, [FromBody] MatchLineupRequestDTO dto)
    {
        try
        {
            var lineup = _mapper.Map<MatchLineup>(dto);
            var result = await _service.AddPlayerToLineupAsync(matchId, lineup);
            var response = _mapper.Map<MatchLineupResponseDTO>(result);
            return StatusCode(201, response);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFromLineup(int matchId, int id)
    {
        try
        {
            await _service.DeleteFromLineupAsync(matchId, id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}