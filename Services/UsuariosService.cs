namespace ferreteriaJuanito;

// Servicio destinado a la ejecucion de metodos del mantenedor de usuarios.
public class UsuariosService : IUsuariosService
{
    private readonly ILogger<UsuariosService> _logger;
    private SqliteDBContext _context;

    public UsuariosService(ILogger<UsuariosService> logger, SqliteDBContext context)
    {
        _logger = logger;
        _context = context;
    }

    // Metodo Select que devuelve una lista de los usuarios en el sistema.
    public IEnumerable<Usuario> Select()
    {
        _logger.LogDebug("Extrae lista de usuarios.");
        return _context.Usuarios;
    }

    // Metodo Create que agrega un nuevo usuario una lista de los usuarios del sistema validad que no exista previamente evitando duplicidad.
    public string Create(Usuario usuario)
    {
        // Variable que almacena el mensaje que retorna la funcion 
        string mensaje;
        // Extrae el usuario que se desea agregar usando LINQ
        Usuario usuarioActual = (from a in _context.Usuarios
                                 where a.Correo == usuario.Correo.ToLower()
                                 select a).FirstOrDefault();
        // Valida que el usuario no exista.
        if (usuarioActual == null)
        {
            // Se cifra la contraseña antes de almacenarla con SHA256.
            usuario.Contraseña = Encrypt.GetSHA256(usuario.Contraseña);
            // Todos los caracteres de cambian a minusculas.
            usuario.Correo = usuario.Correo.ToLower();
            // Se agrega el nuevo usuario al contexto de datos.
            _context.Usuarios.Add(usuario);
            // Se actualizan los cambios con la base de datos
            _context.SaveChanges();
            // Mensaje que se envia al log de consola y que se retorna en el metodo.
            mensaje = $"El usuario {usuario.Correo} se creo correctamente.";
            _logger.LogDebug(mensaje);
            return mensaje;
        }
        // Mensaje que se envia al log de consola y que se retorna en el metodo.
        mensaje = $"El usuario {usuarioActual.Correo} ya existe.";
        _logger.LogDebug(mensaje);
        return mensaje;
    }

    // Metodo Update que Actualiza un usuario especifico sus valores de nombre, contraseña y rol.
    public string Update(Usuario usuario)
    {
        // Variable que almacena el mensaje que retorna la funcion 
        string mensaje;
        
        // Extrae el usuario que se desea agregar usando LINQ
        Usuario usuarioActual = (from a in _context.Usuarios
                                 where a.Correo == usuario.Correo && a.UsuarioId != usuario.UsuarioId
                                 select a).FirstOrDefault();
        if (usuarioActual == null)
        {
            // Extrae el usuario que se desea actualizar.
            usuarioActual = _context.Usuarios.Find(usuario.UsuarioId);
            // Valida que el usuario exista y se asignan los valores.
            if (usuarioActual != null)
            {
                usuarioActual.Nombre = usuario.Nombre == null ? usuarioActual.Nombre : usuario.Nombre;
                usuarioActual.Correo = usuario.Correo == null ? usuarioActual.Correo : usuario.Correo;
                usuarioActual.Contraseña = usuario.Contraseña == null ? usuarioActual.Contraseña : Encrypt.GetSHA256(usuario.Contraseña);
                usuarioActual.Rol = usuario.Rol == null ? usuarioActual.Rol : usuario.Rol;
                // Se actualizan los cambios con la base de datos
                _context.SaveChanges();
                // Mensaje que se envia al log de consola y que se retorna en el metodo.
                mensaje = $"El usuario {usuarioActual.Correo} se ha actualizado correctamente.";
                _logger.LogDebug(mensaje);
                return mensaje;
            }
            // Mensaje que se envia al log de consola y que se retorna en el metodo.
            mensaje = "El usuario que intenta actualizar ya no existe.";
            _logger.LogDebug(mensaje);
            return mensaje;
        }
        // Mensaje que se envia al log de consola y que se retorna en el metodo.
        mensaje = $"El correo { usuarioActual.Correo } ya existe en otro usuario.";
        _logger.LogDebug(mensaje);
        return mensaje;
        
    }

    // Metodo Delete que elimina un usuario especifico solo cuando no existan ventas realcionadas.
    public string Delete(Usuario usuario)
    {
        // Variable que almacena el mensaje que retorna la funcion 
        string mensaje;
        // Extrae el usuario que se desea eliminar.
        Usuario usuarioActual = _context.Usuarios.Find(usuario.UsuarioId);
        // Valida que el usuario exista.
        if (usuarioActual != null)
        {
            // Se extrae una lista con las ventas asociadas al usuario que se va a eliminar.
            IEnumerable<Venta> venta = _context.Ventas.Where(p => p.UsuarioId == usuario.UsuarioId);
            // Se valida que existan ventas relacionadas.
            if (venta.Count() > 0)
            {
                // Mensaje que se envia al log de consola y que se retorna en el metodo.
                mensaje = $"El usuario {usuarioActual.Correo} no se ha eliminado porque tiene ventas asociadas.";
                _logger.LogDebug(mensaje);
                return mensaje;
            }
            // Se extrae una lista de productos en el carrito asociado al usuario que se va a eliminar.
            IEnumerable<Carrito> carrito = _context.Carritos.Where(p => p.UsuarioId == usuario.UsuarioId);
            // Se eliminan todos los productos en el carrito del usuario.
            _context.Carritos.RemoveRange(carrito);
            // Se elimina el usuario.
            _context.Usuarios.Remove(usuarioActual);
            // Se actualizan los cambios con la base de datos
            _context.SaveChanges();
            // Mensaje que se envia al log de consola y que se retorna en el metodo.
            mensaje = $"El usuario {usuarioActual.Correo} se ha eliminado correctamente, de igual manera se ha eliminado su carrito.";
            _logger.LogDebug(mensaje);
            return mensaje;
        }
        // Mensaje que se envia al log de consola y que se retorna en el metodo.
        mensaje = "No existe el usuario que desea eliminar.";
        _logger.LogDebug(mensaje);
        return mensaje;
    }
}