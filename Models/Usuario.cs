using System.Text.Json.Serialization;

namespace ferreteriaJuanito;

public class Usuario
{
    public int UsuarioId { get; set; }
    public string Nombre { get; set; }
    public string Correo { get; set; }

    [JsonIgnore]
    public string Contraseña { get; set; }
    public Roles Rol { get; set; }

    [JsonIgnore]
    public virtual ICollection<Venta> Ventas { get; set; }
}