namespace Mentoring.Application.Result
{
    public class OperationResult
    {
        public bool IsSuccess { get; }
        public string Error { get; }

        protected OperationResult(bool isSuccess, string error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static OperationResult Success() => new OperationResult(true, null);
        public static OperationResult Failure(string error) => new OperationResult(false, error);
    }

    public class OperationResult<T> : OperationResult
    {
        public T Value { get; }

        protected OperationResult(bool isSuccess, T value, string error)
            : base(isSuccess, error)
        {
            Value = value;
        }

        public static new OperationResult<T> Success(T value) => new OperationResult<T>(true, value, null);
        public static new OperationResult<T> Failure(string error) => new OperationResult<T>(false, default, error);
    }
}