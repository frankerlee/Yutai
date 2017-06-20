using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Controls.Historical
{
    public class frmHistoricalMarker : Form
    {
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private SimpleButton btnRefreshDatabaseTime;
        private IContainer components = null;
        private DateTimePicker dateTimePicker1;
        private Label label1;
        private Label label2;
        private string m_HistoricalMarkerName = "";
        private object m_HistoricalMarkerTimeStamp;
        private bool m_IsEdit = false;
        private IDatabaseConnectionInfo2 m_pDatabaseConnectionInfo = null;
        private IHistoricalMarker m_pHistoricalMarker = null;
        private TextEdit txtMarkerName;

        public frmHistoricalMarker()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtMarkerName.Text.Trim().Length != 0)
            {
                this.m_HistoricalMarkerName = this.txtMarkerName.Text.Trim();
                this.m_HistoricalMarkerTimeStamp = this.dateTimePicker1.Value;
                base.DialogResult = DialogResult.OK;
            }
        }

        private void btnRefreshDatabaseTime_Click(object sender, EventArgs e)
        {
            if (this.m_pDatabaseConnectionInfo == null)
            {
                this.dateTimePicker1.Value = DateTime.Now;
            }
            else
            {
                this.dateTimePicker1.Value = (DateTime) this.m_pDatabaseConnectionInfo.ConnectionCurrentDateTime;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmHistoricalMarker_Load(object sender, EventArgs e)
        {
            if (this.m_IsEdit)
            {
                this.Text = "编辑";
                this.txtMarkerName.Text = this.m_pHistoricalMarker.Name;
                this.dateTimePicker1.Value = (DateTime) this.m_pHistoricalMarker.TimeStamp;
            }
            else
            {
                this.Text = "新建";
                this.dateTimePicker1.Value = DateTime.Now;
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHistoricalMarker));
            this.label1 = new Label();
            this.txtMarkerName = new TextEdit();
            this.label2 = new Label();
            this.btnRefreshDatabaseTime = new SimpleButton();
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.dateTimePicker1 = new DateTimePicker();
            this.txtMarkerName.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "输入历史标记名称";
            this.txtMarkerName.Location = new Point(14, 0x19);
            this.txtMarkerName.Name = "txtMarkerName";
            this.txtMarkerName.Size = new Size(0xe0, 0x15);
            this.txtMarkerName.TabIndex = 1;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 0x31);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x89, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "输入历史标记的时间标记";
            this.btnRefreshDatabaseTime.Image = (Image) resources.GetObject("btnRefreshDatabaseTime.Image");
            this.btnRefreshDatabaseTime.Location = new Point(0xd5, 0x45);
            this.btnRefreshDatabaseTime.Name = "btnRefreshDatabaseTime";
            this.btnRefreshDatabaseTime.Size = new Size(0x19, 0x17);
            this.btnRefreshDatabaseTime.TabIndex = 5;
            this.btnRefreshDatabaseTime.Click += new EventHandler(this.btnRefreshDatabaseTime_Click);
            this.btnOK.Location = new Point(0xf9, 9);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x3d, 0x17);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(250, 0x27);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(60, 0x17);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消";
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd H:mm:ss";
            this.dateTimePicker1.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new Point(14, 0x47);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new Size(0xc1, 0x15);
            this.dateTimePicker1.TabIndex = 8;
            this.dateTimePicker1.Value = new DateTime(0x7d7, 1, 3, 0, 0, 0, 0);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x144, 0x6a);
            base.Controls.Add(this.dateTimePicker1);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnRefreshDatabaseTime);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.txtMarkerName);
            base.Controls.Add(this.label1);
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmHistoricalMarker";
            this.Text = "frmHistoricalMarker";
            base.Load += new EventHandler(this.frmHistoricalMarker_Load);
            this.txtMarkerName.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public IDatabaseConnectionInfo2 DatabaseConnectionInfo
        {
            set
            {
                this.m_pDatabaseConnectionInfo = value;
            }
        }

        public IHistoricalMarker HistoricalMarker
        {
            set
            {
                this.m_pHistoricalMarker = value;
                this.m_IsEdit = true;
            }
        }

        public string HistoricalMarkerName
        {
            get
            {
                return this.m_HistoricalMarkerName;
            }
        }

        public object HistoricalMarkerTimeStamp
        {
            get
            {
                return this.m_HistoricalMarkerTimeStamp;
            }
        }
    }
}

