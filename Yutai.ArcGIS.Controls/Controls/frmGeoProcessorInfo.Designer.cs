using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.Geoprocessor;
using Yutai.ArcGIS.Common.ControlExtend;

namespace Yutai.ArcGIS.Controls.Controls
{
    partial class frmGeoProcessorInfo
    {
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
            this.lblGeoProcessor.Size = new Size(24, 14);
            this.lblGeoProcessor.TabIndex = 0;
            this.lblGeoProcessor.Text = "执行";
            this.btnCancel.Location = new Point(408, 13);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.Visible = false;
            this.txtMessage.Location = new Point(13, 88);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new Size(563, 224);
            this.txtMessage.TabIndex = 2;
            this.chkAutoCompleteSucc.Location = new Point(13, 63);
            this.chkAutoCompleteSucc.Name = "chkAutoCompleteSucc";
            this.chkAutoCompleteSucc.Properties.Caption = "成功完成后自动关闭对话框";
            this.chkAutoCompleteSucc.Size = new Size(208, 19);
            this.chkAutoCompleteSucc.TabIndex = 3;
            this.timer1.Interval = 50;
            this.timer1.Tick += new EventHandler(this.timer1_Tick);
            this.progressBar1.Location = new Point(56, 13);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new Size(220, 13);
            this.progressBar1.TabIndex = 5;
            this.progressBar1.Visible = false;
            this.xpProgressBar1.ColorBackGround = Color.White;
            this.xpProgressBar1.ColorBarBorder = Color.FromArgb(170, 240, 170);
            this.xpProgressBar1.ColorBarCenter = Color.FromArgb(10, 150, 10);
            this.xpProgressBar1.ColorText = Color.Black;
            this.xpProgressBar1.Location = new Point(13, 33);
            this.xpProgressBar1.Name = "xpProgressBar1";
            this.xpProgressBar1.Position = 0;
            this.xpProgressBar1.PositionMax = 100;
            this.xpProgressBar1.PositionMin = 0;
            this.xpProgressBar1.Size = new Size(334, 24);
            this.xpProgressBar1.TabIndex = 4;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(588, 324);
            base.Controls.Add(this.progressBar1);
            base.Controls.Add(this.xpProgressBar1);
            base.Controls.Add(this.chkAutoCompleteSucc);
            base.Controls.Add(this.txtMessage);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.lblGeoProcessor);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            base.Icon = (Icon) resources.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmGeoProcessorInfo";
            base.FormClosing += new FormClosingEventHandler(this.frmGeoProcessorInfo_FormClosing);
            base.Load += new EventHandler(this.frmGeoProcessorInfo_Load);
            this.txtMessage.Properties.EndInit();
            this.chkAutoCompleteSucc.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private IContainer components = null;
        private SimpleButton btnCancel;
        private CheckEdit chkAutoCompleteSucc;
        private LabelControl lblGeoProcessor;
        private System.Windows.Forms.ProgressBar progressBar1;
        private Timer timer1;
        private MemoEdit txtMessage;
        private XpProgressBar xpProgressBar1;
    }
}