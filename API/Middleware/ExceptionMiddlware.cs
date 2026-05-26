using API.Errors;
using System.Net;
using System.Text.Json;

namespace API.Middleware
{
    public class ExceptionMiddlware(IHostEnvironment env,RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }

            catch (Exception ex)
            {
                await HandleExceptionAsync(context,ex,env);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex, IHostEnvironment env)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode =(int) HttpStatusCode.InternalServerError;

            var response = env.IsDevelopment() ? new ApiErrorResponse(context.Response.StatusCode, ex.Message, ex.StackTrace)
                : new ApiErrorResponse(context.Response.StatusCode, ex.Message, "InternalServerError");

            var json=JsonSerializer.Serialize(response);

            return context.Response.WriteAsync(json);
        }
    }
}
