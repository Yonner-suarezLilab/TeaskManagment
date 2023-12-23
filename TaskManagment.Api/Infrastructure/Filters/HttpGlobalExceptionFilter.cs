using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TaskManagment.Shared;
using TaskManagment.Infraestructura.Services.ErrorLoggerService;
using TaskManagment.Domain.Exceptions;
using FluentValidation.Results;
using TaskManagment.Api.Infraestructura.ActionResults;

namespace TaskManagment.Api.Infraestructura.Filters
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment env;
        private readonly ILogger<HttpGlobalExceptionFilter> logger;
        private readonly IErrorLoggerService _errorReport;

        public HttpGlobalExceptionFilter(IWebHostEnvironment env, ILogger<HttpGlobalExceptionFilter> logger, IErrorLoggerService errorReport)
        {
            this.env = env;
            this.logger = logger;
            _errorReport = errorReport;
        }

        public void OnException(ExceptionContext context)
        {
            logger.LogError(new EventId(context.Exception.HResult),
                context.Exception,
                context.Exception.Message);
            if (context.Exception.GetType() == typeof(TaskManagmentApiDomainException))
            {
                TaskManagmentApiDomainException res = (TaskManagmentApiDomainException)context.Exception;
                dynamic validation = res.InnerException;

                if (validation == null)
                {
                    var details = new
                    {
                        Message = res.Message,
                        Instance = context.HttpContext.Request.Path,
                        Status = StatusCodes.Status400BadRequest,
                        Detail = res.Message
                    };
                    context.Result = new BadRequestObjectResult(details);
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    context.ExceptionHandled = true;
                    return;
                }

                List<ValidationFailure> errores = validation.Errors;

                var problemDetails = new
                {
                    Message = errores[0].ErrorMessage,
                    Instance = context.HttpContext.Request.Path,
                    Status = StatusCodes.Status400BadRequest,
                    Detail = errores[0].ErrorMessage,
                    ExceptionType = typeof(TaskManagmentApiDomainException).Name
                };

                if (errores.Count > 0)
                {
                    context.Result = new BadRequestObjectResult(problemDetails);
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    context.ExceptionHandled = true;
                    return;
                }

                context.Result = new BadRequestObjectResult(problemDetails);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                var messageGlobal = context.Exception.Message;

                if (env.EnvironmentName != "Development")
                {
                    messageGlobal = "No se ha podido procesar su solicitud.";
                }

                var json = new JsonErrorResponse
                {
                    Messages = messageGlobal
                };

                if (env.EnvironmentName == "Development")
                {
                    json.DeveloperMessage = context.Exception;
                }

                context.Result = new InternalServerErrorObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                _errorReport.LogError(context.Exception);
            }
            context.ExceptionHandled = true;
        }

        private class JsonErrorResponse
        {
            public string Messages { get; set; }

            public object DeveloperMessage { get; set; }
        }
    }

}
