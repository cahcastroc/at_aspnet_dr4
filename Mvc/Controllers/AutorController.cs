using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net;
using System.Text;

namespace Mvc.Controllers
{
    public class AutorController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var jwtToken = HttpContext.Session.GetString("JwtToken");

            using var httpClient = new HttpClient();

            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                using var response = await httpClient.GetAsync("https://localhost:5001/autores");
                response.EnsureSuccessStatusCode();
                var apiResponse = await response.Content.ReadAsStringAsync();

                var autores= JsonConvert.DeserializeObject<List<AutorViewModel>>(apiResponse);

                return View(autores);
            }
            catch (HttpRequestException ex)
            {
                var statusCode = ex.StatusCode;

                if (statusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("NaoAutorizado", "Home");
                }
                else
                {
                    ViewBag.Erro = "Erro ao buscar lista de autores";
                    return View();
                }
            }
        }
        public IActionResult Add()
        {
            var jwtToken = HttpContext.Session.GetString("JwtToken");
            if (jwtToken == null)
            {
                return RedirectToAction("NaoAutorizado", "Home");
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AutorViewModel autorViewModel)
        {
            var jwtToken = HttpContext.Session.GetString("JwtToken");


            using var httpClient = new HttpClient();

            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                StringContent content = new StringContent(JsonConvert.SerializeObject(autorViewModel), Encoding.UTF8, "application/json");
                using var response = await httpClient.PostAsync("https://localhost:5001/autores", content);
                response.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Autor");
            }
            catch (HttpRequestException ex)
            {
                var statusCode = ex.StatusCode;

                if (statusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("NaoAutorizado", "Home");
                }
                else
                {
                    ViewBag.Erro = "Erro ao adicionar o autor";
                    return View();
                }
            }

        }

        [HttpGet]
        public async Task<IActionResult> Detalhes(int id)
        {
            var jwtToken = HttpContext.Session.GetString("JwtToken");

            using var httpClient = new HttpClient();

            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                using var response = await httpClient.GetAsync($"https://localhost:5001/autores/{id}");
                response.EnsureSuccessStatusCode();
                var apiResponse = await response.Content.ReadAsStringAsync();

                var autor = JsonConvert.DeserializeObject<AutorViewModel>(apiResponse);

                return View(autor);
            }
            catch (HttpRequestException ex)
            {
                var statusCode = ex.StatusCode;

                if (statusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("NaoAutorizado", "Home");
                }
                if (statusCode == HttpStatusCode.NotFound)
                {
                    ViewBag.Erro = "Erro ao buscar autor. Verifique o nº de identificação";
                    return View();
                }
                else
                {
                    ViewBag.Erro = "Erro ao buscar livro";
                    return View();
                }
            }
        }

        public IActionResult Editar(int id)
        {
            if (Request.Method == "POST")
            {
                return RedirectToAction("Editar", new { id });
            }
            return View();
        }


        //Put na API
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, AutorViewModel autorViewModel)
        {

            var jwtToken = HttpContext.Session.GetString("JwtToken");


            using var httpClient = new HttpClient();

            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                StringContent content = new StringContent(JsonConvert.SerializeObject(autorViewModel), Encoding.UTF8, "application/json");
                using var response = await httpClient.PutAsync($"https://localhost:5001/autores/{id}", content);
                response.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Autor");
            }
            catch (HttpRequestException ex)
            {
                var statusCode = ex.StatusCode;

                if (statusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("NaoAutorizado", "Home");
                }
                else
                {
                    ViewBag.Erro = "Erro ao editar o autor.";
                    return View();
                }
            }
        }


        public async Task<IActionResult> Deletar(int id)
        {
            var jwtToken = HttpContext.Session.GetString("JwtToken");

            using var httpClient = new HttpClient();

            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

                using var response = await httpClient.DeleteAsync($"https://localhost:5001/autores/{id}");
                response.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Autor");
            }
            catch (HttpRequestException ex)
            {
                var statusCode = ex.StatusCode;

                if (statusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("NaoAutorizado", "Home");
                }
                else
                {
                    return BadRequest();
                }
            }
        }


    }


}
