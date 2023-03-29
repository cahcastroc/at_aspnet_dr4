using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ILivroService
    {
        ICollection<Livro> ListaTodos();
        Livro BuscaLivro(int id);
        void AddLivro(Livro livro);
        void EditaLivro(Livro livro);
        void DeletaLivro(int id);
        void AddAutor(int idLivro, int idAutor);
    }
}
