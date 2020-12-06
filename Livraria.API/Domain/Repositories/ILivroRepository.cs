using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Livraria.API.Domain.Models;

namespace Livraria.API.Domain.Repositories
{
    public interface ILivroRepository
    {
        Task<IEnumerable<Livro>> ListAsync();
        Task<Livro> FindByIdAsync(Livro livro);
        //Task<Livro> FindByIdAsync(int id);
        Task<Livro> AddAsync(Livro livro);

        Task<Livro> Update(Livro livro);
        //void Remove(Livro livro);
        Task<bool> DeleteAsync(Guid id);
    }
}
