using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Livraria.API.Domain.Models;
using Livraria.API.Domain.Repositories;
using Livraria.API.Persistence.Contexts;

namespace Livraria.API.Persistence.Repositories
{
    public class AutorRepository : BaseRepository, IAutorRepository
    {
        public AutorRepository(AppDbContext context) : base(context)
        { }

        public async Task AddAsync(Autor autor)
        {
            await _context.Autores.AddAsync(autor);
        }

        public async Task<Autor> FindByIdAsync(int id)
        {
            return await _context.Autores.FindAsync(id);
        }

        public async Task<IEnumerable<Autor>> ListAsync()
        {
            return await _context.Autores.ToListAsync();
        }

        public void Remove(Autor autor)
        {
            _context.Autores.Remove(autor);
        }

        public void Update(Autor autor)
        {
            _context.Autores.Update(autor);
        }
    }
}
