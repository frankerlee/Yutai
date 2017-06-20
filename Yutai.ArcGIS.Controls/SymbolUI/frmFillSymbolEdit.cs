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
    public class frmFillSymbolEdit : Form
    {
        private SimpleButton btnAddLayer;
        private SimpleButton btnCancel;
        private SimpleButton btnCopy;
        private SimpleButton btnDeleteLayer;
        private SimpleButton btnFixZoomIn;
        private SimpleButton btnFixZoomOut;
        private SimpleButton btnMoveUp;
        private SimpleButton btnOK;
        private SimpleButton btnOnt2One;
        private SimpleButton btnPaste;
        private ComboBoxEdit cboFillType;
        private ComboBoxEdit cboScale;
        private ComboBoxEdit cboUnit;
        private Container components = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Label label1;
        private Label label2;
        private bool m_CanDo = true;
        private IFillSymbol m_CopySymbol = null;
        private double[] m_dblScaleRatio = new double[] { 0.25, 0.5, 1.0, 1.25, 2.0 };
        private int m_oldSel = -1;
        private int m_OldSelItem = 0;
        private int m_oldSelType = -1;
        private IMultiLayerFillSymbol m_pMultiFillSymbol;
        private ISymbol m_pOldSymbol;
        public IStyleGallery m_pSG;
        private int m_ScaleIndex = 2;
        private double[] point_unit_to = new double[] { 1.0, 0.01388889, 0.0352777778, 0.352777778 };
        private SymbolItem symbolItem1;
        private SymbolListBox symbolListBox1;
        private XtraTabControl tabControl1;
        private TabControl tabControl11;
        private SimpleButton tnMoveDown;

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

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
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

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFillSymbolEdit));
            this.groupBox1 = new GroupBox();
            this.tabControl1 = new XtraTabControl();
            this.cboUnit = new ComboBoxEdit();
            this.cboFillType = new ComboBoxEdit();
            this.tabControl11 = new TabControl();
            this.label2 = new Label();
            this.label1 = new Label();
            this.groupBox2 = new GroupBox();
            this.cboScale = new ComboBoxEdit();
            this.btnOnt2One = new SimpleButton();
            this.btnFixZoomOut = new SimpleButton();
            this.btnFixZoomIn = new SimpleButton();
            this.symbolItem1 = new SymbolItem();
            this.groupBox3 = new GroupBox();
            this.btnPaste = new SimpleButton();
            this.btnCopy = new SimpleButton();
            this.tnMoveDown = new SimpleButton();
            this.btnMoveUp = new SimpleButton();
            this.btnDeleteLayer = new SimpleButton();
            this.btnAddLayer = new SimpleButton();
            this.symbolListBox1 = new SymbolListBox();
            this.btnCancel = new SimpleButton();
            this.btnOK = new SimpleButton();
            this.groupBox1.SuspendLayout();
            this.tabControl1.BeginInit();
            this.cboUnit.Properties.BeginInit();
            this.cboFillType.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.cboScale.Properties.BeginInit();
            this.groupBox3.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.tabControl1);
            this.groupBox1.Controls.Add(this.cboUnit);
            this.groupBox1.Controls.Add(this.cboFillType);
            this.groupBox1.Controls.Add(this.tabControl11);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(200, 0x10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x1c0, 0x188);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "属性";
            this.tabControl1.Location = new Point(0x10, 0x40);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Size = new Size(0x1a8, 0x138);
            this.tabControl1.TabIndex = 14;
            this.tabControl1.Text = "xtraTabControl1";
            this.cboUnit.EditValue = "";
            this.cboUnit.Location = new Point(0x110, 0x18);
            this.cboUnit.Name = "cboUnit";
            this.cboUnit.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboUnit.Properties.Items.AddRange(new object[] { "点", "英寸", "厘米", "毫米" });
            this.cboUnit.Size = new Size(0x98, 0x15);
            this.cboUnit.TabIndex = 10;
            this.cboUnit.SelectedIndexChanged += new EventHandler(this.cboUnit_SelectedIndexChanged);
            this.cboFillType.EditValue = "";
            this.cboFillType.Location = new Point(0x38, 0x18);
            this.cboFillType.Name = "cboFillType";
            this.cboFillType.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboFillType.Properties.Items.AddRange(new object[] { "简单填充符号", "标记填充符号", "线填充符号", "渐变填充符号", "图片填充符号", "3D纹理填充" });
            this.cboFillType.Size = new Size(0x98, 0x15);
            this.cboFillType.TabIndex = 9;
            this.cboFillType.SelectedIndexChanged += new EventHandler(this.cboFillType_SelectedIndexChanged);
            this.tabControl11.Location = new Point(0x1a0, 0x60);
            this.tabControl11.Name = "tabControl11";
            this.tabControl11.SelectedIndex = 0;
            this.tabControl11.Size = new Size(0x10, 0x98);
            this.tabControl11.TabIndex = 6;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0xe8, 0x1c);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "单位";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 0x1c);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "类型";
            this.groupBox2.Controls.Add(this.cboScale);
            this.groupBox2.Controls.Add(this.btnOnt2One);
            this.groupBox2.Controls.Add(this.btnFixZoomOut);
            this.groupBox2.Controls.Add(this.btnFixZoomIn);
            this.groupBox2.Controls.Add(this.symbolItem1);
            this.groupBox2.Location = new Point(8, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0xb0, 160);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "预览";
            this.cboScale.EditValue = "";
            this.cboScale.Location = new Point(0x60, 0x80);
            this.cboScale.Name = "cboScale";
            this.cboScale.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboScale.Properties.Items.AddRange(new object[] { "400%", "200%", "100%", "75%", "50%" });
            this.cboScale.Size = new Size(0x48, 0x15);
            this.cboScale.TabIndex = 0x18;
            this.cboScale.SelectedIndexChanged += new EventHandler(this.cboScale_SelectedIndexChanged);
            this.btnOnt2One.Image = (Image) resources.GetObject("btnOnt2One.Image");
            this.btnOnt2One.Location = new Point(0x40, 0x80);
            this.btnOnt2One.Name = "btnOnt2One";
            this.btnOnt2One.Size = new Size(0x16, 0x16);
            this.btnOnt2One.TabIndex = 0x17;
            this.btnOnt2One.Click += new EventHandler(this.btnOnt2One_Click);
            this.btnFixZoomOut.Image = (Image) resources.GetObject("btnFixZoomOut.Image");
            this.btnFixZoomOut.Location = new Point(0x26, 0x80);
            this.btnFixZoomOut.Name = "btnFixZoomOut";
            this.btnFixZoomOut.Size = new Size(0x16, 0x16);
            this.btnFixZoomOut.TabIndex = 0x16;
            this.btnFixZoomOut.Click += new EventHandler(this.btnFixZoomOut_Click);
            this.btnFixZoomIn.Image = (Image) resources.GetObject("btnFixZoomIn.Image");
            this.btnFixZoomIn.Location = new Point(8, 0x80);
            this.btnFixZoomIn.Name = "btnFixZoomIn";
            this.btnFixZoomIn.Size = new Size(0x16, 0x16);
            this.btnFixZoomIn.TabIndex = 0x15;
            this.btnFixZoomIn.Click += new EventHandler(this.btnFixZoomIn_Click);
            this.symbolItem1.BackColor = SystemColors.ControlLight;
            this.symbolItem1.Location = new Point(0x18, 0x18);
            this.symbolItem1.Name = "symbolItem1";
            this.symbolItem1.Size = new Size(0x88, 0x60);
            this.symbolItem1.Symbol = null;
            this.symbolItem1.TabIndex = 1;
            this.groupBox3.Controls.Add(this.btnPaste);
            this.groupBox3.Controls.Add(this.btnCopy);
            this.groupBox3.Controls.Add(this.tnMoveDown);
            this.groupBox3.Controls.Add(this.btnMoveUp);
            this.groupBox3.Controls.Add(this.btnDeleteLayer);
            this.groupBox3.Controls.Add(this.btnAddLayer);
            this.groupBox3.Controls.Add(this.symbolListBox1);
            this.groupBox3.Location = new Point(0x10, 0xb0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(160, 0x108);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "图层";
            this.btnPaste.Image = (Image) resources.GetObject("btnPaste.Image");
            this.btnPaste.Location = new Point(40, 0xe8);
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new Size(0x18, 0x18);
            this.btnPaste.TabIndex = 0x11;
            this.btnPaste.Click += new EventHandler(this.btnPaste_Click);
            this.btnCopy.Image = (Image) resources.GetObject("btnCopy.Image");
            this.btnCopy.Location = new Point(8, 0xe8);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new Size(0x18, 0x18);
            this.btnCopy.TabIndex = 0x10;
            this.btnCopy.Click += new EventHandler(this.btnCopy_Click);
            this.tnMoveDown.Image = (Image) resources.GetObject("tnMoveDown.Image");
            this.tnMoveDown.Location = new Point(0x68, 200);
            this.tnMoveDown.Name = "tnMoveDown";
            this.tnMoveDown.Size = new Size(0x18, 0x18);
            this.tnMoveDown.TabIndex = 15;
            this.tnMoveDown.Click += new EventHandler(this.tnMoveDown_Click);
            this.btnMoveUp.Image = (Image) resources.GetObject("btnMoveUp.Image");
            this.btnMoveUp.Location = new Point(0x48, 200);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new Size(0x18, 0x18);
            this.btnMoveUp.TabIndex = 14;
            this.btnMoveUp.Click += new EventHandler(this.btnMoveUp_Click);
            this.btnDeleteLayer.Image = (Image) resources.GetObject("btnDeleteLayer.Image");
            this.btnDeleteLayer.Location = new Point(40, 200);
            this.btnDeleteLayer.Name = "btnDeleteLayer";
            this.btnDeleteLayer.Size = new Size(0x18, 0x18);
            this.btnDeleteLayer.TabIndex = 13;
            this.btnDeleteLayer.Click += new EventHandler(this.btnDeleteLayer_Click);
            this.btnAddLayer.Image = (Image) resources.GetObject("btnAddLayer.Image");
            this.btnAddLayer.Location = new Point(8, 200);
            this.btnAddLayer.Name = "btnAddLayer";
            this.btnAddLayer.Size = new Size(0x18, 0x18);
            this.btnAddLayer.TabIndex = 12;
            this.btnAddLayer.Click += new EventHandler(this.btnAddLayer_Click);
            this.symbolListBox1.DrawMode = DrawMode.OwnerDrawVariable;
            this.symbolListBox1.Location = new Point(8, 0x18);
            this.symbolListBox1.Name = "symbolListBox1";
            this.symbolListBox1.Size = new Size(0x90, 160);
            this.symbolListBox1.TabIndex = 5;
            this.symbolListBox1.LayerVisibleChanged += new LayerVisibleChangedHandler(this.symbolListBox1_LayerVisibleChanged);
            this.symbolListBox1.LayerColorLockedChanged += new LayerColorLockedChangedHandler(this.symbolListBox1_LayerColorLockedChanged);
            this.symbolListBox1.SelectedIndexChanged += new EventHandler(this.symbolListBox1_SelectedIndexChanged);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0x248, 0x1a0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x38, 0x18);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0x1f8, 0x1a0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x38, 0x18);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x292, 0x1bf);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmFillSymbolEdit";
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "面符号编辑器";
            base.Load += new EventHandler(this.frmFillSymbolEdit_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.EndInit();
            this.cboUnit.Properties.EndInit();
            this.cboFillType.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.cboScale.Properties.EndInit();
            this.groupBox3.ResumeLayout(false);
            base.ResumeLayout(false);
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

