using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraBars;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Controls.SymbolUI;

namespace Yutai.ArcGIS.Controls.Controls.TOCDisplay
{
    //[ToolboxBitmap(typeof(TOCTreeView), "TocTreeView.bmp"), ToolboxItem(true)]
    partial class TOCTreeView
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
            this.barManager1 = new BarManager(this.components);
            this.barDockControlTop = new BarDockControl();
            this.barDockControlBottom = new BarDockControl();
            this.barDockControlLeft = new BarDockControl();
            this.barDockControlRight = new BarDockControl();
            this.popupMenu1 = new PopupMenu(this.components);
            base.SuspendLayout();
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.MaxItemId = 0;
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = SystemColors.Window;
            base.Controls.Add(this.barDockControlLeft);
            base.Controls.Add(this.barDockControlRight);
            base.Controls.Add(this.barDockControlBottom);
            base.Controls.Add(this.barDockControlTop);
            base.Name = "TOCTreeView";
            base.DoubleClick += new EventHandler(this.TOCTreeView_DoubleClick);
            base.Load += new EventHandler(this.TOCTreeView_Load);
            base.MouseDown += new MouseEventHandler(this.TOCTreeView_MouseDown);
            base.MouseMove += new MouseEventHandler(this.TOCTreeView_MouseMove);
            base.Paint += new PaintEventHandler(this.TOCTreeView_Paint);
            base.MouseUp += new MouseEventHandler(this.TOCTreeView_MouseUp);
            base.ResumeLayout(false);
        }

       
        private IContainer components = null;
        private BarDockControl barDockControlBottom;
        private BarDockControl barDockControlLeft;
        private BarDockControl barDockControlRight;
        private BarDockControl barDockControlTop;
        private BarManager barManager1;
        private PopupMenu popupMenu1;
    }
}