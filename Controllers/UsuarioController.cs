using ferreteriaJuanito;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly ILogger<UsuarioController> _logger;
    public IUsuariosService _usuariosService;

    public UsuarioController(ILogger<UsuarioController> logger, IUsuariosService usuariosService)
    {
        _logger = logger;
        _usuariosService = usuariosService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_usuariosService.Select());
    }

    [HttpPost]
    public IActionResult Post([FromBody] Usuario usuario)
    {
        string mensaje = _usuariosService.Create(usuario);
        return Ok(mensaje);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Usuario usuario)
    {
        string mensaje = _usuariosService.Update(id, usuario);
        return Ok(mensaje);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        string mensaje = _usuariosService.Delete(id);
        return Ok(mensaje);
    }

}
