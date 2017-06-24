// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  PointAssist.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/23  09:48
// 更新时间 :  2017/06/23  09:48

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Pipeline.Config.Interfaces;

namespace Yutai.Pipeline.Config.Concretes
{
    public class PointAssist:IPointAssist
    {
        private string _typeName;
        private IYTField _noField;
        private IYTField _codeField;
        private IYTField _xField;
        private IYTField _yField;
        private IYTField _zField;
        private IYTField _rotangField;
        private IYTField _roadCodeField;
        private IYTField _dCodeField;
        private IYTField _dDateField;
        private IYTField _mDateField;
        private IYTField _remarkField;
        private string _name;
        private string _aliasName;
        private bool _visible;
        private string _templateName;
        private IPipelineTemplate _template;

        private List<IYTField> _fields;
        private IFeatureLayer _featureLayer;

        public PointAssist()
        {
            _fields = new List<IYTField>();
        }

        public PointAssist(XmlNode xmlNode)
        {
            _fields = new List<IYTField>();
            ReadFromXml(xmlNode);
        }

        public PointAssist(XmlNode xmlNode, IPipelineTemplate template)
        {
            _fields = new List<IYTField>();
            _template = template;
            LoadTemplate(template);
            ReadFromXml(xmlNode);
        }

        public string TypeName
        {
            get { return _typeName; }
            set { _typeName = value; }
        }

        public IYTField NoField
        {
            get { return _noField; }
            set { _noField = value; }
        }

        public IYTField CodeField
        {
            get { return _codeField; }
            set { _codeField = value; }
        }

        public IYTField XField
        {
            get { return _xField; }
            set { _xField = value; }
        }

        public IYTField YField
        {
            get { return _yField; }
            set { _yField = value; }
        }

        public IYTField ZField
        {
            get { return _zField; }
            set { _zField = value; }
        }

        public IYTField RotangField
        {
            get { return _rotangField; }
            set { _rotangField = value; }
        }

        public IYTField RoadCodeField
        {
            get { return _roadCodeField; }
            set { _roadCodeField = value; }
        }

        public IYTField DCodeField
        {
            get { return _dCodeField; }
            set { _dCodeField = value; }
        }

        public IYTField DDateField
        {
            get { return _dDateField; }
            set { _dDateField = value; }
        }

        public IYTField MDateField
        {
            get { return _mDateField; }
            set { _mDateField = value; }
        }

        public IYTField RemarkField
        {
            get { return _remarkField; }
            set { _remarkField = value; }
        }
        
        public void AutoAssembly(IFeatureLayer pLayer)
        {
            _noField?.LoadFromField(pLayer.FeatureClass.Fields);
            _codeField?.LoadFromField(pLayer.FeatureClass.Fields);
            _xField?.LoadFromField(pLayer.FeatureClass.Fields);
            _yField?.LoadFromField(pLayer.FeatureClass.Fields);
            _zField?.LoadFromField(pLayer.FeatureClass.Fields);
            _rotangField?.LoadFromField(pLayer.FeatureClass.Fields);
            _roadCodeField?.LoadFromField(pLayer.FeatureClass.Fields);
            _dCodeField?.LoadFromField(pLayer.FeatureClass.Fields);
            _dDateField?.LoadFromField(pLayer.FeatureClass.Fields);
            _mDateField?.LoadFromField(pLayer.FeatureClass.Fields);
            _remarkField?.LoadFromField(pLayer.FeatureClass.Fields);
        }

        public void ReadFromXml(XmlNode xml)
        {
            if (xml?.Attributes == null)
                return;
            _name = xml.Attributes["Name"].Value;
            _aliasName = xml.Attributes["AliasName"].Value;
            _visible = Convert.ToBoolean(xml.Attributes["Visible"].Value);
            _templateName = xml.Attributes["Template"].Value;

            XmlNodeList nodeList = xml.SelectNodes("/PointAssist/Fields/Field");

            if (nodeList != null)
                foreach (XmlNode node in nodeList)
                {
                    IYTField field = new YTField(node);
                    _fields.Add(field);
                }

            _noField = _fields.FirstOrDefault(c => c.TypeName == "NoField");
            _codeField = _fields.FirstOrDefault(c => c.TypeName == "CodeField");
            _xField = _fields.FirstOrDefault(c => c.TypeName == "XField");
            _yField = _fields.FirstOrDefault(c => c.TypeName == "YField");
            _zField = _fields.FirstOrDefault(c => c.TypeName == "ZField");
            _rotangField = _fields.FirstOrDefault(c => c.TypeName == "RotangField");
            _roadCodeField = _fields.FirstOrDefault(c => c.TypeName == "RoadCodeField");
            _dCodeField = _fields.FirstOrDefault(c => c.TypeName == "DCodeField");
            _dDateField = _fields.FirstOrDefault(c => c.TypeName == "DDateField");
            _mDateField = _fields.FirstOrDefault(c => c.TypeName == "MDateField");
            _remarkField = _fields.FirstOrDefault(c => c.TypeName == "RemarkField");
        }

        public XmlNode ToXml(XmlDocument doc)
        {
            XmlNode layerNode = doc.CreateElement("PointAssist");
            XmlAttribute nameAttribute = doc.CreateAttribute("Name");
            nameAttribute.Value = _name;
            XmlAttribute aliasNameAttribute = doc.CreateAttribute("AliasName");
            aliasNameAttribute.Value = _aliasName;
            XmlAttribute templateAttribute = doc.CreateAttribute("Template");
            templateAttribute.Value = _templateName;
            XmlAttribute visibleAttribute = doc.CreateAttribute("Visible");
            visibleAttribute.Value = _visible.ToString();
            layerNode.Attributes.Append(nameAttribute);
            layerNode.Attributes.Append(aliasNameAttribute);
            layerNode.Attributes.Append(templateAttribute);
            layerNode.Attributes.Append(visibleAttribute);

            XmlNode fieldsNode = doc.CreateElement("Fields");
            if (_noField != null && _template != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _noField.TypeName) == null)
                fieldsNode.AppendChild(_noField.ToXml(doc));
            if (_codeField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _codeField.TypeName) == null)
                fieldsNode.AppendChild(_codeField.ToXml(doc));
            if (_xField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _xField.TypeName) == null)
                fieldsNode.AppendChild(_xField.ToXml(doc));
            if (_yField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _yField.TypeName) == null)
                fieldsNode.AppendChild(_yField.ToXml(doc));
            if (_zField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _zField.TypeName) == null)
                fieldsNode.AppendChild(_zField.ToXml(doc));
            if (_rotangField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _rotangField.TypeName) == null)
                fieldsNode.AppendChild(_rotangField.ToXml(doc));
            if (_roadCodeField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _roadCodeField.TypeName) == null)
                fieldsNode.AppendChild(_roadCodeField.ToXml(doc));
            if (_dCodeField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _dCodeField.TypeName) == null)
                fieldsNode.AppendChild(_dCodeField.ToXml(doc));
            if (_dDateField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _dDateField.TypeName) == null)
                fieldsNode.AppendChild(_dDateField.ToXml(doc));
            if (_mDateField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _mDateField.TypeName) == null)
                fieldsNode.AppendChild(_mDateField.ToXml(doc));
            if (_remarkField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _remarkField.TypeName) == null)
                fieldsNode.AppendChild(_remarkField.ToXml(doc));
            layerNode.AppendChild(fieldsNode);
            return layerNode;
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

        public string TemplateName
        {
            get { return _templateName; }
            set { _templateName = value; }
        }

        public void LoadTemplate(IPipelineTemplate template)
        {
            _fields.AddRange(template.Fields);
        }

        public IFeatureLayer FeatureLayer
        {
            get { return _featureLayer; }
            set { _featureLayer = value; }
        }
    }
}