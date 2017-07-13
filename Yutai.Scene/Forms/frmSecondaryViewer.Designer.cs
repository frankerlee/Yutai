using System;
using System.ComponentModel;
using ESRI.ArcGIS.Controls;

namespace Yutai.Plugins.Scene.Forms
{
	    partial class frmSecondaryViewer
    {
		protected override void Dispose(bool bool_0)
		{
			if (bool_0 && this.icontainer_0 != null)
			{
				this.icontainer_0.Dispose();
			}
			base.Dispose(bool_0);
		}

	
	private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSecondaryViewer));
			this.axSceneControl1 = new AxSceneControl();
			((ISupportInitialize)this.axSceneControl1).BeginInit();
			base.SuspendLayout();
			this.axSceneControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.axSceneControl1.Location = new System.Drawing.Point(0, 0);
			this.axSceneControl1.Name = "axSceneControl1";
			this.axSceneControl1.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("axSceneControl1.OcxState");
			this.axSceneControl1.Size = new System.Drawing.Size(292, 273);
			this.axSceneControl1.TabIndex = 0;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(292, 273);
			base.Controls.Add(this.axSceneControl1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			base.Name = "frmSecondaryViewer";
			base.TopMost = true;
			base.Load += new EventHandler(this.frmSecondaryViewer_Load);
			((ISupportInitialize)this.axSceneControl1).EndInit();
			base.ResumeLayout(false);
		}

	
		private AxSceneControl axSceneControl1;
    }
}