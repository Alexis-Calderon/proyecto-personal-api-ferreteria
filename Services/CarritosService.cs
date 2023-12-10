using Microsoft.EntityFrameworkCore;

namespace ferreteriaJuanito;

// Servicio destinado a la ejecucion de metodos del mantenedor del carrito.
public class CarritosServise : ICarritosService
{
    private readonly ILogger<CarritosServise> _logger;
    private SqliteDBContext _context;

    public CarritosServise(ILogger<CarritosServise> logger, SqliteDBContext context)
    {
        _logger = logger;
        _context = context;
    }

    // Metodo Select que devuelve una lista de los productos del carrito filtrados por el id del usuario logueado en el sistema.
    public IEnumerable<Carrito> Select(int usuarioId)
    {
        // Se genera un log en consola que especifica la accion que se ejecuta en mode de desarrollo.
        _logger.LogDebug("Extrae lista de productos incluidos en el carrito.");
        // Extrae los productos del carrito filtrados por el id del usuario logueado en el sistema.
        return _context.Carritos.Where(p => p.UsuarioId == usuarioId).Include(p => p.Producto);
    }

    // Metodo Create que agrega un producto al carrito del usuario logueado. 
    public string Create(Carrito carrito)
    {
        // Variable que almacena el mensaje que retorna la funcion 
        string mensaje;
        // Extrae producto del carrito flitrandolo por el id del producto y por el id se usuario logueado en el sistema.
        Carrito carritoActual = _context.Carritos.Where(p => p.UsuarioId == carrito.UsuarioId && p.ProductoId == carrito.ProductoId).FirstOrDefault();
        // Comprueba si el producto que se quiere agregar al carrito no exista previamnete en el carrito.
        if (carritoActual == null)
        {
            // Extrae producto de la lista de productos filtrado po el id del producto.
            Producto producto = _context.Productos.Where(p => p.ProductoId == carrito.ProductoId).FirstOrDefault();
            // Comprueba si el producto que se quiere agregar al carrito aun exista en la lista de productos del sistema.
            if (producto == null)
            {
                // Mensaje que se envia al log de consola y que se retorna en el metodo.
                mensaje = "El producto que intenta agregar al carrito ya no existe.";
                _logger.LogDebug(mensaje);
                return mensaje;
            }
            // Se actualiza la cantidad a 1 del producto que se desea agregar al carrito.
            // la cantidad despues se puede incrementar desde el metodo update de este mismo servicio.
            carrito.Cantidad = 1;
            // Se agrega el producto a la lista de carrito.
            _context.Carritos.Add(carrito);
            // Se actualizan los cambios con la base de datos
            _context.SaveChanges();
            // Mensaje que se envia al log de consola y que se retorna en el metodo.
            mensaje = "El producto ha sido agregado exitosamente al carrito.";
            _logger.LogDebug(mensaje);
            return mensaje;
        }
        // Mensaje que se envia al log de consola y que se retorna en el metodo.
        mensaje = "El producto ya existe en el carrito.";
        _logger.LogDebug(mensaje);
        return mensaje;
    }

    // Metodo Update que actualiza la cantidad del producto en el carrito del usuario logueado.
    public string Update(Carrito carrito)
    {
        // Variable que almacena el mensaje que retorna la funcion 
        string mensaje;
        // Extrae producto del carrito flitrandolo por el id del producto y por el id se usuario logueado en el sistema.
        Carrito carritoActual = _context.Carritos.Where(p => p.UsuarioId == carrito.UsuarioId && p.ProductoId == carrito.ProductoId).FirstOrDefault();
        // Comprueba si el producto que se quiere agregar al carrito exista previamnete en el carrito.
        if (carritoActual != null)
        {
            /* Valida que la cantidad que el usuario desea actualizar del producto en el carrito sea mayor a 0,
            En caso de que la cantidad sea igual o menor a 0 el producto se eliminara del carrito */
            if (carrito.Cantidad > 0)
            {
            // Extrae producto de la lista de productos filtrado po el id del producto.
                Producto producto = _context.Productos.Where(p => p.ProductoId == carrito.ProductoId).FirstOrDefault();
                // Validad que el stock del producto sea mayor o igual a la cantidad que el usuario desea actualizar del producto en el carrito
                if (producto.Stock < carrito.Cantidad)
                {
                    // En caso de que la cantidad que el usuario desea actualizar se mayor la misma se actualiza con la real en stock
                    carritoActual.Cantidad = producto.Stock;
                    // Se actualizan los cambios con la base de datos
                    _context.SaveChanges();
                    // Mensaje que se envia al log de consola y que se retorna en el metodo.
                    mensaje = $"El producto supera las {producto.Stock} unidades en stock.";
                    _logger.LogDebug(mensaje);
                    return mensaje;
                }
                // Se actualiza la cantidad del producto en el carrito. 
                carritoActual.Cantidad = carrito.Cantidad;
                // Se actualizan los cambios con la base de datos
                _context.SaveChanges();
                // Mensaje que se envia al log de consola y que se retorna en el metodo.
                mensaje = "La cantidad del producto se ha actualizado correctamente.";
                _logger.LogDebug(mensaje);
                return mensaje;
            }
            else
            {
                // Se elimina el producto de carrito por tener cantidad igual o menor a 0
                _context.Carritos.Remove(carritoActual);
                // Se actualizan los cambios con la base de datos
                _context.SaveChanges();
                // Mensaje que se envia al log de consola y que se retorna en el metodo.
                mensaje = "El producto se ha eliminado del carrito por tener cantidad cero.";
                _logger.LogDebug(mensaje);
                return mensaje;
            }
        }
        // Mensaje que se envia al log de consola y que se retorna en el metodo.
        mensaje = "El producto que intenta actualizar ya no existe.";
        _logger.LogDebug(mensaje);
        return mensaje;
    }

    // Metodo Delete que elimina el producto en el carrito del usuario logueado.
    public string Delete(Carrito carrito)
    {
        // Variable que almacena el mensaje que retorna la funcion 
        string mensaje;
        // Extrae producto del carrito flitrandolo por el id del producto y por el id se usuario logueado en el sistema.
        Carrito carritoActual = _context.Carritos.Where(p => p.UsuarioId == carrito.UsuarioId && p.ProductoId == carrito.ProductoId).FirstOrDefault();
        // Se valida que el producto aun exista en el carrito asociado al usuario logueado.
        if (carritoActual != null)
        {
            _context.Carritos.Remove(carritoActual);
            // Se actualizan los cambios con la base de datos
            _context.SaveChanges();
        }
        // Mensaje que se envia al log de consola y que se retorna en el metodo.
        mensaje = "El producto se ha eliminado del carrito correctamente.";
        _logger.LogDebug(mensaje);
        return mensaje;
    }
}