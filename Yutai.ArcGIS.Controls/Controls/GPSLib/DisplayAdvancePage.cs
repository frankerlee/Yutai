using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Controls.Controls.GPSLib
{
    public partial class DisplayAdvancePage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private IGpsDisplayProperties m_pGpsDisplayProperties = null;
        private IStyleGallery m_pSG = null;
        private string m_Title = "高级";

        public event OnValueChangeEventHandler OnValueChange;

        public DisplayAdvancePage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
                this.m_pGpsDisplayProperties.ShowCurrentAltitude = this.chkShowCurrentAltitude.Checked;
                this.m_pGpsDisplayProperties.ShowCurrentBearing = this.chkShowCurrentBearing.Checked;
                this.m_pGpsDisplayProperties.ShowCurrentSpeed = this.chkShowCurrentSpeed.Checked;
                this.m_pGpsDisplayProperties.ShowMarkerTrailAltitude = this.chkShowMarkerTrailAltitude.Checked;
                this.m_pGpsDisplayProperties.ShowMarkerTrailBearing = this.chkShowMarkerTrailBearing.Checked;
                this.m_pGpsDisplayProperties.ShowMarkerTrailSpeed = this.chkShowMarkerTrailSpeed.Checked;
                try
                {
                    this.m_pGpsDisplayProperties.HighAltitudeSize = double.Parse(this.txtHighAltitudeSize.Text);
                }
                catch
                {
                }
                try
                {
                    this.m_pGpsDisplayProperties.HighAltitudeValue = double.Parse(this.txtHighAltitudeValue.Text);
                }
                catch
                {
                }
                try
                {
                    this.m_pGpsDisplayProperties.LowAltitudeSize = double.Parse(this.txtLowAltitudeSize.Text);
                }
                catch
                {
                }
                try
                {
                    this.m_pGpsDisplayProperties.LowAltitudeValue = double.Parse(this.txtLowAltitudeValue.Text);
                }
                catch
                {
                }
                try
                {
                    this.m_pGpsDisplayProperties.LowSpeedValue = double.Parse(this.txtLowSpeedValue.Text);
                }
                catch
                {
                }
                if (this.cboSpeedColorRamp.SelectedIndex != -1)
                {
                    this.m_pGpsDisplayProperties.SpeedColorRamp = this.cboSpeedColorRamp.GetSelectColorRamp();
                }
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

        private void chkShowCurrentBearing_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.ValueChange();
            }
        }

        private void colorEdit1_TextChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.ValueChange();
            }
        }

        private void DisplayAdvancePage_Load(object sender, EventArgs e)
        {
            if (this.m_pGpsDisplayProperties != null)
            {
                IStyleGalleryItem item;
                this.chkShowCurrentAltitude.Checked = this.m_pGpsDisplayProperties.ShowCurrentAltitude;
                this.chkShowCurrentBearing.Checked = this.m_pGpsDisplayProperties.ShowCurrentBearing;
                this.chkShowCurrentSpeed.Checked = this.m_pGpsDisplayProperties.ShowCurrentSpeed;
                this.chkShowMarkerTrailAltitude.Checked = this.m_pGpsDisplayProperties.ShowMarkerTrailAltitude;
                this.chkShowMarkerTrailBearing.Checked = this.m_pGpsDisplayProperties.ShowMarkerTrailBearing;
                this.chkShowMarkerTrailSpeed.Checked = this.m_pGpsDisplayProperties.ShowMarkerTrailSpeed;
                this.txtHighAltitudeSize.Text = this.m_pGpsDisplayProperties.HighAltitudeSize.ToString();
                this.txtHighAltitudeValue.Text = this.m_pGpsDisplayProperties.HighAltitudeValue.ToString();
                this.txtLowAltitudeSize.Text = this.m_pGpsDisplayProperties.LowAltitudeSize.ToString();
                this.txtLowAltitudeValue.Text = this.m_pGpsDisplayProperties.LowAltitudeValue.ToString();
                this.txtLowSpeedValue.Text = this.m_pGpsDisplayProperties.LowSpeedValue.ToString();
                this.lblMaxAltituteUnit.Text = CommonHelper.GetUnit(this.m_pGpsDisplayProperties.AltitudeUnits);
                this.lblMinAltituteUnit.Text = this.lblMaxAltituteUnit.Text;
                this.lblMaxSpeedUnit.Text = this.GetSpeedUnitDescription(this.m_pGpsDisplayProperties.SpeedUnits);
                this.lblMinSpeedUnit.Text = this.lblMaxSpeedUnit.Text;
                if (this.m_pGpsDisplayProperties.SpeedColorRamp != null)
                {
                    item = new ServerStyleGalleryItemClass
                    {
                        Item = this.m_pGpsDisplayProperties.SpeedColorRamp
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

        private string GetSpeedUnitDescription(esriGpsSpeedUnits unit)
        {
            switch (unit)
            {
                case esriGpsSpeedUnits.esriGpsSpeedKph:
                    return "公里/小时";

                case esriGpsSpeedUnits.esriGpsSpeedMph:
                    return "英里/小时";

                case esriGpsSpeedUnits.esriGpsSpeedMps:
                    return "米/秒";

                case esriGpsSpeedUnits.esriGpsSpeedFps:
                    return "英尺/秒";

                case esriGpsSpeedUnits.esriGpsSpeedKnots:
                    return "节";
            }
            return "";
        }

        public void ResetControl()
        {
        }

        public void SetObjects(object @object)
        {
            this.m_pGpsDisplayProperties = @object as IGpsDisplayProperties;
        }

        private void txtLowSpeedValue_TextChanged(object sender, EventArgs e)
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

        public IStyleGallery StyleGallery
        {
            set { this.m_pSG = value; }
        }

        public string Title
        {
            get { return this.m_Title; }
            set { this.m_Title = value; }
        }
    }
}