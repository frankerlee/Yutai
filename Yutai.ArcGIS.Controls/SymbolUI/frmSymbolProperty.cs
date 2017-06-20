using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    internal class frmSymbolProperty : Form
    {
        private SimpleButton btnCancle;
        private SimpleButton btnChangeSymbol;
        private SimpleButton btnOK;
        private Container components = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label2;
        private IStyleGallery m_pSG = null;
        private IStyleGalleryItem m_pStyleGalleryItem;
        private enumSymbolType m_SymbolType;
        private SymbolItem symbolItem1;
        private TextEdit txtCategory;
        private TextEdit txtName;

        public frmSymbolProperty()
        {
            this.InitializeComponent();
            this.symbolItem1.HasDrawLine = false;
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.m_pSG = null;
            this.m_pStyleGalleryItem = null;
            base.Close();
        }

        private void btnChangeSymbol_Click(object sender, EventArgs e)
        {
            if (!(this.m_pStyleGalleryItem.Item is ISymbol))
            {
                frmElementProperty property;
                object item = this.m_pStyleGalleryItem.Item;
                if (item is ISymbolBorder)
                {
                    property = new frmElementProperty();
                    BorderSymbolPropertyPage page = new BorderSymbolPropertyPage();
                    property.Text = "边界";
                    property.AddPage(page);
                    if (property.EditProperties(item))
                    {
                        this.symbolItem1.Symbol = item;
                        this.m_pStyleGalleryItem.Item = item;
                        this.symbolItem1.Invalidate();
                    }
                }
                else if (item is ISymbolBackground)
                {
                    property = new frmElementProperty();
                    BackgroundSymbolPropertyPage page2 = new BackgroundSymbolPropertyPage();
                    property.Text = "背景";
                    property.AddPage(page2);
                    if (property.EditProperties(item))
                    {
                        this.symbolItem1.Symbol = item;
                        this.m_pStyleGalleryItem.Item = item;
                        this.symbolItem1.Invalidate();
                    }
                }
                else if (item is ISymbolShadow)
                {
                    property = new frmElementProperty();
                    ShadowSymbolPropertyPage page3 = new ShadowSymbolPropertyPage();
                    property.Text = "阴影";
                    property.AddPage(page3);
                    if (property.EditProperties(item))
                    {
                        this.symbolItem1.Symbol = item;
                        this.m_pStyleGalleryItem.Item = item;
                        this.symbolItem1.Invalidate();
                    }
                }
                else if (item is INorthArrow)
                {
                    property = new frmElementProperty();
                    NorthArrowPropertyPage page4 = new NorthArrowPropertyPage();
                    property.Text = "指北针";
                    property.AddPage(page4);
                    if (property.EditProperties(item))
                    {
                        this.symbolItem1.Symbol = item;
                        this.m_pStyleGalleryItem.Item = item;
                        this.symbolItem1.Invalidate();
                    }
                }
                else
                {
                    IPropertyPage page5;
                    if (item is IScaleBar)
                    {
                        property = new frmElementProperty {
                            Text = "比例尺"
                        };
                        page5 = new ScaleBarFormatPropertyPage();
                        property.AddPage(page5);
                        page5 = new ScaleAndUnitsPropertyPage();
                        property.AddPage(page5);
                        page5 = new NumberAndLabelPropertyPage();
                        property.AddPage(page5);
                        if (property.EditProperties(item))
                        {
                            this.symbolItem1.Symbol = item;
                            this.m_pStyleGalleryItem.Item = item;
                            this.symbolItem1.Invalidate();
                        }
                    }
                    else if (item is IScaleText)
                    {
                        property = new frmElementProperty {
                            Text = "比例尺文本"
                        };
                        page5 = new ScaleTextTextPropertyPage();
                        property.AddPage(page5);
                        page5 = new ScaleTextFormatPropertyPage();
                        property.AddPage(page5);
                        if (property.EditProperties(item))
                        {
                            this.symbolItem1.Symbol = item;
                            this.m_pStyleGalleryItem.Item = item;
                            this.symbolItem1.Invalidate();
                        }
                    }
                    else if (item is ILegendItem)
                    {
                        property = new frmElementProperty {
                            Text = "图例项"
                        };
                        page5 = new LegendItemArrangementPropertyPage();
                        property.AddPage(page5);
                        page5 = new LegendItemGeneralPropertyPage();
                        property.AddPage(page5);
                        if (property.EditProperties(item))
                        {
                            this.symbolItem1.Symbol = item;
                            this.m_pStyleGalleryItem.Item = item;
                            this.symbolItem1.Invalidate();
                        }
                    }
                }
            }
            else
            {
                DialogResult result;
                ISymbol pSym = (ISymbol) this.m_pStyleGalleryItem.Item;
                switch (this.m_SymbolType)
                {
                    case enumSymbolType.enumSTPoint:
                    {
                        frmPointSymbolEdit edit = new frmPointSymbolEdit {
                            m_pSG = this.m_pSG
                        };
                        edit.SetSymbol(pSym);
                        result = edit.ShowDialog();
                        pSym = edit.GetSymbol();
                        break;
                    }
                    case enumSymbolType.enumSTLine:
                    {
                        frmLineSymbolEdit edit2 = new frmLineSymbolEdit {
                            m_pSG = this.m_pSG
                        };
                        edit2.SetSymbol(pSym);
                        result = edit2.ShowDialog();
                        pSym = edit2.GetSymbol();
                        break;
                    }
                    case enumSymbolType.enumSTFill:
                    {
                        frmFillSymbolEdit edit3 = new frmFillSymbolEdit {
                            m_pSG = this.m_pSG
                        };
                        edit3.SetSymbol(pSym);
                        result = edit3.ShowDialog();
                        pSym = edit3.GetSymbol();
                        break;
                    }
                    case enumSymbolType.enumSTText:
                    {
                        frmTextSymbolEdit edit4 = new frmTextSymbolEdit {
                            m_pSG = this.m_pSG
                        };
                        edit4.SetSymbol(pSym);
                        result = edit4.ShowDialog();
                        pSym = edit4.GetSymbol();
                        break;
                    }
                    default:
                        return;
                }
                if (result == DialogResult.OK)
                {
                    this.symbolItem1.Symbol = pSym;
                    this.m_pStyleGalleryItem.Item = pSym;
                    this.symbolItem1.Invalidate();
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmSymbolProperty_Load(object sender, EventArgs e)
        {
            if (this.m_pStyleGalleryItem != null)
            {
                this.txtName.Text = this.m_pStyleGalleryItem.Name;
                this.txtCategory.Text = this.m_pStyleGalleryItem.Category;
                this.symbolItem1.Symbol = this.m_pStyleGalleryItem.Item;
                if (this.symbolItem1.Symbol is IMarkerSymbol)
                {
                    this.m_SymbolType = enumSymbolType.enumSTPoint;
                }
                else if (this.symbolItem1.Symbol is ILineSymbol)
                {
                    this.m_SymbolType = enumSymbolType.enumSTLine;
                }
                else if (this.symbolItem1.Symbol is IFillSymbol)
                {
                    this.m_SymbolType = enumSymbolType.enumSTFill;
                }
                else if (this.symbolItem1.Symbol is ITextSymbol)
                {
                    this.m_SymbolType = enumSymbolType.enumSTText;
                }
                else if (this.symbolItem1.Symbol is IColorRamp)
                {
                    this.m_SymbolType = enumSymbolType.enumSTColorRamp;
                }
                else if (this.symbolItem1.Symbol is IColor)
                {
                    this.m_SymbolType = enumSymbolType.enumSTColor;
                }
                else if (this.symbolItem1.Symbol is ISymbolBorder)
                {
                    this.m_SymbolType = enumSymbolType.enumSTBorder;
                }
                else if (this.symbolItem1.Symbol is ISymbolBackground)
                {
                    this.m_SymbolType = enumSymbolType.enumSTBackground;
                }
                else if (this.symbolItem1.Symbol is ISymbolShadow)
                {
                    this.m_SymbolType = enumSymbolType.enumSTShadow;
                }
                else if (this.symbolItem1.Symbol is IMarkerNorthArrow)
                {
                    this.m_SymbolType = enumSymbolType.enumSTNorthArrow;
                }
                else if (this.symbolItem1.Symbol is IScaleBar)
                {
                    this.m_SymbolType = enumSymbolType.enumSTScaleBar;
                }
                else if (this.symbolItem1.Symbol is IScaleText)
                {
                    this.m_SymbolType = enumSymbolType.enumSTScaleText;
                }
                else if (this.symbolItem1.Symbol is ILegendItem)
                {
                    this.m_SymbolType = enumSymbolType.enumSTLegendItem;
                }
                else
                {
                    this.m_SymbolType = enumSymbolType.enumSTUnknown;
                }
            }
            else
            {
                this.m_pStyleGalleryItem = new ServerStyleGalleryItemClass();
                object obj2 = null;
                switch (this.m_SymbolType)
                {
                    case enumSymbolType.enumSTPoint:
                        this.txtName.Text = "点符号";
                        obj2 = new MultiLayerMarkerSymbolClass();
                        ((IMultiLayerMarkerSymbol) obj2).AddLayer(new SimpleMarkerSymbolClass());
                        break;

                    case enumSymbolType.enumSTLine:
                        this.txtName.Text = "线符号";
                        obj2 = new MultiLayerLineSymbolClass();
                        ((IMultiLayerLineSymbol) obj2).AddLayer(new SimpleLineSymbolClass());
                        break;

                    case enumSymbolType.enumSTFill:
                        this.txtName.Text = "面符号";
                        obj2 = new MultiLayerFillSymbolClass();
                        ((IMultiLayerFillSymbol) obj2).AddLayer(new SimpleFillSymbolClass());
                        break;

                    case enumSymbolType.enumSTText:
                    case enumSymbolType.enumSTColorRamp:
                    case enumSymbolType.enumSTColor:
                    case enumSymbolType.enumSTLinePatch:
                    case enumSymbolType.enumSTAreaPatch:
                        return;

                    case enumSymbolType.enumSTNorthArrow:
                        this.txtName.Text = "指北针";
                        obj2 = new MarkerNorthArrowClass();
                        break;

                    case enumSymbolType.enumSTScaleBar:
                        this.txtName.Text = "比例尺";
                        obj2 = new ScaleLineClass();
                        break;

                    case enumSymbolType.enumSTScaleText:
                        this.txtName.Text = "比例尺文本";
                        obj2 = new ScaleTextClass();
                        break;

                    case enumSymbolType.enumSTBorder:
                        this.txtName.Text = "边界";
                        obj2 = new SymbolBorderClass();
                        break;

                    case enumSymbolType.enumSTShadow:
                        this.txtName.Text = "阴影";
                        obj2 = new SymbolShadowClass();
                        break;

                    case enumSymbolType.enumSTBackground:
                        this.txtName.Text = "背景";
                        obj2 = new SymbolBackgroundClass();
                        break;

                    case enumSymbolType.enumSTLegendItem:
                        this.txtName.Text = "图例项";
                        obj2 = new HorizontalLegendItemClass();
                        break;

                    case enumSymbolType.enumSTLabel:
                        this.txtName.Text = "标注";
                        obj2 = new LabelStyleClass();
                        break;

                    default:
                        return;
                }
                this.symbolItem1.Symbol = obj2;
                this.m_pStyleGalleryItem.Name = this.txtName.Text;
                this.m_pStyleGalleryItem.Category = this.txtCategory.Text;
                this.m_pStyleGalleryItem.Item = obj2;
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSymbolProperty));
            this.btnOK = new SimpleButton();
            this.btnCancle = new SimpleButton();
            this.groupBox1 = new GroupBox();
            this.symbolItem1 = new SymbolItem();
            this.groupBox2 = new GroupBox();
            this.txtCategory = new TextEdit();
            this.txtName = new TextEdit();
            this.label2 = new Label();
            this.label1 = new Label();
            this.btnChangeSymbol = new SimpleButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.txtCategory.Properties.BeginInit();
            this.txtName.Properties.BeginInit();
            base.SuspendLayout();
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0x40, 0xe8);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x30, 0x18);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.tnOK_Click);
            this.btnCancle.DialogResult = DialogResult.Cancel;
            this.btnCancle.Location = new Point(120, 0xe8);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new Size(0x30, 0x18);
            this.btnCancle.TabIndex = 7;
            this.btnCancle.Text = "取消";
            this.btnCancle.Click += new EventHandler(this.btnCancle_Click);
            this.groupBox1.Controls.Add(this.symbolItem1);
            this.groupBox1.Location = new Point(8, 0x68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xa8, 120);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "样式";
            this.symbolItem1.BackColor = SystemColors.ControlLight;
            this.symbolItem1.Location = new Point(12, 0x18);
            this.symbolItem1.Name = "symbolItem1";
            this.symbolItem1.Size = new Size(0x90, 0x58);
            this.symbolItem1.Symbol = null;
            this.symbolItem1.TabIndex = 5;
            this.groupBox2.Controls.Add(this.txtCategory);
            this.groupBox2.Controls.Add(this.txtName);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new Point(8, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0xa8, 0x58);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "符号名称";
            this.txtCategory.EditValue = "";
            this.txtCategory.Location = new Point(40, 0x38);
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.Size = new Size(0x70, 0x15);
            this.txtCategory.TabIndex = 12;
            this.txtName.EditValue = "";
            this.txtName.Location = new Point(40, 0x18);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(0x70, 0x15);
            this.txtName.TabIndex = 11;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 60);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "种类";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0x1c);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "名称";
            this.btnChangeSymbol.Location = new Point(8, 0xe8);
            this.btnChangeSymbol.Name = "btnChangeSymbol";
            this.btnChangeSymbol.Size = new Size(0x30, 0x18);
            this.btnChangeSymbol.TabIndex = 10;
            this.btnChangeSymbol.Text = "属性...";
            this.btnChangeSymbol.Click += new EventHandler(this.btnChangeSymbol_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0xba, 0x107);
            base.Controls.Add(this.btnChangeSymbol);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.btnCancle);
            base.Controls.Add(this.btnOK);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmSymbolProperty";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "符号属性";
            base.Load += new EventHandler(this.frmSymbolProperty_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.txtCategory.Properties.EndInit();
            this.txtName.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void tnOK_Click(object sender, EventArgs e)
        {
            this.m_pSG = null;
            this.m_pStyleGalleryItem.Name = this.txtName.Text;
            this.m_pStyleGalleryItem.Category = this.txtCategory.Text;
            base.Close();
        }

        public IStyleGallery StyleGallery
        {
            set
            {
                this.m_pSG = value;
            }
        }

        public IStyleGalleryItem StyleGalleryItem
        {
            get
            {
                return this.m_pStyleGalleryItem;
            }
            set
            {
                this.m_pStyleGalleryItem = value;
            }
        }

        public enumSymbolType SymbolType
        {
            get
            {
                return this.m_SymbolType;
            }
            set
            {
                this.m_SymbolType = value;
            }
        }
    }
}

