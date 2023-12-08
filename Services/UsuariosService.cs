namespace ferreteriaJuanito;

public class UsuariosService : IUsuariosService
{
    private readonly ILogger<UsuariosService> _logger;
    private SqliteDBContext _context;

    public UsuariosService(ILogger<UsuariosService> logger, SqliteDBContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IEnumerable<Usuario> Select()
    {
        _logger.LogDebug("Extrae lista de usuarios.");
        return _context.Usuarios;
    }

    public string Create(Usuario usuario)
    {
        Usuario usuarioActual = (from a in _context.Usuarios
                                 where a.Correo == usuario.Correo
                                 select a).FirstOrDefault();
        if (usuarioActual == null)
        {
            usuario.Contraseña = Encrypt.GetSHA256(usuario.Contraseña);
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            _logger.LogDebug($"El usuario {usuario.Correo} se creo correctamente.");
            return $"El usuario {usuario.Correo} se creo correctamente.";
        }
        _logger.LogDebug($"El usuario {usuarioActual.Correo} ya existe.");
        return $"El usuario {usuarioActual.Correo} ya existe.";
    }

    public string Update(int usuarioId, Usuario usuario)
    {
        try
        {
            Usuario usuarioActual = _context.Usuarios.Find(usuarioId);
            if (usuarioActual != null)
            {
                usuarioActual.Nombre = usuario.Nombre;
                usuarioActual.Contraseña = Encrypt.GetSHA256(usuario.Contraseña);
                usuarioActual.Rol = usuario.Rol;
                _context.SaveChanges();
                _logger.LogDebug($"El usuario {usuario.Correo} se ha actualizado correctamente.");
                return $"El usuario {usuario.Correo} se ha actualizado correctamente.";
            }
            _logger.LogDebug("El usuario que intenta actualizar ya no existe.");
            return "El usuario que intenta actualizar ya no existe.";
        }
        catch (System.Exception ex)
        {
            _logger.LogCritical(ex.Message);
            return ex.Message;
        }
    }

    public string Delete(int usuarioId)
    {
        Usuario usuarioActual = _context.Usuarios.Find(usuarioId);
        if (usuarioActual != null)
        {
            _context.Usuarios.Remove(usuarioActual);
            _context.SaveChanges();
        }
        _logger.LogDebug($"El usuario {usuarioActual.Correo} se ha eliminado correctamente.");
        return $"El usuario {usuarioActual.Correo} se ha eliminado correctamente.";
    }
}