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
            labelUsuario.Text = "Usu�rio: " + UsuarioSession.Nome + " (" + UsuarioSession.Funcao + ")";
            verificaPermissoesUsuario();
        }

        private void verificaPermissoesUsuario()
        {
            usu�rioToolStripMenuItem.Enabled = false;
            if (UsuarioSession.Funcao == "Gerente" || UsuarioSession.Funcao == "Administrador")
            {
                usu�rioToolStripMenuItem.Enabled = true;
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

        private void usu�rioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmUsuario form = new FrmUsuario();
            form.ShowDialog();
        }

        private void lan�aHorasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmLancto form = new FrmLancto();
            form.ShowDialog();
        }
    }
}
