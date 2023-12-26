using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogMVC.Infra
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var adminRoleId = "1d09d14e-5b3c-410f-ab52-9fc5a74524b5";
            var superAdminRoleId = "715f9444-aaa8-4b46-ad4e-35168d7979ab";
            var userRoleId = "7b42b54d-e407-4f9c-a7a2-c99c54a69d98";
            var roles = new List<IdentityRole>
            {
                new()
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId,
                },
                new()
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                    Id = superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId,
                },
                new()
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId,
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);
            var superAdminId = "8fb4358d-e437-4cba-9114-aaeb23f9a9cf";
            var superAdminUser = new IdentityUser
            {
                UserName = "superadmin@blog.com",
                Email = "superadmin@blog.com".ToUpper(),
                NormalizedEmail = "superadmin@blog.com".ToUpper(),
                Id = superAdminId
            };
            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(superAdminUser, "Superadmin@123");
            builder.Entity<IdentityUser>().HasData(superAdminUser);
            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new()
                {
                    RoleId = adminRoleId,
                    UserId = superAdminId,
                },
                new() {
                    RoleId = superAdminRoleId,
                    UserId = superAdminId
                },
                new() {
                    RoleId = userRoleId,
                    UserId = superAdminId
                },
            };
            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }
    }
}
