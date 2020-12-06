using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Livraria.API.Domain.Models;
using Livraria.API.Domain.Repositories;
using Livraria.API.Persistence.Contexts;
using System.Linq;
using System;

namespace Livraria.API.Persistence.Repositories
{
    public class LivroRepository : BaseRepository, ILivroRepository
    {

        //private const int AUTOR_DOES_NOT_EXIST = -1;
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

        public async Task<Livro> Update(Livro livro)
        {
            try
            {
                var result = await _dataset.SingleOrDefaultAsync(l => l.Id.Equals(livro.Id));
                if (result == null)
                {
                    return null;
                }

                _context.Entry(result).CurrentValues.SetValues(livro);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return livro;
            //_context.Livros.Update(livro);
        }

        async Task<Livro> ILivroRepository.AddAsync(Livro livro)
        {
            try
            {
                //var lastLivro = _context.Livros.OrderByDescending(l => livro.Id).Last();

                //livro.Id = lastLivro.Id + 1; //generate ID for the new book
                livro.Id = Guid.NewGuid(); //generate ID for the new book

                var livroNovo = new Livro
                {
                    Id = livro.Id,
                    Titulo = livro.Titulo,
                    AutorId = livro.AutorId
                };

                if (_context.Livros.Any(l => l.Titulo == livro.Titulo))
                {
                    //TODO: implement duplicate title
                }

                var autor = _context.Autores.
                    Where(a => a.Id == livroNovo.AutorId)
                    .FirstOrDefault();

                if (autor != null) //check if author already exists in database
                {
                    if (livroNovo.Titulo != string.Empty)
                    {
                        _context.Livros.Add(livroNovo);
                        await _context.SaveChangesAsync();
                    }

                }
                else
                {
                    livro.AutorId = Guid.Empty;
                }
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
            return livro;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var result = await _context.Livros.SingleOrDefaultAsync(l => l.Id.Equals(id));
                if (result == null)
                {
                    return false;
                }

                _context.Remove(result);

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
    }
}
