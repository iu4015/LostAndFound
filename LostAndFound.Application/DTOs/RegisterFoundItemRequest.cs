using System.ComponentModel.DataAnnotations;

namespace LostAndFound.Application.DTOs;

public sealed class RegisterFoundItemRequest
{
    [Required, StringLength(200)]
    public string Name { get; init; } = string.Empty;

    [Required, StringLength(2000)]
    public string Description { get; init; } = string.Empty;

    [Required, StringLength(100)]
    public string Category { get; init; } = string.Empty;

    [Required]
    public DateOnly? FoundDate { get; init; }

    [Required, StringLength(300)]
    public string FoundLocation { get; init; } = string.Empty;
}
