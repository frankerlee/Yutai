using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Helpers;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class MDomainControl
    {
        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_2);
        }

       
        private void InitializeComponent()
        {
            this.label5 = new Label();
            this.label3 = new Label();
            this.label1 = new Label();
            this.textBoxMinValue = new TextEdit();
            this.textBoxMaxValue = new TextEdit();
            this.textBoxPrecision = new TextEdit();
            this.textBoxMinValue.Properties.BeginInit();
            this.textBoxMaxValue.Properties.BeginInit();
            this.textBoxPrecision.Properties.BeginInit();
            base.SuspendLayout();
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 56);
            this.label5.Name = "label5";
            this.label5.Size = new Size(29, 17);
            this.label5.TabIndex = 17;
            this.label5.Text = "精度";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(152, 24);
            this.label3.Name = "label3";
            this.label3.Size = new Size(42, 17);
            this.label3.TabIndex = 16;
            this.label3.Text = "最大值";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 24);
            this.label1.Name = "label1";
            this.label1.Size = new Size(42, 17);
            this.label1.TabIndex = 15;
            this.label1.Text = "最小值";
            this.textBoxMinValue.EditValue = "";
            this.textBoxMinValue.Location = new System.Drawing.Point(56, 24);
            this.textBoxMinValue.Name = "textBoxMinValue";
            this.textBoxMinValue.Size = new Size(88, 23);
            this.textBoxMinValue.TabIndex = 21;
            this.textBoxMinValue.TextChanged += new EventHandler(this.textBoxMinValue_TextChanged);
            this.textBoxMaxValue.EditValue = "";
            this.textBoxMaxValue.Location = new System.Drawing.Point(200, 24);
            this.textBoxMaxValue.Name = "textBoxMaxValue";
            this.textBoxMaxValue.Size = new Size(88, 23);
            this.textBoxMaxValue.TabIndex = 22;
            this.textBoxMaxValue.TextChanged += new EventHandler(this.textBoxMaxValue_TextChanged);
            this.textBoxPrecision.EditValue = "";
            this.textBoxPrecision.Location = new System.Drawing.Point(56, 56);
            this.textBoxPrecision.Name = "textBoxPrecision";
            this.textBoxPrecision.Size = new Size(88, 23);
            this.textBoxPrecision.TabIndex = 23;
            this.textBoxPrecision.TextChanged += new EventHandler(this.textBoxPrecision_TextChanged);
            base.Controls.Add(this.textBoxPrecision);
            base.Controls.Add(this.textBoxMaxValue);
            base.Controls.Add(this.textBoxMinValue);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label1);
            base.Name = "MDomainControl";
            base.Size = new Size(304, 280);
            base.Load += new EventHandler(this.MDomainControl_Load);
            this.textBoxMinValue.Properties.EndInit();
            this.textBoxMaxValue.Properties.EndInit();
            this.textBoxPrecision.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private ISpatialReference ispatialReference_0;
        private Label label1;
        private Label label3;
        private Label label5;
        private TextEdit textBoxMaxValue;
        private TextEdit textBoxMinValue;
        private TextEdit textBoxPrecision;
    }
}