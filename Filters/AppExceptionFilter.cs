using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PashaInsuranceTest.Exceptions;
using System.Net;

namespace PashaInsuranceTest.Filters
{
    public class AppExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            // normalda burda loglama da elemeli idim, amma ne ise.... qaldi
            var exception = context.Exception.InnerException ?? context.Exception;
            var response = context.HttpContext.Response;
            response.ContentType = "application/json";
            string errorMessage;
            if (exception is BadRequestException)
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorMessage = exception.Message;
            }
            else if (exception is NotFoundException)
            {
                response.StatusCode = (int)HttpStatusCode.NotFound;
                errorMessage = exception.Message;
            }
            else {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorMessage = "Dont be worry. Be happy";
            }
            context.Result = new JsonResult(new { message = errorMessage });

        }
    }
}


