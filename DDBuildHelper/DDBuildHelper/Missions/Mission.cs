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
using System.Windows.Forms;

namespace DDBuildHelper
{

    public struct BuildModel {
        public string Username;
        public int lib;
        public string FileName;
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
        int missionIndex = 0;
        public Point ProjectPosition = new Point();//Project的位置，此位置很重要，后续多个操作都依赖此相对位置，所以保存下来。
        public BuildModel buildModel;


        Emgu.CV.Image<Bgr, byte> game;
        public Point MatchTemplatePosition;//模板匹配到的位置



        public Form1 mainform;



        Thread requestMissionTh;      
        bool getMission = false;//是否获取了任务
        #endregion


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

        public void start(Form1 form)
        {
            mainform = form;
          
            ////询问任务
            //while (true)
            //{
            //    Thread.Sleep(2000);
            //    HttpReqHelper.requestSync(AppConst.WebUrl + "getmission", delegate (string res)
            //    {
            //        try
            //        {
            //            mission = Coding<MissionModel>.decode(res);
            //            moveNext();
            //            getMission = true ;
            //        }
            //        catch 
            //        {
            //            Debug.Print("未得到任务：" + res);
            //        }
            //    });
            //    if (getMission)
            //    {
            //        break;
            //    }
            //}
            missionIndex = 0;
            buildModel = new BuildModel();
            buildModel.Username = "55555";
            buildModel.lib = 2;
            buildModel.FileName = "000.fbx";
            buildModel.Name = "测试商品";
            buildModel.Maintype = 199;
            buildModel.Sell = 0;
            buildModel.Price = 999;
            buildModel.Length = 12;
            buildModel.Width = 13;
            buildModel.Height = 14;
            buildModel.Tags = "自助上传";


            mainform.clearLog();
            mainform.showLog("开始执行任务:"+ buildModel.FileName+ "   "+ buildModel.Name);

            moveNext();
        }
    



      


        

        public void moveNext() {
            missionIndex++;
            switch (missionIndex)
            {
                case 1:
                    mainform.showLog("清理目录");
                    Mission1.mission();                   
                    break;
                case 2:
                    mainform.showLog("建立目录");
                    Mission2.mission();
                    break;
                case 3:
                    mainform.showLog("下载文件");
                    Mission3.mission();
                    break;
                case 4:
                    mainform.showLog("拖拽预制体");
                    Mission4.mission();
                    break;
                case 5:
                    mainform.showLog("截图");
                    Mission5.mission();
                    break;
                case 6:
                    mainform.showLog("打包");
                    Mission6.mission();
                    break;
                case 7:
                    mainform.showLog("上传");
                    Mission7.mission();
                    break;
                default:
                    break;
            }
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
            mainform.showLog("出错!!!!!!:" + buildModel.FileName + "    " + buildModel.Name + "   " + content);
            //写入log文件
            TxtLog.Log("出错任务：" + buildModel.FileName + "    " + buildModel.Name +"   "+ content);
            //重新轮询任务

        }



    }
}
