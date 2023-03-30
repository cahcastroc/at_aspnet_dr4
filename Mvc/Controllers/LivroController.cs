﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net;
using Domain.ViewModels;
using System.Text;
using Domain.Models;

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
                else
                {
                    ViewBag.Erro = "Erro ao deletar livro";
                    return View();
                }
            }
        }

            }
}