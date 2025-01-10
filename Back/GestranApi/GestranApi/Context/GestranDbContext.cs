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


            modelBuilder.Entity<Status>()
                       .HasKey(u => u.Id);

            modelBuilder.Entity<Item>()
                       .HasKey(u => u.Id);

            modelBuilder.Entity<Checklist>()
                       .HasKey(u => u.Id);

            modelBuilder.Entity<Checklist>()
                       .HasOne(o => o.UsuarioExecutor).WithMany()
                       .HasForeignKey(c => c.IdUsuarioExecutor)
                       .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Checklist>()
                       .HasOne(o => o.UsuarioAlteracao).WithMany()
                       .HasForeignKey(c => c.IdUsuarioAlteracao)
                       .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Checklist>()
                       .HasOne(o => o.Status).WithMany()
                       .HasForeignKey(c => c.IdStatus)
                       .OnDelete(DeleteBehavior.Restrict); ;

            modelBuilder.Entity<ChecklisItem>()
                       .HasKey(u => u.Id);

            modelBuilder.Entity<ChecklisItem>()
                    .HasOne(o => o.Checklist) // Relacionamento (1 para muitos)
                    .WithMany(c => c.ChecklisItens) // Relacionamento inverso
                    .HasForeignKey(o => o.IdChecklist) // Chave estrangeira
                    .OnDelete(DeleteBehavior.Cascade); // Comportamento de exclusão

            modelBuilder.Entity<ChecklisItem>()
                         .HasOne(o => o.Item) // Relacionamento (1 para muitos)
                         .WithMany(c => c.ItensChecklist) // Relacionamento inverso
                         .HasForeignKey(o => o.IdItem) // Chave estrangeira
                         .OnDelete(DeleteBehavior.Cascade); // Comportamento de exclusão
        }

        #region DbSet

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<TipoUsuario> TipoUsuario { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Checklist> Checklist { get; set; }
        public DbSet<ChecklisItem> ChecklisItem { get; set; }

        #endregion

    }
}