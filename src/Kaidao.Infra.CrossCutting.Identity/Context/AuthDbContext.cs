using Kaidao.Domain.IdentityEntity;
using Kaidao.Domain.IdentityEntity.Configurations;
using Kaidao.Infra.CrossCutting.Identity.Seeds;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kaidao.Infra.CrossCutting.Identity.Context
{
    public class AuthDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        // Add-Migration -Context AuthDbContext -Name Initial -OutputDir C:\SourceCode\GraduateThesis\Kaidao-WebAPI\src\Kaidao.Infra.CrossCutting.Identity\Migrations\
        // Update-Database -Context AuthDbContext

        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        // DbSet

        public DbSet<Command> Commands { set; get; }
        public DbSet<Function> Functions { set; get; }
        public DbSet<CommandInFunction> CommandInFunctions { set; get; }
        public DbSet<Permission> Permissions { set; get; }
        public DbSet<UserPermission> UserPermissions { set; get; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configuration
            builder.ApplyConfigurationsFromAssembly(typeof(AppUserConfig).Assembly);

            // Seed Databae
            builder.SeedData();
        }
    }
}