using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace MythAvPlayer
{
    public partial class AvContainer : UserControl
    {
        private int mcount;
        public int Count
        {
            get
            {
                return mcount;
            }
            set
            {
                mcount = value;
                setcount(); 
            }
        }
        public AvContainer()
        {
            InitializeComponent();
            //Count = 1;
        }
        public void setcount()
        {
          //  this.tableLayoutPanel1.ColumnCount = mcount;
           // this.tableLayoutPanel1.RowCount = mcount;
            int k = 0;
            int mwidth = (this.Width - 5 * (mcount - 1)) / mcount;
            int mheight = (this.Height - 5 * (mcount - 1)) / mcount;
            //int mwidth = 29;
            //int mheight = 20;
            for (int i = 0; i < mcount; i++)
            {
                for (int j = 0; j < mcount; j++)
                {
                    var but = new MainControl();
                    but.Name = "control" + k.ToString();
                    but.Location = new Point((mwidth + 5) * i, (mheight + 5) * j);
                    but.Width = mwidth;
                    but.Height = mheight;
                    but.Enabled = true;
                    this.Controls.Add(but);
                }
            }
        }

        private void AvContainer_Load(object sender, EventArgs e)
        {
            Count = 1;
        }
    }
}
