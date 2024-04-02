namespace Store.Features.UOMs.Commands.Put
{
    public class PutUOMsCommand : IRequest<ResponseDto>
    {
        public int UomId { get; set; }
        public PutUOMsDto UomDto { get; set; }

        public PutUOMsCommand(int uomId, PutUOMsDto uomDto)
        {
            UomId = uomId;
            UomDto = uomDto;
        }
    }
}
