using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TaekwondoApp.Shared.Helper;

namespace TaekwondoOrchestration.ApiService.Helpers
{
    public static class ResultExtensions
    {
        // Extension for Result<T> with a generic type
        public static IActionResult ToApiResponse<T>(this Result<T> result)
        {
            if (result.Success)
                return new OkObjectResult(ApiResponse<T>.Ok(result.Value!)); // Assuming Value will never be null when Success is true.
            return new BadRequestObjectResult(ApiResponse<T>.Fail(result.Errors)); // Return failure response with errors.
        }

        // Extension for Result (non-generic)
        public static IActionResult ToApiResponse(this Result result)
        {
            if (result.Success)
                return new OkObjectResult(ApiResponse<string>.Ok("Success")); // Success message
            return new BadRequestObjectResult(ApiResponse<string>.Fail(result.Errors)); // Failure with errors
        }
    }
}
