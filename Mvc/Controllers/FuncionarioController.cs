using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace Mvc.Controllers
{
    public class FuncionarioController : Controller
    {
        [HttpGet]
        public ViewResult Login() => View();


        [HttpGet]
        public ViewResult Registro() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(FuncionarioViewModel funcionarioViewModel)
        {

            using var httpClient = new HttpClient();

            try {
                using var response = await httpClient.PostAsJsonAsync("https://localhost:5001/registro", funcionarioViewModel);
                response.EnsureSuccessStatusCode();
                return RedirectToAction("Login", "Funcionario");
            }
            catch (HttpRequestException ex)
            {
                var statusCode = ex.StatusCode;

                if (statusCode == HttpStatusCode.UnprocessableEntity)
                {
                    ViewBag.Erro = "Funcionário já cadastrado";
                    return View(funcionarioViewModel);
                }
                else 
                {
                    ViewBag.Erro = "Erro ao realizar o cadastro. Verifique os dados inseridos e tente novamente";
                    return View(funcionarioViewModel);
                }                
            }          

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(FuncionarioViewModel funcionarioViewModel)
        {

            using var httpClient = new HttpClient();
            try
            {
                using var response = await httpClient.PostAsJsonAsync("https://localhost:5001/login", funcionarioViewModel);

                response.EnsureSuccessStatusCode();

                var token = await response.Content.ReadAsStringAsync();
                HttpContext.Session.SetString("JwtToken", token);

                return RedirectToAction("Index", "Livro");


            }
            catch (HttpRequestException ex)
            {

                var statusCode = ex.StatusCode;

                if (statusCode == HttpStatusCode.NotFound || statusCode == HttpStatusCode.BadRequest)
                {
                    ViewBag.Erro = "Usuário ou senha inválidos";
                    return View(funcionarioViewModel);
                }
                return View();
            }


        }
    }
}
