namespace ferreteriaJuanito;

// Modelo de datos de las ventas en el sistema.
public class Venta
{
    public int VentaId { get; set; }
    public int UsuarioId { get; set; }
    public DateTime Fecha { get; set; }
    public virtual Usuario Usuario { get; set; }
    public virtual ICollection<VentaDetalle> ventaDetalles { get; set; }
}