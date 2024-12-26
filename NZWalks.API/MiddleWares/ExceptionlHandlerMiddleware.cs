using System.Net;

namespace NZWalks.API.MiddleWares
{
    public class ExceptionlHandlerMiddleware
    {
        private readonly ILogger<ExceptionlHandlerMiddleware> logger;
        private readonly RequestDelegate next;

        public ExceptionlHandlerMiddleware(ILogger<ExceptionlHandlerMiddleware> logger,
             RequestDelegate next) 
        {
            this.logger = logger;
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);

            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();
                //log this exception 
                logger.LogError(ex,$"{errorId}:{ex.Message}");
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType= "application/json";
                var error= new { 
                    errorId, ex.Message };
                await httpContext.Response.WriteAsJsonAsync(error);

                //return custom error respnce

            }
        }
    }
}
