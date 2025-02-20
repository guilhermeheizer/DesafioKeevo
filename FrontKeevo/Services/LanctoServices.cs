using FrontKeevo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static FrontKeevo.Models.ConsisteLancto;

namespace FrontKeevo.Services
{
    public class LanctoServices
    {
        public static async Task<PaginacaoResponse<ConsultaLanctoResponse>> Paginacao(string valor, int skip, int take, bool ordemDesc)
        {
            PaginacaoResponse<ConsultaLanctoResponse> paginacao = new PaginacaoResponse<ConsultaLanctoResponse>(new List<ConsultaLanctoResponse>(), 0, 1, 10);

            try
            {
                var endpoint = Program.Configuration.GetSection("WFKeevo:Endpoint").Value;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(endpoint);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UsuarioSession.Token);
                client.Timeout = new TimeSpan(0, 0, 90);

                HttpResponseMessage response = await client.GetAsync($"MntLancto/ConsultaLancto?valor={valor}&skip={skip}&take={take}&ordemDesc={ordemDesc}");

                if (response.IsSuccessStatusCode)
                {
                    paginacao = JsonConvert.DeserializeObject<PaginacaoResponse<ConsultaLanctoResponse>>(await response.Content.ReadAsStringAsync());
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
        public async static Task<bool> PostLancto(Lancto lancto)
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
                var json = JsonConvert.SerializeObject(lancto);
                var contentLancto = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("Lancto", contentLancto);

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
        public async static Task<bool> PutLancto(Lancto lancto)
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
                var json = JsonConvert.SerializeObject(lancto);
                var contentLancto = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync("Lancto", contentLancto);

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
        public async static Task<bool> DeleteLancto(Guid Id)
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

                HttpResponseMessage response = await client.DeleteAsync($"Lancto/{Id}");

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
        // Consistencia
        public async static Task<bool> PostConsistenciaLancto(ConsisteLanctoResquest consisteLanctoResquest)
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
                var json = JsonConvert.SerializeObject(consisteLanctoResquest);
                var contentLancto = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("MntLancto/ConsisteLancto", contentLancto);

                if (response.IsSuccessStatusCode)
                {
                    resultado = true;
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
