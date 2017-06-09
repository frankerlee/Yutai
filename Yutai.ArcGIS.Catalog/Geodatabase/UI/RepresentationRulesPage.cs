using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal class RepresentationRulesPage : UserControl
    {
        private SimpleButton btnAddLayer;
        private SimpleButton btnDeleteLayer;
        private SimpleButton btnMoveUp;
        private SimpleButton btnOther;
        private ContextMenuStrip contextMenuStrip1;
        private IContainer icontainer_0 = null;
        private IFeatureClass ifeatureClass_0 = null;
        private ToolStripMenuItem importSymbolToolStripMenuItem;
        private IRepresentationRules irepresentationRules_0 = null;
        private ToolStripMenuItem loadRuleToolStripMenuItem;
    //    private RepresationRuleCtrl represationRuleCtrl1;
        private RepresentationruleListBox representationruleListBox1;
        private SimpleButton tnMoveDown;

        public RepresentationRulesPage()
        {
            this.InitializeComponent();
        }

        private void btnAddLayer_Click(object sender, EventArgs e)
        {
            IRepresentationRule repRule = this.method_1(this.ifeatureClass_0);
            this.irepresentationRules_0.Add(repRule);
            this.representationruleListBox1.Reset();
        }

        private void btnDeleteLayer_Click(object sender, EventArgs e)
        {
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.representationruleListBox1.SelectedIndex;
            IRepresentationRule selectRepresentationRule = this.representationruleListBox1.SelectRepresentationRule;
            int selectRepresentationRuleID = this.representationruleListBox1.SelectRepresentationRuleID;
            string selectRepresentationRuleName = this.representationruleListBox1.SelectRepresentationRuleName;
            this.irepresentationRules_0.set_Index(selectRepresentationRuleID, selectedIndex - 1);
            this.representationruleListBox1.Reset();
        }

        private void btnOther_Click(object sender, EventArgs e)
        {
            this.contextMenuStrip1.Show(this, this.btnOther.Right, this.btnOther.Top);
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void importSymbolToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
            this.representationruleListBox1.Size = new Size(0xad, 230);
            this.representationruleListBox1.TabIndex = 0;
            this.representationruleListBox1.SelectedIndexChanged += new EventHandler(this.representationruleListBox1_SelectedIndexChanged);
            //this.represationRuleCtrl1.Location = new System.Drawing.Point(0xb6, 3);
            //this.represationRuleCtrl1.Name = "represationRuleCtrl1";
            //this.represationRuleCtrl1.RepresentationRule = null;
            //this.represationRuleCtrl1.RepresentationRuleItem = null;
            //this.represationRuleCtrl1.Size = new Size(0xfd, 0x107);
            //this.represationRuleCtrl1.TabIndex = 1;
            this.btnAddLayer.Image = (Image)resources.GetObject("btnAddLayer.Image");
            this.btnAddLayer.Location = new System.Drawing.Point(3, 0xf2);
            this.btnAddLayer.Name = "btnAddLayer";
            this.btnAddLayer.Size = new Size(0x1f, 0x18);
            this.btnAddLayer.TabIndex = 0x1a;
            this.btnAddLayer.Click += new EventHandler(this.btnAddLayer_Click);
            this.tnMoveDown.Enabled = false;
            this.tnMoveDown.Image = (Image)resources.GetObject("tnMoveDown.Image");
            this.tnMoveDown.Location = new System.Drawing.Point(0x63, 0xf2);
            this.tnMoveDown.Name = "tnMoveDown";
            this.tnMoveDown.Size = new Size(0x1f, 0x18);
            this.tnMoveDown.TabIndex = 0x1d;
            this.tnMoveDown.Click += new EventHandler(this.tnMoveDown_Click);
            this.btnMoveUp.Enabled = false;
            this.btnMoveUp.Image = (Image)resources.GetObject("btnMoveUp.Image");
            this.btnMoveUp.Location = new System.Drawing.Point(0x43, 0xf2);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new Size(0x1f, 0x18);
            this.btnMoveUp.TabIndex = 0x1c;
            this.btnMoveUp.Click += new EventHandler(this.btnMoveUp_Click);
            this.btnDeleteLayer.Enabled = false;
            this.btnDeleteLayer.Image = (Image)resources.GetObject("btnDeleteLayer.Image");
            this.btnDeleteLayer.Location = new System.Drawing.Point(0x23, 0xf2);
            this.btnDeleteLayer.Name = "btnDeleteLayer";
            this.btnDeleteLayer.Size = new Size(0x1f, 0x18);
            this.btnDeleteLayer.TabIndex = 0x1b;
            this.btnDeleteLayer.Click += new EventHandler(this.btnDeleteLayer_Click);
            this.btnOther.Enabled = false;
            this.btnOther.Image = (Image) resources.GetObject("btnOther.Image");
            this.btnOther.Location = new System.Drawing.Point(0x88, 0xf2);
            this.btnOther.Name = "btnOther";
            this.btnOther.Size = new Size(0x1f, 0x18);
            this.btnOther.TabIndex = 30;
            this.btnOther.Click += new EventHandler(this.btnOther_Click);
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.loadRuleToolStripMenuItem, this.importSymbolToolStripMenuItem });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new Size(0x9f, 0x30);
            this.loadRuleToolStripMenuItem.Name = "loadRuleToolStripMenuItem";
            this.loadRuleToolStripMenuItem.Size = new Size(0x9e, 0x16);
            this.loadRuleToolStripMenuItem.Text = "从符号库中转入";
            this.loadRuleToolStripMenuItem.Click += new EventHandler(this.loadRuleToolStripMenuItem_Click);
            this.importSymbolToolStripMenuItem.Name = "importSymbolToolStripMenuItem";
            this.importSymbolToolStripMenuItem.Size = new Size(0x9e, 0x16);
            this.importSymbolToolStripMenuItem.Text = "符号导入";
            this.importSymbolToolStripMenuItem.Click += new EventHandler(this.importSymbolToolStripMenuItem_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.btnOther);
            base.Controls.Add(this.btnAddLayer);
            base.Controls.Add(this.tnMoveDown);
            base.Controls.Add(this.btnMoveUp);
            base.Controls.Add(this.btnDeleteLayer);
      //      base.Controls.Add(this.represationRuleCtrl1);
            base.Controls.Add(this.representationruleListBox1);
            base.Name = "RepresentationRulesPage";
            base.Size = new Size(0x1bd, 0x113);
            base.Load += new EventHandler(this.RepresentationRulesPage_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void loadRuleToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private IBasicSymbol method_0(IFeatureClass ifeatureClass_1)
        {
            IBasicSymbol symbol = null;
            if ((ifeatureClass_1.ShapeType == esriGeometryType.esriGeometryMultipoint) || (ifeatureClass_1.ShapeType == esriGeometryType.esriGeometryPoint))
            {
                return new BasicMarkerSymbolClass();
            }
            if (ifeatureClass_1.ShapeType == esriGeometryType.esriGeometryPolyline)
            {
                return new BasicLineSymbolClass();
            }
            if (ifeatureClass_1.ShapeType == esriGeometryType.esriGeometryPolygon)
            {
                symbol = new BasicFillSymbolClass();
            }
            return symbol;
        }

        private IRepresentationRule method_1(IFeatureClass ifeatureClass_1)
        {
            IBasicSymbol symbol = this.method_0(ifeatureClass_1);
            IRepresentationRule rule = new RepresentationRuleClass();
            rule.InsertLayer(0, symbol);
            return rule;
        }

        private void representationruleListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            IRepresentationRule selectRepresentationRule = this.representationruleListBox1.SelectRepresentationRule;
            if (selectRepresentationRule != null)
            {
                //this.represationRuleCtrl1.RepresentationRule = selectRepresentationRule;
                //this.represationRuleCtrl1.Init();
                this.btnDeleteLayer.Enabled = this.representationruleListBox1.Items.Count > 1;
                if (this.representationruleListBox1.Items.Count > 1)
                {
                    this.btnMoveUp.Enabled = this.representationruleListBox1.SelectedIndex != 0;
                    this.tnMoveDown.Enabled = this.representationruleListBox1.SelectedIndex != (this.representationruleListBox1.Items.Count - 1);
                }
                else
                {
                    this.btnMoveUp.Enabled = false;
                    this.tnMoveDown.Enabled = false;
                }
            }
            else
            {
                //this.represationRuleCtrl1.RepresentationRule = null;
                //this.represationRuleCtrl1.Init();
                this.btnDeleteLayer.Enabled = false;
                this.btnMoveUp.Enabled = false;
                this.tnMoveDown.Enabled = false;
            }
        }

        private void RepresentationRulesPage_Load(object sender, EventArgs e)
        {
            this.representationruleListBox1.Init(this.irepresentationRules_0);
            //this.represationRuleCtrl1.GeometryType_2 = this.ifeatureClass_0.ShapeType;
            //this.represationRuleCtrl1.CanEdit = false;
        }

        private void tnMoveDown_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.representationruleListBox1.SelectedIndex;
            IRepresentationRule selectRepresentationRule = this.representationruleListBox1.SelectRepresentationRule;
            int selectRepresentationRuleID = this.representationruleListBox1.SelectRepresentationRuleID;
            string selectRepresentationRuleName = this.representationruleListBox1.SelectRepresentationRuleName;
            this.irepresentationRules_0.set_Index(selectRepresentationRuleID, selectedIndex + 1);
            this.representationruleListBox1.Reset();
        }

        public IFeatureClass FeatureClass
        {
            set
            {
                this.ifeatureClass_0 = value;
            }
        }

        public IRepresentationRules RepresentationRules
        {
            set
            {
                this.irepresentationRules_0 = value;
            }
        }
    }
}

