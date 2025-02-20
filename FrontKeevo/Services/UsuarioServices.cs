using FrontKeevo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public static async Task<PaginacaoResponse<Usuario>> Paginacao(string valor, int skip, int take, bool ordemDesc)
        {
            PaginacaoResponse<Usuario> paginacao = new PaginacaoResponse<Usuario>(new List<Usuario>(), 0, 1, 10);

            try
            {
                var endpoint = Program.Configuration.GetSection("WFKeevo:Endpoint").Value;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(endpoint);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UsuarioSession.Token);
                client.Timeout = new TimeSpan(0, 0, 30);

                HttpResponseMessage response = await client.GetAsync($"Usuario/Paginacao?valor={valor}&skip={skip}&take={take}&ordemDesc={ordemDesc}");

                if (response.IsSuccessStatusCode)
                {
                    paginacao = JsonConvert.DeserializeObject<PaginacaoResponse<Usuario>>(await response.Content.ReadAsStringAsync());
                } else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    MessageBox.Show(content);
                }
            } catch (Exception e)
            {
                throw new Exception("Erro:" + e.Message);
            }

            return paginacao;
        }

        // Inclusão
        public async static Task<bool> PostUsuario(Usuario usuario)
        {
            bool resultado = false;

            try
            {
                var endpoint = Program.Configuration.GetSection("WFKeevo:Endpoint").Value;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(endpoint);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UsuarioSession.Token);
                client.Timeout = new TimeSpan(0, 0, 30);
                var json = JsonConvert.SerializeObject(usuario);
                var contentUsuario = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("Usuario", contentUsuario);

                if (response.IsSuccessStatusCode)
                {
                    resultado = true;
                    var content = await response.Content.ReadAsStringAsync();
                    MessageBox.Show(content);
                } else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    MessageBox.Show(content);
                }
            } catch (Exception e)
            {
                throw new Exception("Erro:" + e.Message);
            }

            return resultado;
        }

        // Alteração
        public async static Task<bool> PutUsuario(Usuario usuario)
        {
            bool resultado = false;

            try
            {
                var endpoint = Program.Configuration.GetSection("WFKeevo:Endpoint").Value;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(endpoint);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UsuarioSession.Token);
                client.Timeout = new TimeSpan(0, 0, 30);
                var json = JsonConvert.SerializeObject(usuario);
                var contentUsuario = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync("Usuario", contentUsuario);

                if (response.IsSuccessStatusCode)
                {
                    resultado = true;
                    var content = await response.Content.ReadAsStringAsync();
                    MessageBox.Show(content);
                } else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    MessageBox.Show(content);
                }
            } catch (Exception e)
            {
                throw new Exception("Erro:" + e.Message);
            }

            return resultado;
        }

        // Exclusão
        public async static Task<bool> DeleteUsuario(Guid Id)
        {
            bool resultado = false;

            try
            {
                var endpoint = Program.Configuration.GetSection("WFKeevo:Endpoint").Value;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(endpoint);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UsuarioSession.Token);
                client.Timeout = new TimeSpan(0, 0, 30);

                HttpResponseMessage response = await client.DeleteAsync($"Usuario/{Id}");

                if (response.IsSuccessStatusCode)
                {
                    resultado = true;
                    var content = await response.Content.ReadAsStringAsync();
                    MessageBox.Show(content);
                } else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    MessageBox.Show(content);
                }
            } catch (Exception e)
            {
                throw new Exception("Erro:" + e.Message);
            }

            return resultado;
        }
    }
}
