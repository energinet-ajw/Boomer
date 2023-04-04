using System.Net;
using Newtonsoft.Json;

namespace PetStore.Api.Middleware;

public class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            var response = context.Response;
            if (!response.HasStarted)
            {
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                await response.WriteAsync(JsonConvert.SerializeObject(exception));
            }
        }
    }
}