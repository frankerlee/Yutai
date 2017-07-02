using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Plugins.Interfaces;
using Yutai.Shared;

namespace Yutai.Plugins.Printing
{

    public class PrintingConfig : IPrintingConfig
    {
        private List<IIndexMap> _indexMaps;
        private string _xmlFile;
        private string _templateConnectionString;
        

        public PrintingConfig()
        {
            _indexMaps=new List<IIndexMap>();
            
        }

        public void LoadFromXml(string fileName)
        {
            _xmlFile = fileName;
            XmlDocument doc = new XmlDocument();
            doc.Load(_xmlFile);

            XmlNode dbNode = doc.SelectSingleNode("/PrintingConfig/TemplateDatabase");
            _templateConnectionString = dbNode == null ? "" : dbNode.Attributes["ConnectionString"].Value;
            XmlNodeList nodes = doc.SelectNodes("/PipelineConfig/IndexMaps/IndexMap");
            if (nodes != null)
                foreach (XmlNode node in nodes)
                {
                  IIndexMap indexMap=new IndexMap(node);
                    _indexMaps.Add(indexMap);
                }
            
        }

        public void SaveToXml(string fileName)
        {
            XmlDocument doc = new XmlDocument();

            XmlNode rootNode = doc.CreateElement("PrintingConfig");
            XmlNode dbNode = doc.CreateElement("TemplateDatabase");
            XmlAttribute dbAttr = doc.CreateAttribute("ConnectionString");
            dbAttr.Value = _templateConnectionString;
            dbNode.Attributes.Append(dbAttr);
            rootNode.AppendChild(dbNode);
            XmlNode configNode = doc.CreateElement("IndexMaps");
            for (int i = 0; i < _indexMaps.Count; i++)
            {
                XmlNode subNode = _indexMaps[i].SaveToXml(doc);
                configNode.AppendChild(subNode);
            }
            rootNode.AppendChild(configNode);
            doc.AppendChild(rootNode);
            doc.Save(fileName);
        }

        public void Save()
        {
            SaveToXml(_xmlFile);
        }

        public string TemplateConnectionString
        {
            get { return _templateConnectionString; }
            set { _templateConnectionString = value; }
        }

        public List<IIndexMap> IndexMaps
        {
            get { return _indexMaps; }
            set { _indexMaps = value; }
        }
    }

    

    public class IndexMap : IIndexMap
    {
        private string _name;
        private string _indexLayerName;
        private string _templateName;
        private string _searchFields;
        private string _nameField;
        private string _workspaceName;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string WorkspaceName
        {
            get { return _workspaceName; }
            set { _workspaceName = value; }
        }

        public string IndexLayerName
        {
            get { return _indexLayerName; }
            set { _indexLayerName = value; }
        }

        public string TemplateName
        {
            get { return _templateName; }
            set { _templateName = value; }
        }

        public string SearchFields
        {
            get { return _searchFields; }
            set { _searchFields = value; }
        }

        public string NameField
        {
            get { return _nameField; }
            set { _nameField = value; }
        }

        public IndexMap()
        {
        }

        public IndexMap(XmlNode xmlNode)
        {
            ReadFromXml(xmlNode);
        }

        public void ReadFromXml(XmlNode xml)
        {
            if (xml == null)
                return;
            if (xml.Attributes != null)
            {
                _name = xml.Attributes["Name"] == null ? "" : xml.Attributes["Name"].Value;
                _templateName = xml.Attributes["Template"] == null ? "" : xml.Attributes["Template"].Value;
                _searchFields = xml.Attributes["SearchFields"] == null ? "" : xml.Attributes["SearchFields"].Value;
                _nameField = xml.Attributes["NameField"] == null ? "" : xml.Attributes["NameField"].Value;
                _indexLayerName = xml.Attributes["IndexLayer"] == null ? "" : xml.Attributes["IndexLayer"].Value;
                _indexLayerName = xml.Attributes["Workspace"] == null ? "" : xml.Attributes["Workspace"].Value;
            }
        
        }

        public XmlNode SaveToXml(XmlDocument doc)
        {
            XmlNode layerNode = doc.CreateElement("IndexMap");
            XmlAttribute nameAttribute = doc.CreateAttribute("Name");
            nameAttribute.Value = _name;
            XmlAttribute codeAttribute = doc.CreateAttribute("Template");
            codeAttribute.Value = _templateName;
            XmlAttribute classcodeAttribute = doc.CreateAttribute("SearchFields");
            classcodeAttribute.Value = _searchFields;
            XmlAttribute autoNamesAttribute = doc.CreateAttribute("NameField");
            autoNamesAttribute.Value = _nameField;
            XmlAttribute wksAttribute = doc.CreateAttribute("Workspace");
            wksAttribute.Value = _workspaceName;
            XmlAttribute layerAttribute = doc.CreateAttribute("IndexLayer");
            layerAttribute.Value = _indexLayerName;
            layerNode.Attributes.Append(nameAttribute);
            layerNode.Attributes.Append(codeAttribute);
            layerNode.Attributes.Append(classcodeAttribute);
            layerNode.Attributes.Append(autoNamesAttribute);
            layerNode.Attributes.Append(wksAttribute);
            layerNode.Attributes.Append(layerAttribute);
            return layerNode;
        }
    }
}
