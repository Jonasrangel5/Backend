namespace Fisoterapia.Entities.Auth
{
    public class Reseta
    {
        public int IdReseta { get; set; }
        public Guid IdUsuario { get; set; }
        public int IdLesion { get; set; }
        public string Evolucion { get; set; }
        public string Comentarios { get; set; }

        // Relaciones
        public Usuario Usuario { get; set; }
        public Lesion Lesion { get; set; }
    }
}
