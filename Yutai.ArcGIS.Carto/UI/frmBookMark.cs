using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.UI
{
    public class frmBookMark : Form
    {
        private SimpleButton btnOK;
        private Container container_0 = null;
        private IMap imap_0 = null;
        private Label label1;
        private static int nIndex;
        private SimpleButton simpleButton2;
        private TextEdit txtBookMarker;

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
                if (MessageBox.Show("书签已存在，是否替换!", "书签", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    (bookmark as IAOIBookmark).Location = (this.imap_0 as IActiveView).Extent;
                    base.DialogResult = DialogResult.OK;
                    base.Close();
                }
            }
            else
            {
                nIndex++;
                bookmark = new AOIBookmarkClass {
                    Name = this.txtBookMarker.Text
                };
                (bookmark as IAOIBookmark).Location = (this.imap_0 as IActiveView).Extent;
                (this.imap_0 as IMapBookmarks).AddBookmark(bookmark);
                base.DialogResult = DialogResult.OK;
                base.Close();
            }
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmBookMark_Load(object sender, EventArgs e)
        {
            string str = "书签 " + nIndex.ToString();
            this.txtBookMarker.Text = str;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBookMark));
            this.label1 = new Label();
            this.txtBookMarker = new TextEdit();
            this.btnOK = new SimpleButton();
            this.simpleButton2 = new SimpleButton();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 0x10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x3b, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "书签名称:";
            this.txtBookMarker.EditValue = "";
            this.txtBookMarker.Location = new Point(0x68, 8);
            this.txtBookMarker.Name = "txtBookMarker";
            this.txtBookMarker.Size = new Size(160, 0x15);
            this.txtBookMarker.TabIndex = 1;
            this.btnOK.Location = new Point(0x88, 40);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x40, 0x18);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.simpleButton2.DialogResult = DialogResult.Cancel;
            this.simpleButton2.Location = new Point(0xd0, 40);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new Size(0x40, 0x18);
            this.simpleButton2.TabIndex = 3;
            this.simpleButton2.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x124, 0x4c);
            base.Controls.Add(this.simpleButton2);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.txtBookMarker);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = ((System.Drawing.Icon)resources.GetObject("$Icon"));
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmBookMark";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "创建书签";
            base.Load += new EventHandler(this.frmBookMark_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private static void old_acctor_mc()
        {
            nIndex = 1;
        }

        public IMap Map
        {
            set
            {
                this.imap_0 = value;
            }
        }
    }
}

