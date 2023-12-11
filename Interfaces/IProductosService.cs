namespace ferreteriaJuanito;

// Interface necesaria para la implementacion del servicio ProductosService.
public interface IProductosService
{
    public IEnumerable<Producto> Select();
    public string Create(Producto producto);
    public string Update(Producto producto);
    public string Delete(Producto producto);
} 