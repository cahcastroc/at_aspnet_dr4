using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
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

        public ICollection<Autor> ListaTodos()
        {
            return _appDbContext.Autor.Include(l => l.Livros).ToList();
        }

        public Autor BuscaAutor(int id)
        {
            var autor = _appDbContext.Autor.Include(l => l.Livros).First(l => l.Id == id);
            return autor;
        }

        public void AddAutor(Autor autor)
        {
            _appDbContext.Add(autor);
            _appDbContext.SaveChanges();
        }
        public void EditaAutor(int id, Autor autor)
        {
            var autorAtual = _appDbContext.Autor.Include(l => l.Livros).First(l => l.Id == id);

            if (autorAtual == null)
            {
                throw new Exception();
            }
            autorAtual.Nome = autor.Nome;
            autorAtual.Sobrenome = autor.Sobrenome;
            autorAtual.Email = autor.Email;
            autorAtual.DataNascimento = autor.DataNascimento;


            _appDbContext.SaveChanges();
        }


        public void DeletaAutor(int id)
        {
            var autor = _appDbContext.Autor.First(l => l.Id == id);
            if (autor == null)
            {
                throw new Exception();
            }
            _appDbContext.Remove(autor);
            _appDbContext.SaveChanges();
        }      

       
    }
}
