using Microsoft.Data.SqlClient;
using TaekwondoApp.Shared.Helper;
using System.Net;
using System.Text.Json;

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
            catch (SqlException sqlEx) when (sqlEx.Number == 53) // SQL Server error code for "Server not found"
            {
                _logger.LogError(sqlEx, "Database connection failure");

                // Set a 503 Service Unavailable status code and return a custom message for DB issues
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable; // 503

                var response = ApiResponse<string>.Fail("The database is temporarily unavailable. Please try again later.", 503);
                await context.Response.WriteAsJsonAsync(response);
            }
            catch (SqlException sqlEx)
            {
                // Handle other SQL related errors
                _logger.LogError(sqlEx, "SQL Exception occurred");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;

                var response = ApiResponse<string>.Fail("A database error occurred. Please contact support.", 500);
                await context.Response.WriteAsJsonAsync(response);
            }
            catch (KeyNotFoundException knfEx)
            {
                // Handle not found errors (e.g., resource not found)
                _logger.LogError(knfEx, "Resource not found");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;

                var response = ApiResponse<string>.Fail("The requested resource was not found.", 404);
                await context.Response.WriteAsJsonAsync(response);
            }
            catch (UnauthorizedAccessException unauthEx)
            {
                // Handle unauthorized access errors
                _logger.LogError(unauthEx, "Unauthorized access attempt");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized; // 401

                var response = ApiResponse<string>.Fail("You are not authorized to access this resource.", 401);
                await context.Response.WriteAsJsonAsync(response);
            }
            catch (ArgumentException argEx)
            {
                // Handle invalid input errors
                _logger.LogError(argEx, "Invalid argument provided");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest; // 400

                var response = ApiResponse<string>.Fail("The input provided is invalid. Please check the request parameters.", 400);
                await context.Response.WriteAsJsonAsync(response);
            }
            catch (JsonException jsonEx)
            {
                // Handle JSON parsing errors
                _logger.LogError(jsonEx, "Error parsing JSON");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                var response = ApiResponse<string>.Fail("There was an error processing your request. Please ensure the request format is correct.", 400);
                await context.Response.WriteAsJsonAsync(response);
            }
            catch (Exception ex)
            {
                // Catch any other exceptions
                _logger.LogError(ex, "Unhandled exception");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;

                var response = ApiResponse<string>.Fail("An unexpected error occurred. Please try again later.", 500);
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
