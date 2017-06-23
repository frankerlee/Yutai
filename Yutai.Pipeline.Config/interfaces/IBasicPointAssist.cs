// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  IBasicPointAssist.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/22  16:36
// 更新时间 :  2017/06/22  16:37

using System.Xml;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.Pipeline.Config.Interfaces
{
    public interface IBasicPointAssist
    {
        string TypeName { get; set; }
        IYTField NoField { get; set; }            // 编号字段
        IYTField CodeField { get; set; }          // 要素代码
        IYTField XField { get; set; }             // X坐标
        IYTField YField { get; set; }             // Y坐标
        IYTField ZField { get; set; }             // 地面高程
        IYTField RotangField { get; set; }        // 符号角度
        IYTField RoadCodeField { get; set; }      // 所在道路
        IYTField DCodeField { get; set; }         // 探测单位
        IYTField DDateField { get; set; }         // 探测日期
        IYTField MDateField { get; set; }         // 入库日期
        IYTField RemarkField { get; set; }        // 备注
        void AutoAssembly(IFeatureLayer pLayer);
        void ReadFromXml(XmlNode xml);
        XmlNode ToXml(XmlDocument doc);
    }
}