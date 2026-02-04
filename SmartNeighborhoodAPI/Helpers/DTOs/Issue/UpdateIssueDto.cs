using System.Collections.Generic;

namespace SmartNeighborhoodAPI.Helpers.DTOs.Issue
{
    public class UpdateIssueDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? Priority { get; set; }
        public string? Status { get; set; }
        public string? AssigneeId { get; set; }
        public List<string>? Attachments { get; set; }
    }
}
