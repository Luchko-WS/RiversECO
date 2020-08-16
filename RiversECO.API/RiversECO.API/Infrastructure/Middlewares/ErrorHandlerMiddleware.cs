using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RiversECO.API.Extensions;
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
            catch (Exception exeption)
            {
                await HandleUnknownErrorAsync(context, exeption);
            }
        }

        private async Task HandleUnknownErrorAsync(HttpContext context, Exception exeption)
        {
            if (exeption == null) return;

            await WriteResponseErrorsAsync(context, exeption.ToErrorDetails());
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
