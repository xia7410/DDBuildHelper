using Emgu.CV;
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
    public class Mission7
    {
        static Image<Bgr, byte> tar;


        //任务7 运行DDBuild
        public static void mission()
        {

            //运行unity
            Keybd.keybd_event(Keys.ControlKey, 0, 0, 0);
            Keybd.keybd_event(Keys.P, 0, 0, 0);
            Keybd.keybd_event(Keys.P, 0, 2, 0);
            Keybd.keybd_event(Keys.ControlKey, 0, 2, 0);

            //检查unity是否已启动          
            waitUnityRun();
        }

        //等待unity启动完成
        private static void waitUnityRun() {
            tar = new Image<Bgr, byte>(Properties.Resources.m7);
            System.Windows.Forms.Timer timerWaitUnityRun = new System.Windows.Forms.Timer();
            timerWaitUnityRun.Interval = 1;
            timerWaitUnityRun.Enabled = true;
            timerWaitUnityRun.Start();
            timerWaitUnityRun.Tick += (sen, eve) =>
            {
                double result = Mission.Instance.MatchTemplate(tar);
                if (result > 0.98)
                {
                    ((System.Windows.Forms.Timer)sen).Stop();
                    ((System.Windows.Forms.Timer)sen).Dispose();
                    //unity 已经启动，点击上传文件按钮
                    MouseControl.Click(new Point(635, 1097));
                    waitUploadFile();
                }
            };
        }

        //等待文件上传完成
        private static void waitUploadFile()
        {
            tar = new Image<Bgr, byte>(Properties.Resources.m7_2);
            System.Windows.Forms.Timer timerWaitUploadFile = new System.Windows.Forms.Timer();
            timerWaitUploadFile.Interval = 1;
            timerWaitUploadFile.Enabled = true;
            timerWaitUploadFile.Start();
            timerWaitUploadFile.Tick += (sen, eve) =>
            {
                double result = Mission.Instance.MatchTemplate(tar);
                if (result > 0.98)
                {
                    ((System.Windows.Forms.Timer)sen).Stop();
                    ((System.Windows.Forms.Timer)sen).Dispose();
                    //文件上传完成
                    Keybd.keybd_event(Keys.Enter, 0, 0, 0);
                    Keybd.keybd_event(Keys.Enter, 0, 2, 0);
                    Thread.Sleep(200);
                    //点击上传数据库按钮
                    MouseControl.Click(new Point(735, 1097));
                    waitUploadDB();
                }
            };
        }

        //检查数据库上传完成
        private static void waitUploadDB()
        {
            tar = new Image<Bgr, byte>(Properties.Resources.m7_3);
            System.Windows.Forms.Timer timerWaitUploadDB = new System.Windows.Forms.Timer();
            timerWaitUploadDB.Interval = 1;
            timerWaitUploadDB.Enabled = true;
            timerWaitUploadDB.Start();
            timerWaitUploadDB.Tick += (sen, eve) =>
            {
                double result = Mission.Instance.MatchTemplate(tar);
                if (result > 0.98)
                {
                    ((System.Windows.Forms.Timer)sen).Stop();
                    ((System.Windows.Forms.Timer)sen).Dispose();
                    //数据库上传完成
                    Keybd.keybd_event(Keys.Enter, 0, 0, 0);
                    Keybd.keybd_event(Keys.Enter, 0, 2, 0);
                    Thread.Sleep(200);

                    //停止运行unity
                    Keybd.keybd_event(Keys.ControlKey, 0, 0, 0);
                    Keybd.keybd_event(Keys.P, 0, 0, 0);
                    Keybd.keybd_event(Keys.P, 0, 2, 0);
                    Keybd.keybd_event(Keys.ControlKey, 0, 2, 0);

                    Mission.Instance.moveNext();
                }
            };
        }


    }
}
