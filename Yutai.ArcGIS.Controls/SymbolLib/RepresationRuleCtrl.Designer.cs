using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Controls.SymbolLib
{
    partial class RepresationRuleCtrl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RepresationRuleCtrl));
            this.tabControl1 = new TabControl();
            this.tabPage1 = new TabPage();
            this.symbolItem1 = new SymbolItem();
            this.contextMenuStrip1 = new ContextMenuStrip(this.components);
            this.basicMarkerLayerToolStripMenuItem = new ToolStripMenuItem();
            this.basicLineLayerToolStripMenuItem = new ToolStripMenuItem();
            this.basicFillLayerToolStripMenuItem = new ToolStripMenuItem();
            this.tnMoveDown = new SimpleButton();
            this.btnMoveUp = new SimpleButton();
            this.btnDeleteLayer = new SimpleButton();
            this.btnAddLayer = new SimpleButton();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            base.SuspendLayout();
            this.tabControl1.Alignment = TabAlignment.Left;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(0, 3);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(248, 222);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new EventHandler(this.tabControl1_SelectedIndexChanged);
            this.tabPage1.Controls.Add(this.symbolItem1);
            this.tabPage1.Location = new System.Drawing.Point(22, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new Size(222, 214);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "预览";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.symbolItem1.BackColor = SystemColors.ControlLight;
            this.symbolItem1.Location = new System.Drawing.Point(13, 76);
            this.symbolItem1.Name = "symbolItem1";
            this.symbolItem1.Size = new Size(196, 92);
            this.symbolItem1.Symbol = null;
            this.symbolItem1.TabIndex = 2;
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.basicMarkerLayerToolStripMenuItem, this.basicLineLayerToolStripMenuItem, this.basicFillLayerToolStripMenuItem });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new Size(87, 70);
            this.basicMarkerLayerToolStripMenuItem.Name = "basicMarkerLayerToolStripMenuItem";
            this.basicMarkerLayerToolStripMenuItem.Size = new Size(86, 22);
            this.basicMarkerLayerToolStripMenuItem.Text = "点";
            this.basicMarkerLayerToolStripMenuItem.Click += new EventHandler(this.basicMarkerLayerToolStripMenuItem_Click);
            this.basicLineLayerToolStripMenuItem.Name = "basicLineLayerToolStripMenuItem";
            this.basicLineLayerToolStripMenuItem.Size = new Size(86, 22);
            this.basicLineLayerToolStripMenuItem.Text = "线";
            this.basicLineLayerToolStripMenuItem.Click += new EventHandler(this.basicLineLayerToolStripMenuItem_Click);
            this.basicFillLayerToolStripMenuItem.Name = "basicFillLayerToolStripMenuItem";
            this.basicFillLayerToolStripMenuItem.Size = new Size(86, 22);
            this.basicFillLayerToolStripMenuItem.Text = "面";
            this.basicFillLayerToolStripMenuItem.Click += new EventHandler(this.basicFillLayerToolStripMenuItem_Click);
            this.tnMoveDown.Enabled = false;
            this.tnMoveDown.Image = (Image) resources.GetObject("tnMoveDown.Image");
            this.tnMoveDown.Location = new System.Drawing.Point(118, 231);
            this.tnMoveDown.Name = "tnMoveDown";
            this.tnMoveDown.Size = new Size(31, 24);
            this.tnMoveDown.TabIndex = 25;
            this.tnMoveDown.Click += new EventHandler(this.tnMoveDown_Click);
            this.btnMoveUp.Enabled = false;
            this.btnMoveUp.Image = (Image) resources.GetObject("btnMoveUp.Image");
            this.btnMoveUp.Location = new System.Drawing.Point(86, 231);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new Size(31, 24);
            this.btnMoveUp.TabIndex = 24;
            this.btnMoveUp.Click += new EventHandler(this.btnMoveUp_Click);
            this.btnDeleteLayer.Enabled = false;
            this.btnDeleteLayer.Image = (Image) resources.GetObject("btnDeleteLayer.Image");
            this.btnDeleteLayer.Location = new System.Drawing.Point(54, 231);
            this.btnDeleteLayer.Name = "btnDeleteLayer";
            this.btnDeleteLayer.Size = new Size(31, 24);
            this.btnDeleteLayer.TabIndex = 23;
            this.btnDeleteLayer.Click += new EventHandler(this.btnDeleteLayer_Click);
            this.btnAddLayer.Image = (Image) resources.GetObject("btnAddLayer.Image");
            this.btnAddLayer.Location = new System.Drawing.Point(22, 231);
            this.btnAddLayer.Name = "btnAddLayer";
            this.btnAddLayer.Size = new Size(31, 24);
            this.btnAddLayer.TabIndex = 22;
            this.btnAddLayer.Click += new EventHandler(this.btnAdd_Click);
            base.Controls.Add(this.tabControl1);
            base.Controls.Add(this.btnAddLayer);
            base.Controls.Add(this.tnMoveDown);
            base.Controls.Add(this.btnMoveUp);
            base.Controls.Add(this.btnDeleteLayer);
            base.Name = "RepresationRuleCtrl";
            base.Size = new Size(254, 266);
            base.Load += new EventHandler(this.RepresationRuleCtrl_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

       
        private IContainer components = null;
        private ToolStripMenuItem basicFillLayerToolStripMenuItem;
        private ToolStripMenuItem basicLineLayerToolStripMenuItem;
        private ToolStripMenuItem basicMarkerLayerToolStripMenuItem;
        private SimpleButton btnAddLayer;
        private SimpleButton btnDeleteLayer;
        private SimpleButton btnMoveUp;
        private ContextMenuStrip contextMenuStrip1;
        private SymbolItem symbolItem1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private SimpleButton tnMoveDown;
    }
}