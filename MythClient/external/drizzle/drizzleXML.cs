using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
namespace drizzle
{
    public class drizzleXML
    {
        private string mip;
        private int mport;
        private drizzleTCP.Client client;
        public drizzleXML(string ip, int port)
        {
            mip = ip;
            mport = port;
        }
        private void connect()
        {
            client = new drizzleTCP.Client(mip, mport);
            //client.ClientEventArrive += client_ClientEventArrive;
        }
        public List<int> SendIDRequest()
        {
            List<int> ret = new List<int>();
            connect();
            string str = "GET /list  HTTP/1.0\r\n\r\n";
            string retstr = client.sendmessageNoAsync(str);
            var spstr = retstr.Split(':');
            if (spstr.Length > 1)
            {
                var tmpret = spstr[1].Split(';');
                foreach (var t in tmpret)
                {
                    int intt;
                    if (int.TryParse(t, out intt))
                    {
                        ret.Add(intt);
                    }
                }
            }
            return ret;
        }
        public drizzleXMLReader SendRequest(string sqlrequest)
        {
            connect();
            //string tmp = sqlrequest.Replace(" ", "%20");
            string str = "GET /scripts/dbnet.dll?param=<XML><function>SQL_SELECT</function><Content>";
            string decodeSQL = sqlrequest.ToUpper().Replace(" ", "%20");
            string setstr = str + decodeSQL + "</Content></XML>  HTTP/1.0\r\n\r\n";
            string retstr = client.sendmessageNoAsync(setstr);
            return new drizzleXMLReader(retstr);
            
        }
    }
}
