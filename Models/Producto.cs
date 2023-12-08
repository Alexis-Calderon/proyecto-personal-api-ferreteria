using System.Text.Json.Serialization;

namespace ferreteriaJuanito;

public class Producto
{
    public int ProductoId { get; set; }
    public string Nombre { get; set; }
    public int Precio { get; set; }
    public UnidadesDeMedida UnidadesDeMedida { get; set; }
    public int Stock { get; set; }

    [JsonIgnore]
    public virtual ICollection<Carrito> Carritos { get; set; }
}