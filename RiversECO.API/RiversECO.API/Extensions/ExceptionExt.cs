using System;
using System.Net;
using RiversECO.Common.Exceptions;
using RiversECO.Dtos.Responses;

namespace RiversECO.API.Extensions
{
    public static class ExceptionExt
    {
        public static ApiErrorDetails ToErrorDetails(this DataNotFoundException exception)
        {
            return new ApiErrorDetails
            {
                Type = exception.GetType().Name,
                StatusCode = HttpStatusCode.NotFound,
                Title = exception.Message,
                Details = exception.StackTrace
            };
        }

        public static ApiErrorDetails ToErrorDetails(this Exception exception)
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
