using System;
using System.ComponentModel;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.Plugins.Scene.Forms
{
	internal partial class frmBookMark : System.Windows.Forms.Form
	{




		private Container container_0 = null;

		private IBasicMap ibasicMap_0 = null;

		private static int nIndex;

		public IBasicMap Map
		{
			set
			{
				this.ibasicMap_0 = value;
			}
		}

		static frmBookMark()
		{
			// 注意: 此类型已标记为 'beforefieldinit'.
			frmBookMark.old_acctor_mc();
		}

		public frmBookMark()
		{
			this.InitializeComponent();
		}

	private void btnOK_Click(object sender, EventArgs e)
		{
			if (this.ibasicMap_0 is ISceneBookmarks)
			{
				IBookmark3D bookmark3D = null;
				bool flag = false;
				IArray bookmarks = (this.ibasicMap_0 as ISceneBookmarks).Bookmarks;
				for (int i = 0; i < bookmarks.Count; i++)
				{
					bookmark3D = (bookmarks.get_Element(i) as IBookmark3D);
					if (bookmark3D.Name == this.txtBookMarker.Text)
					{
					    flag = true;
						break;
					}
				}
				if (flag)
				{
					if (System.Windows.Forms.MessageBox.Show("书签已存在，是否替换!", "书签", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
					{
						bookmark3D.Capture((this.ibasicMap_0 as IScene).SceneGraph.ActiveViewer.Camera);
						base.DialogResult = System.Windows.Forms.DialogResult.OK;
						base.Close();
					}
				}
				else
				{
					frmBookMark.nIndex++;
					bookmark3D = new Bookmark3D();
					bookmark3D.Name = this.txtBookMarker.Text;
					bookmark3D.Capture((this.ibasicMap_0 as IScene).SceneGraph.ActiveViewer.Camera);
					(this.ibasicMap_0 as ISceneBookmarks).AddBookmark(bookmark3D);
					base.DialogResult = System.Windows.Forms.DialogResult.OK;
					base.Close();
				}
			}
		}

		private void frmBookMark_Load(object sender, EventArgs e)
		{
			string text = "书签 " + frmBookMark.nIndex.ToString();
			this.txtBookMarker.Text = text;
		}

		private static void old_acctor_mc()
		{
			frmBookMark.nIndex = 1;
		}
	}
}
