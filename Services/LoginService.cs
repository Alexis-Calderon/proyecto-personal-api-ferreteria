using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;

namespace ferreteriaJuanito;

public class LoginService : ILoginService
{
    private readonly ILogger<LoginService> _logger;
    private SqliteDBContext _context;

    private readonly IConfiguration _configuration;

    public LoginService(ILogger<LoginService> logger, SqliteDBContext context, IConfiguration configuration)
    {
        _logger = logger;
        _context = context;
        _configuration = configuration;
    }
    public Usuario Autenticar(Login login)
    {
        Usuario usuarioActual = _context.Usuarios.Where(p=>p.Correo.ToLower() == login.Correo.ToLower() && p.Contraseña == Encrypt.GetSHA256(login.Contraseña)).FirstOrDefault();
        if(usuarioActual != null)
        {
            return usuarioActual;
        }
        return null;
    }

    public string CrearToken(Usuario usuario)
    {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Crear los claims
            var claims = new[]
            {
                new Claim("usuarioId", usuario.UsuarioId.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.Email, usuario.Correo),
                new Claim(ClaimTypes.Role, usuario.Rol.ToString()),
            };


            // Crear el token

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(3),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
