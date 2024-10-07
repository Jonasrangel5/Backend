using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Fisoterapia.Entities.Auth
{
    [Table("TipoUsuario", Schema = "usu")]
    public class TipoUsuario
    {
        [Key]
        [Column("idTipoUsuario")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTipoUsuario { get; set; }
        [Column("descripcion")]
        public string? Descripcion { get; set; }
        [Column("fechaCreacion")]
        public DateTime FechaCreacion { get; set; }
    }
}
