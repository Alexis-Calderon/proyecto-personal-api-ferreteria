using Microsoft.AspNetCore.Mvc;

namespace ferreteriaJuanito;

// Controlador destinado a la ejecucion de metodos del mantenedor de usuarios.
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

    // Metodo Get que lista de todos los usuarios que existen en sistema.
    [HttpGet]
    public IActionResult Get()
    {
        // Se genera un log en consola que especifica la accion que se ejecuta en mode de desarrollo.
        _logger.LogDebug("Metodo get de usuarios.");
        // Retorna en el body elementos en formato Json.
        return Ok(_usuariosService.Select());
    }

    // Metodo Post que agrega un nuevo usuario a la lista de usuarios del sistema.
    [HttpPost]
    public IActionResult Post([FromBody] Usuario usuario)
    {
        // Se genera un log en consola que especifica la accion que se ejecuta en mode de desarrollo.
        _logger.LogDebug("Metodo post de usuarios.");
        // Se almacena el mensaje retorado por la funcion.
        string mensaje = _usuariosService.Create(usuario);
        // Retorna en el body el mensaje almacenado en la variable "mensaje".
        return Ok(mensaje);
    }

    [HttpPut("{usuarioId}")]
    public IActionResult Put(int usuarioId, Usuario usuario)
    {
        // Se genera un log en consola que especifica la accion que se ejecuta en mode de desarrollo.
        _logger.LogDebug("Metodo put de usuarios.");
        // Se almacena el mensaje retorado por la funcion.
        string mensaje = _usuariosService.Update(usuarioId, usuario);
        // Retorna en el body el mensaje almacenado en la variable "mensaje".
        return Ok(mensaje);
    }

    [HttpDelete("{usuarioId}")]
    public IActionResult Delete(int usuarioId)
    {
        // Se genera un log en consola que especifica la accion que se ejecuta en mode de desarrollo.
        _logger.LogDebug("Metodo delete de usuarios.");
        // Se almacena el mensaje retorado por la funcion.
        string mensaje = _usuariosService.Delete(usuarioId);
        // Retorna en el body el mensaje almacenado en la variable "mensaje".
        return Ok(mensaje);
    }
}
