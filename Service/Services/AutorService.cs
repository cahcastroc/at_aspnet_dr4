using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AutorService : IAutorService
    {
        private readonly AppDbContext _appDbContext;

        public AutorService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void AddAutor(Autor autor)
        {
            _appDbContext.Add(autor);
            _appDbContext.SaveChanges();
        }

        public Autor BuscaAutor(int id)
        {
            throw new NotImplementedException();
        }

        public void DeletaAutor(int id)
        {
            throw new NotImplementedException();
        }

        public void EditaAutor(Autor autor)
        {
            throw new NotImplementedException();
        }

        public ICollection<Autor> ListaTodos()
        {
            throw new NotImplementedException();
        }
    }
}
