using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Fisoterapia.Entities.Auth
{
    [Table("Usuario", Schema = "usu")]
    public class Usuario
    {
        [Key]
        [Column("idUsuario")]
        public required Guid IdUsuario { get; set; }
        [Column("usuario")]
        [MaxLength(15)]
        public required string NombreUsuario { get; set; }
        [Column("primerNombre")]
        public required string PrimerNombre { get; set; }
        [Column("segundoNombre")]
        public string? SegundoNombre { get; set; }
        [Column("primerApellido")]
        public required string PrimerApellido { get; set; }
        [Column("segundoApellido")]
        public string? SegundoApellido { get; set; }
        [Column("nombreCompleto")]
        public required string NombreCompleto { get; set; }
        [Column("contraena")]
        public required string Contrasena { get; set; }
        [Column("fechaRegistro")]
        public DateTime FechaRegistro { get; set; }
        [Column("fechaActualizacion")]
        public DateTime? FechaActilizacion { get; set; }
        [Column("idTipoUsuario")]
        public int? IdTipoUsuario { get; set; }
        [ForeignKey("IdTipoUsuario")]
        public virtual TipoUsuario TipoUsuario { get; set; } = null!;
    }
}
