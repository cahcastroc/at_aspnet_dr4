﻿using Domain.Interfaces;
using Domain.Mapper;
using Domain.Models;
using Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
  
    [Route("/livros")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly ILivroService _livroService;

        public LivroController(ILivroService livroService)
        {
            _livroService = livroService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Lista()
        {
            var livrosModel = _livroService.ListaTodos();

            return Ok(LivroMapper.ConverteListaLivrosParaViewModel(livrosModel));

        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult BuscaLivroPorId(int id)
        {
            try
            {
                var livro = _livroService.BuscaLivro(id);
                return Ok(LivroMapper.ConverteParaLivroViewModel(livro));
            }
            catch (InvalidOperationException)
            {
                return NotFound(new { Message = "Livro não encontrado." });
            }

        }

        [Authorize]
        [HttpPost]
        public IActionResult AddLivro([FromBody] LivroViewModel livroViewModel)
        {
            if (livroViewModel == null)
            {
                return BadRequest(new { Message = "Falha ao adicionar livro." });
            }

            var livroModel = LivroMapper.ConverteParaLivroModel(livroViewModel);

            _livroService.AddLivro(livroModel);
            return Ok();

        }

        [Authorize]
        [Route("/livros/autores/{livroId}")]
        [HttpPost]
        public IActionResult AddAutores([FromBody]string autorId, int livroId)
        {                       

            if (!int.TryParse(autorId, out int autorIdInt))
            {
                return BadRequest("Id do autor ou do livro inválido.");
            }

            try
            {
                _livroService.AddAutorLivro(autorIdInt, livroId);
                return Ok();
            }
            catch (DbUpdateException)
            {
                return UnprocessableEntity(new { Message = "Autor já cadastrado no livro informado" });
            }

        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult EditaLivro(int id, [FromBody] LivroViewModel livroViewModel)
        {
            try
            {
                var livro = LivroMapper.ConverteParaLivroModel(livroViewModel);
                _livroService.EditaLivro(id, livro);

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Falha ao Editar livro." });
            }

        }

        [Authorize(Roles = ("ADM"))]
        [HttpDelete("{id}")]
        public IActionResult DeletaLivro(int id)
        {
            try
            {
                _livroService.DeletaLivro(id);

                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Falha ao Excluir livro. Verifique o nº de identificação" });
            }
        }
    }
}
