using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Shared;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    internal class AnnoEditControl : UserControl
    {
        private SimpleButton btnApply;
        private SimpleButton btnReset;
        private System.Windows.Forms.ComboBox cboFontName;
        private ComboBoxEdit cboFontSize;
        private CheckBox chkBold;
        private CheckBox chkItalic;
        private CheckBox chkUnderline;
        private ColorEdit colorEdit1;
        private IContainer components;
        private GroupBox groupBox1;
        private ImageList imageList1;
        private bool m_CanDo = false;
        private IActiveView m_pActiveView = null;
        private IAnnotationFeature m_pAnnoFeat = null;
        private ITextElement m_pTextElement = null;
        private MemoEdit memoEdit1;
        private RadioButton rdoTHACenter;
        private RadioButton rdoTHAFul;
        private RadioButton rdoTHALeft;
        private RadioButton rdoTHARight;

        public AnnoEditControl()
        {
            this.InitializeComponent();
            this.cboFontName.Items.Clear();
            InstalledFontCollection fonts = new InstalledFontCollection();
            for (int i = 0; i < fonts.Families.Length; i++)
            {
                this.cboFontName.Items.Add(fonts.Families[i].Name);
            }
        }

        private void AnnoEditControl_Load(object sender, EventArgs e)
        {
            this.Init();
            this.m_CanDo = true;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (this.m_pAnnoFeat != null)
            {
                try
                {
                    IDataset dataset = (this.m_pAnnoFeat as IObject).Class as IDataset;
                    IWorkspaceEdit workspace = dataset.Workspace as IWorkspaceEdit;
                    workspace.StartEditOperation();
                    this.m_pAnnoFeat.Annotation = this.m_pTextElement as IElement;
                    (this.m_pAnnoFeat as IFeature).Store();
                    workspace.StopEditOperation();
                    this.m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                }
                catch (Exception exception)
                {
                   Logger.Current.Error("", exception, "");
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.m_pTextElement = this.m_pAnnoFeat.Annotation as ITextElement;
            this.Init();
        }

        private void cboFontName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                (this.m_pTextElement as ISymbolCollectionElement).FontName = this.cboFontName.Text;
            }
        }

        private void cboFontSize_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                try
                {
                    (this.m_pTextElement as ISymbolCollectionElement).Size = double.Parse(this.cboFontSize.Text);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private void cboFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void chkBold_Click(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                (this.m_pTextElement as ISymbolCollectionElement).Bold = this.chkBold.Checked;
            }
        }

        private void chkItalic_Click(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                (this.m_pTextElement as ISymbolCollectionElement).Italic = this.chkItalic.Checked;
            }
        }

        private void chkUnderline_Click(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                (this.m_pTextElement as ISymbolCollectionElement).Underline = this.chkUnderline.Checked;
            }
        }

        private void colorEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IColor pColor = (this.m_pTextElement as ISymbolCollectionElement).Color;
                this.UpdateColorFromColorEdit(this.colorEdit1, pColor);
                (this.m_pTextElement as ISymbolCollectionElement).Color = pColor;
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

        private void GetRGB(uint rgb, out int r, out int g, out int b)
        {
            uint num = rgb & 0xff0000;
            b = (int) (num >> 0x10);
            num = rgb & 0xff00;
            g = (int) (num >> 8);
            num = rgb & 0xff;
            r = (int) num;
        }

        private void Init()
        {
            this.memoEdit1.Text = (this.m_pTextElement as ISymbolCollectionElement).Text;
            this.SetColorEdit(this.colorEdit1, (this.m_pTextElement as ISymbolCollectionElement).Color);
            this.chkBold.Checked = (this.m_pTextElement as ISymbolCollectionElement).Bold;
            this.chkItalic.Checked = (this.m_pTextElement as ISymbolCollectionElement).Italic;
            this.chkUnderline.Checked = (this.m_pTextElement as ISymbolCollectionElement).Underline;
            switch ((this.m_pTextElement as ISymbolCollectionElement).HorizontalAlignment)
            {
                case esriTextHorizontalAlignment.esriTHALeft:
                    this.rdoTHALeft.Checked = true;
                    break;

                case esriTextHorizontalAlignment.esriTHACenter:
                    this.rdoTHACenter.Checked = true;
                    break;

                case esriTextHorizontalAlignment.esriTHARight:
                    this.rdoTHARight.Checked = true;
                    break;

                case esriTextHorizontalAlignment.esriTHAFull:
                    this.rdoTHAFul.Checked = true;
                    break;
            }
            this.cboFontName.Text = (this.m_pTextElement as ISymbolCollectionElement).FontName;
            this.cboFontSize.Text = (this.m_pTextElement as ISymbolCollectionElement).Size.ToString();
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnnoEditControl));
            this.memoEdit1 = new MemoEdit();
            this.groupBox1 = new GroupBox();
            this.cboFontSize = new ComboBoxEdit();
            this.colorEdit1 = new ColorEdit();
            this.rdoTHAFul = new RadioButton();
            this.imageList1 = new ImageList(this.components);
            this.rdoTHALeft = new RadioButton();
            this.rdoTHACenter = new RadioButton();
            this.rdoTHARight = new RadioButton();
            this.chkUnderline = new CheckBox();
            this.chkItalic = new CheckBox();
            this.chkBold = new CheckBox();
            this.cboFontName = new System.Windows.Forms.ComboBox();
            this.btnApply = new SimpleButton();
            this.btnReset = new SimpleButton();
            this.memoEdit1.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            this.cboFontSize.Properties.BeginInit();
            this.colorEdit1.Properties.BeginInit();
            base.SuspendLayout();
            this.memoEdit1.EditValue = "";
            this.memoEdit1.Location = new Point(8, 8);
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Size = new Size(0xe8, 0x58);
            this.memoEdit1.TabIndex = 0;
            this.memoEdit1.EditValueChanged += new EventHandler(this.memoEdit1_EditValueChanged);
            this.groupBox1.Controls.Add(this.cboFontSize);
            this.groupBox1.Controls.Add(this.colorEdit1);
            this.groupBox1.Controls.Add(this.rdoTHAFul);
            this.groupBox1.Controls.Add(this.rdoTHALeft);
            this.groupBox1.Controls.Add(this.rdoTHACenter);
            this.groupBox1.Controls.Add(this.rdoTHARight);
            this.groupBox1.Controls.Add(this.chkUnderline);
            this.groupBox1.Controls.Add(this.chkItalic);
            this.groupBox1.Controls.Add(this.chkBold);
            this.groupBox1.Controls.Add(this.cboFontName);
            this.groupBox1.Location = new Point(8, 0x68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(280, 0x58);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.cboFontSize.EditValue = "5";
            this.cboFontSize.Location = new Point(200, 0x11);
            this.cboFontSize.Name = "cboFontSize";
            this.cboFontSize.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboFontSize.Properties.Items.AddRange(new object[] { 
                "5", "6", "7", "8", "9", "10", "11", "12", "14", "16", "18", "20", "22", "24", "26", "28", 
                "34", "36", "48", "72"
             });
            this.cboFontSize.Size = new Size(0x40, 0x15);
            this.cboFontSize.TabIndex = 14;
            this.cboFontSize.EditValueChanged += new EventHandler(this.cboFontSize_EditValueChanged);
            this.cboFontSize.SelectedIndexChanged += new EventHandler(this.cboFontSize_SelectedIndexChanged);
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new Point(8, 0x30);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(0x30, 0x15);
            this.colorEdit1.TabIndex = 13;
            this.colorEdit1.EditValueChanged += new EventHandler(this.colorEdit1_EditValueChanged);
            this.rdoTHAFul.Appearance = Appearance.Button;
            this.rdoTHAFul.ImageIndex = 6;
            this.rdoTHAFul.ImageList = this.imageList1;
            this.rdoTHAFul.Location = new Point(0xf4, 0x2d);
            this.rdoTHAFul.Name = "rdoTHAFul";
            this.rdoTHAFul.Size = new Size(0x1c, 0x18);
            this.rdoTHAFul.TabIndex = 11;
            this.rdoTHAFul.Click += new EventHandler(this.rdoTHAFul_Click);
            this.imageList1.ImageStream = (ImageListStreamer) resources.GetObject("imageList1.ImageStream");
            this.imageList1.TransparentColor = Color.Magenta;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            this.imageList1.Images.SetKeyName(4, "");
            this.imageList1.Images.SetKeyName(5, "");
            this.imageList1.Images.SetKeyName(6, "");
            this.rdoTHALeft.Appearance = Appearance.Button;
            this.rdoTHALeft.ImageIndex = 3;
            this.rdoTHALeft.ImageList = this.imageList1;
            this.rdoTHALeft.Location = new Point(160, 0x2d);
            this.rdoTHALeft.Name = "rdoTHALeft";
            this.rdoTHALeft.Size = new Size(0x1c, 0x18);
            this.rdoTHALeft.TabIndex = 10;
            this.rdoTHALeft.Click += new EventHandler(this.rdoTHALeft_Click);
            this.rdoTHACenter.Appearance = Appearance.Button;
            this.rdoTHACenter.ImageIndex = 4;
            this.rdoTHACenter.ImageList = this.imageList1;
            this.rdoTHACenter.Location = new Point(0xbc, 0x2d);
            this.rdoTHACenter.Name = "rdoTHACenter";
            this.rdoTHACenter.Size = new Size(0x1c, 0x18);
            this.rdoTHACenter.TabIndex = 9;
            this.rdoTHACenter.Click += new EventHandler(this.rdoTHACenter_Click);
            this.rdoTHARight.Appearance = Appearance.Button;
            this.rdoTHARight.ImageIndex = 5;
            this.rdoTHARight.ImageList = this.imageList1;
            this.rdoTHARight.Location = new Point(0xd8, 0x2d);
            this.rdoTHARight.Name = "rdoTHARight";
            this.rdoTHARight.Size = new Size(0x1c, 0x18);
            this.rdoTHARight.TabIndex = 8;
            this.rdoTHARight.TabStop = true;
            this.rdoTHARight.Click += new EventHandler(this.rdoTHARight_Click);
            this.chkUnderline.Appearance = Appearance.Button;
            this.chkUnderline.ImageIndex = 2;
            this.chkUnderline.ImageList = this.imageList1;
            this.chkUnderline.Location = new Point(120, 0x2d);
            this.chkUnderline.Name = "chkUnderline";
            this.chkUnderline.Size = new Size(0x1c, 0x18);
            this.chkUnderline.TabIndex = 7;
            this.chkUnderline.Click += new EventHandler(this.chkUnderline_Click);
            this.chkItalic.Appearance = Appearance.Button;
            this.chkItalic.ImageIndex = 1;
            this.chkItalic.ImageList = this.imageList1;
            this.chkItalic.Location = new Point(0x5c, 0x2d);
            this.chkItalic.Name = "chkItalic";
            this.chkItalic.Size = new Size(0x1c, 0x18);
            this.chkItalic.TabIndex = 6;
            this.chkItalic.Click += new EventHandler(this.chkItalic_Click);
            this.chkBold.Appearance = Appearance.Button;
            this.chkBold.ImageIndex = 0;
            this.chkBold.ImageList = this.imageList1;
            this.chkBold.Location = new Point(0x40, 0x2d);
            this.chkBold.Name = "chkBold";
            this.chkBold.Size = new Size(0x1c, 0x18);
            this.chkBold.TabIndex = 5;
            this.chkBold.Click += new EventHandler(this.chkBold_Click);
            this.cboFontName.Location = new Point(8, 0x10);
            this.cboFontName.Name = "cboFontName";
            this.cboFontName.Size = new Size(0xb8, 20);
            this.cboFontName.TabIndex = 2;
            this.cboFontName.SelectedIndexChanged += new EventHandler(this.cboFontName_SelectedIndexChanged);
            this.btnApply.Location = new Point(0x100, 0x10);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new Size(0x30, 0x18);
            this.btnApply.TabIndex = 2;
            this.btnApply.Text = "应用";
            this.btnApply.Click += new EventHandler(this.btnApply_Click);
            this.btnReset.Location = new Point(0x100, 0x30);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new Size(0x30, 0x18);
            this.btnReset.TabIndex = 3;
            this.btnReset.Text = "重置";
            this.btnReset.Click += new EventHandler(this.btnReset_Click);
            base.Controls.Add(this.btnReset);
            base.Controls.Add(this.btnApply);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.memoEdit1);
            base.Name = "AnnoEditControl";
            base.Size = new Size(0x138, 0xd0);
            base.Load += new EventHandler(this.AnnoEditControl_Load);
            this.memoEdit1.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.cboFontSize.Properties.EndInit();
            this.colorEdit1.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void memoEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_pTextElement.Text = this.memoEdit1.Text;
            }
        }

        private void rdoTHACenter_Click(object sender, EventArgs e)
        {
            if (this.m_CanDo && this.rdoTHACenter.Checked)
            {
                (this.m_pTextElement as ISymbolCollectionElement).HorizontalAlignment = esriTextHorizontalAlignment.esriTHACenter;
            }
        }

        private void rdoTHAFul_Click(object sender, EventArgs e)
        {
            if (this.m_CanDo && this.rdoTHAFul.Checked)
            {
                (this.m_pTextElement as ISymbolCollectionElement).HorizontalAlignment = esriTextHorizontalAlignment.esriTHAFull;
            }
        }

        private void rdoTHALeft_Click(object sender, EventArgs e)
        {
            if (this.m_CanDo && this.rdoTHALeft.Checked)
            {
                (this.m_pTextElement as ISymbolCollectionElement).HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
            }
        }

        private void rdoTHARight_Click(object sender, EventArgs e)
        {
            if (this.m_CanDo && this.rdoTHARight.Checked)
            {
                (this.m_pTextElement as ISymbolCollectionElement).HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight;
            }
        }

        private int RGB(int r, int g, int b)
        {
            uint num = 0;
            num |= (uint) b;
            num = num << 8;
            num |= (uint) g;
            num = num << 8;
            num |= (uint) r;
            return (int) num;
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
                int rGB = pColor.RGB;
                this.GetRGB((uint) rGB, out num, out num2, out num3);
                colorEdit.Color = Color.FromArgb(pColor.Transparency, num, num2, num3);
            }
        }

        private void UpdateColorFromColorEdit(ColorEdit colorEdit, IColor pColor)
        {
            pColor.NullColor = colorEdit.Color.IsEmpty;
            if (!colorEdit.Color.IsEmpty)
            {
                pColor.Transparency = colorEdit.Color.A;
                pColor.RGB = this.RGB(colorEdit.Color.R, colorEdit.Color.G, colorEdit.Color.B);
            }
        }

        public IActiveView ActiveView
        {
            set
            {
                this.m_pActiveView = value;
            }
        }

        public IAnnotationFeature AnnotationFeature
        {
            set
            {
                this.m_pAnnoFeat = value;
                if (this.m_pAnnoFeat != null)
                {
                    this.m_pTextElement = this.m_pAnnoFeat.Annotation as ITextElement;
                    if (this.m_CanDo)
                    {
                        this.m_CanDo = false;
                        this.Init();
                        this.m_CanDo = true;
                    }
                }
            }
        }
    }
}

