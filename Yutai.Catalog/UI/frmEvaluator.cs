namespace Yutai.Catalog.UI
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class frmEvaluator : Form
    {
        private IContainer icontainer_0 = null;

        public frmEvaluator()
        {
            this.InitializeComponent();
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
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x129, 0x111);
            base.Name = "frmEvaluator";
            this.Text = "frmEvaluator";
            base.ResumeLayout(false);
        }
    }
}

