using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BankOper.Business.Models
{
    public class ApiResponse<T>
    {
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public string? Error { get; set; }
        public int StatusCode { get; set; }
        private ApiResponse(T? data, string message, string? error, HttpStatusCode statusCode)
        {
            Data = data;
            Message = message;
            Error = error;
            StatusCode = (int)statusCode;
        }
        public static ApiResponse<T> SuccessResponse(T data, string message = "Success")
            => new ApiResponse<T>(data, message, null, HttpStatusCode.OK);
        public static ApiResponse<T> ValidationError(string message, string? error = null)
            => new ApiResponse<T>(default, message, error, HttpStatusCode.BadRequest);
        public static ApiResponse<T> NotFoundResponse(string message = "Resource not found")
            => new ApiResponse<T>(default, message, null, HttpStatusCode.NotFound);
        public static ApiResponse<T> ErrorResponse(string message, string? error = null)
            => new ApiResponse<T>(default, message, error, HttpStatusCode.InternalServerError);
    }

}
