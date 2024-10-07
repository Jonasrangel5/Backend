using Fisoterapia.Context;
using Fisoterapia.Entities.Auth;
using Fisoterapia.Models;
using Fisoterapia.Models.ResponseError;
using Fisoterapia.Request.TipoUsuarios;
using Fisoterapia.Request.Usuarios;
using Fisoterapia.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace Fisoterapia.Services.Clase
{
    public class AuthC : IAuth
    {
        private readonly FisoterapiaDbContext _context;
        private readonly string secretKey;
        public AuthC(FisoterapiaDbContext context, IConfiguration config) 
        {
            _context = context;
            secretKey = config.GetSection("settings").GetSection("secretkey").ToString();
        }

        
        public async Task<IdResponseInt> EditarTipo(TipoUsuarioEditar request, CancellationToken cancellationToken)
        {
            if (request.IdTipoUsuario <= 0)
            {
                throw new BadRequestException("El IdTipoUsuario proporcionado no es válido.");
            }
            var datos = await _context.TipoUsuarios
               .Where(d => d.IdTipoUsuario == request.IdTipoUsuario).FirstOrDefaultAsync();
            if (datos == null)
            {
                throw new BadRequestException("Tipo Usuario no encontrado");
            }
            //Guard.Against.NotFound(request.IdTipoUsuario, datos);

            datos.Descripcion = request.Descripcion;
            datos.FechaCreacion = request.Fecha;
            await _context.SaveChangesAsync(cancellationToken);

            return new IdResponseInt(datos.IdTipoUsuario);

        }

        public async Task<object> InicioSesion(Login request, CancellationToken cancellationToken)
        {
            try
            {
                var sesion = await _context.Usuario
                    .Where(s => s.NombreUsuario == request.NombreUsuario)
                    .FirstOrDefaultAsync(cancellationToken);
                if(sesion == null)
                {
                    throw new BadRequestException("Credenciales Invalidas");
                }
                string contrasenia = HashPassword(request.Contrasena);
                if (request.Contrasena != contrasenia)
                {
                    throw new BadRequestException("Credenciales Invalidas");
                }
                var KeyBytes = Encoding.ASCII.GetBytes(secretKey);
                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, request.NombreUsuario));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(180),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(KeyBytes), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                string tokencreado = tokenHandler.WriteToken(tokenConfig);
                var exito = new
                {
                    code = 100,
                    token = tokencreado,
                    sucursal = request.NombreUsuario
                };
                return exito;
            } catch (Exception ex) 
            {
                throw new BadRequestException("Algo salio mal en el progrma");
            }
        }

        public async Task<ResponseListDto<IEnumerable<TipoUsuario>>> ListarTipo(CancellationToken cancellationToken)
        {
           var datos = await _context.TipoUsuarios.ToListAsync(cancellationToken);
            return new ResponseListDto<IEnumerable<TipoUsuario>> { Data = datos };
        }

        public async Task<ResponseListDto<IEnumerable<Usuario>>> ListarUsuarios(CancellationToken cancellationToken)
        {
            var datos = await _context.Usuario.ToListAsync(cancellationToken);
            return new ResponseListDto<IEnumerable<Usuario>> { Data = datos };
        }

        public async Task<IdResponse> ModificarUsuario(EditarUsuario request, CancellationToken cancellationToken)
        {
            if (request.IdUsario == null)
            {
                throw new BadRequestException("El IdUsuario proporcionado no es válido.");
            }
            var datos = await _context.Usuario
                .Where(d => d.IdUsuario == request.IdUsario).FirstOrDefaultAsync();
            if (datos == null)
            {
                throw new BadRequestException("Usuario no encontrado");
            }
            DateTime Fecha =  DateTime.Now;
            string NombreCompleto = $"{request.PrimerNombre} {request.SegundoNombre}  {request.PrimerApellido} {request.SegundoApellido} ".Trim();
            NombreCompleto = System.Text.RegularExpressions.Regex.Replace(NombreCompleto, @"\s+", " ");
            string hashedPassword = HashPassword(request.Contrasena);
            datos.NombreUsuario = request.NombreUsuario;
            datos.PrimerNombre = request.PrimerNombre;
            datos.SegundoNombre = request.SegundoNombre;
            datos.PrimerApellido = request.PrimerApellido;
            datos.SegundoApellido = request.SegundoApellido;
            datos.IdTipoUsuario = request.IdTipoUsuario;
            datos.Contrasena = hashedPassword;
            datos.FechaRegistro = request.Fecha;
            datos.FechaActilizacion = Fecha;
            await _context.SaveChangesAsync(cancellationToken);
            return new IdResponse(datos.IdUsuario);

        }

        public async Task<IdResponseInt> RegistrarTipo(TipoUsuarioCreate request, CancellationToken cancellationToken)
        {
            DateTime fecha = DateTime.Now;
            var nuevo = new TipoUsuario
            {
                FechaCreacion = fecha,
                Descripcion = request.Descripcion
            };
            _context.TipoUsuarios.Add(nuevo);
            await _context.SaveChangesAsync(cancellationToken);
            return new IdResponseInt(nuevo.IdTipoUsuario);
        }

        public async Task<IdResponse> RegistrarUsuario(CrearUsuario request, CancellationToken cancellationToken)
        {
           Guid Id = Guid.NewGuid();
           DateTime Fecha = DateTime.Now;
            var entity = _context.Usuario.Where(e => e.NombreUsuario == request.NombreUsuario).FirstOrDefault();
            if(entity != null) 
            {
                throw new BadRequestException("El nombre de usuario ya existe");
            }
            string NombreCompleto = $"{request.PrimerNombre} {request.SegundoNombre}  {request.PrimerApellido} {request.SegundoApellido} ".Trim();
            NombreCompleto = System.Text.RegularExpressions.Regex.Replace(NombreCompleto, @"\s+", " ");
            string hashedPassword = HashPassword(request.Contrasena);
            var nuevo = new Usuario
            {
                IdUsuario = Id,
                NombreUsuario = request.NombreUsuario,
                PrimerNombre = request.PrimerNombre,
                SegundoNombre = request.SegundoNombre,
                PrimerApellido = request.PrimerApellido,
                SegundoApellido = request.SegundoApellido,
                Contrasena = hashedPassword,
                FechaRegistro = Fecha,
                IdTipoUsuario = request.IdTipoUsuario,
                NombreCompleto = NombreCompleto

            };
            _context.Usuario.Add(nuevo);
            await _context.SaveChangesAsync(cancellationToken);
            return new IdResponse(nuevo.IdUsuario);
        }

        public async Task<TipoUsuario> TipoId(int IdTipoUsuario, CancellationToken cancellationToken)
        {
            var datos = await _context.TipoUsuarios.Where(d => d.IdTipoUsuario == IdTipoUsuario).FirstOrDefaultAsync(cancellationToken);
            return datos;
        }

        public async Task<Usuario> UsuarioId(string IdUsuario, CancellationToken cancellationToken)
        {
            Guid idUsuario = Guid.Parse(IdUsuario);
            var datos = await _context.Usuario.Where(d => d.IdUsuario == idUsuario).FirstOrDefaultAsync(cancellationToken);
            return datos;
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
