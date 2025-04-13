using System.Net;

namespace Blazor_Quiz.Services
{
    public class HandleError
    {
        public static async Task<OperationResult<T>> HandleErrorResponse<T>(HttpResponseMessage response)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            switch (response.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    return OperationResult<T>.Failure($"Bad Request: {errorContent}");
                case HttpStatusCode.NotFound:
                    return OperationResult<T>.Failure($"Not Found: {errorContent}");
                case HttpStatusCode.InternalServerError:
                    return OperationResult<T>.Failure($"Internal Server Error: {errorContent}");
                default:
                    return OperationResult<T>.Failure($"HTTP Error: {response.StatusCode}, Content: {errorContent}");
            }
        }
    }
}
