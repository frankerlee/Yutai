using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Controls.Historical
{
    public partial class frmHistorialMarkerManager : Form
    {
        private IHistoricalWorkspace m_pHistoricalWorkspace = null;

        public frmHistorialMarkerManager()
        {
            this.InitializeComponent();
        }

        private void AddToList(IHistoricalMarker pHistoricalMarker)
        {
            ListViewItem item = new ListViewItem(new string[] { pHistoricalMarker.Name, pHistoricalMarker.TimeStamp.ToString() }) {
                Tag = pHistoricalMarker
            };
            this.listView1.Items.Add(item);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            for (int i = this.listView1.SelectedItems.Count - 1; i >= 0; i--)
            {
                IHistoricalMarker tag = this.listView1.SelectedItems[i].Tag as IHistoricalMarker;
                try
                {
                    this.m_pHistoricalWorkspace.RemoveHistoricalMarker(tag.Name);
                    this.listView1.Items.Remove(this.listView1.SelectedItems[i]);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            ListViewItem item = this.listView1.SelectedItems[0];
            IHistoricalMarker tag = item.Tag as IHistoricalMarker;
            frmHistoricalMarker marker2 = new frmHistoricalMarker {
                HistoricalMarker = tag
            };
            if (marker2.ShowDialog() == DialogResult.OK)
            {
                this.m_pHistoricalWorkspace.RemoveHistoricalMarker(tag.Name);
                tag = this.m_pHistoricalWorkspace.AddHistoricalMarker(marker2.HistoricalMarkerName, marker2.HistoricalMarkerTimeStamp);
                item.Text = tag.Name;
                item.SubItems[1].Text = tag.TimeStamp.ToString();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmHistoricalMarker marker = new frmHistoricalMarker {
                DatabaseConnectionInfo = this.m_pHistoricalWorkspace as IDatabaseConnectionInfo2
            };
            if (marker.ShowDialog() == DialogResult.OK)
            {
                IHistoricalMarker marker2 = null;
                if (marker2 != null)
                {
                    MessageBox.Show("名称的时间标记以存在！");
                }
                else
                {
                    this.AddToList(this.m_pHistoricalWorkspace.AddHistoricalMarker(marker.HistoricalMarkerName, marker.HistoricalMarkerTimeStamp));
                }
            }
        }

 private void frmHistorialMarkerManager_Load(object sender, EventArgs e)
        {
            IEnumHistoricalMarker historicalMarkers = this.m_pHistoricalWorkspace.HistoricalMarkers;
            historicalMarkers.Reset();
            IHistoricalMarker marker2 = historicalMarkers.Next();
            string[] items = new string[2];
            IVersionedWorkspace pHistoricalWorkspace = this.m_pHistoricalWorkspace as IVersionedWorkspace;
            while (marker2 != null)
            {
                if (marker2.Name != "DEFAULT")
                {
                    items[0] = marker2.Name;
                    items[1] = marker2.TimeStamp.ToString();
                    ListViewItem item = new ListViewItem(items) {
                        Tag = marker2
                    };
                    this.listView1.Items.Add(item);
                }
                marker2 = historicalMarkers.Next();
            }
        }

 private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnDelete.Enabled = this.listView1.SelectedIndices.Count > 0;
            this.btnEdit.Enabled = this.listView1.SelectedIndices.Count == 1;
        }

        public IHistoricalWorkspace HistoricalWorkspace
        {
            set
            {
                this.m_pHistoricalWorkspace = value;
            }
        }
    }
}

