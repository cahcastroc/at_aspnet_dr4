using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly AppDbContext _context;

        public FuncionarioService(AppDbContext context)
        {
            _context = context;
        }
        public void AddFuncionario(string Email, string Senha, string Name, string Role)
        {
            var funcionario = new Funcionario
            {
                Email = Email.ToLower(),
                Senha = Senha,
                Nome = Name,
                Role = Role.ToUpper()            
            };
            _context.Funcionario?.Add(funcionario);
            _context.SaveChanges();
        }

        public Funcionario? BuscaFuncionario(string Email)
        {                         
            
           return _context.Funcionario?.FirstOrDefault(u => u.Email == Email.ToLower());
        }
    }
}
