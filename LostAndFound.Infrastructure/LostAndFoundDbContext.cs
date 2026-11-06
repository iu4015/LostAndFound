using LostAndFound.Domain;
using Microsoft.EntityFrameworkCore;

namespace LostAndFound.Infrastructure;

public sealed class LostAndFoundDbContext(DbContextOptions<LostAndFoundDbContext> options) : DbContext(options)
{
    public DbSet<FoundItem> FoundItems => Set<FoundItem>();
    public DbSet<ReturnRequest> ReturnRequests => Set<ReturnRequest>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FoundItem>(entity =>
        {
            entity.HasKey(item => item.Id);
            entity.Property(item => item.Name).IsRequired().HasMaxLength(200);
            entity.Property(item => item.Description).IsRequired().HasMaxLength(2000);
            entity.Property(item => item.Category).IsRequired().HasMaxLength(100);
            entity.Property(item => item.FoundLocation).IsRequired().HasMaxLength(300);
            entity.Property(item => item.Status).HasConversion<string>().IsRequired();
            entity.HasIndex(item => new { item.Status, item.Name });
            entity.HasIndex(item => new { item.Status, item.Category });
            entity.HasIndex(item => new { item.Status, item.FoundLocation });
            entity.HasMany(item => item.ReturnRequests)
                .WithOne(request => request.FoundItem)
                .HasForeignKey(request => request.FoundItemId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ReturnRequest>(entity =>
        {
            entity.HasKey(request => request.Id);
            entity.Property(request => request.ContactDetails).IsRequired().HasMaxLength(300);
            entity.Property(request => request.OwnerExplanation).IsRequired().HasMaxLength(2000);
            entity.Property(request => request.Status).HasConversion<string>().IsRequired();
            entity.HasIndex(request => new { request.FoundItemId, request.Status });
        });
    }
}
