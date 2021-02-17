using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Oneonones.Domain.Common;
using Oneonones.Service.Exceptions;
using System.Net;

namespace Oneonones.Infrastructure.Configurations
{
    public class GlobalExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private const string DefaultMessage = "An unexpected error has occurred.";

        public override void OnException(ExceptionContext exceptionContext)
        {
            exceptionContext.Result = exceptionContext.Exception is ApiException apiException
                ? new ObjectResult(new Error(apiException.Message)) { StatusCode = (int)apiException.StatusCode }
                : new ObjectResult(new Error(DefaultMessage)) { StatusCode = (int)HttpStatusCode.InternalServerError };
        }
    }
}