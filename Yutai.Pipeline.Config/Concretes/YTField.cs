using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Pipeline.Config.Interfaces;

namespace Yutai.Pipeline.Config.Concretes
{
    public class YTField:IYTField
    {
        private string _typeName;
        private string _name;
        private string _aliasName;
        private string _autoNames;
        private int _length;
        private int _precision;
        private esriFieldType _fieldType;
        private bool _allowNull;
        private string _fieldTypeStr;

        public YTField() { }
        public YTField(XmlNode node) { ReadFromXml(node);}

        public string TypeName
        {
            get { return _typeName; }
            set { _typeName = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string AliasName
        {
            get { return _aliasName; }
            set { _aliasName = value; }
        }

        public string AutoNames
        {
            get { return _autoNames; }
            set { _autoNames = value; }
        }

        public int Length
        {
            get { return _length; }
            set { _length = value; }
        }

        public int Precision
        {
            get { return _precision; }
            set { _precision = value; }
        }

        public esriFieldType FieldType
        {
            get { return _fieldType; }
            set { _fieldType = value; }
        }

        public bool AllowNull
        {
            get { return _allowNull; }
            set { _allowNull = value; }
        }

        public void ReadFromXml(XmlNode xml)
        {
            _typeName = xml.Attributes["TypeName"].Value;
            _name = xml.Attributes["Name"].Value;
            _aliasName = xml.Attributes["AliasName"].Value;
            _autoNames = xml.Attributes["AutoNames"].Value;
            _length = xml.Attributes["Length"].Value==null?50: Convert.ToInt32(xml.Attributes["Length"].Value);
            _allowNull = xml.Attributes["AllowNull"].Value==null ? true: (xml.Attributes["AllowNull"].Value.ToUpper().StartsWith("T")?true:false);
            _fieldTypeStr = xml.Attributes["FieldType"].Value;
            _precision = xml.Attributes["Precision"].Value == null ? 50 : Convert.ToInt32(xml.Attributes["Precision"].Value);
            _fieldType = FieldHelper.ConvertFromString(_fieldTypeStr);
        }

        public XmlNode ToXml()
        {
            throw new NotImplementedException();
        }
    }
}
