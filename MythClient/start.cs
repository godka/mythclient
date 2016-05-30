using System;
using System.Text;
using DevComponents.DotNetBar;
using System.Drawing;
using System.Runtime.InteropServices;
using drizzle;
using System.Xml;
using System.Collections.Generic;
namespace MythClient
{
    public partial class login : DevComponents.DotNetBar.Metro.MetroForm
    {
        private int userid = -1;
        public login()
        {
            InitializeComponent();
        }
        //private void Login()
        private void btnPurgeRows_Click(object sender, EventArgs e)
        {
            Global.serverip = textBoxX2.Text;
            Hide();
            winmain winm = new winmain();
            winm.ShowDialog();
            Close();
        }
        private void login_Load(object sender, EventArgs e)
        {
            textBoxX2.Text = "121.42.136.168";
           // this.mainControl1.Play();
        }
    }
}