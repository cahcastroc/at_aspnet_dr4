using Domain.Interfaces;
using Domain.Mapper;
using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace Api.Controllers
{
    [Route("/autores")]
    [ApiController]
    public class AutorController : ControllerBase
    {

        private readonly IAutorService _autorService;

        public AutorController(IAutorService autorService)
        {
            _autorService = autorService;
        }


        [HttpGet]
        public IActionResult Lista()
        {
            var autores = _autorService.ListaTodos();
            return Ok(AutorMapper.ConverteListaAutoresParaViewModel(autores));
        }


        [HttpGet("{id}")]
        public IActionResult BuscaAutorPorId(int id)
        {
            try
            {
                var autor = _autorService.BuscaAutor(id);
                return Ok(AutorMapper.ConverteParaAutorViewModel(autor));
            }
            catch (InvalidOperationException)
            {
                return NotFound(new { Message = "Autor não encontrado." });
            }
        }


        [HttpPost]
        public IActionResult AddAutor([FromBody] AutorViewModel autorViewModel)
        {

            if (autorViewModel == null)
            {
                return BadRequest(new { Message = "Falha ao adicionar autor." });
            }

            var autorModel = AutorMapper.ConverteParaAutorModel(autorViewModel);
            _autorService.AddAutor(autorModel);

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult EditaAutor(int id, [FromBody] AutorViewModel autorViewModel)
        {
            try
            {
                var autor = AutorMapper.ConverteParaAutorModel(autorViewModel);
                _autorService.EditaAutor(id, autor);

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Falha ao Editar livro." });
            }
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _autorService.DeletaAutor(id);

                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Falha ao Excluir Autor. Verifique o nº de identificação" });
            }
        }
    }
}
