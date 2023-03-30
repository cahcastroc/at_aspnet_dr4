using Domain.Interfaces;
using Domain.Mapper;
using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

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

        //// GET: api/<AutorController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<AutorController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<AutorController>
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

        //PUT api/<AutorController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //DELETE api/<AutorController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
