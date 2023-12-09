using Microsoft.EntityFrameworkCore;

namespace ferreteriaJuanito;

public class VentasService : IVentasService
{
    private readonly ILogger<VentasService> _logger;
    private SqliteDBContext _context;

    public VentasService(ILogger<VentasService> logger, SqliteDBContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IEnumerable<Venta> Select(int usuarioId)
    {
        return usuarioId == 0 ? _context.Ventas.Include(p => p.Usuario).Include(p => p.ventaDetalles).ThenInclude(p => p.Producto) :
        _context.Ventas.Where(p => p.UsuarioId == usuarioId).Include(p => p.Usuario).Include(p => p.ventaDetalles).ThenInclude(p => p.Producto);
    }

    public string Create(int usuarioId)
    {
        IEnumerable<Carrito> carrito = _context.Carritos.Where(p => p.UsuarioId == usuarioId);
        if (carrito.Count() == 0)
        {
            _logger.LogDebug("El carrito que desea comprar ya no existe.");
            return "El carrito que desea comprar ya no existe.";
        }
        Venta venta = new()
        {
            UsuarioId = usuarioId,
            Fecha = DateTime.Now
        };
        _context.Ventas.Add(venta);
        _context.SaveChanges();
        foreach (Carrito item in carrito)
        {
            VentaDetalle ventaDetalle = new()
            {
                VentaId = venta.VentaId,
                ProductoId = item.ProductoId,
                Cantidad = item.Cantidad,
                Precio = _context.Productos.Where(p => p.ProductoId == item.ProductoId).FirstOrDefault().Precio
            };
            _context.VentaDetalles.Add(ventaDetalle);
            _context.SaveChanges();
        }
        _context.Carritos.RemoveRange(carrito);
        _context.SaveChanges();
        _logger.LogDebug("El carrito se ha eliminado y se ha generado la venta.");
        return "El carrito se ha eliminado y se ha generado la venta.";
    }
}
