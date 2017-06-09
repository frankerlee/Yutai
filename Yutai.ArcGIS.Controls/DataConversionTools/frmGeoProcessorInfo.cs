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
    public class frmGeoProcessorInfo : Form, IGeoProcessorEvents, ITrackCancel, IGPMessagesCallback, IGPServerTrackCancel, IStepProgressor, IProgressor
    {
        private SimpleButton btnCancel;
        private CheckEdit chkAutoCompleteSucc;
        private IContainer components = null;
        private LabelControl lblGeoProcessor;
        private string m_GeoProcessor = "";
        private bool m_IsPostToolExecute = false;
        private bool m_IsPreExecute = false;
        private IGPProcess m_pGPProcess = null;
        private StringBuilder m_sb = new StringBuilder();
        private int pos = 0;
        private System.Windows.Forms.ProgressBar progressBar1;
        private Timer timer1;
        private MemoEdit txtMessage;
        private XpProgressBar xpProgressBar1;

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

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        void IProgressor.Hide()
        {
            base.Hide();
        }

        void IProgressor.Show()
        {
            base.Show();
        }

        void IStepProgressor.Hide()
        {
            base.Hide();
        }

        void IStepProgressor.Show()
        {
            base.Show();
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

        private void InitializeComponent()
        {
            this.components = new Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGeoProcessorInfo));
            this.lblGeoProcessor = new LabelControl();
            this.btnCancel = new SimpleButton();
            this.txtMessage = new MemoEdit();
            this.chkAutoCompleteSucc = new CheckEdit();
            this.timer1 = new Timer(this.components);
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.xpProgressBar1 = new XpProgressBar();
            this.txtMessage.Properties.BeginInit();
            this.chkAutoCompleteSucc.Properties.BeginInit();
            base.SuspendLayout();
            this.lblGeoProcessor.Location = new Point(13, 13);
            this.lblGeoProcessor.Name = "lblGeoProcessor";
            this.lblGeoProcessor.Size = new Size(0x18, 14);
            this.lblGeoProcessor.TabIndex = 0;
            this.lblGeoProcessor.Text = "执行";
            this.btnCancel.Location = new Point(0x198, 13);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x17);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.Visible = false;
            this.txtMessage.Location = new Point(13, 0x58);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new Size(0x233, 0xe0);
            this.txtMessage.TabIndex = 2;
            this.chkAutoCompleteSucc.Location = new Point(13, 0x3f);
            this.chkAutoCompleteSucc.Name = "chkAutoCompleteSucc";
            this.chkAutoCompleteSucc.Properties.Caption = "成功完成后自动关闭对话框";
            this.chkAutoCompleteSucc.Size = new Size(0xd0, 0x13);
            this.chkAutoCompleteSucc.TabIndex = 3;
            this.timer1.Enabled = true;
            this.timer1.Interval = 50;
            this.timer1.Tick += new EventHandler(this.timer1_Tick);
            this.progressBar1.Location = new Point(0x38, 13);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new Size(220, 13);
            this.progressBar1.TabIndex = 5;
            this.progressBar1.Visible = false;
            this.xpProgressBar1.ColorBackGround = Color.White;
            this.xpProgressBar1.ColorBarBorder = Color.FromArgb(170, 240, 170);
            this.xpProgressBar1.ColorBarCenter = Color.FromArgb(10, 150, 10);
            this.xpProgressBar1.ColorText = Color.Black;
            this.xpProgressBar1.Location = new Point(13, 0x21);
            this.xpProgressBar1.Name = "xpProgressBar1";
            this.xpProgressBar1.Position = 0;
            this.xpProgressBar1.PositionMax = 100;
            this.xpProgressBar1.PositionMin = 0;
            this.xpProgressBar1.Size = new Size(0x14e, 0x18);
            this.xpProgressBar1.TabIndex = 4;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x24c, 0x144);
            base.Controls.Add(this.progressBar1);
            base.Controls.Add(this.xpProgressBar1);
            base.Controls.Add(this.chkAutoCompleteSucc);
            base.Controls.Add(this.txtMessage);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.lblGeoProcessor);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) resources.GetObject("$Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmGeoProcessorInfo";
            base.Load += new EventHandler(this.frmGeoProcessorInfo_Load);
            this.txtMessage.Properties.EndInit();
            this.chkAutoCompleteSucc.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
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

