// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  IFunctionLayer.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/27  15:37
// 更新时间 :  2017/06/27  15:38

using System.Collections.Generic;
using System.Xml;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.Pipeline.Config.Interfaces
{
    public interface IFunctionLayer
    {
        string Name { get; set; }
        string AliasName { get; set; }
        bool Visible { get; set; }
        string AutoNames { get; set; }
        string FixAutoNames { get; }
        enumFunctionLayerType FunctionType { get; set; }
        List<IYTField> Fields { get; set; }
        IFeatureClass FeatureClass { get; set; }
        string EsriShortName { get; }
        string EsriClassName { get; set; }
        string ValidateKeys { get; set; }
        void ReadFromXml(XmlNode xmlNode);
        XmlNode ToXml(XmlDocument doc);
        IYTField GetField(string typeWord);
        string GetFieldName(string typeWord);
        IFunctionLayer Clone(bool keepClass);
    }
}