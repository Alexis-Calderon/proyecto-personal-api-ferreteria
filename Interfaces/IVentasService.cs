namespace ferreteriaJuanito;

// Interface necesaria para la implementacion del servicio VentasService.
public interface IVentasService
{
    public IEnumerable<Venta> Select(int usuarioId);
    public string Create(int usuarioId);
}