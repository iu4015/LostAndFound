using LostAndFound.Domain;

namespace LostAndFound.Application.DTOs;

public sealed record ReturnRequestResponse(
    Guid Id,
    Guid FoundItemId,
    string ContactDetails,
    string OwnerExplanation,
    RequestStatus Status,
    DateTimeOffset CreatedAt);
