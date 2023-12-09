namespace ferreteriaJuanito;

public interface IVentasService
{
    public IEnumerable<Venta> Select(int usuarioId);
    public string Create(int usuarioId);
}