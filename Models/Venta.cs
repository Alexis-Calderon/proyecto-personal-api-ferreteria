namespace ferreteriaJuanito;

public class Venta
{
    public int VentaId { get; set; }
    public int UsuarioId { get; set; }
    public DateTime Fecha { get; set; }
    public virtual Usuario Usuario { get; set; }
    public virtual ICollection<VentaDetalle> ventaDetalles { get; set; }
}