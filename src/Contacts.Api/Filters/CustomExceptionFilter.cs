using Contacts.Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Contacts.Api.Filters;

public class CustomExceptionFilter : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is not ValidationException && context.Exception is not ServiceException) return;

        context.HttpContext.Response.ContentType = "application/json";
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        context.Result = context.Exception switch
        {
            ValidationException exception => new JsonResult(exception.Errors.Select(w => new
            {
                w.PropertyName, w.ErrorMessage, w.AttemptedValue
            })),
            ServiceException => new JsonResult(context.Exception.Message),
            _ => throw new NotImplementedException()
        };
    }
}
