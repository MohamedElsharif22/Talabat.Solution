namespace Talabat.APIs.Errors
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }
        public ApiValidationErrorResponse(int statuscode = StatusCodes.Status400BadRequest) : base(statuscode)
        {
            Errors = new List<string>();
        }
    }
}
