namespace Saga.Orchestrator.Models;

public class Result<T>
{
    private T _value;

    public bool Success { get; }
    public string? ErrorMessage { get; }
    public T Value
    {
        get
        {
            if (!Success)
            {
                throw new MemberAccessException(ErrorMessage);
            }

            return _value;
        }
    }

    public Result(T value)
    {
        Success = true;
        _value = value;
    }

    public Result(string? errorMessage)
    {
        ErrorMessage = errorMessage;
        _value = default(T)!;
    }
}   