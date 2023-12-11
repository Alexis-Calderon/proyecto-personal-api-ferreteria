using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ferreteriaJuanito;

// Controlador destinado a la ejecucion de metodos del mantenedor de usuarios.
[ApiController]
[Route("api/[controller]")]
[Authorize]
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
    [Authorize(Roles = "administrador")]
    public IActionResult Get()
    {
        // Se genera un log en consola que especifica la accion que se ejecuta en mode de desarrollo.
        _logger.LogDebug("Metodo get de usuarios.");
        // Retorna en el body elementos en formato Json.
        return Ok(_usuariosService.Select());
    }

    // Metodo Post que agrega un nuevo usuario a la lista de usuarios del sistema.
    [HttpPost]
    [Authorize(Roles = "administrador")]
    public IActionResult Post(Usuario usuario)
    {
        // Se genera un log en consola que especifica la accion que se ejecuta en mode de desarrollo.
        _logger.LogDebug("Metodo post de usuarios.");
        // Se almacena el mensaje retorado por la funcion.
        string mensaje = _usuariosService.Create(usuario);
        // Retorna en el body el mensaje almacenado en la variable "mensaje".
        return Ok(mensaje);
    }

    [HttpPut]
    public IActionResult Put(Usuario usuario)
    {
        // Se extrae el id y el rol del usuario loguedo almacenado el las claims del jwt
        ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
        int idUsuarioLogueado = Int32.Parse(identity.Claims.FirstOrDefault(p => p.Type == "usuarioId").Value);
        Roles rol = (Roles)Enum.Parse(typeof(Roles), identity.Claims.FirstOrDefault(p=> p.Type == ClaimTypes.Role).Value);
        // Se valida que el rol del usuario logueado sea de cliente
        if (rol == Roles.cliente)
        {
            usuario.UsuarioId = idUsuarioLogueado;
            usuario.Rol = rol;
        }
        // Se genera un log en consola que especifica la accion que se ejecuta en mode de desarrollo.
        _logger.LogDebug("Metodo put de usuarios.");
        // Se almacena el mensaje retorado por la funcion.
        string mensaje = _usuariosService.Update(usuario);
        // Retorna en el body el mensaje almacenado en la variable "mensaje".
        return Ok(mensaje);
    }

    [HttpDelete]
    [Authorize(Roles = "administrador")]
    public IActionResult Delete(Usuario usuario)
    {
        // Se genera un log en consola que especifica la accion que se ejecuta en mode de desarrollo.
        _logger.LogDebug("Metodo delete de usuarios.");
        string mensaje;
        // Se extrae el id del usuario loguedo almacenado el las claims del jwt
        ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
        int idUsuarioLogueado = Int32.Parse(identity.Claims.FirstOrDefault(p => p.Type == "usuarioId").Value);
        if (idUsuarioLogueado == usuario.UsuarioId)
        {
            // Se almacena el mensaje retorado por la funcion.
            mensaje = "No se puede eliminar al usuario logueado.";
            // Retorna en el body el mensaje almacenado en la variable "mensaje".
            return Ok(mensaje);
        }
        // Se almacena el mensaje retorado por la funcion.
        mensaje = _usuariosService.Delete(usuario);
        // Retorna en el body el mensaje almacenado en la variable "mensaje".
        return Ok(mensaje);
    }
}
