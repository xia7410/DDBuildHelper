using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace DDBuildHelper
{

    public struct BuildModel {
        public string Username;
        public int Lib;
        public string Filename;
        public string Name;
        public int Maintype;
        public int Sell;
        public double Price;
        public double Length;
        public double Width;
        public double Height;
        public string Tags;
    }


    public class Mission
    {
        #region 属性
        public Point ProjectPosition = new Point();//Project的位置，此位置很重要，后续多个操作都依赖此相对位置，所以保存下来。
        public BuildModel buildModel;
        Emgu.CV.Image<Bgr, byte> game;
        public Point MatchTemplatePosition;//模板匹配到的位置
        public Form1 mainform;
        Thread th;
        #endregion

        #region 单例
        private static Mission instance;
        public static Mission Instance {
            get {
                if (instance == null)
                {
                    instance = new Mission();
                }
                return instance;
            }
        }
        #endregion

        public void start(Form1 form)
        {
            mainform = form;
            //询问任务
            ask();
        }

       
        void ask() {
            HttpReqHelper.requestSync(AppConst.WebUrl + "buildHelper?protocol=1", delegate (string res)
            {
                try
                {
                    buildModel = Coding<BuildModel>.decode(res);
                    mainform.clearLogSafePost();
                    mainform.showLogSafePost("开始执行任务:" + buildModel.Filename + "   " + buildModel.Name);
                    startMissionSafePost();
                }
                catch (Exception err)
                {
                    mainform.showLogSafePost(res+"  "+err.ToString());
                    mainform.showLogSafePost("暂无任务...");
                    Thread.Sleep(3000);
                    ask();
                }
            });
        }


        //运行任务1
        void startMissionSafePost()
        {
            mainform.m_SyncContext.Post(startMission, null);
        }
        void startMission(object state)
        {
            Mission1.mission();
        }

        //任务完成
        public void missionOK()
        {
            mainform.showLog("任务完成，重新询问任务...");
            //通知服务器完成任务
            string code = buildModel.Filename.Substring(0, buildModel.Filename.IndexOf('.'));
            HttpReqHelper.requestSync(AppConst.WebUrl + "buildHelper?protocol=2&fbx="+ code, delegate (string res)
            {
                Thread.Sleep(3000);
                // MessageBox.Show("任务完成，重新询问任务");
                ask();
            });

           
        }

        public double MatchTemplate(Emgu.CV.Image<Bgr, byte> tar)
        {
            game = GameCapture.Instance.game;
            Emgu.CV.Image<Gray, float> result = new Emgu.CV.Image<Gray, float>(game.Width, game.Height);
            result = game.MatchTemplate(tar, TemplateMatchingType.CcorrNormed);
            double min = 0;
            double max = 0;
            Point maxp = new Point(0, 0);
            Point minp = new Point(0, 0);
            Emgu.CV.CvInvoke.MinMaxLoc(result, ref min, ref max, ref minp, ref maxp);
            ////展示截图          
            //CvInvoke.Rectangle(game, new Rectangle(new Point(maxp.X, maxp.Y), new Size(200, 200)), new MCvScalar(0, 255, 255), 2);
            //this.imageBox1.Image = game;
            //Debug.Print(maxp.X + " " + maxp.Y);
            MatchTemplatePosition = maxp;
            if (Mission.Instance.ProjectPosition.X == 0 && Mission.Instance.ProjectPosition.Y == 0)
            {
                Mission.Instance.ProjectPosition = maxp;
            }
            return max;
        }

        //当运行失败后调用
        public void onFaild(string content) {

            //排除unity的弹出框
            MouseControl.Click(AppConst.focuspos2);
            Keybd.keybd_event(Keys.Enter, 0, 0, 0);
            Keybd.keybd_event(Keys.Enter, 0, 2, 0);
            Keybd.keybd_event(Keys.Enter, 0, 0, 0);
            Keybd.keybd_event(Keys.Enter, 0, 2, 0);
            //输出日志
            mainform.showLog("出错!3秒后重启:" + buildModel.Filename + "    " + buildModel.Name + "   " + content);
            //写入log文件
            TxtLog.Log("出错任务：" + buildModel.Filename + "    " + buildModel.Name +"   "+ content);
            //重新轮询任务
            Thread.Sleep(3000);
            ask();
        }
    }
}
