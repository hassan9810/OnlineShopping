namespace Store.Services
{
    public static class LoggedInUserProvider
    {
        public static int GetLoggedInUserId(IHttpContextAccessor httpContextAccessor)
        {
            try
            {

                var userIdClaim = httpContextAccessor.HttpContext.User.Claims
                    .SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    return userId;
                }

                throw new UnauthorizedAccessException("User ID claim not found or not valid.");
            }
            catch (Exception ex)
            {
                throw new UnauthorizedAccessException("Failed to retrieve logged-in user ID.", ex);

            }
        }
    }
}
