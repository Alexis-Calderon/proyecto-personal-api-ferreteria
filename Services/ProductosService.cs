namespace ferreteriaJuanito;

public class ProductosService : IProductosService
{
    private readonly ILogger<ProductosService> _logger;
    private SqliteDBContext _context;

    public ProductosService(ILogger<ProductosService> logger, SqliteDBContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IEnumerable<Producto> Select()
    {
        _logger.LogDebug("Extrae lista de productos.");
        return _context.Productos;
    }

    public string Create(Producto producto)
    {
        Producto productoActual = _context.Productos.Where(p=> p.Nombre == producto.Nombre).FirstOrDefault();
        if (productoActual == null)
        {
            _context.Add(producto);
            _context.SaveChanges();
            _logger.LogDebug($"El producto {producto.Nombre} ha sido creado exitosamente");
            return $"El producto {producto.Nombre} ha sido creado exitosamente";
        }
        _logger.LogDebug($"El producto {productoActual.Nombre} ya existe.");
        return $"El producto {productoActual.Nombre} ya existe.";
    }

    public string Update(int id, Producto producto)
    {
        try
        {
            Producto productoActual = _context.Productos.Find(id);
            if (productoActual != null)
            {
                productoActual.Nombre = producto.Nombre;
                productoActual.Precio = producto.Precio;
                productoActual.UnidadesDeMedida = producto.UnidadesDeMedida;
                productoActual.Stock = producto.Stock;
                _context.SaveChanges();
                _logger.LogDebug($"El producto {producto.Nombre} se ha actualizado correctamente.");
                return $"El producto {producto.Nombre} se ha actualizado correctamente.";
            }
            _logger.LogDebug("El producto que intenta actualizar ya no existe.");
            return "El producto que intenta actualizar ya no existe.";
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return ex.Message;
        }
    }

    public string Delete(int id)
    {
        Producto productoActual = _context.Productos.Find(id);
        if (productoActual != null)
        {
            _context.Productos.Remove(productoActual);
            _context.SaveChanges();
        }
        _logger.LogDebug($"El producto {productoActual.Nombre} se ha eliminado correctamente.");
        return $"El producto {productoActual.Nombre} se ha eliminado correctamente.";
    }
}
