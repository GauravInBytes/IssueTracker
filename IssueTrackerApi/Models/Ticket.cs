using System;
using System.Collections.Generic;

namespace IssueTrackerApi.Models;

public partial class Ticket
{
    public int TicketId { get; set; }

    public int ProjectId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string Status { get; set; } = null!;

    public string Priority { get; set; } = null!;

    public int? AssigneeId { get; set; }

    public int CreatedBy { get; set; }

    public DateOnly? DueDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public virtual ProjectMember? ProjectMember { get; set; }

    public virtual ICollection<TicketHistory> TicketHistories { get; set; } = new List<TicketHistory>();
}
