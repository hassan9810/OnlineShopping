namespace Store.Dto
{
    public abstract class ParentResponseDto
    {
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
    }
}
