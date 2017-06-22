using System.Xml;
using ESRI.ArcGIS.Carto;

namespace Yutai.Pipeline.Config.Interfaces
{
    public interface IBasicPipePoint
    {
        string TypeName { get; set; }
        //下面定义管线点的特殊意义属性
        IYTField NoField { get; set; }            // 管点编号字段
        IYTField XField { get; set; }             // X坐标
        IYTField YField { get; set; }             // Y坐标
        IYTField ZField { get; set; }             // 地面高程
        IYTField DepthField { get; set; }         // 井底埋深
        IYTField FeatureField { get; set; }       // 特征字段
        IYTField SubsidField { get; set; }        // 附属物字段
        IYTField PStyleField { get; set; }        // 井盖类型
        IYTField PDSField { get; set; }           // 井盖规格
        IYTField CodeField { get; set; }          // 要素代码
        IYTField MapNoField { get; set; }         // 图幅号
        IYTField UseStatusField { get; set; }     // 使用状态
        IYTField BCodeField { get; set; }         // 权属单位
        IYTField RotangField { get; set; }        // 符号角度
        IYTField RoadCodeField { get; set; }      // 所在道路
        IYTField MDateField { get; set; }         // 入库日期
        IYTField PCJHField { get; set; }          // 偏心井号
        IYTField RemarkField { get; set; }        // 备注

        void AutoAssembly(IFeatureLayer pLayer);
        void ReadFromXml(XmlNode xml);
        XmlNode ToXml();
    }
}
