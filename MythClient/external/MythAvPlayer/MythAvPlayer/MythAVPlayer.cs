using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
namespace MythAvPlayer
{
    public class MythAVPlayer
    {
        /*
            HBAPI void* WINAPI Myth_ZiyaDecoder_Create(char* ip,int port,int cameraid);
            HBAPI void* WINAPI Myth_Live555Decoder_Create(char* rtsp,char* username,char* password);
            HBAPI void* WINAPI Myth_H264VideoPlayer_Create(void* decoder,void* handle);
            HBAPI int WINAPI Myth_H264VideoPlayer_Start(void* videoplayer,int isthread);
            HBAPI int WINAPI Myth_H264VideoPlayer_Stop(void* videoplayer);
            HBAPI int WINAPI Myth_VideoPlayer_SetText(void* decoder,char* text,int x,int y);
            HBAPI int WINAPI Myth_VideoPlayer_Capture(void* videoplayer);
         */
        [DllImport("cyclops.dll")]
        private extern static int Myth_ZiyaDecoder_Create(char[] ip, int port, int cameraid);
        [DllImport("cyclops.dll")]
        private extern static int Myth_Live555Decoder_Create(char[] rtsp, char[] username, char[] password);
        [DllImport("cyclops.dll")]
        private extern static int Myth_H264VideoPlayer_Create(int decoder, IntPtr handle);
        [DllImport("cyclops.dll")]
        private extern static int Myth_H264VideoPlayer_Start(int videoplayer, int isthread);
        [DllImport("cyclops.dll")]
        private extern static int Myth_H264VideoPlayer_Stop(int videoplayer);
        [DllImport("cyclops.dll")]
        private extern static int Myth_VideoPlayer_Capture(int videoplayer);
        [DllImport("cyclops.dll")]
        private extern static int Myth_VideoPlayer_SetText(int videoplayer, char[] text, int x, int y);
        [DllImport("cyclops.dll")]
        private extern static int Myth_VideoPlayer_SetAlpha(int videoplayer, int alpha);
        
        private int decoder;
        private int player;
        private IntPtr MHandle;
        private int m_alpha = 0;
        public MythAVPlayer(IntPtr handle,string ip, int port, int cameraid)
        {
           // StringBuilder mip = new StringBuilder(ip);
            MHandle = handle;
            decoder = Myth_ZiyaDecoder_Create(ConvertToCppChar(ip), port, cameraid);
            player = Myth_H264VideoPlayer_Create(decoder, MHandle);
        }
        public MythAVPlayer(IntPtr handle,string rtsp, string username, string password)
        {
            MHandle = handle;
            decoder = Myth_Live555Decoder_Create(ConvertToCppChar(rtsp), ConvertToCppChar(username), ConvertToCppChar(password));
            player = Myth_H264VideoPlayer_Create(decoder, MHandle);
        }
        private void playcore(object state)
        {
            Myth_H264VideoPlayer_Start(player, 1);
        }

        public int SetAlpha(int alpha){
            m_alpha = alpha;
            return Myth_VideoPlayer_SetAlpha(player,alpha);
        }

        public void Play()
        {
            ThreadPool.QueueUserWorkItem(playcore);
        }

        public void Stop()
        {
            Myth_H264VideoPlayer_Stop(player);
            
            //thread.Join(100);
        }
        public void capture()
        {
            Myth_VideoPlayer_Capture(player);
        }
        public void SetText(string text, int x, int y)
        {
            Myth_VideoPlayer_SetText(player, ConvertToCppChar(text), x, y);
        }

        public static char[] ConvertToCppChar(string str)
        {
            if (str == null) { return null; }
            char[] tmpchar = str.ToCharArray();
            byte[] buf = System.Text.Encoding.Default.GetBytes(tmpchar);
            char[] aC = System.Text.Encoding.Default.GetChars(buf);
            return aC;
        }
    }
}
