using Microsoft.AspNetCore.Mvc;

namespace ferreteriaJuanito;

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

    [HttpGet]
    [Route("cliente/{usuarioId}")]
    public IActionResult Get(int usuarioId)
    {
        _logger.LogDebug("Devuelve una lista de productos en el carrito");
        return Ok(_carritosService.Select(usuarioId));
    }

    [HttpPost]
    public IActionResult Post(Carrito carrito)
    {
        _logger.LogDebug("Metodo post del carrito.");
        string mensaje = _carritosService.Create(carrito);
        return Ok(mensaje);
    }

    [HttpPut]
    public IActionResult Put(Carrito carrito)
    {
        _logger.LogDebug("Metodo put del carrito.");
        string mensaje = _carritosService.Update(carrito);
        return Ok(mensaje);
    }

    [HttpDelete]
    public IActionResult Delete(Carrito carrito)
    {
        _logger.LogDebug("Metodo delete de productos.");
        string mensaje = _carritosService.Delete(carrito);
        return Ok(mensaje);
    }
}