using System.Text.Json.Serialization;

namespace ferreteriaJuanito;

// Modelo de datos de los productos del sistema.
public class Producto
{
    public int ProductoId { get; set; }
    public string Nombre { get; set; }
    public decimal Precio { get; set; }
    public UnidadesDeMedida UnidadesDeMedida { get; set; }
    public int Stock { get; set; }

    [JsonIgnore]
    public virtual ICollection<Carrito> Carritos { get; set; }

    [JsonIgnore]
    public virtual ICollection<VentaDetalle> ventaDetalles { get; set; }
}