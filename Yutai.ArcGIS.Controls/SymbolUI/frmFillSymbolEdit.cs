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
    [Guid("58FA9E10-739C-414a-A928-E42EBE20603C")]
    public partial class frmFillSymbolEdit : Form
    {
        private bool m_CanDo = true;
        private IFillSymbol m_CopySymbol = null;
        private double[] m_dblScaleRatio = new double[] { 0.25, 0.5, 1.0, 1.25, 2.0 };
        private int m_oldSel = -1;
        private int m_OldSelItem = 0;
        private int m_oldSelType = -1;
        public IStyleGallery m_pSG;
        private int m_ScaleIndex = 2;
        private double[] point_unit_to = new double[] { 1.0, 0.01388889, 0.0352777778, 0.352777778 };

        public frmFillSymbolEdit()
        {
            this.InitializeComponent();
        }

        private void btnAddLayer_Click(object sender, EventArgs e)
        {
            IFillSymbol fillLayer = this.CreateNewSymbol(this.cboFillType.SelectedIndex);
            this.m_pMultiFillSymbol.AddLayer(fillLayer);
            this.m_OldSelItem = 0;
            this.m_pMultiFillSymbol.MoveLayer(fillLayer, 0);
            ((ILayerColorLock) this.m_pMultiFillSymbol).set_LayerColorLock(0, false);
            this.InitControl((ISymbol) this.m_pMultiFillSymbol);
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
            IFillSymbol symbol = this.m_pMultiFillSymbol.get_Layer(this.symbolListBox1.SelectedIndex);
            this.m_CopySymbol = (IFillSymbol) ((IClone) symbol).Clone();
        }

        private void btnDeleteLayer_Click(object sender, EventArgs e)
        {
            if (this.m_pMultiFillSymbol.LayerCount != 1)
            {
                IFillSymbol fillLayer = this.m_pMultiFillSymbol.get_Layer(this.symbolListBox1.SelectedIndex);
                this.m_OldSelItem = this.symbolListBox1.SelectedIndex - 1;
                if (this.m_OldSelItem == -1)
                {
                    this.m_OldSelItem = 0;
                }
                this.m_pMultiFillSymbol.DeleteLayer(fillLayer);
                this.InitControl((ISymbol) this.m_pMultiFillSymbol);
                this.symbolListBox1.Invalidate();
                this.symbolItem1.Invalidate();
            }
        }

        private void btnFixZoomIn_Click(object sender, EventArgs e)
        {
            if (this.m_ScaleIndex != 0)
            {
                this.m_ScaleIndex--;
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
                this.m_ScaleIndex++;
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
                IFillSymbol fillLayer = this.m_pMultiFillSymbol.get_Layer(this.symbolListBox1.SelectedIndex);
                this.m_pMultiFillSymbol.MoveLayer(fillLayer, this.m_OldSelItem);
                this.m_CanDo = false;
                this.InitControl((ISymbol) this.m_pMultiFillSymbol);
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
                IFillSymbol fillLayer = (IFillSymbol) ((IClone) this.m_CopySymbol).Clone();
                this.m_pMultiFillSymbol.AddLayer(fillLayer);
                this.m_OldSelItem = 0;
                this.m_pMultiFillSymbol.MoveLayer(fillLayer, 0);
                this.InitControl((ISymbol) this.m_pMultiFillSymbol);
                this.symbolListBox1.Invalidate();
                this.symbolItem1.Invalidate();
            }
        }

        private void cboFillType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo && (this.m_oldSelType != this.cboFillType.SelectedIndex))
            {
                this.m_oldSelType = this.cboFillType.SelectedIndex;
                IFillSymbol fillLayer = this.CreateNewSymbol(this.cboFillType.SelectedIndex);
                IFillSymbol symbol2 = this.m_pMultiFillSymbol.get_Layer(this.symbolListBox1.SelectedIndex);
                this.m_OldSelItem = this.symbolListBox1.SelectedIndex;
                this.m_pMultiFillSymbol.AddLayer(fillLayer);
                this.m_pMultiFillSymbol.MoveLayer(fillLayer, this.symbolListBox1.SelectedIndex);
                ((ILayerColorLock) this.m_pMultiFillSymbol).set_LayerColorLock(this.symbolListBox1.SelectedIndex, false);
                this.m_pMultiFillSymbol.DeleteLayer(symbol2);
                this.InitControl((ISymbol) this.m_pMultiFillSymbol);
                this.symbolListBox1.Invalidate();
                this.symbolItem1.Invalidate();
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
                for (int i = 0; i < this.tabControl1.TabPages.Count; i++)
                {
                    CommonInterface interface2 = this.tabControl1.TabPages[i].Controls[0] as CommonInterface;
                    if (interface2 != null)
                    {
                        interface2.ChangeUnit(this.point_unit_to[this.cboUnit.SelectedIndex]);
                    }
                }
            }
        }

        private IFillSymbol CreateNewSymbol(int type)
        {
            switch (type)
            {
                case 0:
                    return new SimpleFillSymbolClass();

                case 1:
                    return new MarkerFillSymbolClass();

                case 2:
                    return new LineFillSymbolClass();

                case 3:
                    return new GradientFillSymbolClass();

                case 4:
                    return new PictureFillSymbolClass();

                case 5:
                    return new TextureFillSymbolClass();
            }
            return null;
        }

 private void frmFillSymbolEdit_Load(object sender, EventArgs e)
        {
            this.m_CanDo = false;
            this.cboUnit.SelectedIndex = 0;
            this.cboScale.SelectedIndex = this.m_ScaleIndex;
            this.m_CanDo = true;
            this.InitControl((ISymbol) this.m_pMultiFillSymbol);
        }

        public ISymbol GetSymbol()
        {
            if (this.m_pOldSymbol is IMultiLayerFillSymbol)
            {
                return (ISymbol) this.m_pMultiFillSymbol;
            }
            return (ISymbol) this.m_pMultiFillSymbol;
        }

        private void InitControl(ISymbol pSym)
        {
            this.symbolItem1.Symbol = pSym;
            this.symbolListBox1.Items.Clear();
            this.m_oldSel = -1;
            if (pSym is IMultiLayerFillSymbol)
            {
                for (int i = 0; i < ((IMultiLayerFillSymbol) pSym).LayerCount; i++)
                {
                    SymbolListItem item = new SymbolListItem {
                        m_pSymbol = (ISymbol) ((IMultiLayerFillSymbol) pSym).get_Layer(i),
                        m_bVisible = ((ILayerVisible) pSym).get_LayerVisible(i),
                        m_bLockColor = ((ILayerColorLock) pSym).get_LayerColorLock(i)
                    };
                    this.symbolListBox1.Items.Add(item);
                }
                if (this.symbolListBox1.Items.Count > 0)
                {
                    this.symbolListBox1.SelectedIndex = this.m_OldSelItem;
                }
            }
        }

 public void SetSymbol(ISymbol pSym)
        {
            this.m_pOldSymbol = pSym;
            if (pSym is IMultiLayerFillSymbol)
            {
                this.m_pMultiFillSymbol = (IMultiLayerFillSymbol) ((IClone) pSym).Clone();
            }
            else if (pSym is IFillSymbol)
            {
                this.m_pMultiFillSymbol = new MultiLayerFillSymbolClass();
                this.m_pMultiFillSymbol.AddLayer((IFillSymbol) ((IClone) pSym).Clone());
                (this.m_pMultiFillSymbol as ILayerColorLock).set_LayerColorLock(0, false);
            }
        }

        private void symbolListBox1_LayerColorLockedChanged(object sender, EventArgs e)
        {
            bool flag = ((ILayerColorLock) this.m_pMultiFillSymbol).get_LayerColorLock(this.symbolListBox1.SelectedIndex);
            ((ILayerColorLock) this.m_pMultiFillSymbol).set_LayerColorLock(this.symbolListBox1.SelectedIndex, !flag);
        }

        private void symbolListBox1_LayerVisibleChanged(object sender, EventArgs e)
        {
            bool flag = ((ILayerVisible) this.m_pMultiFillSymbol).get_LayerVisible(this.symbolListBox1.SelectedIndex);
            ((ILayerVisible) this.m_pMultiFillSymbol).set_LayerVisible(this.symbolListBox1.SelectedIndex, !flag);
            this.symbolItem1.Invalidate();
        }

        private void symbolListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_oldSel != this.symbolListBox1.SelectedIndex)
            {
                this.m_oldSel = this.symbolListBox1.SelectedIndex;
                IFillSymbol symbol = this.m_pMultiFillSymbol.get_Layer(this.symbolListBox1.SelectedIndex);
                this.tabControl1.TabPages.Clear();
                this.m_CanDo = false;
                if (symbol is ISimpleFillSymbol)
                {
                    this.cboFillType.SelectedIndex = 0;
                    XtraTabPage page = new XtraTabPage {
                        Text = "简单面"
                    };
                    SimpleFillControl control = new SimpleFillControl {
                        m_unit = this.point_unit_to[this.cboUnit.SelectedIndex],
                        m_pSG = this.m_pSG
                    };
                    control.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                    control.m_SimpleFillSymbol = (ISimpleFillSymbol) symbol;
                    page.Controls.Add(control);
                    this.tabControl1.TabPages.Add(page);
                }
                else
                {
                    XtraTabPage page3;
                    FillPropertyControl control3;
                    if (symbol is IMarkerFillSymbol)
                    {
                        this.cboFillType.SelectedIndex = 1;
                        XtraTabPage page2 = new XtraTabPage {
                            Text = "标记填充"
                        };
                        MarkFillControl control2 = new MarkFillControl {
                            m_MarkerFillSymbol = (IMarkerFillSymbol) symbol,
                            m_pSG = this.m_pSG
                        };
                        control2.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                        page2.Controls.Add(control2);
                        page3 = new XtraTabPage {
                            Text = "填充属性"
                        };
                        control3 = new FillPropertyControl {
                            m_unit = this.point_unit_to[this.cboUnit.SelectedIndex],
                            m_FillProperties = (IFillProperties) symbol
                        };
                        control3.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                        page3.Controls.Add(control3);
                        this.tabControl1.TabPages.Add(page2);
                        this.tabControl1.TabPages.Add(page3);
                    }
                    else if (symbol is ILineFillSymbol)
                    {
                        this.cboFillType.SelectedIndex = 2;
                        XtraTabPage page4 = new XtraTabPage {
                            Text = "线填充属性"
                        };
                        LineFillControl control4 = new LineFillControl {
                            m_unit = this.point_unit_to[this.cboUnit.SelectedIndex],
                            m_LineFillSymbol = (ILineFillSymbol) symbol,
                            m_pSG = this.m_pSG
                        };
                        control4.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                        page4.Controls.Add(control4);
                        this.tabControl1.TabPages.Add(page4);
                    }
                    else if (symbol is IGradientFillSymbol)
                    {
                        this.cboFillType.SelectedIndex = 3;
                        XtraTabPage page5 = new XtraTabPage {
                            Text = "渐变填充"
                        };
                        GradientFillControl control5 = new GradientFillControl {
                            m_GradientFillSymbol = (IGradientFillSymbol) symbol
                        };
                        control5.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                        control5.m_pSG = this.m_pSG;
                        page5.Controls.Add(control5);
                        this.tabControl1.TabPages.Add(page5);
                    }
                    else
                    {
                        XtraTabPage page6;
                        if (symbol is IPictureFillSymbol)
                        {
                            this.cboFillType.SelectedIndex = 4;
                            page6 = new XtraTabPage {
                                Text = "图片填充"
                            };
                            PictureFillControl control6 = new PictureFillControl {
                                m_PictureFillSymbol = (IPictureFillSymbol) symbol,
                                m_pSG = this.m_pSG
                            };
                            control6.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                            page6.Controls.Add(control6);
                            page3 = new XtraTabPage {
                                Text = "填充属性"
                            };
                            control3 = new FillPropertyControl {
                                m_unit = this.point_unit_to[this.cboUnit.SelectedIndex],
                                m_FillProperties = (IFillProperties) symbol
                            };
                            control3.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                            page3.Controls.Add(control3);
                            this.tabControl1.TabPages.Add(page6);
                            this.tabControl1.TabPages.Add(page3);
                        }
                        else if (symbol is ITextureFillSymbol)
                        {
                            this.cboFillType.SelectedIndex = 5;
                            page6 = new XtraTabPage {
                                Text = "3D纹理填充"
                            };
                            TextureFillSymbolCtrl ctrl = new TextureFillSymbolCtrl {
                                m_pTextureFillSymbol = (ITextureFillSymbol) symbol,
                                m_pSG = this.m_pSG
                            };
                            ctrl.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                            page6.Controls.Add(ctrl);
                            this.tabControl1.TabPages.Add(page6);
                        }
                    }
                }
                this.m_oldSelType = this.cboFillType.SelectedIndex;
                this.m_CanDo = true;
            }
        }

        private void tnMoveDown_Click(object sender, EventArgs e)
        {
            if (this.symbolListBox1.SelectedIndex != (this.symbolListBox1.Items.Count - 1))
            {
                this.m_OldSelItem = this.symbolListBox1.SelectedIndex + 1;
                IFillSymbol fillLayer = this.m_pMultiFillSymbol.get_Layer(this.symbolListBox1.SelectedIndex);
                this.m_pMultiFillSymbol.MoveLayer(fillLayer, this.m_OldSelItem);
                this.m_CanDo = false;
                this.InitControl((ISymbol) this.m_pMultiFillSymbol);
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

