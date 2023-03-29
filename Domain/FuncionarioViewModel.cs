using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class FuncionarioViewModel
    {
        public string Nome { get; set; }
        public string Email { get; set; }
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
