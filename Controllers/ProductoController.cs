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

    [HttpPut("{id}")]
    public IActionResult Put(int id, Producto producto)
    {
        _logger.LogDebug("Metodo put de productos.");
        string mensaje = _productosService.Update(id, producto);
        return Ok(mensaje);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _logger.LogDebug("Metodo delete de productos.");
        string mensaje = _productosService.Delete(id);
        return Ok(mensaje);
    }
}