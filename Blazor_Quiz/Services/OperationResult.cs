namespace Blazor_Quiz.Services
{
    public class OperationResult<T>
    {
        public bool Succeeded { get; set; }
        public string ErrorMessage { get; set; }
        public T Data { get; set; }

        public static OperationResult<T> Success(T data)
        {
            return new OperationResult<T> { Succeeded = true, Data = data };
        }

        public static OperationResult<T> Failure(string errorMessage)
        {
            return new OperationResult<T> { Succeeded = false, ErrorMessage = errorMessage };
        }
    }

}
