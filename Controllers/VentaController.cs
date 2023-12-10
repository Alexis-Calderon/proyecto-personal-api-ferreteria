using Microsoft.AspNetCore.Mvc;

namespace ferreteriaJuanito;

// Controlador destinado a la ejecucion de metodos del mantenedor de ventas.
[ApiController]
[Route("api/[controller]")]
public class VentaController : ControllerBase
{
    private readonly ILogger<VentaController> _logger;
    private readonly IVentasService _ventasService;

    public VentaController(ILogger<VentaController> logger, IVentasService ventasService)
    {
        _logger = logger;
        _ventasService = ventasService;
    }

    // Metodo Get que lista de todas las ventas que existen en sistema o solo las de un cliente en especifico.
    [HttpGet("usuario/{usuarioId}")]
    public IActionResult Get(int usuarioId)
    {
        // Se genera un log en consola que especifica la accion que se ejecuta en mode de desarrollo.
        _logger.LogDebug("Metodo get de ventas.");
        // Retorna en el body elementos en formato Json.
        return Ok(_ventasService.Select(usuarioId));
    }

    // Metodo Post que genera una nueva venta con todos los productos del carrito asociados al usuario logueado.
    [HttpPost("{usuarioId}")]
    public IActionResult Post(int usuarioId)
    {
        // Se genera un log en consola que especifica la accion que se ejecuta en mode de desarrollo.
        _logger.LogDebug("Metodo post de ventas.");
        // Se almacena el mensaje retorado por la funcion.
        string mensaje = _ventasService.Create(usuarioId);
        // Retorna en el body el mensaje almacenado en la variable "mensaje".
        return Ok(mensaje);
    }
}