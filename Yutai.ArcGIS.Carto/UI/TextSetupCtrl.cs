using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Controls.SymbolUI;
using IPropertyPage = Yutai.ArcGIS.Common.BaseClasses.IPropertyPage;
using IPropertyPageEvents = Yutai.ArcGIS.Common.BaseClasses.IPropertyPageEvents;
using OnValueChangeEventHandler = Yutai.ArcGIS.Common.BaseClasses.OnValueChangeEventHandler;

namespace Yutai.ArcGIS.Carto.UI
{
    public class TextSetupCtrl : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private SimpleButton btnSymbolSelector;
        private double double_0 = 0.0;
        private esriTextHorizontalAlignment esriTextHorizontalAlignment_0;
        private IContainer icontainer_0;
        private ImageList imageList_0;
        private IStyleGallery istyleGallery_0 = ApplicationBase.StyleGallery;
        private ITextElement itextElement_0 = null;
        private ITextSymbol itextSymbol_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private RadioButton rdoTHACenter;
        private RadioButton rdoTHAFul;
        private RadioButton rdoTHALeft;
        private RadioButton rdoTHARight;
        private string string_0 = "文字";
        private SpinEdit txtAngle;
        private SpinEdit txtCharacterSpace;
        private TextEdit txtFontInfo;
        private SpinEdit txtLeading;
        private MemoEdit txtString;

        public event OnValueChangeEventHandler OnValueChange;

        public TextSetupCtrl()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            try
            {
                if (this.bool_1)
                {
                    this.bool_1 = false;
                    this.itextSymbol_0.Angle = this.double_0;
                    this.itextElement_0.Symbol = (this.itextSymbol_0 as IClone).Clone() as ITextSymbol;
                    this.itextElement_0.Text = this.txtString.Text;
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnSymbolSelector_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.istyleGallery_0);
                    selector.SetSymbol(this.itextSymbol_0);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        this.itextSymbol_0 = selector.GetSymbol() as ITextSymbol;
                        this.esriTextHorizontalAlignment_0 = this.itextSymbol_0.HorizontalAlignment;
                        stdole.IFontDisp font = this.itextElement_0.Symbol.Font;
                        this.txtFontInfo.Text = font.Name + " " + font.Size.ToString();
                        this.double_0 = this.itextSymbol_0.Angle;
                        this.itextSymbol_0.Angle = 0.0;
                        this.bool_0 = false;
                        this.txtAngle.Text = this.double_0.ToString("0.###");
                        this.txtCharacterSpace.Text = (this.itextSymbol_0 as IFormattedTextSymbol).CharacterSpacing.ToString();
                        this.txtLeading.Text = (this.itextSymbol_0 as IFormattedTextSymbol).Leading.ToString("#.##");
                        switch (this.esriTextHorizontalAlignment_0)
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
                        this.bool_0 = true;
                        this.method_1();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextSetupCtrl));
            this.label1 = new Label();
            this.txtString = new MemoEdit();
            this.rdoTHAFul = new RadioButton();
            this.imageList_0 = new ImageList(this.icontainer_0);
            this.rdoTHALeft = new RadioButton();
            this.rdoTHACenter = new RadioButton();
            this.rdoTHARight = new RadioButton();
            this.label2 = new Label();
            this.txtFontInfo = new TextEdit();
            this.label3 = new Label();
            this.txtAngle = new SpinEdit();
            this.txtCharacterSpace = new SpinEdit();
            this.label4 = new Label();
            this.txtLeading = new SpinEdit();
            this.label5 = new Label();
            this.btnSymbolSelector = new SimpleButton();
            this.txtString.Properties.BeginInit();
            this.txtFontInfo.Properties.BeginInit();
            this.txtAngle.Properties.BeginInit();
            this.txtCharacterSpace.Properties.BeginInit();
            this.txtLeading.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "文本:";
            this.txtString.EditValue = "";
            this.txtString.Location = new Point(0x10, 0x20);
            this.txtString.Name = "txtString";
            this.txtString.Size = new Size(0x130, 0x68);
            this.txtString.TabIndex = 1;
            this.txtString.EditValueChanged += new EventHandler(this.txtString_EditValueChanged);
            this.rdoTHAFul.Appearance = Appearance.Button;
            this.rdoTHAFul.ImageIndex = 6;
            this.rdoTHAFul.ImageList = this.imageList_0;
            this.rdoTHAFul.Location = new Point(0x128, 0x90);
            this.rdoTHAFul.Name = "rdoTHAFul";
            this.rdoTHAFul.Size = new Size(0x1c, 0x18);
            this.rdoTHAFul.TabIndex = 15;
            this.rdoTHAFul.Click += new EventHandler(this.rdoTHAFul_Click);
            this.imageList_0.ImageSize = new Size(0x10, 0x10);
            this.imageList_0.ImageStream = (ImageListStreamer) resources.GetObject("imageList1.ImageStream");
            this.imageList_0.TransparentColor = Color.Magenta;
            this.rdoTHALeft.Appearance = Appearance.Button;
            this.rdoTHALeft.ImageIndex = 3;
            this.rdoTHALeft.ImageList = this.imageList_0;
            this.rdoTHALeft.Location = new Point(200, 0x90);
            this.rdoTHALeft.Name = "rdoTHALeft";
            this.rdoTHALeft.Size = new Size(0x1c, 0x18);
            this.rdoTHALeft.TabIndex = 14;
            this.rdoTHALeft.Click += new EventHandler(this.rdoTHALeft_Click);
            this.rdoTHACenter.Appearance = Appearance.Button;
            this.rdoTHACenter.ImageIndex = 4;
            this.rdoTHACenter.ImageList = this.imageList_0;
            this.rdoTHACenter.Location = new Point(0xe8, 0x90);
            this.rdoTHACenter.Name = "rdoTHACenter";
            this.rdoTHACenter.Size = new Size(0x1c, 0x18);
            this.rdoTHACenter.TabIndex = 13;
            this.rdoTHACenter.Click += new EventHandler(this.rdoTHACenter_Click);
            this.rdoTHARight.Appearance = Appearance.Button;
            this.rdoTHARight.ImageIndex = 5;
            this.rdoTHARight.ImageList = this.imageList_0;
            this.rdoTHARight.Location = new Point(0x108, 0x90);
            this.rdoTHARight.Name = "rdoTHARight";
            this.rdoTHARight.Size = new Size(0x1c, 0x18);
            this.rdoTHARight.TabIndex = 12;
            this.rdoTHARight.TabStop = true;
            this.rdoTHARight.Click += new EventHandler(this.rdoTHARight_Click);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 0x90);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x23, 0x11);
            this.label2.TabIndex = 0x10;
            this.label2.Text = "字体:";
            this.txtFontInfo.EditValue = "";
            this.txtFontInfo.Location = new Point(0x30, 0x90);
            this.txtFontInfo.Name = "txtFontInfo";
            this.txtFontInfo.Properties.Appearance.BackColor = SystemColors.InactiveBorder;
            this.txtFontInfo.Properties.Appearance.Options.UseBackColor = true;
            this.txtFontInfo.Properties.ReadOnly = true;
            this.txtFontInfo.Size = new Size(0x80, 0x17);
            this.txtFontInfo.TabIndex = 0x11;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(8, 0xb8);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x23, 0x11);
            this.label3.TabIndex = 0x12;
            this.label3.Text = "角度:";
            int[] bits = new int[4];
            this.txtAngle.EditValue = new decimal(bits);
            this.txtAngle.Location = new Point(0x30, 0xb0);
            this.txtAngle.Name = "txtAngle";
            this.txtAngle.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            int[] bits2 = new int[4];
            bits2[0] = 360;
            this.txtAngle.Properties.MaxValue = new decimal(bits2);
            int[] bits3 = new int[4];
            bits3[0] = 360;
            bits3[3] = -2147483648;
            this.txtAngle.Properties.MinValue = new decimal(bits3);
            this.txtAngle.Properties.UseCtrlIncrement = false;
            this.txtAngle.Size = new Size(0x48, 0x17);
            this.txtAngle.TabIndex = 0x13;
            this.txtAngle.EditValueChanged += new EventHandler(this.txtAngle_EditValueChanged);
            int[] bits4 = new int[4];
            this.txtCharacterSpace.EditValue = new decimal(bits4);
            this.txtCharacterSpace.Location = new Point(0xf8, 0xb0);
            this.txtCharacterSpace.Name = "txtCharacterSpace";
            this.txtCharacterSpace.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            int[] bits5 = new int[4];
            bits5[0] = 360;
            this.txtCharacterSpace.Properties.MaxValue = new decimal(bits5);
            int[] bits6 = new int[4];
            bits6[0] = 360;
            bits6[3] = -2147483648;
            this.txtCharacterSpace.Properties.MinValue = new decimal(bits6);
            this.txtCharacterSpace.Properties.UseCtrlIncrement = false;
            this.txtCharacterSpace.Size = new Size(0x48, 0x17);
            this.txtCharacterSpace.TabIndex = 0x15;
            this.txtCharacterSpace.EditValueChanged += new EventHandler(this.txtCharacterSpace_EditValueChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0xb8, 0xb0);
            this.label4.Name = "label4";
            this.label4.Size = new Size(60, 0x11);
            this.label4.TabIndex = 20;
            this.label4.Text = "字符间距:";
            int[] bits7 = new int[4];
            this.txtLeading.EditValue = new decimal(bits7);
            this.txtLeading.Location = new Point(0xf8, 0xd0);
            this.txtLeading.Name = "txtLeading";
            this.txtLeading.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            int[] bits8 = new int[4];
            bits8[0] = 360;
            this.txtLeading.Properties.MaxValue = new decimal(bits8);
            int[] bits9 = new int[4];
            bits9[0] = 360;
            bits9[3] = -2147483648;
            this.txtLeading.Properties.MinValue = new decimal(bits9);
            this.txtLeading.Properties.UseCtrlIncrement = false;
            this.txtLeading.Size = new Size(0x48, 0x17);
            this.txtLeading.TabIndex = 0x17;
            this.txtLeading.EditValueChanged += new EventHandler(this.txtLeading_EditValueChanged);
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0xc0, 0xd0);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x30, 0x11);
            this.label5.TabIndex = 0x16;
            this.label5.Text = "行间距:";
            this.btnSymbolSelector.Location = new Point(200, 240);
            this.btnSymbolSelector.Name = "btnSymbolSelector";
            this.btnSymbolSelector.Size = new Size(0x48, 0x18);
            this.btnSymbolSelector.TabIndex = 0x18;
            this.btnSymbolSelector.Text = "更改符号";
            this.btnSymbolSelector.Click += new EventHandler(this.btnSymbolSelector_Click);
            base.Controls.Add(this.btnSymbolSelector);
            base.Controls.Add(this.txtLeading);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.txtCharacterSpace);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.txtAngle);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.txtFontInfo);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.rdoTHAFul);
            base.Controls.Add(this.rdoTHALeft);
            base.Controls.Add(this.rdoTHACenter);
            base.Controls.Add(this.rdoTHARight);
            base.Controls.Add(this.txtString);
            base.Controls.Add(this.label1);
            base.Name = "TextSetupCtrl";
            base.Size = new Size(0x158, 280);
            base.Load += new EventHandler(this.TextSetupCtrl_Load);
            this.txtString.Properties.EndInit();
            this.txtFontInfo.Properties.EndInit();
            this.txtAngle.Properties.EndInit();
            this.txtCharacterSpace.Properties.EndInit();
            this.txtLeading.Properties.EndInit();
            base.ResumeLayout(false);
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

        private void method_0()
        {
            if (this.itextElement_0 != null)
            {
                this.txtString.Text = this.itextElement_0.Text;
                stdole.IFontDisp font = this.itextElement_0.Symbol.Font;
                this.txtFontInfo.Text = font.Name + " " + font.Size.ToString();
                this.esriTextHorizontalAlignment_0 = (this.itextElement_0 as ISymbolCollectionElement).HorizontalAlignment;
                this.txtCharacterSpace.Text = (this.itextElement_0 as ISymbolCollectionElement).CharacterSpacing.ToString();
                this.txtLeading.Text = (this.itextElement_0 as ISymbolCollectionElement).Leading.ToString("0.##");
                this.double_0 = this.itextSymbol_0.Angle;
                this.txtAngle.Text = this.double_0.ToString("0.###");
                switch (this.esriTextHorizontalAlignment_0)
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
            }
        }

        private void method_1()
        {
            this.bool_1 = true;
            if (this.OnValueChange != null)
            {
                this.OnValueChange();
            }
        }

        private void rdoTHACenter_Click(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                if (this.rdoTHACenter.Checked)
                {
                    this.itextSymbol_0.HorizontalAlignment = esriTextHorizontalAlignment.esriTHACenter;
                    this.esriTextHorizontalAlignment_0 = esriTextHorizontalAlignment.esriTHACenter;
                }
                this.method_1();
            }
        }

        private void rdoTHAFul_Click(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                if (this.rdoTHAFul.Checked)
                {
                    this.itextSymbol_0.HorizontalAlignment = esriTextHorizontalAlignment.esriTHAFull;
                    this.esriTextHorizontalAlignment_0 = esriTextHorizontalAlignment.esriTHAFull;
                }
                this.method_1();
            }
        }

        private void rdoTHALeft_Click(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                if (this.rdoTHALeft.Checked)
                {
                    this.itextSymbol_0.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
                    this.esriTextHorizontalAlignment_0 = esriTextHorizontalAlignment.esriTHALeft;
                }
                this.method_1();
            }
        }

        private void rdoTHARight_Click(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                if (this.rdoTHARight.Checked)
                {
                    this.itextSymbol_0.HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight;
                    this.esriTextHorizontalAlignment_0 = esriTextHorizontalAlignment.esriTHARight;
                }
                this.method_1();
            }
        }

        public void ResetControl()
        {
            this.bool_0 = false;
            this.method_0();
            this.bool_0 = true;
        }

        public void SetObjects(object object_0)
        {
            this.itextElement_0 = object_0 as ITextElement;
            this.itextSymbol_0 = (this.itextElement_0.Symbol as IClone).Clone() as ITextSymbol;
        }

        private void TextSetupCtrl_Load(object sender, EventArgs e)
        {
            this.method_0();
            this.bool_0 = true;
        }

        private void txtAngle_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    this.double_0 = double.Parse(this.txtAngle.Text);
                    this.method_1();
                }
                catch
                {
                    MessageBox.Show("数据格式错误!");
                }
            }
        }

        private void txtCharacterSpace_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    (this.itextSymbol_0 as IFormattedTextSymbol).CharacterSpacing = double.Parse(this.txtCharacterSpace.Text);
                    this.method_1();
                }
                catch
                {
                    MessageBox.Show("数据格式错误!");
                }
            }
        }

        private void txtLeading_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    (this.itextSymbol_0 as IFormattedTextSymbol).Leading = double.Parse(this.txtLeading.Text);
                    this.method_1();
                }
                catch
                {
                    MessageBox.Show("数据格式错误!");
                }
            }
        }

        private void txtString_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.method_1();
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

