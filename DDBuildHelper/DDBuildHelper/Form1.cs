using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DDBuildHelper
{
    public partial class Form1 : Form
    {

        Image<Bgr, byte> tar;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int x = (2100);
            int y = (700);
            this.StartPosition = FormStartPosition.Manual;
            this.Location = (Point)new Size(x, y);

            //开启截屏
            GameCapture.Instance.init(this.Location, this.Size);
            GameCapture.Instance.start();
            tar = new Image<Bgr, byte>(DDBuildHelper.Properties.Resources.tar);
            //开启助手服务器
            ServerForUnity.Instance.Start();
        }


        //展示日志
        int maxLog = 10;
        public void showLog(string content, Image<Bgr, byte> img = null) {
            EventItem item = new EventItem(content, img);
            this.flowLayoutPanel1.Controls.Add(item);
            this.flowLayoutPanel1.Controls.SetChildIndex(item, 0);

            if (this.flowLayoutPanel1.Controls.Count > maxLog)
            {
                this.flowLayoutPanel1.Controls[this.flowLayoutPanel1.Controls.Count - 1].Dispose();
            }
            this.flowLayoutPanel1.VerticalScroll.Value = 0;
            this.flowLayoutPanel1.VerticalScroll.Value = 0;
            Refresh();
        }

        public void clearLog() {
            this.flowLayoutPanel1.Controls.Clear();
        }

        private void buttonclear_Click(object sender, EventArgs e)
        {
            clearLog();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {           
            DDBuildHelper.Mission.Instance.start(this);          
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

    }


}
