using IssueTrackerApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace IssueTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class TicketsController : ControllerBase
{
    private readonly IssueTrackerDbContext _db;

    public TicketsController(IssueTrackerDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTicketsByProjectId([FromQuery] int? projectId, CancellationToken cancellationToken)
    {
        var query = _db.Tickets.AsNoTracking();

        if (projectId.HasValue)
            query = query.Where(ticket => ticket.ProjectId == projectId.Value);

        var tickets = await query
            .AsNoTracking()
            .OrderByDescending(t => t.CreatedAt)
            .Select(t => new
            {
                t.TicketId,
                t.ProjectId,
                t.Title,
                t.Description,
                t.Status,
                t.Priority,
                t.AssigneeId,
                t.CreatedBy,
                t.DueDate,
                t.CreatedAt,
                Creator = new
                {
                    t.CreatedByNavigation.UserId,
                    t.CreatedByNavigation.FullName
                },
                AssignedTo = t.ProjectMember == null ? null : new
                {
                    t.ProjectMember.User.UserId,
                    t.ProjectMember.User.FullName
                },
                Project = new
                {
                    t.Project.ProjectId,
                    t.Project.Name,
                }
            })
            .ToListAsync(cancellationToken);

        return Ok(tickets);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetTicketById(int id, CancellationToken cancellationToken)
    {
        var ticket = await _db.Tickets
            .AsNoTracking()
            .Where(t => t.TicketId == id)
            .Select(t => new
            {
                t.TicketId,
                t.ProjectId,
                t.Title,
                t.Description,
                t.Status,
                t.Priority,
                t.DueDate,
                t.CreatedAt,
                t.UpdatedAt,
                Creator = new
                {
                    t.CreatedByNavigation.UserId,
                    t.CreatedByNavigation.FullName
                },
                Comments = t.Comments
                    .OrderBy(c => c.CreatedAt)
                    .Select(c => new
                    {
                        c.CommentId,
                        c.Body,
                        c.CreatedAt,
                        Author = new
                        {
                            c.Author.UserId,
                            c.Author.FullName
                        }
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        return ticket is null ? NotFound() : Ok(ticket);
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> UpdateTicket(
        int id,
        [FromBody] JsonElement body,
        CancellationToken cancellationToken)
    {
        var label = body.GetProperty("label").GetString();
        var value = body.GetProperty("value").GetString();

        if (string.IsNullOrWhiteSpace(label) || value is null)
            return BadRequest("Provide label and value.");

        var ticket = await _db.Tickets
            .FirstOrDefaultAsync(t => t.TicketId == id, cancellationToken);

        if (ticket is null)
            return NotFound();

        switch (label.ToLowerInvariant())
        {
            case "title":
                ticket.Title = value;
                break;
            case "description":
                ticket.Description = value;
                break;
            case "status":
                ticket.Status = value;
                break;
            case "priority":
                ticket.Priority = value;
                break;
            default:
                return BadRequest("This field cannot be updated.");
        }

        ticket.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync(cancellationToken);

        return NoContent();
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> UpdateComment(int id, [FromBody] JsonElement body, CancellationToken cancellationToken)
    {
        
        return NoContent();
    }
}