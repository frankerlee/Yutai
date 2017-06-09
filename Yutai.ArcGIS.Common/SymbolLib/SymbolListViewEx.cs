using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.AELicenseProvider;

namespace Yutai.ArcGIS.Common.SymbolLib
{
    //[LicenseProvider(typeof(AELicenseProviderEx))]
    public class SymbolListViewEx : System.Windows.Forms.ListView
	{
		private struct Struct5
		{
			public System.IntPtr hwndFrom;

			public int idFrom;

			public int code;
		}

		public delegate void OnValueChangedHandler(object object_0);

		internal class ItemData
		{
			private IStyleGalleryItem istyleGalleryItem_0;

			private string string_0;

			public IStyleGalleryItem StyleGalleryItem
			{
				get
				{
					return this.istyleGalleryItem_0;
				}
			}

			public string StyleFileName
			{
				get
				{
					return this.string_0;
				}
			}

			public ItemData(IStyleGalleryItem istyleGalleryItem_1, string string_1)
			{
				this.istyleGalleryItem_0 = istyleGalleryItem_1;
				this.string_0 = string_1;
			}
		}

		private const int LVM_FIRST = 4096;

		private const int LVM_GETCOLUMNORDERARRAY = 4155;

		private const int WM_HSCROLL = 276;

		private const int WM_VSCROLL = 277;

		private const int WM_SIZE = 5;

		private const int WM_NOTIFY = 78;

		private const int HDN_FIRST = -300;

		private const int HDN_BEGINDRAG = -310;

		private const int HDN_ITEMCHANGINGA = -300;

		private const int HDN_ITEMCHANGINGW = -320;

		private IContainer icontainer_0 = null;

		private System.Windows.Forms.ColumnHeader columnHeader_0;

		private System.Windows.Forms.ColumnHeader columnHeader_1;

		private System.Windows.Forms.ImageList imageList_0;

		private System.Windows.Forms.ImageList imageList_1;

		private System.Windows.Forms.ImageList imageList_2;

		private System.Windows.Forms.ImageList imageList_3;

		private int int_0 = -1;

		private System.Windows.Forms.TextBox textBox_0;

		private SymbolListViewEx.OnValueChangedHandler onValueChangedHandler_0;

		private bool bool_0 = false;

		private int int_1;

		private int int_2;

		private bool bool_1 = true;
		

		private System.Windows.Forms.ListViewItem listViewItem_0 = null;

		private bool bool_2 = false;

		public event SymbolListViewEx.OnValueChangedHandler OnValueChanged
		{
			add
			{
				SymbolListViewEx.OnValueChangedHandler onValueChangedHandler = this.onValueChangedHandler_0;
				SymbolListViewEx.OnValueChangedHandler onValueChangedHandler2;
				do
				{
					onValueChangedHandler2 = onValueChangedHandler;
					SymbolListViewEx.OnValueChangedHandler value2 = (SymbolListViewEx.OnValueChangedHandler)System.Delegate.Combine(onValueChangedHandler2, value);
					onValueChangedHandler = System.Threading.Interlocked.CompareExchange<SymbolListViewEx.OnValueChangedHandler>(ref this.onValueChangedHandler_0, value2, onValueChangedHandler2);
				}
				while (onValueChangedHandler != onValueChangedHandler2);
			}
			remove
			{
				SymbolListViewEx.OnValueChangedHandler onValueChangedHandler = this.onValueChangedHandler_0;
				SymbolListViewEx.OnValueChangedHandler onValueChangedHandler2;
				do
				{
					onValueChangedHandler2 = onValueChangedHandler;
					SymbolListViewEx.OnValueChangedHandler value2 = (SymbolListViewEx.OnValueChangedHandler)System.Delegate.Remove(onValueChangedHandler2, value);
					onValueChangedHandler = System.Threading.Interlocked.CompareExchange<SymbolListViewEx.OnValueChangedHandler>(ref this.onValueChangedHandler_0, value2, onValueChangedHandler2);
				}
				while (onValueChangedHandler != onValueChangedHandler2);
			}
		}

		public bool CanEditLabel
		{
			get
			{
				return this.bool_1;
			}
			set
			{
				this.bool_1 = value;
			}
		}

		public bool IsFolder
		{
			set
			{
				this.bool_2 = value;
				if (this.bool_2)
				{
					base.LargeImageList = this.imageList_1;
					base.SmallImageList = this.imageList_0;
					this.bool_1 = false;
				}
				else
				{
					base.LargeImageList = this.imageList_3;
					base.SmallImageList = this.imageList_2;
					this.bool_1 = false;
				}
			}
		}

		protected override void Dispose(bool bool_3)
		{
			if (bool_3 && this.icontainer_0 != null)
			{
				this.icontainer_0.Dispose();
			}
			
			base.Dispose(bool_3);
		}

		private void method_0()
		{
			this.icontainer_0 = new Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SymbolListViewEx));
			this.columnHeader_0 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader_1 = new System.Windows.Forms.ColumnHeader();
			this.imageList_0 = new System.Windows.Forms.ImageList(this.icontainer_0);
			this.imageList_1 = new System.Windows.Forms.ImageList(this.icontainer_0);
			this.imageList_2 = new System.Windows.Forms.ImageList(this.icontainer_0);
			this.imageList_3 = new System.Windows.Forms.ImageList(this.icontainer_0);
			base.SuspendLayout();
			this.columnHeader_0.Text = "名称";
			this.columnHeader_0.Width = 120;
			this.columnHeader_1.Text = "种类";
			//this.imageList_0.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("sImageList.ImageStream");
			this.imageList_0.TransparentColor = Color.Transparent;
			//this.imageList_0.Images.SetKeyName(0, "");
			//this.imageList_1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("lImageList.ImageStream");
			this.imageList_1.TransparentColor = Color.Transparent;
			//this.imageList_1.Images.SetKeyName(0, "");
			this.imageList_2.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList_2.ImageSize = new Size(16, 16);
			this.imageList_2.TransparentColor = Color.Transparent;
			this.imageList_3.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList_3.ImageSize = new Size(40, 40);
			this.imageList_3.TransparentColor = Color.Transparent;
			base.Columns.AddRange(new System.Windows.Forms.ColumnHeader[]
			{
				this.columnHeader_0,
				this.columnHeader_1
			});
			base.LargeImageList = this.imageList_3;
			base.SmallImageList = this.imageList_2;
			base.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.SymbolListViewEx_MouseDoubleClick);
			base.SelectedIndexChanged += new System.EventHandler(this.SymbolListViewEx_SelectedIndexChanged);
			base.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SymbolListViewEx_MouseDown);
			base.ResumeLayout(false);
		}

		[System.Runtime.InteropServices.DllImport("user32.dll")]
		private static extern System.IntPtr SendMessage(System.IntPtr intptr_0, int int_3, System.IntPtr intptr_1, System.IntPtr intptr_2);

		[System.Runtime.InteropServices.DllImport("user32.dll", CharSet = CharSet.Ansi)]
		private static extern System.IntPtr SendMessage(System.IntPtr intptr_0, int int_3, int int_4, ref int[] int_5);

		public SymbolListViewEx()
		{
			this.method_0();
			this.textBox_0 = new System.Windows.Forms.TextBox();
			this.textBox_0.Font = this.Font;
			this.textBox_0.Size = new Size(0, 0);
			this.textBox_0.Location = new Point(0, 0);
			this.textBox_0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_0.AutoSize = true;
			base.Controls.Add(this.textBox_0);
			this.textBox_0.LostFocus += new System.EventHandler(this.textBox_0_LostFocus);
			this.textBox_0.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_0_KeyPress);
			this.textBox_0.Text = "";
			this.textBox_0.Visible = false;
		}

		public Rectangle GetSubItemBounds(System.Windows.Forms.ListViewItem listViewItem_1, int int_3)
		{
			int[] columnOrder = this.GetColumnOrder();
			Rectangle empty = Rectangle.Empty;
			if (int_3 >= columnOrder.Length)
			{
				throw new System.IndexOutOfRangeException("SubItem " + int_3 + " out of range");
			}
			if (listViewItem_1 == null)
			{
				throw new System.ArgumentNullException("Item");
			}
			Rectangle bounds = listViewItem_1.GetBounds(System.Windows.Forms.ItemBoundsPortion.Label);
			int num = bounds.Left;
			int i;
			for (i = 0; i < columnOrder.Length; i++)
			{
				System.Windows.Forms.ColumnHeader columnHeader = base.Columns[columnOrder[i]];
				if (columnHeader.Index == int_3)
				{
					break;
				}
				num += columnHeader.Width;
			}
			empty = new Rectangle(num, bounds.Top, base.Columns[columnOrder[i]].Width, bounds.Height);
			return empty;
		}

		public int[] GetColumnOrder()
		{
			System.IntPtr intPtr = System.Runtime.InteropServices.Marshal.AllocHGlobal(System.Runtime.InteropServices.Marshal.SizeOf(typeof(int)) * base.Columns.Count);
			int[] result;
			if (SymbolListViewEx.SendMessage(base.Handle, 4155, new System.IntPtr(base.Columns.Count), intPtr).ToInt32() == 0)
			{
				System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
				result = null;
			}
			else
			{
				int[] array = new int[base.Columns.Count];
				System.Runtime.InteropServices.Marshal.Copy(intPtr, array, 0, base.Columns.Count);
				System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
				result = array;
			}
			return result;
		}

		private void textBox_0_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.bool_0 = false;
				this.textBox_0.Hide();
			}
			else if (e.KeyChar == '\u001b')
			{
				this.bool_0 = true;
				this.textBox_0.Hide();
			}
		}

		private void textBox_0_LostFocus(object sender, System.EventArgs e)
		{
			if (this.bool_0)
			{
				this.bool_0 = false;
				this.textBox_0.Hide();
			}
			else
			{
				if (this.listViewItem_0 != null)
				{
					if (this.int_0 != -1 && this.listViewItem_0.SubItems[this.int_0].Text != this.textBox_0.Text)
					{
						this.listViewItem_0.SubItems[this.int_0].Text = this.textBox_0.Text;
						if (this.int_0 == 0)
						{
							if (this.listViewItem_0.Tag is SymbolListViewEx.ItemData)
							{
								(this.listViewItem_0.Tag as SymbolListViewEx.ItemData).StyleGalleryItem.Name = this.textBox_0.Text;
							}
							else
							{
								(this.listViewItem_0.Tag as IStyleGalleryItem).Name = this.textBox_0.Text;
							}
						}
						else if (this.listViewItem_0.Tag is SymbolListViewEx.ItemData)
						{
							(this.listViewItem_0.Tag as SymbolListViewEx.ItemData).StyleGalleryItem.Category = this.textBox_0.Text;
						}
						else
						{
							(this.listViewItem_0.Tag as IStyleGalleryItem).Category = this.textBox_0.Text;
						}
						if (this.onValueChangedHandler_0 != null)
						{
							this.onValueChangedHandler_0(this.listViewItem_0.Tag);
						}
						this.int_0 = -1;
					}
					this.listViewItem_0 = null;
				}
				this.bool_0 = true;
				this.textBox_0.Hide();
			}
		}

		private void SymbolListViewEx_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		}

		public void UpdateItem(System.Windows.Forms.ListViewItem listViewItem_1)
		{
			if (listViewItem_1 != null)
			{
				if (listViewItem_1.Tag is IStyleGalleryItem)
				{
					string imageKey = listViewItem_1.ImageKey;
					IStyleDraw styleDraw = StyleDrawFactory.CreateStyleDraw((listViewItem_1.Tag as IStyleGalleryItem).Item);
					if (styleDraw != null)
					{
						Bitmap image = styleDraw.StyleGalleryItemToBmp(this.imageList_3.ImageSize, 96.0, 1.0);
						Bitmap image2 = styleDraw.StyleGalleryItemToBmp(this.imageList_2.ImageSize, 96.0, 1.0);
						int arg_A3_0 = this.imageList_3.Images.Count;
						this.imageList_3.Images.RemoveByKey(imageKey);
						this.imageList_3.Images.Add(imageKey, image);
						this.imageList_2.Images.RemoveByKey(imageKey);
						this.imageList_2.Images.Add(imageKey, image2);
					}
				}
				else if (listViewItem_1.Tag is SymbolListViewEx.ItemData)
				{
					string imageKey = listViewItem_1.ImageKey;
					IStyleDraw styleDraw = StyleDrawFactory.CreateStyleDraw((listViewItem_1.Tag as SymbolListViewEx.ItemData).StyleGalleryItem.Item);
					if (styleDraw != null)
					{
						Bitmap image = styleDraw.StyleGalleryItemToBmp(this.imageList_3.ImageSize, 96.0, 1.0);
						Bitmap image2 = styleDraw.StyleGalleryItemToBmp(this.imageList_2.ImageSize, 96.0, 1.0);
						int arg_18B_0 = this.imageList_3.Images.Count;
						this.imageList_3.Images.RemoveByKey(imageKey);
						this.imageList_3.Images.Add(imageKey, image);
						this.imageList_2.Images.RemoveByKey(imageKey);
						this.imageList_2.Images.Add(imageKey, image2);
					}
				}
			}
		}

		public void SetItemSize(int int_3, int int_4, int int_5, int int_6)
		{
			this.imageList_2.ImageSize = new Size(int_3, int_4);
			this.imageList_3.ImageSize = new Size(int_5, int_6);
			base.SmallImageList.ImageSize = new Size(int_3, int_4);
			base.LargeImageList.ImageSize = new Size(int_5, int_6);
		}

		public IStyleGalleryItem GetSelectStyleGalleryItem()
		{
			IStyleGalleryItem result;
			if (base.SelectedItems.Count > 0)
			{
				IStyleGalleryItem styleGalleryItem = null;
				if (base.SelectedItems[0].Tag is IStyleGalleryItem)
				{
					styleGalleryItem = (IStyleGalleryItem)base.SelectedItems[0].Tag;
				}
				else if (base.SelectedItems[0].Tag is SymbolListViewEx.ItemData)
				{
					styleGalleryItem = ((SymbolListViewEx.ItemData)base.SelectedItems[0].Tag).StyleGalleryItem;
				}
				result = styleGalleryItem;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public string GetSelectItemStyleFileName()
		{
			string result;
			if (base.SelectedItems.Count > 0)
			{
				if (base.SelectedItems[0].Tag is IStyleGalleryItem)
				{
					result = "";
					return result;
				}
				if (base.SelectedItems[0].Tag is SymbolListViewEx.ItemData)
				{
					result = ((SymbolListViewEx.ItemData)base.SelectedItems[0].Tag).StyleFileName;
					return result;
				}
			}
			result = "";
			return result;
		}

		public void SetSelectSymbol(string string_0, string string_1)
		{
			string_1 = string_1.ToLower();
			for (int i = 0; i < base.Items.Count; i++)
			{
				System.Windows.Forms.ListViewItem listViewItem = base.Items[i];
				IStyleGalleryItem styleGalleryItem = null;
				if (listViewItem.Tag is IStyleGalleryItem)
				{
					styleGalleryItem = (listViewItem.Tag as IStyleGalleryItem);
				}
				else if (listViewItem.Tag is SymbolListViewEx.ItemData)
				{
					styleGalleryItem = (listViewItem.Tag as SymbolListViewEx.ItemData).StyleGalleryItem;
				}
				if (styleGalleryItem != null && styleGalleryItem.Name.ToLower() == string_1)
				{
					base.SelectedIndices.Clear();
					base.SelectedIndices.Add(i);
					return;
				}
			}
		}

		public void DeleteSelectItem()
		{
			if (base.SelectedItems.Count != 0)
			{
				for (int i = base.SelectedItems.Count - 1; i >= 0; i--)
				{
					string imageKey = base.SelectedItems[i].ImageKey;
					this.imageList_3.Images.RemoveByKey(imageKey);
					this.imageList_2.Images.RemoveByKey(imageKey);
					base.Items.Remove(base.SelectedItems[i]);
				}
			}
		}

		public void RemoveAll()
		{
			this.imageList_3.Images.Clear();
			this.imageList_2.Images.Clear();
			base.Items.Clear();
		}

		public IStyleGalleryItem GetStyleGalleryItem(int int_3)
		{
			IStyleGalleryItem result;
			if (int_3 < 0 || int_3 >= base.Items.Count)
			{
				result = null;
			}
			else
			{
				result = (IStyleGalleryItem)base.Items[int_3];
			}
			return result;
		}

		public void Add(IStyleGalleryItem istyleGalleryItem_0, string string_0)
		{
			IStyleDraw styleDraw = StyleDrawFactory.CreateStyleDraw(istyleGalleryItem_0.Item);
			if (styleDraw != null)
			{
				string[] items = new string[]
				{
					istyleGalleryItem_0.Name,
					istyleGalleryItem_0.Category
				};
				Bitmap image = styleDraw.StyleGalleryItemToBmp(this.imageList_3.ImageSize, 96.0, 1.0);
				Bitmap image2 = styleDraw.StyleGalleryItemToBmp(this.imageList_2.ImageSize, 96.0, 1.0);
				int count = this.imageList_3.Images.Count;
				this.imageList_3.Images.Add(count.ToString(), image);
				this.imageList_2.Images.Add(count.ToString(), image2);
				System.Windows.Forms.ListViewItem listViewItem = new System.Windows.Forms.ListViewItem(items, count.ToString());
				listViewItem.Tag = new SymbolListViewEx.ItemData(istyleGalleryItem_0, string_0);
				base.Items.Add(listViewItem);
			}
		}

		public void Add(IStyleGalleryItem istyleGalleryItem_0)
		{
			IStyleDraw styleDraw = StyleDrawFactory.CreateStyleDraw(istyleGalleryItem_0.Item);
			if (styleDraw != null)
			{
				string[] items = new string[]
				{
					istyleGalleryItem_0.Name,
					istyleGalleryItem_0.Category
				};
				Bitmap image = styleDraw.StyleGalleryItemToBmp(this.imageList_3.ImageSize, 96.0, 1.0);
				Bitmap image2 = styleDraw.StyleGalleryItemToBmp(this.imageList_2.ImageSize, 96.0, 1.0);
				int count = this.imageList_3.Images.Count;
				this.imageList_3.Images.Add(count.ToString(), image);
				this.imageList_2.Images.Add(count.ToString(), image2);
				System.Windows.Forms.ListViewItem listViewItem = new System.Windows.Forms.ListViewItem(items, count.ToString());
				listViewItem.Tag = istyleGalleryItem_0;
				base.Items.Add(listViewItem);
			}
		}

		private void SymbolListViewEx_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (this.bool_1)
			{
				this.int_1 = e.X;
				this.int_2 = e.Y;
			}
		}

		private void SymbolListViewEx_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (this.bool_1 && base.SelectedItems.Count > 0)
			{
				Rectangle itemRect = base.GetItemRect(base.SelectedIndices[0], System.Windows.Forms.ItemBoundsPortion.Icon);
				if (base.View == System.Windows.Forms.View.LargeIcon)
				{
					if (this.int_2 > itemRect.Bottom)
					{
						this.int_0 = -1;
						Rectangle itemRect2 = base.GetItemRect(base.SelectedIndices[0], System.Windows.Forms.ItemBoundsPortion.Label);
						if (this.int_1 >= itemRect2.Left && this.int_1 <= itemRect2.Right && this.int_2 >= itemRect2.Top && this.int_2 <= itemRect2.Bottom)
						{
							this.int_0 = 0;
							this.bool_0 = false;
							this.listViewItem_0 = base.SelectedItems[0];
							Rectangle subItemBounds = this.GetSubItemBounds(base.SelectedItems[0], 0);
							if (subItemBounds.X < 0)
							{
								subItemBounds.Width += subItemBounds.X;
								subItemBounds.X = 0;
							}
							if (subItemBounds.X + subItemBounds.Width > base.Width)
							{
								subItemBounds.Width = base.Width - subItemBounds.Left;
							}
							subItemBounds.Offset(base.Left, base.Top);
							Point p = new Point(0, 0);
							Point point = base.Parent.PointToScreen(p);
							Point point2 = this.textBox_0.Parent.PointToScreen(p);
							subItemBounds.Offset(point.X - point2.X, point.Y - point2.Y);
							base.GetItemRect(base.SelectedIndices[0], System.Windows.Forms.ItemBoundsPortion.Entire);
							this.textBox_0.Multiline = true;
							this.textBox_0.Bounds = subItemBounds;
							this.textBox_0.Show();
							this.textBox_0.Text = this.listViewItem_0.SubItems[this.int_0].Text;
							this.textBox_0.SelectAll();
							this.textBox_0.Focus();
						}
					}
				}
				else if (this.int_1 > itemRect.Right)
				{
					Rectangle itemRect2 = base.GetItemRect(base.SelectedIndices[0]);
					int num = this.int_1;
					int num2 = itemRect.Right;
					int num3 = itemRect.Right;
					int i;
					for (i = 0; i < base.Columns.Count; i++)
					{
						num2 = num3;
						num3 += base.Columns[i].Width;
						if (i == 0)
						{
							num3 -= itemRect.Width;
						}
						if (num > num2 && num < num3)
						{
							break;
						}
					}
					this.int_0 = -1;
					if (base.SelectedItems.Count == 1)
					{
						this.bool_0 = false;
						this.int_0 = i;
						this.listViewItem_0 = base.SelectedItems[0];
						Rectangle itemRect3 = base.GetItemRect(base.Items.IndexOf(this.listViewItem_0), System.Windows.Forms.ItemBoundsPortion.Label);
						this.textBox_0.Multiline = false;
						this.textBox_0.Size = new Size(num3 - num2, this.listViewItem_0.Bounds.Height);
						this.textBox_0.Location = new Point(num2, itemRect3.Y);
						this.textBox_0.Show();
						this.textBox_0.Text = this.listViewItem_0.SubItems[this.int_0].Text;
						this.textBox_0.SelectAll();
						this.textBox_0.Focus();
					}
				}
			}
		}
	}
}
