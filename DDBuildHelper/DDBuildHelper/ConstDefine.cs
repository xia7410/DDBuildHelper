using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDBuildTool
{
   public class ConstDefine
    {

        //static string ip = "192.168.1.101";
        static string ip = "123.207.73.94";
        public static string SocketUrl = ip;
        public static string WebUrl = "http://" + SocketUrl + ":7799/";
        public static string WinPicPath = "";

        //技能槽的位置
        //public static Point[] skillSlotPosition = new Point[] { new Point(), new Point(1030, 160), new Point(1030, 220), new Point(1030, 300), new Point(1030, 360), new Point(1030, 420), new Point(1030, 500), new Point(1030, 560) };

      


        #region 参数
        public const int GameCaptureRefreshRate = 500;//捕捉的刷新率，500毫秒
        public const int GW = 1066;
        public const int GH = 600;
        #endregion

    }
}
