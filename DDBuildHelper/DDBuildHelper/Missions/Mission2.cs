
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DDBuildHelper
{
   public class Mission2
    {



        //任务2 建立打包目录
        public static void mission()
        {
            string code = Mission.Instance.mission.fileName.Substring(0,Mission.Instance.mission.fileName.IndexOf('.'));
            System.IO.Directory.CreateDirectory(AppConst.ddBuildResourcesPath+ code + "_MX1.bundle");
            System.IO.Directory.CreateDirectory(AppConst.ddBuildResourcesPath + code + "_MX1.res");
            System.IO.Directory.CreateDirectory(AppConst.ddBuildResourcesPath + code + "_FA1_pre.bundle");
            ////切换焦点
            //MouseControl.Click(AppConst.focuspos1);
            //Thread.Sleep(200);
            //MouseControl.Click(AppConst.focuspos2);
            Mission.Instance.moveNext();
        }

      
    }
}
