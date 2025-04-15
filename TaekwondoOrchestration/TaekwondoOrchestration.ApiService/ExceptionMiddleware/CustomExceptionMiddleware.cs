using System.Net;

namespace TaekwondoOrchestration.ApiService.ExceptionMiddleware
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionMiddleware> _logger;

        public CustomExceptionMiddleware(RequestDelegate next, ILogger<CustomExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An unhandled exception occurred.");

                // Set the response type to JSON
                httpContext.Response.ContentType = "application/json";

                // Default internal server error
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                // Standardized error response
                var apiResponse = new ApiResponse<object>("An unexpected error occurred", 500);
                await httpContext.Response.WriteAsJsonAsync(apiResponse);
            }
        }
    }
}
