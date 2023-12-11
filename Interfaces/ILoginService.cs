namespace ferreteriaJuanito;

public interface ILoginService
{
    public Usuario Autenticar(Login login);
    public string CrearToken(Usuario usuario);
}