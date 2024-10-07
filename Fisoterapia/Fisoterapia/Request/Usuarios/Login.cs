using FluentValidation;

namespace Fisoterapia.Request.Usuarios
{
    public record class Login
    {
        public required string NombreUsuario { get; set; }
        public required string Contrasena { get; set; }
    }
    public class LoginValidator : AbstractValidator<Login>
    {
        public LoginValidator() 
        {
            RuleFor(v => v.NombreUsuario).NotEmpty();
            RuleFor(v => v.Contrasena).NotEmpty();

        }
    }
}
