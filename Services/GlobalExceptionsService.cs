using System.Net;
using System.Text.Json;

namespace ferreteriaJuanito;

 // Servicio destinado a la ejecucion de metodos del mantenedor de excepciones globales del sistema.
public class GlobalExceptionsService
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionsService> _logger;
    private readonly IHostEnvironment _env;

    public GlobalExceptionsService(RequestDelegate next, ILogger<GlobalExceptionsService> logger, IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    // Metodo que captura cualquier posible excepcion de manera global en el sistema
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            // Se genera un log en consola que especifica el contenido de la excepcion.
            _logger.LogError(ex, ex.Message);
            // Especifica el formato del contenido del contexto.
            context.Response.ContentType = "application/json";
            // Especifica el codigo de error 500.
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            // Se valida que el ambiente sea de desarrollo para generar un contenido mas detallado del error
            var response = _env.IsDevelopment() ? new GlobalException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString()) : new GlobalException(context.Response.StatusCode, "Internal Server Error");
            // Se espesifican las opciones del formato Json.
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            // Se crea una representacion de los valores en formato Json
            var json = JsonSerializer.Serialize(response, options);
            // Se escribe en el contexto el texto con el formato Json  
            await context.Response.WriteAsync(json);
        }
    }
}