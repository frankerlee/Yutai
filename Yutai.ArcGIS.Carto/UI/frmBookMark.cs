using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class frmBookMark : Form
    {
        private Container container_0 = null;
        private IMap imap_0 = null;
        private static int nIndex;

        static frmBookMark()
        {
            old_acctor_mc();
        }

        public frmBookMark()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            IEnumSpatialBookmark bookmarks = (this.imap_0 as IMapBookmarks).Bookmarks;
            bookmarks.Reset();
            ISpatialBookmark bookmark = bookmarks.Next();
            while (bookmark != null)
            {
                if (bookmark.Name == this.txtBookMarker.Text)
                {
                    break;
                }
                bookmark = bookmarks.Next();
            }
            if (bookmark != null)
            {
                if (MessageBox.Show("书签已存在，是否替换!", "书签", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                    DialogResult.Yes)
                {
                    (bookmark as IAOIBookmark).Location = (this.imap_0 as IActiveView).Extent;
                    base.DialogResult = DialogResult.OK;
                    base.Close();
                }
            }
            else
            {
                nIndex++;
                bookmark = new AOIBookmarkClass
                {
                    Name = this.txtBookMarker.Text
                };
                (bookmark as IAOIBookmark).Location = (this.imap_0 as IActiveView).Extent;
                (this.imap_0 as IMapBookmarks).AddBookmark(bookmark);
                base.DialogResult = DialogResult.OK;
                base.Close();
            }
        }

        private void frmBookMark_Load(object sender, EventArgs e)
        {
            string str = "书签 " + nIndex.ToString();
            this.txtBookMarker.Text = str;
        }

        private static void old_acctor_mc()
        {
            nIndex = 1;
        }

        public IMap Map
        {
            set { this.imap_0 = value; }
        }
    }
}