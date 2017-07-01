using System.Xml;
using Yutai.Pipeline.Config.Interfaces;

namespace Yutai.Pipeline.Config.Concretes
{
    public class CommonConfig : ICommonConfig
    {
        private string _name;
        private string _value;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public CommonConfig()
        {
        }

        public CommonConfig(string name, string value)
        {
            _name = name;
            _value = value;
        }

        public CommonConfig(XmlNode node)
        {
            ReadFromXml(node);
        }

        public void ReadFromXml(XmlNode xml)
        {
            if (xml == null)
                return;
            if (xml.Attributes != null)
            {
                _name = xml.Attributes["Name"] == null ? "" : xml.Attributes["Name"].Value;
                _value = xml.Attributes["Value"] == null ? "" : xml.Attributes["Value"].Value;
            }
        }

        public XmlNode ToXml(XmlDocument doc)
        {
            XmlNode layerNode = doc.CreateElement("CommonConfig");
            XmlAttribute nameAttribute = doc.CreateAttribute("Name");
            nameAttribute.Value = _name;
            XmlAttribute codeAttribute = doc.CreateAttribute("Value");
            codeAttribute.Value = _value;
            layerNode.Attributes.Append(nameAttribute);
            layerNode.Attributes.Append(codeAttribute);
            return layerNode;
        }
    }
}