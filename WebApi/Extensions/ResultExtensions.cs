using SharedKernel;

namespace WebApi.Extensions;

public static class ResultExtensions
{
    public static IResult HandleFailure(this Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException();
        }

        var validationResult = result as IValidationResult;

        return Results.Problem(
            statusCode: GetStatusCode(result.Error.Type),
            title: GetTitle(result.Error.Type),
            type: GetType(result.Error.Type),
            extensions: new Dictionary<string, object?>
            {
                { "error", new[] { result.Error } },
                { "validationErrors", new[] { validationResult?.Errors } }
            });


        static int GetStatusCode(ErrorType errorType)
        {
            return errorType switch
            {
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };
        }

        static string GetTitle(ErrorType errorType)
        {
            return errorType switch
            {
                ErrorType.NotFound => "Not Found",
                ErrorType.Validation => "Bad Request",
                ErrorType.Conflict => "Conflict",
                _ => "Internal Server Error"
            };
        }

        static string GetType(ErrorType errorType)
        {
            return errorType switch
            {
                ErrorType.NotFound => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
                ErrorType.Validation => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                ErrorType.Conflict => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8",
                _ => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1"
            };
        }
    }
}
