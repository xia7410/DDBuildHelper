
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
            string code = Mission.Instance.buildModel.FileName.Substring(0,Mission.Instance.buildModel.FileName.IndexOf('.'));
            System.IO.Directory.CreateDirectory(AppConst.ddBuildResourcesPath+ code + "_MX1.bundle");
            System.IO.Directory.CreateDirectory(AppConst.ddBuildResourcesPath + code + "_MX1.res");
            System.IO.Directory.CreateDirectory(AppConst.ddBuildResourcesPath + code + "_FA1pre.bundle");
            Mission.Instance.moveNext();
        }

      
    }
}
