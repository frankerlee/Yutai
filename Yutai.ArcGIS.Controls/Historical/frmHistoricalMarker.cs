using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Controls.Historical
{
    public partial class frmHistoricalMarker : Form
    {
        private string m_HistoricalMarkerName = "";
        private bool m_IsEdit = false;
        private IDatabaseConnectionInfo2 m_pDatabaseConnectionInfo = null;
        private IHistoricalMarker m_pHistoricalMarker = null;

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

