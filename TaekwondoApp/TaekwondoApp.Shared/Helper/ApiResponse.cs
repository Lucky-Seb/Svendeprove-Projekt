namespace TaekwondoApp.Shared.Helper
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }
        public int StatusCode { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public static ApiResponse<T> Ok(T data, int statusCode = 200) =>
            new() { Success = true, Data = data, StatusCode = statusCode };

        public static ApiResponse<T> Fail(string error, int statusCode = 400) =>
            new() { Success = false, Errors = new List<string> { error }, StatusCode = statusCode };

        public static ApiResponse<T> Fail(List<string> errors, int statusCode = 400) =>
            new() { Success = false, Errors = errors, StatusCode = statusCode };
    }
}
