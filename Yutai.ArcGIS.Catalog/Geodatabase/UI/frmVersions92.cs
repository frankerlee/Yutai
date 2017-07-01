using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmVersions92 : Form
    {
        private bool bool_0 = false;
        private IContainer icontainer_0 = null;
        private int int_0 = 0;
        private IWorkspace iworkspace_0 = null;
        private object object_0 = null;

        public frmVersions92()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.cboVersionType.SelectedIndex == 0)
            {
                this.int_0 = 0;
                if (this.VersionInfolist.SelectedItems.Count != -1)
                {
                    this.object_0 = this.VersionInfolist.SelectedItems[0].SubItems[1].Text + "." +
                                    this.VersionInfolist.SelectedItems[0].Text;
                }
            }
            else if (this.cboVersionType.SelectedIndex == 1)
            {
                if (this.rdoHistoryType.SelectedIndex == 0)
                {
                    this.int_0 = 1;
                    this.object_0 = this.cboHistoricalMarker.Text;
                }
                else
                {
                    this.object_0 = this.dateTimePicker1.Value;
                    this.int_0 = 2;
                }
            }
        }

        private void btnRefreshDatabaseTime_Click(object sender, EventArgs e)
        {
            if (this.iworkspace_0 is IDatabaseConnectionInfo2)
            {
                this.dateTimePicker1.Value =
                    (DateTime) (this.iworkspace_0 as IDatabaseConnectionInfo2).ConnectionCurrentDateTime;
            }
            else
            {
                this.dateTimePicker1.Value = DateTime.Now;
            }
        }

        private void cboHistoricalMarker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboHistoricalMarker.SelectedIndex != -1)
            {
                IHistoricalVersion version =
                    (this.iworkspace_0 as IHistoricalWorkspace).FindHistoricalVersionByName(
                        this.cboHistoricalMarker.Text);
                this.lblDateTimeValue.Text = version.TimeStamp.ToString();
            }
        }

        private void cboVersionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                if (this.cboVersionType.SelectedIndex == 0)
                {
                    this.int_0 = 0;
                }
                else if ((this.cboVersionType.SelectedIndex == 1) && (this.int_0 == 0))
                {
                    this.int_0 = 1;
                }
            }
            this.panelHistorical.Visible = this.cboVersionType.SelectedIndex == 1;
            this.panelTransaction.Visible = this.cboVersionType.SelectedIndex == 0;
            if (this.int_0 == 1)
            {
                this.rdoHistoryType.SelectedIndex = 0;
                this.rdoHistoryType_SelectedIndexChanged(this, new EventArgs());
            }
            else if (this.int_0 == 2)
            {
                this.rdoHistoryType.SelectedIndex = 1;
                this.rdoHistoryType_SelectedIndexChanged(this, new EventArgs());
            }
        }

        private void frmVersions92_Load(object sender, EventArgs e)
        {
            this.method_2();
            this.bool_0 = true;
        }

        private int method_0(IWorkspace iworkspace_1, out object object_1)
        {
            int num = -1;
            object_1 = "";
            IPropertySet connectionProperties = iworkspace_1.ConnectionProperties;
            bool flag = false;
            try
            {
                object_1 = connectionProperties.GetProperty("Version").ToString();
                flag = true;
                num = 0;
            }
            catch
            {
            }
            if (!flag)
            {
                try
                {
                    object_1 = connectionProperties.GetProperty("HISTORICAL_NAME").ToString();
                    flag = true;
                    num = 1;
                }
                catch
                {
                }
            }
            if (!flag)
            {
                try
                {
                    object_1 = connectionProperties.GetProperty("HISTORICAL_TIMESTAMP");
                    flag = true;
                    num = 2;
                }
                catch
                {
                }
            }
            return num;
        }

        private string method_1(esriVersionAccess esriVersionAccess_0)
        {
            switch (esriVersionAccess_0)
            {
                case esriVersionAccess.esriVersionAccessPrivate:
                    return "私有";

                case esriVersionAccess.esriVersionAccessProtected:
                    return "保护";
            }
            return "公共";
        }

        private void method_2()
        {
            if (this.iworkspace_0 != null)
            {
                this.VersionInfolist.Items.Clear();
                IEnumVersionInfo versions = (this.iworkspace_0 as IVersionedWorkspace).Versions;
                versions.Reset();
                IVersionInfo info2 = versions.Next();
                string[] items = new string[5];
                while (info2 != null)
                {
                    string versionName = info2.VersionName;
                    string[] strArray2 = versionName.Split(new char[] {'.'});
                    if (strArray2.Length > 1)
                    {
                        items[0] = strArray2[1];
                        items[1] = strArray2[0];
                    }
                    else
                    {
                        items[0] = strArray2[0];
                        items[1] = "";
                    }
                    items[2] = this.method_1(info2.Access);
                    items[3] = info2.Created.ToString();
                    items[4] = info2.Modified.ToString();
                    ListViewItem item = new ListViewItem(items)
                    {
                        Tag = info2
                    };
                    this.VersionInfolist.Items.Add(item);
                    if ((this.int_0 == 0) && (versionName == this.object_0.ToString()))
                    {
                        item.Selected = true;
                    }
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
                    if ((this.int_0 == 1) && (marker2.Name == this.object_0.ToString()))
                    {
                        this.cboHistoricalMarker.SelectedIndex = this.cboHistoricalMarker.Properties.Items.Count - 1;
                    }
                }
                if ((this.cboHistoricalMarker.Properties.Items.Count > 0) &&
                    (this.cboHistoricalMarker.SelectedIndex == -1))
                {
                    this.cboHistoricalMarker.SelectedIndex = 0;
                }
                this.dateTimePicker1.Value = DateTime.Now;
                if (this.int_0 == 0)
                {
                    this.cboVersionType.SelectedIndex = 0;
                    this.panelHistorical.Visible = false;
                }
                else
                {
                    if (this.int_0 == 2)
                    {
                        this.dateTimePicker1.Value = (DateTime) this.object_0;
                    }
                    this.cboVersionType.SelectedIndex = 1;
                    this.panelTransaction.Visible = false;
                    this.cboVersionType_SelectedIndexChanged(this, new EventArgs());
                    this.rdoHistoryType_SelectedIndexChanged(this, new EventArgs());
                }
            }
        }

        private void rdoHistoryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rdoHistoryType.SelectedIndex == 0)
            {
                this.cboHistoricalMarker.Enabled = true;
                this.lblDateTime.Enabled = true;
                this.lblDateTimeValue.Enabled = true;
                this.dateTimePicker1.Enabled = false;
                this.btnRefreshDatabaseTime.Enabled = false;
            }
            else if (this.rdoHistoryType.SelectedIndex == 1)
            {
                this.cboHistoricalMarker.Enabled = false;
                this.lblDateTime.Enabled = false;
                this.lblDateTimeValue.Enabled = false;
                this.dateTimePicker1.Enabled = true;
                this.btnRefreshDatabaseTime.Enabled = true;
            }
        }

        private void VersionInfolist_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        public int Type
        {
            get { return this.int_0; }
            set { this.int_0 = value; }
        }

        public object VersionName
        {
            get { return this.object_0; }
            set { this.object_0 = value; }
        }

        public IWorkspace Workspace
        {
            set
            {
                this.iworkspace_0 = value;
                this.int_0 = this.method_0(this.iworkspace_0, out this.object_0);
            }
        }
    }
}