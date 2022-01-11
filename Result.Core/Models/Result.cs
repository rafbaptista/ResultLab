using System;

namespace Result.Core.Models
{
    public class Result<T>
    {
        public T Data { get; set; }
        public bool IsFailure { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsNull => Data is null;
        public Exception Exception { get; set; }
        public string Error { get; set; }
    }

    public static class Result
    {
        public static Result<T> Failure<T>(Exception ex) => new Result<T> { IsFailure = true, Exception = ex };
        public static Result<T> Failure<T>(string error) => new Result<T> { IsFailure = true, Error = error };
        public static Result<T> Success<T>(T data) => new Result<T> { IsSuccess = true, Data = data };
        public static Result<T> Null<T>() => new Result<T> {};

    }

}
