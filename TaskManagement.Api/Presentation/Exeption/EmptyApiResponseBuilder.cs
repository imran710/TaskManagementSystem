using Library.Results;
using TaskManagement.Api.Common.Response;

namespace TaskManagement.Api.Presentation.Exeption;

public class EmptyApiResponse : ApiResponse<object?> { }

public static class EmptyApiResponseBuilder
{
    public static readonly ValidationErrors Errors = [];

    public static EmptyApiResponse Create(
        string message,
        int statusCode,
        ValidationErrors? errors = null)
    {
        return new EmptyApiResponse()
        {
            Message = message,
            Status = statusCode,
            Errors = errors ?? Errors,
        };
    }
}
