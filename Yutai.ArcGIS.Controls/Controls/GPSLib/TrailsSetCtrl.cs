using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.Historical;
using Yutai.ArcGIS.Controls.SymbolUI;
using IPropertyPage = Yutai.ArcGIS.Common.BaseClasses.IPropertyPage;
using IPropertyPageEvents = Yutai.ArcGIS.Common.BaseClasses.IPropertyPageEvents;
using OnValueChangeEventHandler = Yutai.ArcGIS.Common.BaseClasses.OnValueChangeEventHandler;


namespace Yutai.ArcGIS.Controls.Controls.GPSLib
{
    public partial class TrailsSetCtrl : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private IPositionTrails m_pPositionTrails = null;
        private IStyleGallery m_pSG = null;
        private string m_Title = "轨迹";


        public event OnValueChangeEventHandler OnValueChange;
        public TrailsSetCtrl()
        {
            this.InitializeComponent();
        }

    

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
                this.m_pPositionTrails.ShowLinearTrail = this.chkLineTrail.Checked;
                this.m_pPositionTrails.ShowMarkerTrails = this.chkPointTrail.Checked;
                this.m_pPositionTrails.LinearTrailSymbol = this.btnLineSymbol.Style as ILineSymbol;
                this.m_pPositionTrails.MarkerTrailSymbol = this.btnPointSymbol.Style as IMarkerSymbol;
                try
                {
                    this.m_pPositionTrails.MarkerTrailDistance = double.Parse(this.txtDistance.Text);
                }
                catch
                {
                }
                try
                {
                    this.m_pPositionTrails.MarkerTrailCount = int.Parse(this.txtPointNum.Text);
                }
                catch
                {
                }
                try
                {
                    this.m_pPositionTrails.LinearTrailDistance = double.Parse(this.txtLineLength.Text);
                }
                catch
                {
                }
                if (this.cboSpeedColorRamp.SelectedIndex != -1)
                {
                    this.m_pPositionTrails.MarkerTrailColorRamp = this.cboSpeedColorRamp.GetSelectColorRamp();
                }
            }
        }

        private void btnLineSymbol_Click(object sender, EventArgs e)
        {
            try
            {
                ILineSymbol style = this.btnLineSymbol.Style as ILineSymbol;
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.m_pSG);
                    selector.SetSymbol(style);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        style = selector.GetSymbol() as ILineSymbol;
                        this.btnLineSymbol.Style = style;
                        this.ValueChange();
                    }
                }
            }
            catch
            {
            }
        }

        private void btnPointSymbol_Click(object sender, EventArgs e)
        {
            try
            {
                IMarkerSymbol style = this.btnPointSymbol.Style as IMarkerSymbol;
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.m_pSG);
                    selector.SetSymbol(style);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        style = selector.GetSymbol() as IMarkerSymbol;
                        this.btnPointSymbol.Style = style;
                        this.ValueChange();
                    }
                }
            }
            catch
            {
            }
        }

        public void Cancel()
        {
            this.m_IsPageDirty = false;
        }

        private void cboSpeedColorRamp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.ValueChange();
            }
        }

        private void chkLineTrail_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.ValueChange();
            }
        }

        private void chkPointTrail_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.ValueChange();
            }
        }

 public void ResetControl()
        {
        }

        public void SetObjects(object @object)
        {
            this.m_pPositionTrails = @object as IPositionTrails;
        }

        private void TrailsSetCtrl_Load(object sender, EventArgs e)
        {
            if (this.m_pPositionTrails != null)
            {
                IStyleGalleryItem item;
                this.chkLineTrail.Checked = this.m_pPositionTrails.ShowLinearTrail;
                this.chkPointTrail.Checked = this.m_pPositionTrails.ShowMarkerTrails;
                this.btnLineSymbol.Style = this.m_pPositionTrails.LinearTrailSymbol;
                this.btnPointSymbol.Style = this.m_pPositionTrails.MarkerTrailSymbol;
                this.txtDistance.Text = this.m_pPositionTrails.MarkerTrailDistance.ToString();
                this.txtPointNum.Text = this.m_pPositionTrails.MarkerTrailCount.ToString();
                this.txtLineLength.Text = this.m_pPositionTrails.LinearTrailDistance.ToString();
                if (this.m_pPositionTrails.MarkerTrailColorRamp != null)
                {
                    item = new ServerStyleGalleryItemClass {
                        Item = this.m_pPositionTrails.MarkerTrailColorRamp
                    };
                    this.cboSpeedColorRamp.Add(item);
                }
                if (this.m_pSG != null)
                {
                    IEnumStyleGalleryItem item2 = this.m_pSG.get_Items("Color Ramps", "", "");
                    item2.Reset();
                    for (item = item2.Next(); item != null; item = item2.Next())
                    {
                        this.cboSpeedColorRamp.Add(item);
                    }
                    item2 = null;
                    GC.Collect();
                }
                if (this.cboSpeedColorRamp.Items.Count > 0)
                {
                    this.cboSpeedColorRamp.SelectedIndex = 0;
                }
                else
                {
                    this.cboSpeedColorRamp.Enabled = false;
                }
                this.m_CanDo = true;
            }
        }

        private void txtDistance_TextChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.ValueChange();
            }
        }

        private void txtLineLength_TextChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.ValueChange();
            }
        }

        private void txtPointNum_TextChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.ValueChange();
            }
        }

        private void ValueChange()
        {
            this.m_IsPageDirty = true;
            if (this.OnValueChange != null)
            {
                this.OnValueChange();
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

        public IStyleGallery StyleGallery
        {
            set
            {
                this.m_pSG = value;
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

