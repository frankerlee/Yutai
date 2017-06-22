using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Carto.Library
{
    partial class frmInputXY
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
            this.label1 = new Label();
            this.label2 = new Label();
            this.txtX = new TextBox();
            this.txtY = new TextBox();
            this.btnCancel = new Button();
            this.btnOK = new Button();
            this.label3 = new Label();
            this.label4 = new Label();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "横坐标";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(15, 49);
            this.label2.Name = "label2";
            this.label2.Size = new Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "纵坐标";
            this.txtX.Location = new Point(60, 10);
            this.txtX.Name = "txtX";
            this.txtX.Size = new Size(131, 21);
            this.txtX.TabIndex = 2;
            this.txtY.Location = new Point(60, 46);
            this.txtY.Name = "txtY";
            this.txtY.Size = new Size(131, 21);
            this.txtY.TabIndex = 3;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new Point(120, 88);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(75, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnOK.Location = new Point(39, 88);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(75, 23);
            this.btnOK.TabIndex = 19;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(197, 13);
            this.label3.Name = "label3";
            this.label3.Size = new Size(29, 12);
            this.label3.TabIndex = 23;
            this.label3.Text = "公里";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(200, 50);
            this.label4.Name = "label4";
            this.label4.Size = new Size(29, 12);
            this.label4.TabIndex = 24;
            this.label4.Text = "公里";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(252, 121);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.txtY);
            base.Controls.Add(this.txtX);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmInputXY";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "输入坐标";
            base.Load += new EventHandler(this.frmInputXY_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private Button btnCancel;
        private Button btnOK;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox txtX;
        private TextBox txtY;
    }
}