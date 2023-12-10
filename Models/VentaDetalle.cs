using System.Text.Json.Serialization;

namespace ferreteriaJuanito;

// Modelo de datos de los productos de las ventas en el sistema.
public class VentaDetalle
{
    public int VentaDetalleId { get; set; }
    public int VentaId { get; set; }
    public int ProductoId { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }

    [JsonIgnore]
    public virtual Venta Venta { get; set; }
    public virtual Producto Producto { get; set; }
}