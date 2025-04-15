using Microsoft.AspNetCore.Mvc;

namespace TaekwondoOrchestration.ApiService.Controllers
{
    [ApiController]
    public abstract class ApiBaseController : ControllerBase
    {
        protected ActionResult<ApiResponse<T>> OkResponse<T>(T data) =>
            Ok(ApiResponse<T>.Ok(data));

        protected ActionResult<ApiResponse<T>> NotFoundResponse<T>(string message) =>
            NotFound(ApiResponse<T>.Fail(message, 404));

        protected ActionResult<ApiResponse<T>> CreatedResponse<T>(string routeName, object routeValues, T data) =>
            CreatedAtAction(routeName, routeValues, ApiResponse<T>.Ok(data, 201));
    }

}
