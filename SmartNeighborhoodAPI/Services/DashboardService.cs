using Microsoft.EntityFrameworkCore;
using SmartNeighborhoodAPI.Helpers.DTOs.Dashboard;
using SmartNeighborhoodAPI.Interfaces;
using SmartNeighborhoodAPI.Entites.Enums;
using SmartNeighborhoodAPI.Models;
using System.Linq;
using System.Threading.Tasks;
using SmartNeighborhoodAPI.Entites;
using OurProjectSmartNeiborhood.Entites;
using static SmartNeighborhoodAPI.Helpers.Router;

namespace SmartNeighborhoodAPI.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;
        private readonly IReportGeneratorFactory _reportGeneratorFactory;

        public DashboardService(ApplicationDbContext context, IReportGeneratorFactory reportGeneratorFactory)
        {
            _context = context;
            _reportGeneratorFactory = reportGeneratorFactory;
        }

        public async Task<DashboardStatsDto> GetDashboardStatsAsync(int? neighborhoodId = null, int? residentialUnitId = null)
        {
            var dto = new DashboardStatsDto();

            // Build family filter by neighborhood or residential unit
            IQueryable<OurProjectSmartNeiborhood.Entites.Family> familiesQuery = _context.Families.Include(f => f.Block).ThenInclude(b => b.ResidentialUnit);

            if (residentialUnitId.HasValue)
            {
                familiesQuery = familiesQuery.Where(f => f.Block.ResidentialUnitId == residentialUnitId.Value);
            }
            else if (neighborhoodId.HasValue)
            {
                familiesQuery = familiesQuery.Where(f => f.Block.ResidentialUnit.ResidentialNeighborhoodId == neighborhoodId.Value);
            }

            var familyIds = await familiesQuery.Select(f => f.Id).ToListAsync();

            // Families count
            dto.SocialAndFamily.Families = familyIds.Count;

            // Individuals (persons) via family members
            var personIdsQuery = _context.FamilyMembers.Where(fm => familyIds.Contains(fm.FamilyId)).Select(fm => fm.PersonId).Distinct();
            var personIds = await personIdsQuery.ToListAsync();
            dto.SocialAndFamily.Individuals = personIds.Count;

            // Social stats: divorced (assume enum values: 3=Divorced, 4=Widowed per DTO docs)
            dto.SocialAndFamily.Divorced = await _context.People.Where(p => personIds.Contains(p.Id) && p.MaritalStatus == MaritalStatus.Divorced).CountAsync();
            dto.SocialAndFamily.Widows = await _context.People.Where(p => personIds.Contains(p.Id) && p.MaritalStatus == MaritalStatus.Widowed).CountAsync();

            // Agreements -> ProjectFamilies linked to families
            var projFamiliesQuery = _context.ProjectFamilies.Where(pf => familyIds.Contains(pf.FamilyID));
            var totalAgreements = await projFamiliesQuery.CountAsync();
            var completedAgreements = await projFamiliesQuery.Include(pf => pf.Project).Where(pf => pf.Project.ProjectStatus == ProjectStatus.Completed).CountAsync();
            dto.Agreements.Completed = completedAgreements;
            dto.Agreements.NotCompleted = totalAgreements - completedAgreements;

            // Conflict breakdown by ConfilctType (1=صلح, 2=معاهدات, 3=اتفاقيات)
            var familyMemberIds = await _context.FamilyMembers.Where(fm => familyIds.Contains(fm.FamilyId)).Select(fm => fm.Id).ToListAsync();
            var conflictQuery = _context.Set<ConflictCase>().Where(cc => familyMemberIds.Contains(cc.FirstPartyId) || familyMemberIds.Contains(cc.SecondPartyId));

            // Type 1: Peace (صلح)
            var totalType1 = await conflictQuery.Where(cc => cc.ConflictTypeId == 1).CountAsync();
            var resolvedType1 = await conflictQuery.Where(cc => cc.ConflictTypeId == 1 && cc.IsResolved).CountAsync();
            dto.Agreements.PeaceCompleted = resolvedType1;
            dto.Agreements.PeaceNotCompleted = totalType1 - resolvedType1;

            // Type 2: Treaties (معاهدات)
            var totalType2 = await conflictQuery.Where(cc => cc.ConflictTypeId == 2).CountAsync();
            var resolvedType2 = await conflictQuery.Where(cc => cc.ConflictTypeId == 2 && cc.IsResolved).CountAsync();
            dto.Agreements.TreatiesCompleted = resolvedType2;
            dto.Agreements.TreatiesNotCompleted = totalType2 - resolvedType2;

            // Type 3: Agreements (اتفاقيات)
            var totalType3 = await conflictQuery.Where(cc => cc.ConflictTypeId == 3).CountAsync();
            var resolvedType3 = await conflictQuery.Where(cc => cc.ConflictTypeId == 3 && cc.IsResolved).CountAsync();
            dto.Agreements.AgreementsCompleted = resolvedType3;
            dto.Agreements.AgreementsNotCompleted = totalType3 - resolvedType3;

            // Projects related via ProjectFamilies or ProjectBlocks in the filtered area
            var projectIdsFromFamilies = projFamiliesQuery.Select(pf => pf.ProjectID);
            IQueryable<ProjectBlock> projectIdsFromBlocksQuery = _context.Set<ProjectBlock>().Include(pb => pb.Block).ThenInclude(b => b.ResidentialUnit);
            if (residentialUnitId.HasValue)
            {
                projectIdsFromBlocksQuery = projectIdsFromBlocksQuery.Where(pb => pb.Block.ResidentialUnitId == residentialUnitId.Value);
            }
            else if (neighborhoodId.HasValue)
            {
                projectIdsFromBlocksQuery = projectIdsFromBlocksQuery.Where(pb => pb.Block.ResidentialUnit.ResidentialNeighborhoodId == neighborhoodId.Value);
            }
            var projectIdsFromBlocks = projectIdsFromBlocksQuery.Select(pb => pb.ProjectId);

            var projectIds = await projectIdsFromFamilies.Union(projectIdsFromBlocks).Distinct().ToListAsync();

            if (projectIds.Any())
            {
                dto.Projects.Completed = await _context.Projects.Where(p => projectIds.Contains(p.Id) && p.ProjectStatus == ProjectStatus.Completed).CountAsync();
                dto.Projects.Incomplete = await _context.Projects.Where(p => projectIds.Contains(p.Id) && p.ProjectStatus != ProjectStatus.Completed).CountAsync();
            }
            else
            {
                // If no filters or no linked projects, provide global counts when no filter provided
                if (!neighborhoodId.HasValue && !residentialUnitId.HasValue)
                {
                    dto.Projects.Completed = await _context.Projects.Where(p => p.ProjectStatus == ProjectStatus.Completed).CountAsync();
                    dto.Projects.Incomplete = await _context.Projects.Where(p => p.ProjectStatus != ProjectStatus.Completed).CountAsync();
                }
                else
                {
                    dto.Projects.Completed = 0;
                    dto.Projects.Incomplete = 0;
                }
            }

            // Teams: teams linked to filtered projects or teams with members in filtered persons
            var teamIdsFromProjectTeam = _context.ProjectTeams.Where(pt => projectIds.Contains(pt.ProjectId)).Select(pt => pt.TeamId);
            var teamIdsFromMembers = _context.TeamMembers.Where(tm => personIds.Contains(tm.PersonId)).Select(tm => tm.TeamId);
            var teamIds = await teamIdsFromProjectTeam.Union(teamIdsFromMembers).Distinct().ToListAsync();
            dto.Teams.TeamsCount = teamIds.Count;

            // Population status
            dto.PopulationStatus.Residents = await _context.People.Where(p => personIds.Contains(p.Id) && p.ResidencyStatus == Entites.Enums.ResidencyStatus.Resident).CountAsync();
            dto.PopulationStatus.Displaced = await _context.People.Where(p => personIds.Contains(p.Id) && p.ResidencyStatus == Entites.Enums.ResidencyStatus.Displaced).CountAsync();

            // Income categories for families
            dto.IncomeCategories.CategoryA = await familiesQuery.Where(f => f.FamilyCatgoryId == 1).CountAsync();
            dto.IncomeCategories.CategoryB = await familiesQuery.Where(f => f.FamilyCatgoryId == 2).CountAsync();
            dto.IncomeCategories.CategoryC = await familiesQuery.Where(f => f.FamilyCatgoryId == 3).CountAsync();

            // Health: individuals with chronic diseases
            //dto.Health.IndividualsWithChronicDiseases = await _context.People.Where(p => personIds.Contains(p.Id) && p.HasChronicDiseases == true).CountAsync();
            dto.Health.IndividualsWithChronicDiseases = await _context.People.Where(p => personIds.Contains(p.Id) && p.HasChronicDiseases == true).CountAsync();

            dto.Housing.Rented = await familiesQuery.Where(f => f.HousingType == Entites.Enums.HousingType.Rent).CountAsync();
            dto.Housing.Owned = await familiesQuery.Where(f => f.HousingType == Entites.Enums.HousingType.Property).CountAsync();



            return dto;
        }

        public async Task<byte[]> ExportDashboardStatsAsync(
            ReportFormat format,
            int? neighborhoodId = null,
            int? residentialUnitId = null)
        {
            var stats = await GetDashboardStatsAsync(neighborhoodId, residentialUnitId);

            var parameters = new ReportParameters
            {
                Title = $"Dashboard Stats Report" +
                        (neighborhoodId.HasValue ? $" — Neighborhood #{neighborhoodId}" : string.Empty) +
                        (residentialUnitId.HasValue ? $" — Unit #{residentialUnitId}" : string.Empty),
                Data = stats
            };

            var generator = _reportGeneratorFactory.Create(format);
            return await generator.GenerateAsync(parameters);
        }
    }
}
