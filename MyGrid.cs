using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Диплом
{
    public class MyDataGridView : DataGridView
    {
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (Data.teacher != null)
                {
                    EditEntry newForm = new EditEntry(this.CurrentCell.RowIndex, this.CurrentCell.ColumnIndex);
                    newForm.Show();
                }
                int i=this.CurrentCell.RowIndex;
            }

            base.OnKeyDown(e);
        }
    }

}