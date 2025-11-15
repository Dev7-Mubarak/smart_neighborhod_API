using SmartNeighborhoodAPI.Entites;

namespace SmartNeighborhoodAPI.Interfaces
{
    public interface IHierarchyService
    {
        Task<HashSet<int>> GetAllowedBlockIdsAsync();
    }
}
