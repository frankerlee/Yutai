using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraBars;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Display;
using Yutai.ArcGIS.Common.Editor;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.ArcGIS.Common.Helpers;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    partial class AttributeEditControlExtend
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
            this.tabControl1 = new TabControl();
            this.tabPage1 = new TabPage();
            this.tabPage2 = new TabPage();
            this.panel1 = new Panel();
            this.panel2 = new Panel();
            this.barManager1 = new BarManager(this.components);
            this.bar2 = new Bar();
            this.FlashObject = new BarButtonItem();
            this.ZoomTo = new BarButtonItem();
            this.barDockControlTop = new BarDockControl();
            this.barDockControlBottom = new BarDockControl();
            this.barDockControlLeft = new BarDockControl();
            this.barDockControlRight = new BarDockControl();
            this.tabPage3 = new TabPage();
            this.tabPage4 = new TabPage();
            this.tabControl2 = new TabControl();
            this.tabPage5 = new TabPage();
            this.tabPage6 = new TabPage();
            this.tabControl1.SuspendLayout();
            this.barManager1.BeginInit();
            this.tabControl2.SuspendLayout();
            base.SuspendLayout();
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = DockStyle.Fill;
            this.tabControl1.Location = new Point(0, 26);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(330, 206);
            this.tabControl1.TabIndex = 0;
            this.tabPage1.Location = new Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new Padding(3);
            this.tabPage1.Size = new Size(322, 180);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "注记";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage2.Location = new Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new Padding(3);
            this.tabPage2.Size = new Size(322, 180);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "属性";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(0, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(330, 206);
            this.panel1.TabIndex = 1;
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new Point(0, 26);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(330, 206);
            this.panel2.TabIndex = 2;
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
            this.tabPage3.Location = new Point(4, 21);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new Padding(3);
            this.tabPage3.Size = new Size(322, 183);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "注记";
            this.tabPage3.UseVisualStyleBackColor = true;
            this.tabPage4.Location = new Point(4, 21);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new Padding(3);
            this.tabPage4.Size = new Size(322, 183);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "属性";
            this.tabPage4.UseVisualStyleBackColor = true;
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Controls.Add(this.tabPage6);
            this.tabControl2.Dock = DockStyle.Fill;
            this.tabControl2.Location = new Point(0, 26);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new Size(330, 206);
            this.tabControl2.TabIndex = 4;
            this.tabControl2.Visible = false;
            this.tabPage5.Location = new Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new Padding(3);
            this.tabPage5.Size = new Size(322, 180);
            this.tabPage5.TabIndex = 0;
            this.tabPage5.Text = "属性";
            this.tabPage5.UseVisualStyleBackColor = true;
            this.tabPage6.Location = new Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new Padding(3);
            this.tabPage6.Size = new Size(322, 180);
            this.tabPage6.TabIndex = 1;
            this.tabPage6.Text = "规则";
            this.tabPage6.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.tabControl2);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.tabControl1);
            base.Controls.Add(this.barDockControlLeft);
            base.Controls.Add(this.barDockControlRight);
            base.Controls.Add(this.barDockControlBottom);
            base.Controls.Add(this.barDockControlTop);
            base.Name = "AttributeEditControlExtend";
            base.Size = new Size(330, 232);
            base.Load += new EventHandler(this.AttributeEditControlExtend_Load);
            this.tabControl1.ResumeLayout(false);
            this.barManager1.EndInit();
            this.tabControl2.ResumeLayout(false);
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
        private TabControl tabControl1;
        private TabControl tabControl2;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private TabPage tabPage5;
        private TabPage tabPage6;
        private BarButtonItem ZoomTo;
    }
}