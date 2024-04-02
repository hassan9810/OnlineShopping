namespace Store.Helper
{
    public static class RoleInitializer
    {
        public static async Task SeedRolesAsync(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            await SeedRoleAsync(roleManager, "Admin");
            await SeedRoleAsync(roleManager, "Customer");
        }

        private static async Task SeedRoleAsync(RoleManager<IdentityRole<int>> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var role = new IdentityRole<int>(roleName);
                await roleManager.CreateAsync(role);
            }
        }
    }
}
