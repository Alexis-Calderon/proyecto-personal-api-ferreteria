using Microsoft.AspNetCore.Mvc;

namespace ferreteriaJuanito;

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

    [HttpGet("usuario/{usuarioId}")]
    public IActionResult Get(int usuarioId)
    {
        return Ok(_ventasService.Select(usuarioId));
    }

    [HttpPost("{usuarioId}")]
    public IActionResult Post(int usuarioId)
    {
        return Ok(_ventasService.Create(usuarioId));
    }
}