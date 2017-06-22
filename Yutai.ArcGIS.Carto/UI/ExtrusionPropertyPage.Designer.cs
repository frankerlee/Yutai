using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class ExtrusionPropertyPage
    {
        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_2);
        }

       
        private void InitializeComponent()
        {
            this.chkExtrusion = new CheckBox();
            this.cboExtrusionType = new ComboBox();
            this.label1 = new Label();
            this.label2 = new Label();
            this.txtExtrusionValue = new TextBox();
            base.SuspendLayout();
            this.chkExtrusion.AutoSize = true;
            this.chkExtrusion.Location = new Point(14, 13);
            this.chkExtrusion.Name = "chkExtrusion";
            this.chkExtrusion.Size = new Size(72, 16);
            this.chkExtrusion.TabIndex = 0;
            this.chkExtrusion.Text = "拉伸要素";
            this.chkExtrusion.UseVisualStyleBackColor = true;
            this.chkExtrusion.CheckedChanged += new EventHandler(this.chkExtrusion_CheckedChanged);
            this.cboExtrusionType.FormattingEnabled = true;
            this.cboExtrusionType.Items.AddRange(new object[] { "使用最小Z值", "使用最大Z值", "使用基准高程", "使用绝对值" });
            this.cboExtrusionType.Location = new Point(71, 135);
            this.cboExtrusionType.Name = "cboExtrusionType";
            this.cboExtrusionType.Size = new Size(121, 20);
            this.cboExtrusionType.TabIndex = 1;
            this.cboExtrusionType.SelectedIndexChanged += new EventHandler(this.cboExtrusionType_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 32);
            this.label1.Name = "label1";
            this.label1.Size = new Size(89, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "拉伸值或表达式";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 138);
            this.label2.Name = "label2";
            this.label2.Size = new Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "拉伸方式";
            this.txtExtrusionValue.Location = new Point(14, 56);
            this.txtExtrusionValue.Multiline = true;
            this.txtExtrusionValue.Name = "txtExtrusionValue";
            this.txtExtrusionValue.Size = new Size(217, 63);
            this.txtExtrusionValue.TabIndex = 4;
            this.txtExtrusionValue.TextChanged += new EventHandler(this.txtExtrusionValue_TextChanged);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.txtExtrusionValue);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.cboExtrusionType);
            base.Controls.Add(this.chkExtrusion);
            base.Name = "ExtrusionPropertyPage";
            base.Size = new Size(424, 237);
            base.Load += new EventHandler(this.ExtrusionPropertyPage_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private ComboBox cboExtrusionType;
        private CheckBox chkExtrusion;
        private Label label1;
        private Label label2;
        private TextBox txtExtrusionValue;
    }
}