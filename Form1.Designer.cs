namespace Диплом
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToSave = new System.Windows.Forms.ToolStripMenuItem();
            this.управлениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.подключитьсяКToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ReadFromSql = new System.Windows.Forms.ToolStripMenuItem();
            this.WritteOne = new System.Windows.Forms.ToolStripMenuItem();
            this.WritteAll = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкиSQLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.Names = new System.Windows.Forms.ComboBox();
            this.ShowDataMass = new System.Windows.Forms.ComboBox();
            this.DeletData = new System.Windows.Forms.Button();
            this.dataGrid = new Диплом.MyDataGridView();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.управлениеToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1029, 24);
            this.menuStrip1.TabIndex = 3;
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.открытьToolStripMenuItem,
            this.ToSave});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.открытьToolStripMenuItem.Text = "Открыть";
            this.открытьToolStripMenuItem.Click += new System.EventHandler(this.открытьToolStripMenuItem_Click);
            // 
            // ToSave
            // 
            this.ToSave.Name = "ToSave";
            this.ToSave.Size = new System.Drawing.Size(152, 22);
            this.ToSave.Text = "Сохранить";
            this.ToSave.Click += new System.EventHandler(this.сохранитьToolStripMenuItem_Click);
            // 
            // управлениеToolStripMenuItem
            // 
            this.управлениеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.подключитьсяКToolStripMenuItem,
            this.ReadFromSql,
            this.WritteOne,
            this.WritteAll,
            this.настройкиSQLToolStripMenuItem});
            this.управлениеToolStripMenuItem.Name = "управлениеToolStripMenuItem";
            this.управлениеToolStripMenuItem.Size = new System.Drawing.Size(34, 20);
            this.управлениеToolStripMenuItem.Text = "БД";
            // 
            // подключитьсяКToolStripMenuItem
            // 
            this.подключитьсяКToolStripMenuItem.Name = "подключитьсяКToolStripMenuItem";
            this.подключитьсяКToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.подключитьсяКToolStripMenuItem.Text = "Подключиться";
            this.подключитьсяКToolStripMenuItem.Click += new System.EventHandler(this.подключитьсяКToolStripMenuItem_Click);
            // 
            // ReadFromSql
            // 
            this.ReadFromSql.Name = "ReadFromSql";
            this.ReadFromSql.Size = new System.Drawing.Size(166, 22);
            this.ReadFromSql.Text = "Считать";
            this.ReadFromSql.Click += new System.EventHandler(this.ReadFromSql_Click);
            // 
            // WritteOne
            // 
            this.WritteOne.Name = "WritteOne";
            this.WritteOne.Size = new System.Drawing.Size(166, 22);
            this.WritteOne.Text = "Записать одного";
            this.WritteOne.Click += new System.EventHandler(this.записатьToolStripMenuItem_Click);
            // 
            // WritteAll
            // 
            this.WritteAll.Name = "WritteAll";
            this.WritteAll.Size = new System.Drawing.Size(166, 22);
            this.WritteAll.Text = "Записать всех";
            this.WritteAll.Click += new System.EventHandler(this.WritteAll_Click);
            // 
            // настройкиSQLToolStripMenuItem
            // 
            this.настройкиSQLToolStripMenuItem.Name = "настройкиSQLToolStripMenuItem";
            this.настройкиSQLToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.настройкиSQLToolStripMenuItem.Text = "Настройки БД";
            this.настройкиSQLToolStripMenuItem.Click += new System.EventHandler(this.настройкиSQLToolStripMenuItem_Click);
            // 
            // Names
            // 
            this.Names.FormattingEnabled = true;
            this.Names.Location = new System.Drawing.Point(139, 111);
            this.Names.Name = "Names";
            this.Names.Size = new System.Drawing.Size(121, 21);
            this.Names.TabIndex = 7;
            this.Names.SelectedIndexChanged += new System.EventHandler(this.Names_SelectedIndexChanged);
            // 
            // ShowDataMass
            // 
            this.ShowDataMass.FormattingEnabled = true;
            this.ShowDataMass.Location = new System.Drawing.Point(12, 111);
            this.ShowDataMass.Name = "ShowDataMass";
            this.ShowDataMass.Size = new System.Drawing.Size(121, 21);
            this.ShowDataMass.TabIndex = 10;
            this.ShowDataMass.SelectedIndexChanged += new System.EventHandler(this.ShowDataMass_SelectedIndexChanged);
            // 
            // DeletData
            // 
            this.DeletData.Location = new System.Drawing.Point(12, 82);
            this.DeletData.Name = "DeletData";
            this.DeletData.Size = new System.Drawing.Size(75, 23);
            this.DeletData.TabIndex = 11;
            this.DeletData.Text = "Закрыть файл";
            this.DeletData.UseVisualStyleBackColor = true;
            this.DeletData.Click += new System.EventHandler(this.DeletData_Click);
            // 
            // dataGrid
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Silver;
            this.dataGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dataGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dataGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGrid.GridColor = System.Drawing.SystemColors.HotTrack;
            this.dataGrid.Location = new System.Drawing.Point(12, 148);
            this.dataGrid.MultiSelect = false;
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.ReadOnly = true;
            this.dataGrid.Size = new System.Drawing.Size(994, 248);
            this.dataGrid.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 408);
            this.Controls.Add(this.DeletData);
            this.Controls.Add(this.ShowDataMass);
            this.Controls.Add(this.Names);
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem управлениеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem настройкиSQLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem подключитьсяКToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToSave;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private MyDataGridView dataGrid;
        private System.Windows.Forms.ComboBox Names;
        private System.Windows.Forms.ComboBox ShowDataMass;
        private System.Windows.Forms.Button DeletData;
        private System.Windows.Forms.ToolStripMenuItem ReadFromSql;
        private System.Windows.Forms.ToolStripMenuItem WritteAll;
        private System.Windows.Forms.ToolStripMenuItem WritteOne;
    }
}

