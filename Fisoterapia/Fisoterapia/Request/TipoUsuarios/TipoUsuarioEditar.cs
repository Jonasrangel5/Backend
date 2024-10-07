using FluentValidation;
namespace Fisoterapia.Request.TipoUsuarios
{
    public record class TipoUsuarioEditar
    {
        public required int IdTipoUsuario { get; set; }
        public required string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
    }
    public class TipoUsuarioEditarValidator : AbstractValidator<TipoUsuarioEditar>
    {
        public TipoUsuarioEditarValidator()
        {
            RuleFor(v => v.IdTipoUsuario).NotNull();
            RuleFor(v => v.Descripcion).NotEmpty().MinimumLength(2);
        }
    }
}
