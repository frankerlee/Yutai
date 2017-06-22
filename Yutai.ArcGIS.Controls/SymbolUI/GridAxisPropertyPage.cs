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
    public partial class GridAxisPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private IMapGridBorder m_pBorder = null;
        internal static IMapGrid m_pMapGrid = null;
        internal static IMapGrid m_pOldMapGrid = null;
        public static IStyleGallery m_pSG = null;
        private string m_Title = "坐标轴";

        public event OnValueChangeEventHandler OnValueChange;

        public GridAxisPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
                m_pMapGrid.SetTickVisibility(this.chkMainLeft.Checked, this.chkMainTop.Checked, this.chkMainRight.Checked, this.chkMainBottom.Checked);
                m_pMapGrid.TickLineSymbol = this.btnStyle.Style as ILineSymbol;
                double num = (double) this.txtTickLength.Value;
                num = (this.rdoTickPalce.SelectedIndex == 0) ? -num : num;
                m_pMapGrid.TickLength = num;
                m_pMapGrid.SetSubTickVisibility(this.chkSubLeft.Checked, this.chkSubTop.Checked, this.chkSubRight.Checked, this.chkSubBottom.Checked);
                m_pMapGrid.SubTickLineSymbol = this.btnSubStyle.Style as ILineSymbol;
                num = (double) this.txtSubTickLength.Value;
                num = (this.rdoSubTickPlace.SelectedIndex == 0) ? -num : num;
                m_pMapGrid.SubTickLength = num;
                m_pMapGrid.SubTickCount = (short) this.txtSubCount.Value;
                m_pMapGrid.Border = this.m_pBorder;
            }
        }

        private void btnStyle_Click(object sender, EventArgs e)
        {
            frmSymbolSelector selector = new frmSymbolSelector();
            if (selector != null)
            {
                selector.SetStyleGallery(m_pSG);
                selector.SetSymbol(this.btnStyle.Style);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    this.m_IsPageDirty = true;
                    this.btnStyle.Style = selector.GetSymbol();
                    if (this.OnValueChange != null)
                    {
                        this.OnValueChange();
                    }
                }
            }
        }

        private void btnSubStyle_Click(object sender, EventArgs e)
        {
            frmSymbolSelector selector = new frmSymbolSelector();
            if (selector != null)
            {
                selector.SetStyleGallery(m_pSG);
                selector.SetSymbol(this.btnSubStyle.Style);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    this.m_IsPageDirty = true;
                    this.btnSubStyle.Style = selector.GetSymbol();
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

        private void cboBorderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.cboBorderType.SelectedIndex == 0)
                {
                    this.m_pBorder = new SimpleMapGridBorderClass();
                }
                else if (this.cboBorderType.SelectedIndex == 1)
                {
                    this.m_pBorder = new CalibratedMapGridBorderClass();
                }
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void chkMainTop_CheckedChanged(object sender, EventArgs e)
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

 private void GridAxisPropertyPage_Load(object sender, EventArgs e)
        {
            this.Text = "坐标轴";
            if (m_pMapGrid != null)
            {
                bool leftVis = false;
                bool topVis = false;
                bool bottomVis = false;
                bool rightVis = false;
                m_pMapGrid.QueryTickVisibility(ref leftVis, ref topVis, ref rightVis, ref bottomVis);
                this.chkMainBottom.Checked = bottomVis;
                this.chkMainLeft.Checked = leftVis;
                this.chkMainRight.Checked = rightVis;
                this.chkMainTop.Checked = topVis;
                this.btnStyle.Style = m_pMapGrid.TickLineSymbol;
                double tickLength = m_pMapGrid.TickLength;
                this.rdoTickPalce.SelectedIndex = (tickLength < 0.0) ? 0 : 1;
                this.txtTickLength.Value = (decimal) Math.Abs(tickLength);
                m_pMapGrid.QuerySubTickVisibility(ref leftVis, ref topVis, ref rightVis, ref bottomVis);
                this.chkSubBottom.Checked = bottomVis;
                this.chkSubLeft.Checked = leftVis;
                this.chkSubRight.Checked = rightVis;
                this.chkSubTop.Checked = topVis;
                this.btnSubStyle.Style = m_pMapGrid.SubTickLineSymbol;
                tickLength = m_pMapGrid.SubTickLength;
                this.rdoSubTickPlace.SelectedIndex = (tickLength < 0.0) ? 0 : 1;
                this.txtSubTickLength.Value = (decimal) Math.Abs(tickLength);
                this.txtSubCount.Text = m_pMapGrid.SubTickCount.ToString();
                if (this.m_pBorder is ISimpleMapGridBorder)
                {
                    this.cboBorderType.SelectedIndex = 0;
                }
                else if (this.m_pBorder is ICalibratedMapGridBorder)
                {
                    this.cboBorderType.SelectedIndex = 1;
                }
                else
                {
                    this.cboBorderType.SelectedIndex = -1;
                }
            }
            this.m_CanDo = true;
        }

        public void Hide()
        {
        }

 private void rdoTickPalce_SelectedIndexChanged(object sender, EventArgs e)
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

        public void SetObjects(object @object)
        {
            if (@object is IMapGrid)
            {
                m_pMapGrid = @object as IMapGrid;
                this.m_pBorder = m_pMapGrid.Border;
            }
        }

        private void txtTickLength_EditValueChanged(object sender, EventArgs e)
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

