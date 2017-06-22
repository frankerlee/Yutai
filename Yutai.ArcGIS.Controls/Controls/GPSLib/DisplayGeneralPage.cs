using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.Historical;
using Yutai.ArcGIS.Controls.SymbolUI;
using IPropertyPage = Yutai.ArcGIS.Common.BaseClasses.IPropertyPage;
using IPropertyPageEvents = Yutai.ArcGIS.Common.BaseClasses.IPropertyPageEvents;
using OnValueChangeEventHandler = Yutai.ArcGIS.Common.BaseClasses.OnValueChangeEventHandler;


namespace Yutai.ArcGIS.Controls.Controls.GPSLib
{
    public partial class DisplayGeneralPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private IGpsDisplayProperties m_pGpsDisplayProperties = null;
        private IStyleGallery m_pSG = null;
        private string m_Title = "常规";

        public event OnValueChangeEventHandler OnValueChange;

        public DisplayGeneralPage()
        {
            this.InitializeComponent();
        }

       
        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
                this.m_pGpsDisplayProperties.ShowEstimatedPosition = this.chkShowCurrentPosition.Checked;
                this.m_pGpsDisplayProperties.ShowEstimatedPosition = this.chkShowEstimatedPosition.Checked;
                this.m_pGpsDisplayProperties.UseMinimumDisplayRate = this.chkUseMinimumDisplayRate.Checked;
                try
                {
                    this.m_pGpsDisplayProperties.MinimumDisplayRate = double.Parse(this.txtMinimumDisplayRate.Text);
                }
                catch
                {
                }
                this.m_pGpsDisplayProperties.BaseMarkerSymbol = (this.btnBaseMarkerSymbol.Style as IClone).Clone() as IMarkerSymbol;
                this.m_pGpsDisplayProperties.EstimatedPositionSymbol = (this.btnEstimatedPositionSymbol.Style as IClone).Clone() as IMarkerSymbol;
                this.m_pGpsDisplayProperties.AltitudeUnits = (esriUnits) this.cboAltitudeUnits.SelectedIndex;
                this.m_pGpsDisplayProperties.LatLongDisplayFormat = (esriGpsLatLongFormat) this.cboLatLongDisplayFormat.SelectedIndex;
                this.m_pGpsDisplayProperties.SpeedUnits = (esriGpsSpeedUnits) this.cboSpeedUnits.SelectedIndex;
            }
        }

        private void btnBaseMarkerSymbol_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.m_pSG);
                    selector.SetSymbol(this.m_pGpsDisplayProperties.BaseMarkerSymbol);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        this.btnBaseMarkerSymbol.Style = selector.GetSymbol();
                        this.ValueChange();
                    }
                }
            }
            catch
            {
            }
        }

        private void btnEstimatedPositionSymbol_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.m_pSG);
                    selector.SetSymbol(this.m_pGpsDisplayProperties.EstimatedPositionSymbol);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        this.btnEstimatedPositionSymbol.Style = selector.GetSymbol();
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

        private void cboAltitudeUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.ValueChange();
            }
        }

        private void cboLatLongDisplayFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.ValueChange();
            }
        }

        private void cboSpeedUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.ValueChange();
            }
        }

        private void chkShowCurrentPosition_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.ValueChange();
            }
        }

        private void chkShowEstimatedPosition_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.ValueChange();
            }
        }

        private void chkUseMinimumDisplayRate_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.ValueChange();
            }
        }

        private void DisplayGeneralPage_Load(object sender, EventArgs e)
        {
            if (this.m_pGpsDisplayProperties != null)
            {
                this.chkShowCurrentPosition.Checked = this.m_pGpsDisplayProperties.ShowEstimatedPosition;
                this.chkShowEstimatedPosition.Checked = this.m_pGpsDisplayProperties.ShowEstimatedPosition;
                this.chkUseMinimumDisplayRate.Checked = this.m_pGpsDisplayProperties.UseMinimumDisplayRate;
                this.txtMinimumDisplayRate.Text = this.m_pGpsDisplayProperties.MinimumDisplayRate.ToString();
                this.btnBaseMarkerSymbol.Style = this.m_pGpsDisplayProperties.BaseMarkerSymbol;
                this.btnEstimatedPositionSymbol.Style = this.m_pGpsDisplayProperties.EstimatedPositionSymbol;
                this.cboAltitudeUnits.SelectedIndex = (int) this.m_pGpsDisplayProperties.AltitudeUnits;
                this.cboLatLongDisplayFormat.SelectedIndex = (int) this.m_pGpsDisplayProperties.LatLongDisplayFormat;
                this.cboSpeedUnits.SelectedIndex = (int) this.m_pGpsDisplayProperties.SpeedUnits;
            }
        }

 private void groupBox2_Enter(object sender, EventArgs e)
        {
        }

 public void ResetControl()
        {
        }

        public void SetObjects(object @object)
        {
            this.m_pGpsDisplayProperties = @object as IGpsDisplayProperties;
        }

        private void txtMinimumDisplayRate_TextChanged(object sender, EventArgs e)
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

