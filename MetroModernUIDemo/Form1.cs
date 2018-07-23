using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MetroModernUIDemo
{
    public partial class Form1 : MetroForm
    {
        public Form1()
        {
            InitializeComponent();
            this.multiHeaderDataGridView1.TopRow = new MultiHeaderDataGridView.DataGridViewRowTopRow(this.multiHeaderDataGridView1);
            this.multiHeaderDataGridView1.TopRow.Cells[1].HeaderText = "测试";
            this.multiHeaderDataGridView1.TopRow.Cells[2].ColumnIndex = -1;
            this.multiHeaderDataGridView1.TopRow.Cells[1].ColumnSpan = 2;
            this.multiHeaderDataGridView1.TopRow.Cells[2].HeaderText = "测试";
            this.multiHeaderDataGridView1.TopRow.Cells[2].ColumnIndex = 1;
            this.multiHeaderDataGridView1.TopRow.Cells[2].ColumnSpan = 2;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MetroMessageBox.Show(this, "测试", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);

            for (int i = 0; i < 30; i++)
            {
                this.multiHeaderDataGridView1.Rows.Add(i.ToString(), i.ToString(), i.ToString());
            }
        }
    }
}
