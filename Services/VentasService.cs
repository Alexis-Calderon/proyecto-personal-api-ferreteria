using Microsoft.EntityFrameworkCore;

namespace ferreteriaJuanito;

// Servicio destinado a la ejecucion de metodos del mantenedor de ventas.
public class VentasService : IVentasService
{
    private readonly ILogger<VentasService> _logger;
    private SqliteDBContext _context;

    public VentasService(ILogger<VentasService> logger, SqliteDBContext context)
    {
        _logger = logger;
        _context = context;
    }

    /* Metodo Select que devuelve una lista de todas las ventas con sus productos en caso con el campo "usuarioId" se 0 de lo contrario
     devuelve una lista de todas las ventas con sus productos del usuario logueado en el sistema.*/
    public IEnumerable<Venta> Select(int usuarioId)
    {
        return usuarioId == 0 ? _context.Ventas.Include(p => p.Usuario).Include(p => p.ventaDetalles).ThenInclude(p => p.Producto) :
        _context.Ventas.Where(p => p.UsuarioId == usuarioId).Include(p => p.Usuario).Include(p => p.ventaDetalles).ThenInclude(p => p.Producto);
    }

    // Metodo Create se genera una venta de todos los productos del carrito del usuario logueado.
    public string Create(int usuarioId)
    {
        // Variable que almacena el mensaje que retorna la funcion 
        string mensaje;
        // Extrae la lista de productos en el carrito del usuario logueado.
        IEnumerable<Carrito> carrito = _context.Carritos.Where(p => p.UsuarioId == usuarioId);
        // Valida que la lista sea igual a 0
        if (carrito.Count() == 0)
        {
            // Mensaje que se envia al log de consola y que se retorna en el metodo.
            mensaje = "El carrito que desea comprar ya no existe.";
            _logger.LogDebug(mensaje);
            return mensaje;
        }
        // Se crea un objeto del tipo "Venta" asignando los valores.
        Venta venta = new()
        {
            UsuarioId = usuarioId,
            Fecha = DateTime.Now
        };
        // Se agrega el objeto de tipo "Venta" al contexto
        _context.Ventas.Add(venta);
        // Se actualizan los cambios con la base de datos
        _context.SaveChanges();
        // Se genera un ciclo por la cantidad de productos existentes en el carrito
        foreach (Carrito item in carrito)
        {
            // Se crea un objeto del tipo "VentaDetalle" asignando los valores.
            VentaDetalle ventaDetalle = new()
            {
                VentaId = venta.VentaId,
                ProductoId = item.ProductoId,
                Cantidad = item.Cantidad,
                Precio = _context.Productos.Where(p => p.ProductoId == item.ProductoId).FirstOrDefault().Precio
            };
            // Se agrega el objeto de tipo "VentaDetalle" al contexto
            _context.VentaDetalles.Add(ventaDetalle);
            // Se actualizan los cambios con la base de datos
            _context.SaveChanges();
        }
        // Se eliminan los productos del carrito del usuario logueado.
        _context.Carritos.RemoveRange(carrito);
        // Se actualizan los cambios con la base de datos
        _context.SaveChanges();
        // Mensaje que se envia al log de consola y que se retorna en el metodo.
        mensaje = "El carrito se ha eliminado y se ha generado la venta.";
        _logger.LogDebug(mensaje);
        return mensaje;
    }
}
