using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class PageSetupControl
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
            this.groupBox1 = new GroupBox();
            this.txtPageWidth = new TextEdit();
            this.label1 = new Label();
            this.txtPageHeight = new TextEdit();
            this.label2 = new Label();
            this.groupBox2 = new GroupBox();
            this.label3 = new Label();
            this.txtX = new TextEdit();
            this.txtY = new TextEdit();
            this.label4 = new Label();
            this.txtHeight = new TextEdit();
            this.label5 = new Label();
            this.txtWidth = new TextEdit();
            this.label6 = new Label();
            this.groupBox1.SuspendLayout();
            this.txtPageWidth.Properties.BeginInit();
            this.txtPageHeight.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.txtX.Properties.BeginInit();
            this.txtY.Properties.BeginInit();
            this.txtHeight.Properties.BeginInit();
            this.txtWidth.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.txtPageHeight);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtPageWidth);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(184, 80);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "页面大小";
            this.txtPageWidth.EditValue = "";
            this.txtPageWidth.Location = new System.Drawing.Point(64, 19);
            this.txtPageWidth.Name = "txtPageWidth";
            this.txtPageWidth.Size = new Size(104, 21);
            this.txtPageWidth.TabIndex = 3;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 22);
            this.label1.Name = "label1";
            this.label1.Size = new Size(48, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "页面宽:";
            this.txtPageHeight.EditValue = "";
            this.txtPageHeight.Location = new System.Drawing.Point(64, 51);
            this.txtPageHeight.Name = "txtPageHeight";
            this.txtPageHeight.Size = new Size(104, 21);
            this.txtPageHeight.TabIndex = 5;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 53);
            this.label2.Name = "label2";
            this.label2.Size = new Size(48, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "页面高:";
            this.groupBox2.Controls.Add(this.txtHeight);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtWidth);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtY);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtX);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(16, 104);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(176, 160);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "输出地图的位置和大小";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 24);
            this.label3.Name = "label3";
            this.label3.Size = new Size(17, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "X:";
            this.txtX.EditValue = "";
            this.txtX.Location = new System.Drawing.Point(48, 24);
            this.txtX.Name = "txtX";
            this.txtX.Size = new Size(104, 21);
            this.txtX.TabIndex = 1;
            this.txtY.EditValue = "";
            this.txtY.Location = new System.Drawing.Point(48, 56);
            this.txtY.Name = "txtY";
            this.txtY.Size = new Size(104, 21);
            this.txtY.TabIndex = 3;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 56);
            this.label4.Name = "label4";
            this.label4.Size = new Size(17, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "Y:";
            this.txtHeight.EditValue = "";
            this.txtHeight.Location = new System.Drawing.Point(48, 128);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new Size(104, 21);
            this.txtHeight.TabIndex = 7;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 128);
            this.label5.Name = "label5";
            this.label5.Size = new Size(23, 17);
            this.label5.TabIndex = 6;
            this.label5.Text = "高:";
            this.txtWidth.EditValue = "";
            this.txtWidth.Location = new System.Drawing.Point(48, 96);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new Size(104, 21);
            this.txtWidth.TabIndex = 5;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 96);
            this.label6.Name = "label6";
            this.label6.Size = new Size(23, 17);
            this.label6.TabIndex = 4;
            this.label6.Text = "宽:";
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "PageSetupControl";
            base.Size = new Size(224, 280);
            base.Load += new EventHandler(this.PageSetupControl_Load);
            this.groupBox1.ResumeLayout(false);
            this.txtPageWidth.Properties.EndInit();
            this.txtPageHeight.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.txtX.Properties.EndInit();
            this.txtY.Properties.EndInit();
            this.txtHeight.Properties.EndInit();
            this.txtWidth.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextEdit txtHeight;
        private TextEdit txtPageHeight;
        private TextEdit txtPageWidth;
        private TextEdit txtWidth;
        private TextEdit txtX;
        private TextEdit txtY;
    }
}