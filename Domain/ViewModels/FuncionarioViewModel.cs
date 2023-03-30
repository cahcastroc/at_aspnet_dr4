using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class FuncionarioViewModel
    {
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo email é obrigatório.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo senha é obrigatório.")]
        public string Senha { get; set; }
        public string Role { get; set; }

        public FuncionarioViewModel()
        {
            Nome = "";
            Email = "";
            Senha = "";
            Role = "";
        }

    }
}
