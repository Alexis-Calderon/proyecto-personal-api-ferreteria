using Microsoft.AspNetCore.Mvc;

namespace ferreteriaJuanito;

// Controlador destinado a la ejecucion de metodos del mantenedor del carrito.
[ApiController]
[Route("api/[controller]")]
public class CarritoController : ControllerBase
{
    private ILogger<CarritoController> _logger;
    private ICarritosService _carritosService;
    public CarritoController(ILogger<CarritoController> logger, ICarritosService carritosService)
    {
        _logger = logger;
        _carritosService = carritosService;
    }

    // Metodo Get que lista los producto que posee el usuario logueado en el carrito de compras.
    [HttpGet]
    [Route("cliente/{usuarioId}")]
    public IActionResult Get(int usuarioId)
    {
        // Se genera un log en consola que especifica la accion que se ejecuta en mode de desarrollo.
        _logger.LogDebug("Devuelve una lista de productos en el carrito");
        // Retorna en el body elementos en formato Json.
        return Ok(_carritosService.Select(usuarioId));
    }

    // Metodo Post que agrega un nuevo producto a la lista de productos del carrito asociado al asuario logueado.
    [HttpPost]
    public IActionResult Post(Carrito carrito)
    {
        // Se genera un log en consola que especifica la accion que se ejecuta en mode de desarrollo.
        _logger.LogDebug("Metodo post del carrito.");
        // Se almacena el mensaje retorado por la funcion.
        string mensaje = _carritosService.Create(carrito);
        // Retorna en el body el mensaje almacenado en la variable "mensaje".
        return Ok(mensaje);
    }

    // Metodo Put que actualiza un producto especifico de la lista de productos del carrito asociado al usuario logueado.
    [HttpPut]
    public IActionResult Put(Carrito carrito)
    {
        // Se genera un log en consola que especifica la accion que se ejecuta en mode de desarrollo.
        _logger.LogDebug("Metodo put del carrito.");
        // Se almacena el mensaje retorado por la funcion.
        string mensaje = _carritosService.Update(carrito);
        // Retorna en el body el mensaje almacenado en la variable "mensaje".
        return Ok(mensaje);
    }

    // Metodo Delete que elimina un producto espacifico de la lista de productos del carrito asociado al usuario logueado.
    [HttpDelete]
    public IActionResult Delete(Carrito carrito)
    {
        // Se genera un log en consola que especifica la accion que se ejecuta en mode de desarrollo.
        _logger.LogDebug("Metodo delete de productos.");
        // Se almacena el mensaje retorado por la funcion.
        string mensaje = _carritosService.Delete(carrito);
        // Retorna en el body el mensaje almacenado en la variable "mensaje".
        return Ok(mensaje);
    }
}