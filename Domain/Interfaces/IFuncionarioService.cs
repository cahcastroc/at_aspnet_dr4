using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IFuncionarioService
    {
        Funcionario? BuscaFuncionario(string Email);
        void AddFuncionario(string Email, string Senha, string Name, string Role);
    }
}
