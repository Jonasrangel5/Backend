using Fisoterapia.Models;
using Fisoterapia.Request.TipoUsuarios;
using Fisoterapia.Request.Usuarios;
using Fisoterapia.Services.Interface;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace Fisoterapia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _auth;
        public AuthController(IAuth auth)
        {
            _auth = auth;
        }
        [HttpPost]
        public async Task<IdResponseInt> CrearTipo([FromBody] TipoUsuarioCreate model, CancellationToken cancellationToken)
        {
            var resultado = await _auth.RegistrarTipo(model, cancellationToken);
            return resultado;
        }
        [HttpPost("Login")]
        public async Task<object> Login([FromBody] Login request, CancellationToken cancellationToken)
        {
            var resultado = await _auth.InicioSesion( request, cancellationToken);
            return resultado;
        }
        [HttpPut("{IdTipoUsuario}")]
        public async Task<IdResponseInt> EditarTipo(int IdTipoUsuario, [FromBody] TipoUsuarioEditar model, CancellationToken cancellationToken)
        {
            var resultado = await _auth.EditarTipo(model, cancellationToken);
            return resultado;
        }
        [HttpPost("CrearUsuario")]
        public async Task<IdResponse> CrearUsuario([FromBody] CrearUsuario model, CancellationToken cancellationToken)
        {
            var resultado = await _auth.RegistrarUsuario(model, cancellationToken);
            return resultado;
        }
    }
}
