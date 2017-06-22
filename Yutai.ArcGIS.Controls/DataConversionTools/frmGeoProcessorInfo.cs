using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.Geoprocessor;
using Yutai.ArcGIS.Common.ControlExtend;
using Yutai.ArcGIS.Controls.Historical;

namespace Yutai.ArcGIS.Controls.DataConversionTools
{
    public partial class frmGeoProcessorInfo : Form, IGeoProcessorEvents, ITrackCancel, IGPMessagesCallback, IGPServerTrackCancel, IStepProgressor, IProgressor
    {
        private string m_GeoProcessor = "";
        private bool m_IsPostToolExecute = false;
        private bool m_IsPreExecute = false;
        private IGPProcess m_pGPProcess = null;
        private StringBuilder m_sb = new StringBuilder();
        private int pos = 0;

        public frmGeoProcessorInfo()
        {
            this.InitializeComponent();
        }

        public void Cancel()
        {
        }

        public bool Continue()
        {
            return false;
        }

 public bool Excute()
        {
            bool isPostToolExecute = false;
            this.m_sb.Remove(0, this.m_sb.Length);
            ESRI.ArcGIS.Geoprocessor.Geoprocessor geoprocessor = new ESRI.ArcGIS.Geoprocessor.Geoprocessor();
            geoprocessor.RegisterGeoProcessorEvents(this);
            BaseClass.RunTool(geoprocessor, this.m_pGPProcess, null);
            geoprocessor.UnRegisterGeoProcessorEvents(this);
            isPostToolExecute = this.m_IsPostToolExecute;
            if (!this.m_IsPostToolExecute)
            {
                if (geoprocessor.MessageCount > 0)
                {
                    for (int i = 0; i <= (geoprocessor.MessageCount - 1); i++)
                    {
                        if (this.m_sb.Length > 0)
                        {
                            this.m_sb.Append("\r\n");
                        }
                        this.m_sb.Append(geoprocessor.GetMessage(i));
                    }
                }
                else
                {
                    this.m_sb.Append("命令执行失败!");
                }
                this.txtMessage.Text = this.m_sb.ToString();
            }
            this.timer1.Enabled = false;
            this.m_IsPostToolExecute = false;
            this.m_IsPreExecute = false;
            ComReleaser.ReleaseCOMObject(geoprocessor);
            ComReleaser.ReleaseCOMObject(this.m_pGPProcess);
            geoprocessor = null;
            return isPostToolExecute;
        }

        private void frmGeoProcessorInfo_Load(object sender, EventArgs e)
        {
            this.lblGeoProcessor.Text = "执行" + this.m_GeoProcessor;
            this.Text = this.m_GeoProcessor;
        }

        public IJobMessages GetJobMessages()
        {
            return null;
        }

        public void Init(object pJobTracker)
        {
        }

 public void MessageAdded(IGPMessage Message)
        {
        }

        public int OffsetPosition(int offsetValue)
        {
            this.progressBar1.Value += offsetValue;
            return this.progressBar1.Value;
        }

        public void OnMessageAdded(IGPMessage message)
        {
            if (this.m_sb.Length > 0)
            {
                this.m_sb.Append("\r\n");
            }
            this.m_sb.Append(message.Description);
            this.txtMessage.Text = this.m_sb.ToString();
        }

        public void PostToolExecute(IGPTool Tool, IArray Values, int result, IGPMessages Messages)
        {
            this.m_IsPostToolExecute = true;
        }

        public void PreToolExecute(IGPTool Tool, IArray Values, int processID)
        {
            this.m_IsPreExecute = true;
        }

        public void Reset()
        {
        }

        public void StartTimer(int hWnd, int milliseconds)
        {
        }

        public void Step()
        {
            this.progressBar1.Increment(this.progressBar1.Step);
        }

        public void StopTimer()
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.pos == this.xpProgressBar1.PositionMax)
            {
                this.pos = this.xpProgressBar1.PositionMin;
            }
            this.pos++;
            this.xpProgressBar1.Position = this.pos;
            Application.DoEvents();
        }

        public void ToolboxChange()
        {
            this.txtMessage.Text = "";
        }

        public bool AutoClose
        {
            get
            {
                return this.chkAutoCompleteSucc.Checked;
            }
        }

        public bool CancelOnClick
        {
            get
            {
                return false;
            }
            set
            {
            }
        }

        public bool CancelOnKeyPress
        {
            get
            {
                return false;
            }
            set
            {
            }
        }

        public int CheckTime
        {
            get
            {
                return 0;
            }
            set
            {
            }
        }

        public string GeoProcessor
        {
            set
            {
                this.m_GeoProcessor = value;
            }
        }

        public IGPProcess GPProcess
        {
            set
            {
                this.m_pGPProcess = value;
            }
        }

        public int MaxRange
        {
            get
            {
                return 100;
            }
            set
            {
                this.progressBar1.Maximum = value;
            }
        }

        public string Message
        {
            get
            {
                return "";
            }
            set
            {
            }
        }

        public int MinRange
        {
            get
            {
                return 0;
            }
            set
            {
                this.progressBar1.Minimum = value;
            }
        }

        public int Position
        {
            get
            {
                return this.progressBar1.Value;
            }
            set
            {
                this.progressBar1.Value = value;
            }
        }

        public bool ProcessMessages
        {
            get
            {
                return false;
            }
            set
            {
            }
        }

        public IProgressor Progressor
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        public int StepValue
        {
            get
            {
                return this.progressBar1.Step;
            }
            set
            {
                this.progressBar1.Step = value;
            }
        }

        public bool TimerFired
        {
            get
            {
                return false;
            }
        }
    }
}

