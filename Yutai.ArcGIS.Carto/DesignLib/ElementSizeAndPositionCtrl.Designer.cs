using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    partial class ElementSizeAndPositionCtrl
    {
        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_1);
        }

       
        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.txtY = new TextEdit();
            this.txtX = new TextEdit();
            this.label2 = new Label();
            this.label1 = new Label();
            this.groupBox2 = new GroupBox();
            this.txtHeight = new TextEdit();
            this.txtWidth = new TextEdit();
            this.label3 = new Label();
            this.label4 = new Label();
            this.groupBox1.SuspendLayout();
            this.txtY.Properties.BeginInit();
            this.txtX.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.txtHeight.Properties.BeginInit();
            this.txtWidth.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.txtY);
            this.groupBox1.Controls.Add(this.txtX);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(160, 104);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "位置";
            this.txtY.EditValue = "";
            this.txtY.Location = new System.Drawing.Point(48, 64);
            this.txtY.Name = "txtY";
            this.txtY.Size = new Size(96, 21);
            this.txtY.TabIndex = 3;
            this.txtX.EditValue = "";
            this.txtX.Location = new System.Drawing.Point(48, 32);
            this.txtX.Name = "txtX";
            this.txtX.Size = new Size(96, 21);
            this.txtX.TabIndex = 2;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 64);
            this.label2.Name = "label2";
            this.label2.Size = new Size(17, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Y:";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 32);
            this.label1.Name = "label1";
            this.label1.Size = new Size(17, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "X:";
            this.groupBox2.Controls.Add(this.txtHeight);
            this.groupBox2.Controls.Add(this.txtWidth);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(184, 16);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(160, 104);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "大小";
            this.txtHeight.EditValue = "";
            this.txtHeight.Location = new System.Drawing.Point(48, 64);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new Size(96, 21);
            this.txtHeight.TabIndex = 3;
            this.txtWidth.EditValue = "";
            this.txtWidth.Location = new System.Drawing.Point(48, 32);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new Size(96, 21);
            this.txtWidth.TabIndex = 2;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 64);
            this.label3.Name = "label3";
            this.label3.Size = new Size(29, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "高度";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 32);
            this.label4.Name = "label4";
            this.label4.Size = new Size(29, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "宽度";
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "ElementSizeAndPositionCtrl";
            base.Size = new Size(360, 216);
            base.Load += new EventHandler(this.ElementSizeAndPositionCtrl_Load);
            this.groupBox1.ResumeLayout(false);
            this.txtY.Properties.EndInit();
            this.txtX.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.txtHeight.Properties.EndInit();
            this.txtWidth.Properties.EndInit();
            base.ResumeLayout(false);
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

       
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextEdit txtHeight;
        private TextEdit txtWidth;
        private TextEdit txtX;
        private TextEdit txtY;
    }
}