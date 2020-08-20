using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RiversECO.API.Extensions;
using RiversECO.Common.Exceptions;
using RiversECO.Dtos.Responses;

namespace RiversECO.API.Infrastructure.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            try
            {
                await _next.Invoke(context);
            }
            catch (DataNotFoundException exception)
            {
                await WriteResponseErrorsAsync(context, exception.ToErrorDetails());
            }
            catch (Exception exception)
            {
                await WriteResponseErrorsAsync(context, exception.ToErrorDetails());
            }
        }
        
        private static async Task WriteResponseErrorsAsync(
            HttpContext context,
            ApiErrorDetails errorDetails)
        {
            context.Response.StatusCode = (int)errorDetails.StatusCode;

            var json = JsonConvert.SerializeObject(errorDetails);

            await context
                .Response
                .WriteAsync(json);
        }
    }
}
