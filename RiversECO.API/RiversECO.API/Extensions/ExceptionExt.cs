using System;
using System.Net;
using RiversECO.Dtos.Responses;

namespace RiversECO.API.Extensions
{
    public static class ExceptionExt
    {
        public static ApiErrorDetails ToErrorDetails(this Exception exception, string ndc = null, bool isDebugMode = false)
        {
            return new ApiErrorDetails
            {
                Type = exception.GetType().Name,
                StatusCode = HttpStatusCode.InternalServerError,
                Title = exception.Message,
                Details = exception.StackTrace
            };
        }
    }
}
