using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace School_management_system
{
    public partial class RecodesForm : Form
    {
        public RecodesForm()
        {
            InitializeComponent();
        }

        private void btn_close_MouseEnter(object sender, EventArgs e)
        {
            btn_close.BackColor = Color.Red;
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
