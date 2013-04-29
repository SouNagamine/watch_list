using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using watch.WatchLib;

namespace watch_list
{
    public partial class Form1 : Form
    {
        private ConnectServer server = new ConnectServer();
        public Form1()
        {
            InitializeComponent();
            server.picturebox = this.pictureBox1;
            server.startServer();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            server.kill();
           
        }
    }
}
