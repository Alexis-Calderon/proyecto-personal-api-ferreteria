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

    [HttpPut("{usuarioId}")]
    public IActionResult Put(int usuarioId, Usuario usuario)
    {
        _logger.LogDebug("Metodo put de usuarios.");
        string mensaje = _usuariosService.Update(usuarioId, usuario);
        return Ok(mensaje);
    }

    [HttpDelete("{usuarioId}")]
    public IActionResult Delete(int usuarioId)
    {
        _logger.LogDebug("Metodo delete de usuarios.");
        string mensaje = _usuariosService.Delete(usuarioId);
        return Ok(mensaje);
    }

}
