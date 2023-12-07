namespace ferreteriaJuanito;

public interface IProductosService
{
    public IEnumerable<Producto> Select();
    public string Create(Producto producto);
    public string Update(int id, Producto producto);
    public string Delete(int id);
} 