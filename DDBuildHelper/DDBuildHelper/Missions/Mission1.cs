using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDBuildHelper
{
    public class Mission1
    {

        //任务1 清理furniture文件夹 并 建立新文件夹
        public static void mission() {
            //清理furniture文件夹
            DelectDir(AppConst.ddBuildResourcesPath);
            //建立新文件夹
            string code = Mission.Instance.buildModel.Filename.Substring(0, Mission.Instance.buildModel.Filename.IndexOf('.'));
            System.IO.Directory.CreateDirectory(AppConst.ddBuildResourcesPath + code + "_MX1.bundle");
            System.IO.Directory.CreateDirectory(AppConst.ddBuildResourcesPath + code + "_MX1.res");
            System.IO.Directory.CreateDirectory(AppConst.ddBuildResourcesPath + code + "_FA1pre.bundle");
            Mission.Instance.mainform.showLog("目录完成...");
            Mission2.mission();
        }


        //删除文件夹下所有文件
        public static void DelectDir(string srcPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)            //判断是否文件夹
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true);          //删除子目录和文件
                    }
                    else
                    {
                        File.Delete(i.FullName);      //删除指定文件
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
