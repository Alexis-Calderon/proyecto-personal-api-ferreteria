namespace ferreteriaJuanito;

// Interface necesaria para la implementacion del servicio UsuariosService.
public interface IUsuariosService
{
    public IEnumerable<Usuario> Select();
    public string Create(Usuario usuario);
    public string Update(Usuario usuario);
    public string Delete(Usuario usuario);
}