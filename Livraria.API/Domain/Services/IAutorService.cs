using System.Collections.Generic;
using System.Threading.Tasks;
using Livraria.API.Domain.Models;

namespace Livraria.API.Domain.Services
{
    public interface IAutorService
    {
        Task<IEnumerable<Autor>> ListAsync();
    }
}
