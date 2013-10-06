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
    //Создать большой .doc
    //Курсив внести в отдельный BOOL
    //
    public partial class Form1 : Form
    {
        const int gridSize = 5;
        Point lastTableClick = new Point(0, 0);

        public Form1()
        {
            InitializeComponent();
            dataGrid.RowCount = gridSize;
            dataGrid.ColumnCount = gridSize;
            for (int i = 0; i < dataGrid.Rows.Count; i++)
                dataGrid.Rows[i].Height = 40;
            for (int i = 0; i < dataGrid.Columns.Count; i++)
                dataGrid.Columns[i].Width = 187;

            for (int i = 0; i < dataGrid.Rows.Count; i++)
            {
                dataGrid.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }

            dataGrid.Columns[0].HeaderText = "название столбца";



            dataGrid.Columns[0].HeaderText = "Понедельник";
            dataGrid.Columns[1].HeaderText = "Вторник";
            dataGrid.Columns[2].HeaderText = "Среда";
            dataGrid.Columns[3].HeaderText = "четверг";
            dataGrid.Columns[4].HeaderText = "пятница";

            WritteAll.Enabled = WritteOne.Enabled = ReadFromSql.Enabled = false;
            ClearGrid();
           // ToSave.Enabled = false;
        }


        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Names.Items.Clear();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                WordFile.ReadFromFile(openFileDialog1.FileName);
                for (int i = 0; i < Data.teacher.Count; i++)
                    Names.Items.Add(Data.teacher[i].name);
            }
            setShowDatatMass();

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
            {
                MessageBox.Show("Успешно подключено к БД!");
                WritteAll.Enabled = WritteOne.Enabled = ReadFromSql.Enabled = true;
            }
            else
                MessageBox.Show("Ошибка при подключении. " + err);

        }

        private void Names_SelectedIndexChanged(object sender, EventArgs e)
        {
            int N = Data.selectNomber = Names.SelectedIndex;
            if (N >= Data.teacher.Count) MessageBox.Show("Не корректно задан номер.");// return;
            else
            setGrid(N);

        }

        private void setGrid(int N)
        {
            ClearGrid();
            for (int k = 0; k < gridSize; k++)
                for (int j = 0; j < gridSize; j++)
                    for (int exI = 0; exI < 2; exI++)//четные-не четные недели
                        if (Data.teacher[N].lesson[k, j].exist[exI])
                        {
                            if (!Data.teacher[N].lesson[k, j].bothWeek)
                                dataGrid.Rows[j].Cells[k].Value += ""+(exI+1)+" ";
                            if (Data.teacher[N].lesson[k, j].group[exI] != "")
                                dataGrid.Rows[j].Cells[k].Value += "Гр:" + Data.teacher[N].lesson[k, j].group[exI];
                            if (Data.teacher[N].lesson[k, j].subject[exI] != "")
                                dataGrid.Rows[j].Cells[k].Value += " Пр:" + Data.teacher[N].lesson[k, j].subject[exI];
                            if (Data.teacher[N].lesson[k, j].roomNomber[exI] != "")
                                dataGrid.Rows[j].Cells[k].Value += " Ауд:" + Data.teacher[N].lesson[k, j].roomNomber[exI];
                            dataGrid.Rows[j].Cells[k].Value += "\n";
                        }
        }

        private void ClearGrid()
        {
            for (int k = 0; k < gridSize; k++)
                for (int j = 0; j < gridSize; j++)
                    dataGrid.Rows[j].Cells[k].Value = " ";
        }

        private void ToSql_Click(object sender, EventArgs e)
        {
            if (Names.Items.Count == 0) return;
            int N = Names.SelectedIndex;
            Sql.SetSchedule(N);
        }

        private void FromSql_Click(object sender, EventArgs e)
        {
            Sql.read();
            Names.Items.Clear();
            for (int i = 0; i < Data.teacher.Count; i++)
                Names.Items.Add(Data.teacher[i].name);
            setShowDatatMass();

        }

        private void ShowDataMass_SelectedIndexChanged(object sender, EventArgs e)
        {
            Names.Items.Clear();
            DataMass.setData(ShowDataMass.SelectedIndex);
            for (int i = 0; i < Data.teacher.Count; i++)
                Names.Items.Add(Data.teacher[i].name);
            Names.SelectedIndex =  0;
        }
        private void setShowDatatMass()
        {
            ShowDataMass.Items.Clear();
            for (int i = 0; i < DataMass.massName.Count; i++)
                ShowDataMass.Items.Add(DataMass.massName[i]);
            ShowDataMass.SelectedIndex = ShowDataMass.Items.Count - 1;
        }

        private void DeletData_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Данные будут потеряны. Удалить?", "удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                DataMass.pop(ShowDataMass.SelectedIndex);
            }

            setShowDatatMass();
        }

        private void записатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Names.Items.Count == 0) return;
            int N = Names.SelectedIndex;
            Sql.SetSchedule(N);
        }

        private void ReadFromSql_Click(object sender, EventArgs e)
        {

            Sql.read();
            Names.Items.Clear();
            for (int i = 0; i < Data.teacher.Count; i++)
                Names.Items.Add(Data.teacher[i].name);
            setShowDatatMass();
        }

        private void WritteAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Data.teacher.Count; i++)
                Sql.SetSchedule(i);
        }







    }
}
