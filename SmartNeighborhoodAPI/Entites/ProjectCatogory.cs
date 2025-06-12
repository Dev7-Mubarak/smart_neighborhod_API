using System.Text.Json.Serialization;

namespace OurProjectSmartNeiborhood.Entites
{
    public class ProjectCatogory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = "";
        [JsonIgnore]
        public List<Project> Projects { get; set; }

    }
}
