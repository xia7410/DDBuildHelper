using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class AppConst
{

    static string ip = "192.168.133.50";
    //static string ip = "123.207.73.94";
    public static string WebUrl = "http://" + ip + ":6699/";


    public static string ddBuildResourcesPath = @"C:\Users\Administrator\Desktop\DDBuild\DDBuild\Assets\ResourcesAssetBundle\ResourcesBase\furniture\";



    public static Point focuspos1 = new Point(800,1417);

    public static Point focuspos2 = new Point(800,1355);
    //技能槽的位置
    //public static Point[] skillSlotPosition = new Point[] { new Point(), new Point(1030, 160), new Point(1030, 220), new Point(1030, 300), new Point(1030, 360), new Point(1030, 420), new Point(1030, 500), new Point(1030, 560) };




    #region 参数
    public const int GameCaptureRefreshRate = 200;//捕捉的刷新率，500毫秒
                                                  //public const int GW = 1066;
                                                  //public const int GH = 600;
    #endregion

}
