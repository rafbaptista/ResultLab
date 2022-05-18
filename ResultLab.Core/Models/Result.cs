namespace ResultLab.Core.Models
{
    public class Result<T>
    {
        public T Data { get; set; }
        public bool IsFailure { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

    public static class Result
    {
        public static Result<T> Failure<T>(T data) => new Result<T> { IsSuccess = false, IsFailure = true, Data = data };
        public static Result<T> Success<T>(T data) => new Result<T> { IsSuccess = true,  IsFailure = false, Data = data};
    }

}
