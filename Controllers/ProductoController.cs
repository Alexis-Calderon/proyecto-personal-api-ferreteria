using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ferreteriaJuanito;

// Controlador destinado a la ejecucion de metodos del mantenedor de productos.
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductoController : ControllerBase
{
    private readonly ILogger<ProductoController> _logger;
    private IProductosService _productosService;

    public ProductoController(ILogger<ProductoController> logger, IProductosService productosService)
    {
        _logger = logger;
        _productosService = productosService;
    }

    // Metodo Get que lista de todos los productos que existen en sistema.
    [HttpGet]
    public IActionResult Get()
    {
        // Se genera un log en consola que especifica la accion que se ejecuta en mode de desarrollo.
        _logger.LogDebug("Metodo get de productos.");
        // Retorna en el body elementos en formato Json.
        return Ok(_productosService.Select());
    }

    // Metodo Post que agrega un nuevo producto a la lista de productos del sistema.
    [HttpPost]
    [Authorize(Roles = "administrador")]
    public IActionResult Post(Producto producto)
    {
        // Se genera un log en consola que especifica la accion que se ejecuta en mode de desarrollo.
        _logger.LogDebug("Metodo post de productos.");
        // Se almacena el mensaje retorado por la funcion.
        string mensaje = _productosService.Create(producto);
        // Retorna en el body el mensaje almacenado en la variable "mensaje".
        return Ok(mensaje);
    }

    // Metodo Put que actualiza un producto especifico de la lista de productos del sistema.
    [HttpPut]
    [Authorize(Roles = "administrador")]
    public IActionResult Put(Producto producto)
    {
        // Se genera un log en consola que especifica la accion que se ejecuta en mode de desarrollo.
        _logger.LogDebug("Metodo put de productos.");
        // Se almacena el mensaje retorado por la funcion.
        string mensaje = _productosService.Update(producto);
        // Retorna en el body el mensaje almacenado en la variable "mensaje".
        return Ok(mensaje);
    }

    // Metodo Delete que elimina un producto especifico de la lista de productos del sistema.
    [HttpDelete]
    [Authorize(Roles = "administrador")]
    public IActionResult Delete(Producto producto)
    {
        // Se genera un log en consola que especifica la accion que se ejecuta en mode de desarrollo.
        _logger.LogDebug("Metodo delete de productos.");
        // Se almacena el mensaje retorado por la funcion.
        string mensaje = _productosService.Delete(producto);
        // Retorna en el body el mensaje almacenado en la variable "mensaje".
        return Ok(mensaje);
    }
}