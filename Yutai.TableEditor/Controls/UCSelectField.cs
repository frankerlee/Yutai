using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.Plugins.TableEditor.Controls
{
    public partial class UCSelectField : UserControl
    {
        public event EventHandler SelectComlateEvent;

        private IFeatureClass _featureClass;
        private IFields _fields;
        private IFeature _feature;
        private IField _field;
        private int _fieldIndex;

        public UCSelectField()
        {
            InitializeComponent();
        }

        public UCSelectField(IFeatureClass featureClass)
        {
            InitializeComponent();
            this.FeatureClass = featureClass;
        }

        public UCSelectField(IFields fields)
        {
            InitializeComponent();
            this.Fields = fields;
        }

        public UCSelectField(IFeature feature)
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
            SetItems(comboBoxField, _fields);
            comboBoxField.Text = null;
        }

        private void SetItems(ComboBox comboBox,IFields fields)
        {
            if (comboBox.Items.Count > 0)
                comboBox.Items.Clear();
            int count = fields.FieldCount;
            for (int i = 0; i < count; i++)
            {
                IField pField = fields.Field[i];
                if (pField != null && pField.Editable)
                {
                    comboBox.Items.Add(pField.Name);
                }
            }
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
            SelectComlateEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
