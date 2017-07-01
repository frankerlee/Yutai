using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.Controls.GPSLib
{
    public partial class frmGPSPositionWindows : Form
    {
        public frmGPSPositionWindows()
        {
            this.InitializeComponent();
        }

        private void frmGPSPositionWindows_Load(object sender, EventArgs e)
        {
            GPSManager.GpsPositionUpdated += new GPSManager.GpsPositionUpdatedHandler(this.GPSManager_GpsPositionUpdated);
            GPSManager.ConnectionStatusUpdated +=
                new GPSManager.ConnectionStatusUpdatedEventHandler(this.GPSManager_ConnectionStatusUpdated);
            GPSManager.DateTimeUpdated += new GPSManager.DateTimeUpdatedEventHandler(this.GPSManager_DateTimeUpdated);
            GPSManager.DgpsInfoUpdated += new GPSManager.DgpsInfoUpdatedEventHandler(this.GPSManager_DgpsInfoUpdated);
            GPSManager.DopInfoUpdated += new GPSManager.DopInfoUpdatedEventHandler(this.GPSManager_DopInfoUpdated);
            GPSManager.GroundCourseUpdated +=
                new GPSManager.GroundCourseUpdatedEventHandler(this.GPSManager_GroundCourseUpdated);
            GPSManager.MagneticVarianceUpdated +=
                new GPSManager.MagneticVarianceUpdatedEventHandler(this.GPSManager_MagneticVarianceUpdated);
            GPSManager.SatelliteInfoUpdated +=
                new GPSManager.SatelliteInfoUpdatedEventHandler(this.GPSManager_SatelliteInfoUpdated);
            if (GPSManager.CurrentGpsPositionInfoIsValid)
            {
                this.lblLat.Text = GPSManager.CurrentGpsPositionInfo.latitude.ToString("0.###");
                this.lblLong.Text = GPSManager.CurrentGpsPositionInfo.longitude.ToString("0.###");
                if (GPSManager.CurrentGpsPositionInfo.altitudeValid == 1)
                {
                    this.lblAlti.Text = GPSManager.CurrentGpsPositionInfo.altitude.ToString("0.###");
                }
                else
                {
                    this.lblAlti.Text = "N/A";
                }
            }
        }

        private string GetConnectionDescription(esriGpsConnectionStatus Status)
        {
            switch (Status)
            {
                case esriGpsConnectionStatus.esriGpsConnectionStatusClosed:
                    return "关闭";

                case esriGpsConnectionStatus.esriGpsConnectionStatusOpen:
                    return "端口打开，没有接收数据";

                case esriGpsConnectionStatus.esriGpsConnectionStatusNoSignal:
                    return "没有信号";

                case esriGpsConnectionStatus.esriGpsConnectionStatusPoorSignal:
                    return "信号弱";

                case esriGpsConnectionStatus.esriGpsConnectionStatusReceiving:
                    return "接收数据";
            }
            return "未知状态";
        }

        private void GPSManager_ConnectionStatusUpdated(esriGpsConnectionStatus pConnectionStatus)
        {
            this.lblStatue.Text = this.GetConnectionDescription(pConnectionStatus);
        }

        private void GPSManager_DateTimeUpdated(esriGpsDateTime pNewDateTime)
        {
            if (pNewDateTime.dateValid == 1)
            {
                this.lblUCTDate.Text = pNewDateTime.year.ToString() + "-" + pNewDateTime.month.ToString() + "-" +
                                       pNewDateTime.day.ToString();
            }
            else
            {
                this.lblUCTDate.Text = "N/A";
            }
            if (pNewDateTime.timeValid == 1)
            {
                this.lblUCTTime.Text = pNewDateTime.hour.ToString() + ":" + pNewDateTime.minute.ToString() + ":" +
                                       pNewDateTime.seconds.ToString("0.###");
            }
            else
            {
                this.lblUCTTime.Text = "N/A";
            }
        }

        private void GPSManager_DgpsInfoUpdated(esriGpsDgpsInfo pNewDGPSInfo)
        {
            if (pNewDGPSInfo.ageValid == 1)
            {
                this.lblAge.Text = pNewDGPSInfo.age.ToString("0.###");
            }
            else
            {
                this.lblAge.Text = "N/A";
            }
            if (pNewDGPSInfo.idValid == 1)
            {
                this.lblStateID.Text = pNewDGPSInfo.stationID.ToString();
            }
            else
            {
                this.lblStateID.Text = "N/A";
            }
            if (GPSManager.RealTimeFeed is IGpsFeed)
            {
                if (pNewDGPSInfo.idValid == 1)
                {
                    this.lblQ.Text = "差分GPS";
                }
                else
                {
                    this.lblQ.Text = "单点定位";
                }
            }
            else
            {
                this.lblQ.Text = "N/A";
            }
        }

        private void GPSManager_DopInfoUpdated(esriGpsDOPInfo pdop)
        {
            if (pdop.hdopValid == 1)
            {
                this.lblHDOP.Text = pdop.hdop.ToString("0.###");
            }
            else
            {
                this.lblHDOP.Text = "N/A";
            }
            if (pdop.vdopValid == 1)
            {
                this.lblVDOP.Text = pdop.vdop.ToString("0.###");
            }
            else
            {
                this.lblVDOP.Text = "N/A";
            }
            if (pdop.pdopValid == 1)
            {
                this.lblPDOP.Text = pdop.pdop.ToString("0.###");
            }
            else
            {
                this.lblPDOP.Text = "N/A";
            }
        }

        private void GPSManager_GpsPositionUpdated(esriGpsPositionInfo position)
        {
            if (position.satellitesInUseValid == 1)
            {
                this.lblInuseStateCount.Text = position.satellitesInUse.ToString();
            }
            else
            {
                this.lblInuseStateCount.Text = "N/A";
            }
            this.lblLat.Text = position.latitude.ToString("0.###");
            this.lblLong.Text = position.longitude.ToString("0.###");
            if (position.altitudeValid == 1)
            {
                this.lblAlti.Text = position.altitude.ToString("0.###");
            }
            else
            {
                this.lblAlti.Text = "N/A";
            }
        }

        private void GPSManager_GroundCourseUpdated(esriGpsGroundCourse pGroundCourse)
        {
            if (pGroundCourse.headingValid == 1)
            {
                this.lblHeading.Text = pGroundCourse.Heading.ToString("0.###");
            }
            else
            {
                this.lblHeading.Text = "N/A";
            }
            if (pGroundCourse.speedValid == 1)
            {
                this.lblSpeed.Text = pGroundCourse.speed.ToString("0.###");
            }
            else
            {
                this.lblSpeed.Text = "N/A";
            }
        }

        private void GPSManager_MagneticVarianceUpdated(esriGpsMagneticVariance pMagneticVar)
        {
            if (pMagneticVar.magVarValid == 1)
            {
                this.lblMag.Text = pMagneticVar.magneticVariance.ToString("0.###");
            }
            else
            {
                this.lblMag.Text = "N/A";
            }
        }

        private void GPSManager_SatelliteInfoUpdated(int satelliteCount)
        {
            this.lblStateCount.Text = satelliteCount.ToString();
            if (satelliteCount >= 1)
            {
                try
                {
                    esriGpsSatelliteData data = GPSManager.RealTimeFeed.get_CurrentSatelliteData(0);
                    this.lblSNR.Text = data.snr.ToString();
                }
                catch
                {
                    this.lblSNR.Text = "N/A";
                }
            }
            else
            {
                this.lblSNR.Text = "N/A";
            }
        }
    }
}