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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DDBuildHelper
{
    public partial class Form1 : Form
    {

        #region 属性
        Image<Bgr, byte> tar;
        public SynchronizationContext m_SyncContext = null;
        #endregion

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
            m_SyncContext = SynchronizationContext.Current;

            //开启截屏
            GameCapture.Instance.init(this.Location, this.Size);
            GameCapture.Instance.start();
            tar = new Image<Bgr, byte>(DDBuildHelper.Properties.Resources.tar);
            //开启助手服务器
            ServerForUnity.Instance.Start();
        }


        public struct LogMode
        {
            public string content;
            public Image<Bgr, byte> img;
        }

        public void showLogSafePost(string content, Image<Bgr, byte> img = null)
        {
            LogMode logMode = new LogMode();
            logMode.content = content;
            logMode.img = img;
            m_SyncContext.Post(showLog, logMode);
        }

        //展示日志
        int maxLog = 10;
        public void showLog(object state) {
            LogMode logMode = (LogMode)state;

            EventItem item = new EventItem(logMode.content, logMode.img);
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



        public void clearLogSafePost()
        {
            m_SyncContext.Post(clearLog, null);
        }

        public void clearLog(object state) {
            this.flowLayoutPanel1.Controls.Clear();
        }

        private void buttonclear_Click(object sender, EventArgs e)
        {
            clearLogSafePost();
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
