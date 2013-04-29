using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace watch.WatchLib
{
    public class ConnectServer
    {
        // リスナースレッド
        private Thread th;
        private TcpListener server;
        private TcpClient client;
        public String remote_IP = "127.0.0.1";
        public Int32 remote_Port = 2010;
        public PictureBox picturebox;
        private bool thread_status = true;

        public void startServer()
        {
            th = new Thread(DataListener);
            th.IsBackground = true;
            th.Start();

        }

        /// <summary>
        /// 強制終了
        /// </summary>
        public void  kill() {
            if (client != null && client.Connected == true)
            {
                client.Close();
                
            }
           
            thread_status = false;
            th.Abort();
           
        }

        // リスナースレッド
        private void DataListener()
        {
            try
            {
                
                server = new TcpListener(IPAddress.Parse(remote_IP), remote_Port);

                
                server.Start();

                client = server.AcceptTcpClient();
                while (thread_status == true)
                {



                   

                    NetworkStream ns = client.GetStream();
                    // データ読み出し
                    byte[] data = new byte[10000000];
                    int len = ns.Read(data, 0, data.Length);
                   // string s = System.Text.Encoding.UTF8.GetString(data, 0, len);
                    picturebox.Image = ConvertBytesToImage(data);
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "Server error.");
            }
        }

        public static System.Drawing.Bitmap ConvertBytesToImage(byte[] Image_Bytes)
        {
            MemoryStream ms = new MemoryStream(Image_Bytes);
            Bitmap bmp = new Bitmap(ms);
            ms.Close();
            return bmp;
        }

    }


}
