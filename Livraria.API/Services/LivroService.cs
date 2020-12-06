using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Livraria.API.Domain.Models;
using Livraria.API.Domain.Repositories;
using Livraria.API.Domain.Services;

namespace Livraria.API.Services
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _livroRepository;
        public LivroService(ILivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }

        public async Task<bool> DeleteLivro(Guid id)
        {
            return await _livroRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Livro>> FindByIdAsync(Guid id)
        {
            var livros = await _livroRepository.ListAsync();
            return livros.Where(l => l.Id.Equals(id));
        }

        public async Task<IEnumerable<Livro>> ListAsync()
        {
            return await _livroRepository.ListAsync();
        }

        public async Task<Livro> PostLivro(Livro livro)
        {
            return await _livroRepository.AddAsync(livro);
        }

    }
}
