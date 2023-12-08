namespace ferreteriaJuanito;

public interface IUsuariosService
{
    public IEnumerable<Usuario> Select();
    public string Create(Usuario usuario);
    public string Update(int usuarioId, Usuario usuario);
    public string Delete(int usuarioId);
}