using APIAlerts.network.contract;

namespace APIAlerts.util;

internal class Result<T>
{
    internal T? Data { get; private set; }
    internal ErrorResponse? Error { get; private set; }
    internal bool IsSuccess { get; private set; }

    private Result() { } // Private constructor to enforce factory methods

    internal static Result<T> Success(T data) => new()
    {
        Data = data,
        IsSuccess = true
    };

    internal static Result<T> Failure(ErrorResponse error) => new()
    {
        Error = error,
        IsSuccess = false
    };
}