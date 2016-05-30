using System;
using System.Text;
using DevComponents.DotNetBar;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Windows.Forms;
namespace MythClient
{
    public partial class winmain : DevComponents.DotNetBar.Metro.MetroForm
    {
        public class Camera
        {
            public int CameraID;// = ret.Parse("CameraID");
            public int ptzcontrol;// = ret.Parse("PTZControl");
            public int usercameraid;// = ret.Parse("UserCameraID");
            public int groupid;// = ret.Parse("GroupID");
            public string groupname;//= ret.Parse("GROUPNAME");
            public string name;// = ret.Parse("Name");
            public string IP;// = ret.Parse("MULTICASTIP");
        }
        private class TopLevel
        {
            public int topGroupID;// = ret.Parse("CameraID");
            public string topGroupName;//= ret.Parse("GROUPNAME");
            public int topGroupParent;// = ret.Parse("Name");
        }
        public bool isplaying = false;
        private List<Camera> CameraList = new List<Camera>();
        private List<TopLevel> TopLevelList = new List<TopLevel>();
        public class ListMainKey
        {
            public string ListName;
            public int Key;
            public List<Camera> Clist = new List<Camera>();
            public ListMainKey(int key,string lname)
            {
                Key = key;
                Clist = new List<Camera>();
                ListName = lname;
            }
        }
        public Dictionary<int, ListMainKey> lmainkey = new Dictionary<int, ListMainKey>();
        public winmain()
        {
            InitializeComponent();
        }
        private int MythTryPrase(string str)
        {
            int t;
            if (int.TryParse(str, out t))
            {
                return t;
            }
            else
            {
                return 0;
            }
        }
        private void winmain_Load(object sender, EventArgs e)
        {

        }
        private void AutoCheckList()
        {
            drizzle.drizzleXML xml = new drizzle.drizzleXML(Global.serverip, Global.serverport);
            var ret = xml.SendIDRequest();
            foreach (var t in ret)
            {
                if (!listBox1.Items.Contains(t.ToString()))
                {
                    listBox1.Items.Add(t.ToString());
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            AutoCheckList();
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            if (isplaying)
                this.mainControl1.stop();
            //this.mainControl1.IP = t.IP;
            this.mainControl1.IP = Global.serverip;
            this.mainControl1.Port = Global.serverport;
            //this.mainControl1.Cameraid = t.CameraID;
            this.mainControl1.Cameraid = int.Parse(listBox1.Items[index].ToString());
            this.mainControl1.Play();
            isplaying = true;
        }
    }
}