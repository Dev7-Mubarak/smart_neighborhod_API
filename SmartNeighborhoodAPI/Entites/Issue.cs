using OurProjectSmartNeiborhood.Entites;
using SmartNeighborhoodAPI.Entites.Enums;
using System;
using System.Collections.Generic;

namespace SmartNeighborhoodAPI.Entites
{
    public class Issue
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public IssuePriority Priority { get; set; }
        public IssueStatus Status { get; set; }
        public string ReporterId { get; set; }
        public AppUser Reporter { get; set; }
        public string? AssigneeId { get; set; }
        public AppUser? Assignee { get; set; }
        public List<string> Attachments { get; set; } = new List<string>();
        public DateTime? ResolvedAt { get; set; }

        // ── Row-Level Security anchor ─────────────────────────────────────────
        // Connects an Issue to its physical Block so that the EF Core global
        // query filter can traverse: Issue → Block → ResidentialUnit → Neighbourhood.
        public int? BlockId { get; set; }
        public Block? Block { get; set; }
    }
}
