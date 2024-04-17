using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProyectoUniversidad.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProyectoUniversidad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
        {
            // Crea una instancia de HttpClient
            var httpClient = _httpClientFactory.CreateClient();

            // Serializa el objeto LoginRequest a JSON utilizando Newtonsoft.Json
            var jsonRequest = JsonConvert.SerializeObject(request);

            // Realiza una solicitud a la otra API para verificar las credenciales
            var response = await httpClient.PostAsync("URL_de_la_otra_API",
                new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                // Si la solicitud fue exitosa, lee el contenido de la respuesta
                var responseContent = await response.Content.ReadAsStringAsync();

                // Deserializa la respuesta JSON utilizando Newtonsoft.Json
                var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseContent);

                return loginResponse;
            }
            else
            {
                // Si la solicitud falla, devuelve un error 500
                return StatusCode(500, "Error al procesar la solicitud");
            }
        }

    }
}
