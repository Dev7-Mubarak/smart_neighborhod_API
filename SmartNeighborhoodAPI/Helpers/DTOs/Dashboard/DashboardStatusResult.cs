namespace SmartNeighborhoodAPI.Helpers.DTOs.Dashboard
{
    public sealed class DashboardStatusResult
    {
        public int FamiliesCount { get; set; }
        public int IndividualsCount { get; set; }
        public int DivorcedCount { get; set; }
        public int WidowsCount { get; set; }
        public int AgreementsCompleted { get; set; }
        public int AgreementsNotCompleted { get; set; }
        public int PeaceCompleted { get; set; }
        public int PeaceNotCompleted { get; set; }
        public int TreatiesCompleted { get; set; }
        public int TreatiesNotCompleted { get; set; }
        public int AgreementsTypeCompleted { get; set; }
        public int AgreementsTypeNotCompleted { get; set; }
        public int ProjectsCompleted { get; set; }
        public int ProjectsIncomplete { get; set; }
        public int TeamsCount { get; set; }
        public int ResidentsCount { get; set; }
        public int DisplacedCount { get; set; }
        public int CategoryACount { get; set; }
        public int CategoryBCount { get; set; }
        public int CategoryCCount { get; set; }
        public int ChronicDiseasesCount { get; set; }
        public int HousingRentedCount { get; set; }
        public int HousingOwnedCount { get; set; }
    }
}