using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Oneonones.Services.Exceptions;

namespace Oneonones.Infrastructure.Filters;

public class DomainExceptionFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        context.Result = context.Exception switch
        {
            InvalidException invalidException => new BadRequestObjectResult(new { invalidException.Errors }),
            NotFoundException notFoundException => new NotFoundObjectResult(new { Error = notFoundException.Errors.First() }),
            _ => new NotFoundObjectResult(new { Error = context.Exception.Message }),
        };
    }
}
