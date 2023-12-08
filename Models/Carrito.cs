namespace ferreteriaJuanito;

public class Carrito
{
    public int CarritoId { get; set; }
    public int UsuarioId { get; set; }
    public int ProductoId { get; set; }
    public int Cantidad { get; set; }
    public virtual Producto Productos { get; set; }
}