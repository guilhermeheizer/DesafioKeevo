using FrontKeevo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FrontKeevo.Services
{
    public class TarefaServices
    {
        public static async Task<List<Tarefa>> GetTarefa()
        {
            List<Tarefa> lista = new List<Tarefa>();

            try
            {
                var endpoint = Program.Configuration.GetSection("WFKeevo:Endpoint").Value;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(endpoint);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UsuarioSession.Token);
                client.Timeout = new TimeSpan(0, 0, 30);

                HttpResponseMessage response = await client.GetAsync("Tarefa");

                if (response.IsSuccessStatusCode)
                {
                    lista = JsonConvert.DeserializeObject<List<Tarefa>>(await response.Content.ReadAsStringAsync());
                } else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    MessageBox.Show(content);
                }
            } catch (Exception e) 
            {
                throw new Exception("Erro:" + e.Message);
            }

            return lista;
        }

        public static async Task<List<Tarefa>> Pesquisa(string valor)
        {
            List<Tarefa> lista = new List<Tarefa>();

            try
            {
                var endpoint = Program.Configuration.GetSection("WFKeevo:Endpoint").Value;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(endpoint);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UsuarioSession.Token);
                client.Timeout = new TimeSpan(0, 0, 30);

                HttpResponseMessage response = await client.GetAsync($"Tarefa/Pesquisa?valor={valor}");

                if (response.IsSuccessStatusCode)
                {
                    lista = JsonConvert.DeserializeObject<List<Tarefa>>(await response.Content.ReadAsStringAsync());
                } else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    MessageBox.Show(content);
                }
            } catch (Exception e)
            {
                throw new Exception("Erro:" + e.Message);
            }

            return lista;
        }

        public static async Task<Tarefa> BuscaPorCodigo(int tarCodigo)
        {
            Tarefa tarefa = new Tarefa();

            try
            {
                var endpoint = Program.Configuration.GetSection("WFKeevo:Endpoint").Value;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(endpoint);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UsuarioSession.Token);
                client.Timeout = new TimeSpan(0, 0, 30);

                HttpResponseMessage response = await client.GetAsync($"Tarefa/BuscarPorCodigo/{tarCodigo}");

                if (response.IsSuccessStatusCode)
                {
                    tarefa = JsonConvert.DeserializeObject<Tarefa>(await response.Content.ReadAsStringAsync());
                } else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    MessageBox.Show(content);
                }
            } catch (Exception e)
            {
                throw new Exception("Erro:" + e.Message);
            }

            return tarefa;
        }

        public static async Task<PaginacaoResponse<Tarefa>> Paginacao(string valor, int skip, int take, bool ordemDesc)
        {
            PaginacaoResponse<Tarefa> paginacao = new PaginacaoResponse<Tarefa>(new List<Tarefa>(), 0 , 1, 10);

            try
            {
                var endpoint = Program.Configuration.GetSection("WFKeevo:Endpoint").Value;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(endpoint);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UsuarioSession.Token);
                client.Timeout = new TimeSpan(0, 0, 30);

                HttpResponseMessage response = await client.GetAsync($"Tarefa/Paginacao?valor={valor}&skip={skip}&take={take}&ordemDesc={ordemDesc}");

                if (response.IsSuccessStatusCode)
                {
                    paginacao = JsonConvert.DeserializeObject<PaginacaoResponse<Tarefa>>(await response.Content.ReadAsStringAsync());
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
        public async static Task<bool> PostTarefa(Tarefa tarefa)
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
                var json = JsonConvert.SerializeObject(tarefa);
                var contentTarefa = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("Tarefa", contentTarefa);

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
        public async static Task<bool> PutTarefa(Tarefa tarefa)
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
                var json = JsonConvert.SerializeObject(tarefa);
                var contentTarefa = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync("Tarefa", contentTarefa);

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
        public async static Task<bool> DeleteTarefa(int tarCodigo)
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
                
                HttpResponseMessage response = await client.DeleteAsync($"Tarefa/{tarCodigo}");

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
