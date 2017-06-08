using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class TKStyleSelectPage : UserControl
    {
        private IContainer icontainer_0 = null;
        private YTTKAssiatant jlktkassiatant_0 = null;
        private RadioButton rdoOtherType;
        private RadioButton rdoStandard;

        public TKStyleSelectPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            this.jlktkassiatant_0.TKType = this.rdoStandard.Checked ? TKType.TKStandard : TKType.TKRand;
            return true;
        }

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
            this.rdoStandard.Location = new Point(0x1b, 0x1f);
            this.rdoStandard.Name = "rdoStandard";
            this.rdoStandard.Size = new Size(0x47, 0x10);
            this.rdoStandard.TabIndex = 0;
            this.rdoStandard.TabStop = true;
            this.rdoStandard.Text = "标准分幅";
            this.rdoStandard.UseVisualStyleBackColor = true;
            this.rdoOtherType.AutoSize = true;
            this.rdoOtherType.Location = new Point(0x1b, 0x5e);
            this.rdoOtherType.Name = "rdoOtherType";
            this.rdoOtherType.Size = new Size(0x47, 0x10);
            this.rdoOtherType.TabIndex = 1;
            this.rdoOtherType.Text = "任意分幅";
            this.rdoOtherType.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.rdoOtherType);
            base.Controls.Add(this.rdoStandard);
            base.Name = "TKStyleSelectPage";
            base.Size = new Size(0x14f, 0x102);
            base.Load += new EventHandler(this.TKStyleSelectPage_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void TKStyleSelectPage_Load(object sender, EventArgs e)
        {
            this.rdoStandard.Checked = this.jlktkassiatant_0.TKType == TKType.TKStandard;
        }

        internal YTTKAssiatant YTTKAssiatant
        {
            set
            {
                this.jlktkassiatant_0 = value;
            }
        }
    }
}

