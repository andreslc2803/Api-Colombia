using ApiColombia.BL.Exceptions;
using System.Net;
using System.Text.Json;

namespace ApiColombia.Middleware
{
    /// <summary>
    /// Middleware global para manejo de excepciones.
    /// - Captura excepciones personalizadas (BaseException) y genera respuestas JSON con el código de estado y mensaje.
    /// - Captura cualquier otra excepción no controlada y devuelve un error 500.
    /// - Registra las excepciones en el logger.
    /// </summary>
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
            catch (BaseException ex)
            {
                _logger.LogWarning(ex, ex.Message);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = ex.StatusCode;

                var response = new
                {
                    success = false,
                    message = ex.Message,
                    statusCode = ex.StatusCode
                };

                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(response));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = new
                {
                    success = false,
                    message = "An unexpected error occurred.",
                    statusCode = 500
                };

                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(response));
            }
        }
    }
}