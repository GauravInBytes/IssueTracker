using System;
using System.Collections.Generic;

namespace IssueTrackerApi.Models;

public partial class TicketHistory
{
    public int HistoryId { get; set; }

    public int TicketId { get; set; }

    public int ActorId { get; set; }

    public string FieldName { get; set; } = null!;

    public string? OldValue { get; set; }

    public string? NewValue { get; set; }

    public DateTime ChangedAt { get; set; }

    public virtual User Actor { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;
}
