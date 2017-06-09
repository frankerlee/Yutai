using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Resources;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Symbol;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;
using IPropertyPage = Yutai.ArcGIS.Common.BaseClasses.IPropertyPage;
using IPropertyPageEvents = Yutai.ArcGIS.Common.BaseClasses.IPropertyPageEvents;
using OnValueChangeEventHandler = Yutai.ArcGIS.Common.BaseClasses.OnValueChangeEventHandler;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public class ScaleBarFormatPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private SimpleButton btnStyleInfo;
        private SimpleButton btnStyleSelector;
        private SimpleButton btnSymbol1Selector;
        private SimpleButton btnSymbol2Selector;
        private SimpleButton btnSymbolSelector;
        private System.Windows.Forms.ComboBox cboFontName;
        private ComboBoxEdit cboFontSize;
        private StyleComboBox cboStyle;
        private CheckBox chkBold;
        private CheckBox chkItalic;
        private CheckBox chkUnderline;
        private ColorEdit colorBar;
        private ColorEdit colorTextSymbol;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private IContainer icontainer_0;
        private ImageList imageList_0;
        private ImageList imageList_1;
        private IMapSurroundFrame imapSurroundFrame_0 = null;
        private IStyleGallery istyleGallery_0 = ApplicationBase.StyleGallery;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        internal static IScaleBar m_pScaleBar;
        private string string_0 = "格式";
        private TextEdit txtBarSize;

        public event OnValueChangeEventHandler OnValueChange;

        static ScaleBarFormatPropertyPage()
        {
            old_acctor_mc();
        }

        public ScaleBarFormatPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                this.imapSurroundFrame_0.MapSurround = (m_pScaleBar as IClone).Clone() as IMapSurround;
            }
        }

        private void btnStyleSelector_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.istyleGallery_0);
                    if (m_pScaleBar != null)
                    {
                        selector.SetSymbol(m_pScaleBar);
                    }
                    else
                    {
                        selector.SetSymbol(new ScalebarClass());
                    }
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        m_pScaleBar = selector.GetSymbol() as IScaleBar;
                        this.method_0();
                        IStyleGalleryItem styleGalleryItemAt = this.cboStyle.GetStyleGalleryItemAt(this.cboStyle.Items.Count - 1);
                        if (styleGalleryItemAt != null)
                        {
                            if (styleGalleryItemAt.Name == "<定制>")
                            {
                                styleGalleryItemAt.Item = m_pScaleBar;
                            }
                            else
                            {
                                styleGalleryItemAt = new MyStyleGalleryItem {
                                    Name = "<定制>",
                                    Item = m_pScaleBar
                                };
                                this.cboStyle.Add(styleGalleryItemAt);
                                this.cboStyle.SelectedIndex = this.cboStyle.Items.Count - 1;
                            }
                        }
                        else
                        {
                            styleGalleryItemAt = new MyStyleGalleryItem {
                                Name = "<定制>",
                                Item = m_pScaleBar
                            };
                            this.cboStyle.Add(styleGalleryItemAt);
                            this.cboStyle.SelectedIndex = this.cboStyle.Items.Count - 1;
                        }
                        this.method_4();
                    }
                }
            }
            catch
            {
            }
        }

        private void btnSymbol1Selector_Click(object sender, EventArgs e)
        {
            try
            {
                ISymbol pSym = null;
                if (m_pScaleBar is IScaleLine)
                {
                    pSym = (m_pScaleBar as IScaleLine).LineSymbol as ISymbol;
                }
                else if (m_pScaleBar is ISingleFillScaleBar)
                {
                    pSym = (m_pScaleBar as ISingleFillScaleBar).FillSymbol as ISymbol;
                }
                else if (m_pScaleBar is IDoubleFillScaleBar)
                {
                    pSym = (m_pScaleBar as IDoubleFillScaleBar).FillSymbol1 as ISymbol;
                }
                if (pSym != null)
                {
                    frmSymbolSelector selector = new frmSymbolSelector();
                    if (selector != null)
                    {
                        selector.SetStyleGallery(this.istyleGallery_0);
                        selector.SetSymbol(pSym);
                        if (selector.ShowDialog() == DialogResult.OK)
                        {
                            if (m_pScaleBar is IScaleLine)
                            {
                                (m_pScaleBar as IScaleLine).LineSymbol = selector.GetSymbol() as ILineSymbol;
                            }
                            else if (m_pScaleBar is ISingleFillScaleBar)
                            {
                                (m_pScaleBar as ISingleFillScaleBar).FillSymbol = selector.GetSymbol() as IFillSymbol;
                            }
                            else if (m_pScaleBar is IDoubleFillScaleBar)
                            {
                                (m_pScaleBar as IDoubleFillScaleBar).FillSymbol1 = selector.GetSymbol() as IFillSymbol;
                            }
                            this.method_4();
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void btnSymbol2Selector_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_pScaleBar is IDoubleFillScaleBar)
                {
                    ISymbol pSym = (m_pScaleBar as IDoubleFillScaleBar).FillSymbol2 as ISymbol;
                    frmSymbolSelector selector = new frmSymbolSelector();
                    if (selector != null)
                    {
                        selector.SetStyleGallery(this.istyleGallery_0);
                        selector.SetSymbol(pSym);
                        if (selector.ShowDialog() == DialogResult.OK)
                        {
                            (m_pScaleBar as IDoubleFillScaleBar).FillSymbol2 = selector.GetSymbol() as IFillSymbol;
                            this.method_4();
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void btnSymbolSelector_Click(object sender, EventArgs e)
        {
            try
            {
                ITextSymbol labelSymbol = m_pScaleBar.LabelSymbol;
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.istyleGallery_0);
                    selector.SetSymbol(labelSymbol);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        labelSymbol = selector.GetSymbol() as ITextSymbol;
                        m_pScaleBar.LabelSymbol = labelSymbol;
                        this.bool_0 = false;
                        this.method_1(labelSymbol);
                        this.bool_0 = true;
                        this.method_4();
                    }
                }
            }
            catch
            {
            }
        }

        public void Cancel()
        {
        }

        private void cboFontName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                ITextSymbol labelSymbol = m_pScaleBar.LabelSymbol;
                stdole.IFontDisp font = labelSymbol.Font;
                if (font.Name != this.cboFontName.Name)
                {
                    font.Name = this.cboFontName.Name;
                    labelSymbol.Font = font;
                    m_pScaleBar.LabelSymbol = labelSymbol;
                    this.method_4();
                }
            }
        }

        private void cboFontSize_EditValueChanging(object sender, ChangingEventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    double num = double.Parse(this.cboFontSize.Text);
                    ITextSymbol labelSymbol = m_pScaleBar.LabelSymbol;
                    if (!(labelSymbol.Size == num))
                    {
                        labelSymbol.Size = double.Parse(this.cboFontSize.Text);
                        m_pScaleBar.LabelSymbol = labelSymbol;
                        this.method_4();
                    }
                }
                catch
                {
                }
            }
        }

        private void cboStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IStyleGalleryItem selectStyleGalleryItem = this.cboStyle.GetSelectStyleGalleryItem();
                if (selectStyleGalleryItem == null)
                {
                    m_pScaleBar = null;
                }
                else
                {
                    m_pScaleBar = (selectStyleGalleryItem.Item as IClone).Clone() as IScaleBar;
                }
                this.bool_0 = false;
                this.method_0();
                this.bool_0 = true;
                this.method_4();
            }
        }

        private void chkBold_Click(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                ITextSymbol labelSymbol = m_pScaleBar.LabelSymbol;
                stdole.IFontDisp font = labelSymbol.Font;
                if (font.Bold != this.chkBold.Checked)
                {
                    font.Bold = this.chkBold.Checked;
                    labelSymbol.Font = font;
                    m_pScaleBar.LabelSymbol = labelSymbol;
                    this.method_4();
                }
            }
        }

        private void chkItalic_Click(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                ITextSymbol labelSymbol = m_pScaleBar.LabelSymbol;
                stdole.IFontDisp font = labelSymbol.Font;
                if (font.Italic != this.chkItalic.Checked)
                {
                    font.Italic = this.chkItalic.Checked;
                    labelSymbol.Font = font;
                    m_pScaleBar.LabelSymbol = labelSymbol;
                    this.method_4();
                }
            }
        }

        private void chkUnderline_Click(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                ITextSymbol labelSymbol = m_pScaleBar.LabelSymbol;
                stdole.IFontDisp font = labelSymbol.Font;
                if (font.Underline != this.chkUnderline.Checked)
                {
                    font.Underline = this.chkUnderline.Checked;
                    labelSymbol.Font = font;
                    m_pScaleBar.LabelSymbol = labelSymbol;
                    this.method_4();
                }
            }
        }

        private void colorBar_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IColor barColor = m_pScaleBar.BarColor;
                this.method_2(this.colorBar, barColor);
                m_pScaleBar.BarColor = barColor;
                this.method_4();
            }
        }

        private void colorTextSymbol_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                ITextSymbol labelSymbol = m_pScaleBar.LabelSymbol;
                IColor color = labelSymbol.Color;
                this.method_2(this.colorTextSymbol, color);
                labelSymbol.Color = color;
                m_pScaleBar.LabelSymbol = labelSymbol;
                this.method_4();
            }
        }

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_2);
        }

        private void InitializeComponent()
        {
            this.icontainer_0 = new Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScaleBarFormatPropertyPage));
            this.groupBox1 = new GroupBox();
            this.btnSymbolSelector = new SimpleButton();
            this.label3 = new Label();
            this.label2 = new Label();
            this.label1 = new Label();
            this.cboFontSize = new ComboBoxEdit();
            this.colorTextSymbol = new ColorEdit();
            this.chkUnderline = new CheckBox();
            this.imageList_0 = new ImageList(this.icontainer_0);
            this.chkItalic = new CheckBox();
            this.chkBold = new CheckBox();
            this.cboFontName = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new GroupBox();
            this.btnSymbol2Selector = new SimpleButton();
            this.btnSymbol1Selector = new SimpleButton();
            this.txtBarSize = new TextEdit();
            this.label5 = new Label();
            this.label4 = new Label();
            this.colorBar = new ColorEdit();
            this.imageList_1 = new ImageList(this.icontainer_0);
            this.groupBox3 = new GroupBox();
            this.btnStyleInfo = new SimpleButton();
            this.btnStyleSelector = new SimpleButton();
            this.cboStyle = new StyleComboBox(this.icontainer_0);
            this.groupBox1.SuspendLayout();
            this.cboFontSize.Properties.BeginInit();
            this.colorTextSymbol.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.txtBarSize.Properties.BeginInit();
            this.colorBar.Properties.BeginInit();
            this.groupBox3.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.btnSymbolSelector);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cboFontSize);
            this.groupBox1.Controls.Add(this.colorTextSymbol);
            this.groupBox1.Controls.Add(this.chkUnderline);
            this.groupBox1.Controls.Add(this.chkItalic);
            this.groupBox1.Controls.Add(this.chkBold);
            this.groupBox1.Controls.Add(this.cboFontName);
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xe0, 0x68);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "文本";
            this.btnSymbolSelector.Location = new Point(0x90, 0x48);
            this.btnSymbolSelector.Name = "btnSymbolSelector";
            this.btnSymbolSelector.Size = new Size(0x38, 0x18);
            this.btnSymbolSelector.TabIndex = 0x18;
            this.btnSymbolSelector.Text = "符号...";
            this.btnSymbolSelector.Click += new EventHandler(this.btnSymbolSelector_Click);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(8, 0x4e);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x23, 0x11);
            this.label3.TabIndex = 0x17;
            this.label3.Text = "颜色:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 40);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x23, 0x11);
            this.label2.TabIndex = 0x16;
            this.label2.Text = "大小:";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0x11);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 0x11);
            this.label1.TabIndex = 0x15;
            this.label1.Text = "字体:";
            this.cboFontSize.EditValue = "5";
            this.cboFontSize.Location = new Point(0x30, 40);
            this.cboFontSize.Name = "cboFontSize";
            this.cboFontSize.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboFontSize.Properties.Items.AddRange(new object[] { 
                "5", "6", "7", "8", "9", "10", "11", "12", "14", "16", "18", "20", "22", "24", "26", "28", 
                "34", "36", "48", "72"
             });
            this.cboFontSize.Size = new Size(0x40, 0x15);
            this.cboFontSize.TabIndex = 20;
            this.cboFontSize.EditValueChanging += new ChangingEventHandler(this.cboFontSize_EditValueChanging);
            this.colorTextSymbol.EditValue = Color.Empty;
            this.colorTextSymbol.Location = new Point(0x30, 0x48);
            this.colorTextSymbol.Name = "colorTextSymbol";
            this.colorTextSymbol.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorTextSymbol.Size = new Size(0x30, 0x15);
            this.colorTextSymbol.TabIndex = 0x13;
            this.colorTextSymbol.EditValueChanged += new EventHandler(this.colorTextSymbol_EditValueChanged);
            this.chkUnderline.Appearance = Appearance.Button;
            this.chkUnderline.ImageIndex = 2;
            this.chkUnderline.ImageList = this.imageList_0;
            this.chkUnderline.Location = new Point(0xb8, 40);
            this.chkUnderline.Name = "chkUnderline";
            this.chkUnderline.Size = new Size(0x1c, 0x18);
            this.chkUnderline.TabIndex = 0x12;
            this.chkUnderline.Click += new EventHandler(this.chkUnderline_Click);
            this.imageList_0.ImageSize = new Size(0x10, 0x10);
            this.imageList_0.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            this.imageList_0.TransparentColor = Color.Magenta;
            this.chkItalic.Appearance = Appearance.Button;
            this.chkItalic.ImageIndex = 1;
            this.chkItalic.ImageList = this.imageList_0;
            this.chkItalic.Location = new Point(0x9c, 40);
            this.chkItalic.Name = "chkItalic";
            this.chkItalic.Size = new Size(0x1c, 0x18);
            this.chkItalic.TabIndex = 0x11;
            this.chkItalic.Click += new EventHandler(this.chkItalic_Click);
            this.chkBold.Appearance = Appearance.Button;
            this.chkBold.ImageIndex = 0;
            this.chkBold.ImageList = this.imageList_0;
            this.chkBold.Location = new Point(0x80, 40);
            this.chkBold.Name = "chkBold";
            this.chkBold.Size = new Size(0x1c, 0x18);
            this.chkBold.TabIndex = 0x10;
            this.chkBold.Click += new EventHandler(this.chkBold_Click);
            this.cboFontName.Location = new Point(0x30, 0x10);
            this.cboFontName.Name = "cboFontName";
            this.cboFontName.Size = new Size(160, 20);
            this.cboFontName.TabIndex = 15;
            this.cboFontName.SelectedIndexChanged += new EventHandler(this.cboFontName_SelectedIndexChanged);
            this.groupBox2.Controls.Add(this.btnSymbol2Selector);
            this.groupBox2.Controls.Add(this.btnSymbol1Selector);
            this.groupBox2.Controls.Add(this.txtBarSize);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.colorBar);
            this.groupBox2.Location = new Point(8, 120);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0xe0, 0x58);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "栏";
            this.btnSymbol2Selector.Location = new Point(0x90, 0x30);
            this.btnSymbol2Selector.Name = "btnSymbol2Selector";
            this.btnSymbol2Selector.Size = new Size(0x38, 0x18);
            this.btnSymbol2Selector.TabIndex = 0x1d;
            this.btnSymbol2Selector.Text = "符号2...";
            this.btnSymbol2Selector.Click += new EventHandler(this.btnSymbol2Selector_Click);
            this.btnSymbol1Selector.Location = new Point(0x90, 0x10);
            this.btnSymbol1Selector.Name = "btnSymbol1Selector";
            this.btnSymbol1Selector.Size = new Size(0x38, 0x18);
            this.btnSymbol1Selector.TabIndex = 0x1c;
            this.btnSymbol1Selector.Text = "符号1...";
            this.btnSymbol1Selector.Click += new EventHandler(this.btnSymbol1Selector_Click);
            this.txtBarSize.EditValue = "";
            this.txtBarSize.Location = new Point(0x38, 0x30);
            this.txtBarSize.Name = "txtBarSize";
            this.txtBarSize.Size = new Size(0x30, 0x15);
            this.txtBarSize.TabIndex = 0x1b;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x10, 0x30);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x23, 0x11);
            this.label5.TabIndex = 0x1a;
            this.label5.Text = "大小:";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x10, 0x18);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x23, 0x11);
            this.label4.TabIndex = 0x19;
            this.label4.Text = "颜色:";
            this.colorBar.EditValue = Color.Empty;
            this.colorBar.Location = new Point(0x38, 0x10);
            this.colorBar.Name = "colorBar";
            this.colorBar.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorBar.Size = new Size(0x30, 0x15);
            this.colorBar.TabIndex = 0x18;
            this.colorBar.EditValueChanged += new EventHandler(this.colorBar_EditValueChanged);
            this.imageList_1.ImageSize = new Size(10, 10);
            this.imageList_1.ImageStream = (ImageListStreamer)resources.GetObject("imageList2.ImageStream");
            this.imageList_1.TransparentColor = Color.Transparent;
            this.groupBox3.Controls.Add(this.btnStyleInfo);
            this.groupBox3.Controls.Add(this.btnStyleSelector);
            this.groupBox3.Controls.Add(this.cboStyle);
            this.groupBox3.Location = new Point(8, 0xd8);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0xe0, 0x48);
            this.groupBox3.TabIndex = 0x10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "样式";
            this.btnStyleInfo.ButtonStyle = BorderStyles.Simple;
            this.btnStyleInfo.ImageIndex = 1;
            this.btnStyleInfo.ImageList = this.imageList_1;
            this.btnStyleInfo.Location = new Point(0xc0, 40);
            this.btnStyleInfo.Name = "btnStyleInfo";
            this.btnStyleInfo.Size = new Size(0x10, 0x10);
            this.btnStyleInfo.TabIndex = 0x12;
            this.btnStyleSelector.ButtonStyle = BorderStyles.Simple;
            this.btnStyleSelector.ImageIndex = 0;
            this.btnStyleSelector.ImageList = this.imageList_1;
            this.btnStyleSelector.Location = new Point(0xc0, 0x18);
            this.btnStyleSelector.Name = "btnStyleSelector";
            this.btnStyleSelector.Size = new Size(0x10, 0x10);
            this.btnStyleSelector.TabIndex = 0x11;
            this.btnStyleSelector.Click += new EventHandler(this.btnStyleSelector_Click);
            this.cboStyle.DrawMode = DrawMode.OwnerDrawVariable;
            this.cboStyle.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboStyle.DropDownWidth = 160;
            this.cboStyle.Font = new System.Drawing.Font("宋体", 15f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.cboStyle.Location = new Point(8, 0x18);
            this.cboStyle.Name = "cboStyle";
            this.cboStyle.Size = new Size(0xbc, 0x1f);
            this.cboStyle.TabIndex = 0x10;
            this.cboStyle.SelectedIndexChanged += new EventHandler(this.cboStyle_SelectedIndexChanged);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "ScaleBarFormatPropertyPage";
            base.Size = new Size(0x108, 0x130);
            base.Load += new EventHandler(this.ScaleBarFormatPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.cboFontSize.Properties.EndInit();
            this.colorTextSymbol.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.txtBarSize.Properties.EndInit();
            this.colorBar.Properties.EndInit();
            this.groupBox3.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

        private void method_0()
        {
            if (m_pScaleBar != null)
            {
                this.method_3(this.colorBar, m_pScaleBar.BarColor);
                this.txtBarSize.Text = m_pScaleBar.BarHeight.ToString("0.##");
                this.method_1(m_pScaleBar.LabelSymbol);
            }
        }

        private void method_1(ITextSymbol itextSymbol_0)
        {
            this.method_3(this.colorTextSymbol, itextSymbol_0.Color);
            stdole.IFontDisp font = itextSymbol_0.Font;
            this.chkBold.Checked = font.Bold;
            this.chkItalic.Checked = font.Italic;
            this.chkUnderline.Checked = font.Underline;
            this.cboFontName.Text = font.Name;
            this.cboFontSize.Text = itextSymbol_0.Size.ToString("0.##");
        }

        private void method_2(ColorEdit colorEdit_0, IColor icolor_0)
        {
            icolor_0.NullColor = colorEdit_0.Color.IsEmpty;
            if (!colorEdit_0.Color.IsEmpty)
            {
                icolor_0.Transparency = colorEdit_0.Color.A;
                icolor_0.RGB = ColorManage.EsriRGB(colorEdit_0.Color.R, colorEdit_0.Color.G, colorEdit_0.Color.B);
            }
        }

        private void method_3(ColorEdit colorEdit_0, IColor icolor_0)
        {
            if (icolor_0.NullColor)
            {
                colorEdit_0.Color = Color.Empty;
            }
            else
            {
                int num2;
                int num3;
                int num4;
                ColorManage.GetEsriRGB((uint) icolor_0.RGB, out num2, out num3, out num4);
                colorEdit_0.Color = Color.FromArgb(icolor_0.Transparency, num2, num3, num4);
            }
        }

        private void method_4()
        {
            this.bool_1 = true;
            if (this.OnValueChange != null)
            {
                this.OnValueChange();
            }
        }

        private static void old_acctor_mc()
        {
            m_pScaleBar = null;
        }

        public void ResetControl()
        {
            this.bool_0 = false;
            this.method_0();
            this.bool_0 = true;
        }

        private void ScaleBarFormatPropertyPage_Load(object sender, EventArgs e)
        {
            IStyleGalleryItem item;
            this.cboFontName.Items.Clear();
            InstalledFontCollection fonts = new InstalledFontCollection();
            for (int i = 0; i < fonts.Families.Length; i++)
            {
                this.cboFontName.Items.Add(fonts.Families[i].Name);
            }
            if (this.istyleGallery_0 != null)
            {
                item = null;
                IEnumStyleGalleryItem item2 = this.istyleGallery_0.get_Items("Scale Bars", "", "");
                item2.Reset();
                item = item2.Next();
                while (item != null)
                {
                    this.cboStyle.Add(item);
                    item = item2.Next();
                }
                if (this.cboStyle.Items.Count > 0)
                {
                    this.cboStyle.SelectedIndex = 0;
                }
            }
            if (m_pScaleBar != null)
            {
                item = new MyStyleGalleryItem {
                    Name = "<定制>",
                    Item = m_pScaleBar
                };
                this.cboStyle.SelectStyleGalleryItem(item);
            }
            this.method_0();
            this.bool_0 = true;
        }

        public void SetObjects(object object_0)
        {
            this.imapSurroundFrame_0 = object_0 as IMapSurroundFrame;
            if (this.imapSurroundFrame_0 != null)
            {
                m_pScaleBar = (this.imapSurroundFrame_0.MapSurround as IClone).Clone() as IScaleBar;
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_1;
            }
        }

        int IPropertyPage.Height
        {
            get
            {
                return base.Height;
            }
        }

        int IPropertyPage.Width
        {
            get
            {
                return base.Width;
            }
        }

        public string Title
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }
    }
}

