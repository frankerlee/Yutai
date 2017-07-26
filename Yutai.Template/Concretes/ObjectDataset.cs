using System.Xml;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.Plugins.Template.Interfaces;

namespace Yutai.Plugins.Template.Concretes
{
    public class ObjectDataset : IObjectDataset
    {
        private string _name;
        private string _aliasName;
        private string _baseName;
        private int _id=-1;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int ID { get { return _id; } set { _id = value; } }
        public string AliasName
        {
            get { return _aliasName; }
            set { _aliasName = value; }
        }

        public string BaseName
        {
            get { return _baseName; }
            set { _baseName = value; }
        }

        public ObjectDataset()
        {
            
        }
        public ObjectDataset(IRow pRow)
        {
            _id = pRow.OID;
            _name = FeatureHelper.GetRowValue(pRow, "Dataset").ToString();
            _baseName = FeatureHelper.GetRowValue(pRow, "BaseName").ToString();
            _aliasName = FeatureHelper.GetRowValue(pRow, "AliasName").ToString();
        }
        public void ReadFromXml(XmlNode xmlNode)
        {
            if (xmlNode.Attributes != null)
            {
                _name = xmlNode.Attributes["Name"].Value;
                _baseName = xmlNode.Attributes["BaseName"].Value;
                _aliasName = xmlNode.Attributes["AliasName"].Value;
            }
        }

        public XmlNode ToXml(XmlDocument doc)
        {
            XmlNode templateNode = doc.CreateElement("Dataset");

            XmlAttribute nameAttribute = doc.CreateAttribute("Name");
            nameAttribute.Value = _name;
            templateNode.Attributes.Append(nameAttribute);

            nameAttribute = doc.CreateAttribute("AliasName");
            nameAttribute.Value = _aliasName;
            templateNode.Attributes.Append(nameAttribute);

            nameAttribute = doc.CreateAttribute("BaseName");
            nameAttribute.Value = _baseName;
            templateNode.Attributes.Append(nameAttribute);
            return templateNode;
        }

        public void UpdateRow(IRow pRow)
        {
            pRow.Value[pRow.Fields.FindField("Dataset")] = _name;
            pRow.Value[pRow.Fields.FindField("AliasName")] = _aliasName;
            pRow.Value[pRow.Fields.FindField("BaseName")] = _baseName;
          
            pRow.Store();
            _id = pRow.OID;
        }
    }
}