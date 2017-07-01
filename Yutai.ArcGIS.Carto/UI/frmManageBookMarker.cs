using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class frmManageBookMarker : Form
    {
        private Container container_0 = null;
        private IMap imap_0 = null;

        public frmManageBookMarker()
        {
            this.InitializeComponent();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count == 1)
            {
                ListViewItem item = this.listView1.SelectedItems[0];
                (this.imap_0 as IMapBookmarks).RemoveBookmark(item.Tag as ISpatialBookmark);
                this.listView1.Items.Remove(item);
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            (this.imap_0 as IMapBookmarks).RemoveAllBookmarks();
            this.listView1.Items.Clear();
        }

        private void btnZoomTo_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count == 1)
            {
                ListViewItem item = this.listView1.SelectedItems[0];
                (item.Tag as ISpatialBookmark).ZoomTo(this.imap_0);
                (this.imap_0 as IActiveView).Refresh();
            }
        }

        private void frmManageBookMarker_Load(object sender, EventArgs e)
        {
            IEnumSpatialBookmark bookmarks = (this.imap_0 as IMapBookmarks).Bookmarks;
            bookmarks.Reset();
            for (ISpatialBookmark bookmark2 = bookmarks.Next(); bookmark2 != null; bookmark2 = bookmarks.Next())
            {
                this.listView1.Items.Add(bookmark2.Name).Tag = bookmark2;
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count == 1)
            {
                this.btnDelete.Enabled = true;
                this.btnZoomTo.Enabled = true;
            }
            else
            {
                this.btnDelete.Enabled = false;
                this.btnZoomTo.Enabled = false;
            }
        }

        public IMap Map
        {
            set { this.imap_0 = value; }
        }
    }
}