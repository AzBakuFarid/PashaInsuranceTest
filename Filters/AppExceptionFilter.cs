using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PashaInsuranceTest.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PashaInsuranceTest.Filters
{
    public class AppExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception.InnerException ?? context.Exception;
            var response = context.HttpContext.Response;
            response.ContentType = "application/json";
            if (exception is BadRequestException)
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new JsonResult(new { message = exception.Message });
            }
            else if (exception is NotFoundException)
            {
                response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Result = new JsonResult(new { message = exception.Message });
            }
            else {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Result = new JsonResult(new { message = "Dont be worry. Be happy" });
            }
        }
    }
}


