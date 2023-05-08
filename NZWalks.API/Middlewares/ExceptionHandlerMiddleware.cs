using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NZWalks.API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> logger;
        private readonly RequestDelegate next;

        //steps : log and use request delegate
        // make async method that takes httpcontext and:
        // passes context to requestdelegate
        //logs try catch with guid
        //sets httpcontext status code 500 and content type to json
        //makes anonymous obj to http obj response async write(anonymous) 
        //configure http pipeline in program
        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate next)
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
                logger.LogError(ex, $"{errorId} : {ex.Message}");
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";


                var errorResponse = new
                {
                    Id = errorId,
                    ErrorMessage = ex.Message
                };

                await httpContext.Response.WriteAsJsonAsync(errorResponse);
            }
         
        }
    }
}
