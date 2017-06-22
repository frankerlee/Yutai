using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    public partial class NorthArrowPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private INorthArrow m_pNorthArrow = null;
        private INorthArrow m_pOldNorthArrow = null;
        private string m_Title = "指北针";

        public event OnValueChangeEventHandler OnValueChange;

        public NorthArrowPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                (this.m_pOldNorthArrow as IClone).Assign(this.m_pNorthArrow as IClone);
                this.m_IsPageDirty = false;
            }
        }

        private void btnNorthArrorSelector_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.m_pSG);
                    selector.SetSymbol(this.m_pNorthArrow);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        this.m_pNorthArrow = selector.GetSymbol() as INorthArrow;
                        this.m_CanDo = false;
                        this.Init();
                        this.m_CanDo = true;
                        this.ValueChanged();
                    }
                }
            }
            catch
            {
            }
        }

        private void btnNorthMarkerSymbolSelector_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.m_pSG);
                    selector.SetSymbol((this.m_pNorthArrow as IMarkerNorthArrow).MarkerSymbol);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        (this.m_pNorthArrow as IMarkerNorthArrow).MarkerSymbol = selector.GetSymbol() as IMarkerSymbol;
                        this.m_CanDo = false;
                        this.Init();
                        this.m_CanDo = true;
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

        private void colorEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IColor pColor = this.m_pNorthArrow.Color;
                this.UpdateColorFromColorEdit(this.colorEdit1, pColor);
                this.m_pNorthArrow.Color = pColor;
                this.ValueChanged();
            }
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

        public static void GetEsriRGB(uint rgb, out int r, out int g, out int b)
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

        private void Init()
        {
            if (this.m_pNorthArrow != null)
            {
                this.symbolItem1.Symbol = this.m_pNorthArrow;
                this.txtAngle.Text = this.m_pNorthArrow.Angle.ToString();
                this.txtCalibrationAngle.Text = this.m_pNorthArrow.CalibrationAngle.ToString();
                this.txtSize.Text = this.m_pNorthArrow.Size.ToString();
                this.SetColorEdit(this.colorEdit1, this.m_pNorthArrow.Color);
            }
        }

 private void NorthArrowPropertyPage_Load(object sender, EventArgs e)
        {
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
            this.m_pOldNorthArrow = @object as INorthArrow;
            if (this.m_pOldNorthArrow != null)
            {
                this.m_pNorthArrow = (this.m_pOldNorthArrow as IClone).Clone() as INorthArrow;
            }
        }

        private void txtAngle_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void txtCalibrationAngle_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                try
                {
                    this.m_pNorthArrow.CalibrationAngle = double.Parse(this.txtCalibrationAngle.Text);
                    this.ValueChanged();
                }
                catch
                {
                }
            }
        }

        private void txtSize_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                try
                {
                    this.m_pNorthArrow.Size = double.Parse(this.txtSize.Text);
                    this.ValueChanged();
                }
                catch
                {
                }
            }
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
            this.symbolItem1.Invalidate();
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

