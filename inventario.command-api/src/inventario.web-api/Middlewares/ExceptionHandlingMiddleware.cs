using inventario.web_api.Models;
using Newtonsoft.Json;
using System.Net;

namespace inventario.web_api.Middlewares
{
    public partial class ExceptionHandlingMiddleware
    {
        public ExceptionHandlingMiddleware(RequestDelegate next)
            => _next = next;

        private readonly RequestDelegate _next;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            } catch (Exception ex) 
            {
                await HandleExceptionAsync(context, ex);    
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.ContentType = "application/json";
            var response = httpContext.Response;

            var errorResponse = new ErrorResponse();

            switch (ex)
            {
                case InvalidOperationException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    errorResponse.Message = ex.Message;
                    break;

                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.Message = "Ooops. Algo aconteceu de errado aconteceu por aqui. Verifique os logs!";
                    break;
            }

            var result = JsonConvert.SerializeObject(errorResponse);
            await httpContext.Response.WriteAsync(result);
        }
    }
}
