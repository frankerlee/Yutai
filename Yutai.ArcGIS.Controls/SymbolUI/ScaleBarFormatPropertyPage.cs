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
using Yutai.ArcGIS.Common.Symbol;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.Plugins.Interfaces;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    public class ScaleBarFormatPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
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
        private IContainer components;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private ImageList imageList1;
        private ImageList imageList2;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        internal static IScaleBar m_pOldScaleBar = null;
        internal static IScaleBar m_pScaleBar = null;
        private IStyleGallery m_pSG;//= ApplicationBase.StyleGallery;
        private string m_Title = "格式";
        private TextEdit txtBarSize;

        public event OnValueChangeEventHandler OnValueChange;

        public ScaleBarFormatPropertyPage()
        {
            this.InitializeComponent();
        }

        private IAppContext _context;
        public ScaleBarFormatPropertyPage(IAppContext context)
        {
            this.InitializeComponent();
            _context = context;
            this.m_pSG = _context.StyleGallery;
        }


        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
                (m_pOldScaleBar as IClone).Assign(m_pScaleBar as IClone);
            }
        }

        private void btnStyleSelector_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.m_pSG);
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
                        this.Init();
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
                        this.ValueChanged();
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
                        selector.SetStyleGallery(this.m_pSG);
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
                            this.ValueChanged();
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
                        selector.SetStyleGallery(this.m_pSG);
                        selector.SetSymbol(pSym);
                        if (selector.ShowDialog() == DialogResult.OK)
                        {
                            (m_pScaleBar as IDoubleFillScaleBar).FillSymbol2 = selector.GetSymbol() as IFillSymbol;
                            this.ValueChanged();
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
                    selector.SetStyleGallery(this.m_pSG);
                    selector.SetSymbol(labelSymbol);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        labelSymbol = selector.GetSymbol() as ITextSymbol;
                        m_pScaleBar.LabelSymbol = labelSymbol;
                        this.m_CanDo = false;
                        this.SetTextSymbol(labelSymbol);
                        this.m_CanDo = true;
                        this.ValueChanged();
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
            if (this.m_CanDo)
            {
                ITextSymbol labelSymbol = m_pScaleBar.LabelSymbol;
                stdole.IFontDisp font = labelSymbol.Font;
                if (font.Name != this.cboFontName.Name)
                {
                    font.Name = this.cboFontName.Name;
                    labelSymbol.Font = font;
                    m_pScaleBar.LabelSymbol = labelSymbol;
                    this.ValueChanged();
                }
            }
        }

        private void cboFontSize_EditValueChanging(object sender, ChangingEventArgs e)
        {
            if (this.m_CanDo)
            {
                try
                {
                    double num = double.Parse(this.cboFontSize.Text);
                    ITextSymbol labelSymbol = m_pScaleBar.LabelSymbol;
                    if (labelSymbol.Size != num)
                    {
                        labelSymbol.Size = double.Parse(this.cboFontSize.Text);
                        m_pScaleBar.LabelSymbol = labelSymbol;
                        this.ValueChanged();
                    }
                }
                catch
                {
                }
            }
        }

        private void cboStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
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
                this.m_CanDo = false;
                this.Init();
                this.m_CanDo = true;
                this.ValueChanged();
            }
        }

        private void chkBold_Click(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                ITextSymbol labelSymbol = m_pScaleBar.LabelSymbol;
                stdole.IFontDisp font = labelSymbol.Font;
                if (font.Bold != this.chkBold.Checked)
                {
                    font.Bold = this.chkBold.Checked;
                    labelSymbol.Font = font;
                    m_pScaleBar.LabelSymbol = labelSymbol;
                    this.ValueChanged();
                }
            }
        }

        private void chkItalic_Click(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                ITextSymbol labelSymbol = m_pScaleBar.LabelSymbol;
                stdole.IFontDisp font = labelSymbol.Font;
                if (font.Italic != this.chkItalic.Checked)
                {
                    font.Italic = this.chkItalic.Checked;
                    labelSymbol.Font = font;
                    m_pScaleBar.LabelSymbol = labelSymbol;
                    this.ValueChanged();
                }
            }
        }

        private void chkUnderline_Click(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                ITextSymbol labelSymbol = m_pScaleBar.LabelSymbol;
                stdole.IFontDisp font = labelSymbol.Font;
                if (font.Underline != this.chkUnderline.Checked)
                {
                    font.Underline = this.chkUnderline.Checked;
                    labelSymbol.Font = font;
                    m_pScaleBar.LabelSymbol = labelSymbol;
                    this.ValueChanged();
                }
            }
        }

        private void colorBar_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IColor barColor = m_pScaleBar.BarColor;
                this.UpdateColorFromColorEdit(this.colorBar, barColor);
                m_pScaleBar.BarColor = barColor;
                this.ValueChanged();
            }
        }

        private void colorTextSymbol_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                ITextSymbol labelSymbol = m_pScaleBar.LabelSymbol;
                IColor pColor = labelSymbol.Color;
                this.UpdateColorFromColorEdit(this.colorTextSymbol, pColor);
                labelSymbol.Color = pColor;
                m_pScaleBar.LabelSymbol = labelSymbol;
                this.ValueChanged();
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

        public static int EsriRGB(int r, int g, int b)
        {
            uint num = 0;
            num |= (uint) b;
            num = num << 8;
            num |= (uint) g;
            num = num << 8;
            num |= (uint) r;
            return (int) num;
        }

        public static void GetEsriRGB(uint rgb, out int r, out int g, out int b)
        {
            uint num = rgb & 0xff0000;
            b = (int) (num >> 0x10);
            num = rgb & 0xff00;
            g = (int) (num >> 8);
            num = rgb & 0xff;
            r = (int) num;
        }

        public void Hide()
        {
        }

        private void Init()
        {
            if (m_pScaleBar != null)
            {
                this.SetColorEdit(this.colorBar, m_pScaleBar.BarColor);
                this.txtBarSize.Text = m_pScaleBar.BarHeight.ToString("0.##");
                this.SetTextSymbol(m_pScaleBar.LabelSymbol);
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ScaleBarFormatPropertyPage));
            this.groupBox1 = new GroupBox();
            this.btnSymbolSelector = new SimpleButton();
            this.label3 = new Label();
            this.label2 = new Label();
            this.label1 = new Label();
            this.cboFontSize = new ComboBoxEdit();
            this.colorTextSymbol = new ColorEdit();
            this.chkUnderline = new CheckBox();
            this.imageList1 = new ImageList(this.components);
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
            this.imageList2 = new ImageList(this.components);
            this.groupBox3 = new GroupBox();
            this.btnStyleInfo = new SimpleButton();
            this.btnStyleSelector = new SimpleButton();
            this.cboStyle = new StyleComboBox(this.components);
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
            this.cboFontSize.Size = new Size(0x40, 0x17);
            this.cboFontSize.TabIndex = 20;
            this.cboFontSize.EditValueChanging += new ChangingEventHandler(this.cboFontSize_EditValueChanging);
            this.colorTextSymbol.EditValue = Color.Empty;
            this.colorTextSymbol.Location = new Point(0x30, 0x48);
            this.colorTextSymbol.Name = "colorTextSymbol";
            this.colorTextSymbol.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorTextSymbol.Size = new Size(0x30, 0x17);
            this.colorTextSymbol.TabIndex = 0x13;
            this.colorTextSymbol.EditValueChanged += new EventHandler(this.colorTextSymbol_EditValueChanged);
            this.chkUnderline.Appearance = Appearance.Button;
            this.chkUnderline.ImageIndex = 2;
            this.chkUnderline.ImageList = this.imageList1;
            this.chkUnderline.Location = new Point(0xb8, 40);
            this.chkUnderline.Name = "chkUnderline";
            this.chkUnderline.Size = new Size(0x1c, 0x18);
            this.chkUnderline.TabIndex = 0x12;
            this.chkUnderline.Click += new EventHandler(this.chkUnderline_Click);
            this.imageList1.ImageSize = new Size(0x10, 0x10);
            this.imageList1.ImageStream = (ImageListStreamer) resources.GetObject("imageList1.ImageStream");
            this.imageList1.TransparentColor = Color.Magenta;
            this.chkItalic.Appearance = Appearance.Button;
            this.chkItalic.ImageIndex = 1;
            this.chkItalic.ImageList = this.imageList1;
            this.chkItalic.Location = new Point(0x9c, 40);
            this.chkItalic.Name = "chkItalic";
            this.chkItalic.Size = new Size(0x1c, 0x18);
            this.chkItalic.TabIndex = 0x11;
            this.chkItalic.Click += new EventHandler(this.chkItalic_Click);
            this.chkBold.Appearance = Appearance.Button;
            this.chkBold.ImageIndex = 0;
            this.chkBold.ImageList = this.imageList1;
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
            this.txtBarSize.Size = new Size(0x30, 0x17);
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
            this.colorBar.Size = new Size(0x30, 0x17);
            this.colorBar.TabIndex = 0x18;
            this.colorBar.EditValueChanged += new EventHandler(this.colorBar_EditValueChanged);
            this.imageList2.ImageSize = new Size(10, 10);
            this.imageList2.ImageStream = (ImageListStreamer) resources.GetObject("imageList2.ImageStream");
            this.imageList2.TransparentColor = Color.Transparent;
            this.groupBox3.Controls.Add(this.btnStyleInfo);
            this.groupBox3.Controls.Add(this.btnStyleSelector);
            this.groupBox3.Controls.Add(this.cboStyle);
            this.groupBox3.Location = new Point(8, 0xd8);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0xe0, 0x48);
            this.groupBox3.TabIndex = 0x10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "样式";
            this.btnStyleInfo.Appearance.BackColor = SystemColors.Window;
            this.btnStyleInfo.Appearance.ForeColor = SystemColors.ControlLightLight;
            this.btnStyleInfo.Appearance.Options.UseBackColor = true;
            this.btnStyleInfo.Appearance.Options.UseForeColor = true;
            this.btnStyleInfo.ButtonStyle = BorderStyles.Simple;
            this.btnStyleInfo.ImageIndex = 1;
            this.btnStyleInfo.ImageList = this.imageList2;
            this.btnStyleInfo.Location = new Point(0xc0, 40);
            this.btnStyleInfo.Name = "btnStyleInfo";
            this.btnStyleInfo.Size = new Size(0x10, 0x10);
            this.btnStyleInfo.TabIndex = 0x12;
            this.btnStyleInfo.Visible = false;
            this.btnStyleSelector.Appearance.BackColor = SystemColors.Window;
            this.btnStyleSelector.Appearance.ForeColor = SystemColors.ControlLightLight;
            this.btnStyleSelector.Appearance.Options.UseBackColor = true;
            this.btnStyleSelector.Appearance.Options.UseForeColor = true;
            this.btnStyleSelector.ButtonStyle = BorderStyles.Simple;
            this.btnStyleSelector.ImageIndex = 0;
            this.btnStyleSelector.ImageList = this.imageList2;
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

        private void ScaleBarFormatPropertyPage_Load(object sender, EventArgs e)
        {
            IStyleGalleryItem item;
            this.cboFontName.Items.Clear();
            InstalledFontCollection fonts = new InstalledFontCollection();
            for (int i = 0; i < fonts.Families.Length; i++)
            {
                this.cboFontName.Items.Add(fonts.Families[i].Name);
            }
            if (this.m_pSG != null)
            {
                item = null;
                IEnumStyleGalleryItem item2 = this.m_pSG.get_Items("Scale Bars", "", "");
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
            this.Init();
            this.m_CanDo = true;
        }

        private void SetColorEdit(ColorEdit colorEdit, IColor pColor)
        {
            if (pColor.NullColor)
            {
                colorEdit.Color = Color.Empty;
            }
            else
            {
                int num;
                int num2;
                int num3;
                GetEsriRGB((uint) pColor.RGB, out num, out num2, out num3);
                colorEdit.Color = Color.FromArgb(pColor.Transparency, num, num2, num3);
            }
        }

        public void SetObjects(object @object)
        {
            m_pOldScaleBar = @object as IScaleBar;
            if (m_pOldScaleBar != null)
            {
                m_pScaleBar = (m_pOldScaleBar as IClone).Clone() as IScaleBar;
            }
        }

        private void SetTextSymbol(ITextSymbol pTextSymbol)
        {
            this.SetColorEdit(this.colorTextSymbol, pTextSymbol.Color);
            stdole.IFontDisp font = pTextSymbol.Font;
            this.chkBold.Checked = font.Bold;
            this.chkItalic.Checked = font.Italic;
            this.chkUnderline.Checked = font.Underline;
            this.cboFontName.Text = font.Name;
            this.cboFontSize.Text = pTextSymbol.Size.ToString("0.##");
        }

        private void UpdateColorFromColorEdit(ColorEdit colorEdit, IColor pColor)
        {
            pColor.NullColor = colorEdit.Color.IsEmpty;
            if (!colorEdit.Color.IsEmpty)
            {
                pColor.Transparency = colorEdit.Color.A;
                pColor.RGB = EsriRGB(colorEdit.Color.R, colorEdit.Color.G, colorEdit.Color.B);
            }
        }

        private void ValueChanged()
        {
            this.m_IsPageDirty = true;
            if (this.OnValueChange != null)
            {
                this.OnValueChange();
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.m_IsPageDirty;
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
                return this.m_Title;
            }
            set
            {
                this.m_Title = value;
            }
        }
    }
}

