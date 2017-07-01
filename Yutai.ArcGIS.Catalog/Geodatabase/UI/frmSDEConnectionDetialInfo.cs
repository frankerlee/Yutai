using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmSDEConnectionDetialInfo : Form
    {
        private HISTORICALTYPE historicaltype_0 = HISTORICALTYPE.VERSION;
        private IContainer icontainer_0 = null;
        private IWorkspace iworkspace_0 = null;
        private object object_0 = null;

        public frmSDEConnectionDetialInfo()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.rdoType.SelectedIndex == 0)
            {
                this.historicaltype_0 = HISTORICALTYPE.VERSION;
                if (this.lvwVersions.SelectedItems.Count == 0)
                {
                    this.object_0 = null;
                    return;
                }
                this.object_0 = this.lvwVersions.SelectedItems[0].Tag;
            }
            else if (this.rdoConnectionHM.Checked)
            {
                this.historicaltype_0 = HISTORICALTYPE.HISTORICALNAME;
                if (this.cboHistoricalMarker.SelectedIndex == -1)
                {
                    return;
                }
                this.object_0 = this.cboHistoricalMarker.Text;
            }
            else
            {
                this.historicaltype_0 = HISTORICALTYPE.HISTORICALTIMESTAMP;
                this.object_0 = this.dateTimePicker1.Value;
            }
            base.DialogResult = DialogResult.OK;
        }

        private void btnRefreshDatabaseTime_Click(object sender, EventArgs e)
        {
            this.dateTimePicker1.Value = DateTime.Now;
        }

        private void frmSDEConnectionDetialInfo_Load(object sender, EventArgs e)
        {
            if (this.iworkspace_0 != null)
            {
                IEnumVersionInfo versions = (this.iworkspace_0 as IVersionedWorkspace2).Versions;
                versions.Reset();
                IVersionInfo info2 = versions.Next();
                string[] items = new string[2];
                while (info2 != null)
                {
                    items[0] = info2.VersionName;
                    items[1] = info2.Description;
                    ListViewItem item = new ListViewItem(items)
                    {
                        Tag = info2
                    };
                    this.lvwVersions.Items.Add(item);
                    info2 = versions.Next();
                }
                IHistoricalWorkspace workspace = this.iworkspace_0 as IHistoricalWorkspace;
                IEnumHistoricalMarker historicalMarkers = workspace.HistoricalMarkers;
                historicalMarkers.Reset();
                for (IHistoricalMarker marker2 = historicalMarkers.Next();
                    marker2 != null;
                    marker2 = historicalMarkers.Next())
                {
                    this.cboHistoricalMarker.Properties.Items.Add(marker2.Name);
                }
                if (this.cboHistoricalMarker.Properties.Items.Count > 0)
                {
                    this.cboHistoricalMarker.SelectedIndex = 0;
                }
            }
        }

        private void rdoConnectionDate_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void rdoConnectionDate_Click(object sender, EventArgs e)
        {
            this.cboHistoricalMarker.Enabled = this.rdoConnectionHM.Checked;
            this.dateTimePicker1.Enabled = !this.rdoConnectionHM.Checked;
            this.btnRefreshDatabaseTime.Enabled = !this.rdoConnectionHM.Checked;
        }

        private void rdoConnectionHM_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void rdoConnectionHM_Click(object sender, EventArgs e)
        {
            this.cboHistoricalMarker.Enabled = this.rdoConnectionHM.Checked;
            this.dateTimePicker1.Enabled = !this.rdoConnectionHM.Checked;
            this.btnRefreshDatabaseTime.Enabled = !this.rdoConnectionHM.Checked;
        }

        private void rdoType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool flag = this.rdoType.SelectedIndex == 1;
            this.lvwVersions.Enabled = !flag;
            this.rdoConnectionDate.Enabled = flag;
            this.rdoConnectionHM.Enabled = flag;
            this.cboHistoricalMarker.Enabled = this.rdoConnectionHM.Checked;
            this.dateTimePicker1.Enabled = !this.rdoConnectionHM.Checked;
            this.btnRefreshDatabaseTime.Enabled = !this.rdoConnectionHM.Checked;
        }

        internal HISTORICALTYPE HISTORICAL
        {
            get { return this.historicaltype_0; }
        }

        public object HistoricalInfo
        {
            get { return this.object_0; }
        }

        public IWorkspace Workspace
        {
            get { return this.iworkspace_0; }
            set { this.iworkspace_0 = value; }
        }

        internal enum HISTORICALTYPE
        {
            VERSION,
            HISTORICALTIMESTAMP,
            HISTORICALNAME
        }
    }
}