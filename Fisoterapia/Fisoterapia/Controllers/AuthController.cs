using Fisoterapia.Context;
using Fisoterapia.Entities.Auth;
using Fisoterapia.Models;
using Fisoterapia.Request.TipoUsuarios;
using Fisoterapia.Request.Usuarios;
using Fisoterapia.Services.Interface;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Fisoterapia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _auth;
        private readonly FisoterapiaDbContext _context;

        public AuthController(IAuth auth, FisoterapiaDbContext context)
        {
            _auth = auth;
            _context = context;
        }

        // Endpoints existentes...
        [HttpPost]
        public async Task<IdResponseInt> CrearTipo([FromBody] TipoUsuarioCreate model, CancellationToken cancellationToken)
        {
            var resultado = await _auth.RegistrarTipo(model, cancellationToken);
            return resultado;
        }

        [HttpPost("Login")]
        public async Task<object> Login([FromBody] Login request, CancellationToken cancellationToken)
        {
            var resultado = await _auth.InicioSesion(request, cancellationToken);
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

        // Nuevos Endpoints para Reseta
        [HttpGet("Resetas")]
        public async Task<IActionResult> GetAllResetas(CancellationToken cancellationToken)
        {
            var resetas = await _context.Resetas.ToListAsync(cancellationToken);
            return Ok(resetas);
        }

        [HttpPost("Reseta")]
        public async Task<IActionResult> CreateReseta([FromBody] Reseta reseta, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Resetas.Add(reseta);
            await _context.SaveChangesAsync(cancellationToken);
            return Ok(reseta);
        }

        // Nuevos Endpoints para InformaAvance
        [HttpGet("InformasAvance")]
        public async Task<IActionResult> GetAllInformasAvance(CancellationToken cancellationToken)
        {
            var informes = await _context.InformasAvance.ToListAsync(cancellationToken);
            return Ok(informes);
        }

        [HttpPost("InformaAvance")]
        public async Task<IActionResult> CreateInformaAvance([FromBody] InformaAvance informaAvance, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.InformasAvance.Add(informaAvance);
            await _context.SaveChangesAsync(cancellationToken);
            return Ok(informaAvance);
        }
    }
}
