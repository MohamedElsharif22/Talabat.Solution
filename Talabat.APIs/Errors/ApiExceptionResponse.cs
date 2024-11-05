namespace Talabat.APIs.Errors
{
    public class ApiExceptionResponse(int statuscode = StatusCodes.Status500InternalServerError, string? message = null, string? details = null)
        : ApiResponse(statuscode, message)
    {
        public string? Details { get; set; } = details;
    }
}
