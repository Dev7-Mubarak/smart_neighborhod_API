namespace SmartNeighborhoodAPI.Helpers
{
    public class ErrorDetails
    {
        public ErrorDetails(string field, string errorMessage)
        {
            Field = field;
            ErrorMessage = errorMessage;
        }

        public string Field { get; set; }
        public string ErrorMessage { get; set; }

    }

}
