using Domain.Models;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mapper
{
    public static class AutorMapper
    {
        public static AutorViewModel ConverteParaAutorViewModel(Autor autor) 
        {
            return new AutorViewModel()
            {
                Nome = autor.Nome,
                Sobrenome = autor.Sobrenome,
                Email = autor.Sobrenome,
                DataNascimento = autor.DataNascimento,
                Livros = autor.Livros
            };           

        }
        public static Autor ConverteParaAutorModel(AutorViewModel autorViewModel)
        {
            return new Autor()
            {
                Nome = autorViewModel.Nome,
                Sobrenome = autorViewModel.Sobrenome,
                Email = autorViewModel.Sobrenome,
                DataNascimento = autorViewModel.DataNascimento,
                Livros = autorViewModel.Livros
            };
        }




        public static ICollection<AutorViewModel> ConverteListaAutoresParaViewModel(ICollection<Autor> autores)
        {
            return autores.Select(autor => new AutorViewModel
            {
                Nome = autor.Nome,
                Sobrenome = autor.Sobrenome,
                Email = autor.Sobrenome,
                DataNascimento = autor.DataNascimento,
                Livros = autor.Livros

            }).ToList();

        }
    }
}
