using LostAndFound.Domain;

namespace LostAndFound.Application.Repositories;

public interface IFoundItemRepository
{
    Task AddAsync(FoundItem item, CancellationToken cancellationToken);
    Task<FoundItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<FoundItem>> SearchAsync(
        string? name,
        string? category,
        string? foundLocation,
        DateOnly? foundDate,
        CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
