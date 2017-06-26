using System.Collections.Generic;
using System.Xml;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.Pipeline.Config.Interfaces
{
    public interface IBasicLayerInfo
    {
        string AutoNames { get; set; }
        string FixAutoNames { get; }
        string Name { get; set; }
        string AliasName { get; set; }
        bool Visible { get; set; }
        enumPipelineDataType DataType { get; set; }
        enumPipelineHeightType HeightType { get;set; }
        //! 用来判断图层是否为合法的对应管线图层
        string ValidateKeys { get; set; }
        List<IYTField> Fields { get; set; }
        string TemplateName { get; set; }

        void LoadTemplate(IPipelineTemplate template);
        void ReadFromXml(XmlNode xml);

        void ReadFromXml(XmlNode xml, IPipelineTemplate template);
        XmlNode ToXml(XmlDocument doc);

        //! 在和Map关联后设置对应的图层
        IFeatureClass FeatureClass { get; set; }

        IYTField GetField(string typeWord);

        string GetFieldName(string typeWord);

        string EsriClassName { get; set; }
        IBasicLayerInfo Clone(bool keepClass);

        string EsriShortName { get; }
    }
}