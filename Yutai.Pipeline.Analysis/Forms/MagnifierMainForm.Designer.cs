using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Yutai.Pipeline.Analysis.Classes;
using Yutai.Pipeline.Analysis.Forms;
using Yutai.Pipeline.Analysis.Properties;

namespace Yutai.Pipeline.Analysis.Forms
{
	    partial class MagnifierMainForm
    {
		protected override void Dispose(bool disposing)
		{
			if ((!disposing ? false : this.icontainer_0 != null))
			{
				this.icontainer_0.Dispose();
			}
			base.Dispose(disposing);
		}

	
	private void InitializeComponent()
		{
            this.SuspendLayout();
            // 
            // MagnifierMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 374);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Name = "MagnifierMainForm";
            this.Text = "SimpleMagnifier";
            this.ResumeLayout(false);

		}

	
		private Image image_0;
		private Point point_0;
		private Point point_1;
    }
}