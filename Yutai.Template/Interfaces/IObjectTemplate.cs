// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  IPipelineTemplate.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/22  17:33
// 更新时间 :  2017/06/22  17:33

using System.Collections.Generic;
using System.Xml;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.Plugins.Template.Interfaces
{
    public interface IObjectTemplate
    {
        int ID { get; set; }
        string Name { get; set; }

        string BaseName { get; set; }

        string AliasName { get; set; }

        string DatasetName { get; set; }

        esriFeatureType FeatureType { get; set; }

        string GeometryTypeName { get; set; }

        esriGeometryType GeometryType { get; set; }
        
        List<IYTField> Fields { get; set; }
        void ReadFromXml(XmlNode xmlNode);
        XmlNode ToXml(XmlDocument doc);

        void UpdateRow(IRow pRow);
    }
}