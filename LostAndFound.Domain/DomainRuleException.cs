namespace LostAndFound.Domain;

public sealed class DomainRuleException : Exception
{
    public DomainRuleException(string message)
        : base(message)
    {
    }
}
