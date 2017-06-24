using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Pipeline.Config.Interfaces;

namespace Yutai.Pipeline.Config.Concretes
{
    public class PipePoint : IPipePoint
    {
        private string _typeName;
        private IYTField _noField;
        private IYTField _xField;
        private IYTField _yField;
        private IYTField _zField;
        private IYTField _depthField;
        private IYTField _featureField;
        private IYTField _subsidField;
        private IYTField _pStyleField;
        private IYTField _pdsField;
        private IYTField _codeField;
        private IYTField _mapNoField;
        private IYTField _useStatusField;
        private IYTField _bCodeField;
        private IYTField _rotangField;
        private IYTField _roadCodeField;
        private IYTField _mDateField;
        private IYTField _pcjhField;
        private IYTField _remarkField;
        private string _name;
        private string _aliasName;
        private bool _visible;
        private string _templateName;
        private IPipelineTemplate _template;

        private List<IYTField> _fields;
        private IFeatureLayer _featureLayer;

        public PipePoint()
        {
            _fields = new List<IYTField>();
        }

        public PipePoint(XmlNode xmlNode)
        {
            _fields = new List<IYTField>();
            ReadFromXml(xmlNode);
        }

        public PipePoint(XmlNode xmlNode, IPipelineTemplate template)
        {
            _fields = new List<IYTField>();
            _template = template;
            LoadTemplate(_template);
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

        public IYTField DepthField
        {
            get { return _depthField; }
            set { _depthField = value; }
        }

        public IYTField FeatureField
        {
            get { return _featureField; }
            set { _featureField = value; }
        }

        public IYTField SubsidField
        {
            get { return _subsidField; }
            set { _subsidField = value; }
        }

        public IYTField PStyleField
        {
            get { return _pStyleField; }
            set { _pStyleField = value; }
        }

        public IYTField PDSField
        {
            get { return _pdsField; }
            set { _pdsField = value; }
        }

        public IYTField CodeField
        {
            get { return _codeField; }
            set { _codeField = value; }
        }

        public IYTField MapNoField
        {
            get { return _mapNoField; }
            set { _mapNoField = value; }
        }

        public IYTField UseStatusField
        {
            get { return _useStatusField; }
            set { _useStatusField = value; }
        }

        public IYTField BCodeField
        {
            get { return _bCodeField; }
            set { _bCodeField = value; }
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

        public IYTField MDateField
        {
            get { return _mDateField; }
            set { _mDateField = value; }
        }

        public IYTField PCJHField
        {
            get { return _pcjhField; }
            set { _pcjhField = value; }
        }

        public IYTField RemarkField
        {
            get { return _remarkField; }
            set { _remarkField = value; }
        }
        
        public void AutoAssembly(IFeatureLayer pLayer)
        {
            _noField?.LoadFromField(pLayer.FeatureClass.Fields);
            _xField?.LoadFromField(pLayer.FeatureClass.Fields);
            _yField?.LoadFromField(pLayer.FeatureClass.Fields);
            _zField?.LoadFromField(pLayer.FeatureClass.Fields);
            _depthField?.LoadFromField(pLayer.FeatureClass.Fields);
            _featureField?.LoadFromField(pLayer.FeatureClass.Fields);
            _subsidField?.LoadFromField(pLayer.FeatureClass.Fields);
            _pStyleField?.LoadFromField(pLayer.FeatureClass.Fields);
            _pdsField?.LoadFromField(pLayer.FeatureClass.Fields);
            _codeField?.LoadFromField(pLayer.FeatureClass.Fields);
            _mapNoField?.LoadFromField(pLayer.FeatureClass.Fields);
            _useStatusField?.LoadFromField(pLayer.FeatureClass.Fields);
            _bCodeField?.LoadFromField(pLayer.FeatureClass.Fields);
            _rotangField?.LoadFromField(pLayer.FeatureClass.Fields);
            _roadCodeField?.LoadFromField(pLayer.FeatureClass.Fields);
            _mDateField?.LoadFromField(pLayer.FeatureClass.Fields);
            _pcjhField?.LoadFromField(pLayer.FeatureClass.Fields);
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

            XmlNodeList nodeList = xml.SelectNodes("/PointLayer/Fields/Field");

            if (nodeList != null)
                foreach (XmlNode node in nodeList)
                {
                    IYTField field = new YTField(node);
                    _fields.Add(field);
                }

            _noField = _fields.FirstOrDefault(c => c.TypeName == "NoField");
            _xField = _fields.FirstOrDefault(c => c.TypeName == "XField");
            _yField = _fields.FirstOrDefault(c => c.TypeName == "YField");
            _zField = _fields.FirstOrDefault(c => c.TypeName == "ZField");
            _depthField = _fields.FirstOrDefault(c => c.TypeName == "DepthField");
            _featureField = _fields.FirstOrDefault(c => c.TypeName == "FeatureField");
            _subsidField = _fields.FirstOrDefault(c => c.TypeName == "SubsidField");
            _pStyleField = _fields.FirstOrDefault(c => c.TypeName == "PStyleField");
            _pdsField = _fields.FirstOrDefault(c => c.TypeName == "PDSField");
            _codeField = _fields.FirstOrDefault(c => c.TypeName == "CodeField");
            _mapNoField = _fields.FirstOrDefault(c => c.TypeName == "MapNoField");
            _useStatusField = _fields.FirstOrDefault(c => c.TypeName == "UseStatusField");
            _bCodeField = _fields.FirstOrDefault(c => c.TypeName == "BCodeField");
            _rotangField = _fields.FirstOrDefault(c => c.TypeName == "RotangField");
            _roadCodeField = _fields.FirstOrDefault(c => c.TypeName == "RoadCodeField");
            _mDateField = _fields.FirstOrDefault(c => c.TypeName == "MDateField");
            _pcjhField = _fields.FirstOrDefault(c => c.TypeName == "PCJHField");
            _remarkField = _fields.FirstOrDefault(c => c.TypeName == "RemarkField");
        }

        public XmlNode ToXml(XmlDocument doc)
        {
            XmlNode layerNode = doc.CreateElement("PointLayer");
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
            if (_noField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _noField.TypeName) == null)
                fieldsNode.AppendChild(_noField.ToXml(doc));
            if (_xField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _xField.TypeName) == null)
                fieldsNode.AppendChild(_xField.ToXml(doc));
            if (_yField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _yField.TypeName) == null)
                fieldsNode.AppendChild(_yField.ToXml(doc));
            if (_zField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _zField.TypeName) == null)
                fieldsNode.AppendChild(_zField.ToXml(doc));
            if (_depthField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _depthField.TypeName) == null)
                fieldsNode.AppendChild(_depthField.ToXml(doc));
            if (_featureField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _featureField.TypeName) == null)
                fieldsNode.AppendChild(_featureField.ToXml(doc));
            if (_subsidField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _subsidField.TypeName) == null)
                fieldsNode.AppendChild(_subsidField.ToXml(doc));
            if (_pStyleField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _pStyleField.TypeName) == null)
                fieldsNode.AppendChild(_pStyleField.ToXml(doc));
            if (_pdsField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _pdsField.TypeName) == null)
                fieldsNode.AppendChild(_pdsField.ToXml(doc));
            if (_codeField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _codeField.TypeName) == null)
                fieldsNode.AppendChild(_codeField.ToXml(doc));
            if (_mapNoField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _mapNoField.TypeName) == null)
                fieldsNode.AppendChild(_mapNoField.ToXml(doc));
            if (_useStatusField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _useStatusField.TypeName) == null)
                fieldsNode.AppendChild(_useStatusField.ToXml(doc));
            if (_bCodeField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _bCodeField.TypeName) == null)
                fieldsNode.AppendChild(_bCodeField.ToXml(doc));
            if (_rotangField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _rotangField.TypeName) == null)
                fieldsNode.AppendChild(_rotangField.ToXml(doc));
            if (_roadCodeField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _roadCodeField.TypeName) == null)
                fieldsNode.AppendChild(_roadCodeField.ToXml(doc));
            if (_mDateField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _mDateField.TypeName) == null)
                fieldsNode.AppendChild(_mDateField.ToXml(doc));
            if (_pcjhField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _pcjhField.TypeName) == null)
                fieldsNode.AppendChild(_pcjhField.ToXml(doc));
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
