using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Livraria.API.Domain.Models;
using Livraria.API.Domain.Services;
//using Livraria.API.Errors;
using System;
using System.Net;
using System.Linq;

namespace Livraria.API.Controllers
{
    [Route("/api/[controller]")]
    public class LivrosController : Controller
    {
        private readonly ILivroService _livroService;

        public LivrosController(ILivroService livroService)
        {
            _livroService = livroService;
        }

        [HttpGet]
        public async Task<IEnumerable<Livro>> GetAllAsync()
        {
            var livro = await _livroService.ListAsync();
            return livro;
        }

        [HttpGet("{id}")]
        public async Task<Livro> GetLivro(int id)
        {
            var livro = await _livroService.FindByIdAsync(id);
            return livro.Count() > 0 ? livro.First() : new Livro();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Livro livro)
        {
            try
            {
                var result = await _livroService.PostLivro(livro);
                if (result == null)
                {
                    return BadRequest(
                        new
                        {
                            Error = new { Description = "Undefined error" }
                        }
                    );
                }

                if (result.AutorId == -1)
                {
                    return BadRequest(
                        new
                        {
                            Error = new { Description = "Autor ID does not exist" }
                        }
                    );
                }

                return CreatedAtAction(nameof(GetAllAsync), new { id = livro.Id }, livro);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
