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
    public partial class FrmUsuario : Form
    {
        private PaginacaoResponse<Usuario> paginacao = null;
        private bool novo = false;

        public FrmUsuario()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            AtualizaDados();
        }
        private void HabilitaBotoes()
        {
            buttonNovo.Enabled = false;
            buttonAlterar.Enabled = false;
            buttonExcluir.Enabled = false;
            buttonNovaSenha.Enabled = false;
            buttonSalvar.Enabled = true;
            buttonCancelar.Enabled = true;
            buttonPrimeira.Enabled = false;
            buttonAnterior.Enabled = false;
            buttonProxima.Enabled = false;
            buttonUltima.Enabled = false;
            buttonPesquisar.Enabled = false;
        }

        private void VerificaBotoes()
        {
            buttonSalvar.Enabled = false;
            buttonCancelar.Enabled = false;
            buttonNovo.Enabled = true;
            buttonAlterar.Enabled = false;
            buttonExcluir.Enabled = false;
            buttonNovaSenha.Enabled = false;
            buttonPrimeira.Enabled = false;
            buttonAnterior.Enabled = false;
            buttonProxima.Enabled = false;
            buttonUltima.Enabled = false;
            buttonPesquisar.Enabled = true;

            if (paginacao.TotalLinhas > 0)
            {
                buttonPrimeira.Enabled = true;
                buttonAnterior.Enabled = true;
                buttonProxima.Enabled = true;
                buttonUltima.Enabled = true;
                buttonAlterar.Enabled = true;
                buttonExcluir.Enabled = true;
                buttonNovaSenha.Enabled = true;
            }
        }

        private void HabilitaCampos()
        {
            if (novo)
            {
                textBoxId.ReadOnly = true;
                textBoxNome.ReadOnly = false;
                textBoxSenha.ReadOnly = true;
                textBoxNome.Clear();
                textBoxLogin.Clear();
                textBoxSenha.Clear();
            } else
            {
                textBoxId.ReadOnly = true;
                textBoxNome.ReadOnly = false;
                textBoxSenha.ReadOnly = true;
            }
        }

        private void DesabilitaCampos()
        {
            textBoxId.ReadOnly = true;
            textBoxNome.ReadOnly = true;
            textBoxLogin.ReadOnly = true;
            textBoxSenha.ReadOnly = true;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            AtualizaCamposDetalhes();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

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

        }

        private void buttonUltima_Click(object sender, EventArgs e)
        {

        }
        private async void AtualizaDados()
        {
            int skip = int.Parse(textBoxSkip.Text);
            int take = int.Parse(textBoxTake.Text);

            paginacao = await UsuarioServices.Paginacao(textBoxPesquisa.Text, skip, take, checkBoxOrdem.Checked);
            dataGridView1.DataSource = paginacao.Dados;
            AtualizaCamposDetalhes();
            VerificaBotoes();
        }

        private void AtualizaCamposDetalhes()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                Usuario usuario = (Usuario)dataGridView1.SelectedRows[0].DataBoundItem;
                textBoxId.Text = usuario.Id.ToString();
                textBoxNome.Text = usuario.Nome;
                textBoxLogin.Text = usuario.Login;
                textBoxSenha.Text = usuario.Password;
                comboBoxFuncao.Text = usuario.Funcao.ToString();
                int index = comboBoxFuncao.FindString(usuario.Funcao.ToString());
                comboBoxFuncao.SelectedIndex = index;
            }
        }

        private void buttonProxima_Click_1(object sender, EventArgs e)
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

        private void buttonUltima_Click_1(object sender, EventArgs e)
        {
            decimal paginas = (decimal)paginacao.TotalLinhas / paginacao.Take;
            int quantidadePaginas = (int)Math.Ceiling(paginas);

            if (paginacao.Skip < quantidadePaginas)
            {
                textBoxSkip.Text = quantidadePaginas.ToString();
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

        private void buttonPrimeira_Click_1(object sender, EventArgs e)
        {
            if (paginacao.Skip > 1)
            {
                textBoxSkip.Text = "1";
                AtualizaDados();
            }
        }

        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
        {
            AtualizaCamposDetalhes();
        }

        private void FrmUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonPesquisar_Click(sender, e);
            }
        }

        private void buttonNovo_Click(object sender, EventArgs e)
        {
            novo = true;
            HabilitaBotoes();
            HabilitaCampos();
            tabControl1.SelectedTab = tabPage2; ;
            textBoxNome.Focus();
            comboBoxFuncao.SelectedItem = "Empregado"; // Selecione o item específico como padrão
        }

        private void textBoxLogin_Leave(object sender, EventArgs e)
        {
            textBoxSenha.Text = textBoxLogin.Text;
            comboBoxFuncao.Focus();
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
            AtualizaCamposDetalhes();
        }

        private async void buttonSalvar_Click(object sender, EventArgs e)
        {
            CarregaSalvarAlterar(sender, e);
        }

        private async void CarregaSalvarAlterar(object sender, EventArgs e)
        {
            string funcaoSelecionada = comboBoxFuncao.SelectedItem.ToString();

            // Tenta converter o Id para Guid
            Guid usuarioId;
            if (!Guid.TryParse(textBoxId.Text, out usuarioId))
            {
                MessageBox.Show("Id inválido. Por favor, insira um Id válido.");
                return;
            }
            Usuario usuario = new Usuario()
            {
                Id = usuarioId,
                Nome = textBoxNome.Text,
                Login = textBoxLogin.Text,
                Password = textBoxSenha.Text,
                Funcao = funcaoSelecionada
            };

            var resultado = novo ? await UsuarioServices.PostUsuario(usuario) : await UsuarioServices.PutUsuario(usuario);

            if (resultado)
            {
                tabControl1.SelectedTab = tabPage1;
                AtualizaDados();
                DesabilitaCampos();
            }
        }
        private void buttonAlterar_Click(object sender, EventArgs e)
        {
            novo = false;
            HabilitaBotoes();
            HabilitaCampos();
            tabControl1.SelectedTab = tabPage2;
            textBoxNome.Focus();
        }

        private async void buttonExcluir_Click(object sender, EventArgs e)
        {
            // Verificar se tem informação no grid
            if (dataGridView1.SelectedRows.Count > 0)
            {
                Usuario usuario = (Usuario)dataGridView1.SelectedRows[0].DataBoundItem;
                DialogResult result =
                    MessageBox.Show(
                    null,
                    $"Deseja excluir o usuário {usuario.Nome} - login {usuario.Login}",
                    "Lançamento de Horas",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question
                    );
                if (result == DialogResult.OK)
                {
                    var resultado = await UsuarioServices.DeleteUsuario(usuario.Id);
                    if (resultado)
                    {
                        tabControl1.SelectedTab = tabPage1;
                        AtualizaDados();
                    }
                }
            }
        }

        private async void buttonNovaSenha_Click(object sender, EventArgs e)
        {
            novo = false;

            // Verificar se tem informação no grid
            if (dataGridView1.SelectedRows.Count > 0)
            {
                Usuario usuario = (Usuario)dataGridView1.SelectedRows[0].DataBoundItem;
                DialogResult result =
                    MessageBox.Show(
                    null,
                    $"Deseja alterar a senha do usuário {usuario.Nome} - login {usuario.Login}. A nova senha é igual ao login.",
                    "Lançamento de Horas",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question
                    );
                if (result == DialogResult.OK)
                {
                    CarregaSalvarAlterar(sender, e);
                }
            }
        }

        private void buttonPesquisar_Click_1(object sender, EventArgs e)
        {

        }
    }
}
