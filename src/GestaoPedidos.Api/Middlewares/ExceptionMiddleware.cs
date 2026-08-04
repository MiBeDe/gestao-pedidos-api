using GestaoPedidos.Api.Filters;
using System.Net;
using System.Text.Json;

namespace GestaoPedidos.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }


        private async Task HandleExceptionAsync(
            HttpContext context,
            Exception exception)
        {
            var traceId = context.TraceIdentifier;
            var path = context.Request.Path;


            HttpStatusCode statusCode;
            object errors;


            switch (exception)
            {
                case ExcecaoDeDominio domainException:

                    statusCode = HttpStatusCode.BadRequest;
                    errors = domainException.MensagensDeErro;

                    _logger.LogWarning(
                        exception,
                        "Erro de domínio | Path: {Path} | TraceId: {TraceId}",
                        path,
                        traceId);

                    break;


                case ExcecaoDeValidacao validationException:

                    statusCode = HttpStatusCode.UnprocessableEntity;
                    errors = validationException.Erros;

                    _logger.LogWarning(
                        exception,
                        "Erro de validação | Path: {Path} | TraceId: {TraceId}",
                        path,
                        traceId);

                    break;


                default:

                    statusCode = HttpStatusCode.InternalServerError;

                    errors = new[]
                    {
                        "Erro interno. Tente novamente mais tarde."
                    };


                    _logger.LogError(
                        exception,
                        "Erro não tratado | Path: {Path} | TraceId: {TraceId}",
                        path,
                        traceId);

                    break;
            }


            var response = new
            {
                code = (int)statusCode,
                traceId,
                errors
            };


            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";


            var json = JsonSerializer.Serialize(response);


            await context.Response.WriteAsync(json);
        }
    }
}
