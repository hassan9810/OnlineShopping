namespace Store.AppStoreContext
{
    public class StoreContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<UOM> UOMs { get; set; }
        public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            SeedRoles(modelBuilder);
            SeedUsers(modelBuilder);
        }

        private static void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int>
                {
                    Id = 1,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new IdentityRole<int>
                {
                    Id = 2,
                    Name = "Customer",
                    NormalizedName = "CUSTOMER",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }
            );
        }

        private static void SeedUsers(ModelBuilder modelBuilder)
        {
            // Seed default admin user
            var adminUser = new User
            {
                Id = 1,
                FirstName = "Admin",
                LastName = "User",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var adminPasswordHash = new PasswordHasher<User>().HashPassword(adminUser, "Admin@123");
            adminUser.PasswordHash = adminPasswordHash;

            modelBuilder.Entity<User>().HasData(adminUser);

            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int>
                {
                    UserId = 1,
                    RoleId = 1 
                }
            );

            var customerUser = new User
            {
                Id = 2,
                FirstName = "Customer",
                LastName = "User",
                UserName = "customer",
                NormalizedUserName = "CUSTOMER",
                Email = "customer@example.com",
                NormalizedEmail = "CUSTOMER@EXAMPLE.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var customerPasswordHash = new PasswordHasher<User>().HashPassword(customerUser, "Customer@123"); 
            customerUser.PasswordHash = customerPasswordHash;

            modelBuilder.Entity<User>().HasData(customerUser);

            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int>
                {
                    UserId = 2,
                    RoleId = 2 
                }
            );
        }
    }
}
