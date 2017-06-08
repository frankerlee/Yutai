using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.UI
{
    public class ExtrusionPropertyPage : UserControl, ILayerAndStandaloneTablePropertyPage
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private ComboBox cboExtrusionType;
        private CheckBox chkExtrusion;
        private I3DProperties i3DProperties_0 = null;
        private IBasicMap ibasicMap_0 = null;
        private IContainer icontainer_0 = null;
        private ILayer ilayer_0 = null;
        private Label label1;
        private Label label2;
        private TextBox txtExtrusionValue;

        public ExtrusionPropertyPage()
        {
            this.InitializeComponent();
            this.Text = "Extrusion";
        }

        public bool Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                if (this.chkExtrusion.Checked)
                {
                    this.i3DProperties_0.ExtrusionType = (esriExtrusionType) (this.cboExtrusionType.SelectedIndex + 1);
                }
                else
                {
                    this.i3DProperties_0.ExtrusionType = esriExtrusionType.esriExtrusionNone;
                }
                this.i3DProperties_0.ExtrusionExpressionString = this.txtExtrusionValue.Text;
            }
            return true;
        }

        private void cboExtrusionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
            }
        }

        private void chkExtrusion_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                this.cboExtrusionType.Enabled = this.chkExtrusion.Checked;
            }
        }

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_2);
        }

        private void ExtrusionPropertyPage_Load(object sender, EventArgs e)
        {
            ILayerExtensions extensions = this.ilayer_0 as ILayerExtensions;
            this.cboExtrusionType.SelectedIndex = 0;
            for (int i = 0; i <= (extensions.ExtensionCount - 1); i++)
            {
                if (extensions.get_Extension(i) is I3DProperties)
                {
                    I3DProperties properties = extensions.get_Extension(i) as I3DProperties;
                    this.i3DProperties_0 = properties;
                    if (this.i3DProperties_0.ExtrusionType == esriExtrusionType.esriExtrusionNone)
                    {
                        this.chkExtrusion.Checked = false;
                        this.cboExtrusionType.Enabled = false;
                    }
                    else
                    {
                        this.chkExtrusion.Checked = true;
                        this.cboExtrusionType.SelectedIndex = ((int) this.i3DProperties_0.ExtrusionType) - 1;
                    }
                    this.txtExtrusionValue.Text = this.i3DProperties_0.ExtrusionExpressionString;
                    break;
                }
            }
            this.bool_0 = true;
        }

        private void InitializeComponent()
        {
            this.chkExtrusion = new CheckBox();
            this.cboExtrusionType = new ComboBox();
            this.label1 = new Label();
            this.label2 = new Label();
            this.txtExtrusionValue = new TextBox();
            base.SuspendLayout();
            this.chkExtrusion.AutoSize = true;
            this.chkExtrusion.Location = new Point(14, 13);
            this.chkExtrusion.Name = "chkExtrusion";
            this.chkExtrusion.Size = new Size(0x48, 0x10);
            this.chkExtrusion.TabIndex = 0;
            this.chkExtrusion.Text = "拉伸要素";
            this.chkExtrusion.UseVisualStyleBackColor = true;
            this.chkExtrusion.CheckedChanged += new EventHandler(this.chkExtrusion_CheckedChanged);
            this.cboExtrusionType.FormattingEnabled = true;
            this.cboExtrusionType.Items.AddRange(new object[] { "使用最小Z值", "使用最大Z值", "使用基准高程", "使用绝对值" });
            this.cboExtrusionType.Location = new Point(0x47, 0x87);
            this.cboExtrusionType.Name = "cboExtrusionType";
            this.cboExtrusionType.Size = new Size(0x79, 20);
            this.cboExtrusionType.TabIndex = 1;
            this.cboExtrusionType.SelectedIndexChanged += new EventHandler(this.cboExtrusionType_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 0x20);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x59, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "拉伸值或表达式";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 0x8a);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "拉伸方式";
            this.txtExtrusionValue.Location = new Point(14, 0x38);
            this.txtExtrusionValue.Multiline = true;
            this.txtExtrusionValue.Name = "txtExtrusionValue";
            this.txtExtrusionValue.Size = new Size(0xd9, 0x3f);
            this.txtExtrusionValue.TabIndex = 4;
            this.txtExtrusionValue.TextChanged += new EventHandler(this.txtExtrusionValue_TextChanged);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.txtExtrusionValue);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.cboExtrusionType);
            base.Controls.Add(this.chkExtrusion);
            base.Name = "ExtrusionPropertyPage";
            base.Size = new Size(0x1a8, 0xed);
            base.Load += new EventHandler(this.ExtrusionPropertyPage_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void txtExtrusionValue_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
            }
        }

        public IBasicMap FocusMap
        {
            set
            {
                this.ibasicMap_0 = value;
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_1;
            }
        }

        public object SelectItem
        {
            set
            {
                this.ilayer_0 = value as ILayer;
            }
        }
    }
}

