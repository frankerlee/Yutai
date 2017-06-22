using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.Editor;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    partial class EditTemplateListView
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
            this.components = new Container();
            this.imageList1 = new ImageList(this.components);
            base.SuspendLayout();
            this.imageList1.ColorDepth = ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new Size(16, 16);
            this.imageList1.TransparentColor = Color.Transparent;
            base.SmallImageList = this.imageList1;
            base.ResumeLayout(false);
        }

       
        private IContainer components = null;
        private ImageList imageList1;
        private LVS_EX styles;
    }
}