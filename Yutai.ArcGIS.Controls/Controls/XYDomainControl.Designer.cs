using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Helpers;

namespace Yutai.ArcGIS.Controls.Controls
{
    partial class XYDomainControl
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
            this.label2 = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.label5 = new Label();
            this.textBoxPrecision = new TextEdit();
            this.textBoxMaxX = new TextEdit();
            this.textBoxMinX = new TextEdit();
            this.textBoxMaxY = new TextEdit();
            this.textBoxMinY = new TextEdit();
            this.textBoxPrecision.Properties.BeginInit();
            this.textBoxMaxX.Properties.BeginInit();
            this.textBoxMinX.Properties.BeginInit();
            this.textBoxMaxY.Properties.BeginInit();
            this.textBoxMinY.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 32);
            this.label1.Name = "label1";
            this.label1.Size = new Size(35, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "最小X";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 80);
            this.label2.Name = "label2";
            this.label2.Size = new Size(35, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "最小Y";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(144, 32);
            this.label3.Name = "label3";
            this.label3.Size = new Size(35, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "最大X";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(144, 80);
            this.label4.Name = "label4";
            this.label4.Size = new Size(35, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "最大Y";
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 120);
            this.label5.Name = "label5";
            this.label5.Size = new Size(29, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "精度";
            this.textBoxPrecision.EditValue = "";
            this.textBoxPrecision.Location = new System.Drawing.Point(56, 120);
            this.textBoxPrecision.Name = "textBoxPrecision";
            this.textBoxPrecision.Size = new Size(80, 23);
            this.textBoxPrecision.TabIndex = 26;
            this.textBoxPrecision.TextChanged += new EventHandler(this.textBoxMaxY_TextChanged);
            this.textBoxMaxX.EditValue = "";
            this.textBoxMaxX.Location = new System.Drawing.Point(192, 32);
            this.textBoxMaxX.Name = "textBoxMaxX";
            this.textBoxMaxX.Size = new Size(80, 23);
            this.textBoxMaxX.TabIndex = 25;
            this.textBoxMaxX.TextChanged += new EventHandler(this.textBoxMaxX_TextChanged);
            this.textBoxMinX.EditValue = "";
            this.textBoxMinX.Location = new System.Drawing.Point(56, 32);
            this.textBoxMinX.Name = "textBoxMinX";
            this.textBoxMinX.Size = new Size(80, 23);
            this.textBoxMinX.TabIndex = 24;
            this.textBoxMinX.TextChanged += new EventHandler(this.textBoxMinX_TextChanged);
            this.textBoxMaxY.EditValue = "";
            this.textBoxMaxY.Location = new System.Drawing.Point(192, 72);
            this.textBoxMaxY.Name = "textBoxMaxY";
            this.textBoxMaxY.Size = new Size(80, 23);
            this.textBoxMaxY.TabIndex = 28;
            this.textBoxMaxY.TextChanged += new EventHandler(this.textBoxMaxY_TextChanged);
            this.textBoxMinY.EditValue = "";
            this.textBoxMinY.Location = new System.Drawing.Point(56, 72);
            this.textBoxMinY.Name = "textBoxMinY";
            this.textBoxMinY.Size = new Size(80, 23);
            this.textBoxMinY.TabIndex = 27;
            this.textBoxMinY.TextChanged += new EventHandler(this.textBoxMinY_TextChanged);
            base.Controls.Add(this.textBoxMaxY);
            base.Controls.Add(this.textBoxMinY);
            base.Controls.Add(this.textBoxPrecision);
            base.Controls.Add(this.textBoxMaxX);
            base.Controls.Add(this.textBoxMinX);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "XYDomainControl";
            base.Size = new Size(288, 280);
            base.Load += new EventHandler(this.XYDomainControl_Load);
            this.textBoxPrecision.Properties.EndInit();
            this.textBoxMaxX.Properties.EndInit();
            this.textBoxMinX.Properties.EndInit();
            this.textBoxMaxY.Properties.EndInit();
            this.textBoxMinY.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private ISpatialReference m_pSpatialRefrence;
        private TextEdit textBoxMaxX;
        private TextEdit textBoxMaxY;
        private TextEdit textBoxMinX;
        private TextEdit textBoxMinY;
        private TextEdit textBoxPrecision;
    }
}