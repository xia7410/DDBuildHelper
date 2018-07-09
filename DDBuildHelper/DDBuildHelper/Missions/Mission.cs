using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DDBuildHelper
{

    public struct MissionModel {
        public string fileName;

    }


    public class Mission
    {
        #region 属性
        int missionIndex = 0;
        Thread requestMissionTh;
       public MissionModel mission;
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

        public void start()
        {
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
            mission = new MissionModel();
            mission.fileName = "000.fbx";
            moveNext();
        }
    



      


        

        public void moveNext() {
            missionIndex++;
            switch (missionIndex)
            {
                case 1:
                    Mission1.mission();
                    break;
                case 2:
                    Mission2.mission();
                    break;
                case 3:
                    Mission3.mission();
                    break;
                case 4:
                    Mission4.mission();
                    break;
                default:
                    break;
            }
        }






    }
}
