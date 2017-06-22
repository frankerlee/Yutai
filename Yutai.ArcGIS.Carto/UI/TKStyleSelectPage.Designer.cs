using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class TKStyleSelectPage
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
            this.rdoStandard = new RadioButton();
            this.rdoOtherType = new RadioButton();
            base.SuspendLayout();
            this.rdoStandard.AutoSize = true;
            this.rdoStandard.Checked = true;
            this.rdoStandard.Location = new Point(27, 31);
            this.rdoStandard.Name = "rdoStandard";
            this.rdoStandard.Size = new Size(71, 16);
            this.rdoStandard.TabIndex = 0;
            this.rdoStandard.TabStop = true;
            this.rdoStandard.Text = "标准分幅";
            this.rdoStandard.UseVisualStyleBackColor = true;
            this.rdoOtherType.AutoSize = true;
            this.rdoOtherType.Location = new Point(27, 94);
            this.rdoOtherType.Name = "rdoOtherType";
            this.rdoOtherType.Size = new Size(71, 16);
            this.rdoOtherType.TabIndex = 1;
            this.rdoOtherType.Text = "任意分幅";
            this.rdoOtherType.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.rdoOtherType);
            base.Controls.Add(this.rdoStandard);
            base.Name = "TKStyleSelectPage";
            base.Size = new Size(335, 258);
            base.Load += new EventHandler(this.TKStyleSelectPage_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private RadioButton rdoOtherType;
        private RadioButton rdoStandard;
    }
}