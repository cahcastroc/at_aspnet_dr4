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

            using (var httpClient = new HttpClient()) 
            {
                using (var response = await httpClient.PostAsJsonAsync("https://localhost:5001/registro", funcionarioViewModel)) {
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Login", "Funcionario");
                    }
                    if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
                    {
                        ViewBag.Erro = "Funcionário já cadastrado";
                        return View(funcionarioViewModel);
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        ModelState.AddModelError("", error);
                        ViewBag.Erro = "Erro ao realizar o cadastro. Verifique os dados inseridos e tente novamente";
                        return View(funcionarioViewModel);
                    }
                }
            }         
         
            
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(FuncionarioViewModel funcionarioViewModel)
        {

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsJsonAsync("https://localhost:5001/login", funcionarioViewModel))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var token = await response.Content.ReadAsStringAsync();
                        HttpContext.Session.SetString("JwtToken", token);

                        return RedirectToAction("Index", "Home");
                    }
                    if (response.StatusCode == HttpStatusCode.BadRequest || response.StatusCode == HttpStatusCode.NotFound)
                    {
                        ViewBag.Erro = "Usuário ou senha inválidos";
                        return View(funcionarioViewModel);
                    }
                    else
                    {
                        string errorMessage = await response.Content.ReadAsStringAsync();
                        ModelState.AddModelError("", errorMessage);
                        ViewBag.Erro = "Erro ao realizar o login. Verifique os dados inseridos e tente novamente";
                        return View(funcionarioViewModel);
                    }
                }
            }


        }
    }
}
