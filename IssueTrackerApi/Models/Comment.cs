using System;
using System.Collections.Generic;

namespace IssueTrackerApi.Models;

public partial class Comment
{
    public int CommentId { get; set; }

    public int TicketId { get; set; }

    public int AuthorId { get; set; }

    public string Body { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual User Author { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;
}
