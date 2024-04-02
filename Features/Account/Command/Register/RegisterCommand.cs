namespace Store.Features.Account.Command.Register
{
    public class RegisterCommand : IRequest<ResponseDto>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please Enter Valid UserName")]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [EmailAddress(ErrorMessage = "Please Enter Valid Email")]
        public string Email { get; set; }
    }
}
