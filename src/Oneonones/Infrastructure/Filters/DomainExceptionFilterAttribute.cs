using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Oneonones.Services.Exceptions;

namespace Oneonones.Infrastructure.Filters;

public class DomainExceptionFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var type = context.Exception.GetType();

        context.Result = context.Exception switch
        {
            InvalidException invalidException => new BadRequestObjectResult(new { invalidException.Errors }),
            NotFoundException notFoundException => new NotFoundObjectResult(new { Error = notFoundException.Errors.First() }),
            DbUpdateException => new ConflictObjectResult(new { Error = "Couldn't update the database. Verify request data." }),
            _ => new ObjectResult(new { Error = "Unexpected error occurred. Please inform admin." }) { StatusCode = 500 },
        };
    }
}
