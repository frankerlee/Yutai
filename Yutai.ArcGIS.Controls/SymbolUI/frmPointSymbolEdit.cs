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
    public class frmPointSymbolEdit : Form
    {
        private BarAndDockingController barAndDockingController1;
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
        private ComboBoxEdit cboMarkerType;
        private ComboBoxEdit cboScale;
        private ComboBoxEdit cboUnit;
        private CharacterMarkerControl charactermarkcontrol = new CharacterMarkerControl();
        private CheckBox chkLine;
        private IContainer components;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Label label1;
        private Label label2;
        private bool m_CanDo = true;
        private IMarkerSymbol m_CopySymbol = null;
        private double[] m_dblScaleRatio = new double[] { 0.25, 0.5, 1.0, 1.25, 2.0 };
        private MaskControl m_Maskcontrol = new MaskControl();
        private int m_oldSel = -1;
        private int m_OldSelItem = 0;
        private int m_oldSelType = -1;
        private IMultiLayerMarkerSymbol m_pMultiMarkerSymbol;
        private ISymbol m_pOldSymbol;
        public IStyleGallery m_pSG;
        private int m_ScaleIndex = 2;
        private double[] point_unit_to = new double[] { 1.0, 0.01388889, 0.0352777778, 0.352777778 };
        private SymbolItem symbolItem1;
        private SymbolListBox symbolListBox1;
        private XtraTabControl tabControl1;
        private TabControl tabControl11;
        private SimpleButton tnMoveDown;


      
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

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
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

        private void InitializeComponent()
        {
            this.components = new Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPointSymbolEdit));
            this.groupBox1 = new GroupBox();
            this.tabControl1 = new XtraTabControl();
            this.cboUnit = new ComboBoxEdit();
            this.cboMarkerType = new ComboBoxEdit();
            this.tabControl11 = new TabControl();
            this.label2 = new Label();
            this.label1 = new Label();
            this.groupBox2 = new GroupBox();
            this.symbolItem1 = new SymbolItem();
            this.cboScale = new ComboBoxEdit();
            this.btnOnt2One = new SimpleButton();
            this.btnFixZoomOut = new SimpleButton();
            this.btnFixZoomIn = new SimpleButton();
            this.chkLine = new CheckBox();
            this.groupBox3 = new GroupBox();
            this.btnPaste = new SimpleButton();
            this.btnCopy = new SimpleButton();
            this.tnMoveDown = new SimpleButton();
            this.btnMoveUp = new SimpleButton();
            this.btnDeleteLayer = new SimpleButton();
            this.btnAddLayer = new SimpleButton();
            this.symbolListBox1 = new SymbolListBox();
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.barAndDockingController1 = new BarAndDockingController(this.components);
            this.groupBox1.SuspendLayout();
            this.tabControl1.BeginInit();
            this.cboUnit.Properties.BeginInit();
            this.cboMarkerType.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.cboScale.Properties.BeginInit();
            this.groupBox3.SuspendLayout();
            this.barAndDockingController1.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.tabControl1);
            this.groupBox1.Controls.Add(this.cboUnit);
            this.groupBox1.Controls.Add(this.cboMarkerType);
            this.groupBox1.Controls.Add(this.tabControl11);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(200, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x1b0, 0x188);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "属性";
            this.tabControl1.Location = new Point(0x10, 0x40);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Size = new Size(0x198, 0x138);
            this.tabControl1.TabIndex = 15;
            this.tabControl1.Text = "xtraTabControl1";
            this.cboUnit.EditValue = "";
            this.cboUnit.Location = new Point(0xf8, 30);
            this.cboUnit.Name = "cboUnit";
            this.cboUnit.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboUnit.Properties.Items.AddRange(new object[] { "点", "英寸", "厘米", "毫米" });
            this.cboUnit.Size = new Size(0x98, 0x15);
            this.cboUnit.TabIndex = 14;
            this.cboUnit.SelectedIndexChanged += new EventHandler(this.cboUnit_SelectedIndexChanged);
            this.cboMarkerType.EditValue = "";
            this.cboMarkerType.Location = new Point(0x30, 30);
            this.cboMarkerType.Name = "cboMarkerType";
            this.cboMarkerType.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboMarkerType.Properties.Items.AddRange(new object[] { "简单点符号", "箭头点符号", "图片点符号", "字符点符号", "3D字符点符号", "3D简单点符号", "3D点符号" });
            this.cboMarkerType.Size = new Size(0x90, 0x15);
            this.cboMarkerType.TabIndex = 13;
            this.cboMarkerType.SelectedIndexChanged += new EventHandler(this.cboMarkerType_SelectedIndexChanged);
            this.tabControl11.Location = new Point(0x180, 0x40);
            this.tabControl11.Name = "tabControl11";
            this.tabControl11.SelectedIndex = 0;
            this.tabControl11.Size = new Size(40, 0x138);
            this.tabControl11.TabIndex = 6;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0xd0, 0x20);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "单位";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 0x20);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "类型";
            this.groupBox2.Controls.Add(this.symbolItem1);
            this.groupBox2.Controls.Add(this.cboScale);
            this.groupBox2.Controls.Add(this.btnOnt2One);
            this.groupBox2.Controls.Add(this.btnFixZoomOut);
            this.groupBox2.Controls.Add(this.btnFixZoomIn);
            this.groupBox2.Controls.Add(this.chkLine);
            this.groupBox2.Location = new Point(8, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0xa8, 0xb8);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "预览";
            this.symbolItem1.BackColor = SystemColors.ControlLight;
            this.symbolItem1.Location = new Point(8, 0x18);
            this.symbolItem1.Name = "symbolItem1";
            this.symbolItem1.Size = new Size(0x98, 0x60);
            this.symbolItem1.Symbol = null;
            this.symbolItem1.TabIndex = 0x1d;
            this.cboScale.EditValue = "";
            this.cboScale.Location = new Point(0x5c, 0x98);
            this.cboScale.Name = "cboScale";
            this.cboScale.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboScale.Properties.Items.AddRange(new object[] { "400%", "200%", "100%", "75%", "50%" });
            this.cboScale.Size = new Size(0x48, 0x15);
            this.cboScale.TabIndex = 0x1c;
            this.cboScale.SelectedIndexChanged += new EventHandler(this.cboScale_SelectedIndexChanged);
            this.btnOnt2One.Image = (Image) resources.GetObject("btnOnt2One.Image");
            this.btnOnt2One.Location = new Point(60, 0x98);
            this.btnOnt2One.Name = "btnOnt2One";
            this.btnOnt2One.Size = new Size(0x16, 0x16);
            this.btnOnt2One.TabIndex = 0x1b;
            this.btnOnt2One.Click += new EventHandler(this.btnOnt2One_Click);
            this.btnFixZoomOut.Image = (Image) resources.GetObject("btnFixZoomOut.Image");
            this.btnFixZoomOut.Location = new Point(0x22, 0x98);
            this.btnFixZoomOut.Name = "btnFixZoomOut";
            this.btnFixZoomOut.Size = new Size(0x16, 0x16);
            this.btnFixZoomOut.TabIndex = 0x1a;
            this.btnFixZoomOut.Click += new EventHandler(this.btnFixZoomOut_Click);
            this.btnFixZoomIn.Image = (Image) resources.GetObject("btnFixZoomIn.Image");
            this.btnFixZoomIn.Location = new Point(4, 0x98);
            this.btnFixZoomIn.Name = "btnFixZoomIn";
            this.btnFixZoomIn.Size = new Size(0x16, 0x16);
            this.btnFixZoomIn.TabIndex = 0x19;
            this.btnFixZoomIn.Click += new EventHandler(this.btnFixZoomIn_Click);
            this.chkLine.Checked = true;
            this.chkLine.CheckState = CheckState.Checked;
            this.chkLine.Image = (Image) resources.GetObject("chkLine.Image");
            this.chkLine.ImageAlign = ContentAlignment.MiddleLeft;
            this.chkLine.Location = new Point(10, 0x80);
            this.chkLine.Name = "chkLine";
            this.chkLine.Size = new Size(0x38, 0x10);
            this.chkLine.TabIndex = 2;
            this.chkLine.CheckedChanged += new EventHandler(this.chkLine_CheckedChanged);
            this.groupBox3.Controls.Add(this.btnPaste);
            this.groupBox3.Controls.Add(this.btnCopy);
            this.groupBox3.Controls.Add(this.tnMoveDown);
            this.groupBox3.Controls.Add(this.btnMoveUp);
            this.groupBox3.Controls.Add(this.btnDeleteLayer);
            this.groupBox3.Controls.Add(this.btnAddLayer);
            this.groupBox3.Controls.Add(this.symbolListBox1);
            this.groupBox3.Location = new Point(0x10, 200);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(160, 0xe8);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "图层";
            this.btnPaste.Image = (Image) resources.GetObject("btnPaste.Image");
            this.btnPaste.Location = new Point(0x34, 200);
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new Size(0x18, 0x18);
            this.btnPaste.TabIndex = 0x17;
            this.btnPaste.Click += new EventHandler(this.btnPaste_Click);
            this.btnCopy.Image = (Image) resources.GetObject("btnCopy.Image");
            this.btnCopy.Location = new Point(20, 200);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new Size(0x18, 0x18);
            this.btnCopy.TabIndex = 0x16;
            this.btnCopy.Click += new EventHandler(this.btnCopy_Click);
            this.tnMoveDown.Image = (Image) resources.GetObject("tnMoveDown.Image");
            this.tnMoveDown.Location = new Point(0x74, 0xa8);
            this.tnMoveDown.Name = "tnMoveDown";
            this.tnMoveDown.Size = new Size(0x18, 0x18);
            this.tnMoveDown.TabIndex = 0x15;
            this.tnMoveDown.Click += new EventHandler(this.tnMoveDown_Click);
            this.btnMoveUp.Image = (Image) resources.GetObject("btnMoveUp.Image");
            this.btnMoveUp.Location = new Point(0x54, 0xa8);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new Size(0x18, 0x18);
            this.btnMoveUp.TabIndex = 20;
            this.btnMoveUp.Click += new EventHandler(this.btnMoveUp_Click);
            this.btnDeleteLayer.Image = (Image) resources.GetObject("btnDeleteLayer.Image");
            this.btnDeleteLayer.Location = new Point(0x34, 0xa8);
            this.btnDeleteLayer.Name = "btnDeleteLayer";
            this.btnDeleteLayer.Size = new Size(0x18, 0x18);
            this.btnDeleteLayer.TabIndex = 0x13;
            this.btnDeleteLayer.Click += new EventHandler(this.btnDeleteLayer_Click);
            this.btnAddLayer.Image = (Image) resources.GetObject("btnAddLayer.Image");
            this.btnAddLayer.Location = new Point(20, 0xa8);
            this.btnAddLayer.Name = "btnAddLayer";
            this.btnAddLayer.Size = new Size(0x18, 0x18);
            this.btnAddLayer.TabIndex = 0x12;
            this.btnAddLayer.Click += new EventHandler(this.btnAddLayer_Click);
            this.symbolListBox1.DrawMode = DrawMode.OwnerDrawVariable;
            this.symbolListBox1.Location = new Point(8, 0x10);
            this.symbolListBox1.Name = "symbolListBox1";
            this.symbolListBox1.Size = new Size(0x90, 0x90);
            this.symbolListBox1.TabIndex = 12;
            this.symbolListBox1.LayerVisibleChanged += new LayerVisibleChangedHandler(this.symbolListBox1_LayerVisibleChanged);
            this.symbolListBox1.LayerColorLockedChanged += new LayerColorLockedChangedHandler(this.symbolListBox1_LayerColorLockedChanged);
            this.symbolListBox1.SelectedIndexChanged += new EventHandler(this.symbolListBox1_SelectedIndexChanged);
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0x1e8, 0x198);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x38, 0x18);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0x238, 0x198);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x38, 0x18);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.barAndDockingController1.PaintStyleName = "Office2003";
            this.barAndDockingController1.PropertiesBar.AllowLinkLighting = false;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x282, 0x1b7);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) resources.GetObject("$Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmPointSymbolEdit";
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "点符号编辑器";
            base.Load += new EventHandler(this.frmPointSymbolEdit_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.EndInit();
            this.cboUnit.Properties.EndInit();
            this.cboMarkerType.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.cboScale.Properties.EndInit();
            this.groupBox3.ResumeLayout(false);
            this.barAndDockingController1.EndInit();
            base.ResumeLayout(false);
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

