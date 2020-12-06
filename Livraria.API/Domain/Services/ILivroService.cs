using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Livraria.API.Domain.Models;

namespace Livraria.API.Domain.Services
{
    public interface ILivroService
    {
        Task<IEnumerable<Livro>> ListAsync();

        Task<IEnumerable<Livro>> FindByIdAsync(Guid id);
        Task<Livro> PostLivro(Livro livro);

        Task<bool> DeleteLivro(Guid id);
        //Task<bool> DeleteLivro(int id);
        Task<Livro> UpdateLivro(Livro livro);

    }
}
