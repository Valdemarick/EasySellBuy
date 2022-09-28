namespace Exceptions.CustomExceptions;

public class NotFoundException : Exception
{
    public NotFoundException() { }
    public NotFoundException(string message) : base(message) { }
    public NotFoundException(string name, object key) : base($"The entity \"{name}\" ({key}) not found") { }
}