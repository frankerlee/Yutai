using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Location;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class HatchClassCtrl : UserControl
    {
        private bool bool_0 = false;
        private Container container_0 = null;
        private IFields ifields_0 = null;
        private IHatchClass ihatchClass_0 = null;

        public event ValueChangedHandler ValueChanged;

        public HatchClassCtrl()
        {
            this.InitializeComponent();
        }

        private void cboFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IHatchInputValue hatchInterval = this.ihatchClass_0.HatchInterval;
                if (this.cboFields.SelectedItem is FieldWrap)
                {
                    hatchInterval.Field = (this.cboFields.SelectedItem as FieldWrap).Field.Name;
                }
            }
        }

 private void HatchClassCtrl_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        public void Init()
        {
            if (this.ihatchClass_0 != null)
            {
                this.bool_0 = false;
                IHatchInputValue hatchInterval = this.ihatchClass_0.HatchInterval;
                if (hatchInterval.Value is DBNull)
                {
                    this.txtHatchInterval.Text = "0";
                }
                else
                {
                    this.txtHatchInterval.Text = hatchInterval.Value.ToString();
                }
                if (hatchInterval.Field == "")
                {
                    this.radioGroup1.SelectedIndex = 0;
                }
                else
                {
                    this.radioGroup1.SelectedIndex = 1;
                }
                this.txtHatchInterval.Enabled = this.radioGroup1.SelectedIndex == 0;
                this.cboFields.Enabled = this.radioGroup1.SelectedIndex == 1;
                int num = -1;
                for (int i = 0; i < this.ifields_0.FieldCount; i++)
                {
                    IField field = this.ifields_0.get_Field(i);
                    if ((((field.Type == esriFieldType.esriFieldTypeInteger) || (field.Type == esriFieldType.esriFieldTypeSmallInteger)) || (field.Type == esriFieldType.esriFieldTypeDouble)) || (field.Type == esriFieldType.esriFieldTypeSingle))
                    {
                        this.cboFields.Properties.Items.Add(new FieldWrap(field));
                        if (field.Name == hatchInterval.Field)
                        {
                            num = this.cboFields.Properties.Items.Count - 1;
                        }
                    }
                }
                if (num == -1)
                {
                    num = 0;
                }
                if (this.cboFields.Properties.Items.Count > 0)
                {
                    this.cboFields.SelectedIndex = num;
                }
                this.bool_0 = true;
            }
        }

 private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IHatchInputValue hatchInterval;
                if (this.radioGroup1.SelectedIndex == 0)
                {
                    hatchInterval = this.ihatchClass_0.HatchInterval;
                    hatchInterval.Field = "";
                    try
                    {
                        hatchInterval.Value = double.Parse(this.txtHatchInterval.Text);
                    }
                    catch
                    {
                    }
                }
                else
                {
                    hatchInterval = this.ihatchClass_0.HatchInterval;
                    if (this.cboFields.SelectedItem is FieldWrap)
                    {
                        hatchInterval.Field = (this.cboFields.SelectedItem as FieldWrap).Field.Name;
                    }
                    hatchInterval.Value = 0;
                }
                this.txtHatchInterval.Enabled = this.radioGroup1.SelectedIndex == 0;
                this.cboFields.Enabled = this.radioGroup1.SelectedIndex == 1;
            }
        }

        private void txtHatchInterval_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.ihatchClass_0.HatchInterval.Value = double.Parse(this.txtHatchInterval.Text);
            }
        }

        public IFields Fields
        {
            set
            {
                this.ifields_0 = value;
            }
        }

        public IHatchClass HatchClass
        {
            get
            {
                return this.ihatchClass_0;
            }
            set
            {
                this.ihatchClass_0 = value;
            }
        }

        internal partial class FieldWrap
        {
            private IField ifield_0 = null;

            public FieldWrap(IField ifield_1)
            {
                this.ifield_0 = ifield_1;
            }

            public override string ToString()
            {
                return this.ifield_0.AliasName;
            }

            public IField Field
            {
                get
                {
                    return this.ifield_0;
                }
            }
        }

        public delegate void ValueChangedHandler(object sender, EventArgs e);
    }
}

