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
    public partial class SQL_Settings : Form
    {
        public SQL_Settings()
        {
            InitializeComponent();
            Save.Enabled = false;
        }

        private void OkDecable(object sender, EventArgs e)
        {
            if (SqlIp.Text != "" && SqlName.Text != "" && SqlUserPass.Text != "" && SqlUserName.Text != "")
                Save.Enabled = true;
            else
                Save.Enabled = false;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            Sql.settings.setSettings(SqlIp.Text, SqlName.Text,SqlUserName.Text, SqlUserPass.Text);
            this.Close();
        }

    }
}
