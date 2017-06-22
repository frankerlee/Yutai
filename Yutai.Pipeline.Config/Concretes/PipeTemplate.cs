using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Yutai.Pipeline.Config.Interfaces;

namespace Yutai.Pipeline.Config.Concretes
{
    public  class PipeTemplate:IPipeTemplate
    {
        private string _name;
        private string _caption;
        private List<IYTField> _fields;

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
            throw new NotImplementedException();
        }

        public XmlNode ToXml()
        {
            throw new NotImplementedException();
        }
    }
}
