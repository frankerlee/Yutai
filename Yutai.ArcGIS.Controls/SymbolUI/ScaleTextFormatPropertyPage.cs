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
    public class ScaleTextFormatPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private SimpleButton btnStyleInfo;
        private SimpleButton btnStyleSelector;
        private SimpleButton btnSymbolSelector;
        private System.Windows.Forms.ComboBox cboFontName;
        private ComboBoxEdit cboFontSize;
        private StyleComboBox cboStyle;
        private CheckBox chkBold;
        private CheckBox chkItalic;
        private CheckBox chkUnderline;
        private ColorEdit colorTextSymbol;
        private IContainer components;
        private GroupBox groupBox1;
        private GroupBox groupBox3;
        private ImageList imageList1;
        private ImageList imageList2;
        private Label label1;
        private Label label2;
        private Label label3;
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        internal static IScaleText m_pOldScaleText = null;
        internal static IScaleText m_pScaleText = null;
        private IStyleGallery m_pSG; //= ApplicationBase.StyleGallery;
        private string m_Title = "格式";
        private IAppContext _context;

        public event OnValueChangeEventHandler OnValueChange;

        public ScaleTextFormatPropertyPage()
        {
            this.InitializeComponent();
        }

        public ScaleTextFormatPropertyPage(IAppContext context)
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
                (m_pOldScaleText as IClone).Assign(m_pScaleText as IClone);
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
                    if (m_pScaleText != null)
                    {
                        selector.SetSymbol(m_pScaleText);
                    }
                    else
                    {
                        selector.SetSymbol(new ScaleTextClass());
                    }
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        m_pScaleText = selector.GetSymbol() as IScaleText;
                        this.Init();
                        IStyleGalleryItem styleGalleryItemAt = this.cboStyle.GetStyleGalleryItemAt(this.cboStyle.Items.Count - 1);
                        if (styleGalleryItemAt != null)
                        {
                            if (styleGalleryItemAt.Name == "<定制>")
                            {
                                styleGalleryItemAt.Item = m_pScaleText;
                            }
                            else
                            {
                                styleGalleryItemAt = new MyStyleGalleryItem {
                                    Name = "<定制>",
                                    Item = m_pScaleText
                                };
                                this.cboStyle.Add(styleGalleryItemAt);
                                this.cboStyle.SelectedIndex = this.cboStyle.Items.Count - 1;
                            }
                        }
                        else
                        {
                            styleGalleryItemAt = new MyStyleGalleryItem {
                                Name = "<定制>",
                                Item = m_pScaleText
                            };
                            this.cboStyle.Add(styleGalleryItemAt);
                            this.cboStyle.SelectedIndex = this.cboStyle.Items.Count - 1;
                        }
                        this.ValueChanged();
                        ScaleTextEventsClass.ScaleTextChage(this);
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
                ITextSymbol pSym = m_pScaleText.Symbol;
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.m_pSG);
                    selector.SetSymbol(pSym);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        pSym = selector.GetSymbol() as ITextSymbol;
                        m_pScaleText.Symbol = pSym;
                        this.SetTextSymbol(pSym);
                        ScaleTextEventsClass.ScaleTextChage(this);
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
                ITextSymbol symbol = m_pScaleText.Symbol;
                stdole.IFontDisp font = symbol.Font;
                if (font.Name != this.cboFontName.Name)
                {
                    font.Name = this.cboFontName.Name;
                    symbol.Font = font;
                    m_pScaleText.Symbol = symbol;
                    ScaleTextEventsClass.ScaleTextChage(this);
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
                    ITextSymbol symbol = m_pScaleText.Symbol;
                    if (symbol.Size != num)
                    {
                        symbol.Size = double.Parse(this.cboFontSize.Text);
                        m_pScaleText.Symbol = symbol;
                        ScaleTextEventsClass.ScaleTextChage(this);
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
                    m_pScaleText = null;
                }
                else
                {
                    m_pScaleText = (selectStyleGalleryItem.Item as IClone).Clone() as IScaleText;
                }
                this.m_CanDo = false;
                this.Init();
                this.m_CanDo = true;
                this.ValueChanged();
                ScaleTextEventsClass.ScaleTextChage(this);
            }
        }

        private void chkBold_Click(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                ITextSymbol symbol = m_pScaleText.Symbol;
                stdole.IFontDisp font = symbol.Font;
                if (font.Bold != this.chkBold.Checked)
                {
                    font.Bold = this.chkBold.Checked;
                    symbol.Font = font;
                    m_pScaleText.Symbol = symbol;
                    ScaleTextEventsClass.ScaleTextChage(this);
                    this.ValueChanged();
                }
            }
        }

        private void chkItalic_Click(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                ITextSymbol symbol = m_pScaleText.Symbol;
                stdole.IFontDisp font = symbol.Font;
                if (font.Italic != this.chkItalic.Checked)
                {
                    font.Italic = this.chkItalic.Checked;
                    symbol.Font = font;
                    m_pScaleText.Symbol = symbol;
                    ScaleTextEventsClass.ScaleTextChage(this);
                    this.ValueChanged();
                }
            }
        }

        private void chkUnderline_Click(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                ITextSymbol symbol = m_pScaleText.Symbol;
                stdole.IFontDisp font = symbol.Font;
                if (font.Underline != this.chkUnderline.Checked)
                {
                    font.Underline = this.chkUnderline.Checked;
                    symbol.Font = font;
                    m_pScaleText.Symbol = symbol;
                    ScaleTextEventsClass.ScaleTextChage(this);
                    this.ValueChanged();
                }
            }
        }

        private void colorTextSymbol_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                ITextSymbol symbol = m_pScaleText.Symbol;
                IColor pColor = symbol.Color;
                this.UpdateColorFromColorEdit(this.colorTextSymbol, pColor);
                symbol.Color = pColor;
                m_pScaleText.Symbol = symbol;
                ScaleTextEventsClass.ScaleTextChage(this);
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

        ~ScaleTextFormatPropertyPage()
        {
            m_pScaleText = null;
            m_pOldScaleText = null;
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
            if (m_pScaleText != null)
            {
                this.SetTextSymbol(m_pScaleText.Symbol);
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ScaleTextFormatPropertyPage));
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
            this.imageList2 = new ImageList(this.components);
            this.groupBox3 = new GroupBox();
            this.btnStyleInfo = new SimpleButton();
            this.btnStyleSelector = new SimpleButton();
            this.cboStyle = new StyleComboBox(this.components);
            this.groupBox1.SuspendLayout();
            this.cboFontSize.Properties.BeginInit();
            this.colorTextSymbol.Properties.BeginInit();
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
            this.imageList2.ImageSize = new Size(10, 10);
            this.imageList2.ImageStream = (ImageListStreamer) resources.GetObject("imageList2.ImageStream");
            this.imageList2.TransparentColor = Color.Transparent;
            this.groupBox3.Controls.Add(this.btnStyleInfo);
            this.groupBox3.Controls.Add(this.btnStyleSelector);
            this.groupBox3.Controls.Add(this.cboStyle);
            this.groupBox3.Location = new Point(8, 0x80);
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
            base.Controls.Add(this.groupBox1);
            base.Name = "ScaleTextFormatPropertyPage";
            base.Size = new Size(0x108, 0xe0);
            base.Load += new EventHandler(this.ScaleTextFormatPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.cboFontSize.Properties.EndInit();
            this.colorTextSymbol.Properties.EndInit();
            this.groupBox3.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void ScaleTextFormatPropertyPage_Load(object sender, EventArgs e)
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
                IEnumStyleGalleryItem item2 = this.m_pSG.get_Items("Scale Texts", "", "");
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
            if (m_pScaleText != null)
            {
                item = new MyStyleGalleryItem {
                    Name = "<定制>",
                    Item = m_pScaleText
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
            if (m_pOldScaleText == null)
            {
                m_pOldScaleText = @object as IScaleText;
                if (m_pOldScaleText != null)
                {
                    m_pScaleText = (m_pOldScaleText as IClone).Clone() as IScaleText;
                }
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
            this.cboFontSize.Text = font.Size.ToString();
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

