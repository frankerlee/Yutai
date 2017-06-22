using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    public partial class GridHatchPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private string m_Title = "细切线";

        public event OnValueChangeEventHandler OnValueChange;

        public GridHatchPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
                IGridHatch pMapGrid = GridAxisPropertyPage.m_pMapGrid as IGridHatch;
                double num = double.Parse(this.txtHatchIntervalXDegree.Text);
                double num2 = double.Parse(this.txtHatchIntervalXMinute.Text);
                double num3 = double.Parse(this.txtHatchIntervalXSecond.Text);
                pMapGrid.HatchIntervalX = (num + (num2 / 60.0)) + (num3 / 3600.0);
                num = double.Parse(this.txtHatchIntervalYDegree.Text);
                num2 = double.Parse(this.txtHatchIntervalYMinute.Text);
                num3 = double.Parse(this.txtHatchIntervalYSecond.Text);
                pMapGrid.HatchIntervalY = (num + (num2 / 60.0)) + (num3 / 3600.0);
                pMapGrid.HatchLength = double.Parse(this.txtHatchLength.Text);
                pMapGrid.HatchDirectional = this.chkHatchDirectional.Checked;
                if (this.btnStyle.Style is ILineSymbol)
                {
                    pMapGrid.HatchLineSymbol = this.btnStyle.Style as ILineSymbol;
                }
                else
                {
                    pMapGrid.HatchMarkerSymbol = this.btnStyle.Style as IMarkerSymbol;
                }
                (pMapGrid as IGridAxisTicks).AxisTickOffset = double.Parse(this.txtAxisTickOffset.Text);
            }
        }

        public void Cancel()
        {
        }

        private void chkHatchDirectional_CheckedChanged(object sender, EventArgs e)
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

        public void DegreeToDMS(double Degree, out double d, out double m, out double s)
        {
            int num = Math.Sign(Degree);
            Degree = Math.Abs(Degree);
            d = Math.Floor(Degree);
            Degree = (Degree - d) * 60.0;
            m = Math.Floor(Degree);
            s = (Degree - m) * 60.0;
        }

 private void GridHatchPropertyPage_Load(object sender, EventArgs e)
        {
            double num;
            double num2;
            double num3;
            IGridHatch pMapGrid = GridAxisPropertyPage.m_pMapGrid as IGridHatch;
            this.DegreeToDMS(pMapGrid.HatchIntervalX, out num, out num2, out num3);
            this.txtHatchIntervalXDegree.Text = num.ToString();
            this.txtHatchIntervalXMinute.Text = num2.ToString();
            this.txtHatchIntervalXSecond.Text = num3.ToString();
            this.DegreeToDMS(pMapGrid.HatchIntervalY, out num, out num2, out num3);
            this.txtHatchIntervalYDegree.Text = num.ToString();
            this.txtHatchIntervalYMinute.Text = num2.ToString();
            this.txtHatchIntervalYSecond.Text = num3.ToString();
            this.txtHatchLength.Text = pMapGrid.HatchLength.ToString();
            this.chkHatchDirectional.Checked = pMapGrid.HatchDirectional;
            if (pMapGrid.HatchLineSymbol != null)
            {
                this.btnStyle.Style = pMapGrid.HatchLineSymbol;
            }
            else
            {
                this.btnStyle.Style = pMapGrid.HatchMarkerSymbol;
            }
            this.txtAxisTickOffset.Text = (pMapGrid as IGridAxisTicks).AxisTickOffset.ToString();
            this.m_CanDo = true;
        }

        public void Hide()
        {
        }

 public void SetObjects(object @object)
        {
        }

        private void txtAxisTickOffset_EditValueChanged(object sender, EventArgs e)
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

        private void txtHatchIntervalXDegree_EditValueChanged(object sender, EventArgs e)
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

        private void txtHatchLength_EditValueChanged(object sender, EventArgs e)
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

