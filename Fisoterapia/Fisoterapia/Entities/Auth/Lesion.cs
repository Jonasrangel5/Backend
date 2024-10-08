using System.ComponentModel.DataAnnotations;

namespace Fisoterapia.Entities.Auth
{
    public class Lesion
    {
        [Key]  // Esta línea define que IdLesion es la clave primaria
        public int IdLesion { get; set; }
        public string Descripcion { get; set; }
    }
}
