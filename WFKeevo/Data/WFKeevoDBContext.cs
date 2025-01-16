using Microsoft.EntityFrameworkCore;
using WFKeevo.Models;

namespace WFKeevo.Data
{
    public class WFKeevoDBContext : DbContext
    {
        // Metodo construtor padrão do entity framework
        public WFKeevoDBContext(DbContextOptions<WFKeevoDBContext> options) : base(options)
        {

        }

        public DbSet<Tarefa> Tarefa { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Lancto> Lancto { get; set; }
    }
}
