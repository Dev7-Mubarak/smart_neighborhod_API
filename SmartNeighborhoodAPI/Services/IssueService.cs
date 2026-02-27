using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Entites.Enums;
using SmartNeighborhoodAPI.Helpers;
using SmartNeighborhoodAPI.Helpers.DTOs.Issue;
using SmartNeighborhoodAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SmartNeighborhoodAPI.Services
{
    public class IssueService : IIssueService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public IssueService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResponse<PaginatedResult<IssueDto>>> GetAllIssuesAsync(string? status, string? priority, string? category, string? sortBy, string? sortOrder, int page, int limit)
        {
            var query = _context.Issues.AsQueryable();

            if (!string.IsNullOrEmpty(status) && Enum.TryParse<IssueStatus>(status, true, out var statusEnum))
            {
                query = query.Where(i => i.Status == statusEnum);
            }

            if (!string.IsNullOrEmpty(priority) && Enum.TryParse<IssuePriority>(priority, true, out var priorityEnum))
            {
                query = query.Where(i => i.Priority == priorityEnum);
            }

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(i => i.Category.ToLower() == category.ToLower());
            }

            // Sorting
            if (!string.IsNullOrEmpty(sortBy))
            {
                var descending = sortOrder?.ToLower() == "desc";
                query = sortBy.ToLower() switch
                {
                    "title" => descending ? query.OrderByDescending(i => i.Title) : query.OrderBy(i => i.Title),
                    "priority" => descending ? query.OrderByDescending(i => i.Priority) : query.OrderBy(i => i.Priority),
                    "status" => descending ? query.OrderByDescending(i => i.Status) : query.OrderBy(i => i.Status),
                    _ => query.OrderBy(i => i.Id),
                };
            }

            var totalItems = await query.CountAsync();
            var issues = await query.Skip((page - 1) * limit).Take(limit).ToListAsync();
            var issueDtos = _mapper.Map<List<IssueDto>>(issues);

            var result = new PaginatedResult<IssueDto>(issueDtos, totalItems, page, limit);
            return ApiResponse<PaginatedResult<IssueDto>>.Success(result);
        }

        public async Task<ApiResponse<IssueDto>> GetIssueByIdAsync(Guid id)
        {
            var issue = await _context.Issues.FindAsync(id);
            if (issue == null)
            {
                return ApiResponse<IssueDto>.Error(HttpStatusCode.NotFound, "Issue not found");
            }
            var issueDto = _mapper.Map<IssueDto>(issue);
            return ApiResponse<IssueDto>.Success(issueDto);
        }

        public async Task<ApiResponse<IssueDto>> CreateIssueAsync(CreateIssueDto createIssueDto, string reporterId)
        {
            var issue = _mapper.Map<Issue>(createIssueDto);
            issue.ReporterId = reporterId;
            issue.Status = IssueStatus.Open;

            _context.Issues.Add(issue);
            await _context.SaveChangesAsync();

            var issueDto = _mapper.Map<IssueDto>(issue);
            return ApiResponse<IssueDto>.Success(issueDto, "Issue created successfully");
        }

        public async Task<ApiResponse<IssueDto>> UpdateIssueAsync(Guid id, UpdateIssueDto updateIssueDto)
        {
            var issue = await _context.Issues.FindAsync(id);
            if (issue == null)
            {
                return ApiResponse<IssueDto>.Error(HttpStatusCode.NotFound, "Issue not found");
            }

            _mapper.Map(updateIssueDto, issue);

            if (updateIssueDto.Status?.ToLower() == "resolved")
            {
                issue.ResolvedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
            var issueDto = _mapper.Map<IssueDto>(issue);
            return ApiResponse<IssueDto>.Success(issueDto, "Issue updated successfully");
        }

        public async Task<ApiResponse<bool>> DeleteIssueAsync(Guid id)
        {
            var issue = await _context.Issues.FindAsync(id);
            if (issue == null)
            {
                return ApiResponse<bool>.Error(HttpStatusCode.NotFound, "Issue not found");
            }

            _context.Issues.Remove(issue);
            await _context.SaveChangesAsync();
            return ApiResponse<bool>.Success(true, "Issue deleted successfully");
        }
    }
}
