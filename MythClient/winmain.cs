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
            if (Global.uid == -1)
            {
                MessageBoxEx.Show("UserID is Null","Error");
            }
            else
            {
                drizzle.drizzleXML xml = new drizzle.drizzleXML(Global.serverip, Global.serverport);
                var ret = xml.SendRequest(string.Format(Global.levelrequest, Global.uid));
                if (ret.success)
                {
                    do
                    {
                        TopLevel tmp = new TopLevel();
                        tmp.topGroupID = MythTryPrase(ret.Parse("topGroupID"));
                        tmp.topGroupName = ret.Parse("topGroupName");
                        tmp.topGroupParent = MythTryPrase(ret.Parse("topGroupParent"));
                        TopLevelList.Add(tmp);
                    }
                    while (ret.moveNext());
                }
                else
                {

                    ret = xml.SendRequest(string.Format(Global.tripplesteprequest, Global.uid));
                    if (ret.success)
                    {
                        do
                        {
                            Camera tmp = new Camera();
                            tmp.groupname = ret.Parse("GROUPNAME");
                            tmp.usercameraid = MythTryPrase(ret.Parse("UserCameraID"));
                            tmp.name = ret.Parse("Name");
                            tmp.groupid = MythTryPrase(ret.Parse("GroupID"));
                            tmp.IP = ret.Parse("MULTICASTIP");
                            tmp.CameraID = MythTryPrase(ret.Parse("CameraID"));
                            tmp.ptzcontrol = MythTryPrase(ret.Parse("PTZControl"));
                            if (!lmainkey.ContainsKey(tmp.groupid))
                            {
                                ListMainKey tmplist = new ListMainKey(tmp.groupid,tmp.groupname);
                                lmainkey[tmp.groupid] = tmplist;
                            }
                            lmainkey[tmp.groupid].Clist.Add(tmp);
                            //   CameraList.Add(tmp);
                        }
                        while (ret.moveNext());
                    }
                }
                foreach (ListMainKey t in lmainkey.Values)
                {

                    if (t.Key == 0)
                    {
                        foreach (Camera c in t.Clist)
                        {
                            TreeNode tmpnode = new TreeNode();
                            tmpnode.Text = c.name + " " + c.CameraID.ToString();
                            tmpnode.Tag = c;
                            treeView1.Nodes.Add(tmpnode);
                        }
                    }
                    else
                    {
                        TreeNode node = new TreeNode();
                        node.Text = t.ListName;
                        foreach (Camera c in t.Clist)
                        {
                            TreeNode tmpnode = new TreeNode();
                            tmpnode.Text = c.name + " " + c.CameraID.ToString();
                            tmpnode.Tag = c;
                            node.Nodes.Add(tmpnode);
                        }
                        treeView1.Nodes.Add(node);
                    }
                }
            }

        }

        private void treeView1_Click(object sender, EventArgs e)
        {

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.ByMouse)
            {
                if (e.Node.Nodes.Count == 0)
                {
                    if (isplaying)
                        this.mainControl1.stop();
                    Camera t = (Camera)e.Node.Tag;
                    this.mainControl1.IP = t.IP;
                    this.mainControl1.Port = 5834;
                    this.mainControl1.Cameraid = t.CameraID;
                    this.mainControl1.Play();
                    isplaying = true;
                }
            }
        }
    }
}