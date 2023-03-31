using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class AutorViewModel
    {
        public int Id { get; set; }
        [MaxLength(250)]
        [Required]
        public string? Nome { get; set; }
        [MaxLength(250)]
        [Required]
        public string? Sobrenome { get; set; }
        [MaxLength(250)]
        [Required]
        public string? Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public ICollection<Livro>? Livros { get; set; }
    }
}

