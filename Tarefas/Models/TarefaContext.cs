using Microsoft.EntityFrameworkCore;

namespace Tarefas.Models
{
    public class TarefaContext : DbContext
    {
        public TarefaContext(DbContextOptions<TarefaContext> options) : base(options) { }


        public DbSet<Tarefa> Tarefas { get; set; } = null!;
        public DbSet<Categoria> Categorias { get; set; } = null!;
        public DbSet<Condicao> Condicoes { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>().HasData(

                new Categoria { CategoriaId = "trabalho", Nome = "Trabalho" },
                new Categoria { CategoriaId = "casa", Nome = "Casa" },
                new Categoria { CategoriaId = "ex", Nome = "Exercicio" },
                new Categoria { CategoriaId = "compra", Nome = "Compra" },
                new Categoria { CategoriaId = "contato", Nome = "Contato" }
                );

            modelBuilder.Entity<Condicao>().HasData(
                new Condicao { CondicaoId = "aberto", Nome = "Aberto" },
                new Condicao { CondicaoId = "concluido", Nome = "Concluido" }
                );
        }

    }
}
