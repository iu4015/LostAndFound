using LostAndFound.Application.Repositories;
using LostAndFound.Domain;
using Microsoft.EntityFrameworkCore;

namespace LostAndFound.Infrastructure.Repositories;

public sealed class ReturnRequestRepository(LostAndFoundDbContext dbContext) : IReturnRequestRepository
{
    public async Task AddAsync(ReturnRequest request, CancellationToken cancellationToken)
    {
        await dbContext.ReturnRequests.AddAsync(request, cancellationToken);
    }

    public Task<ReturnRequest?> GetByIdWithItemAsync(Guid id, CancellationToken cancellationToken) =>
        dbContext.ReturnRequests
            .Include(request => request.FoundItem)
            .SingleOrDefaultAsync(request => request.Id == id, cancellationToken);

    public Task SaveChangesAsync(CancellationToken cancellationToken) =>
        dbContext.SaveChangesAsync(cancellationToken);
}
