using FrontKeevo.Models;
using FrontKeevo.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static FrontKeevo.Models.ConsisteLancto;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using ToolTip = System.Windows.Forms.ToolTip;

namespace FrontKeevo.Views
{
    public partial class FrmLancto : Form
    {
        private PaginacaoResponse<ConsultaLanctoResponse> paginacaoResponse = null;
        private bool novo = false;
        private DateTime? dataHoraInicial;
        private DateTime? dataHoraFinal;
        private int horaInicio;
        private int horaFinal;
        private int minutoInicio;
        private int minutoFinal;
        private int tarefaCodigo;
        private bool finalizaTarefa;

        public FrmLancto()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            InicializaFormLancto();
            AtualizaDados();
        }

        private void InicializaFormLancto()
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.buttonConsultaTarefa, "Consulta Tarefa");

            dateTimePickerDataInicial.Text = dateTimePickerDataInicial.Value.Date.ToUniversalTime().AddDays(-7).ToString("o");
            textBoxHoraFinal.KeyPress += textBoxHoraFinal_KeyPress;
            textBoxHoraFinal.Leave += textBoxHoraFinal_Leave;
            textBoxMinutoFinal.KeyPress += textBoxMinutoFinal_KeyPress;
            textBoxMinutoFinal.Leave += textBoxMinutoFinal_Leave;
            buttonFinalizaTarefa.Enabled = false;
            textBox1TarNome.Enabled = false;

            if (!PermissaoUsuario())
            {
                textBoxUsuarioLogin.Text = UsuarioSession.Login;
                textBoxUsuarioLogin.Enabled = false;
                textBoxUsuarioNome.Text = UsuarioSession.Nome;
                textBoxUsuarioNome.Enabled = false;
                comboBoxUsuarioFuncao.Text = UsuarioSession.Funcao;
                comboBoxUsuarioFuncao.Enabled = false;
            }
        }

        private void buttonSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void AtualizaDados()
        {
            int skip = int.Parse(textBoxSkip.Text);
            int take = int.Parse(textBoxTake.Text);
            var dataFinalComHorario = dateTimePickerDataFinal.Value.Date.AddHours(23).AddMinutes(59);

            var consultaLanctoRequest = new
            {
                UsuarioLogin = textBoxUsuarioLogin.Text,
                UsuarioNome = textBoxUsuarioNome.Text,
                DataInicial = dateTimePickerDataInicial.Value.Date.ToUniversalTime().ToString("o"),
                DataFinal = dataFinalComHorario.ToUniversalTime().ToString("o"),
                TarCodigo = string.IsNullOrWhiteSpace(textBoxTarCodigo.Text) ? (int?)null : int.Parse(textBoxTarCodigo.Text),
                TarNome = textBoxTarNome.Text,
                UsuarioFuncao = comboBoxUsuarioFuncao.Text,
                HorarioAberto = checkBoxHorarioAberto.Checked ? 'S' : 'N'
            };
            string valor = JsonConvert.SerializeObject(consultaLanctoRequest);
            paginacaoResponse = await LanctoServices.Paginacao(valor, skip, take, checkBoxOrdem.Checked);

            dataGridView1.DataSource = paginacaoResponse.Dados;
            AtualizaCamposDetalhes();
            VerificaBotoes();
        }

        private void AtualizaCamposDetalhes()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                ConsultaLanctoResponse consultaLanctoResponse = (ConsultaLanctoResponse)dataGridView1.SelectedRows[0].DataBoundItem;
                textBoxLandId.Text = consultaLanctoResponse.LanId.ToString();

                dateTimePickerLanDataInicio.Text = consultaLanctoResponse.LanDataInicial.Value.ToString("dd/MM/yyyy");
                textBoxHoraInicio.Text = consultaLanctoResponse.LanDataInicial.Value.ToString("HH");
                textBoxMinutoInicio.Text = consultaLanctoResponse.LanDataInicial.Value.ToString("mm");
                if (consultaLanctoResponse.LanDataFinal.HasValue)
                {
                    dateTimePickerLanDataFinal.Text = consultaLanctoResponse.LanDataFinal.Value.ToString("dd/MM/yyyy");
                    textBoxHoraFinal.Text = consultaLanctoResponse.LanDataFinal.Value.ToString("HH");
                    textBoxMinutoFinal.Text = consultaLanctoResponse.LanDataFinal.Value.ToString("mm");
                } else
                {
                    dateTimePickerLanDataFinal.Text = dateTimePickerLanDataInicio.Text;
                    textBoxHoraFinal.Text = "";
                    textBoxMinutoFinal.Text = "";
                    buttonFinalizaTarefa.Enabled = true;
                }
                textBoxUsuarioId.Text = consultaLanctoResponse.Id.ToString();
                textBoxNome.Text = consultaLanctoResponse.Nome;
                textBox1TarCodigo.Text = consultaLanctoResponse.TarCodigo.ToString();
                textBox1TarNome.Text = consultaLanctoResponse.TarNome;
            }
        }
        private void VerificaBotoes()
        {
            buttonSalvar.Enabled = false;
            buttonCancelar.Enabled = false;
            buttonNovo.Enabled = true;
            buttonAlterar.Enabled = false;
            buttonExcluir.Enabled = false;
            buttonPrimeira.Enabled = false;
            buttonAnterior.Enabled = false;
            buttonProxima.Enabled = false;
            buttonUltima.Enabled = false;

            if (paginacaoResponse != null && paginacaoResponse.TotalLinhas > 0)
            {
                buttonPrimeira.Enabled = true;
                buttonAnterior.Enabled = true;
                buttonProxima.Enabled = true;
                buttonUltima.Enabled = true;
                buttonAlterar.Enabled = true;
                buttonExcluir.Enabled = true;
            }
        }

        private static bool PermissaoUsuario()
        {
            bool resultado = (UsuarioSession.Funcao == "Administrador" || UsuarioSession.Funcao == "Gerente") ?
                true : false;

            return resultado;
        }

        private void DesabilitaCampos()
        {
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            checkBoxInicializaAgora.Checked = true;

            if (novo)
            {
                DateTime dataHoraAgora = DateTime.Now;
                dateTimePickerLanDataInicio.Value = dataHoraAgora;
                horaInicio = dataHoraAgora.Hour;
                minutoInicio = dataHoraAgora.Minute;
                textBoxHoraInicio.Text = horaInicio.ToString();
                textBoxMinutoInicio.Text = minutoInicio.ToString();

                DateTime dataHoraFinal = dataHoraAgora.AddMinutes(5);
                dateTimePickerLanDataFinal.Value = dataHoraFinal;
                horaFinal = dataHoraFinal.Hour;
                minutoFinal = dataHoraFinal.Minute;
                textBoxHoraFinal.Text = horaFinal.ToString();
                textBoxMinutoFinal.Text = minutoFinal.ToString();
            } else
            {
                checkBoxInicializaAgora.Checked = false;
            }
        }

        private void textBoxHoraFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permite apenas dígitos, backspace, e delete
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxHoraFinal_Leave(object sender, EventArgs e)
        {
            if (textBoxHoraFinal.Text == null || textBoxHoraFinal.Text == "")
            {
                textBoxMinutoFinal.Text = "";
            } else
            {
                // Após a entrada, verificar se o valor está entre 0 e 23
                if (int.TryParse(textBoxHoraFinal.Text, out int horas))
                {
                    if (horas < 0 || horas > 23)
                    {
                        MessageBox.Show("Por favor, insira um valor entre 0 e 23 horas.");
                        textBoxHoraFinal.Focus();
                    }
                } else
                {
                    MessageBox.Show("Por favor, insira um número válido entre 0 e 23 horas.");
                    textBoxHoraFinal.Focus();
                }
            }
        }

        private void textBoxMinutoFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permite apenas dígitos, backspace, e delete
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxMinutoFinal_Leave(object sender, EventArgs e)
        {
            if (textBoxMinutoFinal.Text == null || textBoxMinutoFinal.Text == "")
            {
                textBoxHoraFinal.Text = "";
            } else
            {
                // Após a entrada, verificar se o valor está entre 0 e 23
                if (int.TryParse(textBoxMinutoFinal.Text, out int horas))
                {
                    if (horas < 0 || horas > 59)
                    {
                        MessageBox.Show("Por favor, insira um valor entre 0 e 59 minutos.");
                        textBoxMinutoFinal.Focus();
                    }
                } else
                {
                    MessageBox.Show("Por favor, insira um número válido entre 0 e 59 minutos.");
                    textBoxMinutoFinal.Focus();
                }
            }
        }

        private void textBoxHoraInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permite apenas dígitos, backspace, e delete
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxHoraInicio_Leave(object sender, EventArgs e)
        {
            // Após a entrada, verificar se o valor está entre 0 e 23
            if (int.TryParse(textBoxHoraInicio.Text, out horaInicio))
            {
                if (horaInicio < 0 || horaInicio > 23)
                {
                    MessageBox.Show("Por favor, insira um valor entre 0 e 23 horas.");
                    textBoxHoraInicio.Focus();
                }
            } else
            {
                MessageBox.Show("Por favor, insira um número válido entre 0 e 23 horas.");
                textBoxHoraInicio.Focus();
            }
        }

        private void textBoxMinutoInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permite apenas dígitos, backspace, e delete
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxMinutoInicio_Leave(object sender, EventArgs e)
        {    // Após a entrada, verificar se o valor está entre 0 e 23
            if (int.TryParse(textBoxMinutoInicio.Text, out minutoInicio))
            {
                if (minutoInicio < 0 || minutoInicio > 59)
                {
                    MessageBox.Show("Por favor, insira um valor entre 0 e 59 minutos.");
                    textBoxMinutoInicio.Focus();
                }
            } else
            {
                MessageBox.Show("Por favor, insira um número válido entre 0 e 59 minutos.");
                textBoxMinutoInicio.Focus();
            }

            dataHoraInicial = new DateTime
                (
                dateTimePickerLanDataInicio.Value.Year,
                dateTimePickerLanDataInicio.Value.Month,
                dateTimePickerLanDataInicio.Value.Day,
                horaInicio,
                minutoInicio,
                0 // segundos
                );

            // Fuso horário de Brasília (GMT-3)
            TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            dataHoraInicial = TimeZoneInfo.ConvertTime((DateTime)dataHoraInicial, timeZone);
        }

        private void buttonPesquisar_Click(object sender, EventArgs e)
        {
            buttonFinalizaTarefa.Enabled = false;
            AtualizaDados();
        }

        private void FrmLancto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonPesquisar_Click(sender, e);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            AtualizaCamposDetalhes();
        }

        private void buttonPrimeira_Click(object sender, EventArgs e)
        {
            if (paginacaoResponse.Skip > 1)
            {
                textBoxSkip.Text = "1";
                AtualizaDados();
            }
        }

        private void buttonAnterior_Click(object sender, EventArgs e)
        {
            if (paginacaoResponse.Skip > 1)
            {
                paginacaoResponse.Skip--;
                textBoxSkip.Text = paginacaoResponse.Skip.ToString();
                AtualizaDados();
            }
        }

        private void buttonProxima_Click(object sender, EventArgs e)
        {
            decimal paginas = (decimal)paginacaoResponse.TotalLinhas / paginacaoResponse.Take;
            int quantidadePaginas = (int)Math.Ceiling(paginas);

            if (paginacaoResponse.Skip < quantidadePaginas)
            {
                paginacaoResponse.Skip++;
                textBoxSkip.Text = paginacaoResponse.Skip.ToString();
                AtualizaDados();
            }
        }

        private void buttonUltima_Click(object sender, EventArgs e)
        {
            decimal paginas = (decimal)paginacaoResponse.TotalLinhas / paginacaoResponse.Take;
            int quantidadePaginas = (int)Math.Ceiling(paginas);

            if (paginacaoResponse.Skip < quantidadePaginas)
            {
                textBoxSkip.Text = quantidadePaginas.ToString();
                AtualizaDados();
            }
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
            AtualizaCamposDetalhes();
            VerificaBotoes();
            DesabilitaCampos();
        }

        private async void textBox1TarCodigo_Leave(object sender, EventArgs e)
        {
            Tarefa tarefa = new Tarefa();

            if (int.TryParse(textBox1TarCodigo.Text, out tarefaCodigo))
            {
                if (tarefaCodigo == 0)
                {
                    MessageBox.Show("Informe um valor maior que zero.");
                    textBoxHoraInicio.Focus();
                } else
                {
                    tarefa = await TarefaServices.BuscaPorCodigo(tarefaCodigo);

                    textBox1TarNome.Text = tarefa.TarNome;

                    if (tarefa.TarDataFinal != null && novo)
                    {
                        MessageBox.Show("Tarefa finalizada, não pode lançar horas.");
                    }
                    textBox1TarNome.Text = tarefa.TarNome;
                }
            } else
            {
                MessageBox.Show("Informe um número maior que zero.");
            }
        }

        private void textBox1TarCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permite apenas dígitos, backspace, e delete
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void buttonNovo_Click(object sender, EventArgs e)
        {
            novo = true;
            HabilitaBotoes();
            HabilitaCampos();
            DateTime dataHoraAgora = DateTime.Now;
            dateTimePickerLanDataInicio.Value = dataHoraAgora;
            horaInicio = dataHoraAgora.Hour;
            minutoInicio = dataHoraAgora.Minute;
            textBoxHoraInicio.Text = horaInicio.ToString();
            textBoxMinutoInicio.Text = minutoInicio.ToString();
            tabControl1.SelectedTab = tabPage2;
            textBox1TarCodigo.Focus();
        }

        private void HabilitaBotoes()
        {
            buttonNovo.Enabled = false;
            buttonAlterar.Enabled = false;
            buttonExcluir.Enabled = false;
            buttonFinalizaTarefa.Enabled = false;
            buttonSalvar.Enabled = true;
            buttonCancelar.Enabled = true;
            buttonPrimeira.Enabled = false;
            buttonAnterior.Enabled = false;
            buttonProxima.Enabled = false;
            buttonUltima.Enabled = false;
            buttonPesquisar.Enabled = true;
        }

        private void HabilitaCampos()
        {
            Guid novoLanId = Guid.NewGuid();

            textBoxLandId.ReadOnly = true;
            textBoxUsuarioId.ReadOnly = true;
            textBoxNome.ReadOnly = true;
            if (novo)
            {
                textBoxLandId.Text = novoLanId.ToString();
                textBox1TarCodigo.Clear();
                textBox1TarNome.Clear();
                textBoxHoraInicio.Clear();
                textBoxMinutoInicio.Clear();
                textBoxHoraFinal.Clear();
                textBoxMinutoFinal.Clear();
                textBoxUsuarioId.Text = UsuarioSession.Id.ToString();
                textBoxNome.Text = UsuarioSession.Nome;
            } else
            {
                checkBoxInicializaAgora.Enabled = false;
                dateTimePickerLanDataInicio.Enabled = false;
                dateTimePickerLanDataFinal.Enabled = false;
                textBoxHoraInicio.ReadOnly = true;
                textBoxMinutoInicio.ReadOnly = true;
                textBoxHoraFinal.ReadOnly = true;
                textBoxMinutoFinal.ReadOnly = true;
            }
        }

        private async void buttonSalvar_Click(object sender, EventArgs e)
        {
            if (textBoxHoraFinal.Text == "" && textBoxMinutoFinal.Text == "")
            {
                finalizaTarefa = false;
            }
            if (textBoxHoraFinal.Text != "" && textBoxMinutoFinal.Text != "")
            {
                finalizaTarefa = true;
            }
            if ((textBoxHoraFinal.Text != "" && textBoxMinutoFinal.Text == "") ||
                (textBoxHoraFinal.Text == "" && textBoxMinutoFinal.Text != ""))
            {
                MessageBox.Show("Preencha o horário final corretamente.");
            } else
            {
                Guid lanId;
                if (!Guid.TryParse(textBoxLandId.Text, out lanId))
                {
                    MessageBox.Show("Id lancto inválido. Por favor, insira um Id válido.");
                    return;
                }

                Guid usuarioId;
                if (!Guid.TryParse(textBoxUsuarioId.Text, out usuarioId))
                {
                    MessageBox.Show("UsuarioId inválido. Por favor, insira um Id válido.");
                    return;
                }

                if (DateTime.TryParse(dateTimePickerLanDataInicio.Text, out DateTime dataInicio))
                {
                    dataInicio = dataInicio.Date;
                }

                if (DateTime.TryParse(dateTimePickerLanDataFinal.Text, out DateTime dataFinal))
                {
                    dataFinal = dataFinal.Date;
                }

                if (int.TryParse(textBox1TarCodigo.Text, out int tarCodigo))
                {
                    Lancto lancto = new Lancto()
                    {
                        LanId = lanId,
                        LanDataInicio = DateTime.SpecifyKind(new DateTime(dataInicio.Year, dataInicio.Month, dataInicio.Day,
                                                                           int.Parse(textBoxHoraInicio.Text), int.Parse(textBoxMinutoInicio.Text), 0), DateTimeKind.Utc),
                        LanDataFinal = finalizaTarefa ? DateTime.SpecifyKind(new DateTime(dataFinal.Year, dataFinal.Month, dataFinal.Day,
                                                                         int.Parse(textBoxHoraFinal.Text), int.Parse(textBoxMinutoFinal.Text), 0), DateTimeKind.Utc) : null,
                        UsuarioId = usuarioId,
                        TarCodigo = tarCodigo
                    };

                    ConsisteLanctoResquest consisteLanctoResquest = new ConsisteLanctoResquest()
                    {
                        Id = usuarioId,
                        LanDataInicial = DateTime.SpecifyKind(new DateTime(dataInicio.Year, dataInicio.Month, dataInicio.Day,
                                                                           int.Parse(textBoxHoraInicio.Text), int.Parse(textBoxMinutoInicio.Text), 0), DateTimeKind.Utc),
                        LanDataFinal = finalizaTarefa ? DateTime.SpecifyKind(new DateTime(dataFinal.Year, dataFinal.Month, dataFinal.Day,
                                                                         int.Parse(textBoxHoraFinal.Text), int.Parse(textBoxMinutoFinal.Text), 0), DateTimeKind.Utc) : null,
                        InclusaoRegistro = novo,
                    };

                    var resultado = await LanctoServices.PostConsistenciaLancto(consisteLanctoResquest);

                    if (!resultado)
                    {
                        tabControl1.SelectedTab = tabPage1;
                        AtualizaDados();
                        DesabilitaCampos();
                    } else
                    {
                        resultado = novo ? await LanctoServices.PostLancto(lancto) : await LanctoServices.PutLancto(lancto);

                        if (resultado)
                        {
                            tabControl1.SelectedTab = tabPage1;
                            AtualizaDados();
                            DesabilitaCampos();
                        }
                    }
                } else
                {
                    // Tratamento para quando a conversão falha
                    MessageBox.Show("Erro ao converter os valores. Verifique os formatos.");
                }
            }
        }

        private async void buttonExcluir_Click(object sender, EventArgs e)
        {
            // Verificar se tem informação no grid
            if (dataGridView1.SelectedRows.Count > 0)
            {
                ConsultaLanctoResponse consultaLanctoResponse = (ConsultaLanctoResponse)dataGridView1.SelectedRows[0].DataBoundItem;
                DialogResult result =
                    MessageBox.Show(
                null,
                    $"Deseja excluir o lançamento de horas referente a tarefa {consultaLanctoResponse.TarCodigo}-{consultaLanctoResponse.TarNome} do dia {consultaLanctoResponse.LanDataInicial.Value:dd/MM/yyyy hh:mm}",
                    "Lançamento de Horas",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question
                    );
                if (result == DialogResult.OK)
                {
                    var resultado = await LanctoServices.DeleteLancto(consultaLanctoResponse.LanId);
                    if (resultado)
                    {
                        tabControl1.SelectedTab = tabPage1;
                        AtualizaDados();
                    }
                }
            }
        }

        private void buttonAlterar_Click(object sender, EventArgs e)
        {
            novo = false;
            HabilitaBotoes();
            HabilitaCampos();
            tabControl1.SelectedTab = tabPage2;
            textBoxTarCodigo.Focus();
        }

        private void buttonConsultaTarefa_Click(object sender, EventArgs e)
        {
            FrmHelpTarefa form = new FrmHelpTarefa();
            form.Owner = this;
            form.ShowDialog();

            textBox1TarCodigo.Text = TarefaSession.TarCodigo.ToString();
            textBox1TarNome.Text = TarefaSession.TarNome.ToString();

            TarefaSession.LimparSessao();
        }

        private async void buttonFinalizaTarefa_Click(object sender, EventArgs e)
        {
            novo = false;

            // Verificar se tem informação no grid
            if (dataGridView1.SelectedRows.Count > 0)
            {
                ConsultaLanctoResponse consultaLanctoResponse = (ConsultaLanctoResponse)dataGridView1.SelectedRows[0].DataBoundItem;
                DialogResult result =
                    MessageBox.Show(
                null,
                    $"Deseja finalizar a tarefa agora {consultaLanctoResponse.TarCodigo}-{consultaLanctoResponse.TarNome} do dia {consultaLanctoResponse.LanDataInicial.Value:dd/MM/yyyy hh:mm}",
                    "Lançamento de Horas",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question
                    );
                if (result == DialogResult.OK)
                {
                    Guid lanId;
                    if (!Guid.TryParse(textBoxLandId.Text, out lanId))
                    {
                        MessageBox.Show("Id lancto inválido. Por favor, insira um Id válido.");
                        return;
                    }
                    Guid usuarioId;
                    if (!Guid.TryParse(textBoxUsuarioId.Text, out usuarioId))
                    {
                        MessageBox.Show("UsuarioId inválido. Por favor, insira um Id válido.");
                        return;
                    }

                    if (DateTime.TryParse(dateTimePickerLanDataInicio.Text, out DateTime dataInicio))
                    {
                        dataInicio = dataInicio.Date;
                    }

                    if (DateTime.TryParse(dateTimePickerLanDataFinal.Text, out DateTime dataFinal))
                    {
                        dataFinal = dataFinal.Date;
                        dataFinal = DateTime.Now;
                        textBoxHoraFinal.Text = dataFinal.Hour.ToString();
                        textBoxMinutoFinal.Text = dataFinal.Minute.ToString();
                    }

                    if (int.TryParse(textBox1TarCodigo.Text, out int tarCodigo))
                    {
                        Lancto lancto = new Lancto()
                        {
                            LanId = lanId,
                            LanDataInicio = DateTime.SpecifyKind(new DateTime(dataInicio.Year, dataInicio.Month, dataInicio.Day,
                                                                               int.Parse(textBoxHoraInicio.Text), int.Parse(textBoxMinutoInicio.Text), 0), DateTimeKind.Utc),
                            LanDataFinal = DateTime.SpecifyKind(new DateTime(dataFinal.Year, dataFinal.Month, dataFinal.Day,
                                                                             int.Parse(textBoxHoraFinal.Text), int.Parse(textBoxMinutoFinal.Text), 0), DateTimeKind.Utc),
                            UsuarioId = usuarioId,
                            TarCodigo = tarCodigo
                        };

                        ConsisteLanctoResquest consisteLanctoResquest = new ConsisteLanctoResquest()
                        {
                            Id = usuarioId,
                            LanDataInicial = DateTime.SpecifyKind(new DateTime(dataInicio.Year, dataInicio.Month, dataInicio.Day,
                                                                               int.Parse(textBoxHoraInicio.Text), int.Parse(textBoxMinutoInicio.Text), 0), DateTimeKind.Utc),
                            LanDataFinal = DateTime.SpecifyKind(new DateTime(dataFinal.Year, dataFinal.Month, dataFinal.Day,
                                                                             int.Parse(textBoxHoraFinal.Text), int.Parse(textBoxMinutoFinal.Text), 0), DateTimeKind.Utc),
                            InclusaoRegistro = novo,
                        };

                        var resultado = await LanctoServices.PostConsistenciaLancto(consisteLanctoResquest);

                        if (!resultado)
                        {
                            tabControl1.SelectedTab = tabPage1;
                            AtualizaDados();
                            DesabilitaCampos();
                        } else
                        {
                            resultado = await LanctoServices.PutLancto(lancto);

                            if (resultado)
                            {
                                tabControl1.SelectedTab = tabPage1;
                                AtualizaDados();
                                DesabilitaCampos();
                            }
                        }
                    } else
                    {
                        // Tratamento para quando a conversão falha
                        MessageBox.Show("Erro ao converter os valores. Verifique os formatos.");
                    }
                }
            }
        }
    };
}