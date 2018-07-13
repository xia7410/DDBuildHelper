using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DDBuildHelper
{
    public class Mission5
    {

        #region 属性
        static Image<Bgr, byte> tar;
        
        static int timeCount = 0;//防止打包超时
        #endregion

        static bool to6 = false;
        //任务5 打包
        public static void mission()
        {
            to6 = false;
            //按下alt+b
            Thread.Sleep(500);
            Keybd.keybd_event(Keys.Menu, 0, 0, 0);
            Keybd.keybd_event(Keys.B, 0, 0, 0);

            Keybd.keybd_event(Keys.B, 0, 2, 0);
            Keybd.keybd_event(Keys.Menu, 0, 2, 0);

            //检查打包是否完成
            timeCount = 0;
            tar = new Image<Bgr, byte>(Properties.Resources.m6);
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 500;           
            timer.Enabled = true;
            timer.Start();
            timer.Tick += (sen, eve) =>
            {
                double result = Mission.Instance.MatchTemplate(tar);

                if (result > 0.98)
                {
                    //timer.Stop();
                    //timer.Dispose();
                    ((System.Windows.Forms.Timer)sen).Stop();
                    ((System.Windows.Forms.Timer)sen).Dispose();

                    if (to6 == false)
                    {
                        //点击确定按钮
                        Keybd.keybd_event(Keys.Enter, 0, 0, 0);
                        Keybd.keybd_event(Keys.Enter, 0, 2, 0);
                        Mission.Instance.mainform.showLog("打包完成...");
                        Mission6.mission();
                        to6 = true;
                    }
                    else
                    {
                        Mission.Instance.mainform.showLog("这不该发生，已经去6了，还要去干吗？");
                    }

                }
                timeCount++;
                if (timeCount > 60)//超过30秒
                {
                    Mission.Instance.onFaild("m6:打包超时！");
                    return;
                }
            };
        }     
    }
}
