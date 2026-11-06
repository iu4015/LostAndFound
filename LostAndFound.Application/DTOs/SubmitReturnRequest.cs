using System.ComponentModel.DataAnnotations;

namespace LostAndFound.Application.DTOs;

public sealed class SubmitReturnRequest
{
    [Required, StringLength(300)]
    public string ContactDetails { get; init; } = string.Empty;

    [Required, StringLength(2000)]
    public string OwnerExplanation { get; init; } = string.Empty;
}
