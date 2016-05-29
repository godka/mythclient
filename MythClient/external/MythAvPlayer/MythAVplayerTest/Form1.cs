using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MythAVplayerTest
{
    public partial class Form1 : Form
    {
        bool mustclose = false;
        public Form1()
        {
            InitializeComponent();
            this.FormClosed += new FormClosedEventHandler(Form1_FormClosed);
        }

        void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(mustclose)
                this.mainControl1.stop();
                    //throw new NotImplementedException();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.mainControl1.ip = "114.215.207.53";
            this.mainControl1.port = 5834;
            this.mainControl1.cameraid = 1683;
            this.mainControl1.Play();
            mustclose = true;
            //string ip = "120.204.70.218";
            //byte[] bytes = Encoding.Default.GetBytes(ip);
            //string byteip = Encoding.ASCII.GetString(bytes);
           
            /*
            this.mainControl2.ip = "120.204.70.218";
            this.mainControl2.port = 5834;
            this.mainControl2.cameraid = 1001;
            this.mainControl2.Play();

            this.mainControl3.ip = "120.204.70.218";
            this.mainControl3.port = 5834;
            this.mainControl3.cameraid = 1002;
            this.mainControl3.Play();

            this.mainControl4.ip = "120.204.70.218";
            this.mainControl4.port = 5834;
            this.mainControl4.cameraid = 1035;
            this.mainControl4.Play();
             * */
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.mainControl1.capture();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
