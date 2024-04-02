namespace Store.Features.UOMs.Commands.Delete
{
    public class DeleteUOMsCommand : IRequest<ResponseDto>
    {
        public int UomId { get; set; }

        public DeleteUOMsCommand(int uomId)
        {
            UomId = uomId;
        }
    }
}
