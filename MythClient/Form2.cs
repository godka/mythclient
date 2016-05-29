using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MythClient
{
    public partial class Form2 : DevComponents.DotNetBar.Metro.MetroForm
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.avContainer1.Count = 3;
            //this.avContainer1.setcount();
        }
    }
}
