using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using TodoAPI.Context;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TodoAPI.Utils.ErrorResponses;

public class GenericErrorHandler : IGenericErrorHandler
{

    public class ErrorDetail()
    {
        public string Detail { get; set; } = string.Empty;
        public string? Pointer { get; set; }
    }

    private readonly IHttpContextAccessor _httpContextAccessor;

    public GenericErrorHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public HttpContext? HttpContext { get => _httpContextAccessor.HttpContext; set => throw new NotImplementedException(); }

    public ActionResult AlreadyExist(string? detail, List<ErrorDetail>? errors = null)
    {

        ProblemDetails pd = new()
        {
            Type = "https://problems-registry.smartbear.com/already-exists/",
            Title = "Já existe.",
            Detail = "O recurso que está sendo criado já existe.",
            Status = 409,
        };

        pd.Extensions.Add("Code", "409-01");
        pd.Extensions["traceId"] = HttpContext?.TraceIdentifier;

        if (!string.IsNullOrEmpty(detail))
        {
            errors ??= new List<ErrorDetail>();
            errors.Add(new ErrorDetail { Detail = detail });
        }

        if (errors != null && errors.Count > 0)
        {
            pd.Extensions.Add("errors", errors);
        }

        return new ObjectResult(pd) { StatusCode = StatusCodes.Status409Conflict };
    }

    public ActionResult BadRequest(string? detail = null, List<ErrorDetail>? errors = null)
    {
        ProblemDetails pd = new()
        {
            Type = "https://problems-registry.smartbear.com/invalid-body-property-format/",
            Title = "Corpo de requisição inválido",
            Detail = "Corpo de requisição está mal formatado.",
            Status = 400,
        };

        pd.Extensions.Add("Code", "400-04");
        pd.Extensions["traceId"] = HttpContext?.TraceIdentifier;

        if (!string.IsNullOrEmpty(detail))
        {
            errors ??= new List<ErrorDetail>();
            errors.Add(new ErrorDetail { Detail = detail });
        }

        if (errors != null && errors.Count > 0)
        {
            pd.Extensions.Add("errors", errors);
        }

        return new ObjectResult(pd) { StatusCode = StatusCodes.Status400BadRequest};
    }

    public ActionResult MissingParameter(string? detail, List<ErrorDetail>? errors = null)
    {
        ProblemDetails pd = new()
        {
            Type = "https://problems-registry.smartbear.com/missing-request-parameter/",
            Title = "Parametro de requisição inválido.",
            Detail = "Parametro da requisição está mal formatado.",
            Status = 400,
        };

        pd.Extensions.Add("Code", "400-03");
        pd.Extensions["traceId"] = HttpContext?.TraceIdentifier;

        if (!string.IsNullOrEmpty(detail))
        {
            errors ??= new List<ErrorDetail>();
            errors.Add(new ErrorDetail { Detail = detail });
        }

        if (errors != null && errors.Count > 0)
        {
            pd.Extensions.Add("errors", errors);
        }

        return new ObjectResult(pd) { StatusCode = StatusCodes.Status400BadRequest };
    }

    public ActionResult ResourceNotFound(string? detail, List<ErrorDetail>? errors = null)
    {
        ProblemDetails pd = new()
        {
            Type = "https://problems-registry.smartbear.com/not-found/",
            Title = "Não encontrado.",
            Detail = "O recurso especificado não foi encontrado.",
            Status = 404,
        };

        pd.Extensions.Add("Code", "404-1");
        pd.Extensions["traceId"] = HttpContext?.TraceIdentifier;

        if (!string.IsNullOrEmpty(detail))
        {
            errors ??= new List<ErrorDetail>();
            errors.Add(new ErrorDetail { Detail = detail });
        }

        if (errors != null && errors.Count > 0)
        {
            pd.Extensions.Add("errors", errors);
        }

        return new ObjectResult(pd) { StatusCode = StatusCodes.Status404NotFound };
    }
}
