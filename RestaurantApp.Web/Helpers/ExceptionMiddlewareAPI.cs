using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantApp.Web.Helpers
{
    public class ExceptionMiddlewareAPI : IMiddleware
    {
        private readonly ILogger<ExceptionMiddlewareAPI> _logger;

        public ExceptionMiddlewareAPI(ILogger<ExceptionMiddlewareAPI> logger)
        {
            _logger = logger;
        }

        //Method to handle exception
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var traceId = Guid.NewGuid();
                _logger.LogError($"Error occure while processing the request, TraceId : ${traceId}, Message : ${ex.Message}, StackTrace: ${ex.StackTrace}");

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var problemDetails = new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                    Title = "Internal Server Error",
                    Status = (int)StatusCodes.Status500InternalServerError,
                    Instance = context.Request.Path,
                    Detail = $"Internal server error occured, traceId : {traceId}",
                };
                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }
}