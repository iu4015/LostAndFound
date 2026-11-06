namespace LostAndFound.Application.Errors;

public sealed class ResourceNotFoundException(string message) : Exception(message);
