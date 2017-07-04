using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using Yutai.Plugins.Interfaces;

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
            if (string.IsNullOrEmpty(_xmlFile))
            {
                _xmlFile = Application.StartupPath + "\\plugins\\configs\\Printing.xml";
            }
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
}