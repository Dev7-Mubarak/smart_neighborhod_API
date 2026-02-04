using System.Collections.Generic;

namespace SmartNeighborhoodAPI.Helpers.DTOs.Dashboard
{
    public class DashboardStatsDto
    {
        public SocialFamilyStatsDto SocialAndFamily { get; set; } = new SocialFamilyStatsDto();
        public AgreementStatsDto Agreements { get; set; } = new AgreementStatsDto();
        public ProjectStatsDto Projects { get; set; } = new ProjectStatsDto();
        public TeamsStatsDto Teams { get; set; } = new TeamsStatsDto();
        public PopulationStatusDto PopulationStatus { get; set; } = new PopulationStatusDto();
        public IncomeCategoriesDto IncomeCategories { get; set; } = new IncomeCategoriesDto();
        public HealthStatsDto Health { get; set; } = new HealthStatsDto();
        public HousingStatsDto Housing { get; set; } = new HousingStatsDto();
    }

    public class SocialFamilyStatsDto
    {
        public int Divorced { get; set; }
        public int Widows { get; set; }
        public int Families { get; set; }
        public int Individuals { get; set; }
    }

    public class AgreementStatsDto
    {
        public int Completed { get; set; }
        public int NotCompleted { get; set; }

        // Breakdown by conflict type (seeded ConfilctType ids: 1=صلح, 2=معاهدات, 3=اتفاقيات)
        public int PeaceCompleted { get; set; }
        public int PeaceNotCompleted { get; set; }

        public int TreatiesCompleted { get; set; }
        public int TreatiesNotCompleted { get; set; }

        public int AgreementsCompleted { get; set; }
        public int AgreementsNotCompleted { get; set; }
    }

    public class ProjectStatsDto
    {
        public int Completed { get; set; }
        public int Incomplete { get; set; }
    }

    public class TeamsStatsDto
    {
        public int TeamsCount { get; set; }
    }

    public class PopulationStatusDto
    {
        public int Residents { get; set; }
        public int Displaced { get; set; }
    }

    public class IncomeCategoriesDto
    {
        public int CategoryA { get; set; }
        public int CategoryB { get; set; }
        public int CategoryC { get; set; }
    }

    public class HealthStatsDto
    {
        public int IndividualsWithChronicDiseases { get; set; }
    }

    public class HousingStatsDto
    {
        public int Rented { get; set; }
        public int Owned { get; set; }
    }


}
