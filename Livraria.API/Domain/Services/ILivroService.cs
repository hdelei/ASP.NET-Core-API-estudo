using System.Collections.Generic;
using System.Threading.Tasks;
using Livraria.API.Domain.Models;

namespace Livraria.API.Domain.Services
{
    public interface ILivroService
    {
        Task<IEnumerable<Livro>> ListAsync();

        Task<IEnumerable<Livro>> FindByIdAsync(int id);
        Task<Livro> PostLivro(Livro livro);

        Task<bool> DeleteLivro(int id);
        //Task<bool> DeleteLivro(int id);

    }
}
