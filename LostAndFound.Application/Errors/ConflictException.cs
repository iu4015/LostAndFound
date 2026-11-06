namespace LostAndFound.Application.Errors;

public sealed class ConflictException(string message) : Exception(message);
