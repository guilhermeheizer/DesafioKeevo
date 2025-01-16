using FrontKeevo.Models;
using FrontKeevo.Views;
using System;
using System.Windows.Forms;

namespace FrontKeevo
{
    public partial class FrmPrincipal : Form
    {
        // Metodo construtor
        public FrmPrincipal()
        {
            InitializeComponent();
            labelUsuario.Text = "Usuário: " + UsuarioSession.Nome;
            verificaPermissoesUsuario();
        }

        private void verificaPermissoesUsuario()
        {
            usuárioToolStripMenuItem.Enabled = false;
            if (UsuarioSession.Funcao == "Gerente" || UsuarioSession.Funcao == "Administrador")
            {
                usuárioToolStripMenuItem.Enabled = true;
            }
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tarefaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTarefa form = new FrmTarefa();
            form.ShowDialog();
        }
    }
}
