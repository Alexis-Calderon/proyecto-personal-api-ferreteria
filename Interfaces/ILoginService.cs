namespace ferreteriaJuanito;

// Interface necesaria para la implementacion del servicio LoginService.
public interface ILoginService
{
    public Usuario Autenticar(Login login);
    public string CrearToken(Usuario usuario);
}