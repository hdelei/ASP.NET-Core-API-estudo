using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Livraria.API.Domain.Models;
using Livraria.API.Domain.Services;

namespace Livraria.API.Controllers
{
    [Route("/api/[controller]")]
    public class AutoresController : Controller
    {
        private readonly IAutorService _autorService;

        public AutoresController(IAutorService autorService)
        {
            _autorService = autorService;
        }

        [HttpGet]
        public async Task<IEnumerable<Autor>> GetAllAsync()
        {
            var autores = await _autorService.ListAsync();
            return autores;
        }
    }
}
