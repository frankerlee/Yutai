using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Controls.SymbolUI;
using IPropertyPage = Yutai.ArcGIS.Common.BaseClasses.IPropertyPage;
using IPropertyPageEvents = Yutai.ArcGIS.Common.BaseClasses.IPropertyPageEvents;
using OnValueChangeEventHandler = Yutai.ArcGIS.Common.BaseClasses.OnValueChangeEventHandler;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public partial class LabelFormatPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private IGridLabel igridLabel_0 = null;
        protected IMapGrid m_pMapGrid = null;
        [CompilerGenerated]
        private string string_0 = "标注";

        public event OnValueChangeEventHandler OnValueChange;

        public LabelFormatPropertyPage()
        {
            this.InitializeComponent();
            this.cboFontName.Items.Clear();
            using (InstalledFontCollection fonts = new InstalledFontCollection())
            {
                for (int i = 0; i < fonts.Families.Length; i++)
                {
                    this.cboFontName.Items.Add(fonts.Families[i].Name);
                }
            }
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                this.m_pMapGrid.SetLabelVisibility(this.chkLabelLeft.Checked, this.chkLabelTop.Checked, this.chkLabelRight.Checked, this.chkLabelBottom.Checked);
                this.igridLabel_0.set_LabelAlignment(esriGridAxisEnum.esriGridAxisBottom, !this.chkverticalBottom.Checked);
                this.igridLabel_0.set_LabelAlignment(esriGridAxisEnum.esriGridAxisLeft, !this.chkverticalLeft.Checked);
                this.igridLabel_0.set_LabelAlignment(esriGridAxisEnum.esriGridAxisRight, !this.chkverticalRight.Checked);
                this.igridLabel_0.set_LabelAlignment(esriGridAxisEnum.esriGridAxisTop, !this.chkverticalTop.Checked);
                stdole.IFontDisp font = this.igridLabel_0.Font;
                if (this.cboFontName.SelectedIndex != -1)
                {
                    font.Name = this.cboFontName.Text;
                }
                font.Size = (decimal) double.Parse(this.cboFontSize.Text);
                font.Bold = this.chkBold.Checked;
                font.Italic = this.chkIta.Checked;
                font.Underline = this.chkUnderLine.Checked;
                this.igridLabel_0.Font = font;
                IColor color = this.igridLabel_0.Color;
                this.method_3(this.colorEdit1, color);
                this.igridLabel_0.Color = color;
                this.igridLabel_0.LabelOffset = Convert.ToDouble(this.txtLabelOffset.Text);
                this.m_pMapGrid.LabelFormat = this.igridLabel_0;
            }
        }

        private void btnProperty_Click(object sender, EventArgs e)
        {
            frmElementProperty property = new frmElementProperty {
                Text = "格网标注属性"
            };
            bool flag = false;
            if (this.cboFormat.SelectedIndex == 0)
            {
                NumericFormatPropertyPage page = new NumericFormatPropertyPage();
                property.AddPage(page);
                INumberFormat format = (this.igridLabel_0 as IFormattedGridLabel).Format;
                if (flag = property.EditProperties(format))
                {
                    (this.igridLabel_0 as IFormattedGridLabel).Format = format;
                }
            }
            else if (this.cboFormat.SelectedIndex == 1)
            {
                MixedLabelPropertyPage page2 = new MixedLabelPropertyPage();
                property.AddPage(page2);
                flag = property.EditProperties(this.igridLabel_0);
            }
            else
            {
                CornerGridLabelPropertyPage page3 = new CornerGridLabelPropertyPage();
                property.AddPage(page3);
                PrincipalDigitsLabelPropertyPage page4 = new PrincipalDigitsLabelPropertyPage();
                property.AddPage(page4);
                flag = property.EditProperties(this.igridLabel_0);
            }
            if (flag)
            {
                this.bool_1 = true;
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
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void cboFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void cboFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                if (this.cboFormat.SelectedIndex == 0)
                {
                    this.igridLabel_0 = new FormattedGridLabelClass();
                }
                else if (this.cboFormat.SelectedIndex == 1)
                {
                    this.igridLabel_0 = new MixedFontGridLabelClass();
                }
                else
                {
                    this.igridLabel_0 = new DMSGridLabelClass();
                }
                this.bool_0 = false;
                this.method_4();
                this.bool_0 = false;
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void chkBold_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void chkLabelTop_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void chkverticalBottom_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void colorEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

 public void Hide()
        {
        }

 private void LabelFormatPropertyPage_Load(object sender, EventArgs e)
        {
            this.m_pMapGrid = this.MapTemplate.MapGrid;
            if (this.m_pMapGrid != null)
            {
                bool leftVis = false;
                bool topVis = false;
                bool rightVis = false;
                bool bottomVis = false;
                this.m_pMapGrid.QueryLabelVisibility(ref leftVis, ref topVis, ref rightVis, ref bottomVis);
                this.chkLabelBottom.Checked = bottomVis;
                this.chkLabelLeft.Checked = leftVis;
                this.chkLabelRight.Checked = rightVis;
                this.chkLabelTop.Checked = topVis;
                this.igridLabel_0 = this.m_pMapGrid.LabelFormat;
                if (this.igridLabel_0 is IMixedFontGridLabel2)
                {
                    this.cboFormat.SelectedIndex = 1;
                }
                else if (this.igridLabel_0 is IFormattedGridLabel)
                {
                    this.cboFormat.SelectedIndex = 0;
                }
                else
                {
                    this.cboFormat.SelectedIndex = 2;
                }
                this.method_4();
                this.bool_0 = true;
            }
        }

        private void method_0(ColorEdit colorEdit_0, IColor icolor_0)
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
                int rGB = icolor_0.RGB;
                this.method_1((uint) rGB, out num2, out num3, out num4);
                colorEdit_0.Color = Color.FromArgb(icolor_0.Transparency, num2, num3, num4);
            }
        }

        private void method_1(uint uint_0, out int int_0, out int int_1, out int int_2)
        {
            uint num = uint_0 & 16711680;
             int_2 = (int) (num >> 16);
            num = uint_0 & 65280;
            int_1 = (int) (num >> 8);
            num = uint_0 & 255;
            int_0 = (int) num;
        }

        private int method_2(int int_0, int int_1, int int_2)
        {
            uint num = 0;
            num = (uint) (0 | int_2);
            num = num << 8;
            num |=(uint) int_1;
            num = num << 8;
            return (int) (num | int_0);
        }

        private void method_3(ColorEdit colorEdit_0, IColor icolor_0)
        {
            icolor_0.NullColor = colorEdit_0.Color.IsEmpty;
            if (!colorEdit_0.Color.IsEmpty)
            {
                icolor_0.Transparency = colorEdit_0.Color.A;
                icolor_0.RGB = this.method_2(colorEdit_0.Color.R, colorEdit_0.Color.G, colorEdit_0.Color.B);
            }
        }

        private void method_4()
        {
            this.chkverticalBottom.Checked = !this.igridLabel_0.get_LabelAlignment(esriGridAxisEnum.esriGridAxisBottom);
            this.chkverticalLeft.Checked = !this.igridLabel_0.get_LabelAlignment(esriGridAxisEnum.esriGridAxisLeft);
            this.chkverticalRight.Checked = !this.igridLabel_0.get_LabelAlignment(esriGridAxisEnum.esriGridAxisRight);
            this.chkverticalTop.Checked = !this.igridLabel_0.get_LabelAlignment(esriGridAxisEnum.esriGridAxisTop);
            string name = this.igridLabel_0.Font.Name;
            for (int i = 0; i < this.cboFontName.Items.Count; i++)
            {
                if (name == this.cboFontName.Items[i].ToString())
                {
                    this.cboFontName.SelectedIndex = i;
                    break;
                }
            }
            this.cboFontSize.Text = this.igridLabel_0.Font.Size.ToString();
            this.chkBold.Checked = this.igridLabel_0.Font.Bold;
            this.chkIta.Checked = this.igridLabel_0.Font.Italic;
            this.chkUnderLine.Checked = this.igridLabel_0.Font.Underline;
            this.method_0(this.colorEdit1, this.igridLabel_0.Color);
            this.txtLabelOffset.Text = this.igridLabel_0.LabelOffset.ToString();
        }

        private void method_5(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        public void ResetControl()
        {
        }

        public void SetObjects(object object_0)
        {
            this.MapTemplate = object_0 as MapCartoTemplateLib.MapTemplate;
            this.m_pMapGrid = this.MapTemplate.MapGrid;
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

        public MapCartoTemplateLib.MapTemplate MapTemplate
        {
            [CompilerGenerated]
            get
            {
                return this.mapTemplate_0;
            }
            [CompilerGenerated]
            set
            {
                this.mapTemplate_0 = value;
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

