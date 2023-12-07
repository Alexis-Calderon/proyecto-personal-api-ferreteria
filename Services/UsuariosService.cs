using ferreteriaJuanito;

public class UsuariosService : IUsuariosService
{
    SqliteDBContext _context;
    public UsuariosService(SqliteDBContext context)
    {
        _context = context;
    }

    public IEnumerable<Usuario> Select()
    {
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
            _context.Add(usuario);
            _context.SaveChanges();
            return "El usuario se creo correctamente.";
        }
        return "El usuario ya existe.";
    }

    public string Update(int id, Usuario usuario)
    {
        try
        {
            Usuario usuarioActual = _context.Usuarios.Find(id);
            if (usuarioActual != null)
            {
                usuarioActual.Nombre = usuario.Nombre;
                usuarioActual.Contraseña = Encrypt.GetSHA256(usuario.Contraseña);
                usuarioActual.Rol = usuario.Rol;
                _context.SaveChanges();
                return "El usuario se ha actualizado correctamente.";
            }
            return "El usuario que intenta actualizar ya no existe.";
        }
        catch (System.Exception ex)
        {
            return ex.Message;
        }
    }

    public string Delete(int id)
    {
        Usuario usuarioActual = _context.Usuarios.Find(id);
        if (usuarioActual != null)
        {
            _context.Remove(usuarioActual);
            _context.SaveChanges();
        }
        return "El usuario se ha eliminado correctamente.";
    }
}