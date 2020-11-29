using System.Collections.Generic;
using System.Threading.Tasks;
using Livraria.API.Domain.Models;

namespace Livraria.API.Domain.Repositories
{
    public interface IAutorRepository
    {
        Task<IEnumerable<Autor>> ListAsync();
        Task AddAsync(Autor autor);
        Task<Autor> FindByIdAsync(int id);
        void Update(Autor autor);
        void Remove(Autor autor);
    }
}
