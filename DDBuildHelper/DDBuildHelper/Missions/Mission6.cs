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
    public class Mission6
    {

        #region 属性
        static Image<Bgr, byte> tar;
        static System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        static int timeCount = 0;//防止打包超时
        #endregion


        //任务6 打包
        public static void mission()
        {
            //按下alt+b
            Thread.Sleep(500);
            Keybd.keybd_event(Keys.Menu, 0, 0, 0);
            Keybd.keybd_event(Keys.B, 0, 0, 0);

            Keybd.keybd_event(Keys.B, 0, 2, 0);
            Keybd.keybd_event(Keys.Menu, 0, 2, 0);

            //检查打包是否完成
            timeCount = 0;
            tar = new Image<Bgr, byte>(Properties.Resources.m6);          
            timer.Interval = 500;
            timer.Tick += new EventHandler(Timer_TimesUp);
            timer.Enabled = true;
            timer.Start();
        }

      
        private static void Timer_TimesUp(object sender, EventArgs e)
        {
           
            double result = Mission.Instance.MatchTemplate(tar);

            if (result > 0.98)
            {
                timer.Stop();
                timer.Dispose();
                //点击确定按钮
                Keybd.keybd_event(Keys.Enter, 0, 0, 0);
                Keybd.keybd_event(Keys.Enter, 0, 2, 0);

                Mission.Instance.moveNext();
            }
            timeCount++;
            if (timeCount>60)//超过30秒
            {
                Mission.Instance.onFaild("m6:打包超时！");
                return;
            }
        }

     
    }
}
