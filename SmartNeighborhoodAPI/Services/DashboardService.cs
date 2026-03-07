using Microsoft.EntityFrameworkCore;
using SmartNeighborhoodAPI.Helpers.DTOs.Dashboard;
using SmartNeighborhoodAPI.Entites.Enums;
using SmartNeighborhoodAPI.Interfaces;
using SmartNeighborhoodAPI.Models;
using System.Threading.Tasks;

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
            var results = await _context.Database
                .SqlQuery<DashboardStatusResult>($"EXEC [dbo].[sp_GetDashboardStatus] @NeighborhoodId = {neighborhoodId}, @ResidentialUnitId = {residentialUnitId}")
                .ToListAsync();

            var result = results.Count > 0
                ? results[0]
                : new DashboardStatusResult();

            return MapToDashboardStats(result);
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

        private static DashboardStatsDto MapToDashboardStats(DashboardStatusResult result)
        {
            return new DashboardStatsDto
            {
                SocialAndFamily = new SocialFamilyStatsDto
                {
                    Families = result.FamiliesCount,
                    Individuals = result.IndividualsCount,
                    Divorced = result.DivorcedCount,
                    Widows = result.WidowsCount
                },
                Agreements = new AgreementStatsDto
                {
                    Completed = result.AgreementsCompleted,
                    NotCompleted = result.AgreementsNotCompleted,
                    PeaceCompleted = result.PeaceCompleted,
                    PeaceNotCompleted = result.PeaceNotCompleted,
                    TreatiesCompleted = result.TreatiesCompleted,
                    TreatiesNotCompleted = result.TreatiesNotCompleted,
                    AgreementsCompleted = result.AgreementsTypeCompleted,
                    AgreementsNotCompleted = result.AgreementsTypeNotCompleted
                },
                Projects = new ProjectStatsDto
                {
                    Completed = result.ProjectsCompleted,
                    Incomplete = result.ProjectsIncomplete
                },
                Teams = new TeamsStatsDto
                {
                    TeamsCount = result.TeamsCount
                },
                PopulationStatus = new PopulationStatusDto
                {
                    Residents = result.ResidentsCount,
                    Displaced = result.DisplacedCount
                },
                IncomeCategories = new IncomeCategoriesDto
                {
                    CategoryA = result.CategoryACount,
                    CategoryB = result.CategoryBCount,
                    CategoryC = result.CategoryCCount
                },
                Health = new HealthStatsDto
                {
                    IndividualsWithChronicDiseases = result.ChronicDiseasesCount
                },
                Housing = new HousingStatsDto
                {
                    Rented = result.HousingRentedCount,
                    Owned = result.HousingOwnedCount
                }
            };
        }
    }
}
