using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Interfaces;
using System.Collections.Generic;

namespace SmartNeighborhoodAPI.Services
{
    public class HierarchyService : IHierarchyService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserContextService _userContextService;

        public HierarchyService(ApplicationDbContext context, UserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }

        public async Task<HashSet<int>> GetAllowedBlockIdsAsync()
        {

            var currentUser = _userContextService.GetCurrentUser();
            if (currentUser.Role != Role.BlockManager)
            {
                var allIds = await _context.Blocks
                    .Select(b => b.Id)
                    .ToListAsync();

                return new HashSet<int>(allIds);
            }

            var rootBlockIds = await _context.Blocks
                .Where(b => b.UnitManagerId == currentUser.Id)
                .Select(b => b.Id)
                .ToListAsync();

            if (!rootBlockIds.Any())
                return new HashSet<int>();

            var flatBlocks = await _context.Blocks
                .Select(b => new { b.Id, b.BlockManagerId })
                .ToListAsync();

            // 3) BFS to collect all descendants
            var allowedBlockIds = new HashSet<int>(rootBlockIds);
            var queue = new Queue<int>(rootBlockIds);

            while (queue.Count > 0)
            {
                var currentId = queue.Dequeue();

                var children = flatBlocks
                    .Where(x => x.BlockManagerId == currentId)
                    .Select(x => x.Id);

                foreach (var childId in children)
                {
                    if (allowedBlockIds.Add(childId))
                        queue.Enqueue(childId);
                }
            }

            return allowedBlockIds;
        }
    }
}
