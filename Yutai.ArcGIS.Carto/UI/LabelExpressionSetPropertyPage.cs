using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class LabelExpressionSetPropertyPage : UserControl
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Container container_0 = null;
        private IAnnotationExpressionEngine iannotationExpressionEngine_0 = null;
        public IGeoFeatureLayer m_GeoFeatureLayer = null;
        private string string_0 = "";

        public LabelExpressionSetPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            this.string_0 = this.txtExpression.Text;
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedIndices.Count > 0)
            {
                string text = this.listView1.SelectedItems[0].Text;
                if (this.bool_1)
                {
                    string[] strArray = this.string_0.Split(new char[] { this.iannotationExpressionEngine_0.AppendCode.Trim()[0] });
                    string str2 = strArray[0];
                    for (int i = 1; i < strArray.Length; i++)
                    {
                        str2 = str2 + this.iannotationExpressionEngine_0.AppendCode + "\" \"" + this.iannotationExpressionEngine_0.AppendCode + strArray[i];
                    }
                    str2 = str2 + this.iannotationExpressionEngine_0.AppendCode + "\" \"" + this.iannotationExpressionEngine_0.AppendCode + "[" + text + "]";
                    this.string_0 = str2;
                    this.txtExpression.Text = str2;
                }
                else
                {
                    MessageBox.Show("高级模式无法添加数据");
                }
            }
        }

        private void cboExpressionParser_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                string str;
                int num;
                this.bool_1 = !this.checkEdit1.Checked;
                if (this.bool_1)
                {
                    try
                    {
                        IAnnotationExpressionParser parser = this.iannotationExpressionEngine_0.SetExpression(this.iannotationExpressionEngine_0.AppendCode, this.string_0);
                        str = "[" + parser.get_Attribute(0) + "]";
                        num = 1;
                        while (num < parser.AttributeCount)
                        {
                            str = str + this.iannotationExpressionEngine_0.AppendCode + "\" \"" + this.iannotationExpressionEngine_0.AppendCode + "[" + parser.get_Attribute(num) + "]";
                            num++;
                        }
                        this.string_0 = str;
                        this.txtExpression.Text = str;
                    }
                    catch
                    {
                    }
                }
                else
                {
                    string[] strArray2 = this.string_0.Split(new char[] { this.iannotationExpressionEngine_0.AppendCode.Trim()[0] });
                    str = strArray2[0];
                    for (num = 1; num < strArray2.Length; num++)
                    {
                        str = str + ", " + strArray2[num];
                    }
                    this.string_0 = this.iannotationExpressionEngine_0.CreateFunction("FindLabel", str, this.string_0);
                    this.txtExpression.Text = this.string_0;
                }
            }
        }

 private void LabelExpressionSetPropertyPage_Load(object sender, EventArgs e)
        {
            ITable table = this.method_0();
            if (table != null)
            {
                IFields fields = table.Fields;
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    IField field = fields.get_Field(i);
                    if (((field.Type != esriFieldType.esriFieldTypeBlob) && (field.Type != esriFieldType.esriFieldTypeGeometry)) && (field.Type != esriFieldType.esriFieldTypeRaster))
                    {
                        this.listView1.Items.Add(field.Name);
                    }
                }
                this.txtExpression.Text = this.string_0;
                this.checkEdit1.Checked = !this.bool_1;
                if (this.iannotationExpressionEngine_0 != null)
                {
                    this.cboExpressionParser.Text = this.iannotationExpressionEngine_0.Name;
                }
                this.bool_0 = true;
            }
        }

        private ITable method_0()
        {
            if (this.m_GeoFeatureLayer == null)
            {
                return null;
            }
            if (this.m_GeoFeatureLayer is IDisplayTable)
            {
                return (this.m_GeoFeatureLayer as IDisplayTable).DisplayTable;
            }
            if (this.m_GeoFeatureLayer is IAttributeTable)
            {
                return (this.m_GeoFeatureLayer as IAttributeTable).AttributeTable;
            }
            return (this.m_GeoFeatureLayer.FeatureClass as ITable);
        }

        private void txtExpression_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.string_0 = this.txtExpression.Text;
            }
        }

        public IAnnotationExpressionEngine AnnotationExpressionEngine
        {
            get
            {
                return this.iannotationExpressionEngine_0;
            }
            set
            {
                this.iannotationExpressionEngine_0 = value;
            }
        }

        public IGeoFeatureLayer GeoFeatureLayer
        {
            set
            {
                this.m_GeoFeatureLayer = value;
            }
        }

        public bool IsExpressionSimple
        {
            get
            {
                return this.bool_1;
            }
            set
            {
                this.bool_1 = value;
            }
        }

        public ILabelEngineLayerProperties LabelEngineLayerProp
        {
            set
            {
                this.ilabelEngineLayerProperties_0 = value;
            }
        }

        public string LabelExpression
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }
    }
}

