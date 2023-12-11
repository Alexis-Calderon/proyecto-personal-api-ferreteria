namespace ferreteriaJuanito;

// Interface necesaria para la implementacion del servicio CarritosService.
public interface ICarritosService
{
    public IEnumerable<Carrito> Select(int idUsuarioLogueado);
    public string Create(Carrito carrito, int idUsuarioLogueado);
    public string Update(Carrito carrito, int idUsuarioLogueado);
    public string Delete(Carrito carrito, int idUsuarioLogueado);
}