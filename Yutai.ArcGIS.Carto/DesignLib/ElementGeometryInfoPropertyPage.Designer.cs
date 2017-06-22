using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    partial class ElementGeometryInfoPropertyPage
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
            this.label1 = new Label();
            this.label2 = new Label();
            this.groupBox1 = new GroupBox();
            this.txtArea = new TextEdit();
            this.txtPerimeter = new TextEdit();
            this.txtY = new TextEdit();
            this.txtX = new TextEdit();
            this.label3 = new Label();
            this.label4 = new Label();
            this.groupBox1.SuspendLayout();
            this.txtArea.Properties.BeginInit();
            this.txtPerimeter.Properties.BeginInit();
            this.txtY.Properties.BeginInit();
            this.txtX.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "面积";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 40);
            this.label2.Name = "label2";
            this.label2.Size = new Size(29, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "周长";
            this.groupBox1.Controls.Add(this.txtY);
            this.groupBox1.Controls.Add(this.txtX);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(16, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(160, 80);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "中心";
            this.txtArea.EditValue = "";
            this.txtArea.Location = new System.Drawing.Point(56, 16);
            this.txtArea.Name = "txtArea";
            this.txtArea.Size = new Size(80, 21);
            this.txtArea.TabIndex = 3;
            this.txtPerimeter.EditValue = "";
            this.txtPerimeter.Location = new System.Drawing.Point(56, 40);
            this.txtPerimeter.Name = "txtPerimeter";
            this.txtPerimeter.Size = new Size(80, 21);
            this.txtPerimeter.TabIndex = 4;
            this.txtY.EditValue = "";
            this.txtY.Location = new System.Drawing.Point(32, 40);
            this.txtY.Name = "txtY";
            this.txtY.Size = new Size(80, 21);
            this.txtY.TabIndex = 8;
            this.txtX.EditValue = "";
            this.txtX.Location = new System.Drawing.Point(32, 16);
            this.txtX.Name = "txtX";
            this.txtX.Size = new Size(80, 21);
            this.txtX.TabIndex = 7;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 42);
            this.label3.Name = "label3";
            this.label3.Size = new Size(11, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Y";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 18);
            this.label4.Name = "label4";
            this.label4.Size = new Size(11, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "X";
            base.Controls.Add(this.txtPerimeter);
            base.Controls.Add(this.txtArea);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "ElementGeometryInfoPropertyPage";
            base.Size = new Size(264, 184);
            base.Load += new EventHandler(this.ElementGeometryInfoPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.txtArea.Properties.EndInit();
            this.txtPerimeter.Properties.EndInit();
            this.txtY.Properties.EndInit();
            this.txtX.Properties.EndInit();
            base.ResumeLayout(false);
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

       
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextEdit txtArea;
        private TextEdit txtPerimeter;
        private TextEdit txtX;
        private TextEdit txtY;
    }
}