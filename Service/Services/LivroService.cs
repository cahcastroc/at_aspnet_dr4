using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class LivroService : ILivroService
    {
        public void AddAutor(int idLivro, int idAutor)
        {
            throw new NotImplementedException();
        }

        public void AddLivro(Livro livro)
        {
            throw new NotImplementedException();
        }

        public Livro BuscaLivro(int id)
        {
            throw new NotImplementedException();
        }

        public void DeletaLivro(int id)
        {
            throw new NotImplementedException();
        }

        public void EditaLivro(Livro livro)
        {
            throw new NotImplementedException();
        }

        public ICollection<Livro> ListaTodos()
        {
            throw new NotImplementedException();
        }
    }
}
