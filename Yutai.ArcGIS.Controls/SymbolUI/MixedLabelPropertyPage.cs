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

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    public partial class MixedLabelPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private IMixedFontGridLabel2 m_pMixedFormatGridLabel = null;
        private INumberFormat m_pNumberFormat = null;
        private string m_Title = "混合字体标注";

        public event OnValueChangeEventHandler OnValueChange;

        public MixedLabelPropertyPage()
        {
            this.InitializeComponent();
            this.cboFontName.Items.Clear();
            InstalledFontCollection fonts = new InstalledFontCollection();
            for (int i = 0; i < fonts.Families.Length; i++)
            {
                this.cboFontName.Items.Add(fonts.Families[i].Name);
            }
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
                stdole.IFontDisp secondaryFont = this.m_pMixedFormatGridLabel.SecondaryFont;
                if (this.cboFontName.SelectedIndex != -1)
                {
                    secondaryFont.Name = this.cboFontName.Text;
                }
                secondaryFont.Bold = this.chkBold.Checked;
                secondaryFont.Italic = this.chkIta.Checked;
                secondaryFont.Underline = this.chkUnderLine.Checked;
                this.m_pMixedFormatGridLabel.SecondaryFont = secondaryFont;
                IColor secondaryColor = this.m_pMixedFormatGridLabel.SecondaryColor;
                this.UpdateColorFromColorEdit(this.colorEdit1, secondaryColor);
                this.m_pMixedFormatGridLabel.SecondaryColor = secondaryColor;
                this.m_pMixedFormatGridLabel.SecondaryFontSize = double.Parse(this.cboFontSize.Text);
                if (this.radioGroup.SelectedIndex == 0)
                {
                    this.m_pMixedFormatGridLabel.NumGroupedDigits = 0;
                }
                else
                {
                    this.m_pMixedFormatGridLabel.NumGroupedDigits = (short) this.txtNumGroupedDigits.Value;
                }
            }
        }

        private void btnNumberFormat_Click(object sender, EventArgs e)
        {
            frmElementProperty property = new frmElementProperty
            {
                Text = "数字格式属性"
            };
            NumericFormatPropertyPage page = new NumericFormatPropertyPage();
            property.AddPage(page);
            if (property.EditProperties(this.m_pNumberFormat))
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        public void Cancel()
        {
        }

        private void cboFontName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void cboFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void chkBold_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void chkIta_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void chkUnderLine_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void colorEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void GetRGB(uint rgb, out int r, out int g, out int b)
        {
            uint num = rgb & 16711680;
            b = (int) (num >> 16);
            num = rgb & 65280;
            g = (int) (num >> 8);
            num = rgb & 255;
            r = (int) num;
        }

        public void Hide()
        {
        }

        private void MixedLabelPropertyPage_Load(object sender, EventArgs e)
        {
            string name = this.m_pMixedFormatGridLabel.SecondaryFont.Name;
            for (int i = 0; i < this.cboFontName.Items.Count; i++)
            {
                if (name == this.cboFontName.Items[i].ToString())
                {
                    this.cboFontName.SelectedIndex = i;
                    break;
                }
            }
            if (this.m_pMixedFormatGridLabel.NumGroupedDigits <= 0)
            {
                this.radioGroup.SelectedIndex = 0;
                this.txtNumGroupedDigits.Value = 3M;
            }
            else
            {
                this.radioGroup.SelectedIndex = 1;
                this.txtNumGroupedDigits.Value = this.m_pMixedFormatGridLabel.NumGroupedDigits;
            }
            this.cboFontSize.Text = this.m_pMixedFormatGridLabel.SecondaryFontSize.ToString();
            this.chkBold.Checked = this.m_pMixedFormatGridLabel.SecondaryFont.Bold;
            this.chkIta.Checked = this.m_pMixedFormatGridLabel.SecondaryFont.Italic;
            this.chkUnderLine.Checked = this.m_pMixedFormatGridLabel.SecondaryFont.Underline;
            this.SetColorEdit(this.colorEdit1, this.m_pMixedFormatGridLabel.SecondaryColor);
            this.m_CanDo = true;
        }

        private void radioGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtNumGroupedDigits.Enabled = this.radioGroup.SelectedIndex == 1;
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
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

        public void SetObjects(object @object)
        {
            this.m_pMixedFormatGridLabel = @object as IMixedFontGridLabel2;
            this.m_pNumberFormat = (this.m_pMixedFormatGridLabel as IFormattedGridLabel).Format;
            if (this.m_pNumberFormat == null)
            {
                this.m_pNumberFormat = new NumericFormatClass();
                (this.m_pMixedFormatGridLabel as IFormattedGridLabel).Format = this.m_pNumberFormat;
            }
        }

        private void txtNumGroupedDigits_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
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

        public bool IsPageDirty
        {
            get { return this.m_IsPageDirty; }
        }

        int IPropertyPage.Height
        {
            get { return base.Height; }
        }

        int IPropertyPage.Width
        {
            get { return base.Width; }
        }

        public string Title
        {
            get { return this.m_Title; }
            set { }
        }
    }
}