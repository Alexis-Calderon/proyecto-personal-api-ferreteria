using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ferreteriaJuanito;

// Controlador destinado a la ejecucion de metodos del mantenedor de ventas.
[ApiController]
[Route("api/[controller]")]
[Authorize]
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
    [HttpGet]
    public IActionResult Get()
    {
        // Se extrae el id y el rol del usuario loguedo almacenado el las claims del jwt
        ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
        int idUsuarioLogueado = Int32.Parse(identity.Claims.FirstOrDefault(p => p.Type == "usuarioId").Value);
        Roles rol = (Roles)Enum.Parse(typeof(Roles), identity.Claims.FirstOrDefault(p=> p.Type == "usuarioId").Value);
        // Se valida que el rol del usuario logueado sea de cliente
        if (rol == Roles.cliente)
        {
            // Se genera un log en consola que especifica la accion que se ejecuta en mode de desarrollo.
            _logger.LogDebug("Metodo get de ventas.");
            // Retorna en el body elementos en formato Json.
            return Ok(_ventasService.Select(idUsuarioLogueado));
        }
        // Se genera un log en consola que especifica la accion que se ejecuta en mode de desarrollo.
        _logger.LogDebug("Metodo get de ventas.");
        // Retorna en el body elementos en formato Json.
        return Ok(_ventasService.Select(null));
    }

    // Metodo Post que genera una nueva venta con todos los productos del carrito asociados al usuario logueado.
    [HttpPost]
    [Authorize(Roles = "cliente")]
    public IActionResult Post()
    {
        // Se extrae el id del usuario loguedo almacenado el las claims del jwt
        ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
        int idUsuarioLogueado = Int32.Parse(identity.Claims.FirstOrDefault(p => p.Type == "usuarioId").Value);
        // Se genera un log en consola que especifica la accion que se ejecuta en mode de desarrollo.
        _logger.LogDebug("Metodo post de ventas.");
        // Se almacena el mensaje retorado por la funcion.
        string mensaje = _ventasService.Create(idUsuarioLogueado);
        // Retorna en el body el mensaje almacenado en la variable "mensaje".
        return Ok(mensaje);
    }
}