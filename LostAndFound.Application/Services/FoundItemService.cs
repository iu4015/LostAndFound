using LostAndFound.Application.DTOs;
using LostAndFound.Application.Errors;
using LostAndFound.Application.Repositories;
using LostAndFound.Domain;

namespace LostAndFound.Application.Services;

public sealed class FoundItemService(IFoundItemRepository repository)
{
    public async Task<FoundItemResponse> RegisterAsync(
        RegisterFoundItemRequest request,
        CancellationToken cancellationToken)
    {
        var item = new FoundItem(
            request.Name,
            request.Description,
            request.Category,
            request.FoundDate!.Value,
            request.FoundLocation);

        await repository.AddAsync(item, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
        return Map(item);
    }

    public static FoundItemResponse Map(FoundItem item) => new(
        item.Id,
        item.Name,
        item.Description,
        item.Category,
        item.FoundDate,
        item.FoundLocation,
        item.Status,
        item.CreatedAt);

    public async Task<IReadOnlyList<FoundItemResponse>> SearchAsync(
        SearchFoundItemsRequest request,
        CancellationToken cancellationToken)
    {
        var items = await repository.SearchAsync(
            request.Name,
            request.Category,
            request.FoundLocation,
            request.FoundDate,
            cancellationToken);

        return items.Select(Map).ToList();
    }

    public async Task<FoundItemResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var item = await repository.GetByIdAsync(id, cancellationToken)
            ?? throw new ResourceNotFoundException($"Found item {id} was not found.");

        return Map(item);
    }
}
