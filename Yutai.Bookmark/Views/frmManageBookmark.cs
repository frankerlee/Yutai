using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Bookmark.Views
{
    public partial class frmManageBookmark : Form
    {
        private IAppContext _context;
        private IBasicMap m_pMap;
        public frmManageBookmark(IAppContext context)
        {
            InitializeComponent();
            _context = context;
            m_pMap = _context.MapControl.Map as IBasicMap;
        }

        private void lstBookmarks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstBookmarks.SelectedItems.Count != 1)
            {
                this.btnDelete.Enabled = false;
                this.btnZoomTo.Enabled = false;
            }
            else
            {
                this.btnDelete.Enabled = true;
                this.btnZoomTo.Enabled = true;
            }
        }

        private void frmManageBookmark_Load(object sender, EventArgs e)
        {
            IMapBookmarks pBookmarks = m_pMap as IMapBookmarks;
            if (pBookmarks != null)
            {
                IEnumSpatialBookmark pEnumSpatialBookmark = pBookmarks.Bookmarks;
                ISpatialBookmark pBookmark;
                pEnumSpatialBookmark.Reset();
                while ((pBookmark = pEnumSpatialBookmark.Next()) != null)
                {
                    this.lstBookmarks.Items.Add(pBookmark.Name).Tag = pBookmark;
                }
            }
        }

        private void btnZoomTo_Click(object sender, EventArgs e)
        {
            if (this.lstBookmarks.SelectedItems.Count == 1)
            {
                ListViewItem item = this.lstBookmarks.SelectedItems[0];
                if (item.Tag is ISpatialBookmark)
                {
                  (item.Tag as ISpatialBookmark).ZoomTo(m_pMap as IMap);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.lstBookmarks.SelectedItems.Count == 1)
            {
                ListViewItem item = this.lstBookmarks.SelectedItems[0];
                if (item.Tag is ISpatialBookmark)
                {
                    (m_pMap as IMapBookmarks).RemoveBookmark(item.Tag as ISpatialBookmark);
                    this.lstBookmarks.Items.Remove(item);
                }
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (this.lstBookmarks.SelectedItems.Count == 1)
            {
                ListViewItem item = this.lstBookmarks.SelectedItems[0];
                if (item.Tag is ISpatialBookmark)
                {
                    (m_pMap as IMapBookmarks).RemoveAllBookmarks();
                    this.lstBookmarks.Items.Clear();
                }
            }
        }
    }
}
