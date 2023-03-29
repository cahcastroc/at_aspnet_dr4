using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAutorService
    {
        ICollection<Autor> ListaTodos();
        Autor BuscaAutor(int id);
        void AddAutor(Autor autor);
        void EditaAutor(Autor autor);
        void DeletaAutor(int id);
      

    }
}
