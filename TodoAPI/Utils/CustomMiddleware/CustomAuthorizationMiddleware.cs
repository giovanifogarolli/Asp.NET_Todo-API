namespace TodoAPI.Utils.CustomMiddleware;

public class CustomAuthorizationMiddleware
{

    private readonly RequestDelegate _next;

    public CustomAuthorizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {

        await _next(context);

        if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
        {
            context.Response.ContentType = "application/json";
            var response = new
            {
                type = "https://problems-registry.smartbear.com/unauthorized/",
                title = "Não autorizado",
                detail = "Token de acesso ausente, inválido ou expirado.",
                status = 401,
                code = "401-01",
                trace = context.TraceIdentifier
            };
            await context.Response.WriteAsJsonAsync(response);
        }
        else if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
        {
            context.Response.ContentType = "application/json";
            var response = new
            {
                type = "https://problems-registry.smartbear.com/forbidden",
                title = "Acesso negado",
                detail = "Você não tem permissão para acessar este recurso.",
                status = 403,
                code = "403-01",
                trace = context.TraceIdentifier
            };
            await context.Response.WriteAsJsonAsync(response);
        }
    }

}
