using Domain.Models;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mapper
{
    public static class LivroMapper
    {
        public static LivroViewModel ConverteParaLivroViewModel(Livro livro) 
        {
            return new LivroViewModel()
            {
                Titulo = livro.Titulo,
                ISBN = livro.ISBN,
                Ano = livro.Ano,
                Autores = livro.Autores
            };            

        }

        public static Livro ConverteParaLivroModel(LivroViewModel livroViewModel)
        {
            return new Livro()
            {
                Titulo = livroViewModel.Titulo,
                ISBN = livroViewModel.ISBN,
                Ano = livroViewModel.Ano,
                Autores = livroViewModel.Autores
            };

        }

        public static ICollection<LivroViewModel> ConverteListaLivrosParaViewModel(ICollection<Livro> livros)
        {
            return livros.Select(livro => new LivroViewModel
            {
                Titulo = livro.Titulo,
                Ano = livro.Ano,
                ISBN = livro.ISBN,
                Autores = livro.Autores

            }).ToList();

        }
    }
}
