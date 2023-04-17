using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Realtea.App.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
        }
    }
}

