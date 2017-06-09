using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.ArcGIS.Controls.Controls.ConfigSetting
{
    internal class frmLayerInfo : Form
    {
        private SimpleButton btnChangeVersion;
        private IContainer components = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private SimpleButton simpleButton1;
        private TextEdit txtLayerName;
        private TextEdit txtMaxScale;
        private TextEdit txtMinScale;

        public frmLayerInfo()
        {
            this.InitializeComponent();
            this.LayerName = "";
            this.MinScale = 0.0;
            this.MaxScale = 0.0;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmLayerInfo_Load(object sender, EventArgs e)
        {
            this.txtLayerName.Text = this.LayerName;
            this.txtMinScale.Text = this.MinScale.ToString();
            this.txtMaxScale.Text = this.MaxScale.ToString();
        }

        private void InitializeComponent()
        {
            this.btnChangeVersion = new SimpleButton();
            this.txtMaxScale = new TextEdit();
            this.txtMinScale = new TextEdit();
            this.txtLayerName = new TextEdit();
            this.label3 = new Label();
            this.label2 = new Label();
            this.label1 = new Label();
            this.simpleButton1 = new SimpleButton();
            this.txtMaxScale.Properties.BeginInit();
            this.txtMinScale.Properties.BeginInit();
            this.txtLayerName.Properties.BeginInit();
            base.SuspendLayout();
            this.btnChangeVersion.DialogResult = DialogResult.Cancel;
            this.btnChangeVersion.Location = new Point(0xea, 0x5c);
            this.btnChangeVersion.Name = "btnChangeVersion";
            this.btnChangeVersion.Size = new Size(0x30, 0x18);
            this.btnChangeVersion.TabIndex = 20;
            this.btnChangeVersion.Text = "取消";
            this.txtMaxScale.EditValue = "0";
            this.txtMaxScale.Location = new Point(0x69, 0x41);
            this.txtMaxScale.Name = "txtMaxScale";
            this.txtMaxScale.Size = new Size(0xac, 0x15);
            this.txtMaxScale.TabIndex = 0x13;
            this.txtMinScale.EditValue = "0";
            this.txtMinScale.Location = new Point(0x69, 0x26);
            this.txtMinScale.Name = "txtMinScale";
            this.txtMinScale.Size = new Size(0xac, 0x15);
            this.txtMinScale.TabIndex = 0x12;
            this.txtLayerName.EditValue = "";
            this.txtLayerName.Location = new Point(0x44, 6);
            this.txtLayerName.Name = "txtLayerName";
            this.txtLayerName.Size = new Size(0xd1, 0x15);
            this.txtLayerName.TabIndex = 0x11;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(12, 70);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x4d, 12);
            this.label3.TabIndex = 0x10;
            this.label3.Text = "最大比例: 1:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 0x2b);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x4d, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "最小比例: 1:";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x2f, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "图层名:";
            this.simpleButton1.Location = new Point(0xa3, 0x5c);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new Size(0x30, 0x18);
            this.simpleButton1.TabIndex = 0x15;
            this.simpleButton1.Text = "确定";
            this.simpleButton1.Click += new EventHandler(this.simpleButton1_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x125, 0x8a);
            base.Controls.Add(this.simpleButton1);
            base.Controls.Add(this.btnChangeVersion);
            base.Controls.Add(this.txtMaxScale);
            base.Controls.Add(this.txtMinScale);
            base.Controls.Add(this.txtLayerName);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmLayerInfo";
            this.Text = "图层信息";
            base.Load += new EventHandler(this.frmLayerInfo_Load);
            this.txtMaxScale.Properties.EndInit();
            this.txtMinScale.Properties.EndInit();
            this.txtLayerName.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (this.txtLayerName.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入层名");
            }
            else
            {
                try
                {
                    this.MinScale = double.Parse(this.txtMinScale.Text);
                    this.MaxScale = double.Parse(this.txtMaxScale.Text);
                }
                catch
                {
                    MessageBox.Show("请输入数字!");
                    return;
                }
                this.LayerName = this.txtLayerName.Text.Trim();
                base.DialogResult = DialogResult.OK;
                base.Close();
            }
        }

        public string LayerName { get; set; }

        public double MaxScale { get; set; }

        public double MinScale { get; set; }
    }
}

