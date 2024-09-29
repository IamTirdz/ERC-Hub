using ERC.Hub.Business.Common.Exceptions;
using ERC.Hub.Business.Common.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace ERC.Hub.Function.Middlewares
{
    public class ExceptionHandlingMiddleware : IFunctionsWorkerMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger, JsonSerializerSettings jsonSerializerSettings)
        {
            _logger = logger;
            _jsonSerializerSettings = jsonSerializerSettings;
        }

        public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (BaseException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(FunctionContext context, BaseException ex)
        {
            var httpReqData = await context.GetHttpRequestDataAsync();
            if (httpReqData != null)
            {
                var statusCode = ex switch
                {
                    ForbiddenException => HttpStatusCode.Forbidden,
                    NotFoundException => HttpStatusCode.NotFound,
                    UnauthorizedException => HttpStatusCode.Unauthorized,
                    BadRequestException => HttpStatusCode.BadRequest,
                    InputValidationException => HttpStatusCode.UnprocessableEntity,
                    _ => HttpStatusCode.InternalServerError
                };
                
                ex.ErrorResponse.ErrorCode = (int)statusCode;
                var response = ex.ErrorResponse;
                var jsonResponse = JsonConvert.SerializeObject(response, _jsonSerializerSettings);

                _logger.LogError(ex, "ExceptionMessage: {exceptionMessage}", ex.ErrorResponse.Message);
                var newHttpResponse = httpReqData.CreateResponse(statusCode);
                newHttpResponse.Headers.Add("Content-Type", "application/json");

                await newHttpResponse.WriteStringAsync(jsonResponse);
                context.GetInvocationResult().Value = newHttpResponse;
            }
        }

        private async Task HandleExceptionAsync(FunctionContext context, Exception ex)
        {
            var httpReqData = await context.GetHttpRequestDataAsync();
            if (httpReqData != null)
            {
                var statusCode = HttpStatusCode.InternalServerError;
                var message = "An unhandled exception has occurred.";
                                                
                var response = new ErrorResponse
                {
                    ErrorCode = (int)statusCode,
                    Message = message,
                };
                var jsonResponse = JsonConvert.SerializeObject(response, _jsonSerializerSettings);

                _logger.LogError(ex, "ExceptionMessage: {exceptionMessage}", message);
                var newHttpResponse = httpReqData.CreateResponse(statusCode);
                newHttpResponse.Headers.Add("Content-Type", "application/json");

                await newHttpResponse.WriteStringAsync(jsonResponse);                
                context.GetInvocationResult().Value = newHttpResponse;
            }
        }
    }
}
