using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;
using IPropertyPage = Yutai.ArcGIS.Common.BaseClasses.IPropertyPage;
using IPropertyPageEvents = Yutai.ArcGIS.Common.BaseClasses.IPropertyPageEvents;
using OnValueChangeEventHandler = Yutai.ArcGIS.Common.BaseClasses.OnValueChangeEventHandler;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public partial class GridAxisPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Container container_0 = null;
        private IMapGrid imapGrid_0 = null;
        private IMapGridBorder imapGridBorder_0 = null;
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
            m_pSG = ApplicationBase.StyleGallery;
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                this.imapGrid_0.SetTickVisibility(this.chkMainLeft.Checked, this.chkMainTop.Checked,
                    this.chkMainRight.Checked, this.chkMainBottom.Checked);
                this.imapGrid_0.TickLineSymbol = this.btnStyle.Style as ILineSymbol;
                double num = Convert.ToDouble(this.txtTikLength.Text);
                if (this.rdoInInner.Checked)
                {
                    num = -num;
                }
                this.imapGrid_0.TickLength = num;
                this.imapGrid_0.SetSubTickVisibility(this.chkSubLeft.Checked, this.chkSubTop.Checked,
                    this.chkSubRight.Checked, this.chkSubBottom.Checked);
                this.imapGrid_0.SubTickLineSymbol = this.btnSubStyle.Style as ILineSymbol;
                num = Convert.ToDouble(this.txtSubTickLength.Text);
                if (this.rdoSubTickInner.Checked)
                {
                    num = -num;
                }
                this.imapGrid_0.SubTickLength = num;
                this.imapGrid_0.SubTickCount = Convert.ToInt16(this.txtSubCount.Text);
                this.imapGrid_0.Border = this.imapGridBorder_0;
            }
        }

        private void btnStyle_Click(object sender, EventArgs e)
        {
            frmSymbolSelector selector = new frmSymbolSelector();
            if (selector != null)
            {
                selector.SetStyleGallery(ApplicationBase.StyleGallery);
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
                selector.SetStyleGallery(ApplicationBase.StyleGallery);
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
            this.imapGrid_0 = this.MapTemplate.MapGrid;
            this.Text = "坐标轴";
            if (this.imapGrid_0 != null)
            {
                bool leftVis = false;
                bool topVis = false;
                bool bottomVis = false;
                bool rightVis = false;
                this.imapGrid_0.QueryTickVisibility(ref leftVis, ref topVis, ref rightVis, ref bottomVis);
                this.chkMainBottom.Checked = bottomVis;
                this.chkMainLeft.Checked = leftVis;
                this.chkMainRight.Checked = rightVis;
                this.chkMainTop.Checked = topVis;
                this.btnStyle.Style = this.imapGrid_0.TickLineSymbol;
                double tickLength = this.imapGrid_0.TickLength;
                if (tickLength < 0.0)
                {
                    this.rdoInInner.Checked = true;
                }
                else
                {
                    this.rdoInOutside.Checked = true;
                }
                this.txtTikLength.Text = string.Format("{0}", Math.Abs(tickLength));
                this.imapGrid_0.QuerySubTickVisibility(ref leftVis, ref topVis, ref rightVis, ref bottomVis);
                this.chkSubBottom.Checked = bottomVis;
                this.chkSubLeft.Checked = leftVis;
                this.chkSubRight.Checked = rightVis;
                this.chkSubTop.Checked = topVis;
                this.btnSubStyle.Style = this.imapGrid_0.SubTickLineSymbol;
                tickLength = this.imapGrid_0.SubTickLength;
                if (tickLength < 0.0)
                {
                    this.rdoSubTickInner.Checked = true;
                }
                else
                {
                    this.rdoSubTickOutside.Checked = true;
                }
                this.txtSubTickLength.Text = string.Format("{0}", Math.Abs(tickLength));
                this.txtSubCount.Text = this.imapGrid_0.SubTickCount.ToString();
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

        private void method_0(object sender, EventArgs e)
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

        private void method_1(object sender, EventArgs e)
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

        private void method_2(object sender, ChangingEventArgs e)
        {
        }

        private static void old_acctor_mc()
        {
            m_pSG = null;
            m_pOldMapGrid = null;
        }

        public void ResetControl()
        {
        }

        public void SetObjects(object object_0)
        {
            this.MapTemplate = object_0 as MapCartoTemplateLib.MapTemplate;
            this.imapGrid_0 = this.MapTemplate.MapGrid;
            this.imapGridBorder_0 = this.imapGrid_0.Border;
        }

        public bool IsPageDirty
        {
            get { return this.bool_1; }
        }

        int IPropertyPage.Height
        {
            get { return base.Height; }
        }

        int IPropertyPage.Width
        {
            get { return base.Width; }
        }

        public MapCartoTemplateLib.MapTemplate MapTemplate { get; set; }

        public string Title
        {
            get { return this.string_0; }
            set { this.string_0 = value; }
        }
    }
}