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

namespace FrontKeevo
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void buttonLogin_Click(object sender, EventArgs e)
        {
            // Verificação do usuário
            UsuarioLogin login = new UsuarioLogin()
            {
                Login = textBoxLogin.Text,
                Password = textBoxPassword.Text
            };

            UsuarioResponse response = await UsuarioServices.Login(login);

            if (response == null)
            {
                MessageBox.Show("Login ou senha inválidos.");
                return;
            }

            UsuarioSession.Id = response.Usuario.Id;
            UsuarioSession.Login = response.Usuario.Login;
            UsuarioSession.Nome = response.Usuario.Nome;
            UsuarioSession.Funcao = response.Usuario.Funcao;
            UsuarioSession.Token = response.Token;

            Close();
        }

        private void FrmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonLogin_Click(sender, e);
            }

        }
    }
}
