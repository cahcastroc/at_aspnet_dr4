using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net;
using Domain.ViewModels;
using System.Text;
using Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System;

namespace Mvc.Controllers
{
    public class LivroController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var jwtToken = HttpContext.Session.GetString("JwtToken");

            using var httpClient = new HttpClient();

            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                using var response = await httpClient.GetAsync("https://localhost:5001/livros");
                response.EnsureSuccessStatusCode();
                var apiResponse = await response.Content.ReadAsStringAsync();

                var livros = JsonConvert.DeserializeObject<List<LivroViewModel>>(apiResponse);

                return View(livros);
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
                    ViewBag.Erro = "Erro ao buscar lista de livros";
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
        public async Task<IActionResult> Add(LivroViewModel livroViewModel)
        {
            var jwtToken = HttpContext.Session.GetString("JwtToken");


            using var httpClient = new HttpClient();

            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                StringContent content = new StringContent(JsonConvert.SerializeObject(livroViewModel), Encoding.UTF8, "application/json");
                using var response = await httpClient.PostAsync("https://localhost:5001/livros", content);
                response.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Livro");
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
                    ViewBag.Erro = "Erro ao adicionar livro";
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
                using var response = await httpClient.GetAsync($"https://localhost:5001/livros/{id}");
                response.EnsureSuccessStatusCode();
                var apiResponse = await response.Content.ReadAsStringAsync();

                var livro = JsonConvert.DeserializeObject<LivroViewModel>(apiResponse);

                return View(livro);
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
                    ViewBag.Erro = "Erro ao buscar livro. Verifique o nº de identificação";
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
        public async Task<IActionResult> Editar(int id, LivroViewModel livroViewModel)
        {

            var jwtToken = HttpContext.Session.GetString("JwtToken");


            using var httpClient = new HttpClient();

            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                StringContent content = new StringContent(JsonConvert.SerializeObject(livroViewModel), Encoding.UTF8, "application/json");
                using var response = await httpClient.PutAsync($"https://localhost:5001/livros/{id}", content);
                response.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Livro");
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
                    ViewBag.Erro = "Erro ao editar o livro.";
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

                using var response = await httpClient.DeleteAsync($"https://localhost:5001/livros/{id}");
                response.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Livro");
            }
            catch (HttpRequestException ex)
            {
                var statusCode = ex.StatusCode;

                if (statusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("NaoAutorizado", "Home");
                }
                if (statusCode == HttpStatusCode.Forbidden)
                {
                    return RedirectToAction("Forbidden", "Home");
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        public async Task<IActionResult> SelecionarAutorLivro(int id)
        {

            var jwtToken = HttpContext.Session.GetString("JwtToken");

            using var httpClient = new HttpClient();

            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                using var response = await httpClient.GetAsync("https://localhost:5001/autores");
                response.EnsureSuccessStatusCode();
                var apiResponse = await response.Content.ReadAsStringAsync();

                var autores = JsonConvert.DeserializeObject<List<AutorViewModel>>(apiResponse);


                ViewBag.Autores = new SelectList(autores, "Id", "Nome");

                ViewBag.LivroId = id;

                return View();
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
                    ViewBag.Erro = "Erro ao exibir lista de autores";
                    return View();
                }
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VincularAutor(string autorId, int livroId)
        {


            var jwtToken = HttpContext.Session.GetString("JwtToken");


            using var httpClient = new HttpClient();

            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                StringContent content = new StringContent($"\"{autorId}\"", Encoding.UTF8, "application/json");

                using var response = await httpClient.PostAsync($"https://localhost:5001/livros/autores/{livroId}", content);
                response.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Livro");
            }
            catch (HttpRequestException ex)
            {
                var statusCode = ex.StatusCode;

                if (statusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("NaoAutorizado", "Home");
                }

                if (statusCode == HttpStatusCode.UnprocessableEntity) 
                {
                    ViewBag.Erro = "Autor já cadastrado no livro informado";
                    return View();
                }                
                else
                {
                    ViewBag.Erro = "Erro ao vincular autor";
                    return View();
                }
            }
        }
    }
}
