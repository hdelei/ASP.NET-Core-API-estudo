using Microsoft.EntityFrameworkCore;
using Livraria.API.Domain.Models;
using System;

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
            builder.Entity<Autor>().HasIndex(a => a.Nome).IsUnique();


            //builder.Entity<Autor>().HasMany
            //(p => p.Livros).WithOne(p => p.Autor).HasForeignKey(p => p.AutorId);

            builder.Entity<Autor>().HasData(
                //Id definido manualmente. Usando provedor in-memory
                new Autor
                {
                    Id = Guid.Parse("74c5be24-88b8-479b-ae68-f0140177d5eb"),
                    Nome = "Drauzio Varella"
                },
                new Autor
                {
                    Id = Guid.Parse("01299e9f-286a-4f53-a590-5cd8467a0c42"),
                    Nome = "Paulo Coelho"
                }
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
            builder.Entity<Livro>()
                .HasIndex(l => l.Titulo)
                .IsUnique();

            //builder.Entity<Livro>().Property(p => p.AutorId);


            builder.Entity<Livro>().HasData(
                //Id definido manualmente. Usando provedor in-memory
                new Livro
                {
                    Id = Guid.NewGuid(),
                    Titulo = "Carandiru",
                    AutorId = Guid.Parse("74c5be24-88b8-479b-ae68-f0140177d5eb")
                },
                new Livro
                {
                    Id = Guid.NewGuid(),
                    Titulo = "Brida",
                    AutorId = Guid.Parse("01299e9f-286a-4f53-a590-5cd8467a0c42")
                }
            );

        }
    }
}
