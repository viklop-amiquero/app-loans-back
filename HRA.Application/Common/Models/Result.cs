using System.Collections;

namespace HRA.Application.Common.Models
{
    public interface Iresult
    {
        bool HasSucceeded { get; }
        int StatusCode { get; }
    }

    public interface Iresult<T> : Iresult
    {
        T Value { get; }
    }


    public class SuccessResult : Iresult
    {
        public SuccessResult()
        {
            StatusCode = 200;
            HasSucceeded = true;
        }
        public bool HasSucceeded { get; private set; }
        public int StatusCode { get; private set; }
    }

    public class SuccessResult<T> : Iresult<T>
    {
        public SuccessResult() => (HasSucceeded, StatusCode) = (true, 200);
        public SuccessResult(T value) : this() => Value = value;
        public T Value { get; }
        public int StatusCode { get; private set; }
        public bool HasSucceeded { get; }
    }
    public class FailureResult : Iresult
    {
        public int StatusCode { get; set; }
        public bool HasSucceeded { get; private set; }
    }

    public class FailureResult<T> : Iresult<T> where T : IEnumerable
    {
        public FailureResult()
        {
            StatusCode = 400;
            HasSucceeded = false;
        }

        public FailureResult(T errors)
        {
            Value = errors;
        }

        public int StatusCode { get; set; }
        public bool HasSucceeded { get; private set; }
        public T Value { get; set; }
    }

    public class DetailError
    {
        public DetailError(string errorCode, string message)
        {
            ErrorCode = errorCode;
            Message = message;
        }
        public string ErrorCode { get; }
        public string Message { get; }
    }
}
