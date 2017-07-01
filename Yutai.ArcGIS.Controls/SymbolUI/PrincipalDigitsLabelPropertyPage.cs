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

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    public partial class PrincipalDigitsLabelPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool m_CanDo = false;
        private IPrincipalDigitsGridLabel m_GridLable = null;
        private bool m_IsPageDirty = false;
        private string m_Title = "主要数字";

        public event OnValueChangeEventHandler OnValueChange;

        public PrincipalDigitsLabelPropertyPage()
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
                stdole.IFontDisp smallLabelFont = this.m_GridLable.SmallLabelFont;
                if (this.cboFontName.SelectedIndex != -1)
                {
                    smallLabelFont.Name = this.cboFontName.Text;
                }
                smallLabelFont.Size = (decimal) double.Parse(this.cboFontSize.Text);
                smallLabelFont.Bold = this.chkBold.Checked;
                smallLabelFont.Italic = this.chkIta.Checked;
                smallLabelFont.Underline = this.chkUnderLine.Checked;
                this.m_GridLable.SmallLabelFont = smallLabelFont;
                IColor smallLabelColor = this.m_GridLable.SmallLabelColor;
                this.UpdateColorFromColorEdit(this.colorEdit1, smallLabelColor);
                this.m_GridLable.SmallLabelColor = smallLabelColor;
                this.m_GridLable.BaseDigitCount = (int) this.txtBaseDigitCount.Value;
                this.m_GridLable.PrincipalDigitCount = (int) this.txtPrincipalDigitCount.Value;
                this.m_GridLable.UnitSuffix = this.txtUnitSuffix.Text;
                this.m_GridLable.NorthingSuffix = this.txtNorthingSuffix.Text;
                this.m_GridLable.EastingSuffix = this.txtEastingSuffix.Text;
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
        }

        private void colorEdit1_EditValueChanged(object sender, EventArgs e)
        {
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

        private void PrincipalDigitsLabelPropertyPage_Load(object sender, EventArgs e)
        {
            string name = this.m_GridLable.SmallLabelFont.Name;
            for (int i = 0; i < this.cboFontName.Items.Count; i++)
            {
                if (name == this.cboFontName.Items[i].ToString())
                {
                    this.cboFontName.SelectedIndex = i;
                    break;
                }
            }
            this.cboFontSize.Text = this.m_GridLable.SmallLabelSize.ToString();
            this.chkBold.Checked = this.m_GridLable.SmallLabelFont.Bold;
            this.chkIta.Checked = this.m_GridLable.SmallLabelFont.Italic;
            this.chkUnderLine.Checked = this.m_GridLable.SmallLabelFont.Underline;
            this.SetColorEdit(this.colorEdit1, this.m_GridLable.SmallLabelColor);
            this.txtBaseDigitCount.Value = this.m_GridLable.BaseDigitCount;
            this.txtPrincipalDigitCount.Value = this.m_GridLable.PrincipalDigitCount;
            this.txtUnitSuffix.Text = this.m_GridLable.UnitSuffix;
            this.txtNorthingSuffix.Text = this.m_GridLable.NorthingSuffix;
            this.txtEastingSuffix.Text = this.m_GridLable.EastingSuffix;
            this.m_CanDo = true;
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
            this.m_GridLable = @object as IPrincipalDigitsGridLabel;
        }

        private void txtBaseDigitCount_EditValueChanged(object sender, EventArgs e)
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

        private void txtEastingSuffix_EditValueChanged(object sender, EventArgs e)
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

        private void txtNorthingSuffix_EditValueChanged(object sender, EventArgs e)
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

        private void txtPrincipalDigitCount_EditValueChanged(object sender, EventArgs e)
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

        private void txtUnitSuffix_EditValueChanged(object sender, EventArgs e)
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