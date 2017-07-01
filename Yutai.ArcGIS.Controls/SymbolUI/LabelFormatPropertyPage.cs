using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    public partial class LabelFormatPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private string m_Title = "标注";
        private IGridLabel pGridLabel = null;

        public event OnValueChangeEventHandler OnValueChange;

        public LabelFormatPropertyPage()
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
                GridAxisPropertyPage.m_pMapGrid.SetLabelVisibility(this.chkLabelLeft.Checked, this.chkLabelTop.Checked,
                    this.chkLabelRight.Checked, this.chkLabelBottom.Checked);
                this.pGridLabel.set_LabelAlignment(esriGridAxisEnum.esriGridAxisBottom, !this.chkverticalBottom.Checked);
                this.pGridLabel.set_LabelAlignment(esriGridAxisEnum.esriGridAxisLeft, !this.chkverticalLeft.Checked);
                this.pGridLabel.set_LabelAlignment(esriGridAxisEnum.esriGridAxisRight, !this.chkverticalRight.Checked);
                this.pGridLabel.set_LabelAlignment(esriGridAxisEnum.esriGridAxisTop, !this.chkverticalTop.Checked);
                stdole.IFontDisp font = this.pGridLabel.Font;
                if (this.cboFontName.SelectedIndex != -1)
                {
                    font.Name = this.cboFontName.Text;
                }
                font.Size = (decimal) double.Parse(this.cboFontSize.Text);
                font.Bold = this.chkBold.Checked;
                font.Italic = this.chkIta.Checked;
                font.Underline = this.chkUnderLine.Checked;
                this.pGridLabel.Font = font;
                IColor pColor = this.pGridLabel.Color;
                this.UpdateColorFromColorEdit(this.colorEdit1, pColor);
                this.pGridLabel.Color = pColor;
                this.pGridLabel.LabelOffset = (double) this.txtLabelOffset.Value;
                GridAxisPropertyPage.m_pMapGrid.LabelFormat = this.pGridLabel;
            }
        }

        private void btnProperty_Click(object sender, EventArgs e)
        {
            frmElementProperty property = new frmElementProperty
            {
                Text = "格网标注属性"
            };
            bool flag = false;
            if (this.cboFormat.SelectedIndex == 0)
            {
                NumericFormatPropertyPage page = new NumericFormatPropertyPage();
                property.AddPage(page);
                INumberFormat format = (this.pGridLabel as IFormattedGridLabel).Format;
                flag = property.EditProperties(format);
                if (flag)
                {
                    (this.pGridLabel as IFormattedGridLabel).Format = format;
                }
            }
            else if (this.cboFormat.SelectedIndex == 1)
            {
                MixedLabelPropertyPage page2 = new MixedLabelPropertyPage();
                property.AddPage(page2);
                flag = property.EditProperties(this.pGridLabel);
            }
            else
            {
                CornerGridLabelPropertyPage page3 = new CornerGridLabelPropertyPage();
                property.AddPage(page3);
                PrincipalDigitsLabelPropertyPage page4 = new PrincipalDigitsLabelPropertyPage();
                property.AddPage(page4);
                flag = property.EditProperties(this.pGridLabel);
            }
            if (flag)
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

        private void cboFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if (this.cboFormat.SelectedIndex == 0)
                {
                    this.pGridLabel = new FormattedGridLabelClass();
                }
                else if (this.cboFormat.SelectedIndex == 1)
                {
                    this.pGridLabel = new MixedFontGridLabelClass();
                }
                else
                {
                    this.pGridLabel = new DMSGridLabelClass();
                }
                this.m_CanDo = false;
                this.SetGridLabel();
                this.m_CanDo = false;
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

        private void chkLabelTop_CheckedChanged(object sender, EventArgs e)
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

        private void chkSubBottom_CheckedChanged(object sender, EventArgs e)
        {
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

        private void LabelFormatPropertyPage_Load(object sender, EventArgs e)
        {
            if (GridAxisPropertyPage.m_pMapGrid != null)
            {
                bool leftVis = false;
                bool topVis = false;
                bool rightVis = false;
                bool bottomVis = false;
                GridAxisPropertyPage.m_pMapGrid.QueryLabelVisibility(ref leftVis, ref topVis, ref rightVis,
                    ref bottomVis);
                this.chkLabelBottom.Checked = bottomVis;
                this.chkLabelLeft.Checked = leftVis;
                this.chkLabelRight.Checked = rightVis;
                this.chkLabelTop.Checked = topVis;
                this.pGridLabel = GridAxisPropertyPage.m_pMapGrid.LabelFormat;
                if (this.pGridLabel is IMixedFontGridLabel2)
                {
                    this.cboFormat.SelectedIndex = 1;
                }
                else if (this.pGridLabel is IFormattedGridLabel)
                {
                    this.cboFormat.SelectedIndex = 0;
                }
                else
                {
                    this.cboFormat.SelectedIndex = 2;
                }
                this.SetGridLabel();
                this.m_CanDo = true;
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

        private void SetGridLabel()
        {
            this.chkverticalBottom.Checked = !this.pGridLabel.get_LabelAlignment(esriGridAxisEnum.esriGridAxisBottom);
            this.chkverticalLeft.Checked = !this.pGridLabel.get_LabelAlignment(esriGridAxisEnum.esriGridAxisLeft);
            this.chkverticalRight.Checked = !this.pGridLabel.get_LabelAlignment(esriGridAxisEnum.esriGridAxisRight);
            this.chkverticalTop.Checked = !this.pGridLabel.get_LabelAlignment(esriGridAxisEnum.esriGridAxisTop);
            string name = this.pGridLabel.Font.Name;
            for (int i = 0; i < this.cboFontName.Items.Count; i++)
            {
                if (name == this.cboFontName.Items[i].ToString())
                {
                    this.cboFontName.SelectedIndex = i;
                    break;
                }
            }
            this.cboFontSize.Text = this.pGridLabel.Font.Size.ToString();
            this.chkBold.Checked = this.pGridLabel.Font.Bold;
            this.chkIta.Checked = this.pGridLabel.Font.Italic;
            this.chkUnderLine.Checked = this.pGridLabel.Font.Underline;
            this.SetColorEdit(this.colorEdit1, this.pGridLabel.Color);
            this.txtLabelOffset.Text = this.pGridLabel.LabelOffset.ToString();
        }

        public void SetObjects(object @object)
        {
        }

        private void txtLabelOffset_EditValueChanged(object sender, EventArgs e)
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
            set { this.m_Title = value; }
        }
    }
}