namespace LostAndFound.Application.DTOs;

public sealed class SearchFoundItemsRequest
{
    public string? Name { get; init; }
    public string? Category { get; init; }
    public string? FoundLocation { get; init; }
    public DateOnly? FoundDate { get; init; }
}
