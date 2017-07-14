using System;
using System.ComponentModel;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.Plugins.Scene.Forms
{
	internal partial class frmManageBookMarker : System.Windows.Forms.Form
	{





		private Container container_0 = null;

		private IBasicMap ibasicMap_0 = null;

		public IBasicMap Map
		{
			set
			{
				this.ibasicMap_0 = value;
			}
		}

		public frmManageBookMarker()
		{
			this.InitializeComponent();
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

		private void frmManageBookMarker_Load(object sender, EventArgs e)
		{
			if (this.ibasicMap_0 is ISceneBookmarks)
			{
				IArray bookmarks = (this.ibasicMap_0 as ISceneBookmarks).Bookmarks;
				for (int i = 0; i < bookmarks.Count; i++)
				{
					IBookmark3D bookmark3D = bookmarks.get_Element(i) as IBookmark3D;
					this.listView1.Items.Add(bookmark3D.Name).Tag = bookmark3D;
				}
			}
		}

		private void btnZoomTo_Click(object sender, EventArgs e)
		{
			if (this.listView1.SelectedItems.Count == 1)
			{
				System.Windows.Forms.ListViewItem listViewItem = this.listView1.SelectedItems[0];
				if (listViewItem.Tag is IBookmark3D)
				{
					(listViewItem.Tag as IBookmark3D).Apply((this.ibasicMap_0 as IScene).SceneGraph.ActiveViewer, false, 0.0);
				}
			}
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (this.listView1.SelectedItems.Count == 1)
			{
				System.Windows.Forms.ListViewItem listViewItem = this.listView1.SelectedItems[0];
				if (listViewItem.Tag is IBookmark3D)
				{
					(this.ibasicMap_0 as ISceneBookmarks).RemoveBookmark(listViewItem.Tag as IBookmark3D);
					this.listView1.Items.Remove(listViewItem);
				}
			}
		}

		private void btnDeleteAll_Click(object sender, EventArgs e)
		{
			if (this.ibasicMap_0 is ISceneBookmarks)
			{
				(this.ibasicMap_0 as ISceneBookmarks).RemoveAllBookmarks();
				this.listView1.Items.Clear();
			}
		}
	}
}
