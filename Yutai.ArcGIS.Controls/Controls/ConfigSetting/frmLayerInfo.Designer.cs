using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.ArcGIS.Controls.Controls.ConfigSetting
{
    partial class frmLayerInfo
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
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
            this.btnChangeVersion.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnChangeVersion.Location = new Point(234, 92);
            this.btnChangeVersion.Name = "btnChangeVersion";
            this.btnChangeVersion.Size = new Size(48, 24);
            this.btnChangeVersion.TabIndex = 20;
            this.btnChangeVersion.Text = "取消";
            this.txtMaxScale.EditValue = "0";
            this.txtMaxScale.Location = new Point(105, 65);
            this.txtMaxScale.Name = "txtMaxScale";
            this.txtMaxScale.Size = new Size(172, 21);
            this.txtMaxScale.TabIndex = 19;
            this.txtMinScale.EditValue = "0";
            this.txtMinScale.Location = new Point(105, 38);
            this.txtMinScale.Name = "txtMinScale";
            this.txtMinScale.Size = new Size(172, 21);
            this.txtMinScale.TabIndex = 18;
            this.txtLayerName.EditValue = "";
            this.txtLayerName.Location = new Point(68, 6);
            this.txtLayerName.Name = "txtLayerName";
            this.txtLayerName.Size = new Size(209, 21);
            this.txtLayerName.TabIndex = 17;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(12, 70);
            this.label3.Name = "label3";
            this.label3.Size = new Size(77, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "最大比例: 1:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 43);
            this.label2.Name = "label2";
            this.label2.Size = new Size(77, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "最小比例: 1:";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(47, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "图层名:";
            this.simpleButton1.Location = new Point(163, 92);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new Size(48, 24);
            this.simpleButton1.TabIndex = 21;
            this.simpleButton1.Text = "确定";
            this.simpleButton1.Click += new EventHandler(this.simpleButton1_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(293, 138);
            base.Controls.Add(this.simpleButton1);
            base.Controls.Add(this.btnChangeVersion);
            base.Controls.Add(this.txtMaxScale);
            base.Controls.Add(this.txtMinScale);
            base.Controls.Add(this.txtLayerName);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
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

       
        private IContainer components = null;
        private SimpleButton btnChangeVersion;
        private Label label1;
        private Label label2;
        private Label label3;
        private SimpleButton simpleButton1;
        private TextEdit txtLayerName;
        private TextEdit txtMaxScale;
        private TextEdit txtMinScale;
    }
}