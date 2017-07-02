using System.Xml;

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
    }
}