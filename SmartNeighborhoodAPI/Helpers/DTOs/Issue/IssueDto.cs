using SmartNeighborhoodAPI.Entites.Enums;
using System;
using System.Collections.Generic;

namespace SmartNeighborhoodAPI.Helpers.DTOs.Issue
{
    public class IssueDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public IssuePriority Priority { get; set; }
        public IssueStatus Status { get; set; }
        public string ReporterId { get; set; }
        public string? AssigneeId { get; set; }
        public List<string> Attachments { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? ResolvedAt { get; set; }
    }
}
