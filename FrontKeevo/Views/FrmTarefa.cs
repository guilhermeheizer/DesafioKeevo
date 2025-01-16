using FrontKeevo.Models;
using FrontKeevo.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrontKeevo.Views
{
    public partial class FrmTarefa : Form
    {
        private PaginacaoResponse<Tarefa> paginacao = null;

        public FrmTarefa()
        {
            InitializeComponent();
            atualizaDados();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void atualizaDados()
        {
            int skip = int.Parse(textBoxSkip.Text);
            int take = int.Parse(textBoxTake.Text);

            paginacao = await TarefaServices.Paginacao(textBoxPesquisa.Text, skip, take, checkBoxOrdem.Checked);
            dataGridView1.DataSource = paginacao.Dados;
            atualizaCamposDetalhes();
        }

        private void buttonSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonPesquisar_Click(object sender, EventArgs e)
        {
            atualizaDados();
        }

        private void FrmTarefa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonPesquisar_Click(sender, e);
            }
        }

        private void buttonPrimeira_Click(object sender, EventArgs e)
        {
            if (paginacao.Skip > 1)
            {
                textBoxSkip.Text = "1";
                atualizaDados();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (paginacao.Skip > 1)
            {
                paginacao.Skip--;
                textBoxSkip.Text = paginacao.Skip.ToString();
                atualizaDados();
            }
        }

        private void buttonProxima_Click(object sender, EventArgs e)
        {
            decimal paginas = paginacao.TotalLinhas / paginacao.Take;
            int quantidadePaginas = (int)Math.Ceiling(paginas);

            if (paginacao.Skip < quantidadePaginas)
            {
                paginacao.Skip++;
                textBoxSkip.Text = quantidadePaginas.ToString();
                atualizaDados();
            }
        }

        private void buttonUltima_Click(object sender, EventArgs e)
        {
            decimal paginas = paginacao.TotalLinhas / paginacao.Take;
            int quantidadePaginas = (int)Math.Ceiling(paginas);

            if (paginacao.Skip < quantidadePaginas)
            {
                textBoxSkip.Text = quantidadePaginas.ToString();
                atualizaDados();
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            atualizaCamposDetalhes();
        }

        private void atualizaCamposDetalhes()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                Tarefa tarefa = (Tarefa)dataGridView1.SelectedRows[0].DataBoundItem;
                textBoxTarCodigo.Text = tarefa.TarCodigo.ToString();
                textBoxTarNome.Text = tarefa.TarNome;
                textBoxTarDataInicio.Text = tarefa.TarDataInicio.ToString();
                textBoxTarDataFinal.Text = tarefa.TarDataFinal.ToString();
                textBoxTarStatus.Text = tarefa.TarStatus.ToString();
            }
        }

        private void buttonNovo_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
            textBoxTarCodigo.Clear();
            textBoxTarNome.Clear();
            textBoxTarDataInicio.Clear();
            textBoxTarDataFinal.Clear();
            textBoxTarStatus.Clear();
            textBoxTarCodigo.Focus();

        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
            atualizaCamposDetalhes();
        }

        private async void buttonSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxTarDataFinal.Text))
            {
                textBoxTarDataFinal.Text = textBoxTarDataInicio.Text;
            }

            if (int.TryParse(textBoxTarCodigo.Text, out int tarCodigo) &&
               DateTime.TryParse(textBoxTarDataInicio.Text, out DateTime tarDataInicio) &&
               DateTime.TryParse(textBoxTarDataFinal.Text, out DateTime tarDataFinal) &&
               int.TryParse(textBoxTarStatus.Text, out int tarStatus))
            {
                Tarefa tarefa = new Tarefa()
                {
                    TarCodigo = tarCodigo,
                    TarNome = textBoxTarNome.Text,
                    TarDataInicio = tarDataInicio.ToUniversalTime(),
                    TarDataFinal = tarDataFinal.ToUniversalTime(),
                    TarStatus = tarStatus
                };

                var resultado = await TarefaServices.PostTarefa(tarefa);

                if (resultado)
                {
                    tabControl1.SelectedTab = tabPage1;
                    atualizaCamposDetalhes();
                }
            } else
            {
                // Tratamento para quando a conversão falha
                MessageBox.Show("Erro ao converter os valores. Verifique os formatos.");
            }
        }
    }
}
