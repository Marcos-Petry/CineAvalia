using CineAvalia.Models;
using Microsoft.EntityFrameworkCore;

namespace CineAvalia.Data
{
    public class CineAvaliaContext : DbContext 
    {
        public CineAvaliaContext(DbContextOptions<CineAvaliaContext> options) : base(options)
        { 
        }

        public DbSet<Filme> Filme { get; set; }

        public DbSet<Genero> Genero { get; set; }

        public DbSet<Produtora> Produtora { get; set; }

        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<Avaliacao> Avaliacao { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração de relacionamento entre Filme e Genero
            modelBuilder.Entity<Filme>()
               .HasOne(f => f.Genero)
               .WithMany() // Indica que um Genero pode ter muitos Filmes
               .HasForeignKey(f => f.GeneroId);

            // Configuração de relacionamento entre Filme e Produtora
            modelBuilder.Entity<Filme>()
               .HasOne(f => f.Produtora)
               .WithMany() // Indica que uma Produtora pode ter muitos Filmes
               .HasForeignKey(f => f.ProdutoraId);

            modelBuilder.Entity<Filme>()
              .HasMany(f => f.Avaliacoes) // Muda para 'HasMany' para indicar que um Filme pode ter muitas Avaliações
              .WithOne(a => a.Filme) // Mantém 'WithOne' para indicar que uma Avaliação está associada a um único Filme
              .HasForeignKey(a => a.FilmeId); // 'FilmeId' é a chave estrangeira na tabela 'Avaliacao'


            modelBuilder.Entity<Avaliacao>()
              .HasOne(a => a.Usuario)
              .WithMany(u => u.Avaliacoes) // Corrigido para refletir a propriedade correta no modelo Usuario
              .HasForeignKey(a => a.UsuarioId);




        }

    }
}
