using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class frmAnnoScaleSet : Form
    {
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private Container container_0 = null;
        private GroupBox gropScaleset;
        private IAnnotateLayerProperties iannotateLayerProperties_0 = null;
        private Label label2;
        private Label label3;
        private Panel panelScaleSet;
        private RadioGroup rdoDisplayScale;
        private TextEdit txtMaxScale;
        private TextEdit txtMinScale;

        public frmAnnoScaleSet()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.rdoDisplayScale.SelectedIndex == 0)
            {
                this.iannotateLayerProperties_0.AnnotationMaximumScale = 0.0;
                this.iannotateLayerProperties_0.AnnotationMinimumScale = 0.0;
            }
            else
            {
                double num = 0.0;
                double num2 = 0.0;
                try
                {
                    num = double.Parse(this.txtMaxScale.Text);
                }
                catch
                {
                    MessageBox.Show("请输入数字!");
                    return;
                }
                try
                {
                    num2 = double.Parse(this.txtMinScale.Text);
                }
                catch
                {
                    MessageBox.Show("请输入数字!");
                    return;
                }
                this.iannotateLayerProperties_0.AnnotationMaximumScale = num;
                this.iannotateLayerProperties_0.AnnotationMinimumScale = num2;
            }
            base.DialogResult = DialogResult.OK;
            base.Close();
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmAnnoScaleSet_Load(object sender, EventArgs e)
        {
            if (this.iannotateLayerProperties_0 != null)
            {
                this.txtMaxScale.Text = this.iannotateLayerProperties_0.AnnotationMaximumScale.ToString();
                this.txtMinScale.Text = this.iannotateLayerProperties_0.AnnotationMinimumScale.ToString();
                if ((this.iannotateLayerProperties_0.AnnotationMinimumScale == 0.0) && (this.iannotateLayerProperties_0.AnnotationMaximumScale == 0.0))
                {
                    this.rdoDisplayScale.SelectedIndex = 0;
                }
                else
                {
                    this.rdoDisplayScale.SelectedIndex = 1;
                }
                if (this.rdoDisplayScale.SelectedIndex == 0)
                {
                    this.panelScaleSet.Enabled = false;
                }
                else
                {
                    this.panelScaleSet.Enabled = true;
                }
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAnnoScaleSet));
            this.gropScaleset = new GroupBox();
            this.panelScaleSet = new Panel();
            this.txtMaxScale = new TextEdit();
            this.txtMinScale = new TextEdit();
            this.label3 = new Label();
            this.label2 = new Label();
            this.rdoDisplayScale = new RadioGroup();
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.gropScaleset.SuspendLayout();
            this.panelScaleSet.SuspendLayout();
            this.txtMaxScale.Properties.BeginInit();
            this.txtMinScale.Properties.BeginInit();
            this.rdoDisplayScale.Properties.BeginInit();
            base.SuspendLayout();
            this.gropScaleset.Controls.Add(this.panelScaleSet);
            this.gropScaleset.Controls.Add(this.rdoDisplayScale);
            this.gropScaleset.Location = new Point(8, 8);
            this.gropScaleset.Name = "gropScaleset";
            this.gropScaleset.Size = new Size(0x180, 160);
            this.gropScaleset.TabIndex = 8;
            this.gropScaleset.TabStop = false;
            this.gropScaleset.Text = "比例范围";
            this.panelScaleSet.Controls.Add(this.txtMaxScale);
            this.panelScaleSet.Controls.Add(this.txtMinScale);
            this.panelScaleSet.Controls.Add(this.label3);
            this.panelScaleSet.Controls.Add(this.label2);
            this.panelScaleSet.Location = new Point(8, 0x48);
            this.panelScaleSet.Name = "panelScaleSet";
            this.panelScaleSet.Size = new Size(0x170, 0x48);
            this.panelScaleSet.TabIndex = 5;
            this.txtMaxScale.EditValue = "";
            this.txtMaxScale.Location = new Point(80, 0x29);
            this.txtMaxScale.Name = "txtMaxScale";
            this.txtMaxScale.Size = new Size(240, 0x15);
            this.txtMaxScale.TabIndex = 8;
            this.txtMinScale.EditValue = "";
            this.txtMinScale.Location = new Point(80, 9);
            this.txtMinScale.Name = "txtMinScale";
            this.txtMinScale.Size = new Size(240, 0x15);
            this.txtMinScale.TabIndex = 7;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0, 0x29);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x41, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "放大超过1:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0, 9);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x41, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "缩小超过1:";
            this.rdoDisplayScale.Location = new Point(8, 0x18);
            this.rdoDisplayScale.Name = "rdoDisplayScale";
            this.rdoDisplayScale.Properties.Appearance.BackColor = SystemColors.InactiveBorder;
            this.rdoDisplayScale.Properties.Appearance.Options.UseBackColor = true;
            this.rdoDisplayScale.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoDisplayScale.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "在所有比例尺下都显示"), new RadioGroupItem(null, "不显示注记，当") });
            this.rdoDisplayScale.Size = new Size(0xa8, 0x30);
            this.rdoDisplayScale.TabIndex = 0;
            this.rdoDisplayScale.SelectedIndexChanged += new EventHandler(this.rdoDisplayScale_SelectedIndexChanged);
            this.btnOK.Location = new Point(240, 0xb0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x40, 0x18);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0x138, 0xb0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x40, 0x18);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x198, 0xd5);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.gropScaleset);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = ((System.Drawing.Icon)resources.GetObject("$Icon"));
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmAnnoScaleSet";
            this.Text = "比例范围";
            base.Load += new EventHandler(this.frmAnnoScaleSet_Load);
            this.gropScaleset.ResumeLayout(false);
            this.panelScaleSet.ResumeLayout(false);
            this.panelScaleSet.PerformLayout();
            this.txtMaxScale.Properties.EndInit();
            this.txtMinScale.Properties.EndInit();
            this.rdoDisplayScale.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void rdoDisplayScale_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rdoDisplayScale.SelectedIndex == 0)
            {
                this.panelScaleSet.Enabled = false;
            }
            else
            {
                this.panelScaleSet.Enabled = true;
            }
        }

        public IAnnotateLayerProperties AnnotateLayerProperties
        {
            set
            {
                this.iannotateLayerProperties_0 = value;
            }
        }
    }
}

