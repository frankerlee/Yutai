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
    public class RepresationRuleCtrl : UserControl
    {
        private ToolStripMenuItem basicFillLayerToolStripMenuItem;
        private ToolStripMenuItem basicLineLayerToolStripMenuItem;
        private ToolStripMenuItem basicMarkerLayerToolStripMenuItem;
        private SimpleButton btnAddLayer;
        private SimpleButton btnDeleteLayer;
        private SimpleButton btnMoveUp;
        private IContainer components = null;
        private ContextMenuStrip contextMenuStrip1;
        private BasicMarkerSymbolLayer layer1 = null;
        private bool m_CanEdit = true;
        private IRepresentationRuleItem m_oldRepresentationRuleItem = null;
        private IRepresentationRule m_pRepresentationRule = null;
        private IRepresentationRuleItem m_pRepresentationRuleItem = null;
        private SymbolItem symbolItem1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private SimpleButton tnMoveDown;

        public RepresationRuleCtrl()
        {
            this.InitializeComponent();
        }

        private void basicFillLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabPage page = new TabPage("面");
            IBasicFillSymbol symbol = new BasicFillSymbolClass();
            this.m_pRepresentationRule.InsertLayer(this.m_pRepresentationRule.LayerCount, symbol as IBasicSymbol);
            BasicFillSymbolLayer layer = new BasicFillSymbolLayer {
                BasicSymbol = symbol as IBasicSymbol,
                GeometryType = this.m_pRepresentationRuleItem.GeometryType
            };
            page.Controls.Add(layer);
            layer.Dock = DockStyle.Fill;
            this.tabControl1.TabPages.Add(page);
        }

        private void basicLineLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabPage page = new TabPage("线");
            IBasicLineSymbol symbol = new BasicLineSymbolClass();
            this.m_pRepresentationRule.InsertLayer(this.m_pRepresentationRule.LayerCount, symbol as IBasicSymbol);
            BasicLineSymbolLayer layer = new BasicLineSymbolLayer {
                GeometryType = this.m_pRepresentationRuleItem.GeometryType,
                BasicSymbol = symbol as IBasicSymbol
            };
            page.Controls.Add(layer);
            layer.Dock = DockStyle.Fill;
            this.tabControl1.TabPages.Add(page);
        }

        private void basicMarkerLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IMarkerPlacement placement;
            TabPage page = new TabPage("点");
            IBasicMarkerSymbol symbol = new BasicMarkerSymbolClass();
            if (this.m_pRepresentationRuleItem.GeometryType == esriGeometryType.esriGeometryPolyline)
            {
                placement = new MarkerPlacementAlongLineClass();
                symbol.MarkerPlacement = placement;
            }
            else if (this.m_pRepresentationRuleItem.GeometryType == esriGeometryType.esriGeometryPolygon)
            {
                placement = new MarkerPlacementInsidePolygonClass();
                symbol.MarkerPlacement = placement;
            }
            this.m_pRepresentationRule.InsertLayer(this.m_pRepresentationRule.LayerCount, symbol as IBasicSymbol);
            BasicMarkerSymbolLayer layer = new BasicMarkerSymbolLayer {
                GeometryType = this.m_pRepresentationRuleItem.GeometryType,
                BasicSymbol = symbol as IBasicSymbol
            };
            page.Controls.Add(layer);
            layer.Dock = DockStyle.Fill;
            this.tabControl1.TabPages.Add(page);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.contextMenuStrip1.Show(this, this.btnAddLayer.Left, this.btnAddLayer.Bottom);
        }

        private void btnDeleteLayer_Click(object sender, EventArgs e)
        {
            this.m_pRepresentationRule.RemoveLayer(this.tabControl1.SelectedIndex);
            this.tabControl1.TabPages.RemoveAt(this.tabControl1.SelectedIndex);
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            TabPage tabPage = this.tabControl1.TabPages[this.tabControl1.SelectedIndex];
            IBasicSymbol symbol = this.m_pRepresentationRule.get_Layer(this.tabControl1.SelectedIndex - 1);
            this.m_pRepresentationRule.RemoveLayer(this.tabControl1.SelectedIndex - 1);
            this.m_pRepresentationRule.InsertLayer(this.tabControl1.SelectedIndex - 2, symbol);
            int index = this.tabControl1.SelectedIndex - 1;
            this.tabControl1.TabPages.RemoveAt(this.tabControl1.SelectedIndex);
            this.tabControl1.TabPages.Insert(index, tabPage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void Init()
        {
            int num;
            this.contextMenuStrip1.Items.Clear();
            for (num = this.tabControl1.TabCount - 1; num > 0; num--)
            {
                this.tabControl1.TabPages.RemoveAt(num);
            }
            this.symbolItem1.Symbol = this.m_pRepresentationRuleItem;
            if (this.m_pRepresentationRule != null)
            {
                for (num = 0; num < this.m_pRepresentationRule.LayerCount; num++)
                {
                    TabPage page;
                    IBasicSymbol symbol = this.m_pRepresentationRule.get_Layer(num);
                    if (symbol is IBasicMarkerSymbol)
                    {
                        this.contextMenuStrip1.Items.Add(this.basicMarkerLayerToolStripMenuItem);
                        page = new TabPage("点");
                        BasicMarkerSymbolLayer layer = new BasicMarkerSymbolLayer {
                            BasicSymbol = symbol,
                            GeometryType = this.m_pRepresentationRuleItem.GeometryType
                        };
                        page.Controls.Add(layer);
                        layer.Dock = DockStyle.Fill;
                        this.tabControl1.TabPages.Add(page);
                    }
                    else if (symbol is IBasicLineSymbol)
                    {
                        this.contextMenuStrip1.Items.Add(this.basicMarkerLayerToolStripMenuItem);
                        this.contextMenuStrip1.Items.Add(this.basicLineLayerToolStripMenuItem);
                        page = new TabPage("线");
                        BasicLineSymbolLayer layer2 = new BasicLineSymbolLayer {
                            BasicSymbol = symbol,
                            GeometryType = this.m_pRepresentationRuleItem.GeometryType
                        };
                        page.Controls.Add(layer2);
                        layer2.Dock = DockStyle.Fill;
                        this.tabControl1.TabPages.Add(page);
                    }
                    else if (symbol is IBasicFillSymbol)
                    {
                        this.contextMenuStrip1.Items.Add(this.basicMarkerLayerToolStripMenuItem);
                        this.contextMenuStrip1.Items.Add(this.basicLineLayerToolStripMenuItem);
                        this.contextMenuStrip1.Items.Add(this.basicFillLayerToolStripMenuItem);
                        page = new TabPage("面");
                        BasicFillSymbolLayer layer3 = new BasicFillSymbolLayer {
                            BasicSymbol = symbol,
                            GeometryType = this.m_pRepresentationRuleItem.GeometryType
                        };
                        page.Controls.Add(layer3);
                        layer3.Dock = DockStyle.Fill;
                        this.tabControl1.TabPages.Add(page);
                    }
                }
            }
            this.btnAddLayer.Enabled = this.m_CanEdit;
            this.btnDeleteLayer.Enabled = this.m_CanEdit;
            this.btnMoveUp.Enabled = this.m_CanEdit;
            this.tnMoveDown.Enabled = this.m_CanEdit;
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
            this.tabControl1.Size = new Size(0xf8, 0xde);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new EventHandler(this.tabControl1_SelectedIndexChanged);
            this.tabPage1.Controls.Add(this.symbolItem1);
            this.tabPage1.Location = new System.Drawing.Point(0x16, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new Size(0xde, 0xd6);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "预览";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.symbolItem1.BackColor = SystemColors.ControlLight;
            this.symbolItem1.Location = new System.Drawing.Point(13, 0x4c);
            this.symbolItem1.Name = "symbolItem1";
            this.symbolItem1.Size = new Size(0xc4, 0x5c);
            this.symbolItem1.Symbol = null;
            this.symbolItem1.TabIndex = 2;
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.basicMarkerLayerToolStripMenuItem, this.basicLineLayerToolStripMenuItem, this.basicFillLayerToolStripMenuItem });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new Size(0x57, 70);
            this.basicMarkerLayerToolStripMenuItem.Name = "basicMarkerLayerToolStripMenuItem";
            this.basicMarkerLayerToolStripMenuItem.Size = new Size(0x56, 0x16);
            this.basicMarkerLayerToolStripMenuItem.Text = "点";
            this.basicMarkerLayerToolStripMenuItem.Click += new EventHandler(this.basicMarkerLayerToolStripMenuItem_Click);
            this.basicLineLayerToolStripMenuItem.Name = "basicLineLayerToolStripMenuItem";
            this.basicLineLayerToolStripMenuItem.Size = new Size(0x56, 0x16);
            this.basicLineLayerToolStripMenuItem.Text = "线";
            this.basicLineLayerToolStripMenuItem.Click += new EventHandler(this.basicLineLayerToolStripMenuItem_Click);
            this.basicFillLayerToolStripMenuItem.Name = "basicFillLayerToolStripMenuItem";
            this.basicFillLayerToolStripMenuItem.Size = new Size(0x56, 0x16);
            this.basicFillLayerToolStripMenuItem.Text = "面";
            this.basicFillLayerToolStripMenuItem.Click += new EventHandler(this.basicFillLayerToolStripMenuItem_Click);
            this.tnMoveDown.Enabled = false;
            this.tnMoveDown.Image = (Image) resources.GetObject("tnMoveDown.Image");
            this.tnMoveDown.Location = new System.Drawing.Point(0x76, 0xe7);
            this.tnMoveDown.Name = "tnMoveDown";
            this.tnMoveDown.Size = new Size(0x1f, 0x18);
            this.tnMoveDown.TabIndex = 0x19;
            this.tnMoveDown.Click += new EventHandler(this.tnMoveDown_Click);
            this.btnMoveUp.Enabled = false;
            this.btnMoveUp.Image = (Image) resources.GetObject("btnMoveUp.Image");
            this.btnMoveUp.Location = new System.Drawing.Point(0x56, 0xe7);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new Size(0x1f, 0x18);
            this.btnMoveUp.TabIndex = 0x18;
            this.btnMoveUp.Click += new EventHandler(this.btnMoveUp_Click);
            this.btnDeleteLayer.Enabled = false;
            this.btnDeleteLayer.Image = (Image) resources.GetObject("btnDeleteLayer.Image");
            this.btnDeleteLayer.Location = new System.Drawing.Point(0x36, 0xe7);
            this.btnDeleteLayer.Name = "btnDeleteLayer";
            this.btnDeleteLayer.Size = new Size(0x1f, 0x18);
            this.btnDeleteLayer.TabIndex = 0x17;
            this.btnDeleteLayer.Click += new EventHandler(this.btnDeleteLayer_Click);
            this.btnAddLayer.Image = (Image) resources.GetObject("btnAddLayer.Image");
            this.btnAddLayer.Location = new System.Drawing.Point(0x16, 0xe7);
            this.btnAddLayer.Name = "btnAddLayer";
            this.btnAddLayer.Size = new Size(0x1f, 0x18);
            this.btnAddLayer.TabIndex = 0x16;
            this.btnAddLayer.Click += new EventHandler(this.btnAdd_Click);
            base.Controls.Add(this.tabControl1);
            base.Controls.Add(this.btnAddLayer);
            base.Controls.Add(this.tnMoveDown);
            base.Controls.Add(this.btnMoveUp);
            base.Controls.Add(this.btnDeleteLayer);
            base.Name = "RepresationRuleCtrl";
            base.Size = new Size(0xfe, 0x10a);
            base.Load += new EventHandler(this.RepresationRuleCtrl_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void RepresationRuleCtrl_Load(object sender, EventArgs e)
        {
            this.contextMenuStrip1.Items.Clear();
            if (this.m_pRepresentationRuleItem != null)
            {
                this.symbolItem1.Symbol = this.m_pRepresentationRuleItem;
            }
            if (this.m_pRepresentationRule != null)
            {
                for (int i = 0; i < this.m_pRepresentationRule.LayerCount; i++)
                {
                    TabPage page;
                    IBasicSymbol symbol = this.m_pRepresentationRule.get_Layer(i);
                    if (symbol is IBasicMarkerSymbol)
                    {
                        this.contextMenuStrip1.Items.Add(this.basicMarkerLayerToolStripMenuItem);
                        page = new TabPage("点");
                        BasicMarkerSymbolLayer layer = new BasicMarkerSymbolLayer {
                            BasicSymbol = symbol,
                            GeometryType = this.m_pRepresentationRuleItem.GeometryType
                        };
                        page.Controls.Add(layer);
                        layer.Dock = DockStyle.Fill;
                        this.tabControl1.TabPages.Add(page);
                    }
                    else if (symbol is IBasicLineSymbol)
                    {
                        this.contextMenuStrip1.Items.Add(this.basicMarkerLayerToolStripMenuItem);
                        this.contextMenuStrip1.Items.Add(this.basicLineLayerToolStripMenuItem);
                        page = new TabPage("线");
                        BasicLineSymbolLayer layer2 = new BasicLineSymbolLayer {
                            BasicSymbol = symbol,
                            GeometryType = this.m_pRepresentationRuleItem.GeometryType
                        };
                        page.Controls.Add(layer2);
                        layer2.Dock = DockStyle.Fill;
                        this.tabControl1.TabPages.Add(page);
                    }
                    else if (symbol is IBasicFillSymbol)
                    {
                        this.contextMenuStrip1.Items.Add(this.basicMarkerLayerToolStripMenuItem);
                        this.contextMenuStrip1.Items.Add(this.basicLineLayerToolStripMenuItem);
                        this.contextMenuStrip1.Items.Add(this.basicFillLayerToolStripMenuItem);
                        page = new TabPage("面");
                        BasicFillSymbolLayer layer3 = new BasicFillSymbolLayer {
                            BasicSymbol = symbol,
                            GeometryType = this.m_pRepresentationRuleItem.GeometryType
                        };
                        page.Controls.Add(layer3);
                        layer3.Dock = DockStyle.Fill;
                        this.tabControl1.TabPages.Add(page);
                    }
                }
            }
            this.btnAddLayer.Enabled = this.m_CanEdit;
            this.btnDeleteLayer.Enabled = this.m_CanEdit;
            this.btnMoveUp.Enabled = this.m_CanEdit;
            this.tnMoveDown.Enabled = this.m_CanEdit;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl1.TabCount == 2)
            {
                this.btnMoveUp.Enabled = false;
                this.tnMoveDown.Enabled = false;
                this.btnDeleteLayer.Enabled = false;
            }
            else
            {
                this.btnDeleteLayer.Enabled = this.tabControl1.SelectedIndex > 0;
                this.btnMoveUp.Enabled = this.tabControl1.SelectedIndex > 1;
                this.tnMoveDown.Enabled = this.tabControl1.SelectedIndex < this.tabControl1.TabCount;
            }
        }

        private void tnMoveDown_Click(object sender, EventArgs e)
        {
            IBasicSymbol symbol = this.m_pRepresentationRule.get_Layer(this.tabControl1.SelectedIndex - 1);
            this.m_pRepresentationRule.RemoveLayer(this.tabControl1.SelectedIndex - 1);
            this.m_pRepresentationRule.InsertLayer(this.tabControl1.SelectedIndex, symbol);
            TabPage tabPage = this.tabControl1.TabPages[this.tabControl1.SelectedIndex];
            int index = this.tabControl1.SelectedIndex + 1;
            this.tabControl1.TabPages.RemoveAt(this.tabControl1.SelectedIndex);
            this.tabControl1.TabPages.Insert(index, tabPage);
        }

        public bool CanEdit
        {
            set
            {
                this.m_CanEdit = value;
            }
        }

        public esriGeometryType GeometryType
        {
            set
            {
                if (this.m_pRepresentationRuleItem == null)
                {
                    this.m_pRepresentationRuleItem = new RepresentationRuleItemClass();
                }
                this.m_pRepresentationRuleItem.GeometryType = value;
            }
        }

        public IRepresentationRule RepresentationRule
        {
            get
            {
                return this.m_pRepresentationRule;
            }
            set
            {
                if (value != null)
                {
                    if (this.m_pRepresentationRuleItem == null)
                    {
                        this.m_pRepresentationRuleItem = new RepresentationRuleItemClass();
                    }
                    this.m_pRepresentationRuleItem.RepresentationRule = value;
                    this.m_pRepresentationRule = this.m_pRepresentationRuleItem.RepresentationRule;
                }
            }
        }

        public IRepresentationRuleItem RepresentationRuleItem
        {
            get
            {
                return this.m_pRepresentationRuleItem;
            }
            set
            {
                if (value != null)
                {
                    this.m_pRepresentationRuleItem = (value as IClone).Clone() as IRepresentationRuleItem;
                    this.m_pRepresentationRule = this.m_pRepresentationRuleItem.RepresentationRule;
                }
            }
        }
    }
}

