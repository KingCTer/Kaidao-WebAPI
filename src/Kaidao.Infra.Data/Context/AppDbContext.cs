using Kaidao.Domain.AppEntity;
using Kaidao.Domain.AppEntity.Configurations;
using Kaidao.Domain.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Kaidao.Infra.Data.Context
{
    public class AppDbContext : DbContext
    {
        // Add-Migration -Context AppDbContext -Name Initial -OutputDir C:\SourceCode\GraduateThesis\Kaidao-WebAPI\src\Kaidao.Infra.Data\Migrations\
        // Update-Database -Context AppDbContext

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSet
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Chapter> Chapters { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Config Fluent API
            builder.ApplyConfiguration(new BookMap());
            builder.ApplyConfiguration(new AuthorMap());
            builder.ApplyConfiguration(new CategoryMap());
            builder.ApplyConfiguration(new ChapterMap());

            base.OnModelCreating(builder);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is EntityTrackable)
                .ToList();
            UpdateSoftDelete(entities);
            UpdateTimestamps(entities);
        }

        private static void UpdateSoftDelete(List<EntityEntry> entries)
        {
            var filtered = entries.Where(x => x.State == EntityState.Added || x.State == EntityState.Deleted);

            foreach (var entry in filtered)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        //entry.CurrentValues["IsDeleted"] = false;
                        ((EntityTrackable)entry.Entity).IsDeleted = false;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        //entry.CurrentValues["IsDeleted"] = true;
                        ((EntityTrackable)entry.Entity).IsDeleted = true;
                        break;
                }
            }
        }

        private static void UpdateTimestamps(List<EntityEntry> entries)
        {
            var filtered = entries.Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

            // TODO: Get real current user id
            var currentUserId = "Anonymous";

            foreach (var entry in filtered)
            {
                if (entry.State == EntityState.Added)
                {
                    ((EntityTrackable)entry.Entity).CreatedAt = DateTime.UtcNow;
                    ((EntityTrackable)entry.Entity).CreatedBy = currentUserId;
                }

                ((EntityTrackable)entry.Entity).LastUpdatedAt = DateTime.UtcNow;
                ((EntityTrackable)entry.Entity).LastUpdatedBy = currentUserId;
            }
        }
    }
}