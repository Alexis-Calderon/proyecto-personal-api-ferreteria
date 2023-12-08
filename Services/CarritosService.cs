
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ferreteriaJuanito;

public class CarritosServise : ICarritosService
{
    private readonly ILogger<CarritosServise> _logger;
    private SqliteDBContext _context;

    public CarritosServise(ILogger<CarritosServise> logger, SqliteDBContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IEnumerable<Carrito> Select(int usuarioId)
    {
        return _context.Carritos.Where(p=> p.UsuarioId == usuarioId).Include(p=>p.Productos);
    }

    public string Create(Carrito carrito)
    {
        Carrito carritoActual = _context.Carritos.Where(p=> p.UsuarioId == carrito.UsuarioId && p.ProductoId == carrito.ProductoId).FirstOrDefault();
        if (carritoActual == null)
        {
            _context.Carritos.Add(carrito);
            _context.SaveChanges();
            _logger.LogDebug("El producto ha sido agregado exitosamente al carrito.");
            return "El producto ha sido agregado exitosamente al carrito.";
        }
        _logger.LogDebug("El producto ya existe en el carrito.");
        return "El producto ya existe en el carrito.";
    }

    public string Update(Carrito carrito)
    {
        try
        {
            Carrito carritoActual = _context.Carritos.Where(p=> p.UsuarioId == carrito.UsuarioId && p.ProductoId == carrito.ProductoId).FirstOrDefault();
            if (carritoActual != null)
            {
                if (carrito.Cantidad > 0)
                {
                    carritoActual.Cantidad = carrito.Cantidad;
                    _context.SaveChanges();
                    _logger.LogDebug("La cantidad del producto se ha actualizado correctamente.");
                    return "La cantidad del producto se ha actualizado correctamente.";
                }
                else
                {
                    _context.Carritos.Remove(carritoActual);
                    _context.SaveChanges();
                    _logger.LogDebug("El producto se ha eliminado del carrito por tener cantidad cero.");
                    return "El producto se ha eliminado del carrito por tener cantidad cero.";
                }
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

    public string Delete(Carrito carrito)
    {
        Carrito carritoActual = _context.Carritos.Where(p=> p.UsuarioId == carrito.UsuarioId && p.ProductoId == carrito.ProductoId).FirstOrDefault();
        if (carritoActual != null)
        {
            _context.Carritos.Remove(carritoActual);
            _context.SaveChanges();
        }
        _logger.LogDebug("El producto se ha eliminado del carrito correctamente.");
        return "El producto se ha eliminado del carrito correctamente.";
    }
}
