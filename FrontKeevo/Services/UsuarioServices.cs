using FrontKeevo.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FrontKeevo.Services
{
    public class UsuarioServices
    {
        public async static Task<UsuarioResponse> Login(UsuarioLogin login)
        {
            // Class responsavel para executar todas as logicas de Usuario

            UsuarioResponse usuarioResponse = null;

            var endpoint = Program.Configuration.GetSection("WFKeevo:Endpoint").Value;
        
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(endpoint);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            client.Timeout = new TimeSpan(0, 0, 30);

            HttpResponseMessage response = await client.PostAsJsonAsync("Usuario/Login", login);

            if (response.IsSuccessStatusCode)
            {
                usuarioResponse = 
                    JsonConvert.DeserializeObject<UsuarioResponse>(await response.Content.ReadAsStringAsync());
            }

            return usuarioResponse;
        }
    }
}
