using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Диплом
{

    public partial class Form1 : Form
    {
        const int gridSize = 6;
        public Form1()
        {
            InitializeComponent();
            dataGrid.RowCount = gridSize;
            dataGrid.ColumnCount = gridSize;
            for (int i = 1; i < dataGrid.Rows.Count; i++)
                dataGrid.Rows[i].Height = 40;
            for (int i = 1; i < dataGrid.Columns.Count; i++)
                dataGrid.Columns[i].Width = 100;
            dataGrid.Columns[0].Width = 20;

            dataGrid.Rows[0].Cells[1].Value = "Понедельник";
            dataGrid.Rows[0].Cells[1].ReadOnly = true;

            dataGrid.Rows[0].Cells[2].Value = "Вторник";
            dataGrid.Rows[0].Cells[2].ReadOnly = true;

            dataGrid.Rows[0].Cells[3].Value = "Среда";
            dataGrid.Rows[0].Cells[3].ReadOnly = true;

            dataGrid.Rows[0].Cells[4].Value = "четверг";
            dataGrid.Rows[0].Cells[4].ReadOnly = true;

            dataGrid.Rows[0].Cells[5].Value = "пятница";
            dataGrid.Rows[0].Cells[5].ReadOnly = true;

            dataGrid.Rows[1].Cells[0].Value = "1";
            dataGrid.Rows[1].Cells[0].ReadOnly = true;

            dataGrid.Rows[2].Cells[0].Value = "2";
            dataGrid.Rows[2].Cells[0].ReadOnly = true;

            dataGrid.Rows[3].Cells[0].Value = "3";
            dataGrid.Rows[3].Cells[0].ReadOnly = true;

            dataGrid.Rows[4].Cells[0].Value = "4";
            dataGrid.Rows[4].Cells[0].ReadOnly = true;

            dataGrid.Rows[5].Cells[0].Value = "5";
            dataGrid.Rows[5].Cells[0].ReadOnly = true;

        }


        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Names.Items.Clear();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
              //  using (var f = new loadStatus())
               // {
                  //  f.str = openFileDialog1.FileName;
                  //  f.ShowDialog(this);
              //  }
                 WordFile.ReadFromFile(openFileDialog1.FileName);

                 for (int i = 0; i < Data.teacher.Count; i++)
                     Names.Items.Add(Data.teacher[i].name);
            }

        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                WordFile.CreateWordDoc(saveFileDialog1.FileName);
        }

        private void настройкиSQLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var f = new SQL_Settings()) f.ShowDialog(this);
        }

        private void подключитьсяКToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string err;
            err = Sql.connet();
            if (err == "")
                MessageBox.Show("Успешно подключено к БД!");
            else
                MessageBox.Show("Ошибка при подключении. " + err);

        }

        private void Names_SelectedIndexChanged(object sender, EventArgs e)
        {
               int N = Names.SelectedIndex; 
            
            //Clear
            {
                for (int k = 0; k < gridSize; k++)
                    for (int j = 0; j < gridSize; j++)
                        try
                        {
                            dataGrid.Rows[j + 1].Cells[k + 1].Value = "";
                        }
                        catch { }
            }


            if (N >= Data.teacher.Count) return;

            for (int k = 0; k < gridSize; k++)
            {
                dataGrid.Rows[0].Cells[0].Value = Data.teacher[N].name;

                try
                {
                    dataGrid.Columns[k + 1].Width = 70;
                }
                catch { }
                try
                {
                    for (int j = 0; j < gridSize; j++)
                    {
                        dataGrid.Rows[j + 1].Cells[k + 1].Value = "";

                        if (Data.teacher[N].lesson[k, j].exist[0])
                        {
                            if (!Data.teacher[N].lesson[k, j].bothWeek)
                                dataGrid.Rows[j + 1].Cells[k + 1].Value += "1 ";
                            dataGrid.Columns[k + 1].Width = 200;
                            if (Data.teacher[N].lesson[k, j].grup[0] != "")
                                dataGrid.Rows[j + 1].Cells[k + 1].Value += "Гр:" + Data.teacher[N].lesson[k, j].grup[0];
                            if (Data.teacher[N].lesson[k, j].subject[0] != "")
                                dataGrid.Rows[j + 1].Cells[k + 1].Value += " Пр:" + Data.teacher[N].lesson[k, j].subject[0];
                            if (Data.teacher[N].lesson[k, j].roomNomber[0] != "")
                                dataGrid.Rows[j + 1].Cells[k + 1].Value += " Ауд:" + Data.teacher[N].lesson[k, j].roomNomber[0];
                        }

                        if (Data.teacher[N].lesson[k, j].exist[1])
                        {
                            dataGrid.Rows[j + 1].Cells[k + 1].Value += "\n2 ";
                            dataGrid.Columns[k + 1].Width = 200;
                            if (Data.teacher[N].lesson[k, j].grup[1] != "")
                                dataGrid.Rows[j + 1].Cells[k + 1].Value += "Гр:" + Data.teacher[N].lesson[k, j].grup[1];
                            if (Data.teacher[N].lesson[k, j].subject[1] != "")
                                dataGrid.Rows[j + 1].Cells[k + 1].Value += "Пр:" + Data.teacher[N].lesson[k, j].subject[1];
                            if (Data.teacher[N].lesson[k, j].roomNomber[1] != "")
                                dataGrid.Rows[j + 1].Cells[k + 1].Value += "Ауд:" + Data.teacher[N].lesson[k, j].roomNomber[1];
                        }
                    }

                }
                catch
                {
                }
            }
        }





    }
}
