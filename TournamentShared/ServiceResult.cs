using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TournamentShared
{
    public class ServiceResult<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? ErrorMessage { get; set; }
        public string? Location { get; set; }

        public ProblemDetails? ProblemDetails { get; set; }

        public ServiceResult<T> NotFound(string message) =>
            new()
            {
                Success = false,
                //ErrorMessage = message,
                ProblemDetails = new ProblemDetails()
                {
                    Type = "https://tools.ietf.org/html/rfc9110#section-15.5.5",
                    Title = ErrorMessage ?? "Not Found",
                    Status = StatusCodes.Status404NotFound,
                }
            };
        public ServiceResult<T> BadRequest(string message) =>
            new()
            {
                Success = false,
                //ErrorMessage = message,
                ProblemDetails = new ProblemDetails()
                {
                    Type = "https://tools.ietf.org/html/rfc9110#section-15.5.5",
                    Title = ErrorMessage ?? "Not Found",
                    Status = StatusCodes.Status404NotFound,
                }
            };

        public static ServiceResult<T> Ok(T data) =>
            new() { Success = true, Data = data };
        public static ServiceResult<T> CreatedAtAction(T data, string location) =>
            new() { Success = true, Data = data, Location = location };

        public static ServiceResult<T> NoContent() =>
            new() { Success = true };
    }

}
