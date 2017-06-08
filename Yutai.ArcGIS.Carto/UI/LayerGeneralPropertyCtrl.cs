using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.UI
{
    public class LayerGeneralPropertyCtrl : UserControl, ILayerAndStandaloneTablePropertyPage
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private CheckEdit chkVisible;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private ILayer ilayer_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private RadioGroup rdoDisplayScale;
        private TextEdit txtLayerName;
        private TextEdit txtMaxScale;
        private TextEdit txtMinScale;

        public LayerGeneralPropertyCtrl()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                this.ilayer_0.Name = this.txtLayerName.Text;
                this.ilayer_0.Visible = this.chkVisible.Checked;
                if (this.rdoDisplayScale.SelectedIndex == 0)
                {
                    this.ilayer_0.MinimumScale = 0.0;
                    this.ilayer_0.MaximumScale = 0.0;
                }
                else
                {
                    try
                    {
                        this.ilayer_0.MinimumScale = Convert.ToDouble(this.txtMinScale.Text);
                    }
                    catch
                    {
                    }
                    try
                    {
                        this.ilayer_0.MaximumScale = Convert.ToDouble(this.txtMaxScale.Text);
                    }
                    catch
                    {
                    }
                }
            }
            return true;
        }

        private void chkVisible_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
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
            this.label1 = new Label();
            this.txtLayerName = new TextEdit();
            this.chkVisible = new CheckEdit();
            this.groupBox1 = new GroupBox();
            this.txtMaxScale = new TextEdit();
            this.txtMinScale = new TextEdit();
            this.label3 = new Label();
            this.label2 = new Label();
            this.rdoDisplayScale = new RadioGroup();
            this.txtLayerName.Properties.BeginInit();
            this.chkVisible.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            this.txtMaxScale.Properties.BeginInit();
            this.txtMinScale.Properties.BeginInit();
            this.rdoDisplayScale.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(60, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "图层名称:";
            this.txtLayerName.EditValue = "";
            this.txtLayerName.Location = new Point(0x58, 8);
            this.txtLayerName.Name = "txtLayerName";
            this.txtLayerName.Size = new Size(0xe0, 0x17);
            this.txtLayerName.TabIndex = 1;
            this.txtLayerName.EditValueChanged += new EventHandler(this.txtLayerName_EditValueChanged);
            this.chkVisible.Location = new Point(0x148, 8);
            this.chkVisible.Name = "chkVisible";
            this.chkVisible.Properties.Caption = "可见";
            this.chkVisible.Size = new Size(0x38, 0x13);
            this.chkVisible.TabIndex = 2;
            this.chkVisible.CheckedChanged += new EventHandler(this.chkVisible_CheckedChanged);
            this.groupBox1.Controls.Add(this.txtMaxScale);
            this.groupBox1.Controls.Add(this.txtMinScale);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.rdoDisplayScale);
            this.groupBox1.Location = new Point(0x10, 0x30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(360, 160);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "比例范围";
            this.txtMaxScale.EditValue = "";
            this.txtMaxScale.Location = new Point(0x60, 0x70);
            this.txtMaxScale.Name = "txtMaxScale";
            this.txtMaxScale.Size = new Size(240, 0x17);
            this.txtMaxScale.TabIndex = 4;
            this.txtMaxScale.EditValueChanged += new EventHandler(this.txtMaxScale_EditValueChanged);
            this.txtMinScale.EditValue = "";
            this.txtMinScale.Location = new Point(0x60, 80);
            this.txtMinScale.Name = "txtMinScale";
            this.txtMinScale.Size = new Size(240, 0x17);
            this.txtMinScale.TabIndex = 3;
            this.txtMinScale.EditValueChanged += new EventHandler(this.txtMinScale_EditValueChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x18, 0x70);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x42, 0x11);
            this.label3.TabIndex = 2;
            this.label3.Text = "放大超过1:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x18, 80);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x42, 0x11);
            this.label2.TabIndex = 1;
            this.label2.Text = "缩小超过1:";
            this.rdoDisplayScale.Location = new Point(8, 0x18);
            this.rdoDisplayScale.Name = "rdoDisplayScale";
            this.rdoDisplayScale.Properties.Appearance.BackColor = SystemColors.InactiveBorder;
            this.rdoDisplayScale.Properties.Appearance.Options.UseBackColor = true;
            this.rdoDisplayScale.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoDisplayScale.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "在所有比例尺下都显示"), new RadioGroupItem(null, "不显示图层，当") });
            this.rdoDisplayScale.Size = new Size(0xa8, 0x30);
            this.rdoDisplayScale.TabIndex = 0;
            this.rdoDisplayScale.SelectedIndexChanged += new EventHandler(this.rdoDisplayScale_SelectedIndexChanged);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.chkVisible);
            base.Controls.Add(this.txtLayerName);
            base.Controls.Add(this.label1);
            base.Name = "LayerGeneralPropertyCtrl";
            base.Size = new Size(0x198, 240);
            base.Load += new EventHandler(this.LayerGeneralPropertyCtrl_Load);
            this.txtLayerName.Properties.EndInit();
            this.chkVisible.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.txtMaxScale.Properties.EndInit();
            this.txtMinScale.Properties.EndInit();
            this.rdoDisplayScale.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void LayerGeneralPropertyCtrl_Load(object sender, EventArgs e)
        {
            this.method_0();
            this.bool_0 = true;
        }

        private void method_0()
        {
            this.txtLayerName.Text = this.ilayer_0.Name;
            this.chkVisible.Checked = this.ilayer_0.Visible;
            this.txtMaxScale.Text = this.ilayer_0.MaximumScale.ToString();
            this.txtMinScale.Text = this.ilayer_0.MinimumScale.ToString();
            if ((this.ilayer_0.MaximumScale == 0.0) && (this.ilayer_0.MinimumScale == 0.0))
            {
                this.rdoDisplayScale.SelectedIndex = 0;
            }
            else
            {
                this.rdoDisplayScale.SelectedIndex = 1;
            }
            this.rdoDisplayScale_SelectedIndexChanged(this, null);
        }

        private void rdoDisplayScale_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rdoDisplayScale.SelectedIndex == 0)
            {
                this.txtMaxScale.Enabled = false;
                this.txtMinScale.Enabled = false;
            }
            else
            {
                this.txtMaxScale.Enabled = true;
                this.txtMinScale.Enabled = true;
            }
            if (this.bool_0)
            {
                this.bool_1 = true;
            }
        }

        private void txtLayerName_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
            }
        }

        private void txtMaxScale_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
            }
        }

        private void txtMinScale_EditValueChanged(object sender, EventArgs e)
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
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_1;
            }
        }

        public ILayer Layer
        {
            set
            {
                this.ilayer_0 = value;
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

