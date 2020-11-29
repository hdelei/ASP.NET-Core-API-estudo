using System.Collections.Generic;

namespace Livraria.API.Domain.Models
{
    public class Autor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        //public IList<Livro> Livros { get; set; } = new List<Livro>();
    }
}
