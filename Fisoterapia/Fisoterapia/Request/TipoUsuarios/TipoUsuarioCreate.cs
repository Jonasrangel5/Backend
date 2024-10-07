using FluentValidation;

namespace Fisoterapia.Request.TipoUsuarios
{
    public record class TipoUsuarioCreate
    {
        public required string Descripcion { get; set; }
    }
    public class TipoUsuarioCreateValidator : AbstractValidator<TipoUsuarioCreate>
    {
        public TipoUsuarioCreateValidator()
        {
            RuleFor(v => v.Descripcion).NotEmpty().MinimumLength(2);
        }
    }
}
