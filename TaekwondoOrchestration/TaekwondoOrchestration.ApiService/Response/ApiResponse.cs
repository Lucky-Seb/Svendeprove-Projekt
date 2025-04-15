namespace TaekwondoOrchestration.ApiService.Response
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public int StatusCode { get; set; }  // Optional, can be used for more detailed control over status codes

        // Constructor for success
        public ApiResponse(T data, string message = null)
        {
            Data = data;
            Message = message;
            Success = true;
            StatusCode = 200;  // Default to OK (200)
        }

        // Constructor for error
        public ApiResponse(string message, int statusCode = 400)
        {
            Data = default;
            Message = message;
            Success = false;
            StatusCode = statusCode;
        }
    }
}
