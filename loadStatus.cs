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
    public partial class loadStatus : Form
    {
        public static  loadStatus progress;
        public string str;
        public loadStatus()
        {
            progress=this;
            InitializeComponent();
        }
     public void StatusBarPlass(int N, int Max)
        {
            progressBar.PerformStep();
         }

     private void loadStatus_Load(object sender, EventArgs e)
     {

           
         WordFile.ReadFromFile(str);
     }
    
    }
}
