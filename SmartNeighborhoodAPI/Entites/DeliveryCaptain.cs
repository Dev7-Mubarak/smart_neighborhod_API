namespace SmartNeighborhoodAPI.Entites
{
    public class DeliveryCaptain
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FinancialAccountName { get; set; }
        public string? FinancialAccountNumber { get; set; }
    }

    public class DeliveryCaptainDto
    {
        public string FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FinancialAccountName { get; set; }
        public string? FinancialAccountNumber { get; set; }
    }
}
