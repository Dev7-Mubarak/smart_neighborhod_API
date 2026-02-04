using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartNeighborhoodAPI.Helpers.DTOs.Issue
{
    public class CreateIssueDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Priority { get; set; }
        public List<string>? Attachments { get; set; }
    }
}
