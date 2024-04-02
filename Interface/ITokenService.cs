namespace Store.Interface
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user, UserManager<User> userManager);
    }
}
