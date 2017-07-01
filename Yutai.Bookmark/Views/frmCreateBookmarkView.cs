using System.Collections.Generic;
using System.Windows.Forms;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Bookmark.Views
{
    public partial class frmCreateBookmarkView : Form
    {
        private IAppContext _context;
        private static int nIndex;
        private IBasicMap m_pMap;

        public frmCreateBookmarkView(IAppContext context)
        {
            InitializeComponent();
            _context = context;
            m_pMap = _context.FocusMap as IBasicMap;
        }

        static frmCreateBookmarkView()
        {
            frmCreateBookmarkView.old_acctor_mc();
        }

        private static void old_acctor_mc()
        {
            frmCreateBookmarkView.nIndex = 1;
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            IMapBookmarks pBookmarks = m_pMap as IMapBookmarks;
            if (pBookmarks != null)
            {
                bool flag = false;
                IEnumSpatialBookmark bookmarks = pBookmarks.Bookmarks;
                ISpatialBookmark pBookmark;
                bookmarks.Reset();
                while ((pBookmark = bookmarks.Next()) != null)
                {
                    if (pBookmark.Name == txtBookmarkName.Text)
                        break;
                }
                if (!flag)
                {
                    frmCreateBookmarkView.nIndex = frmCreateBookmarkView.nIndex + 1;
                    pBookmark = new AOIBookmarkClass()
                    {
                        Name = txtBookmarkName.Text,
                        Location = (m_pMap as IActiveView).Extent
                    };

                    pBookmarks.AddBookmark(pBookmark);
                }
            }
        }

        private void CreateBookmarkView_Load(object sender, System.EventArgs e)
        {
            string str = string.Concat("书签 ", frmCreateBookmarkView.nIndex.ToString());
            this.txtBookmarkName.Text = str;
        }
    }
}