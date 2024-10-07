using FluentValidation;

namespace Fisoterapia.Request.Usuarios
{
    public record class CrearUsuario
    {
        public required string NombreUsuario { get; set; }

        public required string PrimerNombre { get; set; }

        public string? SegundoNombre { get; set; }

        public required string PrimerApellido { get; set; }

        public string? SegundoApellido { get; set; }

        public required string Contrasena { get; set; }
        public required string ConfirmarContrasena { get; set; }
        public int? IdTipoUsuario { get; set; }
    }
    public class CrearUsuarioValidator: AbstractValidator<CrearUsuario> 
    {
        public CrearUsuarioValidator() 
        {
            RuleFor(v => v.NombreUsuario).NotEmpty().MinimumLength(2).MaximumLength(15);
            RuleFor(v => v.PrimerNombre).NotEmpty().MinimumLength(3);
            RuleFor(v => v.PrimerApellido).NotEmpty().MinimumLength(2);
            RuleFor(v => v.Contrasena).NotEmpty();
            RuleFor(v => v.ConfirmarContrasena).NotEmpty();
        }
    }
}
