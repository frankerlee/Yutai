using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Yutai.Pipeline.Config.Interfaces;

namespace Yutai.Pipeline.Config.Concretes
{
    public  class PipelineTemplate:IPipelineTemplate
    {
        private string _name;
        private string _caption;
        private List<IYTField> _fields;

        public PipelineTemplate()
        {
            _fields=new List<IYTField>();
        }

        public PipelineTemplate(XmlNode node)
        {
            _fields = new List<IYTField>();
            ReadFromXml(node);
        }



        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Caption
        {
            get { return _caption; }
            set { _caption = value; }
        }

        public List<IYTField> Fields
        {
            get { return _fields; }
            set { _fields = value; }
        }

        public void ReadFromXml(XmlNode xmlNode)
        {
            _name = xmlNode.Attributes["Name"].Value;
            _caption = xmlNode.Attributes["Caption"].Value;
            XmlNodeList nodeList = xmlNode.SelectNodes("/Template/Fields/Field");
            foreach (XmlNode node in nodeList)
            {
                IYTField field = new YTField(node);
                _fields.Add(field);
            }
        }

        public XmlNode ToXml()
        {
            throw new NotImplementedException();
        }
    }
}
