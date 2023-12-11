namespace ferreteriaJuanito;

// Servicio destinado a la ejecucion de metodos del mantenedor de productos.
public class ProductosService : IProductosService
{
    private readonly ILogger<ProductosService> _logger;
    private SqliteDBContext _context;

    public ProductosService(ILogger<ProductosService> logger, SqliteDBContext context)
    {
        _logger = logger;
        _context = context;
    }

    // Metodo Select que devuelve una lista de los productos en el sistema.
    public IEnumerable<Producto> Select()
    {
        _logger.LogDebug("Extrae lista de productos.");
        return _context.Productos;
    }

    // Metodo Create que agrega un nuevo producto una lista de los productos del sistema validad que no exista previamente evitando duplicidad.
    public string Create(Producto producto)
    {
        // Variable que almacena el mensaje que retorna la funcion 
        string mensaje;
        // Extrae el producto de la lista del sistema.
        Producto productoActual = _context.Productos.Where(p => p.Nombre == producto.Nombre).FirstOrDefault();
        // Valida que no exista el producto en la base de datos
        if (productoActual == null)
        {
            // Agrega el producto al contexto de datos.
            _context.Productos.Add(producto);
            // Se actualizan los cambios con la base de datos
            _context.SaveChanges();
            // Mensaje que se envia al log de consola y que se retorna en el metodo.
            mensaje = $"El producto {producto.Nombre} ha sido creado exitosamente";
            _logger.LogDebug(mensaje);
            return mensaje;
        }
        // Mensaje que se envia al log de consola y que se retorna en el metodo.
        mensaje = $"El producto {productoActual.Nombre} ya existe.";
        _logger.LogDebug(mensaje);
        return mensaje;
    }

    // Metodo Update que Actualiza un producto especifico sus valores de nombre, precio, unidad de medida y el stock.
    public string Update(Producto producto)
    {
        // Variable que almacena el mensaje que retorna la funcion 
        string mensaje;
        // Extrae el producto de la lista del sistema.
        Producto productoActual = _context.Productos.Where(p=>p.Nombre == producto.Nombre && p.ProductoId != producto.ProductoId).FirstOrDefault();
        // Valida que exista el producto en la base de datos y asigna sus valores.
        if (productoActual == null)
        {
            productoActual =  _context.Productos.Where(p=>p.ProductoId == producto.ProductoId).FirstOrDefault();
            if (productoActual != null)
            {
                productoActual.Nombre = producto.Nombre;
                productoActual.Precio = producto.Precio;
                productoActual.UnidadesDeMedida = producto.UnidadesDeMedida;
                productoActual.Stock = producto.Stock;
                // Se actualizan los cambios con la base de datos
                _context.SaveChanges();
                // Mensaje que se envia al log de consola y que se retorna en el metodo.
                mensaje = $"El producto {producto.Nombre} se ha actualizado correctamente.";
                _logger.LogDebug(mensaje);
                return mensaje;
            }
            // Mensaje que se envia al log de consola y que se retorna en el metodo.
            mensaje = "El producto que intenta actualizar ya no existe.";
            _logger.LogDebug(mensaje);
            return mensaje;
        }
        // Mensaje que se envia al log de consola y que se retorna en el metodo.
        mensaje = "Ya existe otro producto con el mismo nombre.";
        _logger.LogDebug(mensaje);
        return mensaje;
    }

    // Metodo Delete que elimina un producto especifico y todas las entradas en los carritos.
    public string Delete(Producto producto)
    {
        // Variable que almacena el mensaje que retorna la funcion 
        string mensaje;
        // Extrae el producto de la lista del sistema.
        Producto productoActual = _context.Productos.Find(producto.ProductoId);
        // Valida que exista el producto en la base de datos.
        if (productoActual != null)
        {
            // Extrae todas las entradas que tiene el producto en los carritos.
            IEnumerable<Carrito> carrito = _context.Carritos.Where(p => p.ProductoId == producto.ProductoId);
            // Elimina todas las entradas del producto en todos los carritos.
            _context.Carritos.RemoveRange(carrito);
            // Elimina el producto de la lista de productos del sistema.
            _context.Productos.Remove(productoActual);
            // Se actualizan los cambios con la base de datos
            _context.SaveChanges();
            // Mensaje que se envia al log de consola y que se retorna en el metodo.
            mensaje = $"El producto {productoActual.Nombre} se ha eliminado correctamente, de igual manera han sido eliminadas todas las entradas en los carritos.";
            _logger.LogDebug(mensaje);
            return mensaje;
        }
        // Mensaje que se envia al log de consola y que se retorna en el metodo.
        mensaje = "No existe el producto que desea eliminar.";
        _logger.LogDebug(mensaje);
        return mensaje;
    }
}