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
        private bool checkLogin(string Name, string Password)
        {
            drizzleXML dxml = new drizzleXML( Global.serverip, Global.serverport);
            drizzleXMLReader tmp = dxml.SendRequest(string.Format(Global.loginrequest,Name,Password));
            var ret = tmp.Parse("UserID");
            if (!ret.Equals(string.Empty))
            {
                userid = int.Parse(ret);
                Global.uid = userid;
                return true;
            }
            else
                return false;
        }
        private void btnPurgeRows_Click(object sender, EventArgs e)
        {
            string Name = textBoxX2.Text;
            string Password = maskedTextBoxAdv1 .Text;
            if (Name == string.Empty && Password == string.Empty)
            {
                MessageBoxEx.Show("用户名与密码不能为空");
                return;
            }
            else if (checkLogin(Name, Password))
            {

                Hide();
                winmain winm = new winmain();
                winm.ShowDialog();
             //   Global.key = Global.ReadKeyIni();
             //   (new frmmain()).ShowDialog();
                Close();
            }
            else
            {
                MessageBoxEx.Show("用户名或密码错误，请重新输入");
                return;
            }
                
            //this.Activate();
        
        }
        private void login_Load(object sender, EventArgs e)
        {
           // this.mainControl1.Play();
        }
    }
}