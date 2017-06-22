using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class frmBookMark
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBookMark));
            this.label1 = new Label();
            this.txtBookMarker = new TextEdit();
            this.btnOK = new SimpleButton();
            this.simpleButton2 = new SimpleButton();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "书签名称:";
            this.txtBookMarker.EditValue = "";
            this.txtBookMarker.Location = new Point(104, 8);
            this.txtBookMarker.Name = "txtBookMarker";
            this.txtBookMarker.Size = new Size(160, 21);
            this.txtBookMarker.TabIndex = 1;
            this.btnOK.Location = new Point(136, 40);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(64, 24);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton2.Location = new Point(208, 40);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new Size(64, 24);
            this.simpleButton2.TabIndex = 3;
            this.simpleButton2.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(292, 76);
            base.Controls.Add(this.simpleButton2);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.txtBookMarker);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmBookMark";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "创建书签";
            base.Load += new EventHandler(this.frmBookMark_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private SimpleButton btnOK;
        private Label label1;
        private SimpleButton simpleButton2;
        private TextEdit txtBookMarker;
    }
}