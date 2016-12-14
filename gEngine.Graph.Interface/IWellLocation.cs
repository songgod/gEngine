using static gEngine.Graph.Interface.Enums;

namespace gEngine.Graph.Interface
{
    /*
     * 功能：井位接口
     * 创建时间：2016-11-16
     * 创建人：zhc
     */
    public interface IWellLocation:IObject
    {
        
        //井号
        string WellNum { get; set; }
        //井别（水井、油井）
        WellType WellType { get; set; }
        //井类 
        WellCategory WellCategory { get; set; }
        //坐标系上X值
        double X { get; set; }
        //坐标系上Y值
        double Y { get; set; }
    }

}
