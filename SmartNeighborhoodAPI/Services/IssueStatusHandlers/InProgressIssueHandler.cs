using AutoMapper;
using Microsoft.Extensions.Logging;
using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Helpers;
using SmartNeighborhoodAPI.Helpers.DTOs.Issue;
using SmartNeighborhoodAPI.Interfaces;

namespace SmartNeighborhoodAPI.Services.IssueStatusHandlers
{
    public class InProgressIssueHandler : IIssueStatusHandler
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<InProgressIssueHandler> _logger;

        public InProgressIssueHandler(ApplicationDbContext context, IMapper mapper, ILogger<InProgressIssueHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public Task<ApiResponse<bool>> HandleAsync(Issue issue, UpdateIssueDto dto)
        {
            _logger.LogInformation("Issue {IssueId} transitioned to InProgress status.", issue.Id);
            return Task.FromResult(ApiResponse<bool>.Success(true));
        }
    }
}
