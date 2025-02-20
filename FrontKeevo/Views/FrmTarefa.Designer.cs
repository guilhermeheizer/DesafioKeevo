namespace FrontKeevo.Views
{
    partial class FrmTarefa
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTarefa));
            panel1 = new System.Windows.Forms.Panel();
            panel5 = new System.Windows.Forms.Panel();
            checkBoxOrdem = new System.Windows.Forms.CheckBox();
            buttonPesquisar = new System.Windows.Forms.Button();
            textBoxPesquisa = new System.Windows.Forms.TextBox();
            panel4 = new System.Windows.Forms.Panel();
            textBoxSkip = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            textBoxTake = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            buttonUltima = new System.Windows.Forms.Button();
            buttonProxima = new System.Windows.Forms.Button();
            buttonAnterior = new System.Windows.Forms.Button();
            buttonPrimeira = new System.Windows.Forms.Button();
            panel3 = new System.Windows.Forms.Panel();
            buttonSair = new System.Windows.Forms.Button();
            panel2 = new System.Windows.Forms.Panel();
            panel7 = new System.Windows.Forms.Panel();
            buttonCancelar = new System.Windows.Forms.Button();
            buttonSalvar = new System.Windows.Forms.Button();
            panel6 = new System.Windows.Forms.Panel();
            buttonExcluir = new System.Windows.Forms.Button();
            buttonAlterar = new System.Windows.Forms.Button();
            buttonNovo = new System.Windows.Forms.Button();
            tabControl1 = new System.Windows.Forms.TabControl();
            tabPage1 = new System.Windows.Forms.TabPage();
            dataGridView1 = new System.Windows.Forms.DataGridView();
            Tarefa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Nome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataInicio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataFim = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            tabPage2 = new System.Windows.Forms.TabPage();
            comboBoxTarStatus = new System.Windows.Forms.ComboBox();
            dateTimePickerDataFinal = new System.Windows.Forms.DateTimePicker();
            dateTimePickerDataInicial = new System.Windows.Forms.DateTimePicker();
            label7 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            textBoxTarNome = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();
            textBoxTarCodigo = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            panel1.SuspendLayout();
            panel5.SuspendLayout();
            panel4.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            panel7.SuspendLayout();
            panel6.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(panel5);
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(panel3);
            panel1.Dock = System.Windows.Forms.DockStyle.Top;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(1030, 120);
            panel1.TabIndex = 0;
            // 
            // panel5
            // 
            panel5.Controls.Add(checkBoxOrdem);
            panel5.Controls.Add(buttonPesquisar);
            panel5.Controls.Add(textBoxPesquisa);
            panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            panel5.Location = new System.Drawing.Point(491, 0);
            panel5.Name = "panel5";
            panel5.Size = new System.Drawing.Size(425, 120);
            panel5.TabIndex = 2;
            // 
            // checkBoxOrdem
            // 
            checkBoxOrdem.AutoSize = true;
            checkBoxOrdem.Location = new System.Drawing.Point(6, 84);
            checkBoxOrdem.Name = "checkBoxOrdem";
            checkBoxOrdem.Size = new System.Drawing.Size(170, 24);
            checkBoxOrdem.TabIndex = 2;
            checkBoxOrdem.Text = "Ordenar Decrescente";
            checkBoxOrdem.UseVisualStyleBackColor = true;
            // 
            // buttonPesquisar
            // 
            buttonPesquisar.Image = (System.Drawing.Image)resources.GetObject("buttonPesquisar.Image");
            buttonPesquisar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonPesquisar.Location = new System.Drawing.Point(292, 25);
            buttonPesquisar.Name = "buttonPesquisar";
            buttonPesquisar.Size = new System.Drawing.Size(113, 44);
            buttonPesquisar.TabIndex = 1;
            buttonPesquisar.Text = "Pesquisar";
            buttonPesquisar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            buttonPesquisar.UseVisualStyleBackColor = true;
            buttonPesquisar.Click += buttonPesquisar_Click;
            // 
            // textBoxPesquisa
            // 
            textBoxPesquisa.Location = new System.Drawing.Point(6, 25);
            textBoxPesquisa.Name = "textBoxPesquisa";
            textBoxPesquisa.Size = new System.Drawing.Size(280, 27);
            textBoxPesquisa.TabIndex = 0;
            // 
            // panel4
            // 
            panel4.Controls.Add(textBoxSkip);
            panel4.Controls.Add(label2);
            panel4.Controls.Add(textBoxTake);
            panel4.Controls.Add(label1);
            panel4.Controls.Add(buttonUltima);
            panel4.Controls.Add(buttonProxima);
            panel4.Controls.Add(buttonAnterior);
            panel4.Controls.Add(buttonPrimeira);
            panel4.Dock = System.Windows.Forms.DockStyle.Left;
            panel4.Location = new System.Drawing.Point(0, 0);
            panel4.Name = "panel4";
            panel4.Size = new System.Drawing.Size(491, 120);
            panel4.TabIndex = 1;
            // 
            // textBoxSkip
            // 
            textBoxSkip.Location = new System.Drawing.Point(268, 84);
            textBoxSkip.Name = "textBoxSkip";
            textBoxSkip.Size = new System.Drawing.Size(40, 27);
            textBoxSkip.TabIndex = 7;
            textBoxSkip.Text = "1";
            textBoxSkip.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            textBoxSkip.TextChanged += textBox1_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(209, 84);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(53, 20);
            label2.TabIndex = 6;
            label2.Text = "Página";
            // 
            // textBoxTake
            // 
            textBoxTake.Location = new System.Drawing.Point(163, 84);
            textBoxTake.Name = "textBoxTake";
            textBoxTake.Size = new System.Drawing.Size(40, 27);
            textBoxTake.TabIndex = 5;
            textBoxTake.Text = "10";
            textBoxTake.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 84);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(145, 20);
            label1.TabIndex = 4;
            label1.Text = "Registros por Página";
            // 
            // buttonUltima
            // 
            buttonUltima.Image = (System.Drawing.Image)resources.GetObject("buttonUltima.Image");
            buttonUltima.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            buttonUltima.Location = new System.Drawing.Point(369, 25);
            buttonUltima.Name = "buttonUltima";
            buttonUltima.Size = new System.Drawing.Size(113, 44);
            buttonUltima.TabIndex = 3;
            buttonUltima.Text = "Última";
            buttonUltima.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            buttonUltima.UseVisualStyleBackColor = true;
            buttonUltima.Click += buttonUltima_Click;
            // 
            // buttonProxima
            // 
            buttonProxima.Image = (System.Drawing.Image)resources.GetObject("buttonProxima.Image");
            buttonProxima.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            buttonProxima.Location = new System.Drawing.Point(250, 25);
            buttonProxima.Name = "buttonProxima";
            buttonProxima.Size = new System.Drawing.Size(113, 44);
            buttonProxima.TabIndex = 2;
            buttonProxima.Text = "Próxima";
            buttonProxima.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            buttonProxima.UseVisualStyleBackColor = true;
            buttonProxima.Click += buttonProxima_Click;
            // 
            // buttonAnterior
            // 
            buttonAnterior.Image = (System.Drawing.Image)resources.GetObject("buttonAnterior.Image");
            buttonAnterior.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            buttonAnterior.Location = new System.Drawing.Point(131, 25);
            buttonAnterior.Name = "buttonAnterior";
            buttonAnterior.Size = new System.Drawing.Size(113, 44);
            buttonAnterior.TabIndex = 1;
            buttonAnterior.Text = "Anterior";
            buttonAnterior.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            buttonAnterior.UseVisualStyleBackColor = true;
            buttonAnterior.Click += buttonAnterior_Click;
            // 
            // buttonPrimeira
            // 
            buttonPrimeira.Image = Properties.Resources.SetaPrimeira32X32;
            buttonPrimeira.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            buttonPrimeira.Location = new System.Drawing.Point(12, 25);
            buttonPrimeira.Name = "buttonPrimeira";
            buttonPrimeira.Size = new System.Drawing.Size(113, 44);
            buttonPrimeira.TabIndex = 0;
            buttonPrimeira.Text = "Primeira";
            buttonPrimeira.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            buttonPrimeira.UseVisualStyleBackColor = true;
            buttonPrimeira.Click += buttonPrimeira_Click;
            // 
            // panel3
            // 
            panel3.Controls.Add(buttonSair);
            panel3.Dock = System.Windows.Forms.DockStyle.Right;
            panel3.Location = new System.Drawing.Point(916, 0);
            panel3.Name = "panel3";
            panel3.Size = new System.Drawing.Size(114, 120);
            panel3.TabIndex = 0;
            // 
            // buttonSair
            // 
            buttonSair.Image = Properties.Resources.Sair32X32;
            buttonSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonSair.Location = new System.Drawing.Point(8, 25);
            buttonSair.Name = "buttonSair";
            buttonSair.Size = new System.Drawing.Size(94, 44);
            buttonSair.TabIndex = 0;
            buttonSair.Text = "Sair";
            buttonSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            buttonSair.UseVisualStyleBackColor = true;
            buttonSair.Click += buttonSair_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(panel7);
            panel2.Controls.Add(panel6);
            panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            panel2.Location = new System.Drawing.Point(0, 471);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(1030, 66);
            panel2.TabIndex = 1;
            // 
            // panel7
            // 
            panel7.Controls.Add(buttonCancelar);
            panel7.Controls.Add(buttonSalvar);
            panel7.Dock = System.Windows.Forms.DockStyle.Right;
            panel7.Location = new System.Drawing.Point(769, 0);
            panel7.Name = "panel7";
            panel7.Size = new System.Drawing.Size(261, 66);
            panel7.TabIndex = 1;
            // 
            // buttonCancelar
            // 
            buttonCancelar.Image = Properties.Resources.Cancelar32X32;
            buttonCancelar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            buttonCancelar.Location = new System.Drawing.Point(131, 10);
            buttonCancelar.Name = "buttonCancelar";
            buttonCancelar.Size = new System.Drawing.Size(113, 44);
            buttonCancelar.TabIndex = 12;
            buttonCancelar.Text = "Cancelar";
            buttonCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            buttonCancelar.UseVisualStyleBackColor = true;
            buttonCancelar.Click += buttonCancelar_Click;
            // 
            // buttonSalvar
            // 
            buttonSalvar.Image = (System.Drawing.Image)resources.GetObject("buttonSalvar.Image");
            buttonSalvar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            buttonSalvar.Location = new System.Drawing.Point(12, 10);
            buttonSalvar.Name = "buttonSalvar";
            buttonSalvar.Size = new System.Drawing.Size(113, 44);
            buttonSalvar.TabIndex = 11;
            buttonSalvar.Text = "Salvar";
            buttonSalvar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            buttonSalvar.UseVisualStyleBackColor = true;
            buttonSalvar.Click += buttonSalvar_Click;
            // 
            // panel6
            // 
            panel6.Controls.Add(buttonExcluir);
            panel6.Controls.Add(buttonAlterar);
            panel6.Controls.Add(buttonNovo);
            panel6.Dock = System.Windows.Forms.DockStyle.Left;
            panel6.Location = new System.Drawing.Point(0, 0);
            panel6.Name = "panel6";
            panel6.Size = new System.Drawing.Size(491, 66);
            panel6.TabIndex = 0;
            // 
            // buttonExcluir
            // 
            buttonExcluir.Image = (System.Drawing.Image)resources.GetObject("buttonExcluir.Image");
            buttonExcluir.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            buttonExcluir.Location = new System.Drawing.Point(250, 10);
            buttonExcluir.Name = "buttonExcluir";
            buttonExcluir.Size = new System.Drawing.Size(113, 44);
            buttonExcluir.TabIndex = 10;
            buttonExcluir.Text = "Excluir";
            buttonExcluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            buttonExcluir.UseVisualStyleBackColor = true;
            buttonExcluir.Click += buttonExcluir_Click;
            // 
            // buttonAlterar
            // 
            buttonAlterar.Image = (System.Drawing.Image)resources.GetObject("buttonAlterar.Image");
            buttonAlterar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            buttonAlterar.Location = new System.Drawing.Point(131, 10);
            buttonAlterar.Name = "buttonAlterar";
            buttonAlterar.Size = new System.Drawing.Size(113, 44);
            buttonAlterar.TabIndex = 9;
            buttonAlterar.Text = "Alterar";
            buttonAlterar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            buttonAlterar.UseVisualStyleBackColor = true;
            buttonAlterar.Click += buttonAlterar_Click;
            // 
            // buttonNovo
            // 
            buttonNovo.Image = (System.Drawing.Image)resources.GetObject("buttonNovo.Image");
            buttonNovo.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            buttonNovo.Location = new System.Drawing.Point(12, 10);
            buttonNovo.Name = "buttonNovo";
            buttonNovo.Size = new System.Drawing.Size(113, 44);
            buttonNovo.TabIndex = 8;
            buttonNovo.Text = "Novo";
            buttonNovo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            buttonNovo.UseVisualStyleBackColor = true;
            buttonNovo.Click += buttonNovo_Click;
            // 
            // tabControl1
            // 
            tabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            tabControl1.Location = new System.Drawing.Point(0, 120);
            tabControl1.Multiline = true;
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(1030, 351);
            tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(dataGridView1);
            tabPage1.Location = new System.Drawing.Point(30, 4);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new System.Windows.Forms.Padding(3);
            tabPage1.Size = new System.Drawing.Size(996, 343);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Registros";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonShadow;
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { Tarefa, Nome, DataInicio, DataFim, Status });
            dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridView1.GridColor = System.Drawing.Color.DarkGray;
            dataGridView1.Location = new System.Drawing.Point(3, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new System.Drawing.Size(990, 337);
            dataGridView1.TabIndex = 0;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            // 
            // Tarefa
            // 
            Tarefa.DataPropertyName = "TarCodigo";
            Tarefa.HeaderText = "Tarefa";
            Tarefa.MinimumWidth = 6;
            Tarefa.Name = "Tarefa";
            Tarefa.Width = 60;
            // 
            // Nome
            // 
            Nome.DataPropertyName = "TarNome";
            Nome.HeaderText = "Nome";
            Nome.MinimumWidth = 6;
            Nome.Name = "Nome";
            Nome.Width = 500;
            // 
            // DataInicio
            // 
            DataInicio.DataPropertyName = "TarDataInicio";
            DataInicio.HeaderText = "Data Início";
            DataInicio.MinimumWidth = 6;
            DataInicio.Name = "DataInicio";
            DataInicio.Width = 125;
            // 
            // DataFim
            // 
            DataFim.DataPropertyName = "TarDataFim";
            DataFim.HeaderText = "Data Fim";
            DataFim.MinimumWidth = 6;
            DataFim.Name = "DataFim";
            DataFim.Width = 125;
            // 
            // Status
            // 
            Status.DataPropertyName = "TarStatus";
            Status.HeaderText = "Status";
            Status.MinimumWidth = 6;
            Status.Name = "Status";
            Status.Width = 55;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(comboBoxTarStatus);
            tabPage2.Controls.Add(dateTimePickerDataFinal);
            tabPage2.Controls.Add(dateTimePickerDataInicial);
            tabPage2.Controls.Add(label7);
            tabPage2.Controls.Add(label6);
            tabPage2.Controls.Add(label5);
            tabPage2.Controls.Add(textBoxTarNome);
            tabPage2.Controls.Add(label4);
            tabPage2.Controls.Add(textBoxTarCodigo);
            tabPage2.Controls.Add(label3);
            tabPage2.Location = new System.Drawing.Point(30, 4);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new System.Windows.Forms.Padding(3);
            tabPage2.Size = new System.Drawing.Size(982, 332);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Detalhes";
            tabPage2.UseVisualStyleBackColor = true;
            tabPage2.Click += tabPage2_Click;
            // 
            // comboBoxTarStatus
            // 
            comboBoxTarStatus.FormattingEnabled = true;
            comboBoxTarStatus.Items.AddRange(new object[] { "1-Incluida", "2-Executando", "3-Finalizada", "4-Cancelada" });
            comboBoxTarStatus.Location = new System.Drawing.Point(115, 193);
            comboBoxTarStatus.Name = "comboBoxTarStatus";
            comboBoxTarStatus.Size = new System.Drawing.Size(116, 28);
            comboBoxTarStatus.TabIndex = 13;
            // 
            // dateTimePickerDataFinal
            // 
            dateTimePickerDataFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            dateTimePickerDataFinal.Location = new System.Drawing.Point(115, 147);
            dateTimePickerDataFinal.MinDate = new System.DateTime(2015, 1, 1, 0, 0, 0, 0);
            dateTimePickerDataFinal.Name = "dateTimePickerDataFinal";
            dateTimePickerDataFinal.Size = new System.Drawing.Size(124, 27);
            dateTimePickerDataFinal.TabIndex = 12;
            // 
            // dateTimePickerDataInicial
            // 
            dateTimePickerDataInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            dateTimePickerDataInicial.Location = new System.Drawing.Point(115, 102);
            dateTimePickerDataInicial.MinDate = new System.DateTime(2015, 1, 1, 0, 0, 0, 0);
            dateTimePickerDataInicial.Name = "dateTimePickerDataInicial";
            dateTimePickerDataInicial.Size = new System.Drawing.Size(124, 27);
            dateTimePickerDataInicial.TabIndex = 11;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(28, 196);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(49, 20);
            label7.TabIndex = 9;
            label7.Text = "Status";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(28, 147);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(76, 20);
            label6.TabIndex = 7;
            label6.Text = "Data Final";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(28, 102);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(81, 20);
            label5.TabIndex = 4;
            label5.Text = "Data Início";
            // 
            // textBoxTarNome
            // 
            textBoxTarNome.Location = new System.Drawing.Point(115, 63);
            textBoxTarNome.Name = "textBoxTarNome";
            textBoxTarNome.Size = new System.Drawing.Size(686, 27);
            textBoxTarNome.TabIndex = 3;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(28, 63);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(50, 20);
            label4.TabIndex = 2;
            label4.Text = "Nome";
            // 
            // textBoxTarCodigo
            // 
            textBoxTarCodigo.Location = new System.Drawing.Point(115, 26);
            textBoxTarCodigo.Name = "textBoxTarCodigo";
            textBoxTarCodigo.Size = new System.Drawing.Size(49, 27);
            textBoxTarCodigo.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(29, 26);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(49, 20);
            label3.TabIndex = 0;
            label3.Text = "Tarefa";
            // 
            // FrmTarefa
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1030, 537);
            Controls.Add(tabControl1);
            Controls.Add(panel2);
            Controls.Add(panel1);
            KeyPreview = true;
            Name = "FrmTarefa";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Casdatro de Tarefas";
            KeyDown += FrmTarefa_KeyDown;
            panel1.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel3.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel7.ResumeLayout(false);
            panel6.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button buttonSair;
        private System.Windows.Forms.Button buttonPrimeira;
        private System.Windows.Forms.Button buttonAnterior;
        private System.Windows.Forms.Button buttonUltima;
        private System.Windows.Forms.Button buttonProxima;
        private System.Windows.Forms.TextBox textBoxSkip;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxTake;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonPesquisar;
        private System.Windows.Forms.TextBox textBoxPesquisa;
        private System.Windows.Forms.CheckBox checkBoxOrdem;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button buttonNovo;
        private System.Windows.Forms.Button buttonAlterar;
        private System.Windows.Forms.Button buttonCancelar;
        private System.Windows.Forms.Button buttonSalvar;
        private System.Windows.Forms.Button buttonExcluir;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox textBoxTarCodigo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxTarNome;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dateTimePickerDataInicial;
        private System.Windows.Forms.DateTimePicker dateTimePickerDataFinal;
        private System.Windows.Forms.ComboBox comboBoxTarStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tarefa;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nome;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataInicio;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataFim;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
    }
}