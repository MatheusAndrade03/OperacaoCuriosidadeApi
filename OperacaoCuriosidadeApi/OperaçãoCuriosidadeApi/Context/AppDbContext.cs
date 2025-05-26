using Microsoft.EntityFrameworkCore;
using OperaçãoCuriosidadeApi.Models;

namespace OperaçãoCuriosidadeApi.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Colaborador> Colaboradores { get; set; }
    }
}
