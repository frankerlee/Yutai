using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class BulidGN_Summary
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
            this.memoEdit1 = new MemoEdit();
            this.memoEdit1.Properties.BeginInit();
            base.SuspendLayout();
            this.memoEdit1.EditValue = "memoEdit1";
            this.memoEdit1.Location = new Point(24, 32);
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Size = new Size(272, 200);
            this.memoEdit1.TabIndex = 0;
            base.Controls.Add(this.memoEdit1);
            base.Name = "BulidGN_Summary";
            base.Size = new Size(328, 256);
            base.Load += new EventHandler(this.BulidGN_Summary_Load);
            this.memoEdit1.Properties.EndInit();
            base.ResumeLayout(false);
        }
    
        private MemoEdit memoEdit1;
    }
}