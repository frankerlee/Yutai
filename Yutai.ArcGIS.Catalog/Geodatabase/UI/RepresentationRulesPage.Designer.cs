using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolLib;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class RepresentationRulesPage
    {
        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

       
        private void InitializeComponent()
        {
            this.icontainer_0 = new Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RepresentationRulesPage));
            this.representationruleListBox1 = new RepresentationruleListBox();
       //     this.represationRuleCtrl1 = new RepresationRuleCtrl();
            this.btnAddLayer = new SimpleButton();
            this.tnMoveDown = new SimpleButton();
            this.btnMoveUp = new SimpleButton();
            this.btnDeleteLayer = new SimpleButton();
            this.btnOther = new SimpleButton();
            this.contextMenuStrip1 = new ContextMenuStrip(this.icontainer_0);
            this.loadRuleToolStripMenuItem = new ToolStripMenuItem();
            this.importSymbolToolStripMenuItem = new ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            base.SuspendLayout();
            this.representationruleListBox1.DrawMode = DrawMode.OwnerDrawVariable;
            this.representationruleListBox1.FormattingEnabled = true;
            this.representationruleListBox1.Location = new System.Drawing.Point(3, 3);
            this.representationruleListBox1.Name = "representationruleListBox1";
            this.representationruleListBox1.Size = new Size(173, 230);
            this.representationruleListBox1.TabIndex = 0;
            this.representationruleListBox1.SelectedIndexChanged += new EventHandler(this.representationruleListBox1_SelectedIndexChanged);
            //this.represationRuleCtrl1.Location = new System.Drawing.Point(182, 3);
            //this.represationRuleCtrl1.Name = "represationRuleCtrl1";
            //this.represationRuleCtrl1.RepresentationRule = null;
            //this.represationRuleCtrl1.RepresentationRuleItem = null;
            //this.represationRuleCtrl1.Size = new Size(253, 263);
            //this.represationRuleCtrl1.TabIndex = 1;
            this.btnAddLayer.Image = (Image)resources.GetObject("btnAddLayer.Image");
            this.btnAddLayer.Location = new System.Drawing.Point(3, 242);
            this.btnAddLayer.Name = "btnAddLayer";
            this.btnAddLayer.Size = new Size(31, 24);
            this.btnAddLayer.TabIndex = 26;
            this.btnAddLayer.Click += new EventHandler(this.btnAddLayer_Click);
            this.tnMoveDown.Enabled = false;
            this.tnMoveDown.Image = (Image)resources.GetObject("tnMoveDown.Image");
            this.tnMoveDown.Location = new System.Drawing.Point(99, 242);
            this.tnMoveDown.Name = "tnMoveDown";
            this.tnMoveDown.Size = new Size(31, 24);
            this.tnMoveDown.TabIndex = 29;
            this.tnMoveDown.Click += new EventHandler(this.tnMoveDown_Click);
            this.btnMoveUp.Enabled = false;
            this.btnMoveUp.Image = (Image)resources.GetObject("btnMoveUp.Image");
            this.btnMoveUp.Location = new System.Drawing.Point(67, 242);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new Size(31, 24);
            this.btnMoveUp.TabIndex = 28;
            this.btnMoveUp.Click += new EventHandler(this.btnMoveUp_Click);
            this.btnDeleteLayer.Enabled = false;
            this.btnDeleteLayer.Image = (Image)resources.GetObject("btnDeleteLayer.Image");
            this.btnDeleteLayer.Location = new System.Drawing.Point(35, 242);
            this.btnDeleteLayer.Name = "btnDeleteLayer";
            this.btnDeleteLayer.Size = new Size(31, 24);
            this.btnDeleteLayer.TabIndex = 27;
            this.btnDeleteLayer.Click += new EventHandler(this.btnDeleteLayer_Click);
            this.btnOther.Enabled = false;
            this.btnOther.Image = (Image) resources.GetObject("btnOther.Image");
            this.btnOther.Location = new System.Drawing.Point(136, 242);
            this.btnOther.Name = "btnOther";
            this.btnOther.Size = new Size(31, 24);
            this.btnOther.TabIndex = 30;
            this.btnOther.Click += new EventHandler(this.btnOther_Click);
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.loadRuleToolStripMenuItem, this.importSymbolToolStripMenuItem });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new Size(159, 48);
            this.loadRuleToolStripMenuItem.Name = "loadRuleToolStripMenuItem";
            this.loadRuleToolStripMenuItem.Size = new Size(158, 22);
            this.loadRuleToolStripMenuItem.Text = "从符号库中转入";
            this.loadRuleToolStripMenuItem.Click += new EventHandler(this.loadRuleToolStripMenuItem_Click);
            this.importSymbolToolStripMenuItem.Name = "importSymbolToolStripMenuItem";
            this.importSymbolToolStripMenuItem.Size = new Size(158, 22);
            this.importSymbolToolStripMenuItem.Text = "符号导入";
            this.importSymbolToolStripMenuItem.Click += new EventHandler(this.importSymbolToolStripMenuItem_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.btnOther);
            base.Controls.Add(this.btnAddLayer);
            base.Controls.Add(this.tnMoveDown);
            base.Controls.Add(this.btnMoveUp);
            base.Controls.Add(this.btnDeleteLayer);
      //      base.Controls.Add(this.represationRuleCtrl1);
            base.Controls.Add(this.representationruleListBox1);
            base.Name = "RepresentationRulesPage";
            base.Size = new Size(445, 275);
            base.Load += new EventHandler(this.RepresentationRulesPage_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnAddLayer;
        private SimpleButton btnDeleteLayer;
        private SimpleButton btnMoveUp;
        private SimpleButton btnOther;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem importSymbolToolStripMenuItem;
        private ToolStripMenuItem loadRuleToolStripMenuItem;
        private RepresationRuleCtrl represationRuleCtrl1;
        private RepresentationruleListBox representationruleListBox1;
        private SimpleButton tnMoveDown;
    }
}