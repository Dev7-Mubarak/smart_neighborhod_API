namespace SmartNeighborhoodAPI.Entites
{
    public class ResidentialUnit
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string UnitManagerId { get; set; }
        public AppUser UnitManager { get; set; }

        public ICollection<Block> Blocks { get; set; } = new List<Block>();
    }
}
