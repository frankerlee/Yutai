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
    public class LabelExpressionSetPropertyPage : UserControl
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private SimpleButton btnAppend;
        private ComboBoxEdit cboExpressionParser;
        private CheckEdit checkEdit1;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private IAnnotationExpressionEngine iannotationExpressionEngine_0 = null;
        private ILabelEngineLayerProperties ilabelEngineLayerProperties_0;
        private Label label1;
        private ListView listView1;
        public IGeoFeatureLayer m_GeoFeatureLayer = null;
        private string string_0 = "";
        private MemoEdit txtExpression;

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

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_2);
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.btnAppend = new SimpleButton();
            this.listView1 = new ListView();
            this.groupBox2 = new GroupBox();
            this.label1 = new Label();
            this.cboExpressionParser = new ComboBoxEdit();
            this.txtExpression = new MemoEdit();
            this.checkEdit1 = new CheckEdit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.cboExpressionParser.Properties.BeginInit();
            this.txtExpression.Properties.BeginInit();
            this.checkEdit1.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.btnAppend);
            this.groupBox1.Controls.Add(this.listView1);
            this.groupBox1.Location = new Point(0x10, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x100, 0x90);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "标注字段";
            this.btnAppend.Location = new Point(0x18, 0x70);
            this.btnAppend.Name = "btnAppend";
            this.btnAppend.Size = new Size(0x40, 0x18);
            this.btnAppend.TabIndex = 1;
            this.btnAppend.Text = "添加";
            this.btnAppend.Click += new EventHandler(this.btnAppend_Click);
            this.listView1.Location = new Point(0x10, 0x18);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0xe8, 80);
            this.listView1.TabIndex = 0;
            this.listView1.View = View.SmallIcon;
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cboExpressionParser);
            this.groupBox2.Controls.Add(this.txtExpression);
            this.groupBox2.Location = new Point(8, 0xb8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x108, 0x98);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "表达式";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x18, 0x70);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x30, 0x11);
            this.label1.TabIndex = 2;
            this.label1.Text = "解析器:";
            this.cboExpressionParser.EditValue = "VBScript";
            this.cboExpressionParser.Location = new Point(80, 0x70);
            this.cboExpressionParser.Name = "cboExpressionParser";
            this.cboExpressionParser.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboExpressionParser.Properties.Enabled = false;
            this.cboExpressionParser.Properties.Items.AddRange(new object[] { "JScript", "VBScript" });
            this.cboExpressionParser.Size = new Size(0x68, 0x17);
            this.cboExpressionParser.TabIndex = 1;
            this.cboExpressionParser.SelectedIndexChanged += new EventHandler(this.cboExpressionParser_SelectedIndexChanged);
            this.txtExpression.EditValue = "";
            this.txtExpression.Location = new Point(0x10, 0x18);
            this.txtExpression.Name = "txtExpression";
            this.txtExpression.Size = new Size(0xe0, 0x48);
            this.txtExpression.TabIndex = 0;
            this.txtExpression.EditValueChanged += new EventHandler(this.txtExpression_EditValueChanged);
            this.checkEdit1.Location = new Point(0xb0, 160);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "高级";
            this.checkEdit1.Size = new Size(0x48, 0x13);
            this.checkEdit1.TabIndex = 2;
            this.checkEdit1.CheckedChanged += new EventHandler(this.checkEdit1_CheckedChanged);
            base.Controls.Add(this.checkEdit1);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "LabelExpressionSetPropertyPage";
            base.Size = new Size(0x128, 0x158);
            base.Load += new EventHandler(this.LabelExpressionSetPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.cboExpressionParser.Properties.EndInit();
            this.txtExpression.Properties.EndInit();
            this.checkEdit1.Properties.EndInit();
            base.ResumeLayout(false);
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

