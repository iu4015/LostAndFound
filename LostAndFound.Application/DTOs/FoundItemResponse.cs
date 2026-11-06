using LostAndFound.Domain;

namespace LostAndFound.Application.DTOs;

public sealed record FoundItemResponse(
    Guid Id,
    string Name,
    string Description,
    string Category,
    DateOnly FoundDate,
    string FoundLocation,
    ItemStatus Status,
    DateTimeOffset CreatedAt);
