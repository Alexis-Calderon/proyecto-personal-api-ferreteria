namespace ferreteriaJuanito;

// Modelo de datos de los productos dentro del carrito.
public class Carrito
{
    public int CarritoId { get; set; }
    public int UsuarioId { get; set; }
    public int ProductoId { get; set; }
    public int Cantidad { get; set; }
    public virtual Producto Producto { get; set; }
}