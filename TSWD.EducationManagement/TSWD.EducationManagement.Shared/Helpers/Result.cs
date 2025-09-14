namespace TSWD.EducationManagement.Shared.Helpers
{
    public class Result<T>
    {
        public bool Success { get; private set; }
        public string? Message { get; private set; }
        public T? Data { get; private set; }

        private Result(bool success, string? message, T? data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public static Result<T> Ok(T data, string? message = null) => new(true, message, data);
        public static Result<T> Fail(string message) => new(false, message, default);
    }
}
