// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  IBasicPipeLine.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/22  15:40
// 更新时间 :  2017/06/22  15:40

using System.Xml;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.Pipeline.Config.Interfaces
{
    public interface IBasicPipeLine
    {
        string TypeName { get; set; }
        string HeightTypeName { get; set; }
        enumPipelineHeightType HeightType { get; set; }
        IYTField NoField { get; set; }            // 编号字段
        IYTField SPointField { get; set; }        // 起点点号
        IYTField EPointField { get; set; }        // 终点点号
        IYTField SDeepField { get; set; }         // 起点埋深
        IYTField EDeepField { get; set; }         // 终点埋深
        IYTField SHField { get; set; }            // 起点高程
        IYTField EHField { get; set; }            // 终点高程
        IYTField CodeField { get; set; }          // 要素代码
        IYTField MaterialField { get; set; }      // 材质
        IYTField DTypeField { get; set; }         // 埋设方式
        IYTField LineStyleField { get; set; }     // 线形
        IYTField DSField { get; set; }            // 管径
        IYTField SectionSizeField { get; set; }   // 断面尺寸
        IYTField PDMField { get; set; }           // 套管
        IYTField PipeNatureField { get; set; }    // 管线性质
        IYTField MSRQField { get; set; }          // 埋设日期
        IYTField MDateField { get; set; }         // 入库日期
        IYTField UseStatusField { get; set; }     // 使用状态
        IYTField BCodeField { get; set; }         // 权属单位
        IYTField RoadCodeField { get; set; }      // 所在道路
        IYTField CabCountField { get; set; }      // 条数
        IYTField VolPresField { get; set; }       // 压力值
        IYTField HoleCountField { get; set; }     // 总孔数
        IYTField HoleUsedField { get; set; }      // 已用孔数
        IYTField FlowDField { get; set; }         // 流向
        IYTField RemarkField { get; set; }        // 备注
        void AutoAssembly(IFeatureLayer pLayer);
        void ReadFromXml(XmlNode xml);
        XmlNode ToXml(XmlDocument doc);
    }
}