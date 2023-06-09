﻿using Domain.Interfaces;
using Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Service.Services;
using System.Security.Cryptography;
using System.Text;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private readonly IFuncionarioService _funcionarioService;
        private readonly TokenService _token;
        private readonly IConfiguration _configuration;

        public FuncionarioController(IFuncionarioService funcionarioService, TokenService token, IConfiguration configuration)
        {
            _funcionarioService = funcionarioService;
            _token = token;
            _configuration = configuration;
        }

        private string MaskPassword(string Senha)
        {
            var hashPassword = SHA512.HashData(Encoding.ASCII.GetBytes(Senha));
            return Encoding.ASCII.GetString(hashPassword);
        }


        [HttpPost]
        [Route("/registro")]
        public IActionResult Post([FromBody] FuncionarioViewModel funcionarioViewModel)
        {
            var funcionario = _funcionarioService.BuscaFuncionario(funcionarioViewModel.Email);
            Console.WriteLine(funcionario);
            if (funcionario != null)
            {               
                return UnprocessableEntity(new { Message = "Funcionário já cadastrado" });
            }


            _funcionarioService.AddFuncionario(funcionarioViewModel.Email, MaskPassword(funcionarioViewModel.Senha), funcionarioViewModel.Nome, funcionarioViewModel.Role);
            return Ok(new { Message = "Registro realizado com sucesso" });
        }


        [HttpPost]
        [Route("/login")]
        public IActionResult Login([FromBody] FuncionarioViewModel funcionarioViewModel)
        {
            var funcionario = _funcionarioService.BuscaFuncionario(funcionarioViewModel.Email);

            if (funcionario == null)
            {
                return NotFound(new { Message = "Funcionário não encontrado" });
            }
            if (funcionario.Senha != MaskPassword(funcionarioViewModel.Senha))
            {
                return BadRequest(new { Message = "Senha Inválida" });
            }            
            return Ok(_token.GerarToken(funcionario, _configuration.GetValue("TokenSecret", "#")));
        }


    }
}
