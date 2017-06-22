using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Location;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class frmHatchAlignment
    {
        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

       
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHatchAlignment));
            this.rdoLeft = new RadioButton();
            this.rdoCenter = new RadioButton();
            this.rdoRight = new RadioButton();
            this.label1 = new Label();
            this.label2 = new Label();
            this.txtSupplementalAngle = new SpinEdit();
            this.label3 = new Label();
            this.btnOK = new SimpleButton();
            this.simpleButton2 = new SimpleButton();
            this.txtSupplementalAngle.Properties.BeginInit();
            base.SuspendLayout();
            this.rdoLeft.Location = new Point(16, 40);
            this.rdoLeft.Name = "rdoLeft";
            this.rdoLeft.Size = new Size(64, 16);
            this.rdoLeft.TabIndex = 0;
            this.rdoLeft.Text = "左边";
            this.rdoCenter.Location = new Point(88, 40);
            this.rdoCenter.Name = "rdoCenter";
            this.rdoCenter.Size = new Size(64, 16);
            this.rdoCenter.TabIndex = 1;
            this.rdoCenter.Text = "中心";
            this.rdoRight.Location = new Point(160, 40);
            this.rdoRight.Name = "rdoRight";
            this.rdoRight.Size = new Size(64, 16);
            this.rdoRight.TabIndex = 2;
            this.rdoRight.Text = "右边";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 64);
            this.label1.Name = "label1";
            this.label1.Size = new Size(257, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "刻度线垂直于基线。补角能被加到计算的角度上";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 8);
            this.label2.Name = "label2";
            this.label2.Size = new Size(221, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "刻度线能在基线的左边、右边或基线中心";
            int[] bits = new int[4];
            this.txtSupplementalAngle.EditValue = new decimal(bits);
            this.txtSupplementalAngle.Location = new Point(16, 88);
            this.txtSupplementalAngle.Name = "txtSupplementalAngle";
            this.txtSupplementalAngle.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtSupplementalAngle.Size = new Size(88, 21);
            this.txtSupplementalAngle.TabIndex = 5;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(128, 96);
            this.label3.Name = "label3";
            this.label3.Size = new Size(17, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "度";
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new Point(168, 120);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(56, 24);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton2.Location = new Point(248, 120);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new Size(56, 24);
            this.simpleButton2.TabIndex = 8;
            this.simpleButton2.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(330, 157);
            base.Controls.Add(this.simpleButton2);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.txtSupplementalAngle);
            base.Controls.Add(this.rdoRight);
            base.Controls.Add(this.rdoCenter);
            base.Controls.Add(this.rdoLeft);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmHatchAlignment";
            this.Text = "刻度线方向";
            base.Load += new EventHandler(this.frmHatchAlignment_Load);
            this.txtSupplementalAngle.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private SimpleButton btnOK;
        private Label label1;
        private Label label2;
        private Label label3;
        private RadioButton rdoCenter;
        private RadioButton rdoLeft;
        private RadioButton rdoRight;
        private SimpleButton simpleButton2;
        private SpinEdit txtSupplementalAngle;
    }
}