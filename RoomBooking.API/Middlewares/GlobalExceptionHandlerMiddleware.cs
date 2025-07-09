using System.Net;
using Microsoft.AspNetCore.Mvc;
using BDC.Portal.Colaborador.Exceptions.DomainExceptions;
using BDC.Portal.Colaborador.Exceptions.ApplicationExceptions;
using BDC.Portal.Colaborador.Exceptions.InfrastructureExceptions;

namespace BDC.Portal.Colaborador.API.Middlewares
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;


        public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, context.Request);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex, HttpRequest request)
        {
            if (context.Response.HasStarted)
            {
                _logger.LogWarning("A resposta já foi iniciada. Não é possível modificar o status.");
                return;
            }

            var exceptionToHandle = ex is AggregateException aggregateException
                ? aggregateException.InnerException ?? ex
                : ex;

            context.Response.ContentType = "application/json";

            int statusCode;

            if (exceptionToHandle is ValidationException)
                statusCode = (int)HttpStatusCode.UnprocessableEntity;
            else if (exceptionToHandle is BusinessException)
                statusCode = (int)HttpStatusCode.BadRequest;
            else if (exceptionToHandle is EntityNotFoundException)
                statusCode = (int)HttpStatusCode.NotFound;
            else if (exceptionToHandle is UnauthorizedAccessApplicationException)
                statusCode = (int)HttpStatusCode.Unauthorized;
            else if (exceptionToHandle is DatabaseException)
                statusCode = (int)HttpStatusCode.ServiceUnavailable;
            else if (exceptionToHandle is ExternalServiceException)
                statusCode = (int)HttpStatusCode.BadGateway;
            else
                statusCode = (int)HttpStatusCode.InternalServerError;

            var problemDetails = new ProblemDetails
            {
                Type = $"https://httpstatuses.io/{statusCode}",
                Title = "Erro na API",
                Status = statusCode,
                Detail = ex.Message,
                Instance = request.Path
            };

            //  Logando erro no Serilog com detalhes da requisição
            _logger.LogError(ex, "Erro na API: {Method} {Path} - StatusCode: {StatusCode} - IP: {RemoteIpAddress}",
                request.Method, request.Path, statusCode, context.Connection.RemoteIpAddress);

            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}
