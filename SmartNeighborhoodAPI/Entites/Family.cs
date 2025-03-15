using System.Text.Json.Serialization;

namespace OurProjectSmartNeiborhood.Entites
{
    public class Family
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string FamilyNotes { get; set; }
        public int FamilyCatgoryId { get; set; }
        [JsonIgnore]
        public FamilyCatgory FamilyCatgory { get; set; }
        public int FamilyTypeId { get; set; }
        public FamilyType FamilyType { get; set; }
        [JsonIgnore]
        public List<FamilyMember> FamilyMembers { get; set; }
        public int BlockId { get; set; }
        [JsonIgnore]
        public Block Block { get; set; }
    }
}
