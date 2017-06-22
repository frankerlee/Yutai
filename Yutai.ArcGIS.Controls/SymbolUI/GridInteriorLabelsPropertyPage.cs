using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    public partial class GridInteriorLabelsPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private string m_Title = "内部标注";

        public event OnValueChangeEventHandler OnValueChange;

        public GridInteriorLabelsPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
                IGridInteriorLabels pMapGrid = GridAxisPropertyPage.m_pMapGrid as IGridInteriorLabels;
                pMapGrid.ShowInteriorLabels = this.chkShowInteriorLabels.Checked;
                double num = double.Parse(this.txtHatchIntervalXDegree.Text);
                double num2 = double.Parse(this.txtHatchIntervalXMinute.Text);
                double num3 = double.Parse(this.txtHatchIntervalXSecond.Text);
                pMapGrid.InteriorLabelIntervalX = (num + (num2 / 60.0)) + (num3 / 3600.0);
                num = double.Parse(this.txtHatchIntervalYDegree.Text);
                num2 = double.Parse(this.txtHatchIntervalYMinute.Text);
                num3 = double.Parse(this.txtHatchIntervalYSecond.Text);
                pMapGrid.InteriorLabelIntervalY = (num + (num2 / 60.0)) + (num3 / 3600.0);
            }
        }

        public void Cancel()
        {
        }

        private void chkShowInteriorLabels_CheckedChanged(object sender, EventArgs e)
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

 private void GridInteriorLabelsPropertyPage_Load(object sender, EventArgs e)
        {
            double num;
            double num2;
            double num3;
            IGridInteriorLabels pMapGrid = GridAxisPropertyPage.m_pMapGrid as IGridInteriorLabels;
            this.chkShowInteriorLabels.Checked = pMapGrid.ShowInteriorLabels;
            this.DegreeToDMS(pMapGrid.InteriorLabelIntervalX, out num, out num2, out num3);
            this.txtHatchIntervalXDegree.Text = num.ToString();
            this.txtHatchIntervalXMinute.Text = num2.ToString();
            this.txtHatchIntervalXSecond.Text = num3.ToString();
            this.DegreeToDMS(pMapGrid.InteriorLabelIntervalY, out num, out num2, out num3);
            this.txtHatchIntervalYDegree.Text = num.ToString();
            this.txtHatchIntervalYMinute.Text = num2.ToString();
            this.txtHatchIntervalYSecond.Text = num3.ToString();
            this.m_CanDo = true;
        }

        public void Hide()
        {
        }

 public void SetObjects(object @object)
        {
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

        private void txtHatchIntervalXMinute_EditValueChanged(object sender, EventArgs e)
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

        private void txtHatchIntervalXSecond_EditValueChanged(object sender, EventArgs e)
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

        private void txtHatchIntervalYDegree_EditValueChanged(object sender, EventArgs e)
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

        private void txtHatchIntervalYMinute_EditValueChanged(object sender, EventArgs e)
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

        private void txtHatchIntervalYSecond_EditValueChanged(object sender, EventArgs e)
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
            }
        }
    }
}

