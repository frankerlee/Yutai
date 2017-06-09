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
    public class ScaleTextFormatPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
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
        private GroupBox groupBox1;
        private GroupBox groupBox3;
        private IContainer icontainer_0;
        private ImageList imageList_0;
        private ImageList imageList_1;
        private IMapSurroundFrame imapSurroundFrame_0 = null;
        private IStyleGallery istyleGallery_0 = ApplicationBase.StyleGallery;
        private Label label1;
        private Label label2;
        private Label label3;
        internal static IScaleText m_pScaleText;
        private string string_0 = "格式";

        public event OnValueChangeEventHandler OnValueChange;

        static ScaleTextFormatPropertyPage()
        {
            old_acctor_mc();
        }

        public ScaleTextFormatPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                this.imapSurroundFrame_0.MapSurround = (m_pScaleText as IClone).Clone() as IMapSurround;
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
                        this.method_0();
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
                        this.method_4();
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
                    selector.SetStyleGallery(this.istyleGallery_0);
                    selector.SetSymbol(pSym);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        pSym = selector.GetSymbol() as ITextSymbol;
                        m_pScaleText.Symbol = pSym;
                        this.method_1(pSym);
                        ScaleTextEventsClass.ScaleTextChage(this);
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
                ITextSymbol symbol = m_pScaleText.Symbol;
                stdole.IFontDisp font = symbol.Font;
                if (font.Name != this.cboFontName.Name)
                {
                    font.Name = this.cboFontName.Name;
                    symbol.Font = font;
                    m_pScaleText.Symbol = symbol;
                    ScaleTextEventsClass.ScaleTextChage(this);
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
                    ITextSymbol symbol = m_pScaleText.Symbol;
                    if (!(symbol.Size == num))
                    {
                        symbol.Size = double.Parse(this.cboFontSize.Text);
                        m_pScaleText.Symbol = symbol;
                        ScaleTextEventsClass.ScaleTextChage(this);
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
                    m_pScaleText = null;
                }
                else
                {
                    m_pScaleText = (selectStyleGalleryItem.Item as IClone).Clone() as IScaleText;
                }
                this.bool_0 = false;
                this.method_0();
                this.bool_0 = true;
                this.method_4();
                ScaleTextEventsClass.ScaleTextChage(this);
            }
        }

        private void chkBold_Click(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                ITextSymbol symbol = m_pScaleText.Symbol;
                stdole.IFontDisp font = symbol.Font;
                if (font.Bold != this.chkBold.Checked)
                {
                    font.Bold = this.chkBold.Checked;
                    symbol.Font = font;
                    m_pScaleText.Symbol = symbol;
                    ScaleTextEventsClass.ScaleTextChage(this);
                    this.method_4();
                }
            }
        }

        private void chkItalic_Click(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                ITextSymbol symbol = m_pScaleText.Symbol;
                stdole.IFontDisp font = symbol.Font;
                if (font.Italic != this.chkItalic.Checked)
                {
                    font.Italic = this.chkItalic.Checked;
                    symbol.Font = font;
                    m_pScaleText.Symbol = symbol;
                    ScaleTextEventsClass.ScaleTextChage(this);
                    this.method_4();
                }
            }
        }

        private void chkUnderline_Click(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                ITextSymbol symbol = m_pScaleText.Symbol;
                stdole.IFontDisp font = symbol.Font;
                if (font.Underline != this.chkUnderline.Checked)
                {
                    font.Underline = this.chkUnderline.Checked;
                    symbol.Font = font;
                    m_pScaleText.Symbol = symbol;
                    ScaleTextEventsClass.ScaleTextChage(this);
                    this.method_4();
                }
            }
        }

        private void colorTextSymbol_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                ITextSymbol symbol = m_pScaleText.Symbol;
                IColor color = symbol.Color;
                this.method_2(this.colorTextSymbol, color);
                symbol.Color = color;
                m_pScaleText.Symbol = symbol;
                ScaleTextEventsClass.ScaleTextChage(this);
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
            System.ComponentModel.ComponentResourceManager  resources = new System.ComponentModel.ComponentResourceManager(typeof(ScaleTextFormatPropertyPage));
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
            this.imageList_1 = new ImageList(this.icontainer_0);
            this.groupBox3 = new GroupBox();
            this.btnStyleInfo = new SimpleButton();
            this.btnStyleSelector = new SimpleButton();
            this.cboStyle = new StyleComboBox(this.icontainer_0);
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
            this.imageList_1.ImageSize = new Size(10, 10);
            this.imageList_1.ImageStream = (ImageListStreamer)resources.GetObject("imageList2.ImageStream");
            this.imageList_1.TransparentColor = Color.Transparent;
            this.groupBox3.Controls.Add(this.btnStyleInfo);
            this.groupBox3.Controls.Add(this.btnStyleSelector);
            this.groupBox3.Controls.Add(this.cboStyle);
            this.groupBox3.Location = new Point(8, 0x80);
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

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

        private void method_0()
        {
            if (m_pScaleText != null)
            {
                this.method_1(m_pScaleText.Symbol);
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
            this.cboFontSize.Text = font.Size.ToString();
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
            m_pScaleText = null;
        }

        public void ResetControl()
        {
            this.bool_0 = false;
            this.method_0();
            this.bool_0 = true;
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
            if (this.istyleGallery_0 != null)
            {
                item = null;
                IEnumStyleGalleryItem item2 = this.istyleGallery_0.get_Items("Scale Texts", "", "");
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
            this.method_0();
            this.bool_0 = true;
        }

        public void SetObjects(object object_0)
        {
            this.imapSurroundFrame_0 = object_0 as IMapSurroundFrame;
            if ((this.imapSurroundFrame_0 != null) && (m_pScaleText == null))
            {
                m_pScaleText = (this.imapSurroundFrame_0.MapSurround as IClone).Clone() as IScaleText;
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

