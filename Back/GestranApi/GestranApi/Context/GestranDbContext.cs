using GestranApi.Models.Entidades;
using Microsoft.EntityFrameworkCore;
namespace GestranApi.Context
{
    public class GestranDbContext : DbContext
    {
        public GestranDbContext(DbContextOptions<GestranDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<TipoUsuario>()
                       .HasKey(u => u.Id);
        }

        #region DbSet

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<TipoUsuario> TipoUsuario { get; set; }

        #endregion

    }
}