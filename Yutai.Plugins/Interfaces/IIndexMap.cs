using System.Collections.Generic;
using System.Xml;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.Plugins.Interfaces
{
    public interface IIndexMap
    {
        string Name { get; set; }
        string WorkspaceName { get; set; }
        string IndexLayerName { get; set; }
        string TemplateName { get; set; }
        string SearchFields { get; set; }
        string NameField { get; set; }
        void ReadFromXml(XmlNode xml);
        XmlNode SaveToXml(XmlDocument doc);

        IFeatureCursor Search(string searchKey);
        List<IPrintPageInfo> QueryPageInfo(IGeometry searchGeometry, string searchKeys);
        List<IPrintPageInfo> QueryPageInfo(IGeometry searchGeometry);
        List<IPrintPageInfo> QueryPageInfo(string searchKeys);
    }
}