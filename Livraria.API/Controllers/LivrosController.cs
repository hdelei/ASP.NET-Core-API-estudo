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

        [HttpGet("id/{id}")]
        public async Task<Livro> GetLivro(Guid id)
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

                if (result.Titulo == string.Empty)
                {
                    return BadRequest(
                        new
                        {
                            Error = new { Description = "O Título não pode ser vazio" }
                        }
                    );
                }

                if (result.AutorId == Guid.Empty)
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _livroService.DeleteLivro(id));
            }
            catch (ArgumentException e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put(Livro livro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _livroService.UpdateLivro(livro));
            }
            catch (ArgumentException e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
