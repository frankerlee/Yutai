using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    partial class CornerGridLabelPropertyPage
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
            this.label1 = new Label();
            this.chkUpperLeft = new CheckEdit();
            this.chkUpperRight = new CheckEdit();
            this.chkLowerRight = new CheckEdit();
            this.chkLowerLeft = new CheckEdit();
            this.chkUpperLeft.Properties.BeginInit();
            this.chkUpperRight.Properties.BeginInit();
            this.chkLowerRight.Properties.BeginInit();
            this.chkLowerLeft.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(153, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择哪个角显示完整的数字";
            this.chkUpperLeft.Location = new Point(32, 56);
            this.chkUpperLeft.Name = "chkUpperLeft";
            this.chkUpperLeft.Properties.Caption = "左上";
            this.chkUpperLeft.RightToLeft = RightToLeft.Yes;
            this.chkUpperLeft.Size = new Size(88, 19);
            this.chkUpperLeft.TabIndex = 1;
            this.chkUpperLeft.CheckedChanged += new EventHandler(this.chkUpperLeft_CheckedChanged);
            this.chkUpperRight.Location = new Point(128, 56);
            this.chkUpperRight.Name = "chkUpperRight";
            this.chkUpperRight.Properties.Caption = "右上";
            this.chkUpperRight.RightToLeft = RightToLeft.Yes;
            this.chkUpperRight.Size = new Size(88, 19);
            this.chkUpperRight.TabIndex = 2;
            this.chkUpperRight.CheckedChanged += new EventHandler(this.chkUpperRight_CheckedChanged);
            this.chkLowerRight.Location = new Point(132, 107);
            this.chkLowerRight.Name = "chkLowerRight";
            this.chkLowerRight.Properties.Caption = "右下";
            this.chkLowerRight.RightToLeft = RightToLeft.Yes;
            this.chkLowerRight.Size = new Size(88, 19);
            this.chkLowerRight.TabIndex = 4;
            this.chkLowerRight.CheckedChanged += new EventHandler(this.chkLowerRight_CheckedChanged);
            this.chkLowerLeft.Location = new Point(36, 107);
            this.chkLowerLeft.Name = "chkLowerLeft";
            this.chkLowerLeft.Properties.Caption = "左下";
            this.chkLowerLeft.RightToLeft = RightToLeft.Yes;
            this.chkLowerLeft.Size = new Size(88, 19);
            this.chkLowerLeft.TabIndex = 3;
            this.chkLowerLeft.CheckedChanged += new EventHandler(this.chkLowerLeft_CheckedChanged);
            base.Controls.Add(this.chkLowerRight);
            base.Controls.Add(this.chkLowerLeft);
            base.Controls.Add(this.chkUpperRight);
            base.Controls.Add(this.chkUpperLeft);
            base.Controls.Add(this.label1);
            base.Name = "CornerGridLabelPropertyPage";
            base.Size = new Size(256, 232);
            base.Load += new EventHandler(this.CornerGridLabelPropertyPage_Load);
            this.chkUpperLeft.Properties.EndInit();
            this.chkUpperRight.Properties.EndInit();
            this.chkLowerRight.Properties.EndInit();
            this.chkLowerLeft.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private CheckEdit chkLowerLeft;
        private CheckEdit chkLowerRight;
        private CheckEdit chkUpperLeft;
        private CheckEdit chkUpperRight;
        private Label label1;
    }
}