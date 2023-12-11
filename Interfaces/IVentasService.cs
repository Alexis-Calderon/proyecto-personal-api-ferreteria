namespace ferreteriaJuanito;

// Interface necesaria para la implementacion del servicio VentasService.
public interface IVentasService
{
    public IEnumerable<Venta> Select(int? idUsuarioLogueado);
    public string Create(int idUsuarioLogueado);
}