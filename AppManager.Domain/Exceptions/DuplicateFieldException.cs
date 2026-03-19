namespace AppManager.Domain.Exceptions;

public class DuplicateFieldException(string field, string? message) : Exception(message)
{
    public readonly string Field = field;
}