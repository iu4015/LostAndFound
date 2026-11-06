using LostAndFound.Application.Repositories;
using LostAndFound.Domain;
using Microsoft.EntityFrameworkCore;

namespace LostAndFound.Infrastructure.Repositories;

public sealed class FoundItemRepository(LostAndFoundDbContext dbContext) : IFoundItemRepository
{
    public async Task AddAsync(FoundItem item, CancellationToken cancellationToken)
    {
        await dbContext.FoundItems.AddAsync(item, cancellationToken);
    }

    public Task<FoundItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        dbContext.FoundItems.SingleOrDefaultAsync(item => item.Id == id, cancellationToken);

    public async Task<IReadOnlyList<FoundItem>> SearchAsync(
        string? name,
        string? category,
        string? foundLocation,
        DateOnly? foundDate,
        CancellationToken cancellationToken)
    {
        IQueryable<FoundItem> query = dbContext.FoundItems
            .AsNoTracking()
            .Where(item => item.Status == ItemStatus.Available);

        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(item => item.Name.Contains(name));
        }

        if (!string.IsNullOrWhiteSpace(category))
        {
            query = query.Where(item => item.Category == category);
        }

        if (!string.IsNullOrWhiteSpace(foundLocation))
        {
            query = query.Where(item => item.FoundLocation == foundLocation);
        }

        if (foundDate.HasValue)
        {
            query = query.Where(item => item.FoundDate == foundDate.Value);
        }

        return await query
            .OrderBy(item => item.FoundDate)
            .ThenBy(item => item.Name)
            .ThenBy(item => item.Id)
            .ToListAsync(cancellationToken);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken) =>
        dbContext.SaveChangesAsync(cancellationToken);
}
