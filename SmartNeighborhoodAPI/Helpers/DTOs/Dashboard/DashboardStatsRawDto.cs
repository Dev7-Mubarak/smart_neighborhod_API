namespace SmartNeighborhoodAPI.Helpers.DTOs.Dashboard
{
    public class DashboardStatsRawDto
    {
        // SocialAndFamily
        public int FamiliesCount { get; set; }
        public int IndividualsCount { get; set; }
        public int DivorcedCount { get; set; }
        public int WidowsCount { get; set; }

        // Agreements
        public int AgreementsCompleted { get; set; }
        public int AgreementsNotCompleted { get; set; }
        public int PeaceCompleted { get; set; }
        public int PeaceNotCompleted { get; set; }
        public int TreatiesCompleted { get; set; }
        public int TreatiesNotCompleted { get; set; }
        public int AgreementsTypeCompleted { get; set; }
        public int AgreementsTypeNotCompleted { get; set; }

        // Projects
        public int ProjectsCompleted { get; set; }
        public int ProjectsIncomplete { get; set; }

        // Teams
        public int TeamsCount { get; set; }

        // PopulationStatus
        public int ResidentsCount { get; set; }
        public int DisplacedCount { get; set; }

        // IncomeCategories
        public int CategoryACount { get; set; }
        public int CategoryBCount { get; set; }
        public int CategoryCCount { get; set; }

        // Health
        public int ChronicDiseasesCount { get; set; }

        // Housing
        public int HousingRentedCount { get; set; }
        public int HousingOwnedCount { get; set; }
    }
}
