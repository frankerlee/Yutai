using System;
using System.Xml;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.Plugins.Template.Interfaces;

namespace Yutai.Plugins.Template.Concretes
{
    public class YTField 
    {
    
        private string _name;
        private string _aliasName;
        private int _length;
        private int _precision;
        private esriFieldType _fieldType;
        private bool _allowNull;
        private string _domain;
        private string _esriFieldName;
        private int _id;
        private string _fieldTypeName;
        private bool _isKey;
        private string _defaultValue;


        public YTField()
        {
            _length = 50;
            _precision = 0;
            _fieldTypeName = "String";
            _allowNull = true;
            _isKey = false;
            _domain = "";
            _name = "";
            _aliasName = "";

        }

        public YTField(YTField pField)
        {
            _name = pField.Name;
            _aliasName = pField.AliasName;
            _length = pField.Length;
            _precision = pField.Precision;
            _fieldType = pField.FieldType;
            _fieldTypeName = pField.FieldTypeName;
            _allowNull = pField.AllowNull;
            _domain = pField.DomainName;
            _isKey = pField.IsKey;
          
            if (string.IsNullOrEmpty(_domain))
            {
                DomainName = pField.DomainName;
            }
        }

        public YTField(IRow pRow)
        {
            _id = pRow.OID;
            _name = FeatureHelper.GetRowValue(pRow, "FieldName").ToString();
            _aliasName = FeatureHelper.GetRowValue(pRow, "AliasName").ToString();
            _length = ConvertHelper.ObjectToInt(FeatureHelper.GetRowValue(pRow, "FieldLength"));
            _precision = ConvertHelper.ObjectToInt(FeatureHelper.GetRowValue(pRow, "FieldPrecision"));
            _fieldTypeName = FeatureHelper.GetRowValue(pRow, "FieldType").ToString();
            _fieldType = FieldHelper.ConvertFromSimpleString(_fieldTypeName);
            string allowNullStr= FeatureHelper.GetRowValue(pRow, "AllowNull").ToString();
            _allowNull = string.IsNullOrEmpty(allowNullStr) || allowNullStr.ToUpper().StartsWith("T") || allowNullStr == "真";
            string keyStr= FeatureHelper.GetRowValue(pRow, "IsKey").ToString();
            _isKey = string.IsNullOrEmpty(keyStr)==false || (keyStr.ToUpper().StartsWith("T") == true ? true : false);
            _domain = FeatureHelper.GetRowValue(pRow, "FieldDomain").ToString();
            _defaultValue = FeatureHelper.GetRowValue(pRow, "DefaultValue").ToString();
        }

        public YTField(XmlNode node)
        {
            ReadFromXml(node);
        }

        public int ID
        {
            get { return _id; }
            set { _id = value; }
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

        public bool IsKey
        {
            get { return _isKey; }
            set { _isKey = value; }
        }

        public string FieldTypeName
        {
            get { return _fieldTypeName; }
            set
            {
                _fieldTypeName = value;
                _fieldType = FieldHelper.ConvertFromSimpleString(_fieldTypeName);
            }
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

        public string DomainName
        {
            get { return _domain; }
            set { _domain = value; }
        }

        public string DefaultValue
        {
            get { return _defaultValue; }
            set { _defaultValue = value; }
        }


        public void ReadFromXml(XmlNode xml)
        {
            if (xml.Attributes != null)
            {
              
                _name = xml.Attributes["Name"].Value;
                _aliasName = xml.Attributes["AliasName"].Value;
                _length = string.IsNullOrWhiteSpace(xml.Attributes["Length"].Value)
                    ? 50
                    : Convert.ToInt32(xml.Attributes["Length"].Value);
                _allowNull = string.IsNullOrWhiteSpace(xml.Attributes["AllowNull"].Value) || xml.Attributes["AllowNull"].Value.ToUpper().StartsWith("T") || xml.Attributes["AllowNull"].Value.Equals("真");
                _fieldTypeName = xml.Attributes["FieldType"].Value;
                _precision = string.IsNullOrWhiteSpace(xml.Attributes["Precision"].Value)
                    ? 50
                    : Convert.ToInt32(xml.Attributes["Precision"].Value);
                _fieldType = FieldHelper.ConvertFromString(_fieldTypeName);
                _domain = xml.Attributes["FieldDomain"] == null ? "" : xml.Attributes["FieldDomain"].Value;
                _defaultValue = xml.Attributes["DefaultValue"] == null ? "" : xml.Attributes["DefaultValue"].Value;
                _isKey = string.IsNullOrWhiteSpace(xml.Attributes["IsKey"].Value) || (xml.Attributes["IsKey"].Value.ToUpper().StartsWith("T"));

            }
        }

        public XmlNode ToXml(XmlDocument doc)
        {
            XmlNode fieldNode = doc.CreateElement("Field");
            XmlAttribute nameAttribute = doc.CreateAttribute("Name");
            nameAttribute.Value = _name;
            XmlAttribute aliasNameAttribute = doc.CreateAttribute("AliasName");
            aliasNameAttribute.Value = _aliasName;
            XmlAttribute lengthAttribute = doc.CreateAttribute("Length");
            lengthAttribute.Value = _length.ToString();
            XmlAttribute allowNullAttribute = doc.CreateAttribute("AllowNull");
            allowNullAttribute.Value = _allowNull.ToString();
            XmlAttribute fieldTypeAttribute = doc.CreateAttribute("FieldType");
            fieldTypeAttribute.Value = FieldHelper.ConvertToSimpleString(_fieldType);
            XmlAttribute precisionAttribute = doc.CreateAttribute("Precision");
            precisionAttribute.Value = _precision.ToString();
            XmlAttribute domainAttribute = doc.CreateAttribute("FieldDomain");
            domainAttribute.Value = _domain;
            XmlAttribute defaultAttribute = doc.CreateAttribute("DefaultValue");
            defaultAttribute.Value = _defaultValue;

            fieldNode.Attributes.Append(nameAttribute);
            fieldNode.Attributes.Append(aliasNameAttribute);
            fieldNode.Attributes.Append(lengthAttribute);
            fieldNode.Attributes.Append(allowNullAttribute);
            fieldNode.Attributes.Append(fieldTypeAttribute);
            fieldNode.Attributes.Append(precisionAttribute);
            fieldNode.Attributes.Append(domainAttribute);
            domainAttribute = doc.CreateAttribute("IsKey");
            domainAttribute.Value = _isKey ? "True":"False";
            fieldNode.Attributes.Append(domainAttribute);
            fieldNode.Attributes.Append(defaultAttribute);

            return fieldNode;
        }

       
        public string EsriFieldName
        {
            get { return _esriFieldName; }
            set { _esriFieldName = value; }
        }

        public YTField Clone(bool keepClass)
        {
            YTField newField = new YTField(this);
            return newField;
        }

        public void UpdateRow(IRow pRow)
        {
            pRow.Value[pRow.Fields.FindField("FieldName")] = _name;
            pRow.Value[pRow.Fields.FindField("AliasName")] = _aliasName;
            pRow.Value[pRow.Fields.FindField("FieldLength")] = _length;
            pRow.Value[pRow.Fields.FindField("FieldPrecision")] = _precision;
            pRow.Value[pRow.Fields.FindField("FieldType")] = FieldHelper.ConvertToSimpleString(_fieldType);
            pRow.Value[pRow.Fields.FindField("AllowNull")] = _allowNull ? "True" : "False";
            pRow.Value[pRow.Fields.FindField("IsKey")] = _isKey ? "True" : "False";
            pRow.Value[pRow.Fields.FindField("FieldDomain")] = _domain;
            pRow.Value[pRow.Fields.FindField("DefaultValue")] = _defaultValue;
            pRow.Store();
            _id = pRow.OID;
        }

        public IField CreateField()
        {
            IFieldEdit pFieldEdit=new Field() as IFieldEdit;
            pFieldEdit.Name_2 = _name;
            pFieldEdit.AliasName_2 = _aliasName;
            pFieldEdit.IsNullable_2 = _allowNull;
            pFieldEdit.Type_2 = _fieldType;
            pFieldEdit.Length_2 = _length;
            if (_fieldType == esriFieldType.esriFieldTypeDouble || _fieldType == esriFieldType.esriFieldTypeSingle)
            {
                pFieldEdit.Precision_2 = _precision;
            }
            return pFieldEdit as IField;
        }

        public bool IsValid(out string msg)
        {
            msg = "";
            if (string.IsNullOrEmpty(_name))
            {
                msg = "名称不能为空!";
                return false;
            }
            if (string.IsNullOrEmpty(_fieldTypeName))
            {
                msg = "类型不能为空!";
                return false;
            }
            return true;
        }
    }
}