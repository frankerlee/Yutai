using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraBars;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Display;
using Yutai.ArcGIS.Common.Editor;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.ArcGIS.Common.Helpers;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    partial class ZDAttributeEditControl
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
            this.panel1 = new Panel();
            this.barManager1 = new BarManager(this.components);
            this.bar2 = new Bar();
            this.FlashObject = new BarButtonItem();
            this.ZoomTo = new BarButtonItem();
            this.barDockControlTop = new BarDockControl();
            this.barDockControlBottom = new BarDockControl();
            this.barDockControlLeft = new BarDockControl();
            this.barDockControlRight = new BarDockControl();
            this.treeView1 = new TreeView();
            this.panel2 = new Panel();
            this.barManager1.BeginInit();
            base.SuspendLayout();
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(101, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(229, 206);
            this.panel1.TabIndex = 1;
            this.barManager1.Bars.AddRange(new Bar[] { this.bar2 });
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new BarItem[] { this.FlashObject, this.ZoomTo });
            this.barManager1.MaxItemId = 2;
            this.bar2.BarName = "Custom 3";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(this.FlashObject), new LinkPersistInfo(this.ZoomTo) });
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Custom 3";
            this.FlashObject.Caption = "闪烁";
            this.FlashObject.Id = 0;
            this.FlashObject.Name = "FlashObject";
            this.FlashObject.ItemClick += new ItemClickEventHandler(this.FlashObject_ItemClick);
            this.ZoomTo.Caption = "缩放到";
            this.ZoomTo.Id = 1;
            this.ZoomTo.Name = "ZoomTo";
            this.ZoomTo.ItemClick += new ItemClickEventHandler(this.ZoomTo_ItemClick);
            this.treeView1.Dock = DockStyle.Left;
            this.treeView1.Location = new Point(0, 26);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new Size(101, 206);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new TreeViewEventHandler(this.treeView1_AfterSelect);
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new Point(101, 26);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(229, 206);
            this.panel2.TabIndex = 4;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.treeView1);
            base.Controls.Add(this.barDockControlLeft);
            base.Controls.Add(this.barDockControlRight);
            base.Controls.Add(this.barDockControlBottom);
            base.Controls.Add(this.barDockControlTop);
            base.Name = "ZDAttributeEditControl";
            base.Size = new Size(330, 232);
            base.Load += new EventHandler(this.AttributeEditControlExtend_Load);
            this.barManager1.EndInit();
            base.ResumeLayout(false);
        }

       
        private IContainer components = null;
        private Bar bar2;
        private BarDockControl barDockControlBottom;
        private BarDockControl barDockControlLeft;
        private BarDockControl barDockControlRight;
        private BarDockControl barDockControlTop;
        private BarManager barManager1;
        private BarButtonItem FlashObject;
        private IActiveViewEvents_Event m_pActiveViewEvents;
        private Panel panel1;
        private Panel panel2;
        private TreeView treeView1;
        private BarButtonItem ZoomTo;
    }
}