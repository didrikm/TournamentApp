namespace TournamentShared
{
    public class ServiceResult<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? ErrorMessage { get; set; }
        public string? Location { get; set; }

        public static ServiceResult<T> NotFound(string message) =>
            new() { Success = false, ErrorMessage = message };
        public static ServiceResult<T> BadRequest(string message) =>
            new() { Success = false, ErrorMessage = message };

        public static ServiceResult<T> Ok(T data) =>
            new() { Success = true, Data = data };
        public static ServiceResult<T> CreatedAtAction(T data, string location) =>
            new() { Success = true, Data = data, Location = location };

        public static ServiceResult<T> NoContent() =>
            new() { Success = true };
    }

}
