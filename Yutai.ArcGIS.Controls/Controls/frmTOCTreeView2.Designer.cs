using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Controls.Controls.TOCTreeview;
using Yutai.ArcGIS.Framework.Docking;

namespace Yutai.ArcGIS.Controls.Controls
{
    partial class frmTOCTreeView2
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

       
 private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTOCTreeView));
            this.tocTreeView1 = new TOCControl();
            base.SuspendLayout();
            this.tocTreeView1.AutoScroll = true;
            this.tocTreeView1.BackColor = SystemColors.ControlLightLight;
            this.tocTreeView1.Dock = DockStyle.Fill;
            this.tocTreeView1.Location = new Point(0, 0);
            this.tocTreeView1.Name = "tocTreeView1";
            this.tocTreeView1.Size = new Size(292, 273);
            this.tocTreeView1.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(292, 273);
            base.Controls.Add(this.tocTreeView1);
            base.DockAreas = DockAreas.DockBottom | DockAreas.DockTop | DockAreas.DockRight | DockAreas.DockLeft | DockAreas.Float;
            base.HideOnClose = true;
            
            base.Name = "frmTOCTreeView";
            base.ShowHint = DockState.DockLeft;
            base.TabText = "图层树";
            this.Text = "图层树";
            base.ResumeLayout(false);
        }

       
        private IContainer components = null;
    }
}