using AutoMapper;
using Microsoft.Extensions.Logging;
using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Helpers;
using SmartNeighborhoodAPI.Helpers.DTOs.Issue;
using SmartNeighborhoodAPI.Interfaces;

namespace SmartNeighborhoodAPI.Services.IssueStatusHandlers
{
    public class ResolvedIssueHandler : IIssueStatusHandler
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ResolvedIssueHandler> _logger;

        public ResolvedIssueHandler(ApplicationDbContext context, IMapper mapper, ILogger<ResolvedIssueHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public Task<ApiResponse<bool>> HandleAsync(Issue issue, UpdateIssueDto dto)
        {
            issue.ResolvedAt = DateTime.UtcNow;
            _logger.LogInformation("Issue {IssueId} resolved at {ResolvedAt}.", issue.Id, issue.ResolvedAt);
            return Task.FromResult(ApiResponse<bool>.Success(true));
        }
    }
}
