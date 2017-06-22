using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.Library
{
    partial class frmCircleInput
    {
        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

       
        private void InitializeComponent()
        {
            this.btnCancel = new Button();
            this.btnOK = new Button();
            this.label4 = new Label();
            this.label3 = new Label();
            this.txtYCoor = new TextBox();
            this.txtXCoor = new TextBox();
            this.label5 = new Label();
            this.label7 = new Label();
            this.label8 = new Label();
            this.txtRadio = new TextBox();
            this.label9 = new Label();
            base.SuspendLayout();
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(175, 129);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(75, 23);
            this.btnCancel.TabIndex = 24;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnOK.Location = new System.Drawing.Point(80, 129);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(75, 23);
            this.btnOK.TabIndex = 23;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(215, 57);
            this.label4.Name = "label4";
            this.label4.Size = new Size(29, 12);
            this.label4.TabIndex = 22;
            this.label4.Text = "公里";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(212, 20);
            this.label3.Name = "label3";
            this.label3.Size = new Size(29, 12);
            this.label3.TabIndex = 21;
            this.label3.Text = "公里";
            this.txtYCoor.Location = new System.Drawing.Point(106, 54);
            this.txtYCoor.Name = "txtYCoor";
            this.txtYCoor.Size = new Size(100, 21);
            this.txtYCoor.TabIndex = 20;
            this.txtXCoor.Location = new System.Drawing.Point(106, 17);
            this.txtXCoor.Name = "txtXCoor";
            this.txtXCoor.Size = new Size(100, 21);
            this.txtXCoor.TabIndex = 19;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(215, 91);
            this.label5.Name = "label5";
            this.label5.Size = new Size(29, 12);
            this.label5.TabIndex = 27;
            this.label5.Text = "公里";
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 56);
            this.label7.Name = "label7";
            this.label7.Size = new Size(65, 12);
            this.label7.TabIndex = 22;
            this.label7.Text = "圆心纵坐标";
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 20);
            this.label8.Name = "label8";
            this.label8.Size = new Size(65, 12);
            this.label8.TabIndex = 21;
            this.label8.Text = "圆心横坐标";
            this.txtRadio.Location = new System.Drawing.Point(106, 89);
            this.txtRadio.Name = "txtRadio";
            this.txtRadio.Size = new Size(100, 21);
            this.txtRadio.TabIndex = 28;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 92);
            this.label9.Name = "label9";
            this.label9.Size = new Size(29, 12);
            this.label9.TabIndex = 27;
            this.label9.Text = "半径";
            base.ClientSize = new Size(256, 183);
            base.Controls.Add(this.txtRadio);
            base.Controls.Add(this.label9);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.txtYCoor);
            base.Controls.Add(this.txtXCoor);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.label8);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label5);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmCircleInput";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "输入圆心,半径";
            base.Load += new EventHandler(this.frmCircleInput_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private Button btnCancel;
        private Button btnOK;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label7;
        private Label label8;
        private Label label9;
        private TextBox txtRadio;
        private TextBox txtXCoor;
        private TextBox txtYCoor;
    }
}