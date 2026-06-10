// Added for Enum

namespace MineDev.MineTrack.Platform.Shared.Application.Model;

/// <summary>
///     Generic Result class for Command Handlers in the Application Layer.
/// </summary>
/// <typeparam name="T">The type of the result value.</typeparam>
public class Result<T>
{
    protected Result(bool isSuccess, T? value, string message, Enum? error)
    {
        IsSuccess = isSuccess;
        Value = value;
        Message = message;
        Error = error;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public T? Value { get; }
    public string Message { get; }
    public Enum? Error { get; }

    // Modified Success method to match new constructor
    public static Result<T> Success(T value) => new(true, value, string.Empty, null);

    // New Failure method using Enum? and string message
    public static Result<T> Failure(Enum error, string message) => new(false, default, message, error);
}

/// <summary>
///     Non-generic Result class for void Command Handlers.
/// </summary>
public class Result : Result<object>
{
    private Result(bool isSuccess, string message, Enum? error) : base(isSuccess, null, message, error) { }

    public static Result Success() => new(true, string.Empty, null);

    public new static Result Failure(Enum error, string message) => new(false, message, error);
}