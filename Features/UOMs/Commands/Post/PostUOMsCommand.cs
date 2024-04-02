namespace Store.Features.UOMs.Commands.Post
{
    public class PostUOMsCommand : IRequest<ResponseDto>
    {
        public string UnitMeasure { get; set; }
        public string Description { get; set; }
    }
}
