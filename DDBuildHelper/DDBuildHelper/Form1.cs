﻿using Emgu.CV;
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
        Image<Bgr, byte> game;


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





        private void button1_Click(object sender, EventArgs e)
        {
            double result = MatchTemplate();
            if (result > 0.98)
            {
                Debug.Print("目标检测的结果： " + result);
                MouseControl.Click(new Point(725 + 10, 24 + 15));
            }
            else
            {
                Debug.Print("目标检测的结果： " + result);
            }
          
        }

        double MatchTemplate()
        {
            game = GameCapture.Instance.game;
            Image<Gray, float> result = new Image<Gray, float>(game.Width, game.Height);
            result = game.MatchTemplate(tar, TemplateMatchingType.CcorrNormed);
            double min = 0;
            double max = 0;
            Point maxp = new Point(0, 0);
            Point minp = new Point(0, 0);
            CvInvoke.MinMaxLoc(result, ref min, ref max, ref minp, ref maxp);
            //展示截图          
            CvInvoke.Rectangle(game, new Rectangle(new Point(maxp.X, maxp.Y), new Size(200, 200)), new MCvScalar(0, 255, 255), 2);
            this.imageBox1.Image = game;
            Debug.Print(maxp.X + " " + maxp.Y);
            return max;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string url = AppConst.WebUrl + "/Windows/resourcesbase/000.fbx";
            HttpReqHelper.downloadFile(url, @"D:\000.fbx", delegate (string err)
            {
                MessageBox.Show("下载完了");
            });
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MouseControl.Drag(new Point(333,133),new Vector2(1,1),100);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DDBuildHelper.Mission.Instance.start();
        }

      
    }


}
