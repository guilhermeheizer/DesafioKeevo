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
    public partial class FrmHelpTarefa : Form
    {
        private PaginacaoResponse<Tarefa> paginacao = null;
        public FrmHelpTarefa()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            AtualizaDados();
        }

        private async void AtualizaDados()
        {
            int skip = int.Parse(textBoxSkip.Text);
            int take = int.Parse(textBoxTake.Text);

            paginacao = await TarefaServices.Paginacao(textBoxPesquisa.Text, skip, take, checkBoxOrdem.Checked);
            dataGridView1.DataSource = paginacao.Dados;
            AtualizaCamposDetalhes();
        }

        private void AtualizaCamposDetalhes()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                Tarefa tarefa = (Tarefa)dataGridView1.SelectedRows[0].DataBoundItem;
                textBoxTarCodigo.Text = tarefa.TarCodigo.ToString();
                textBoxTarNome.Text = tarefa.TarNome;
                dateTimePickerDataInicial.Text = tarefa.TarDataInicio.ToString();
                dateTimePickerDataFinal.Text = tarefa.TarDataFinal.ToString();
                comboBoxTarStatus.Text = tarefa.TarStatus.ToString();
                int index = comboBoxTarStatus.FindString(tarefa.TarStatus.ToString());
                comboBoxTarStatus.SelectedIndex = index;
            }
        }

        private void buttonSair_Click(object sender, EventArgs e)
        {
            // TODO : Limpar a área de comunicação
            Close();
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
            AtualizaCamposDetalhes();
        }

        private void buttonPrimeira_Click(object sender, EventArgs e)
        {
            if (paginacao.Skip > 1)
            {
                textBoxSkip.Text = "1";
                AtualizaDados();
            }
        }

        private void buttonAnterior_Click(object sender, EventArgs e)
        {
            if (paginacao.Skip > 1)
            {
                paginacao.Skip--;
                textBoxSkip.Text = paginacao.Skip.ToString();
                AtualizaDados();
            }
        }

        private void buttonProxima_Click(object sender, EventArgs e)
        {
            decimal paginas = (decimal)paginacao.TotalLinhas / paginacao.Take;
            int quantidadePaginas = (int)Math.Ceiling(paginas);

            if (paginacao.Skip < quantidadePaginas)
            {
                paginacao.Skip++;
                textBoxSkip.Text = paginacao.Skip.ToString();
                AtualizaDados();
            }
        }

        private void buttonUltima_Click(object sender, EventArgs e)
        {
            decimal paginas = (decimal)paginacao.TotalLinhas / paginacao.Take;
            int quantidadePaginas = (int)Math.Ceiling(paginas);

            if (paginacao.Skip < quantidadePaginas)
            {
                textBoxSkip.Text = quantidadePaginas.ToString();
                AtualizaDados();
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            AtualizaCamposDetalhes();
        }

        private void buttonSalvar_Click(object sender, EventArgs e)
        {
            SelecionaTarefa();
        }

        private void buttonPesquisar_Click(object sender, EventArgs e)
        {
            AtualizaDados();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
            SelecionaTarefa();
        }

        private void SelecionaTarefa()
        {
            TarefaSession.TarCodigo = Convert.ToInt32(textBoxTarCodigo.Text); ;
            TarefaSession.TarNome = textBoxTarNome.Text;
            TarefaSession.TarDataInicio = dateTimePickerDataInicial.Value;
            TarefaSession.TarDataFinal = dateTimePickerDataFinal.Value;
            string statusSelecionado = comboBoxTarStatus.SelectedItem.ToString();
            string[] partes = statusSelecionado.Split('-');
            int statusNumerico = int.Parse(partes[0]);
            TarefaSession.TarStatus = statusNumerico;
            Close();
        }
    }
}
