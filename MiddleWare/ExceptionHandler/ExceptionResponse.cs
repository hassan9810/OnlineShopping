namespace Store.MiddleWare.ExceptionHandler
{
    public class ExceptionResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string? ErrorMessage { get; set; }
        public List<string>? Errors { get; set; } = new List<string>();

        public ExceptionResponse(HttpStatusCode statusCode, string errorMessage, List<string>? errors = null)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
            Errors = errors;
        }
    }
}
