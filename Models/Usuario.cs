using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ferreteriaJuanito;

[Table("Usuarios")]
public class Usuario
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Nombre { get; set; }

    [Required]
    [StringLength(100)]
    public string Correo { get; set; }

    [Required]
    public string Contraseña { get; set; }

    [Required]
    public Roles Rol { get; set; }
}
