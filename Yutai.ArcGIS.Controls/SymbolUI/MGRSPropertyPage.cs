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
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    public partial class MGRSPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private string m_Title = "MGRS";

        public event OnValueChangeEventHandler OnValueChange;

        public MGRSPropertyPage()
        {
            this.InitializeComponent();
            this.cboFontName.Items.Clear();
            InstalledFontCollection fonts = new InstalledFontCollection();
            for (int i = 0; i < fonts.Families.Length; i++)
            {
                this.cboFontName.Items.Add(fonts.Families[i].Name);
                this.cboGSLFontName.Items.Add(fonts.Families[i].Name);
            }
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
                IGridLadderLabels pMapGrid = GridAxisPropertyPage.m_pMapGrid as IGridLadderLabels;
                stdole.IFontDisp ladderLabelFont = pMapGrid.LadderLabelFont;
                if (this.cboFontName.SelectedIndex != -1)
                {
                    ladderLabelFont.Name = this.cboFontName.Text;
                }
                ladderLabelFont.Bold = this.chkBold.Checked;
                ladderLabelFont.Italic = this.chkIta.Checked;
                ladderLabelFont.Underline = this.chkUnderLine.Checked;
                pMapGrid.LadderLabelFont = ladderLabelFont;
                try
                {
                    pMapGrid.LadderLabelSize = double.Parse(this.cboFontSize.Text);
                }
                catch
                {
                }
                IColor ladderLabelColor = pMapGrid.LadderLabelColor;
                this.UpdateColorFromColorEdit(this.colorEdit1, ladderLabelColor);
                pMapGrid.LadderLabelColor = ladderLabelColor;
                pMapGrid.LadderLabelColumnCount = (int) this.txtColumnCount.Value;
                pMapGrid.LadderLabelRowCount = (int) this.txtRowCount.Value;
                pMapGrid.ShowLadderLabels = this.chkShowLadderLabels.Checked;
                pMapGrid.ShowOuterLabelsOnly = this.chkShowOuterLabelsOnly.Checked;
                IMgrsGrid grid = GridAxisPropertyPage.m_pMapGrid as IMgrsGrid;
                ladderLabelFont = grid.GridSquareLabelFont;
                if (this.cboGSLFontName.SelectedIndex != -1)
                {
                    ladderLabelFont.Name = this.cboGSLFontName.Text;
                }
                ladderLabelFont.Bold = this.chkBold_GSL.Checked;
                ladderLabelFont.Italic = this.chkIta_GSL.Checked;
                ladderLabelFont.Underline = this.chkUnderLine_GSL.Checked;
                grid.GridSquareLabelFont = ladderLabelFont;
                try
                {
                    grid.GridSquareLabelSize = double.Parse(this.cboGSLFontsize.Text);
                }
                catch
                {
                }
                ladderLabelColor = grid.GridSquareLabelColor;
                this.UpdateColorFromColorEdit(this.colorEdit2, ladderLabelColor);
                grid.GridSquareLabelColor = ladderLabelColor;
                grid.ShowGridSquareIdentifiers = this.chkShowGSIdentifiers.Checked;
                grid.GridSquareBoundarySymbol = this.btnBorder.Style as ILineSymbol;
                grid.GridSquareLabelStyle = (esriGridSquareLabelStyleEnum) this.cboGSLStyle.SelectedIndex;
                grid.InteriorTickSymbol = this.btnTickSymbol.Style as ILineSymbol;
                grid.InteriorTickLength = (double) this.txtTickLength.Value;
                this.m_CanDo = true;
            }
        }

        private void btnBorder_Click(object sender, EventArgs e)
        {
            frmSymbolSelector selector = new frmSymbolSelector();
            if (selector != null)
            {
                selector.SetStyleGallery(GridAxisPropertyPage.m_pSG);
                selector.SetSymbol(this.btnBorder.Style);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    this.btnBorder.Style = selector.GetSymbol();
                    if (this.OnValueChange != null)
                    {
                        this.OnValueChange();
                    }
                }
            }
        }

        private void btnTickSymbol_Click(object sender, EventArgs e)
        {
            frmSymbolSelector selector = new frmSymbolSelector();
            if (selector != null)
            {
                selector.SetStyleGallery(GridAxisPropertyPage.m_pSG);
                selector.SetSymbol(this.btnTickSymbol.Style);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    this.btnTickSymbol.Style = selector.GetSymbol();
                    if (this.OnValueChange != null)
                    {
                        this.OnValueChange();
                    }
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

        private void cboGSLFontName_SelectedIndexChanged(object sender, EventArgs e)
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

        private void cboGSLFontsize_SelectedIndexChanged(object sender, EventArgs e)
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

        private void cboGSLStyle_SelectedIndexChanged(object sender, EventArgs e)
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

        private void chkShowGSIdentifiers_CheckedChanged(object sender, EventArgs e)
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

        private void chkShowLadderLabels_CheckedChanged(object sender, EventArgs e)
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

        private void chkShowOuterLabelsOnly_CheckedChanged(object sender, EventArgs e)
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

        private void colorEdit2_EditValueChanged(object sender, EventArgs e)
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

        private void MGRSPropertyPage_Load(object sender, EventArgs e)
        {
            int num;
            IGridLadderLabels pMapGrid = GridAxisPropertyPage.m_pMapGrid as IGridLadderLabels;
            string name = pMapGrid.LadderLabelFont.Name;
            for (num = 0; num < this.cboFontName.Items.Count; num++)
            {
                if (name == this.cboFontName.Items[num].ToString())
                {
                    this.cboFontName.SelectedIndex = num;
                    break;
                }
            }
            this.cboFontSize.Text = pMapGrid.LadderLabelSize.ToString();
            this.chkBold.Checked = pMapGrid.LadderLabelFont.Bold;
            this.chkIta.Checked = pMapGrid.LadderLabelFont.Italic;
            this.chkUnderLine.Checked = pMapGrid.LadderLabelFont.Underline;
            this.SetColorEdit(this.colorEdit1, pMapGrid.LadderLabelColor);
            this.txtColumnCount.Value = pMapGrid.LadderLabelColumnCount;
            this.txtRowCount.Value = pMapGrid.LadderLabelRowCount;
            this.chkShowLadderLabels.Checked = pMapGrid.ShowLadderLabels;
            this.chkShowOuterLabelsOnly.Checked = pMapGrid.ShowOuterLabelsOnly;
            IMgrsGrid grid = GridAxisPropertyPage.m_pMapGrid as IMgrsGrid;
            name = grid.GridSquareLabelFont.Name;
            for (num = 0; num < this.cboGSLFontName.Items.Count; num++)
            {
                if (name == this.cboGSLFontName.Items[num].ToString())
                {
                    this.cboGSLFontName.SelectedIndex = num;
                    break;
                }
            }
            this.cboGSLFontsize.Text = grid.GridSquareLabelSize.ToString();
            this.chkBold_GSL.Checked = grid.GridSquareLabelFont.Bold;
            this.chkIta_GSL.Checked = grid.GridSquareLabelFont.Italic;
            this.chkUnderLine_GSL.Checked = grid.GridSquareLabelFont.Underline;
            this.SetColorEdit(this.colorEdit2, grid.GridSquareLabelColor);
            this.chkShowGSIdentifiers.Checked = grid.ShowGridSquareIdentifiers;
            this.btnBorder.Style = grid.GridSquareBoundarySymbol;
            this.cboGSLStyle.SelectedIndex = (int) grid.GridSquareLabelStyle;
            this.btnTickSymbol.Style = grid.InteriorTickSymbol;
            this.txtTickLength.Value = (decimal) grid.InteriorTickLength;
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
        }

        private void txtRowCount_EditValueChanged(object sender, EventArgs e)
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