// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  IPipelineLayer.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/22  17:32
// 更新时间 :  2017/06/22  17:32

using System.Collections.Generic;
using System.Xml;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.Pipeline.Config.Interfaces
{
    public interface IPipelineLayer
    {
        string Name { get; set; }
        string Code { get; set; }

        string AutoNames { get; set; }
        string FixAutoNames { get; }

        List<IBasicLayerInfo> Layers { get;set; }
        void ReadFromXml(XmlNode xml);
        XmlNode ToXml(XmlDocument doc);
        bool OrganizeFeatureClass(IFeatureClass featureClass);
        IPipelineLayer NewOrganizeFeatureClass(IFeatureClass featureClass);

        IPipelineLayer Clone(bool keepClass);
        IWorkspace Workspace { get; set; }

    }
}