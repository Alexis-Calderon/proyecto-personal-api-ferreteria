using ferreteriaJuanito;

public interface IUsuariosService
{
    public IEnumerable<Usuario> Select();
    public string Create(Usuario usuario);
    public string Update(int id, Usuario usuario);
    public string Delete(int id);
}