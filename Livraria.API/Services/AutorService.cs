using System.Collections.Generic;
using System.Threading.Tasks;
using Livraria.API.Domain.Models;
using Livraria.API.Domain.Repositories;
using Livraria.API.Domain.Services;

namespace Livraria.API.Services
{
    public class AutorService : IAutorService
    {
        private readonly IAutorRepository _autorRepository;
        public AutorService(IAutorRepository autorRepository)
        {
            _autorRepository = autorRepository;
        }

        public async Task<IEnumerable<Autor>> ListAsync()
        {
            return await _autorRepository.ListAsync();
        }
    }
}
