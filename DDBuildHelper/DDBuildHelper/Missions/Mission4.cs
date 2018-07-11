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
        static int waitUnityLoadFbx = 5000;//等待unity读取fbx的时间。
     


        //任务4 拖拽预制体
        public static void mission()
        {

            Thread.Sleep(waitUnityLoadFbx);

            //点选.res文件夹
            tar = new Image<Bgr, byte>(DDBuildHelper.Properties.Resources.m4);//先找到project位置
            double result =Mission.Instance. MatchTemplate(tar);
            if (result > 0.98)
            {
                Debug.Print("目标检测的结果： " + result);
                MouseControl.Click(new Point(30, Mission.Instance.MatchTemplatePosition.Y + 250));//点选.res文件夹
                MouseControl.Click(new Point(30, Mission.Instance.MatchTemplatePosition.Y + 250));//点选.res文件夹  这里必须点两下！！！              
            }
            else
            {
                Debug.Print("目标检测的结果： " + result);
                MessageBox.Show("m4:未找到.res文件夹" + result);
            }
            //进行拖拽，找到拖拽点
            Thread.Sleep(200);
            MouseControl.Click(new Point(Mission.Instance.ProjectPosition.X, Mission.Instance.ProjectPosition.Y-100)); //拖拽之前先点击一下hierarchy面板
            tar = new Image<Bgr, byte>(DDBuildHelper.Properties.Resources.m4_2);
            Thread.Sleep(500);
            result = Mission.Instance.MatchTemplate(tar);
            if (result > 0.98)
            {
                Debug.Print("目标检测的结果： " + result);
                MouseControl.Drag(new Point(Mission.Instance.MatchTemplatePosition.X + 20, Mission.Instance.MatchTemplatePosition.Y + 5), new Vector2(-1, -1), 120, onDragedToHierarchy);
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
            double result = Mission.Instance.MatchTemplate(tar);
            if (result > 0.98)
            {
                Debug.Print("目标检测的结果： " + result);
                MouseControl.Drag2(new Vector2(Mission.Instance.MatchTemplatePosition.X+5, Mission.Instance.MatchTemplatePosition.Y+5), new Vector2(Mission.Instance.ProjectPosition.X, Mission.Instance.ProjectPosition.Y + 235), ondragedToBundleFolder);
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

    }
}
