namespace LostAndFound.Domain;

public sealed class FoundItem
{
    private FoundItem()
    {
    }

    public FoundItem(
        string name,
        string description,
        string category,
        DateOnly foundDate,
        string foundLocation)
    {
        ValidateText(name, nameof(name));
        ValidateText(description, nameof(description));
        ValidateText(category, nameof(category));
        ValidateText(foundLocation, nameof(foundLocation));

        Id = Guid.NewGuid();
        Name = name.Trim();
        Description = description.Trim();
        Category = category.Trim();
        FoundDate = foundDate;
        FoundLocation = foundLocation.Trim();
        Status = ItemStatus.Available;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string Category { get; private set; } = string.Empty;
    public DateOnly FoundDate { get; private set; }
    public string FoundLocation { get; private set; } = string.Empty;
    public ItemStatus Status { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public ICollection<ReturnRequest> ReturnRequests { get; private set; } = new List<ReturnRequest>();

    public void EnsureAvailable()
    {
        if (Status != ItemStatus.Available)
        {
            throw new DomainRuleException("The found item is not available.");
        }
    }

    public void MarkReturned()
    {
        EnsureAvailable();
        Status = ItemStatus.Returned;
    }

    private static void ValidateText(string value, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainRuleException($"{parameterName} is required.");
        }
    }
}
