using Fisoterapia.Entities.Auth;
using Fisoterapia.Models;
using Fisoterapia.Request.TipoUsuarios;
using Fisoterapia.Request.Usuarios;
using MassTransit.SagaStateMachine;

namespace Fisoterapia.Services.Interface
{
    public interface IAuth
    {

        public  Task<IdResponseInt> RegistrarTipo(TipoUsuarioCreate request, CancellationToken cancellationToken);
        public Task<IdResponseInt> EditarTipo(TipoUsuarioEditar request, CancellationToken cancellationToken);
        public Task<IdResponse> RegistrarUsuario(CrearUsuario request, CancellationToken cancellationToken);
        public Task<IdResponse> ModificarUsuario(EditarUsuario request, CancellationToken cancellationToken);
        public Task<ResponseListDto<IEnumerable<TipoUsuario>>> ListarTipo(CancellationToken cancellationToken);
        public Task<ResponseListDto<IEnumerable<Usuario>>> ListarUsuarios(CancellationToken cancellationToken);
        public Task<TipoUsuario> TipoId(int IdTipoUsuario, CancellationToken cancellationToken);
        public Task<Usuario> UsuarioId(string IdUsuario, CancellationToken cancellationToken);
        public Task<object> InicioSesion(Login request, CancellationToken cancellationToken);
    }
}
