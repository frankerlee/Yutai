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
    public partial class TextSetupCtrl : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private double double_0 = 0.0;
        private IStyleGallery istyleGallery_0 = ApplicationBase.StyleGallery;
        private ITextElement itextElement_0 = null;
        private ITextSymbol itextSymbol_0 = null;
        private string string_0 = "文字";

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

