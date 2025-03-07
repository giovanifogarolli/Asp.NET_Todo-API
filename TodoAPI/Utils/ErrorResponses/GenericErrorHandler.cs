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

        if (!string.IsNullOrEmpty(detail))
        {
            errors ??= new List<ErrorDetail>();
            errors.Add(new ErrorDetail { Detail = detail });
        }

        throw new ApiException(
            statusCode: 409,
            type: "https://problems-registry.smartbear.com/already-exists/",
            title: "Já existe.",
            detail: "O recurso que está sendo criado já existe.",
            code: "409-01",
            traceId: HttpContext?.TraceIdentifier,
            errors: errors
        );
    }



    public ActionResult BadRequest(string? detail = null, List<ErrorDetail>? errors = null)
    {

        if (!string.IsNullOrEmpty(detail))
        {
            errors ??= new List<ErrorDetail>();
            errors.Add(new ErrorDetail { Detail = detail });
        }

        throw new ApiException(
            statusCode: 400,
            type: "https://problems-registry.smartbear.com/invalid-body-property-format/",
            title: "Corpo de requisição inválido",
            detail: "Corpo de requisição está mal formatado.",
            code: "400-04",
            traceId: HttpContext?.TraceIdentifier,
            errors: errors
        );
    }

    public ActionResult MissingParameter(string? detail, List<ErrorDetail>? errors = null)
    {
        if (!string.IsNullOrEmpty(detail))
        {
            errors ??= new List<ErrorDetail>();
            errors.Add(new ErrorDetail { Detail = detail });
        }

        throw new ApiException(
            statusCode: 400,
            type: "https://problems-registry.smartbear.com/missing-request-parameter/",
            title: "Parâmetro de requisição inválido.",
            detail: "Parâmetro da requisição está mal formatado.",
            code: "400-03",
            traceId: HttpContext?.TraceIdentifier,
            errors: errors
        );
    }

    public ActionResult ResourceNotFound(string? detail, List<ErrorDetail>? errors = null)
    {
        if (!string.IsNullOrEmpty(detail))
        {
            errors ??= new List<ErrorDetail>();
            errors.Add(new ErrorDetail { Detail = detail });
        }

        throw new ApiException(
            statusCode: 404,
            type: "https://problems-registry.smartbear.com/not-found/",
            title: "Não encontrado.",
            detail: "O recurso especificado não foi encontrado.",
            code: "404-01",
            traceId: HttpContext?.TraceIdentifier,
            errors: errors
        );
    }
}
