using ManagementByObjectives.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ManagementByObjectives.Controllers
{
    public class UsuarioController : Controller
    {
        HttpClient client;

        public UsuarioController(IHttpClientFactory factory)
        {
            client = factory.CreateClient();
        }
        public async Task<IActionResult> Listar()
        {
            client.BaseAddress = new Uri("http://localhost:5203");
            client.DefaultRequestHeaders.Accept.Add(new
                System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                HttpResponseMessage response = client.GetAsync("api/usuario").Result;
                if (response.IsSuccessStatusCode)
                {
                    var listaUsuarios = await response.Content.ReadAsAsync<Usuario[]>();
                    return View(listaUsuarios.ToList());
                }
                else
                {
                    throw new Exception("Ocorreu um erro na listagem!");
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro na listagem!");
            }
        }

        [HttpGet]
        public IActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Incluir(Usuario usuario)
        {
            try
            {
                usuario.Status = 1;
                usuario.DataCriacao = DateTime.Today;

                client.BaseAddress = new Uri("http://localhost:5203");
                client.DefaultRequestHeaders.Accept.Add(new
                    MediaTypeWithQualityHeaderValue("application/json"));

                // gerar o json a partir do objeto fornecido como parâmetro

                string json = JsonConvert.SerializeObject(usuario);

                // gerar o fluxo de bytes para a API

                HttpContent content = new StringContent(json, Encoding.Unicode, "application/json");

                // enviar o objetivo para a API

                var response = await client.PostAsync("api/usuario", content);


                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Listar");
                }
                else
                {
                    string erro = $"{response.StatusCode} - {response.ReasonPhrase}";
                    throw new Exception(erro);
                }

            }
            catch (Exception ex)
            {
                return View("_Erro", ex);
            }

        }

        [HttpGet]
        public Task<IActionResult> Editar(int id)
        {
            return ProcessarUsuario(id, "Editar");

        }



        private async Task<IActionResult> ProcessarUsuario(int id, string viewName)
        {
            client.BaseAddress = new Uri("http://localhost:5203");
            client.DefaultRequestHeaders.Accept.Add(new
                System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                HttpResponseMessage response = client.GetAsync("api/usuario/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var usuario = await response.Content.ReadAsAsync<Usuario>();
                    //var autenticado = await response.Content.ReadAsAsync<bool[]>();                    
                    return View(viewName, usuario);
                }
                else
                {
                    throw new Exception("Ocorreu um erro na autenticação!");
                }

            }
            catch (Exception ex)
            {
                return View("_Erro", ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Usuario usuario)
        {
            try
            {
                usuario.DataCriacao = DateTime.Today;

                client.BaseAddress = new Uri("http://localhost:5203");
                client.DefaultRequestHeaders.Accept.Add(new
                    MediaTypeWithQualityHeaderValue("application/json"));

                // gerar o json a partir do objeto fornecido como parâmetro

                string json = JsonConvert.SerializeObject(usuario);

                // gerar o fluxo de bytes para a API

                HttpContent content = new StringContent(json, Encoding.Unicode, "application/json");

                // enviar o objetivo para a API

                var response = await client.PutAsync("api/usuario", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Listar");
                }
                else
                {
                    string erro = $"{response.StatusCode} - {response.ReasonPhrase}";
                    throw new Exception(erro);
                }

            }
            catch (Exception ex)
            {
                return View("_Erro", ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Excluir(Usuario usuario)
        {
            try
            {
                usuario.Status = 1;
                usuario.DataCriacao = DateTime.Today;

                client.BaseAddress = new Uri("http://localhost:5203");
                client.DefaultRequestHeaders.Accept.Add(new
                    MediaTypeWithQualityHeaderValue("application/json"));

                // gerar o json a partir do objeto fornecido como parâmetro

                string json = JsonConvert.SerializeObject(usuario);

                // gerar o fluxo de bytes para a API

                HttpContent content = new StringContent(json, Encoding.Unicode, "application/json");

                // enviar o objetivo para a API

                var response = await client.DeleteAsync("api/usuario");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Listar");
                }
                else
                {
                    string erro = $"{response.StatusCode} - {response.ReasonPhrase}";
                    throw new Exception(erro);
                }

            }
            catch (Exception ex)
            {
                return View("_Erro", ex);
            }

        }
    }
}
