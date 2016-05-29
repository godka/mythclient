using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace MythAvPlayer
{
    public partial class MainControl : UserControl
    {
        public string IP
        {
            get;
            set;
        }
        public int Port
        {
            get;
            set;
        }
        public int Cameraid
        {
            get;
            set;
        }
        public string Text
        {
            get;
            set;
        }
        MythAvPlayer.MythAVPlayer avplayer = null;
        public void Play()
        {
            avplayer = new MythAVPlayer(this.Handle, this.IP, this.Port, this.Cameraid);
            avplayer.Play();
            //avplayer.SetText("我们这边没有奶妈", 5, 5);
        }
        public void capture()
        {
            avplayer.capture();
        }
        public void stop()
        {
            avplayer.Stop();
        }
        public MainControl()
        {
            InitializeComponent();
        }

        ~MainControl()
        {
            if(avplayer!=null)
                avplayer.Stop();
        }
        private void MainControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (avplayer != null)
                this.avplayer.SetAlpha(30);
        }

        private void MainControl_MouseLeave(object sender, EventArgs e)
        {

            if (avplayer != null)
                this.avplayer.SetAlpha(0);
        }

        private void MainControl_Load(object sender, EventArgs e)
        {

        }

    }
}
