using TaekwondoApp.Shared.Helper;
namespace TaekwondoOrchestration.ApiService.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled Exception");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;

                var response = ApiResponse<string>.Fail("An unexpected error occurred.", 500);
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }

}
