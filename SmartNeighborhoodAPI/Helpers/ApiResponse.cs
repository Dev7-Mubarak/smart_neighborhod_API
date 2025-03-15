using System.Net;

namespace SmartNeighborhoodAPI.Helpers
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
        public List<ErrorDetails>? Errors { get; set; }


        public static ApiResponse<T> Success(T data = default, string message = "")
        {
            return new ApiResponse<T>
            {
                IsSuccess = true,
                Data = data,
                Message = message,
                StatusCode = HttpStatusCode.OK,
            };
        }

        public static ApiResponse<T> Error(HttpStatusCode statusCode, string message = "", List<ErrorDetails>? errors = null)
        {
            return new ApiResponse<T>
            {
                IsSuccess = false,
                Message = message,
                StatusCode = statusCode,
                Errors = errors
            };
        }
    }

}
