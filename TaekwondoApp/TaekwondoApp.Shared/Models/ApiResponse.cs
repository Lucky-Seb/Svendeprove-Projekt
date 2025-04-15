using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaekwondoApp.Shared.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }
        public int StatusCode { get; set; }
        public DateTime Timestamp { get; set; }

        public ApiResponse()
        {
            Timestamp = DateTime.UtcNow;
        }

        public static ApiResponse<T> Ok(T data, int statusCode = 200)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Data = data,
                StatusCode = statusCode
            };
        }

        public static ApiResponse<T> Fail(List<string> errors, int statusCode = 400)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Errors = errors,
                StatusCode = statusCode
            };
        }

        public static ApiResponse<T> Fail(string error, int statusCode = 400)
        {
            return Fail(new List<string> { error }, statusCode);
        }
    }
}
