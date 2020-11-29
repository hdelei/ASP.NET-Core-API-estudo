using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Livraria.API.Domain.Models;
using Livraria.API.Domain.Repositories;
using Livraria.API.Persistence.Contexts;
using System.Linq;

namespace Livraria.API.Persistence.Repositories
{
    public class LivroRepository : BaseRepository, ILivroRepository
    {

        private const int AUTOR_DOES_NOT_EXIST = -1;
        public LivroRepository(AppDbContext context) : base(context)
        {
            _dataset = _context.Set<Livro>();
        }

        private DbSet<Livro> _dataset;

        public async Task AddAsync(Livro livro)
        {
            await _context.Livros.AddAsync(livro);
        }

        public async Task<Livro> FindByIdAsync(Livro livro)
        {
            return await _context.Livros.FindAsync(livro);
        }

        public async Task<IEnumerable<Livro>> ListAsync()
        {
            return await _context.Livros.ToListAsync();
        }

        public void Remove(Livro livro)
        {
            _context.Livros.Remove(livro);
        }

        public void Update(Livro livro)
        {
            _context.Livros.Update(livro);
        }

        async Task<Livro> ILivroRepository.AddAsync(Livro livro)
        {
            try
            {
                var lastLivro = _context.Livros.OrderByDescending(l => livro.Id).Last();

                livro.Id = lastLivro.Id + 1; //generate ID for the new book

                var livroNovo = new Livro
                {
                    Id = livro.Id,
                    Titulo = livro.Titulo,
                    AutorId = livro.AutorId
                };

                var autor = _context.Autores.
                    Where(a => a.Id == livroNovo.AutorId)
                    .FirstOrDefault();

                if (autor != null) //check if author already exists in database
                {
                    _context.Livros.Add(livroNovo);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    livro.AutorId = AUTOR_DOES_NOT_EXIST;
                }
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
            return livro;
        }
    }
}
