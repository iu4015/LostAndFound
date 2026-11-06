namespace LostAndFound.Domain;

public sealed class ReturnRequest
{
    private ReturnRequest()
    {
    }

    public ReturnRequest(Guid foundItemId, string contactDetails, string ownerExplanation)
    {
        if (foundItemId == Guid.Empty)
        {
            throw new DomainRuleException("A found item is required.");
        }

        ValidateText(contactDetails, nameof(contactDetails));
        ValidateText(ownerExplanation, nameof(ownerExplanation));

        Id = Guid.NewGuid();
        FoundItemId = foundItemId;
        ContactDetails = contactDetails.Trim();
        OwnerExplanation = ownerExplanation.Trim();
        Status = RequestStatus.Submitted;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public Guid Id { get; private set; }
    public Guid FoundItemId { get; private set; }
    public FoundItem FoundItem { get; private set; } = null!;
    public string ContactDetails { get; private set; } = string.Empty;
    public string OwnerExplanation { get; private set; } = string.Empty;
    public RequestStatus Status { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    public void Approve()
    {
        EnsureSubmitted();
        Status = RequestStatus.Approved;
    }

    public void Reject()
    {
        EnsureSubmitted();
        Status = RequestStatus.Rejected;
    }

    private void EnsureSubmitted()
    {
        if (Status != RequestStatus.Submitted)
        {
            throw new DomainRuleException("Only submitted requests can be reviewed.");
        }
    }

    private static void ValidateText(string value, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainRuleException($"{parameterName} is required.");
        }
    }
}
