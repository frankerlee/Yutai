// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  FunctionLayer.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/27  15:54
// 更新时间 :  2017/06/27  15:54

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Pipeline.Config.Helpers;
using Yutai.Pipeline.Config.Interfaces;

namespace Yutai.Pipeline.Config.Concretes
{
    public class FunctionLayer : IFunctionLayer
    {
        private string _name;
        private string _aliasName;
        private bool _visible;
        private string _autoNames;
        private enumFunctionLayerType _functionType;
        private List<IYTField> _fields;
        private IFeatureClass _featureClass;
        private string _esriClassName;
        private string _validateKeys;

        public FunctionLayer()
        {
        }

        public FunctionLayer(XmlNode node)
        {
            ReadFromXml(node);
        }

        public FunctionLayer(IFunctionLayer layer, bool keepClass)
        {
            _name = layer.Name;
            _aliasName = layer.AliasName;
            _visible = layer.Visible;
            _autoNames = layer.AutoNames;
            _functionType = layer.FunctionType;
            _fields = new List<IYTField>();
            foreach (IYTField layerField in layer.Fields)
            {
                _fields.Add(layerField.Clone(keepClass));
            }
            if (keepClass)
            {
                _featureClass = layer.FeatureClass;
                _esriClassName = layer.EsriClassName;
            }
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

        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        public string AutoNames
        {
            get { return _autoNames; }
            set { _autoNames = value; }
        }

        public string FixAutoNames
        {
            get { return "/" + _autoNames + "/"; }
        }

        public enumFunctionLayerType FunctionType
        {
            get { return _functionType; }
            set { _functionType = value; }
        }

        public List<IYTField> Fields
        {
            get { return _fields; }
            set { _fields = value; }
        }

        public IFeatureClass FeatureClass
        {
            get { return _featureClass; }
            set
            {
                _featureClass = value;
                _esriClassName = ((IDataset) _featureClass).Name;
                _name = ((IDataset) _featureClass).Name;
                _aliasName = _featureClass.AliasName;
                LoadFieldAutoNames();
            }
        }

        private void LoadFieldAutoNames()
        {
            for (int i = 0; i < _featureClass.Fields.FieldCount; i++)
            {
                IField pField = _featureClass.Fields.Field[i];
                IYTField ytField =
                    _fields.FirstOrDefault(
                        c =>
                            c.Name == pField.Name || c.AliasName == pField.Name || c.AliasName == pField.AliasName ||
                            c.Name == pField.AliasName || c.FixAutoNames.Contains("/" + pField.Name + "/"));
                if (ytField != null)
                {
                    ytField.EsriFieldName = pField.Name;
                    ytField.Name = pField.Name;
                    ytField.AliasName = pField.AliasName;
                    continue;
                }
            }
        }

        public string EsriShortName
        {
            get
            {
                if (_featureClass == null)
                {
                    return "";
                }
                string paramName = ((IDataset) _featureClass).Name;
                string str = paramName;
                int num = paramName.LastIndexOf(".", StringComparison.Ordinal);
                if (num >= 0)
                {
                    str = paramName.Substring(num + 1);
                }
                return str;
            }
        }

        public string EsriClassName
        {
            get { return _esriClassName; }
            set { _esriClassName = value; }
        }

        public string ValidateKeys
        {
            get { return _validateKeys; }
            set { _validateKeys = value; }
        }

        public void ReadFromXml(XmlNode xmlNode)
        {
            if (xmlNode.Attributes != null)
            {
                _name = xmlNode.Attributes["Name"].Value;
                _aliasName = xmlNode.Attributes["AliasName"] == null ? "" : xmlNode.Attributes["AliasName"].Value;
                _visible = xmlNode.Attributes["Visible"] == null ||
                           xmlNode.Attributes["Visible"].Value.ToUpper().StartsWith("T");
                _functionType = xmlNode.Attributes["Type"] == null
                    ? enumFunctionLayerType.Other
                    : EnumHelper.ConvertFunctionLayerTypeFromString(xmlNode.Attributes["Type"].Value);
            }
            XmlNodeList nodeList =
                xmlNode.SelectNodes($"/PipelineConfig/FunctionLayers/FunctionLayer[@Name='{_name}']/Fields/Field");
            foreach (XmlNode node in nodeList)
            {
                IYTField field = new YTField(node);
                _fields.Add(field);
            }
        }

        public XmlNode ToXml(XmlDocument doc)
        {
            XmlNode functionNode = doc.CreateElement("FunctionLayer");
            XmlAttribute nameAttribute = doc.CreateAttribute("Name");
            nameAttribute.Value = _name;
            XmlAttribute aliasAttribute = doc.CreateAttribute("AliasName");
            aliasAttribute.Value = _aliasName;
            XmlAttribute visibleAttribute = doc.CreateAttribute("Visible");
            visibleAttribute.Value = _visible.ToString();
            XmlAttribute typeAttribute = doc.CreateAttribute("Type");
            typeAttribute.Value = EnumHelper.ConvertFunctionLayerTypeToString(_functionType);
            functionNode.Attributes.Append(nameAttribute);
            functionNode.Attributes.Append(aliasAttribute);
            functionNode.Attributes.Append(visibleAttribute);
            functionNode.Attributes.Append(typeAttribute);

            XmlNode fieldsNode = doc.CreateElement("Fields");
            foreach (IYTField ytField in _fields)
            {
                fieldsNode.AppendChild(ytField.ToXml(doc));
            }
            functionNode.AppendChild(fieldsNode);

            return functionNode;
        }

        public IYTField GetField(string typeWord)
        {
            IYTField field = _fields.FirstOrDefault(c => c.TypeName == typeWord);
            return field;
        }

        public string GetFieldName(string typeWord)
        {
            IYTField field = _fields.FirstOrDefault(c => c.TypeName == typeWord);
            return field != null ? field.Name : typeWord;
        }

        public IFunctionLayer Clone(bool keepClass)
        {
            return new FunctionLayer(this, keepClass);
        }
    }
}