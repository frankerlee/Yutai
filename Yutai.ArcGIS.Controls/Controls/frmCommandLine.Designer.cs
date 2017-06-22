using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Framework.Docking;

namespace Yutai.ArcGIS.Controls.Controls
{
    partial class frmCommandLine
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCommandLine));
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(292, 273);
            base.DockAreas = DockAreas.DockBottom | DockAreas.DockTop | DockAreas.DockRight | DockAreas.DockLeft | DockAreas.Float;
            base.HideOnClose = true;
            
            base.Name = "frmCommandLine";
            base.ShowHint = DockState.DockBottom;
            base.TabText = "命令行";
            this.Text = "命令行";
            base.ResumeLayout(false);
        }

       
        private IContainer components = null;
    }
}