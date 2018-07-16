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
    public class Mission2
    {
        //任务2 下载fbx文件
        public static void mission()
        {
            string url = AppConst.WebUrl + "/Windows/resourcesbase/fbx/" + Mission.Instance.buildModel.Filename;
            string code = Mission.Instance.buildModel.Filename.Substring(0, Mission.Instance.buildModel.Filename.IndexOf('.'));
            string path = AppConst.ddBuildResourcesPath + code + @"_MX1.res\" + Mission.Instance.buildModel.Filename;
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
                    Thread.Sleep(1000);
                    MouseControl.Click(AppConst.focuspos2);
                    Mission.Instance.mainform.showLog("下载fbx完成...");
                    Mission3.mission();
                }
            });


        }

    }
}
