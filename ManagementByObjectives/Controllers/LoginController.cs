using ManagementByObjectives.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace ManagementByObjectives.Controllers
{
    public class LoginController : Controller
    {
        HttpClient client;

        public LoginController(IHttpClientFactory factory)
        {
            client = factory.CreateClient();
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Usuario usuario)
        {
            client.BaseAddress = new Uri("http://localhost:5203"); 
            client.DefaultRequestHeaders.Accept.Add(new
                System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")); 
            try 
            {
                HttpResponseMessage response = client.GetAsync("api/autenticacao/"+usuario.Email +"/" +usuario.Senha).Result;

                if (response.IsSuccessStatusCode)
                {
                    var autenticado = await response.Content.ReadAsStringAsync();
                    //var autenticado = await response.Content.ReadAsAsync<bool[]>();                    
                    return RedirectToAction("Index", "Objetivo");
                }
                else
                {
                    throw new Exception("Ocorreu um erro na autenticação!");
                }

            }
            catch(Exception ex)
            {
                return null; 
            }

        }

    }


}
