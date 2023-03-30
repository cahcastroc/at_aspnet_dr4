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
    public class LivroService : ILivroService
    {
        private readonly AppDbContext _appDbContext;

        public LivroService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
              


        public void AddAutorLivro(int idAutor, int idLivro)
        {
            var livroDb = _appDbContext.Livro.First(p => p.Id == idLivro);
            var autorDb = _appDbContext.Autor.First(c => c.Id == idAutor);

            if(livroDb.Autores.Contains(autorDb))
            {
                throw new DbUpdateException();
            }

            livroDb.Autores.Add(autorDb);
            _appDbContext.SaveChanges();
        }

        public void AddLivro(Livro livro)
        {           
                _appDbContext.Add(livro);
                _appDbContext.SaveChanges();     
       
        }

        public Livro BuscaLivro(int id)
        {
            var livro = _appDbContext.Livro.Include(l => l.Autores).First(l => l.Id == id);
          
            return livro;           
           
        }

        public void DeletaLivro(int id)
        {
           var livro = _appDbContext.Livro.First(l => l.Id == id);
           if (livro == null) 
            {
                throw new Exception();
            }
            _appDbContext.Remove(livro);
            _appDbContext.SaveChanges();
        }

        public void EditaLivro(int id, Livro livro)
        {
            var livroAtual = _appDbContext.Livro.First(l => l.Id == id);

            if (livroAtual == null) 
            {
                throw new Exception();
            }
            livroAtual.Titulo = livro.Titulo;
            livroAtual.Ano = livro.Ano;
            livroAtual.ISBN = livro.ISBN;

            _appDbContext.SaveChanges();
        }
      

        public ICollection<Livro> ListaTodos()
        {
          return _appDbContext.Livro.Include(l => l.Autores).ToList() ;
        }
    }
}
