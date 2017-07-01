using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Common.SymbolUI;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    /// <summary>
    /// 线符号编辑窗体
    /// </summary>
    [Guid("8FB4B208-6846-4144-A4DA-52650462479E")]
    public partial class frmLineSymbolEdit : Form
    {
        private double[] point_unit_to = new double[] {1, 0.01388889, 0.0352777778, 0.352777778};


        private bool m_CanDo = true;

        private double[] m_dblScaleRatio = new double[] {0.25, 0.5, 1, 1.25, 2};

        private int m_ScaleIndex = 2;

        private int m_OldSelItem = 0;

        public IStyleGallery m_pSG;

        private int m_oldSel = -1;

        private int m_oldSelType = -1;

        private ILineSymbol m_CopySymbol = null;


        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        public frmLineSymbolEdit()
        {
            this.InitializeComponent();
        }

        private void btnAddLayer_Click(object sender, EventArgs e)
        {
            ILineSymbol lineSymbol = this.CreateNewSymbol(this.cboLineType.SelectedIndex);
            this.m_pMultiLineSymbol.AddLayer(lineSymbol);
            this.m_OldSelItem = 0;
            this.m_pMultiLineSymbol.MoveLayer(lineSymbol, 0);
            ((ILayerColorLock) this.m_pMultiLineSymbol).LayerColorLock[0] = false;
            this.InitControl((ISymbol) this.m_pMultiLineSymbol);
            this.symbolListBox1.Invalidate();
            this.symbolItem1.Invalidate();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.m_pSG = null;
            base.Close();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            ILineSymbol layer = this.m_pMultiLineSymbol.Layer[this.symbolListBox1.SelectedIndex];
            this.m_CopySymbol = (ILineSymbol) ((IClone) layer).Clone();
        }

        private void btnDeleteLayer_Click(object sender, EventArgs e)
        {
            if (this.m_pMultiLineSymbol.LayerCount != 1)
            {
                ILineSymbol layer = this.m_pMultiLineSymbol.Layer[this.symbolListBox1.SelectedIndex];
                this.m_OldSelItem = this.symbolListBox1.SelectedIndex - 1;
                if (this.m_OldSelItem == -1)
                {
                    this.m_OldSelItem = 0;
                }
                this.m_pMultiLineSymbol.DeleteLayer(layer);
                this.InitControl((ISymbol) this.m_pMultiLineSymbol);
                this.symbolListBox1.Invalidate();
                this.symbolItem1.Invalidate();
            }
        }

        private void btnFixZoomIn_Click(object sender, EventArgs e)
        {
            if (this.m_ScaleIndex != 0)
            {
                frmLineSymbolEdit mScaleIndex = this;
                mScaleIndex.m_ScaleIndex = mScaleIndex.m_ScaleIndex - 1;
                this.symbolItem1.ScaleRatio = this.m_dblScaleRatio[this.m_ScaleIndex];
                this.m_CanDo = false;
                this.cboScale.SelectedIndex = this.m_ScaleIndex;
                this.m_CanDo = true;
                this.symbolItem1.Invalidate();
            }
        }

        private void btnFixZoomOut_Click(object sender, EventArgs e)
        {
            if (this.m_ScaleIndex != 4)
            {
                frmLineSymbolEdit mScaleIndex = this;
                mScaleIndex.m_ScaleIndex = mScaleIndex.m_ScaleIndex + 1;
                this.symbolItem1.ScaleRatio = this.m_dblScaleRatio[this.m_ScaleIndex];
                this.m_CanDo = false;
                this.cboScale.SelectedIndex = this.m_ScaleIndex;
                this.m_CanDo = true;
                this.symbolItem1.Invalidate();
            }
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            if (this.symbolListBox1.SelectedIndex != 0)
            {
                this.m_OldSelItem = this.symbolListBox1.SelectedIndex - 1;
                ILineSymbol layer = this.m_pMultiLineSymbol.Layer[this.symbolListBox1.SelectedIndex];
                this.m_pMultiLineSymbol.MoveLayer(layer, this.m_OldSelItem);
                this.m_CanDo = false;
                this.InitControl((ISymbol) this.m_pMultiLineSymbol);
                this.m_CanDo = true;
                this.symbolListBox1.Invalidate();
                this.symbolItem1.Invalidate();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.m_pSG = null;
            base.Close();
        }

        private void btnOnt2One_Click(object sender, EventArgs e)
        {
            if (this.m_ScaleIndex != 2)
            {
                this.m_ScaleIndex = 2;
                this.symbolItem1.ScaleRatio = this.m_dblScaleRatio[this.m_ScaleIndex];
                this.m_CanDo = false;
                this.cboScale.SelectedIndex = this.m_ScaleIndex;
                this.m_CanDo = true;
                this.symbolItem1.Invalidate();
            }
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            if (this.m_CopySymbol != null)
            {
                ILineSymbol lineSymbol = (ILineSymbol) ((IClone) this.m_CopySymbol).Clone();
                this.m_pMultiLineSymbol.AddLayer(lineSymbol);
                this.m_OldSelItem = 0;
                this.m_pMultiLineSymbol.MoveLayer(lineSymbol, 0);
                this.InitControl((ISymbol) this.m_pMultiLineSymbol);
                this.symbolListBox1.Invalidate();
                this.symbolItem1.Invalidate();
            }
        }

        private void cboLineType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if (this.m_oldSelType != this.cboLineType.SelectedIndex)
                {
                    this.m_oldSelType = this.cboLineType.SelectedIndex;
                    ILineSymbol lineSymbol = this.CreateNewSymbol(this.cboLineType.SelectedIndex);
                    ILineSymbol layer = this.m_pMultiLineSymbol.Layer[this.symbolListBox1.SelectedIndex];
                    this.m_OldSelItem = this.symbolListBox1.SelectedIndex;
                    this.m_pMultiLineSymbol.AddLayer(lineSymbol);
                    this.m_pMultiLineSymbol.MoveLayer(lineSymbol, this.symbolListBox1.SelectedIndex);
                    ((ILayerColorLock) this.m_pMultiLineSymbol).LayerColorLock[this.symbolListBox1.SelectedIndex] =
                        false;
                    this.m_pMultiLineSymbol.DeleteLayer(layer);
                    this.InitControl((ISymbol) this.m_pMultiLineSymbol);
                    this.symbolListBox1.Invalidate();
                    this.symbolItem1.Invalidate();
                }
            }
        }

        private void cboScale_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_ScaleIndex = this.cboScale.SelectedIndex;
                this.symbolItem1.ScaleRatio = this.m_dblScaleRatio[this.m_ScaleIndex];
                this.symbolItem1.Invalidate();
            }
        }

        private void cboUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                for (int i = 0; i < this.xtraTabControl1.TabPages.Count; i++)
                {
                    CommonInterface item = this.xtraTabControl1.TabPages[i].Controls[0] as CommonInterface;
                    if (item != null)
                    {
                        item.ChangeUnit(this.point_unit_to[this.cboUnit.SelectedIndex]);
                    }
                }
            }
        }

        private ILineSymbol CreateNewSymbol(int type)
        {
            ILineSymbol simpleLineSymbolClass = null;
            switch (type)
            {
                case 0:
                {
                    simpleLineSymbolClass = new SimpleLineSymbolClass();
                    break;
                }
                case 1:
                {
                    simpleLineSymbolClass = new CartographicLineSymbolClass();
                    break;
                }
                case 2:
                {
                    simpleLineSymbolClass = new MarkerLineSymbolClass();
                    break;
                }
                case 3:
                {
                    simpleLineSymbolClass = new HashLineSymbolClass();
                    break;
                }
                case 4:
                {
                    simpleLineSymbolClass = new PictureLineSymbolClass();
                    break;
                }
                case 5:
                {
                    simpleLineSymbolClass = new SimpleLine3DSymbolClass();
                    break;
                }
                case 6:
                {
                    simpleLineSymbolClass = new TextureLineSymbolClass();
                    break;
                }
            }
            return simpleLineSymbolClass;
        }

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        private void frmLineSymbolEdit_Load(object sender, EventArgs e)
        {
            this.m_CanDo = false;
            this.cboUnit.SelectedIndex = 0;
            this.cboScale.SelectedIndex = this.m_ScaleIndex;
            this.m_CanDo = true;
            this.rdoLine.Checked = true;
            this.InitControl((ISymbol) this.m_pMultiLineSymbol);
        }

        public ISymbol GetSymbol()
        {
            ISymbol symbol;
            symbol = (!(this.m_pOldSymbol is IMultiLayerLineSymbol)
                ? (ISymbol) this.m_pMultiLineSymbol
                : (ISymbol) this.m_pMultiLineSymbol);
            return symbol;
        }

        private void InitControl(ISymbol pSym)
        {
            this.symbolItem1.Symbol = pSym;
            if (pSym is IMultiLayerLineSymbol)
            {
                this.symbolListBox1.Items.Clear();
                this.m_oldSel = -1;
                for (int i = 0; i < ((IMultiLayerLineSymbol) pSym).LayerCount; i++)
                {
                    SymbolListItem symbolListItem = new SymbolListItem()
                    {
                        m_pSymbol = (ISymbol) ((IMultiLayerLineSymbol) pSym).Layer[i],
                        m_bVisible = ((ILayerVisible) pSym).LayerVisible[i],
                        m_bLockColor = ((ILayerColorLock) pSym).LayerColorLock[i]
                    };
                    this.symbolListBox1.Items.Add(symbolListItem);
                }
                if (this.symbolListBox1.Items.Count > 0)
                {
                    this.symbolListBox1.SelectedIndex = this.m_OldSelItem;
                }
            }
        }

        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void rdoLine_Click(object sender, EventArgs e)
        {
            if (this.rdoLine.Checked)
            {
                this.symbolItem1.HasDrawPLine = false;
                this.symbolItem1.Invalidate();
            }
        }

        private void rdoPline_Click(object sender, EventArgs e)
        {
            if (this.rdoPline.Checked)
            {
                this.symbolItem1.HasDrawPLine = true;
                this.symbolItem1.Invalidate();
            }
        }

        public void SetSymbol(ISymbol pSym)
        {
            this.m_pOldSymbol = pSym;
            if (pSym is IMultiLayerLineSymbol)
            {
                this.m_pMultiLineSymbol = (IMultiLayerLineSymbol) ((IClone) pSym).Clone();
            }
            else if (pSym is ILineSymbol)
            {
                this.m_pMultiLineSymbol = new MultiLayerLineSymbolClass();
                this.m_pMultiLineSymbol.AddLayer((ILineSymbol) ((IClone) pSym).Clone());
                (this.m_pMultiLineSymbol as ILayerColorLock).LayerColorLock[0] = false;
            }
        }

        private void symbolListBox1_LayerColorLockedChanged(object sender, EventArgs e)
        {
            bool layerColorLock =
                ((ILayerColorLock) this.m_pMultiLineSymbol).LayerColorLock[this.symbolListBox1.SelectedIndex];
            ((ILayerColorLock) this.m_pMultiLineSymbol).LayerColorLock[this.symbolListBox1.SelectedIndex] =
                !layerColorLock;
        }

        private void symbolListBox1_LayerVisibleChanged(object sender, EventArgs e)
        {
            bool layerVisible =
                ((ILayerVisible) this.m_pMultiLineSymbol).LayerVisible[this.symbolListBox1.SelectedIndex];
            ((ILayerVisible) this.m_pMultiLineSymbol).LayerVisible[this.symbolListBox1.SelectedIndex] = !layerVisible;
            this.symbolItem1.Invalidate();
        }

        private void symbolListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            XtraTabPage xtraTabPage;
            CartoLineControl cartoLineControl;
            XtraTabPage xtraTabPage1;
            TemplateControl templateControl;
            XtraTabPage xtraTabPage2;
            LinePropertyControl linePropertyControl;
            XtraTabPage xtraTabPage3;
            if (this.m_oldSel != this.symbolListBox1.SelectedIndex)
            {
                this.m_oldSel = this.symbolListBox1.SelectedIndex;
                ILineSymbol layer = this.m_pMultiLineSymbol.Layer[this.symbolListBox1.SelectedIndex];
                this.xtraTabControl1.TabPages.Clear();
                this.m_CanDo = false;
                if (layer is ISimpleLineSymbol)
                {
                    this.cboLineType.SelectedIndex = 0;
                    XtraTabPage xtraTabPage4 = new XtraTabPage()
                    {
                        Text = "简单线符号"
                    };
                    SimpleLineControl simpleLineControl = new SimpleLineControl()
                    {
                        m_unit = this.point_unit_to[this.cboUnit.SelectedIndex],
                        m_pSimpleLineSymbol = (ISimpleLineSymbol) layer
                    };
                    simpleLineControl.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                    xtraTabPage4.Controls.Add(simpleLineControl);
                    this.xtraTabControl1.TabPages.Add(xtraTabPage4);
                }
                else if (layer is IMarkerLineSymbol)
                {
                    this.cboLineType.SelectedIndex = 2;
                    XtraTabPage xtraTabPage5 = new XtraTabPage()
                    {
                        Text = "标记线"
                    };
                    MarkerLineControl markerLineControl = new MarkerLineControl()
                    {
                        m_pMarkerLineSymbol = (IMarkerLineSymbol) layer,
                        m_pSG = this.m_pSG
                    };
                    markerLineControl.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                    xtraTabPage5.Controls.Add(markerLineControl);
                    xtraTabPage = new XtraTabPage()
                    {
                        Text = "制图线"
                    };
                    cartoLineControl = new CartoLineControl()
                    {
                        m_unit = this.point_unit_to[this.cboUnit.SelectedIndex],
                        m_CartographLineSymbol = (ICartographicLineSymbol) layer
                    };
                    cartoLineControl.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                    xtraTabPage.Controls.Add(cartoLineControl);
                    xtraTabPage1 = new XtraTabPage()
                    {
                        Text = "模板"
                    };
                    templateControl = new TemplateControl()
                    {
                        m_unit = this.point_unit_to[this.cboUnit.SelectedIndex],
                        m_LineProperties = (ILineProperties) layer
                    };
                    templateControl.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                    xtraTabPage1.Controls.Add(templateControl);
                    xtraTabPage2 = new XtraTabPage()
                    {
                        Text = "线属性"
                    };
                    linePropertyControl = new LinePropertyControl()
                    {
                        m_LineProperties = (ILineProperties) layer,
                        m_unit = this.point_unit_to[this.cboUnit.SelectedIndex]
                    };
                    linePropertyControl.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                    xtraTabPage2.Controls.Add(linePropertyControl);
                    this.xtraTabControl1.TabPages.Add(xtraTabPage5);
                    this.xtraTabControl1.TabPages.Add(xtraTabPage);
                    this.xtraTabControl1.TabPages.Add(xtraTabPage1);
                    this.xtraTabControl1.TabPages.Add(xtraTabPage2);
                }
                else if (layer is IHashLineSymbol)
                {
                    this.cboLineType.SelectedIndex = 3;
                    XtraTabPage xtraTabPage6 = new XtraTabPage()
                    {
                        Text = "细切线"
                    };
                    HashLineSymbolControl hashLineSymbolControl = new HashLineSymbolControl()
                    {
                        m_pHashLineSymbol = (IHashLineSymbol) layer,
                        m_pSG = this.m_pSG
                    };
                    hashLineSymbolControl.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                    xtraTabPage6.Controls.Add(hashLineSymbolControl);
                    xtraTabPage = new XtraTabPage()
                    {
                        Text = "制图线"
                    };
                    cartoLineControl = new CartoLineControl()
                    {
                        m_unit = this.point_unit_to[this.cboUnit.SelectedIndex],
                        m_CartographLineSymbol = (ICartographicLineSymbol) layer
                    };
                    cartoLineControl.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                    xtraTabPage.Controls.Add(cartoLineControl);
                    xtraTabPage1 = new XtraTabPage()
                    {
                        Text = "模板"
                    };
                    templateControl = new TemplateControl()
                    {
                        m_LineProperties = (ILineProperties) layer,
                        m_unit = this.point_unit_to[this.cboUnit.SelectedIndex]
                    };
                    templateControl.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                    xtraTabPage1.Controls.Add(templateControl);
                    xtraTabPage2 = new XtraTabPage()
                    {
                        Text = "线属性"
                    };
                    linePropertyControl = new LinePropertyControl()
                    {
                        m_LineProperties = (ILineProperties) layer,
                        m_unit = this.point_unit_to[this.cboUnit.SelectedIndex]
                    };
                    linePropertyControl.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                    xtraTabPage2.Controls.Add(linePropertyControl);
                    this.xtraTabControl1.TabPages.Add(xtraTabPage6);
                    this.xtraTabControl1.TabPages.Add(xtraTabPage);
                    this.xtraTabControl1.TabPages.Add(xtraTabPage1);
                    this.xtraTabControl1.TabPages.Add(xtraTabPage2);
                }
                else if (layer is ICartographicLineSymbol)
                {
                    this.cboLineType.SelectedIndex = 1;
                    xtraTabPage = new XtraTabPage()
                    {
                        Text = "制图线"
                    };
                    cartoLineControl = new CartoLineControl()
                    {
                        m_unit = this.point_unit_to[this.cboUnit.SelectedIndex],
                        m_CartographLineSymbol = (ICartographicLineSymbol) layer
                    };
                    cartoLineControl.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                    xtraTabPage.Controls.Add(cartoLineControl);
                    xtraTabPage1 = new XtraTabPage()
                    {
                        Text = "模板"
                    };
                    templateControl = new TemplateControl()
                    {
                        m_LineProperties = (ILineProperties) layer,
                        m_unit = this.point_unit_to[this.cboUnit.SelectedIndex]
                    };
                    templateControl.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                    xtraTabPage1.Controls.Add(templateControl);
                    xtraTabPage2 = new XtraTabPage()
                    {
                        Text = "线属性"
                    };
                    linePropertyControl = new LinePropertyControl()
                    {
                        m_LineProperties = (ILineProperties) layer,
                        m_unit = this.point_unit_to[this.cboUnit.SelectedIndex]
                    };
                    linePropertyControl.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                    xtraTabPage2.Controls.Add(linePropertyControl);
                    this.xtraTabControl1.TabPages.Add(xtraTabPage);
                    this.xtraTabControl1.TabPages.Add(xtraTabPage1);
                    this.xtraTabControl1.TabPages.Add(xtraTabPage2);
                }
                else if (layer is IPictureLineSymbol)
                {
                    this.cboLineType.SelectedIndex = 4;
                    xtraTabPage3 = new XtraTabPage()
                    {
                        Text = "图片线"
                    };
                    PictureLineControl pictureLineControl = new PictureLineControl()
                    {
                        m_unit = this.point_unit_to[this.cboUnit.SelectedIndex],
                        m_PictureLineSymbol = (IPictureLineSymbol) layer
                    };
                    pictureLineControl.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                    xtraTabPage3.Controls.Add(pictureLineControl);
                    this.xtraTabControl1.TabPages.Add(xtraTabPage3);
                }
                else if (layer is ISimpleLine3DSymbol)
                {
                    this.cboLineType.SelectedIndex = 5;
                    xtraTabPage3 = new XtraTabPage()
                    {
                        Text = "3D简单线"
                    };
                    Simple3DLineSymbolCtrl simple3DLineSymbolCtrl = new Simple3DLineSymbolCtrl()
                    {
                        m_unit = this.point_unit_to[this.cboUnit.SelectedIndex],
                        m_pSimpleLine3DSymbol = (ISimpleLine3DSymbol) layer
                    };
                    simple3DLineSymbolCtrl.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                    xtraTabPage3.Controls.Add(simple3DLineSymbolCtrl);
                    this.xtraTabControl1.TabPages.Add(xtraTabPage3);
                }
                else if (layer is ITextureLineSymbol)
                {
                    this.cboLineType.SelectedIndex = 6;
                    xtraTabPage3 = new XtraTabPage()
                    {
                        Text = "纹理线"
                    };
                    TextureLineSymbolCtrl textureLineSymbolCtrl = new TextureLineSymbolCtrl()
                    {
                        m_unit = this.point_unit_to[this.cboUnit.SelectedIndex],
                        m_pTextureLineSymbol = (ITextureLineSymbol) layer
                    };
                    textureLineSymbolCtrl.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                    xtraTabPage3.Controls.Add(textureLineSymbolCtrl);
                    this.xtraTabControl1.TabPages.Add(xtraTabPage3);
                }
                this.m_oldSelType = this.cboLineType.SelectedIndex;
                this.m_CanDo = true;
            }
        }

        private void symbolListBox1_SelectedIndexChanged1(object sender, EventArgs e)
        {
            TabPage tabPage;
            CartoLineControl cartoLineControl;
            TabPage tabPage1;
            TemplateControl templateControl;
            TabPage tabPage2;
            LinePropertyControl linePropertyControl;
            if (this.m_oldSel != this.symbolListBox1.SelectedIndex)
            {
                this.m_oldSel = this.symbolListBox1.SelectedIndex;
                ILineSymbol layer = this.m_pMultiLineSymbol.Layer[this.symbolListBox1.SelectedIndex];
                this.tabControl1.TabPages.Clear();
                this.m_CanDo = false;
                if (layer is ISimpleLineSymbol)
                {
                    this.cboLineType.SelectedIndex = 0;
                    TabPage tabPage3 = new TabPage("简单线符号");
                    SimpleLineControl simpleLineControl = new SimpleLineControl()
                    {
                        m_unit = this.point_unit_to[this.cboUnit.SelectedIndex],
                        m_pSimpleLineSymbol = (ISimpleLineSymbol) layer
                    };
                    simpleLineControl.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                    tabPage3.Controls.Add(simpleLineControl);
                    this.tabControl1.TabPages.Add(tabPage3);
                }
                else if (layer is IMarkerLineSymbol)
                {
                    this.cboLineType.SelectedIndex = 2;
                    TabPage tabPage4 = new TabPage("标记线");
                    MarkerLineControl markerLineControl = new MarkerLineControl()
                    {
                        m_pMarkerLineSymbol = (IMarkerLineSymbol) layer,
                        m_pSG = this.m_pSG
                    };
                    markerLineControl.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                    tabPage4.Controls.Add(markerLineControl);
                    tabPage = new TabPage("制图线");
                    cartoLineControl = new CartoLineControl()
                    {
                        m_unit = this.point_unit_to[this.cboUnit.SelectedIndex],
                        m_CartographLineSymbol = (ICartographicLineSymbol) layer
                    };
                    cartoLineControl.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                    tabPage.Controls.Add(cartoLineControl);
                    tabPage1 = new TabPage("模板");
                    templateControl = new TemplateControl()
                    {
                        m_unit = this.point_unit_to[this.cboUnit.SelectedIndex],
                        m_LineProperties = (ILineProperties) layer
                    };
                    templateControl.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                    tabPage1.Controls.Add(templateControl);
                    tabPage2 = new TabPage("线属性");
                    linePropertyControl = new LinePropertyControl()
                    {
                        m_LineProperties = (ILineProperties) layer,
                        m_unit = this.point_unit_to[this.cboUnit.SelectedIndex]
                    };
                    linePropertyControl.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                    tabPage2.Controls.Add(linePropertyControl);
                    this.tabControl1.TabPages.Add(tabPage4);
                    this.tabControl1.TabPages.Add(tabPage);
                    this.tabControl1.TabPages.Add(tabPage1);
                    this.tabControl1.TabPages.Add(tabPage2);
                }
                else if (layer is IHashLineSymbol)
                {
                    this.cboLineType.SelectedIndex = 3;
                    TabPage tabPage5 = new TabPage("细切线");
                    HashLineSymbolControl hashLineSymbolControl = new HashLineSymbolControl()
                    {
                        m_pHashLineSymbol = (IHashLineSymbol) layer,
                        m_pSG = this.m_pSG
                    };
                    hashLineSymbolControl.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                    tabPage5.Controls.Add(hashLineSymbolControl);
                    tabPage = new TabPage("制图线");
                    cartoLineControl = new CartoLineControl()
                    {
                        m_unit = this.point_unit_to[this.cboUnit.SelectedIndex],
                        m_CartographLineSymbol = (ICartographicLineSymbol) layer
                    };
                    cartoLineControl.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                    tabPage.Controls.Add(cartoLineControl);
                    tabPage1 = new TabPage("模板");
                    templateControl = new TemplateControl()
                    {
                        m_LineProperties = (ILineProperties) layer,
                        m_unit = this.point_unit_to[this.cboUnit.SelectedIndex]
                    };
                    templateControl.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                    tabPage1.Controls.Add(templateControl);
                    tabPage2 = new TabPage("线属性");
                    linePropertyControl = new LinePropertyControl()
                    {
                        m_LineProperties = (ILineProperties) layer,
                        m_unit = this.point_unit_to[this.cboUnit.SelectedIndex]
                    };
                    linePropertyControl.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                    tabPage2.Controls.Add(linePropertyControl);
                    this.tabControl1.TabPages.Add(tabPage5);
                    this.tabControl1.TabPages.Add(tabPage);
                    this.tabControl1.TabPages.Add(tabPage1);
                    this.tabControl1.TabPages.Add(tabPage2);
                }
                else if (layer is ICartographicLineSymbol)
                {
                    this.cboLineType.SelectedIndex = 1;
                    tabPage = new TabPage("制图线");
                    cartoLineControl = new CartoLineControl()
                    {
                        m_unit = this.point_unit_to[this.cboUnit.SelectedIndex],
                        m_CartographLineSymbol = (ICartographicLineSymbol) layer
                    };
                    cartoLineControl.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                    tabPage.Controls.Add(cartoLineControl);
                    tabPage1 = new TabPage("模板");
                    templateControl = new TemplateControl()
                    {
                        m_LineProperties = (ILineProperties) layer,
                        m_unit = this.point_unit_to[this.cboUnit.SelectedIndex]
                    };
                    templateControl.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                    tabPage1.Controls.Add(templateControl);
                    tabPage2 = new TabPage("线属性");
                    linePropertyControl = new LinePropertyControl()
                    {
                        m_LineProperties = (ILineProperties) layer,
                        m_unit = this.point_unit_to[this.cboUnit.SelectedIndex]
                    };
                    linePropertyControl.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                    tabPage2.Controls.Add(linePropertyControl);
                    this.tabControl1.TabPages.Add(tabPage);
                    this.tabControl1.TabPages.Add(tabPage1);
                    this.tabControl1.TabPages.Add(tabPage2);
                }
                if (layer is IPictureLineSymbol)
                {
                    this.cboLineType.SelectedIndex = 4;
                    TabPage tabPage6 = new TabPage("图片线");
                    PictureLineControl pictureLineControl = new PictureLineControl()
                    {
                        m_unit = this.point_unit_to[this.cboUnit.SelectedIndex],
                        m_PictureLineSymbol = (IPictureLineSymbol) layer
                    };
                    pictureLineControl.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                    tabPage6.Controls.Add(pictureLineControl);
                    this.tabControl1.TabPages.Add(tabPage6);
                }
                this.m_oldSelType = this.cboLineType.SelectedIndex;
                this.m_CanDo = true;
            }
        }

        private void tnMoveDown_Click(object sender, EventArgs e)
        {
            if (this.symbolListBox1.SelectedIndex != this.symbolListBox1.Items.Count - 1)
            {
                this.m_OldSelItem = this.symbolListBox1.SelectedIndex + 1;
                ILineSymbol layer = this.m_pMultiLineSymbol.Layer[this.symbolListBox1.SelectedIndex];
                this.m_pMultiLineSymbol.MoveLayer(layer, this.m_OldSelItem);
                this.m_CanDo = false;
                this.InitControl((ISymbol) this.m_pMultiLineSymbol);
                this.m_CanDo = true;
                this.symbolListBox1.Invalidate();
                this.symbolItem1.Invalidate();
            }
        }

        private void ValueChanged(object sender, EventArgs e)
        {
            this.symbolItem1.Invalidate();
            this.symbolListBox1.Invalidate();
        }
    }
}