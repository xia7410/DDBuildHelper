using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DDBuildHelper
{
    public class Mission4
    {
        static Image<Bgr, byte> tar;
      

        //任务4 截图
        public static void mission()
        {
            //进行截图
            Thread.Sleep(500);
            Image<Bgr, byte> game;
            game = GameCapture.Instance.game;
            Image<Bgr, byte> preImg = game.Copy(new Rectangle( new Point (900,168), new Size (600,600)));
            //保存截图
            string code = Mission.Instance.buildModel.Filename.Substring(0, Mission.Instance.buildModel.Filename.IndexOf('.'));
            string path = AppConst.ddBuildResourcesPath + code + @"_FA1pre.bundle\pre.jpg";
            preImg.ToBitmap().Save(path);
            //删掉hierarchy面板中的model
            MouseControl.Click(new Point(Mission.Instance.MatchTemplatePosition.X + 5, Mission.Instance.MatchTemplatePosition.Y + 5));
            Keybd.keybd_event(Keys.Delete, 0, 0, 0);
            Keybd.keybd_event(Keys.Delete, 0, 2, 0);
            Thread.Sleep(200);
            //切换焦点
            MouseControl.Click(AppConst.focuspos1);
            Thread.Sleep(1000);
            MouseControl.Click(AppConst.focuspos2);
            Thread.Sleep(3000);
            //点击_pre.bundle文件夹
            MouseControl.Click(new Point(Mission.Instance.ProjectPosition.X, Mission.Instance.ProjectPosition.Y + 202));
            Thread.Sleep(500);
            //点击pre图片，随后进行设置
            tar = new Image<Bgr, byte>(DDBuildHelper.Properties.Resources.m5);//找到预览图位置
            double result = Mission.Instance.MatchTemplate(tar);
            if (result > 0.98)
            {
                Debug.Print("目标检测的结果： " + result);
                MouseControl.Click(new Point(Mission.Instance.MatchTemplatePosition.X, Mission.Instance.MatchTemplatePosition.Y + 20));
                //进行图片设置
                setting();
            }
            else
            {
                Debug.Print("目标检测的结果： " + result);
             //   MessageBox.Show("m5:未找到pre图片" + result);
                Mission.Instance.onFaild("m5:未找到pre图片" + result);
                return;
            }
        }

        //进行图片设置
        static void setting() {
            Thread.Sleep(300);
            //点击textureType
            MouseControl.Click(new Point(2300,147));
            Thread.Sleep(300);
            //点击advanced
            MouseControl.Click(new Point(2300, 346));
            Thread.Sleep(300);
            //点击Read/Write Enabled
            MouseControl.Click(new Point(2283, 237));
            Thread.Sleep(300);
            //点击format
            MouseControl.Click(new Point(2300, 550));
            Thread.Sleep(300);
            //点击RGBA 32bit
            MouseControl.Click(new Point(2300, 814));
            Thread.Sleep(300);
            //点击apply
            MouseControl.Click(new Point(2533, 579));
            Mission.Instance.mainform.showLog("预览图完成...");
            Mission5.mission();
        }

    }
}
