using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Entites.Enums;
using SmartNeighborhoodAPI.Interfaces;

namespace OurProjectSmartNeiborhood.Entites
{
    public class Project : ISyncable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int ProjectCatogoryId { get; set; }
        public ProjectCatogory ProjectCatogory { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ProjectStatus ProjectStatus { get; set; }
        public double? Budget { get; set; }
        public ProjectPriority ProjectPriority { get; set; }
        public int? ManagerId { get; set; }
        public AppUser? Manager { get; set; }

        public ICollection<ProjectTeam> ProjectTeams { get; set; }
        public ICollection<ProjectFamily> ProjectFamilies { get; set; }
        public ICollection<ProjectBlock> ProjectBlocks { get; set; } = new List<ProjectBlock>();

        // ISyncable implementation
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        public string? ClientId { get; set; }
    }
}
