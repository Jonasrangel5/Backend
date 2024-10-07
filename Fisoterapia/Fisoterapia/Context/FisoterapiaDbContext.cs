using Fisoterapia.Entities.Auth;
using Microsoft.EntityFrameworkCore;

namespace Fisoterapia.Context
{
    public class FisoterapiaDbContext : DbContext
    {
        public FisoterapiaDbContext(DbContextOptions<FisoterapiaDbContext> options) : base(options)
        {
        }
        public DbSet<TipoUsuario> TipoUsuarios { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

    }
}
