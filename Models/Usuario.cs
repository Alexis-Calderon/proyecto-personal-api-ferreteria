using System.Text.Json.Serialization;

namespace ferreteriaJuanito;

// Modelo de datos de los usuarios en el sistema.
public class Usuario
{
    public int UsuarioId { get; set; }
    public string Nombre { get; set; }
    public string Correo { get; set; }
    public string Contraseña { get; set; }
    public Roles? Rol { get; set; }

    [JsonIgnore]
    public virtual ICollection<Venta> Ventas { get; set; }
}