using LostAndFound.Domain;

namespace LostAndFound.Application.Repositories;

public interface IReturnRequestRepository
{
    Task AddAsync(ReturnRequest request, CancellationToken cancellationToken);
    Task<ReturnRequest?> GetByIdWithItemAsync(Guid id, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
