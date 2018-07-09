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
        static Image<Bgr, byte> game;
        static int waitUnityLoadFbx = 3000;//等待unity读取fbx的时间。
        static Point MatchTemplatePosition;//模板匹配到的位置
        static Point ProjectPosition = new Point();//Project的位置，此位置很重要，后续多个操作都依赖此相对位置，所以保存下来。


        //任务4 拖拽预制体
        public static void mission()
        {

            Thread.Sleep(waitUnityLoadFbx);

            //点选.res文件夹
            tar = new Image<Bgr, byte>(DDBuildHelper.Properties.Resources.m4);
            double result = MatchTemplate();
            if (result > 0.98)
            {
                Debug.Print("目标检测的结果： " + result);
                MouseControl.Click(new Point(30, MatchTemplatePosition.Y + 250));//点选.res文件夹
                MouseControl.Click(new Point(30, MatchTemplatePosition.Y + 250));//点选.res文件夹  这里必须点两下！！！              
            }
            else
            {
                Debug.Print("目标检测的结果： " + result);
                MessageBox.Show("m4:未找到.res文件夹" + result);
            }
            //进行拖拽，找到拖拽点
            Thread.Sleep(200);
            MouseControl.Click(new Point(ProjectPosition.X, ProjectPosition.Y-100)); //拖拽之前先点击一下hierarchy面板
            tar = new Image<Bgr, byte>(DDBuildHelper.Properties.Resources.m4_2);
            Thread.Sleep(500);
            result = MatchTemplate();
            if (result > 0.98)
            {
                Debug.Print("目标检测的结果： " + result);
                MouseControl.Drag(new Point(MatchTemplatePosition.X + 20, MatchTemplatePosition.Y + 5), new Vector2(-1, -1), 120, onDragedToHierarchy);
            }
            else
            {
                Debug.Print("目标检测的结果： " + result);
                MessageBox.Show("m4:未找到待拖拽物体" + result);
            }
        }
        //拖拽至Hierarchy面板后调用
        static void onDragedToHierarchy() {
            //为预制体改名字
            Thread.Sleep(1000);
            Keybd.keybd_event(Keys.F2, 0, 0, 0);
            Keybd.keybd_event(Keys.F2, 0, 2, 0);
            Thread.Sleep(200);
            Keybd.keybd_event(Keys.M, 0, 0, 0);
            Keybd.keybd_event(Keys.M, 0, 2, 0);
            Thread.Sleep(200);
            Keybd.keybd_event(Keys.O, 0, 0, 0);
            Keybd.keybd_event(Keys.O, 0, 2, 0);
            Thread.Sleep(200);
            Keybd.keybd_event(Keys.D, 0, 0, 0);
            Keybd.keybd_event(Keys.D, 0, 2, 0);
            Thread.Sleep(200);
            Keybd.keybd_event(Keys.E, 0, 0, 0);
            Keybd.keybd_event(Keys.E, 0, 2, 0);
            Thread.Sleep(200);
            Keybd.keybd_event(Keys.L, 0, 0, 0);
            Keybd.keybd_event(Keys.L, 0, 2, 0);
            Thread.Sleep(200);
            Keybd.keybd_event(Keys.Enter, 0, 0, 0);
            Keybd.keybd_event(Keys.Enter, 0, 2, 0);
            dragToBundleFolder();
            
        }

        //将预制体拖拽到.bundle文件夹中去
        static void dragToBundleFolder() {
            Thread.Sleep(500);
            tar = new Image<Bgr, byte>(DDBuildHelper.Properties.Resources.m4_3);
            double result = MatchTemplate();
            if (result > 0.98)
            {
                Debug.Print("目标检测的结果： " + result);
                MouseControl.Drag2(new Vector2(MatchTemplatePosition.X+5, MatchTemplatePosition.Y+5), new Vector2(ProjectPosition.X, ProjectPosition.Y + 235), ondragedToBundleFolder);
            }
            else
            {
                Debug.Print("目标检测的结果： " + result);
                MessageBox.Show("m4:回拽时错误：" + result);
            }            
        }

        //将预制体拖拽到.bundle文件夹后调用
        static void ondragedToBundleFolder() {
            Mission.Instance.moveNext();
        }

        static double MatchTemplate()
        {
            game = GameCapture.Instance.game;
            Image<Gray, float> result = new Image<Gray, float>(game.Width, game.Height);
            result = game.MatchTemplate(tar, TemplateMatchingType.CcorrNormed);
            double min = 0;
            double max = 0;
            Point maxp = new Point(0, 0);
            Point minp = new Point(0, 0);
            CvInvoke.MinMaxLoc(result, ref min, ref max, ref minp, ref maxp);
            ////展示截图          
            //CvInvoke.Rectangle(game, new Rectangle(new Point(maxp.X, maxp.Y), new Size(200, 200)), new MCvScalar(0, 255, 255), 2);
            //this.imageBox1.Image = game;
            //Debug.Print(maxp.X + " " + maxp.Y);
            MatchTemplatePosition = maxp;
            if (ProjectPosition.X==0 && ProjectPosition.Y==0)
            {
                ProjectPosition = maxp;
            }
            return max;
        }
    }
}
