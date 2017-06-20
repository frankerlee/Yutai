using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.Controls.GPSLib
{
    public class frmGPSPositionWindows : Form
    {
        private IContainer components = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private GroupBox groupBox5;
        private Label label1;
        private Label label10;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label16;
        private Label label18;
        private Label label20;
        private Label label22;
        private Label label24;
        private Label label25;
        private Label label26;
        private Label label27;
        private Label label28;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label lblAge;
        private Label lblAlti;
        private Label lblHDOP;
        private Label lblHeading;
        private Label lblInuseStateCount;
        private Label lblLat;
        private Label lblLong;
        private Label lblMag;
        private Label lblPDOP;
        private Label lblQ;
        private Label lblSNR;
        private Label lblSpeed;
        private Label lblStateCount;
        private Label lblStateID;
        private Label lblStatue;
        private Label lblUCTDate;
        private Label lblUCTTime;
        private Label lblVDOP;

        public frmGPSPositionWindows()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmGPSPositionWindows_Load(object sender, EventArgs e)
        {
            GPSManager.GpsPositionUpdated += new GPSManager.GpsPositionUpdatedHandler(this.GPSManager_GpsPositionUpdated);
            GPSManager.ConnectionStatusUpdated += new GPSManager.ConnectionStatusUpdatedEventHandler(this.GPSManager_ConnectionStatusUpdated);
            GPSManager.DateTimeUpdated += new GPSManager.DateTimeUpdatedEventHandler(this.GPSManager_DateTimeUpdated);
            GPSManager.DgpsInfoUpdated += new GPSManager.DgpsInfoUpdatedEventHandler(this.GPSManager_DgpsInfoUpdated);
            GPSManager.DopInfoUpdated += new GPSManager.DopInfoUpdatedEventHandler(this.GPSManager_DopInfoUpdated);
            GPSManager.GroundCourseUpdated += new GPSManager.GroundCourseUpdatedEventHandler(this.GPSManager_GroundCourseUpdated);
            GPSManager.MagneticVarianceUpdated += new GPSManager.MagneticVarianceUpdatedEventHandler(this.GPSManager_MagneticVarianceUpdated);
            GPSManager.SatelliteInfoUpdated += new GPSManager.SatelliteInfoUpdatedEventHandler(this.GPSManager_SatelliteInfoUpdated);
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
                this.lblUCTDate.Text = pNewDateTime.year.ToString() + "-" + pNewDateTime.month.ToString() + "-" + pNewDateTime.day.ToString();
            }
            else
            {
                this.lblUCTDate.Text = "N/A";
            }
            if (pNewDateTime.timeValid == 1)
            {
                this.lblUCTTime.Text = pNewDateTime.hour.ToString() + ":" + pNewDateTime.minute.ToString() + ":" + pNewDateTime.seconds.ToString("0.###");
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

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGPSPositionWindows));
            this.groupBox1 = new GroupBox();
            this.label13 = new Label();
            this.label9 = new Label();
            this.label10 = new Label();
            this.lblHeading = new Label();
            this.label12 = new Label();
            this.lblSpeed = new Label();
            this.label6 = new Label();
            this.lblAlti = new Label();
            this.label8 = new Label();
            this.lblLat = new Label();
            this.label4 = new Label();
            this.lblLong = new Label();
            this.label1 = new Label();
            this.lblStatue = new Label();
            this.label3 = new Label();
            this.groupBox2 = new GroupBox();
            this.lblUCTDate = new Label();
            this.label20 = new Label();
            this.lblUCTTime = new Label();
            this.label22 = new Label();
            this.label7 = new Label();
            this.lblQ = new Label();
            this.label16 = new Label();
            this.lblMag = new Label();
            this.label18 = new Label();
            this.groupBox3 = new GroupBox();
            this.lblStateCount = new Label();
            this.lblSNR = new Label();
            this.label24 = new Label();
            this.lblInuseStateCount = new Label();
            this.label26 = new Label();
            this.groupBox4 = new GroupBox();
            this.lblAge = new Label();
            this.label25 = new Label();
            this.lblStateID = new Label();
            this.label28 = new Label();
            this.groupBox5 = new GroupBox();
            this.lblPDOP = new Label();
            this.label27 = new Label();
            this.lblVDOP = new Label();
            this.label5 = new Label();
            this.lblHDOP = new Label();
            this.label14 = new Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.lblHeading);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.lblSpeed);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.lblAlti);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lblLat);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblLong);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xce, 0x7d);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.label13.AutoSize = true;
            this.label13.Location = new Point(0x89, 0x68);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x11, 12);
            this.label13.TabIndex = 12;
            this.label13.Text = "度";
            this.label9.AutoSize = true;
            this.label9.Location = new Point(0x89, 0x52);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x3b, 12);
            this.label9.TabIndex = 11;
            this.label9.Text = "公里/小时";
            this.label10.AutoSize = true;
            this.label10.Location = new Point(0x89, 60);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x11, 12);
            this.label10.TabIndex = 10;
            this.label10.Text = "米";
            this.lblHeading.AutoSize = true;
            this.lblHeading.Location = new Point(0x31, 0x68);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new Size(0x17, 12);
            this.lblHeading.TabIndex = 9;
            this.lblHeading.Text = "N/A";
            this.label12.AutoSize = true;
            this.label12.Location = new Point(14, 0x68);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x1d, 12);
            this.label12.TabIndex = 8;
            this.label12.Text = "航向";
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.Location = new Point(0x31, 0x52);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new Size(0x17, 12);
            this.lblSpeed.TabIndex = 7;
            this.lblSpeed.Text = "N/A";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(14, 0x52);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x1d, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "速度";
            this.lblAlti.AutoSize = true;
            this.lblAlti.Location = new Point(0x31, 60);
            this.lblAlti.Name = "lblAlti";
            this.lblAlti.Size = new Size(0x17, 12);
            this.lblAlti.TabIndex = 5;
            this.lblAlti.Text = "N/A";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(14, 60);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x1d, 12);
            this.label8.TabIndex = 4;
            this.label8.Text = "高度";
            this.lblLat.AutoSize = true;
            this.lblLat.Location = new Point(0x31, 0x26);
            this.lblLat.Name = "lblLat";
            this.lblLat.Size = new Size(0x17, 12);
            this.lblLat.TabIndex = 3;
            this.lblLat.Text = "N/A";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(14, 0x26);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x1d, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "纬度";
            this.lblLong.AutoSize = true;
            this.lblLong.Location = new Point(0x31, 0x10);
            this.lblLong.Name = "lblLong";
            this.lblLong.Size = new Size(0x17, 12);
            this.lblLong.TabIndex = 1;
            this.lblLong.Text = "N/A";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(14, 0x10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "经度";
            this.lblStatue.AutoSize = true;
            this.lblStatue.Location = new Point(0x2d, 140);
            this.lblStatue.Name = "lblStatue";
            this.lblStatue.Size = new Size(0x17, 12);
            this.lblStatue.TabIndex = 11;
            this.lblStatue.Text = "N/A";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(10, 140);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x1d, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "状态";
            this.groupBox2.Controls.Add(this.lblUCTDate);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.lblUCTTime);
            this.groupBox2.Controls.Add(this.label22);
            this.groupBox2.Location = new Point(0xe2, 0x10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x9f, 0x44);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "时间";
            this.lblUCTDate.AutoSize = true;
            this.lblUCTDate.Location = new Point(0x45, 0x29);
            this.lblUCTDate.Name = "lblUCTDate";
            this.lblUCTDate.Size = new Size(0x17, 12);
            this.lblUCTDate.TabIndex = 3;
            this.lblUCTDate.Text = "N/A";
            this.label20.AutoSize = true;
            this.label20.Location = new Point(14, 0x29);
            this.label20.Name = "label20";
            this.label20.Size = new Size(0x2f, 12);
            this.label20.TabIndex = 2;
            this.label20.Text = "UCT日期";
            this.lblUCTTime.AutoSize = true;
            this.lblUCTTime.Location = new Point(0x45, 0x13);
            this.lblUCTTime.Name = "lblUCTTime";
            this.lblUCTTime.Size = new Size(0x17, 12);
            this.lblUCTTime.TabIndex = 1;
            this.lblUCTTime.Text = "N/A";
            this.label22.AutoSize = true;
            this.label22.Location = new Point(14, 0x13);
            this.label22.Name = "label22";
            this.label22.Size = new Size(0x2f, 12);
            this.label22.TabIndex = 0;
            this.label22.Text = "UCT时间";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x86, 0xa3);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x11, 12);
            this.label7.TabIndex = 10;
            this.label7.Text = "度";
            this.lblQ.AutoSize = true;
            this.lblQ.Location = new Point(0x4e, 0xb9);
            this.lblQ.Name = "lblQ";
            this.lblQ.Size = new Size(0x17, 12);
            this.lblQ.TabIndex = 7;
            this.lblQ.Text = "N/A";
            this.label16.AutoSize = true;
            this.label16.Location = new Point(11, 0xb9);
            this.label16.Name = "label16";
            this.label16.Size = new Size(0x41, 12);
            this.label16.TabIndex = 6;
            this.label16.Text = "质量指示器";
            this.lblMag.AutoSize = true;
            this.lblMag.Location = new Point(0x4e, 0xa3);
            this.lblMag.Name = "lblMag";
            this.lblMag.Size = new Size(0x17, 12);
            this.lblMag.TabIndex = 5;
            this.lblMag.Text = "N/A";
            this.label18.AutoSize = true;
            this.label18.Location = new Point(11, 0xa3);
            this.label18.Name = "label18";
            this.label18.Size = new Size(0x29, 12);
            this.label18.TabIndex = 4;
            this.label18.Text = "磁偏角";
            this.groupBox3.Controls.Add(this.lblStateCount);
            this.groupBox3.Controls.Add(this.lblSNR);
            this.groupBox3.Controls.Add(this.label24);
            this.groupBox3.Controls.Add(this.lblInuseStateCount);
            this.groupBox3.Controls.Add(this.label26);
            this.groupBox3.Location = new Point(11, 0xcd);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0xce, 0x40);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "可用卫星";
            this.lblStateCount.AutoSize = true;
            this.lblStateCount.Location = new Point(0x84, 0x15);
            this.lblStateCount.Name = "lblStateCount";
            this.lblStateCount.Size = new Size(0x17, 12);
            this.lblStateCount.TabIndex = 4;
            this.lblStateCount.Text = "N/A";
            this.lblSNR.AutoSize = true;
            this.lblSNR.Location = new Point(0x53, 0x29);
            this.lblSNR.Name = "lblSNR";
            this.lblSNR.Size = new Size(0x17, 12);
            this.lblSNR.TabIndex = 3;
            this.lblSNR.Text = "N/A";
            this.label24.AutoSize = true;
            this.label24.Location = new Point(14, 0x29);
            this.label24.Name = "label24";
            this.label24.Size = new Size(0x41, 12);
            this.label24.TabIndex = 2;
            this.label24.Text = "平均信噪比";
            this.lblInuseStateCount.AutoSize = true;
            this.lblInuseStateCount.Location = new Point(0x52, 0x13);
            this.lblInuseStateCount.Name = "lblInuseStateCount";
            this.lblInuseStateCount.Size = new Size(0x17, 12);
            this.lblInuseStateCount.TabIndex = 1;
            this.lblInuseStateCount.Text = "N/A";
            this.label26.AutoSize = true;
            this.label26.Location = new Point(14, 0x13);
            this.label26.Name = "label26";
            this.label26.Size = new Size(0x35, 12);
            this.label26.TabIndex = 0;
            this.label26.Text = "卫星数量";
            this.groupBox4.Controls.Add(this.lblAge);
            this.groupBox4.Controls.Add(this.label25);
            this.groupBox4.Controls.Add(this.lblStateID);
            this.groupBox4.Controls.Add(this.label28);
            this.groupBox4.Location = new Point(0xe2, 0xcd);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(0x9f, 0x40);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "差分GPS";
            this.lblAge.AutoSize = true;
            this.lblAge.Location = new Point(0x45, 0x2b);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new Size(0x17, 12);
            this.lblAge.TabIndex = 3;
            this.lblAge.Text = "N/A";
            this.label25.AutoSize = true;
            this.label25.Location = new Point(14, 0x2b);
            this.label25.Name = "label25";
            this.label25.Size = new Size(0x29, 12);
            this.label25.TabIndex = 2;
            this.label25.Text = "成熟度";
            this.lblStateID.AutoSize = true;
            this.lblStateID.Location = new Point(0x45, 0x15);
            this.lblStateID.Name = "lblStateID";
            this.lblStateID.Size = new Size(0x17, 12);
            this.lblStateID.TabIndex = 1;
            this.lblStateID.Text = "N/A";
            this.label28.AutoSize = true;
            this.label28.Location = new Point(14, 0x15);
            this.label28.Name = "label28";
            this.label28.Size = new Size(0x29, 12);
            this.label28.TabIndex = 0;
            this.label28.Text = "站编号";
            this.groupBox5.Controls.Add(this.lblPDOP);
            this.groupBox5.Controls.Add(this.label27);
            this.groupBox5.Controls.Add(this.lblVDOP);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.lblHDOP);
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.Location = new Point(0xe2, 90);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new Size(0x9f, 0x5e);
            this.groupBox5.TabIndex = 15;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "精度因子";
            this.lblPDOP.AutoSize = true;
            this.lblPDOP.Location = new Point(0x45, 0x3e);
            this.lblPDOP.Name = "lblPDOP";
            this.lblPDOP.Size = new Size(0x17, 12);
            this.lblPDOP.TabIndex = 5;
            this.lblPDOP.Text = "N/A";
            this.label27.AutoSize = true;
            this.label27.Location = new Point(14, 0x3e);
            this.label27.Name = "label27";
            this.label27.Size = new Size(0x1d, 12);
            this.label27.TabIndex = 4;
            this.label27.Text = "PDOP";
            this.lblVDOP.AutoSize = true;
            this.lblVDOP.Location = new Point(0x45, 0x27);
            this.lblVDOP.Name = "lblVDOP";
            this.lblVDOP.Size = new Size(0x17, 12);
            this.lblVDOP.TabIndex = 3;
            this.lblVDOP.Text = "N/A";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(14, 0x27);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x1d, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "VDOP";
            this.lblHDOP.AutoSize = true;
            this.lblHDOP.Location = new Point(0x45, 0x15);
            this.lblHDOP.Name = "lblHDOP";
            this.lblHDOP.Size = new Size(0x17, 12);
            this.lblHDOP.TabIndex = 1;
            this.lblHDOP.Text = "N/A";
            this.label14.AutoSize = true;
            this.label14.Location = new Point(14, 0x15);
            this.label14.Name = "label14";
            this.label14.Size = new Size(0x1d, 12);
            this.label14.TabIndex = 0;
            this.label14.Text = "HDOP";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(400, 0x119);
            base.Controls.Add(this.groupBox5);
            base.Controls.Add(this.groupBox4);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.lblStatue);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.label18);
            base.Controls.Add(this.lblQ);
            base.Controls.Add(this.lblMag);
            base.Controls.Add(this.label16);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmGPSPositionWindows";
            this.Text = "GPS位置";
            base.Load += new EventHandler(this.frmGPSPositionWindows_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

