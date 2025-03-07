using Microsoft.AspNetCore.Mvc;
using static TodoAPI.Utils.ErrorResponses.GenericErrorHandler;

namespace TodoAPI.Utils.ErrorResponses;

public interface IGenericErrorHandler
{
    public ActionResult ResourceNotFound(string? detail = null, List<ErrorDetail>? errors = null);

    public ActionResult BadRequest(string? detail = null, List<ErrorDetail>? errors = null);

    public ActionResult AlreadyExist(string? detail = null, List<ErrorDetail>? errors = null);

    public ActionResult MissingParameter(string? detail = null, List<ErrorDetail>? errors = null);

}
