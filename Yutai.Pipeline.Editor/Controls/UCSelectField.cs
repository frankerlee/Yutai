using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using Yutai.UI.Helpers;

namespace Yutai.Pipeline.Editor.Controls
{
    public partial class UcSelectField : UserControl
    {
        public event SelectComplateHandler SelectComlateEvent;

        private IFeatureClass _featureClass;
        private IFields _fields;
        private IFeature _feature;
        private IField _field;
        private int _fieldIndex;

        public UcSelectField()
        {
            InitializeComponent();
        }

        public UcSelectField(IFeatureClass featureClass)
        {
            InitializeComponent();
            this.FeatureClass = featureClass;
        }

        public UcSelectField(IFields fields)
        {
            InitializeComponent();
            this.Fields = fields;
        }

        public UcSelectField(IFeature feature)
        {
            InitializeComponent();
            this.Feature = feature;
        }

        public IFeatureClass FeatureClass
        {
            private get { return _featureClass; }
            set
            {
                _featureClass = value;
                if (_featureClass != null)
                    Fields = _featureClass.Fields;
            }
        }

        /// <summary>
        /// 所选字段对象
        /// </summary>
        public IField Field
        {
            get { return _field; }
            private set
            {
                _field = value;
                OnSelectComlateEvent();
            }
        }

        public IFields Fields
        {
            private get { return _fields; }
            set
            {
                _fields = value;
                LoadFields();
            }
        }

        public IFeature Feature
        {
            private get { return _feature; }
            set
            {
                _feature = value;
                if (_feature != null)
                    Fields = _feature.Fields;
            }
        }

        [Browsable(true)]
        [Description("是否显示文本"), Category("扩展"), DefaultValue(false)]
        public bool VisibleLabel
        {
            get { return !this.splitContainer1.Panel1Collapsed; }
            set { this.splitContainer1.Panel1Collapsed = !value; }
        }

        [Browsable(true)]
        [Description("与控件关联的文本"), Category("扩展"), DefaultValue(null)]
        public string Label
        {
            get { return this.label1.Text; }
            set
            {
                this.label1.Text = value;
                this.splitContainer1.SplitterDistance = this.label1.Width;
            }
        }

        [Browsable(true)]
        [Description("与控件关联的文本"), Category("扩展"), DefaultValue(0)]
        public int LabelWidth
        {
            get { return this.splitContainer1.SplitterDistance; }
            set
            {
                this.splitContainer1.SplitterDistance = value;
            }
        }

        [Browsable(true)]
        [Description("文本的对齐方式"), Category("扩展"), DefaultValue(ContentAlignment.MiddleLeft)]
        public ContentAlignment LabelAlign
        {
            get { return this.label1.TextAlign; }
            set { this.label1.TextAlign = value; }
        }


        /// <summary>
        /// 所选字段的索引值
        /// </summary>
        public int FieldIndex
        {
            get { return _fieldIndex; }
            private set { _fieldIndex = value; }
        }

        private void LoadFields()
        {
            if (_fields == null)
                return;
            ComboBoxHelper.AddItemsFromFields(_fields, comboBoxField);
            comboBoxField.Text = null;
        }

        private void comboBoxField_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxField.SelectedItem == null || string.IsNullOrEmpty(comboBoxField.SelectedItem.ToString()))
                return;

            string fieldName = comboBoxField.SelectedItem.ToString();
            this.FieldIndex = _fields.FindField(fieldName);
            this.Field = _fields.Field[_fieldIndex];
        }

        protected virtual void OnSelectComlateEvent()
        {
            var handler = SelectComlateEvent;
            if (handler != null) handler();
        }
    }
}
