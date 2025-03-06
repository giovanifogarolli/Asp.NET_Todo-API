using Microsoft.AspNetCore.Mvc;
using static TodoAPI.Utils.ErrorResponses.GenericErrorHandler;

namespace TodoAPI.Utils.ErrorResponses;

public interface IGenericErrorHandler
{
    public ActionResult ResourceNotFound(string? detail, List<ErrorDetail>? errors = null);

    public ActionResult BadRequest(string? detail, List<ErrorDetail>? errors = null);

    public ActionResult AlreadyExist(string? detail, List<ErrorDetail>? errors = null);

    public ActionResult MissingParameter(string? detail, List<ErrorDetail>? errors = null);

}
