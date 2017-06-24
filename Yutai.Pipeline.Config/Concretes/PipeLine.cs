// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  PipeLine.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/23  09:19
// 更新时间 :  2017/06/23  09:19

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Pipeline.Config.Helpers;
using Yutai.Pipeline.Config.Interfaces;

namespace Yutai.Pipeline.Config.Concretes
{
    public class PipeLine : IPipeLine
    {
        private string _typeName;
        private IYTField _noField;
        private IYTField _sPointField;
        private IYTField _ePointField;
        private IYTField _sDeepField;
        private IYTField _eDeepField;
        private IYTField _shField;
        private IYTField _ehField;
        private IYTField _codeField;
        private IYTField _materialField;
        private IYTField _dTypeField;
        private IYTField _lineStyleField;
        private IYTField _dsField;
        private IYTField _sectionSizeField;
        private IYTField _pdmField;
        private IYTField _pipeNatureField;
        private IYTField _msrqField;
        private IYTField _mDateField;
        private IYTField _useStatusField;
        private IYTField _bCodeField;
        private IYTField _roadCodeField;
        private IYTField _cabCountField;
        private IYTField _volPresField;
        private IYTField _holeCountField;
        private IYTField _holeUsedField;
        private IYTField _flowDField;
        private IYTField _remarkField;
        private string _name;
        private string _aliasName;
        private bool _visible;
        private string _templateName;
        private IPipelineTemplate _template;

        private List<IYTField> _fields;
        private IFeatureLayer _featureLayer;
        private string _heightTypeName;
        private enumPipelineHeightType _heightType;

        public PipeLine()
        {
            _fields = new List<IYTField>();
        }

        public PipeLine(XmlNode xmlNode)
        {
            _fields = new List<IYTField>();
            ReadFromXml(xmlNode);
        }

        public PipeLine(XmlNode xmlNode, IPipelineTemplate template)
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

        public string HeightTypeName
        {
            get { return _heightTypeName; }
            set { _heightTypeName = value; }
        }

        public enumPipelineHeightType HeightType
        {
            get { return _heightType; }
            set { _heightType = value; }
        }

        public IYTField NoField
        {
            get { return _noField; }
            set { _noField = value; }
        }

        public IYTField SPointField
        {
            get { return _sPointField; }
            set { _sPointField = value; }
        }

        public IYTField EPointField
        {
            get { return _ePointField; }
            set { _ePointField = value; }
        }

        public IYTField SDeepField
        {
            get { return _sDeepField; }
            set { _sDeepField = value; }
        }

        public IYTField EDeepField
        {
            get { return _eDeepField; }
            set { _eDeepField = value; }
        }

        public IYTField SHField
        {
            get { return _shField; }
            set { _shField = value; }
        }

        public IYTField EHField
        {
            get { return _ehField; }
            set { _ehField = value; }
        }

        public IYTField CodeField
        {
            get { return _codeField; }
            set { _codeField = value; }
        }

        public IYTField MaterialField
        {
            get { return _materialField; }
            set { _materialField = value; }
        }

        public IYTField DTypeField
        {
            get { return _dTypeField; }
            set { _dTypeField = value; }
        }

        public IYTField LineStyleField
        {
            get { return _lineStyleField; }
            set { _lineStyleField = value; }
        }

        public IYTField DSField
        {
            get { return _dsField; }
            set { _dsField = value; }
        }

        public IYTField SectionSizeField
        {
            get { return _sectionSizeField; }
            set { _sectionSizeField = value; }
        }

        public IYTField PDMField
        {
            get { return _pdmField; }
            set { _pdmField = value; }
        }

        public IYTField PipeNatureField
        {
            get { return _pipeNatureField; }
            set { _pipeNatureField = value; }
        }

        public IYTField MSRQField
        {
            get { return _msrqField; }
            set { _msrqField = value; }
        }

        public IYTField MDateField
        {
            get { return _mDateField; }
            set { _mDateField = value; }
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

        public IYTField RoadCodeField
        {
            get { return _roadCodeField; }
            set { _roadCodeField = value; }
        }

        public IYTField CabCountField
        {
            get { return _cabCountField; }
            set { _cabCountField = value; }
        }

        public IYTField VolPresField
        {
            get { return _volPresField; }
            set { _volPresField = value; }
        }

        public IYTField HoleCountField
        {
            get { return _holeCountField; }
            set { _holeCountField = value; }
        }

        public IYTField HoleUsedField
        {
            get { return _holeUsedField; }
            set { _holeUsedField = value; }
        }

        public IYTField FlowDField
        {
            get { return _flowDField; }
            set { _flowDField = value; }
        }

        public IYTField RemarkField
        {
            get { return _remarkField; }
            set { _remarkField = value; }
        }
        
        public void AutoAssembly(IFeatureLayer pLayer)
        {
            _noField?.LoadFromField(pLayer.FeatureClass.Fields);
            _sPointField?.LoadFromField(pLayer.FeatureClass.Fields);
            _ePointField?.LoadFromField(pLayer.FeatureClass.Fields);
            _sDeepField?.LoadFromField(pLayer.FeatureClass.Fields);
            _eDeepField?.LoadFromField(pLayer.FeatureClass.Fields);
            _shField?.LoadFromField(pLayer.FeatureClass.Fields);
            _ehField?.LoadFromField(pLayer.FeatureClass.Fields);
            _codeField?.LoadFromField(pLayer.FeatureClass.Fields);
            _materialField?.LoadFromField(pLayer.FeatureClass.Fields);
            _dTypeField?.LoadFromField(pLayer.FeatureClass.Fields);
            _lineStyleField?.LoadFromField(pLayer.FeatureClass.Fields);
            _dsField?.LoadFromField(pLayer.FeatureClass.Fields);
            _sectionSizeField?.LoadFromField(pLayer.FeatureClass.Fields);
            _pdmField?.LoadFromField(pLayer.FeatureClass.Fields);
            _pipeNatureField?.LoadFromField(pLayer.FeatureClass.Fields);
            _msrqField?.LoadFromField(pLayer.FeatureClass.Fields);
            _mDateField?.LoadFromField(pLayer.FeatureClass.Fields);
            _useStatusField?.LoadFromField(pLayer.FeatureClass.Fields);
            _bCodeField?.LoadFromField(pLayer.FeatureClass.Fields);
            _roadCodeField?.LoadFromField(pLayer.FeatureClass.Fields);
            _cabCountField?.LoadFromField(pLayer.FeatureClass.Fields);
            _volPresField?.LoadFromField(pLayer.FeatureClass.Fields);
            _holeCountField?.LoadFromField(pLayer.FeatureClass.Fields);
            _holeUsedField?.LoadFromField(pLayer.FeatureClass.Fields);
            _flowDField?.LoadFromField(pLayer.FeatureClass.Fields);
            _remarkField?.LoadFromField(pLayer.FeatureClass.Fields);
        }

        public void ReadFromXml(XmlNode xml)
        {
            if (xml?.Attributes == null)
                return;
            _name = xml.Attributes["Name"].Value;
            _aliasName = xml.Attributes["AliasName"].Value;
            _visible = Convert.ToBoolean(xml.Attributes["Visible"].Value);
            _heightTypeName = xml.Attributes["HeightType"].Value;
            _heightType = EnumHelper.ConvertHeightTypeFromStr(_heightTypeName);
            _templateName = xml.Attributes["Template"].Value;

            XmlNodeList nodeList = xml.SelectNodes("/LineLayer/Fields/Field");

            if (nodeList != null)
                foreach (XmlNode node in nodeList)
                {
                    IYTField field = new YTField(node);
                    _fields.Add(field);
                }
            _noField = _fields.FirstOrDefault(c => c.TypeName == "NoField");
            _sPointField = _fields.FirstOrDefault(c => c.TypeName == "SPointField");
            _ePointField = _fields.FirstOrDefault(c => c.TypeName == "EPointField");
            _sDeepField = _fields.FirstOrDefault(c => c.TypeName == "SDeepField");
            _eDeepField = _fields.FirstOrDefault(c => c.TypeName == "EDeepField");
            _shField = _fields.FirstOrDefault(c => c.TypeName == "SHField");
            _ehField = _fields.FirstOrDefault(c => c.TypeName == "EHField");
            _codeField = _fields.FirstOrDefault(c => c.TypeName == "CodeField");
            _materialField = _fields.FirstOrDefault(c => c.TypeName == "MaterialField");
            _dTypeField = _fields.FirstOrDefault(c => c.TypeName == "DTypeField");
            _lineStyleField = _fields.FirstOrDefault(c => c.TypeName == "LineStyleField");
            _dsField = _fields.FirstOrDefault(c => c.TypeName == "DSField");
            _sectionSizeField = _fields.FirstOrDefault(c => c.TypeName == "SectionSizeField");
            _pdmField = _fields.FirstOrDefault(c => c.TypeName == "PDMField");
            _pipeNatureField = _fields.FirstOrDefault(c => c.TypeName == "PipeNatureField");
            _msrqField = _fields.FirstOrDefault(c => c.TypeName == "MSRQField");
            _mDateField = _fields.FirstOrDefault(c => c.TypeName == "MDateField");
            _useStatusField = _fields.FirstOrDefault(c => c.TypeName == "UseStatusField");
            _bCodeField = _fields.FirstOrDefault(c => c.TypeName == "BCodeField");
            _roadCodeField = _fields.FirstOrDefault(c => c.TypeName == "RoadCodeField");
            _cabCountField = _fields.FirstOrDefault(c => c.TypeName == "CabCountField");
            _volPresField = _fields.FirstOrDefault(c => c.TypeName == "VolPresField");
            _holeCountField = _fields.FirstOrDefault(c => c.TypeName == "HoleCountField");
            _holeUsedField = _fields.FirstOrDefault(c => c.TypeName == "HoleUsedField");
            _flowDField = _fields.FirstOrDefault(c => c.TypeName == "FlowDField");
            _remarkField = _fields.FirstOrDefault(c => c.TypeName == "RemarkField");
        }

        public XmlNode ToXml(XmlDocument doc)
        {
            XmlNode layerNode = doc.CreateElement("LineLayer");
            XmlAttribute nameAttribute = doc.CreateAttribute("Name");
            nameAttribute.Value = _name;
            XmlAttribute aliasNameAttribute = doc.CreateAttribute("AliasName");
            aliasNameAttribute.Value = _aliasName;
            XmlAttribute templateAttribute = doc.CreateAttribute("Template");
            templateAttribute.Value = _templateName;

            XmlAttribute heightTypeAttribute = doc.CreateAttribute("HeightType");
            heightTypeAttribute.Value = EnumHelper.ConvertHeightTypeToStr(_heightType);
            XmlAttribute visibleAttribute = doc.CreateAttribute("Visible");
            visibleAttribute.Value = _visible.ToString();
            layerNode.Attributes.Append(nameAttribute);
            layerNode.Attributes.Append(aliasNameAttribute);
            layerNode.Attributes.Append(templateAttribute);
            layerNode.Attributes.Append(visibleAttribute);
            layerNode.Attributes.Append(heightTypeAttribute);

            XmlNode fieldsNode = doc.CreateElement("Fields");
            if (_noField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _noField.TypeName) == null)
                fieldsNode.AppendChild(_noField.ToXml(doc));
            if (_sPointField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _sPointField.TypeName) == null)
                fieldsNode.AppendChild(_sPointField.ToXml(doc));
            if (_ePointField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _ePointField.TypeName) == null)
                fieldsNode.AppendChild(_ePointField.ToXml(doc));
            if (_sDeepField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _sDeepField.TypeName) == null)
                fieldsNode.AppendChild(_sDeepField.ToXml(doc));
            if (_eDeepField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _eDeepField.TypeName) == null)
                fieldsNode.AppendChild(_eDeepField.ToXml(doc));
            if (_shField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _shField.TypeName) == null)
                fieldsNode.AppendChild(_shField.ToXml(doc));
            if (_ehField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _ehField.TypeName) == null)
                fieldsNode.AppendChild(_ehField.ToXml(doc));
            if (_codeField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _codeField.TypeName) == null)
                fieldsNode.AppendChild(_codeField.ToXml(doc));
            if (_materialField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _materialField.TypeName) == null)
                fieldsNode.AppendChild(_materialField.ToXml(doc));
            if (_dTypeField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _dTypeField.TypeName) == null)
                fieldsNode.AppendChild(_dTypeField.ToXml(doc));
            if (_lineStyleField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _lineStyleField.TypeName) == null)
                fieldsNode.AppendChild(_lineStyleField.ToXml(doc));
            if (_dsField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _dsField.TypeName) == null)
                fieldsNode.AppendChild(_dsField.ToXml(doc));
            if (_sectionSizeField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _sectionSizeField.TypeName) == null)
                fieldsNode.AppendChild(_sectionSizeField.ToXml(doc));
            if (_pdmField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _pdmField.TypeName) == null)
                fieldsNode.AppendChild(_pdmField.ToXml(doc));
            if (_pipeNatureField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _pipeNatureField.TypeName) == null)
                fieldsNode.AppendChild(_pipeNatureField.ToXml(doc));
            if (_msrqField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _msrqField.TypeName) == null)
                fieldsNode.AppendChild(_msrqField.ToXml(doc));
            if (_mDateField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _mDateField.TypeName) == null)
                fieldsNode.AppendChild(_mDateField.ToXml(doc));
            if (_useStatusField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _useStatusField.TypeName) == null)
                fieldsNode.AppendChild(_useStatusField.ToXml(doc));
            if (_bCodeField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _bCodeField.TypeName) == null)
                fieldsNode.AppendChild(_bCodeField.ToXml(doc));
            if (_roadCodeField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _roadCodeField.TypeName) == null)
                fieldsNode.AppendChild(_roadCodeField.ToXml(doc));
            if (_cabCountField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _cabCountField.TypeName) == null)
                fieldsNode.AppendChild(_cabCountField.ToXml(doc));
            if (_volPresField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _volPresField.TypeName) == null)
                fieldsNode.AppendChild(_volPresField.ToXml(doc));
            if (_holeCountField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _holeCountField.TypeName) == null)
                fieldsNode.AppendChild(_holeCountField.ToXml(doc));
            if (_holeUsedField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _holeUsedField.TypeName) == null)
                fieldsNode.AppendChild(_holeUsedField.ToXml(doc));
            if (_flowDField != null && _template != null && _template.Fields.FirstOrDefault(c => c.TypeName == _flowDField.TypeName) == null)
                fieldsNode.AppendChild(_flowDField.ToXml(doc));
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