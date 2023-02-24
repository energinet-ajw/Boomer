using Newtonsoft.Json;

namespace Boomer.WebApi.Extensions;

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
                await response.WriteAsync(JsonConvert.SerializeObject(exception));
            }
        }
    }
}