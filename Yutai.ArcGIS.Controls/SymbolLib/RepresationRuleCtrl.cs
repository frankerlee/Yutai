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
    public partial class RepresationRuleCtrl : UserControl
    {
        private BasicMarkerSymbolLayer layer1 = null;
        private bool m_CanEdit = true;
        private IRepresentationRuleItem m_oldRepresentationRuleItem = null;
        private IRepresentationRule m_pRepresentationRule = null;
        private IRepresentationRuleItem m_pRepresentationRuleItem = null;

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

