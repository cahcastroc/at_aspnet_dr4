using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class LivroViewModel
    {
        public int Id { get; set; }
        [MaxLength(250)]
        [Required(ErrorMessage = "O preenchimento do título do livro é obrigatório.")]
        public string? Titulo { get; set; }
        [MaxLength(250)]
        [Required(ErrorMessage = "O preenchimento do ISBN do livro é obrigatório.")]
        public string? ISBN { get; set; }
        
        [Required(ErrorMessage = "O preenchimento do ano do livro é obrigatório.")]
        public DateTime Ano { get; set; }
        public ICollection<Autor>? Autores { get; set; }
    }
}
