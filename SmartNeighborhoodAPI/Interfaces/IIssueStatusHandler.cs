using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Helpers;
using SmartNeighborhoodAPI.Helpers.DTOs.Issue;

namespace SmartNeighborhoodAPI.Interfaces
{
    public interface IIssueStatusHandler
    {
        Task<ApiResponse<bool>> HandleAsync(Issue issue, UpdateIssueDto dto);
    }
}
