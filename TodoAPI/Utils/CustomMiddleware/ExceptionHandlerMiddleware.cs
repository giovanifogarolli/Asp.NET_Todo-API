using Microsoft.AspNetCore.Mvc;
using TodoAPI.Utils.ErrorResponses;

namespace TodoAPI.Utils.CustomMiddleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, "API Exception: {Message}", ex.Message);

            var problemDetails = new ProblemDetails
            {
                Type = ex.Type,
                Title = ex.Title,
                Detail = ex.Message,
                Status = ex.StatusCode
            };

            problemDetails.Extensions.Add("code", ex.Code);
            problemDetails.Extensions.Add("traceId", ex.TraceId);

            if (ex.Errors != null && ex.Errors.Count > 0)
            {
                problemDetails.Extensions.Add("errors", ex.Errors);
            }

            context.Response.StatusCode = ex.StatusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled Exception: {Message}", ex.Message);

            var problemDetails = new ProblemDetails
            {
                Type = "https://problems-registry.smartbear.com/internal-error/",
                Title = "Erro interno do servidor",
                Detail = "Ocorreu um erro inesperado.",
                Status = StatusCodes.Status500InternalServerError
            };

            problemDetails.Extensions.Add("traceId", context.TraceIdentifier);

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}
