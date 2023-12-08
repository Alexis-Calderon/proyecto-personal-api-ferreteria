using Microsoft.AspNetCore.Mvc;

namespace ferreteriaJuanito;

[ApiController]
[Route("api/[controller]")]
public class ProductoController : ControllerBase 
{
    private readonly ILogger<ProductoController> _logger;
    private IProductosService _productosService;

    public ProductoController(ILogger<ProductoController> logger, IProductosService productosService)
    {
        _logger = logger;
        _productosService = productosService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        _logger.LogDebug("Metodo get de productos.");
        return Ok(_productosService.Select());
    }

    [HttpPost]
    public IActionResult Post(Producto producto)
    {
        _logger.LogDebug("Metodo post de productos.");
        string mensaje = _productosService.Create(producto);
        return Ok(mensaje);
    }

    [HttpPut("{productoId}")]
    public IActionResult Put(int productoId, Producto producto)
    {
        _logger.LogDebug("Metodo put de productos.");
        string mensaje = _productosService.Update(productoId, producto);
        return Ok(mensaje);
    }

    [HttpDelete("{productoId}")]
    public IActionResult Delete(int productoId)
    {
        _logger.LogDebug("Metodo delete de productos.");
        string mensaje = _productosService.Delete(productoId);
        return Ok(mensaje);
    }
}