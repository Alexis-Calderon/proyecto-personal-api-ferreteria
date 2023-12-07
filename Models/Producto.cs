using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ferreteriaJuanito;

[Table("Productos")]
public class Producto
{

    [Key]
    [Required]
    public int id { get; set; }

    [StringLength(100)]
    [Required]
    public string Nombre { get; set; }

    [Required]
    public int Precio { get; set; }

    [Required]
    public UnidadesDeMedida UnidadesDeMedida { get; set; }

    [Required]
    public int Stock { get; set; }
}