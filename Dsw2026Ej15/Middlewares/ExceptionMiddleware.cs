using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Dsw2026Ej15.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(ValidationException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "applications/json";
                await context.Response.WriteAsJsonAsync(new { message = ex.Message });
            }
            catch(Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "applications/json";
                await context.Response.WriteAsJsonAsync(new { message = "Ocurrio un error inesperado en el servidor" });
                //await HandleExceptionAsync(context, ex); metodo usado por el profe
            }
        }

        //Metodo asincronico del profe, luego el profe serializa el mensaje del error en un JSON
        //private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    }
}
