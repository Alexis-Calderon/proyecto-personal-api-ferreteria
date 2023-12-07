using Microsoft.AspNetCore.Mvc;

namespace ferreteriaJuanito;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly ILogger<UsuarioController> _logger;
    private IUsuariosService _usuariosService;

    public UsuarioController(ILogger<UsuarioController> logger, IUsuariosService usuariosService)
    {
        _logger = logger;
        _usuariosService = usuariosService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        _logger.LogDebug("Metodo get de usuarios.");
        return Ok(_usuariosService.Select());
    }

    [HttpPost]
    public IActionResult Post([FromBody] Usuario usuario)
    {
        _logger.LogDebug("Metodo post de usuarios.");
        string mensaje = _usuariosService.Create(usuario);
        return Ok(mensaje);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Usuario usuario)
    {
        _logger.LogDebug("Metodo put de usuarios.");
        string mensaje = _usuariosService.Update(id, usuario);
        return Ok(mensaje);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _logger.LogDebug("Metodo delete de usuarios.");
        string mensaje = _usuariosService.Delete(id);
        return Ok(mensaje);
    }

}
