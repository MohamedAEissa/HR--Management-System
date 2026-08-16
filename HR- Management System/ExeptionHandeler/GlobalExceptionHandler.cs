using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace EventHub.API.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
           
            if (exception is ValidationException validationException)
            {
                _logger.LogWarning("Validation failed: {Errors}", validationException.Errors);


                var errors = validationException.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        f => f.Key,
                       f => f.Select(e => e.ErrorMessage).ToArray()
                    );


                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Validation Failed",
                    Detail = "One or more validation errors occurred. Please check the inputs.",
                    Instance = httpContext.Request.Path
                };

               
                problemDetails.Extensions["errors"] = errors;

                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

                await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

             
                return true;
            }


            var defaultProblemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError, 
                Title = "Bad Request",
                Detail = exception.Message, 
                Instance = httpContext.Request.Path
            };

            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            await httpContext.Response.WriteAsJsonAsync(defaultProblemDetails, cancellationToken);

            return true;
        }
    }
}
