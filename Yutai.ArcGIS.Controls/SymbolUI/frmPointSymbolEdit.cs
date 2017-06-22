using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraBars;
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
    [Guid("5BFD54A5-AC94-4bd5-8533-8E99F3C7C002")]
    public partial class frmPointSymbolEdit : Form
    {
        private CharacterMarkerControl charactermarkcontrol = new CharacterMarkerControl();
        private bool m_CanDo = true;
        private IMarkerSymbol m_CopySymbol = null;
        private double[] m_dblScaleRatio = new double[] { 0.25, 0.5, 1.0, 1.25, 2.0 };
        private MaskControl m_Maskcontrol = new MaskControl();
        private int m_oldSel = -1;
        private int m_OldSelItem = 0;
        private int m_oldSelType = -1;
        public IStyleGallery m_pSG;
        private int m_ScaleIndex = 2;
        private double[] point_unit_to = new double[] { 1.0, 0.01388889, 0.0352777778, 0.352777778 };


      
        public frmPointSymbolEdit()
        {
            this.InitializeComponent();
        }

        private void btnAddLayer_Click(object sender, EventArgs e)
        {
            if (this.cboMarkerType.SelectedIndex != -1)
            {
                IMarkerSymbol markerLayer = this.CreateNewSymbol(this.cboMarkerType.SelectedIndex);
                this.m_pMultiMarkerSymbol.AddLayer(markerLayer);
                this.m_OldSelItem = 0;
                this.m_pMultiMarkerSymbol.MoveLayer(markerLayer, 0);
                ((ILayerColorLock) this.m_pMultiMarkerSymbol).set_LayerColorLock(0, false);
                this.InitControl((ISymbol) this.m_pMultiMarkerSymbol);
                this.symbolListBox1.Invalidate();
                this.symbolItem1.Invalidate();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            IMarkerSymbol symbol = this.m_pMultiMarkerSymbol.get_Layer(this.symbolListBox1.SelectedIndex);
            this.m_CopySymbol = (IMarkerSymbol) ((IClone) symbol).Clone();
        }

        private void btnDeleteLayer_Click(object sender, EventArgs e)
        {
            if (this.m_pMultiMarkerSymbol.LayerCount != 1)
            {
                IMarkerSymbol markerLayer = this.m_pMultiMarkerSymbol.get_Layer(this.symbolListBox1.SelectedIndex);
                this.m_OldSelItem = this.symbolListBox1.SelectedIndex - 1;
                if (this.m_OldSelItem == -1)
                {
                    this.m_OldSelItem = 0;
                }
                this.m_pMultiMarkerSymbol.DeleteLayer(markerLayer);
                this.InitControl((ISymbol) this.m_pMultiMarkerSymbol);
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
                IMarkerSymbol markerLayer = this.m_pMultiMarkerSymbol.get_Layer(this.symbolListBox1.SelectedIndex);
                this.m_pMultiMarkerSymbol.MoveLayer(markerLayer, this.m_OldSelItem);
                this.m_CanDo = false;
                this.InitControl((ISymbol) this.m_pMultiMarkerSymbol);
                this.m_CanDo = true;
                this.symbolListBox1.Invalidate();
                this.symbolItem1.Invalidate();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
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
                IMarkerSymbol markerLayer = (IMarkerSymbol) ((IClone) this.m_CopySymbol).Clone();
                this.m_pMultiMarkerSymbol.AddLayer(markerLayer);
                this.m_OldSelItem = 0;
                this.m_pMultiMarkerSymbol.MoveLayer(markerLayer, 0);
                this.InitControl((ISymbol) this.m_pMultiMarkerSymbol);
                this.symbolListBox1.Invalidate();
                this.symbolItem1.Invalidate();
            }
        }

        private void cboMarkerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo && (this.m_oldSelType != this.cboMarkerType.SelectedIndex))
            {
                this.m_oldSelType = this.cboMarkerType.SelectedIndex;
                IMarkerSymbol markerLayer = this.CreateNewSymbol(this.cboMarkerType.SelectedIndex);
                IMarkerSymbol symbol2 = this.m_pMultiMarkerSymbol.get_Layer(this.symbolListBox1.SelectedIndex);
                this.m_OldSelItem = this.symbolListBox1.SelectedIndex;
                this.m_pMultiMarkerSymbol.AddLayer(markerLayer);
                this.m_pMultiMarkerSymbol.MoveLayer(markerLayer, this.symbolListBox1.SelectedIndex);
                ((ILayerColorLock) this.m_pMultiMarkerSymbol).set_LayerColorLock(this.symbolListBox1.SelectedIndex, false);
                this.m_pMultiMarkerSymbol.DeleteLayer(symbol2);
                this.InitControl((ISymbol) this.m_pMultiMarkerSymbol);
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

        private void chkLine_CheckedChanged(object sender, EventArgs e)
        {
            this.symbolItem1.HasDrawLine = this.chkLine.Checked;
            this.symbolItem1.Invalidate();
        }

        private IMarkerSymbol CreateNewSymbol(int type)
        {
            switch (type)
            {
                case 0:
                    return new SimpleMarkerSymbolClass();

                case 1:
                    return new ArrowMarkerSymbolClass();

                case 2:
                    return new PictureMarkerSymbolClass();

                case 3:
                    return new CharacterMarkerSymbolClass();

                case 4:
                    return new CharacterMarker3DSymbolClass();

                case 5:
                    return new SimpleMarker3DSymbolClass();

                case 6:
                    return new Marker3DSymbolClass();
            }
            return null;
        }

 private void frmPointSymbolEdit_Load(object sender, EventArgs e)
        {
            this.m_CanDo = false;
            this.cboUnit.SelectedIndex = 0;
            this.cboScale.SelectedIndex = this.m_ScaleIndex;
            this.m_CanDo = true;
            this.m_Maskcontrol.m_pMask = (IMask) this.m_pMultiMarkerSymbol;
            this.m_Maskcontrol.m_unit = this.point_unit_to[this.cboUnit.SelectedIndex];
            this.m_Maskcontrol.m_pSG = this.m_pSG;
            this.charactermarkcontrol.ValueChanged += new ValueChangedHandler(this.ValueChanged);
            this.m_Maskcontrol.ValueChanged += new ValueChangedHandler(this.ValueChanged);
            this.InitControl((ISymbol) this.m_pMultiMarkerSymbol);
        }

        public ISymbol GetSymbol()
        {
            if (this.m_pOldSymbol is IMultiLayerMarkerSymbol)
            {
                return (ISymbol) this.m_pMultiMarkerSymbol;
            }
            return (ISymbol) this.m_pMultiMarkerSymbol;
        }

        private void InitControl(ISymbol pSym)
        {
            this.symbolItem1.Symbol = pSym;
            if (pSym is IMultiLayerMarkerSymbol)
            {
                this.symbolListBox1.Items.Clear();
                this.m_oldSel = -1;
                for (int i = 0; i < ((IMultiLayerMarkerSymbol) pSym).LayerCount; i++)
                {
                    SymbolListItem item = new SymbolListItem {
                        m_pSymbol = (ISymbol) ((IMultiLayerMarkerSymbol) pSym).get_Layer(i),
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
            if (pSym is IMultiLayerMarkerSymbol)
            {
                this.m_pMultiMarkerSymbol = (IMultiLayerMarkerSymbol) ((IClone) pSym).Clone();
            }
            else if (pSym is IMarkerSymbol)
            {
                this.m_pMultiMarkerSymbol = new MultiLayerMarkerSymbolClass();
                this.m_pMultiMarkerSymbol.AddLayer((IMarkerSymbol) ((IClone) pSym).Clone());
                (this.m_pMultiMarkerSymbol as ILayerColorLock).set_LayerColorLock(0, false);
            }
        }

        private void symbolListBox1_LayerColorLockedChanged(object sender, EventArgs e)
        {
            bool flag = ((ILayerColorLock) this.m_pMultiMarkerSymbol).get_LayerColorLock(this.symbolListBox1.SelectedIndex);
            ((ILayerColorLock) this.m_pMultiMarkerSymbol).set_LayerColorLock(this.symbolListBox1.SelectedIndex, !flag);
        }

        private void symbolListBox1_LayerVisibleChanged(object sender, EventArgs e)
        {
            bool flag = ((ILayerVisible) this.m_pMultiMarkerSymbol).get_LayerVisible(this.symbolListBox1.SelectedIndex);
            ((ILayerVisible) this.m_pMultiMarkerSymbol).set_LayerVisible(this.symbolListBox1.SelectedIndex, !flag);
            this.symbolItem1.Invalidate();
        }

        private void symbolListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_oldSel != this.symbolListBox1.SelectedIndex)
            {
                XtraTabPage page;
                XtraTabPage page2;
                this.m_oldSel = this.symbolListBox1.SelectedIndex;
                IMarkerSymbol symbol = this.m_pMultiMarkerSymbol.get_Layer(this.symbolListBox1.SelectedIndex);
                this.tabControl1.TabPages.Clear();
                this.m_CanDo = false;
                if (symbol is ISymbol3D)
                {
                    if (symbol is ICharacterMarker3DSymbol)
                    {
                        this.cboMarkerType.SelectedIndex = 4;
                        page = new XtraTabPage {
                            Text = "3D字符点属性"
                        };
                        CharacterMarker3DSymbolCtrl ctrl = new CharacterMarker3DSymbolCtrl();
                        ctrl.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                        ctrl.m_unit = this.point_unit_to[this.cboUnit.SelectedIndex];
                        ctrl.m_pCharacterMarker3DSymbol = (ICharacterMarker3DSymbol) symbol;
                        page.Controls.Add(ctrl);
                        this.tabControl1.TabPages.Add(page);
                        ctrl.InitControl();
                    }
                    else if (symbol is ISimpleMarker3DSymbol)
                    {
                        this.cboMarkerType.SelectedIndex = 5;
                        page = new XtraTabPage {
                            Text = "3D简单点属性"
                        };
                        SimpleMarker3DSymbolCtrl ctrl2 = new SimpleMarker3DSymbolCtrl();
                        ctrl2.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                        ctrl2.m_unit = this.point_unit_to[this.cboUnit.SelectedIndex];
                        ctrl2.m_pSimpleMarker3DSymbol = (ISimpleMarker3DSymbol) symbol;
                        page.Controls.Add(ctrl2);
                        this.tabControl1.TabPages.Add(page);
                        ctrl2.InitControl();
                    }
                    else if (symbol is IMarker3DSymbol)
                    {
                        this.cboMarkerType.SelectedIndex = 6;
                        page = new XtraTabPage {
                            Text = "3D点属性"
                        };
                        Marker3DSymbolCtrl ctrl3 = new Marker3DSymbolCtrl();
                        ctrl3.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                        ctrl3.m_unit = this.point_unit_to[this.cboUnit.SelectedIndex];
                        ctrl3.m_pMarker3DSymbol = (IMarker3DSymbol) symbol;
                        page.Controls.Add(ctrl3);
                        this.tabControl1.TabPages.Add(page);
                        ctrl3.InitControl();
                    }
                    Marker3DPlacementCtrl ctrl4 = new Marker3DPlacementCtrl();
                    page2 = new XtraTabPage {
                        Text = "3D放置"
                    };
                    ctrl4.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                    ctrl4.m_unit = this.point_unit_to[this.cboUnit.SelectedIndex];
                    ctrl4.m_pMarker3DSymbol = (IMarker3DPlacement) symbol;
                    page2.Controls.Add(ctrl4);
                    this.tabControl1.TabPages.Add(page2);
                    ctrl4.InitControl();
                }
                else
                {
                    if (symbol is ICharacterMarkerSymbol)
                    {
                        this.cboMarkerType.SelectedIndex = 3;
                        page = new XtraTabPage {
                            Text = "字符点属性"
                        };
                        CharacterMarkerControl charactermarkcontrol = this.charactermarkcontrol;
                        charactermarkcontrol.m_unit = this.point_unit_to[this.cboUnit.SelectedIndex];
                        charactermarkcontrol.m_CharacterMarkerSymbol = (ICharacterMarkerSymbol) symbol;
                        page.Controls.Add(charactermarkcontrol);
                        this.tabControl1.TabPages.Add(page);
                        charactermarkcontrol.InitControl();
                    }
                    else if (symbol is IArrowMarkerSymbol)
                    {
                        this.cboMarkerType.SelectedIndex = 1;
                        XtraTabPage page3 = new XtraTabPage {
                            Text = "箭头点属性"
                        };
                        ArrowMarkerControl control2 = new ArrowMarkerControl {
                            m_unit = this.point_unit_to[this.cboUnit.SelectedIndex],
                            m_ArrowMarkerSymbol = (IArrowMarkerSymbol) symbol
                        };
                        control2.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                        page3.Controls.Add(control2);
                        this.tabControl1.TabPages.Add(page3);
                    }
                    else if (symbol is IPictureMarkerSymbol)
                    {
                        this.cboMarkerType.SelectedIndex = 2;
                        XtraTabPage page4 = new XtraTabPage {
                            Text = "图片点属性"
                        };
                        PictureMarkerControl control3 = new PictureMarkerControl {
                            m_unit = this.point_unit_to[this.cboUnit.SelectedIndex],
                            m_PictureMarkerSymbol = (IPictureMarkerSymbol) symbol
                        };
                        control3.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                        page4.Controls.Add(control3);
                        this.tabControl1.TabPages.Add(page4);
                    }
                    else if (symbol is ISimpleMarkerSymbol)
                    {
                        this.cboMarkerType.SelectedIndex = 0;
                        XtraTabPage page5 = new XtraTabPage {
                            Text = "简单点属性"
                        };
                        SimpleMarkerControl control4 = new SimpleMarkerControl {
                            m_unit = this.point_unit_to[this.cboUnit.SelectedIndex],
                            m_SimpleMarkerSymbol = (ISimpleMarkerSymbol) symbol
                        };
                        control4.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                        page5.Controls.Add(control4);
                        this.tabControl1.TabPages.Add(page5);
                    }
                    page2 = new XtraTabPage {
                        Text = "掩模"
                    };
                    page2.Controls.Add(this.m_Maskcontrol);
                    this.tabControl1.TabPages.Add(page2);
                    this.m_Maskcontrol.InitControl();
                }
                this.m_oldSelType = this.cboMarkerType.SelectedIndex;
                this.m_CanDo = true;
            }
        }

        private void tnMoveDown_Click(object sender, EventArgs e)
        {
            if (this.symbolListBox1.SelectedIndex != (this.symbolListBox1.Items.Count - 1))
            {
                this.m_OldSelItem = this.symbolListBox1.SelectedIndex + 1;
                IMarkerSymbol markerLayer = this.m_pMultiMarkerSymbol.get_Layer(this.symbolListBox1.SelectedIndex);
                this.m_pMultiMarkerSymbol.MoveLayer(markerLayer, this.m_OldSelItem);
                this.m_CanDo = false;
                this.InitControl((ISymbol) this.m_pMultiMarkerSymbol);
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

