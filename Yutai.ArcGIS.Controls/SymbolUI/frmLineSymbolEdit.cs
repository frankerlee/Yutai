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
    public class frmLineSymbolEdit : Form
    {
        private double[] point_unit_to = new double[] { 1, 0.01388889, 0.0352777778, 0.352777778 };

        private ISymbol m_pOldSymbol;

        private IMultiLayerLineSymbol m_pMultiLineSymbol;

        private bool m_CanDo = true;

        private double[] m_dblScaleRatio = new double[] { 0.25, 0.5, 1, 1.25, 2 };

        private int m_ScaleIndex = 2;

        private int m_OldSelItem = 0;

        public IStyleGallery m_pSG;

        private int m_oldSel = -1;

        private int m_oldSelType = -1;

        private ILineSymbol m_CopySymbol = null;

        private Label label1;

        private Label label2;

        private GroupBox groupBox2;

        private SymbolItem symbolItem1;

        private GroupBox groupBox3;

        private SymbolListBox symbolListBox1;

        private RadioButton rdoLine;

        private GroupBox rdoPline1;

        private TabControl tabControl1;

        private RadioButton rdoPline;

        private SimpleButton btnPaste;

        private SimpleButton btnCopy;

        private SimpleButton tnMoveDown;

        private SimpleButton btnMoveUp;

        private SimpleButton btnDeleteLayer;

        private SimpleButton btnAddLayer;

        private ComboBoxEdit cboScale;

        private SimpleButton btnOnt2One;

        private SimpleButton btnFixZoomOut;

        private SimpleButton btnFixZoomIn;

        private ComboBoxEdit cboUnit;

        private ComboBoxEdit cboLineType;

        private SimpleButton btnCancel;

        private SimpleButton btnOK;

        private XtraTabControl xtraTabControl1;

        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private Container components = null;

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
            ((ILayerColorLock)this.m_pMultiLineSymbol).LayerColorLock[0] = false;
            this.InitControl((ISymbol)this.m_pMultiLineSymbol);
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
            this.m_CopySymbol = (ILineSymbol)((IClone)layer).Clone();
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
                this.InitControl((ISymbol)this.m_pMultiLineSymbol);
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
                this.InitControl((ISymbol)this.m_pMultiLineSymbol);
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
                ILineSymbol lineSymbol = (ILineSymbol)((IClone)this.m_CopySymbol).Clone();
                this.m_pMultiLineSymbol.AddLayer(lineSymbol);
                this.m_OldSelItem = 0;
                this.m_pMultiLineSymbol.MoveLayer(lineSymbol, 0);
                this.InitControl((ISymbol)this.m_pMultiLineSymbol);
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
                    ((ILayerColorLock)this.m_pMultiLineSymbol).LayerColorLock [this.symbolListBox1.SelectedIndex] = false;
                    this.m_pMultiLineSymbol.DeleteLayer(layer);
                    this.InitControl((ISymbol)this.m_pMultiLineSymbol);
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
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.components != null)
                {
                    this.components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private void frmLineSymbolEdit_Load(object sender, EventArgs e)
        {
            this.m_CanDo = false;
            this.cboUnit.SelectedIndex = 0;
            this.cboScale.SelectedIndex = this.m_ScaleIndex;
            this.m_CanDo = true;
            this.rdoLine.Checked = true;
            this.InitControl((ISymbol)this.m_pMultiLineSymbol);
        }

        public ISymbol GetSymbol()
        {
            ISymbol symbol;
            symbol = (!(this.m_pOldSymbol is IMultiLayerLineSymbol) ? (ISymbol)this.m_pMultiLineSymbol : (ISymbol)this.m_pMultiLineSymbol);
            return symbol;
        }

        private void InitControl(ISymbol pSym)
        {
            this.symbolItem1.Symbol = pSym;
            if (pSym is IMultiLayerLineSymbol)
            {
                this.symbolListBox1.Items.Clear();
                this.m_oldSel = -1;
                for (int i = 0; i < ((IMultiLayerLineSymbol)pSym).LayerCount; i++)
                {
                    SymbolListItem symbolListItem = new SymbolListItem()
                    {
                        m_pSymbol = (ISymbol)((IMultiLayerLineSymbol)pSym).Layer [i],
                        m_bVisible = ((ILayerVisible)pSym).LayerVisible[i],
                        m_bLockColor = ((ILayerColorLock)pSym).LayerColorLock[i]
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
        private void InitializeComponent()
        {
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmLineSymbolEdit));
            this.rdoPline1 = new GroupBox();
            this.xtraTabControl1 = new XtraTabControl();
            this.cboUnit = new ComboBoxEdit();
            this.cboLineType = new ComboBoxEdit();
            this.tabControl1 = new TabControl();
            this.label2 = new Label();
            this.label1 = new Label();
            this.groupBox2 = new GroupBox();
            this.cboScale = new ComboBoxEdit();
            this.btnOnt2One = new SimpleButton();
            this.btnFixZoomOut = new SimpleButton();
            this.btnFixZoomIn = new SimpleButton();
            this.rdoPline = new RadioButton();
            this.rdoLine = new RadioButton();
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
            this.rdoPline1.SuspendLayout();
            ((ISupportInitialize)this.xtraTabControl1).BeginInit();
            ((ISupportInitialize)this.cboUnit.Properties).BeginInit();
            ((ISupportInitialize)this.cboLineType.Properties).BeginInit();
            this.groupBox2.SuspendLayout();
            ((ISupportInitialize)this.cboScale.Properties).BeginInit();
            this.groupBox3.SuspendLayout();
            base.SuspendLayout();
            this.rdoPline1.Controls.Add(this.xtraTabControl1);
            this.rdoPline1.Controls.Add(this.cboUnit);
            this.rdoPline1.Controls.Add(this.cboLineType);
            this.rdoPline1.Controls.Add(this.tabControl1);
            this.rdoPline1.Controls.Add(this.label2);
            this.rdoPline1.Controls.Add(this.label1);
            this.rdoPline1.Location = new Point(192, 8);
            this.rdoPline1.Name = "rdoPline1";
            this.rdoPline1.Size = new Size(464, 400);
            this.rdoPline1.TabIndex = 0;
            this.rdoPline1.TabStop = false;
            this.rdoPline1.Text = "属性";
            this.xtraTabControl1.Location = new Point(16, 56);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.Size = new Size(432, 328);
            this.xtraTabControl1.TabIndex = 13;
            this.xtraTabControl1.Text = "xtraTabControl1";
            this.cboUnit.EditValue = "";
            this.cboUnit.Location = new Point(240, 24);
            this.cboUnit.Name = "cboUnit";
            EditorButtonCollection buttons = this.cboUnit.Properties.Buttons;
            EditorButton[] editorButton = new EditorButton[] { new EditorButton(ButtonPredefines.Combo) };
            buttons.AddRange(editorButton);
            ComboBoxItemCollection items = this.cboUnit.Properties.Items;
            object[] objArray = new object[] { "点", "英寸", "厘米", "毫米" };
            items.AddRange(objArray);
            this.cboUnit.Size = new Size(152, 21);
            this.cboUnit.TabIndex = 12;
            this.cboUnit.SelectedIndexChanged += new EventHandler(this.cboUnit_SelectedIndexChanged);
            this.cboLineType.EditValue = "";
            this.cboLineType.Location = new Point(56, 24);
            this.cboLineType.Name = "cboLineType";
            EditorButtonCollection editorButtonCollection = this.cboLineType.Properties.Buttons;
            editorButton = new EditorButton[] { new EditorButton(ButtonPredefines.Combo) };
            editorButtonCollection.AddRange(editorButton);
            ComboBoxItemCollection comboBoxItemCollection = this.cboLineType.Properties.Items;
            objArray = new object[] { "简单线符号", "制图线符号", "标记线符号", "碎切线符号", "图片线符号", "3D简单线", "3D纹理线" };
            comboBoxItemCollection.AddRange(objArray);
            this.cboLineType.Size = new Size(144, 21);
            this.cboLineType.TabIndex = 11;
            this.cboLineType.SelectedIndexChanged += new EventHandler(this.cboLineType_SelectedIndexChanged);
            this.tabControl1.Location = new Point(288, 344);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(160, 40);
            this.tabControl1.TabIndex = 6;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(208, 26);
            this.label2.Name = "label2";
            this.label2.Size = new Size(29, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "单位";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 26);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "类型";
            this.groupBox2.Controls.Add(this.cboScale);
            this.groupBox2.Controls.Add(this.btnOnt2One);
            this.groupBox2.Controls.Add(this.btnFixZoomOut);
            this.groupBox2.Controls.Add(this.btnFixZoomIn);
            this.groupBox2.Controls.Add(this.rdoPline);
            this.groupBox2.Controls.Add(this.rdoLine);
            this.groupBox2.Controls.Add(this.symbolItem1);
            this.groupBox2.Location = new Point(8, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(168, 192);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "预览";
            this.cboScale.EditValue = "";
            this.cboScale.Location = new Point(92, 160);
            this.cboScale.Name = "cboScale";
            EditorButtonCollection buttons1 = this.cboScale.Properties.Buttons;
            editorButton = new EditorButton[] { new EditorButton(ButtonPredefines.Combo) };
            buttons1.AddRange(editorButton);
            ComboBoxItemCollection items1 = this.cboScale.Properties.Items;
            objArray = new object[] { "400%", "200%", "100%", "75%", "50%" };
            items1.AddRange(objArray);
            this.cboScale.Size = new Size(72, 21);
            this.cboScale.TabIndex = 28;
            this.cboScale.SelectedIndexChanged += new EventHandler(this.cboScale_SelectedIndexChanged);
            this.btnOnt2One.Image = (Image)componentResourceManager.GetObject("btnOnt2One.Image");
            this.btnOnt2One.Location = new Point(60, 160);
            this.btnOnt2One.Name = "btnOnt2One";
            this.btnOnt2One.Size = new Size(22, 22);
            this.btnOnt2One.TabIndex = 27;
            this.btnOnt2One.Click += new EventHandler(this.btnOnt2One_Click);
            this.btnFixZoomOut.Image = (Image)componentResourceManager.GetObject("btnFixZoomOut.Image");
            this.btnFixZoomOut.Location = new Point(34, 160);
            this.btnFixZoomOut.Name = "btnFixZoomOut";
            this.btnFixZoomOut.Size = new Size(22, 22);
            this.btnFixZoomOut.TabIndex = 26;
            this.btnFixZoomOut.Click += new EventHandler(this.btnFixZoomOut_Click);
            this.btnFixZoomIn.Image = (Image)componentResourceManager.GetObject("btnFixZoomIn.Image");
            this.btnFixZoomIn.Location = new Point(4, 160);
            this.btnFixZoomIn.Name = "btnFixZoomIn";
            this.btnFixZoomIn.Size = new Size(22, 22);
            this.btnFixZoomIn.TabIndex = 25;
            this.btnFixZoomIn.Click += new EventHandler(this.btnFixZoomIn_Click);
            this.rdoPline.Image = (Image)componentResourceManager.GetObject("rdoPline.Image");
            this.rdoPline.Location = new Point(56, 136);
            this.rdoPline.Name = "rdoPline";
            this.rdoPline.Size = new Size(32, 16);
            this.rdoPline.TabIndex = 18;
            this.rdoPline.Click += new EventHandler(this.rdoPline_Click);
            this.rdoLine.Image = (Image)componentResourceManager.GetObject("rdoLine.Image");
            this.rdoLine.Location = new Point(8, 136);
            this.rdoLine.Name = "rdoLine";
            this.rdoLine.Size = new Size(32, 16);
            this.rdoLine.TabIndex = 17;
            this.rdoLine.Click += new EventHandler(this.rdoLine_Click);
            this.symbolItem1.BackColor = SystemColors.ControlLight;
            this.symbolItem1.Location = new Point(16, 32);
            this.symbolItem1.Name = "symbolItem1";
            this.symbolItem1.Size = new Size(128, 88);
            this.symbolItem1.Symbol = null;
            this.symbolItem1.TabIndex = 1;
            this.groupBox3.Controls.Add(this.btnPaste);
            this.groupBox3.Controls.Add(this.btnCopy);
            this.groupBox3.Controls.Add(this.tnMoveDown);
            this.groupBox3.Controls.Add(this.btnMoveUp);
            this.groupBox3.Controls.Add(this.btnDeleteLayer);
            this.groupBox3.Controls.Add(this.btnAddLayer);
            this.groupBox3.Controls.Add(this.symbolListBox1);
            this.groupBox3.Location = new Point(16, 208);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(160, 232);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "图层";
            this.btnPaste.Image = (Image)componentResourceManager.GetObject("btnPaste.Image");
            this.btnPaste.Location = new Point(40, 200);
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new Size(24, 24);
            this.btnPaste.TabIndex = 23;
            this.btnPaste.Click += new EventHandler(this.btnPaste_Click);
            this.btnCopy.Image = (Image)componentResourceManager.GetObject("btnCopy.Image");
            this.btnCopy.Location = new Point(8, 200);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new Size(24, 24);
            this.btnCopy.TabIndex = 22;
            this.btnCopy.Click += new EventHandler(this.btnCopy_Click);
            this.tnMoveDown.Image = (Image)componentResourceManager.GetObject("tnMoveDown.Image");
            this.tnMoveDown.Location = new Point(104, 168);
            this.tnMoveDown.Name = "tnMoveDown";
            this.tnMoveDown.Size = new Size(24, 24);
            this.tnMoveDown.TabIndex = 21;
            this.tnMoveDown.Click += new EventHandler(this.tnMoveDown_Click);
            this.btnMoveUp.Image = (Image)componentResourceManager.GetObject("btnMoveUp.Image");
            this.btnMoveUp.Location = new Point(72, 168);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new Size(24, 24);
            this.btnMoveUp.TabIndex = 20;
            this.btnMoveUp.Click += new EventHandler(this.btnMoveUp_Click);
            this.btnDeleteLayer.Image = (Image)componentResourceManager.GetObject("btnDeleteLayer.Image");
            this.btnDeleteLayer.Location = new Point(40, 168);
            this.btnDeleteLayer.Name = "btnDeleteLayer";
            this.btnDeleteLayer.Size = new Size(24, 24);
            this.btnDeleteLayer.TabIndex = 19;
            this.btnDeleteLayer.Click += new EventHandler(this.btnDeleteLayer_Click);
            this.btnAddLayer.Image = (Image)componentResourceManager.GetObject("btnAddLayer.Image");
            this.btnAddLayer.Location = new Point(8, 168);
            this.btnAddLayer.Name = "btnAddLayer";
            this.btnAddLayer.Size = new Size(24, 24);
            this.btnAddLayer.TabIndex = 18;
            this.btnAddLayer.Click += new EventHandler(this.btnAddLayer_Click);
            this.symbolListBox1.DrawMode = DrawMode.OwnerDrawVariable;
            this.symbolListBox1.Location = new Point(8, 24);
            this.symbolListBox1.Name = "symbolListBox1";
            this.symbolListBox1.Size = new Size(144, 136);
            this.symbolListBox1.TabIndex = 5;
            this.symbolListBox1.LayerVisibleChanged += new LayerVisibleChangedHandler(this.symbolListBox1_LayerVisibleChanged);
            this.symbolListBox1.LayerColorLockedChanged += new LayerColorLockedChangedHandler(this.symbolListBox1_LayerColorLockedChanged);
            this.symbolListBox1.SelectedIndexChanged += new EventHandler(this.symbolListBox1_SelectedIndexChanged);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(584, 416);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(56, 24);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(504, 416);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(56, 24);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(666, 447);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.rdoPline1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
           
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmLineSymbolEdit";
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "线符号编辑器";
            base.Load += new EventHandler(this.frmLineSymbolEdit_Load);
            this.rdoPline1.ResumeLayout(false);
            this.rdoPline1.PerformLayout();
            ((ISupportInitialize)this.xtraTabControl1).EndInit();
            ((ISupportInitialize)this.cboUnit.Properties).EndInit();
            ((ISupportInitialize)this.cboLineType.Properties).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((ISupportInitialize)this.cboScale.Properties).EndInit();
            this.groupBox3.ResumeLayout(false);
            base.ResumeLayout(false);
        }

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
                this.m_pMultiLineSymbol = (IMultiLayerLineSymbol)((IClone)pSym).Clone();
            }
            else if (pSym is ILineSymbol)
            {
                this.m_pMultiLineSymbol = new MultiLayerLineSymbolClass();
                this.m_pMultiLineSymbol.AddLayer((ILineSymbol)((IClone)pSym).Clone());
                (this.m_pMultiLineSymbol as ILayerColorLock).LayerColorLock [0] = false;
            }
        }

        private void symbolListBox1_LayerColorLockedChanged(object sender, EventArgs e)
        {
            bool layerColorLock = ((ILayerColorLock)this.m_pMultiLineSymbol).LayerColorLock [this.symbolListBox1.SelectedIndex];
            ((ILayerColorLock)this.m_pMultiLineSymbol).LayerColorLock [this.symbolListBox1.SelectedIndex] = !layerColorLock;
        }

        private void symbolListBox1_LayerVisibleChanged(object sender, EventArgs e)
        {
            bool layerVisible = ((ILayerVisible)this.m_pMultiLineSymbol).LayerVisible [this.symbolListBox1.SelectedIndex];
            ((ILayerVisible)this.m_pMultiLineSymbol).LayerVisible [this.symbolListBox1.SelectedIndex] = !layerVisible;
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
                        m_pSimpleLineSymbol = (ISimpleLineSymbol)layer
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
                        m_pMarkerLineSymbol = (IMarkerLineSymbol)layer,
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
                        m_CartographLineSymbol = (ICartographicLineSymbol)layer
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
                        m_LineProperties = (ILineProperties)layer
                    };
                    templateControl.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                    xtraTabPage1.Controls.Add(templateControl);
                    xtraTabPage2 = new XtraTabPage()
                    {
                        Text = "线属性"
                    };
                    linePropertyControl = new LinePropertyControl()
                    {
                        m_LineProperties = (ILineProperties)layer,
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
                        m_pHashLineSymbol = (IHashLineSymbol)layer,
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
                        m_CartographLineSymbol = (ICartographicLineSymbol)layer
                    };
                    cartoLineControl.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                    xtraTabPage.Controls.Add(cartoLineControl);
                    xtraTabPage1 = new XtraTabPage()
                    {
                        Text = "模板"
                    };
                    templateControl = new TemplateControl()
                    {
                        m_LineProperties = (ILineProperties)layer,
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
                        m_LineProperties = (ILineProperties)layer,
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
                        m_CartographLineSymbol = (ICartographicLineSymbol)layer
                    };
                    cartoLineControl.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                    xtraTabPage.Controls.Add(cartoLineControl);
                    xtraTabPage1 = new XtraTabPage()
                    {
                        Text = "模板"
                    };
                    templateControl = new TemplateControl()
                    {
                        m_LineProperties = (ILineProperties)layer,
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
                        m_LineProperties = (ILineProperties)layer,
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
                        m_PictureLineSymbol = (IPictureLineSymbol)layer
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
                        m_pSimpleLine3DSymbol = (ISimpleLine3DSymbol)layer
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
                        m_pTextureLineSymbol = (ITextureLineSymbol)layer
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
                        m_pSimpleLineSymbol = (ISimpleLineSymbol)layer
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
                        m_pMarkerLineSymbol = (IMarkerLineSymbol)layer,
                        m_pSG = this.m_pSG
                    };
                    markerLineControl.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                    tabPage4.Controls.Add(markerLineControl);
                    tabPage = new TabPage("制图线");
                    cartoLineControl = new CartoLineControl()
                    {
                        m_unit = this.point_unit_to[this.cboUnit.SelectedIndex],
                        m_CartographLineSymbol = (ICartographicLineSymbol)layer
                    };
                    cartoLineControl.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                    tabPage.Controls.Add(cartoLineControl);
                    tabPage1 = new TabPage("模板");
                    templateControl = new TemplateControl()
                    {
                        m_unit = this.point_unit_to[this.cboUnit.SelectedIndex],
                        m_LineProperties = (ILineProperties)layer
                    };
                    templateControl.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                    tabPage1.Controls.Add(templateControl);
                    tabPage2 = new TabPage("线属性");
                    linePropertyControl = new LinePropertyControl()
                    {
                        m_LineProperties = (ILineProperties)layer,
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
                        m_pHashLineSymbol = (IHashLineSymbol)layer,
                        m_pSG = this.m_pSG
                    };
                    hashLineSymbolControl.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                    tabPage5.Controls.Add(hashLineSymbolControl);
                    tabPage = new TabPage("制图线");
                    cartoLineControl = new CartoLineControl()
                    {
                        m_unit = this.point_unit_to[this.cboUnit.SelectedIndex],
                        m_CartographLineSymbol = (ICartographicLineSymbol)layer
                    };
                    cartoLineControl.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                    tabPage.Controls.Add(cartoLineControl);
                    tabPage1 = new TabPage("模板");
                    templateControl = new TemplateControl()
                    {
                        m_LineProperties = (ILineProperties)layer,
                        m_unit = this.point_unit_to[this.cboUnit.SelectedIndex]
                    };
                    templateControl.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                    tabPage1.Controls.Add(templateControl);
                    tabPage2 = new TabPage("线属性");
                    linePropertyControl = new LinePropertyControl()
                    {
                        m_LineProperties = (ILineProperties)layer,
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
                        m_CartographLineSymbol = (ICartographicLineSymbol)layer
                    };
                    cartoLineControl.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                    tabPage.Controls.Add(cartoLineControl);
                    tabPage1 = new TabPage("模板");
                    templateControl = new TemplateControl()
                    {
                        m_LineProperties = (ILineProperties)layer,
                        m_unit = this.point_unit_to[this.cboUnit.SelectedIndex]
                    };
                    templateControl.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                    tabPage1.Controls.Add(templateControl);
                    tabPage2 = new TabPage("线属性");
                    linePropertyControl = new LinePropertyControl()
                    {
                        m_LineProperties = (ILineProperties)layer,
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
                        m_PictureLineSymbol = (IPictureLineSymbol)layer
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
                this.InitControl((ISymbol)this.m_pMultiLineSymbol);
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