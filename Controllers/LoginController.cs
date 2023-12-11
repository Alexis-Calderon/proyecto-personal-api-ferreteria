using Microsoft.AspNetCore.Mvc;

namespace ferreteriaJuanito;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly ILogger<LoginController> _logger;
    private readonly ILoginService _loginService;

    public LoginController(ILogger<LoginController> logger, ILoginService loginService)
    {
        _logger = logger;
        _loginService = loginService;
    }

    // Metodo Post que recibe las credenciales ingresadas y devuele el token de autenticacion al FrontEnd.
    [HttpPost]
    public IActionResult Post(Login login)
    {
        Usuario usuario = _loginService.Autenticar(login);
        if (usuario != null)
        {
           string token =  _loginService.CrearToken(usuario);
           return Ok(token);
        }
        return NotFound("Usuario no existe");
    }
}