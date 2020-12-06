using System;

namespace Livraria.API.Domain.Models
{
    public class Livro
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        //public string Autor { get; set; }
        public short QuantidadeEstoque { get; set; }


        public Guid AutorId { get; set; }
        //public Autor Autor { get; set; }
    }
}
