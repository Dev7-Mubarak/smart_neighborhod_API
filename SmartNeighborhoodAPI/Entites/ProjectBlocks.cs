namespace SmartNeighborhoodAPI.Entites
{
    public class ProjectBlock
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public int BlockId { get; set; }
        public Block Block { get; set; }
    }
}
