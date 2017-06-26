using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Helpers;
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
        private string _domainValues;
        private string _esriFieldName;
      

        public YTField() { }

        public YTField(IYTField pField)
        {
            _typeName = pField.TypeName;
            _name = pField.Name;
            _aliasName = pField.AliasName;
            _autoNames = pField.AutoNames;
            _length = pField.Length;
            _precision = pField.Precision;
            _fieldType = pField.FieldType;
            _allowNull = pField.AllowNull;
            _domainValues = pField.DomainValues;

        }
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

        public string FixAutoNames
        {
            get { return "/"+_autoNames+"/"; }
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

        public string DomainValues
        {
            get { return _domainValues; }
            set { _domainValues = value; }
        }

        public void ReadFromXml(XmlNode xml)
        {
            if (xml.Attributes != null)
            {
                _typeName = xml.Attributes["TypeName"].Value;
                _name = xml.Attributes["Name"].Value;
                _aliasName = xml.Attributes["AliasName"].Value;
                _autoNames = xml.Attributes["AutoNames"].Value;
                _length = string.IsNullOrWhiteSpace(xml.Attributes["Length"].Value) ? 50 : Convert.ToInt32(xml.Attributes["Length"].Value);
                _allowNull = string.IsNullOrWhiteSpace(xml.Attributes["AllowNull"].Value) || (xml.Attributes["AllowNull"].Value.ToUpper().StartsWith("T"));
                _fieldTypeStr = xml.Attributes["FieldType"].Value;
                _precision = string.IsNullOrWhiteSpace(xml.Attributes["Precision"].Value) ? 50: Convert.ToInt32(xml.Attributes["Precision"].Value);
                _fieldType = FieldHelper.ConvertFromString(_fieldTypeStr);
                _domainValues = xml.Attributes["DomainValues"]==null?"": xml.Attributes["DomainValues"].Value;
            }
        }

        public XmlNode ToXml(XmlDocument doc)
        {
            XmlNode fieldNode = doc.CreateElement("Field");
            XmlAttribute typeNameAttribute = doc.CreateAttribute("TypeName");
            typeNameAttribute.Value = _typeName;
            XmlAttribute nameAttribute = doc.CreateAttribute("Name");
            nameAttribute.Value = _name;
            XmlAttribute aliasNameAttribute = doc.CreateAttribute("AliasName");
            aliasNameAttribute.Value = _aliasName;
            XmlAttribute lengthAttribute = doc.CreateAttribute("Length");
            lengthAttribute.Value = _length.ToString();
            XmlAttribute allowNullAttribute = doc.CreateAttribute("AllowNull");
            allowNullAttribute.Value = _allowNull.ToString();
            XmlAttribute fieldTypeAttribute = doc.CreateAttribute("FieldType");
            fieldTypeAttribute.Value = FieldHelper.QueryFieldTypeName(_fieldType);
            XmlAttribute precisionAttribute = doc.CreateAttribute("Precision");
            precisionAttribute.Value = _precision.ToString();

            XmlAttribute domainAttribute = doc.CreateAttribute("DomainValues");
            domainAttribute.Value = _domainValues;

            fieldNode.Attributes.Append(typeNameAttribute);
            fieldNode.Attributes.Append(nameAttribute);
            fieldNode.Attributes.Append(aliasNameAttribute);
            fieldNode.Attributes.Append(lengthAttribute);
            fieldNode.Attributes.Append(allowNullAttribute);
            fieldNode.Attributes.Append(fieldTypeAttribute);
            fieldNode.Attributes.Append(precisionAttribute);
            fieldNode.Attributes.Append(domainAttribute);
            return fieldNode;
        }

        public void LoadFromField(IFields fields)
        {
            string[] autoNames = _autoNames.Split(';');
            foreach (string autoName in autoNames)
            {
                int i = fields.FindField(autoName);
                if (i >= 0)
                {
                    IField field = fields.Field[i];
                    _name = field.Name;
                    _aliasName = field.AliasName;
                    _length = field.Length;
                    _allowNull = field.IsNullable;
                    _fieldType = field.Type;
                    _precision = field.Precision;
                }
            }
        }

        public string EsriFieldName
        {
            get { return _esriFieldName; }
            set { _esriFieldName = value; }
        }

        public IYTField Clone(bool keepClass)
        {
            IYTField newField= new YTField(this);
            if (keepClass)
            {
                newField.EsriFieldName = this._esriFieldName;
            }
            return newField;
        }
    }
}
