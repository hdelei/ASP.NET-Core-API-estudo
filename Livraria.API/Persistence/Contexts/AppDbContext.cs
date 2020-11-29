using Microsoft.EntityFrameworkCore;
using Livraria.API.Domain.Models;

namespace Livraria.API.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Autor> Autores { get; set; }
        public DbSet<Livro> Livros { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Autor>().ToTable("Autores");
            builder.Entity<Autor>().HasKey(a => a.Id);
            builder.Entity<Autor>().Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Autor>().Property(a => a.Nome).IsRequired().HasMaxLength(30);
            //builder.Entity<Autor>().HasMany
            //(p => p.Livros).WithOne(p => p.Autor).HasForeignKey(p => p.AutorId);

            builder.Entity<Autor>().HasData(
                //Id definido manualmente pois vamos usar o  provedor in-memory
                new Autor { Id = 100, Nome = "Drauzio Varella" },
                new Autor { Id = 101, Nome = "Paulo Coelho" }
            );

            builder.Entity<Livro>().ToTable("Livros");
            builder.Entity<Livro>().HasKey(l => l.Id);
            builder.Entity<Livro>().Property(l => l.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Livro>().Property(l => l.Titulo).IsRequired().HasMaxLength(50);
            builder.Entity<Livro>().Property(l => l.QuantidadeEstoque).IsRequired();
            builder.Entity<Livro>()
                .HasOne<Autor>()
                .WithMany()
                .HasForeignKey(a => a.AutorId);
            //builder.Entity<Livro>().Property(p => p.AutorId);


            builder.Entity<Livro>().HasData(
                //Id definido manualmente pois vamos usar o  provedor in-memory
                new Livro { Id = 100, Titulo = "Carandiru", AutorId = 100 },
                new Livro { Id = 101, Titulo = "Brida", AutorId = 101 }
            );

        }
    }
}
