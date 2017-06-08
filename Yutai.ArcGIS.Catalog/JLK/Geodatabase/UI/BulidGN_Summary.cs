namespace JLK.Geodatabase.UI
{
    using JLK.Editors;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class BulidGN_Summary : UserControl
    {
        private Container container_0 = null;
        private MemoEdit memoEdit1;

        public BulidGN_Summary()
        {
            this.InitializeComponent();
        }

        private void BulidGN_Summary_Load(object sender, EventArgs e)
        {
            this.memoEdit1.Text = BulidGeometryNetworkHelper.BulidGNHelper.Summary();
        }

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
            this.memoEdit1 = new MemoEdit();
            this.memoEdit1.Properties.BeginInit();
            base.SuspendLayout();
            this.memoEdit1.EditValue = "memoEdit1";
            this.memoEdit1.Location = new Point(0x18, 0x20);
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Size = new Size(0x110, 200);
            this.memoEdit1.TabIndex = 0;
            base.Controls.Add(this.memoEdit1);
            base.Name = "BulidGN_Summary";
            base.Size = new Size(0x148, 0x100);
            base.Load += new EventHandler(this.BulidGN_Summary_Load);
            this.memoEdit1.Properties.EndInit();
            base.ResumeLayout(false);
        }
    }
}

