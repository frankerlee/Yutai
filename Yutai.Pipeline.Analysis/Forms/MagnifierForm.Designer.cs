
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Yutai.Pipeline.Analysis.Classes;
using Yutai.Pipeline.Analysis.Properties;

namespace Yutai.Pipeline.Analysis.Forms
{
	    partial class MagnifierForm
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
			base.SuspendLayout();
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(292, 269);
			base.Name = "MagnifierForm";
			base.StartPosition = FormStartPosition.CenterScreen;
			base.Text = "MagnifierUI";
			base.ResumeLayout(false);
		}

	
		private Timer timer_0;
		private Configuration configuration_0;
		private Image image_0;
		private Image image_1;
		private Image image_2;
		private Point point_0;
		private PointF pointF_0;
		private PointF pointF_1;
		private Point point_1;
    }
}