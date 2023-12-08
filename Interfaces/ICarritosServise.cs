namespace ferreteriaJuanito;

public interface ICarritosService
{
    public IEnumerable<Carrito> Select(int usuarioId);
    public string Create(Carrito carrito);
    public string Update(Carrito carrito);
    public string Delete(Carrito carrito);
}