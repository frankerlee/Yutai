using System.Collections.Generic;
using System.Xml;
using Yutai.Pipeline.Config.Interfaces;

namespace Yutai.Pipeline.Config.Concretes
{
    public class YTDomain : IYTDomain
    {
        private string _domainName;
        private string _domainValues;
        private Dictionary<string, string> _domainDirectory=new Dictionary<string, string>();

        public string DomainName
        {
            get { return _domainName; }
            set { _domainName = value; }
        }

        public string DomainValues
        {
            get { return _domainValues; }
            set { _domainValues = value; }
        }

        public Dictionary<string, string> DomainDirectory
        {
            get { return _domainDirectory; }
            set { _domainDirectory = value; }
        }

        public YTDomain()
        {
            
        }

        public YTDomain(string domainName,string domainValues)
        {
            _domainName = domainName;
            _domainValues = domainValues;
            ParseDomainValues();


        }

        private void ParseDomainValues()
        {
            _domainDirectory.Clear();
            if (string.IsNullOrEmpty(_domainValues)) return;
            string[] values = _domainValues.Split('/');
            for (int i = 0; i < values.Length; i++)
            {
                string[] domainPair = values[i].Split(':');
                if (domainPair.Length == 1)
                {
                    _domainDirectory.Add(domainPair[0], domainPair[0]);
                }
                else
                {
                    _domainDirectory.Add(domainPair[0], domainPair[1]);
                }
            }
        }
        public void ReadFromXml(XmlNode xml)
        {
            if (xml.Attributes != null)
            {
                _domainName = xml.Attributes["Name"].Value;
                _domainValues = xml.Attributes["Values"] == null ? "" : xml.Attributes["DomainValues"].Value;
                if (!string.IsNullOrEmpty(_domainValues))
                {
                    ParseDomainValues();
                }
            }
        }

        public XmlNode ToXml(XmlDocument doc)
        {
            XmlNode fieldNode = doc.CreateElement("Domain");
            XmlAttribute nameAttribute = doc.CreateAttribute("Name");
            nameAttribute.Value = _domainName;
            XmlAttribute domainAttribute = doc.CreateAttribute("Values");
            domainAttribute.Value = _domainValues;
            fieldNode.Attributes.Append(nameAttribute);
            fieldNode.Attributes.Append(domainAttribute);
            return fieldNode;
        }
    }
}