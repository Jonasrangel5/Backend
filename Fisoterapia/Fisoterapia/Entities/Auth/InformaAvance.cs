using System.ComponentModel.DataAnnotations;

namespace Fisoterapia.Entities.Auth
{
    public class InformaAvance
    {
        [Key]  // Esta línea marca IdInforma como clave primaria
        public int IdInforma { get; set; }
        public Guid IdUsuario { get; set; }
        public int IdLesion { get; set; }
        public string EvolucionLesion { get; set; }
        public string EjerciciosRealizados { get; set; }
        public string Comentarios { get; set; }

        // Relaciones
        public Usuario Usuario { get; set; }
        public Lesion Lesion { get; set; }
    }
}
