using SmartNeighborhoodAPI.Helpers;
using SmartNeighborhoodAPI.Helpers.DTOs.Issue;
using System;
using System.Threading.Tasks;

namespace SmartNeighborhoodAPI.Interfaces
{
    public interface IIssueService
    {
        Task<ApiResponse<PaginatedResult<IssueDto>>> GetAllIssuesAsync(string status, string priority, string category, string sortBy, string sortOrder, int page, int limit);
        Task<ApiResponse<IssueDto>> GetIssueByIdAsync(Guid id);
        Task<ApiResponse<IssueDto>> CreateIssueAsync(CreateIssueDto createIssueDto, string reporterId);
        Task<ApiResponse<IssueDto>> UpdateIssueAsync(Guid id, UpdateIssueDto updateIssueDto);
        Task<ApiResponse<bool>> DeleteIssueAsync(Guid id);
    }
}

