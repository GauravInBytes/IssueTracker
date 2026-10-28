using IssueTrackerApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IssueTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly IssueTrackerDbContext _db;

    public TicketsController(IssueTrackerDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetTickets(
        [FromQuery] int? projectId,
        CancellationToken cancellationToken)
    {
        var query = _db.Tickets.AsNoTracking();

        if (projectId.HasValue)
            query = query.Where(ticket => ticket.ProjectId == projectId.Value);

        var tickets = await query
            .OrderByDescending(ticket => ticket.CreatedAt)
            .ToListAsync(cancellationToken);

        return Ok(tickets);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetTicket(
        int id,
        CancellationToken cancellationToken)
    {
        var ticket = await _db.Tickets
            .AsNoTracking()
            .FirstOrDefaultAsync(ticket => ticket.TicketId == id, cancellationToken);

        return ticket is null ? NotFound() : Ok(ticket);
    }
}