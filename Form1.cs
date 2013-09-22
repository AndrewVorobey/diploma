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
        public static List<formatTimeTable> data;
        public Form1()
        {
            InitializeComponent();
        }


        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                WordFile.ReadFromFile(openFileDialog1.FileName);

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
            err=Sql.connet();
            if (err=="")
                MessageBox.Show("Успешно подключено к БД!");
            else
                MessageBox.Show("Ошибка при подключении. "+err);

        }


    }
}
