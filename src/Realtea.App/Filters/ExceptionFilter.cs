using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Realtea.Core.Enums;
using Realtea.Core.Exceptions;

namespace Realtea.App.Filters
{
    public class ExceptionFilter : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.Exception is ApiException apiException)
            {
                var statusCode = MapToStatusCode(apiException.FailureType);
                context.Result = new ObjectResult(apiException.Message)
                {
                    StatusCode = statusCode,
                };

                return Task.CompletedTask;
            }

            context.Result = new ObjectResult(context.Exception.Message)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };

            return Task.CompletedTask;
        }

        private int MapToStatusCode(FailureType failureType)
        {
            return failureType switch
            {
                FailureType.Absent => (int)HttpStatusCode.BadRequest,
                FailureType.InvalidData => (int) HttpStatusCode.BadRequest,
                FailureType.Insufficient => (int)HttpStatusCode.BadRequest,
                FailureType.Conflict => (int)HttpStatusCode.Conflict,
                _ => (int)HttpStatusCode.InternalServerError
            }; ;
        }
    }
}

