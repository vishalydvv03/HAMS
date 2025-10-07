using HAMS.Data;
using HAMS.Domain.Entities;
using HAMS.Utility;
using System.Net;
using System.Text.Json;

namespace HAMS.API.Middlewares
{
    public class GlobalExceptionHandlingMiddleware 
    {
        private readonly RequestDelegate next;
        private readonly HamsDbContext context;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next, HamsDbContext context)
        {
            this.next = next;
            this.context = context;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var exceptionLog = new ExceptionLog()
                {
                    Message = ex.Message,
                    Type = ex.GetType().Name,
                    StackTrace = ex.StackTrace ?? string.Empty,
                    URL = httpContext.Request?.Path,
                    CreatedDate = DateTime.UtcNow,
                };

                context.ExceptionLogs.Add(exceptionLog);
                await context.SaveChangesAsync();

                var response = new ErrorResponse()
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = "An unexpected error occurred. Please try again later."
                };

                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = response.StatusCode;

                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
