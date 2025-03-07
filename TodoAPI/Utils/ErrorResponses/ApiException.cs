using static TodoAPI.Utils.ErrorResponses.GenericErrorHandler;

namespace TodoAPI.Utils.ErrorResponses;

public class ApiException : Exception
{
    public int StatusCode { get; }
    public string Type { get; }
    public string Title { get; }
    public string? TraceId { get; }
    public string? Code { get; }
    public List<ErrorDetail>? Errors { get; }

    public ApiException(int statusCode, string type, string title, string? detail = null, string? code = null, string? traceId = null, List<ErrorDetail>? errors = null)
        : base(detail)
    {
        StatusCode = statusCode;
        Type = type;
        Title = title;
        Code = code;
        TraceId = traceId;
        Errors = errors;
    }
}
