using FrontKeevo.Models;
using FrontKeevo.Services;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FrontKeevo.Views
{
    public partial class FrmTarefa : Form
    {
        private PaginacaoResponse<Tarefa> paginacao = null;
        private bool novo = false;

        // Formulário construtor
        public FrmTarefa()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            AtualizaDados();
        }

        private void VerificaBotoes()
        {
            buttonSalvar.Enabled = false;
            buttonCancelar.Enabled = false;
            buttonNovo.Enabled = PermissaoUsuario(); // Todos usuários podem incluir tarefa
            buttonAlterar.Enabled = false; // Habilitar se tiver registro
            buttonExcluir.Enabled = false; // Habilitar se tiver registro
            buttonPrimeira.Enabled = false; // Habilitar se tiver registro
            buttonAnterior.Enabled = false; // Habilitar se tiver registro
            buttonProxima.Enabled = false; // Habilitar se tiver registro
            buttonUltima.Enabled = false; // Habilitar se tiver registro
            buttonPesquisar.Enabled = true;

            if (paginacao.TotalLinhas > 0)
            {
                buttonPrimeira.Enabled = true;
                buttonAnterior.Enabled = true;
                buttonProxima.Enabled = true;
                buttonUltima.Enabled = true;
                buttonAlterar.Enabled = PermissaoUsuario();
                buttonExcluir.Enabled = PermissaoUsuario();
            }
        }

        private bool PermissaoUsuario()
        {
            bool resultado = (UsuarioSession.Funcao == "Administrador" || UsuarioSession.Funcao == "Gerente" || UsuarioSession.Funcao == "Empregado") ?
                true : false;

            return resultado;
        }

        private void HabilitaCampos()
        {
            if (novo)
            {
                textBoxTarCodigo.ReadOnly = false;
                textBoxTarNome.ReadOnly = false;
                textBoxTarCodigo.Clear();
                textBoxTarNome.Clear();
            } else
            {
                textBoxTarCodigo.ReadOnly = true;
                textBoxTarNome.ReadOnly = false;
            }
        }

        private void DesabilitaCampos()
        {
            textBoxTarCodigo.ReadOnly = true;
            textBoxTarNome.ReadOnly = true;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void AtualizaDados()
        {
            int skip = int.Parse(textBoxSkip.Text);
            int take = int.Parse(textBoxTake.Text);

            paginacao = await TarefaServices.Paginacao(textBoxPesquisa.Text, skip, take, checkBoxOrdem.Checked);
            dataGridView1.DataSource = paginacao.Dados;
            AtualizaCamposDetalhes();
            VerificaBotoes();
        }

        private void buttonSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonPesquisar_Click(object sender, EventArgs e)
        {
            AtualizaDados();
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
                AtualizaDados();
            }
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void buttonNovo_Click(object sender, EventArgs e)
        {
            novo = true;
            HabilitaBotoes();
            HabilitaCampos();
            tabControl1.SelectedTab = tabPage2;
            dateTimePickerDataInicial.Value = DateTime.Now.Date;
            dateTimePickerDataFinal.Value = DateTime.Now.Date;
            textBoxTarCodigo.Focus();
            comboBoxTarStatus.SelectedItem = "1-Incluida"; // Selecione o item específico como padrão
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
            AtualizaCamposDetalhes();
            VerificaBotoes();
            DesabilitaCampos();
        }

        private async void buttonSalvar_Click(object sender, EventArgs e)
        {
            string statusSelecionado = comboBoxTarStatus.SelectedItem.ToString();
            string[] partes = statusSelecionado.Split('-');
            int statusNumerico = int.Parse(partes[0]);

            if (int.TryParse(textBoxTarCodigo.Text, out int tarCodigo) &&
               DateTime.TryParse(dateTimePickerDataInicial.Text, out DateTime tarDataInicio) &&
               DateTime.TryParse(dateTimePickerDataFinal.Text, out DateTime tarDataFinal))
            {
                Tarefa tarefa = new Tarefa()
                {
                    TarCodigo = tarCodigo,
                    TarNome = textBoxTarNome.Text,
                    TarDataInicio = DateTime.SpecifyKind(new DateTime(tarDataInicio.Year, tarDataInicio.Month, tarDataInicio.Day, 0, 0, 0), DateTimeKind.Utc),
                    TarDataFinal = DateTime.SpecifyKind(new DateTime(tarDataFinal.Year, tarDataFinal.Month, tarDataFinal.Day, 0, 0, 0), DateTimeKind.Utc),
                    TarStatus = statusNumerico
                };

                var resultado = novo ? await TarefaServices.PostTarefa(tarefa) : await TarefaServices.PutTarefa(tarefa);

                if (resultado)
                {
                    tabControl1.SelectedTab = tabPage1;
                    AtualizaDados();
                    DesabilitaCampos();
                }
            } else
            {
                // Tratamento para quando a conversão falha
                MessageBox.Show("Erro ao converter os valores. Verifique os formatos.");
            }
        }

        private void HabilitaBotoes()
        {
            buttonNovo.Enabled = false;
            buttonAlterar.Enabled = false;
            buttonExcluir.Enabled = false;
            buttonSalvar.Enabled = true;
            buttonCancelar.Enabled = true;
            buttonPrimeira.Enabled = false;
            buttonAnterior.Enabled = false;
            buttonProxima.Enabled = false;
            buttonUltima.Enabled = false;
            buttonPesquisar.Enabled = false;
        }

        private void buttonAlterar_Click(object sender, EventArgs e)
        {
            novo = false;
            HabilitaBotoes();
            HabilitaCampos();
            tabControl1.SelectedTab = tabPage2;
            textBoxTarCodigo.Focus();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private async void buttonExcluir_Click(object sender, EventArgs e)
        {
            // Verificar se tem informação no grid
            if (dataGridView1.SelectedRows.Count > 0)
            {
                Tarefa tarefa = (Tarefa)dataGridView1.SelectedRows[0].DataBoundItem;
                DialogResult result =
                    MessageBox.Show(
                    null,
                    $"Deseja excluir a tarefa {tarefa.TarCodigo.ToString()} {tarefa.TarNome}",
                    "Lançamento de Horas",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question
                    );
                if (result == DialogResult.OK)
                {
                    var resultado = await TarefaServices.DeleteTarefa(tarefa.TarCodigo);
                    if (resultado)
                    {
                        tabControl1.SelectedTab = tabPage1;
                        AtualizaDados();
                    }
                }
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
    }
}
