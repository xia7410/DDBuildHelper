﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DDBuildHelper
{
    public class Mission3
    {
        //任务3 下载fbx文件
        public static void mission()
        {
            string url = AppConst.WebUrl + "/Windows/resourcesbase/" + Mission.Instance.buildModel.FileName;
            string code = Mission.Instance.buildModel.FileName.Substring(0, Mission.Instance.buildModel.FileName.IndexOf('.'));
            string path = AppConst.ddBuildResourcesPath + code + @"_MX1.res\" + Mission.Instance.buildModel.FileName;
            HttpReqHelper.downloadFile(url, path, delegate (string err)
            {
                if (err != null)
                {
                   // MessageBox.Show("下载FBX出错" + err);
                    Mission.Instance.onFaild("m3下载FBX出错" + err);
                    return;
                }
                else
                {
                    //切换焦点
                    MouseControl.Click(AppConst.focuspos1);
                    Thread.Sleep(200);
                    MouseControl.Click(AppConst.focuspos2);                    
                    Mission.Instance.moveNext();
                }
            });


        }

    }
}
