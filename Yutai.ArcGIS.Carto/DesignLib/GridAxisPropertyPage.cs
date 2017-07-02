using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;
using IPropertyPage = Yutai.ArcGIS.Common.BaseClasses.IPropertyPage;
using IPropertyPageEvents = Yutai.ArcGIS.Common.BaseClasses.IPropertyPageEvents;
using OnValueChangeEventHandler = Yutai.ArcGIS.Common.BaseClasses.OnValueChangeEventHandler;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public partial class GridAxisPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        
        private IMapGridBorder imapGridBorder_0 = null;
        internal static IMapGrid m_pMapGrid;
        internal static IMapGrid m_pOldMapGrid;
        public static IStyleGallery m_pSG;
        private string string_0 = "坐标轴";

        public event OnValueChangeEventHandler OnValueChange;

        static GridAxisPropertyPage()
        {
            old_acctor_mc();
        }

        public GridAxisPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
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
                m_pMapGrid.Border = this.imapGridBorder_0;
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
                    this.bool_1 = true;
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
                    this.bool_1 = true;
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
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.cboBorderType.SelectedIndex == 0)
                {
                    this.imapGridBorder_0 = new SimpleMapGridBorderClass();
                }
                else if (this.cboBorderType.SelectedIndex == 1)
                {
                    this.imapGridBorder_0 = new CalibratedMapGridBorderClass();
                }
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void chkSubTop_CheckedChanged(object sender, EventArgs e)
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
                if (this.imapGridBorder_0 is ISimpleMapGridBorder)
                {
                    this.cboBorderType.SelectedIndex = 0;
                }
                else if (this.imapGridBorder_0 is ICalibratedMapGridBorder)
                {
                    this.cboBorderType.SelectedIndex = 1;
                }
                else
                {
                    this.cboBorderType.SelectedIndex = -1;
                }
            }
            this.bool_0 = true;
        }

        public void Hide()
        {
        }

 private static void old_acctor_mc()
        {
            m_pSG = null;
            m_pMapGrid = null;
            m_pOldMapGrid = null;
        }

        private void rdoSubTickPlace_EditValueChanged(object sender, EventArgs e)
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
            if (object_0 is IMapGrid)
            {
                m_pMapGrid = object_0 as IMapGrid;
                this.imapGridBorder_0 = m_pMapGrid.Border;
            }
        }

        private void txtSubTickLength_EditValueChanged(object sender, EventArgs e)
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

        private void txtTickLength_EditValueChanging(object sender, ChangingEventArgs e)
        {
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

