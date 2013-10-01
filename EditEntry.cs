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
    public partial class EditEntry : Form
    {
        public int a, b;
        public EditEntry()
        {
            InitializeComponent();
        }
        public EditEntry(int A, int B)
        {
            InitializeComponent();
            a = A; b = B;

            even.Checked = Data.teacher[Data.selectNomber].lesson[b, a].bothWeek;

            hook1.Checked = Data.teacher[Data.selectNomber].lesson[b, a].exist[0];
            Lection.Checked = Data.teacher[Data.selectNomber].lesson[b, a].lection[0];
            subject.Text = Data.teacher[Data.selectNomber].lesson[b, a].subject[0];
            roomNomber.Text = Data.teacher[Data.selectNomber].lesson[b, a].roomNomber[0];
            group.Text = Data.teacher[Data.selectNomber].lesson[b, a].group[0];
            chekGroup.Text = Lesson.getLectionGroup(group.Text) + " ?";

            hook2.Checked = Data.teacher[Data.selectNomber].lesson[b, a].exist[1];
            Lection1.Checked = Data.teacher[Data.selectNomber].lesson[b, a].lection[1];
            subject1.Text = Data.teacher[Data.selectNomber].lesson[b, a].subject[1];
            roomNomber1.Text = Data.teacher[Data.selectNomber].lesson[b, a].roomNomber[1];
            group1.Text = Data.teacher[Data.selectNomber].lesson[b, a].group[1];
            chekGroup1.Text = Lesson.getLectionGroup(group.Text) + " ?";


        }

        private void EditEntry_Load(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            chekGroup.Text = Lesson.getLectionGroup(group.Text) + " ?";
            chek1();
        }

        private void roomNomber_TextChanged(object sender, EventArgs e)
        {
            chek1();
        }

        private void subject_TextChanged(object sender, EventArgs e)
        {
            chek1();
        }
        private void chek1()
        {
            hook1.Checked = true;
            if (Lesson.getRoomNomber(roomNomber.Text).Length < 4)
            {
                roomNomber.BackColor = System.Drawing.Color.Red;
                hook1.Checked = false;
            }
            else
            {
                roomNomber.BackColor = System.Drawing.Color.White;
                roomNomber.Text = Lesson.getRoomNomber(roomNomber.Text);
            }

        }





        private void subject1_TextChanged(object sender, EventArgs e)
        {
            chek2();
        }

        private void roomNomber1_TextChanged(object sender, EventArgs e)
        {
            chek2();
        }

        private void group1_TextChanged(object sender, EventArgs e)
        {
            chekGroup1.Text = Lesson.getLectionGroup(group1.Text) + " ?";
            chek2();

        }
        private void chek2()
        {
            hook2.Checked = true;
            if (Lesson.getRoomNomber(roomNomber1.Text).Length < 4)
            {
                roomNomber1.BackColor = System.Drawing.Color.Red;
                hook2.Checked = false;
            }
            else
            {
                roomNomber1.BackColor = System.Drawing.Color.White;
                roomNomber1.Text = Lesson.getRoomNomber(roomNomber1.Text);
            }
        }

        private void even_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2.Visible = !even.Checked;
        }

        private void Save_Click(object sender, EventArgs e)
        {

            Data.teacher[Data.selectNomber].lesson[b, a].bothWeek = even.Checked;
            if (Lection.Checked)
            {
                Data.teacher[Data.selectNomber].lesson[b, a].lection[0] = Lection.Checked;
                Data.teacher[Data.selectNomber].lesson[b, a].exist[0] = hook1.Checked;
                Data.teacher[Data.selectNomber].lesson[b, a].subject[0] = subject.Text;
                Data.teacher[Data.selectNomber].lesson[b, a].roomNomber[0] = roomNomber.Text;
                Data.teacher[Data.selectNomber].lesson[b, a].group[0] = group.Text;
            }


            if (Lection1.Checked)
            {
                Data.teacher[Data.selectNomber].lesson[b, a].lection[1] = Lection1.Checked;
                Data.teacher[Data.selectNomber].lesson[b, a].exist[1] = hook2.Checked;
                Data.teacher[Data.selectNomber].lesson[b, a].subject[1] = subject1.Text;
                Data.teacher[Data.selectNomber].lesson[b, a].roomNomber[1] = roomNomber1.Text;
                Data.teacher[Data.selectNomber].lesson[b, a].group[1] = group1.Text;
            }
            this.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {


            DialogResult result = MessageBox.Show("Данные будут потеряны. Удалить?", "удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }


    }
}
