using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class frmScaleSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmScaleSet));
            this.label1 = new Label();
            this.txtScale = new TextEdit();
            this.groupBox1 = new GroupBox();
            this.txtY = new TextEdit();
            this.label3 = new Label();
            this.txtX = new TextEdit();
            this.label2 = new Label();
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.txtScale.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            this.txtY.Properties.BeginInit();
            this.txtX.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "比例尺:";
            this.txtScale.EditValue = "";
            this.txtScale.Location = new System.Drawing.Point(56, 8);
            this.txtScale.Name = "txtScale";
            this.txtScale.Size = new Size(128, 21);
            this.txtScale.TabIndex = 1;
            this.groupBox1.Controls.Add(this.txtY);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtX);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(16, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(176, 88);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "左下角坐标";
            this.txtY.EditValue = "";
            this.txtY.Location = new System.Drawing.Point(40, 56);
            this.txtY.Name = "txtY";
            this.txtY.Size = new Size(120, 21);
            this.txtY.TabIndex = 4;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 56);
            this.label3.Name = "label3";
            this.label3.Size = new Size(17, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "Y:";
            this.txtX.EditValue = "";
            this.txtX.Location = new System.Drawing.Point(40, 24);
            this.txtX.Name = "txtX";
            this.txtX.Size = new Size(120, 21);
            this.txtX.TabIndex = 2;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 24);
            this.label2.Name = "label2";
            this.label2.Size = new Size(17, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "X:";
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(56, 136);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(56, 24);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(128, 136);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(56, 24);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(200, 173);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.txtScale);
            base.Controls.Add(this.label1);
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmScaleSet";
            this.Text = "比例尺设置";
            base.Load += new EventHandler(this.frmScaleSet_Load);
            this.txtScale.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.txtY.Properties.EndInit();
            this.txtX.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextEdit txtScale;
        private TextEdit txtX;
        private TextEdit txtY;
    }
}