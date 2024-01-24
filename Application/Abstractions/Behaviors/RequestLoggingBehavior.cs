using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using SharedKernel;

namespace Application.Abstractions.Behaviors;

internal sealed class RequestLoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    where TResponse : Result
{
    private readonly ILogger<RequestLoggingBehavior<TRequest, TResponse>> _logger;

    public RequestLoggingBehavior(ILogger<RequestLoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        _logger.LogInformation("----- Processing request: {RequestName}", requestName);

        var result = await next();

        if (result.IsSuccess)
        {
            _logger.LogInformation("----- Completed request: {RequestName}", requestName);
        }
        else
        {
            using (LogContext.PushProperty("Error", result.Error, true))
            {
                _logger.LogError("----- Failed request: {RequestName}", requestName);
            }
        }

        return result;
    }
}
