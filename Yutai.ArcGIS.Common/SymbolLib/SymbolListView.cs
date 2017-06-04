using System.ComponentModel;
using System.Drawing;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Common.SymbolLib
{
	internal class SymbolListView : System.Windows.Forms.ListView
	{
		public delegate void OnValueChangedHandler(object object_0);

		private SymbolListView.OnValueChangedHandler onValueChangedHandler_0;

		private System.Windows.Forms.ListViewItem listViewItem_0 = null;

		private bool bool_0 = false;

		private int int_0;

		private int int_1;

		private System.Windows.Forms.TextBox textBox_0;

		private System.Windows.Forms.ColumnHeader columnHeader_0;

		private System.Windows.Forms.ColumnHeader columnHeader_1;

		private System.Windows.Forms.ImageList imageList_0;

		private System.Windows.Forms.ImageList imageList_1;

		private System.Windows.Forms.ImageList imageList_2;

		private System.Windows.Forms.ImageList imageList_3;

		private IContainer icontainer_0;

		private bool bool_1 = false;

		private bool bool_2 = false;

		private int int_2 = -1;

		public event SymbolListView.OnValueChangedHandler OnValueChanged
		{
			add
			{
				SymbolListView.OnValueChangedHandler onValueChangedHandler = this.onValueChangedHandler_0;
				SymbolListView.OnValueChangedHandler onValueChangedHandler2;
				do
				{
					onValueChangedHandler2 = onValueChangedHandler;
					SymbolListView.OnValueChangedHandler value2 = (SymbolListView.OnValueChangedHandler)System.Delegate.Combine(onValueChangedHandler2, value);
					onValueChangedHandler = System.Threading.Interlocked.CompareExchange<SymbolListView.OnValueChangedHandler>(ref this.onValueChangedHandler_0, value2, onValueChangedHandler2);
				}
				while (onValueChangedHandler != onValueChangedHandler2);
			}
			remove
			{
				SymbolListView.OnValueChangedHandler onValueChangedHandler = this.onValueChangedHandler_0;
				SymbolListView.OnValueChangedHandler onValueChangedHandler2;
				do
				{
					onValueChangedHandler2 = onValueChangedHandler;
					SymbolListView.OnValueChangedHandler value2 = (SymbolListView.OnValueChangedHandler)System.Delegate.Remove(onValueChangedHandler2, value);
					onValueChangedHandler = System.Threading.Interlocked.CompareExchange<SymbolListView.OnValueChangedHandler>(ref this.onValueChangedHandler_0, value2, onValueChangedHandler2);
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
				}
				else
				{
					base.LargeImageList = this.imageList_3;
					base.SmallImageList = this.imageList_2;
				}
			}
		}

		public SymbolListView()
		{
			this.method_0();
			base.SetStyle(System.Windows.Forms.ControlStyles.UserPaint, true);
			base.SetStyle(System.Windows.Forms.ControlStyles.DoubleBuffer, true);
			this.textBox_0 = new System.Windows.Forms.TextBox();
			this.textBox_0.AutoSize = true;
			base.Controls.Add(this.textBox_0);
			this.textBox_0.LostFocus += new System.EventHandler(this.textBox_0_LostFocus);
			this.textBox_0.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_0_KeyPress);
			this.textBox_0.Visible = false;
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
			System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager(typeof(SymbolListView));
			this.columnHeader_0 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader_1 = new System.Windows.Forms.ColumnHeader();
			this.imageList_0 = new System.Windows.Forms.ImageList(this.icontainer_0);
			this.imageList_1 = new System.Windows.Forms.ImageList(this.icontainer_0);
			this.imageList_2 = new System.Windows.Forms.ImageList(this.icontainer_0);
			this.imageList_3 = new System.Windows.Forms.ImageList(this.icontainer_0);
			this.columnHeader_0.Text = "名称";
			this.columnHeader_0.Width = 120;
			this.columnHeader_1.Text = "种类";
			this.imageList_0.ImageSize = new Size(16, 16);
		//	this.imageList_0.ImageStream = (System.Windows.Forms.ImageListStreamer)resourceManager.GetObject("sImageList.ImageStream");
			this.imageList_0.TransparentColor = Color.Transparent;
			this.imageList_1.ImageSize = new Size(48, 48);
		//	this.imageList_1.ImageStream = (System.Windows.Forms.ImageListStreamer)resourceManager.GetObject("lImageList.ImageStream");
			this.imageList_1.TransparentColor = Color.Transparent;
			this.imageList_2.ImageSize = new Size(16, 16);
			this.imageList_2.TransparentColor = Color.Transparent;
			this.imageList_3.ImageSize = new Size(48, 48);
			this.imageList_3.TransparentColor = Color.Transparent;
			base.Columns.AddRange(new System.Windows.Forms.ColumnHeader[]
			{
				this.columnHeader_0,
				this.columnHeader_1
			});
			base.LargeImageList = this.imageList_3;
			base.SmallImageList = this.imageList_2;
			base.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SymbolListView_MouseDown);
			base.DoubleClick += new System.EventHandler(this.SymbolListView_DoubleClick);
			base.StyleChanged += new System.EventHandler(this.SymbolListView_StyleChanged);
			base.SelectedIndexChanged += new System.EventHandler(this.SymbolListView_SelectedIndexChanged);
		}

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs paintEventArgs_0)
		{
			base.OnPaint(paintEventArgs_0);
			StringFormat stringFormat = new StringFormat();
			stringFormat.LineAlignment = StringAlignment.Center;
			if (base.View == System.Windows.Forms.View.Details)
			{
				for (int i = 0; i < base.Items.Count; i++)
				{
					Rectangle bounds = base.Items[i].GetBounds(System.Windows.Forms.ItemBoundsPortion.Icon);
					if (bounds.Bottom >= 0 && bounds.Top <= base.ClientRectangle.Height)
					{
						if (base.Items[i].SubItems[0].Text.Length == 0)
						{
							bounds.Width = base.Columns[0].Width + bounds.Width;
						}
						Rectangle bounds2 = base.Items[i].GetBounds(System.Windows.Forms.ItemBoundsPortion.Entire);
						float x = (float)(bounds2.Left + bounds.Width);
						float y = (float)bounds2.Top;
						RectangleF rectangleF = new RectangleF(x, y, 0f, (float)bounds2.Height);
						if (base.Items[i].Tag != null)
						{
							Graphics graphics = paintEventArgs_0.Graphics;
							System.IntPtr hdc = graphics.GetHdc();
							if (base.Items[i].Tag is ISymbol)
							{
								this.DrawSymbol(hdc.ToInt32(), bounds, base.Items[i].Tag);
							}
							else if (base.Items[i].Tag is IStyleGalleryItem)
							{
								this.DrawSymbol(hdc.ToInt32(), bounds, (base.Items[i].Tag as IStyleGalleryItem).Item);
							}
							graphics.ReleaseHdc(hdc);
						}
						SolidBrush solidBrush;
						SolidBrush solidBrush2;
						if (base.Items[i].Selected)
						{
							solidBrush = new SolidBrush(base.Items[i].ForeColor);
							solidBrush2 = new SolidBrush(base.Items[i].BackColor);
						}
						else
						{
							solidBrush = new SolidBrush(base.Items[i].BackColor);
							solidBrush2 = new SolidBrush(base.Items[i].ForeColor);
						}
						for (int j = 0; j < base.Items[i].SubItems.Count; j++)
						{
							if (j > 1)
							{
								rectangleF.X += (float)base.Columns[j - 1].Width;
							}
							else if (j == 1)
							{
								rectangleF.X = rectangleF.X + (float)base.Columns[0].Width - (float)bounds.Width;
							}
							if (j == 0)
							{
								rectangleF.Width = (float)(base.Columns[j].Width - bounds.Width);
							}
							else
							{
								rectangleF.Width = (float)base.Columns[j].Width;
							}
							if (j == 0 || base.FullRowSelect)
							{
								if (j != 0 || base.Items[i].SubItems[j].Text.Length != 0)
								{
									paintEventArgs_0.Graphics.FillRectangle(solidBrush, rectangleF);
									paintEventArgs_0.Graphics.DrawString(base.Items[i].SubItems[j].Text, this.Font, solidBrush2, rectangleF);
								}
							}
							else
							{
								paintEventArgs_0.Graphics.DrawString(base.Items[i].SubItems[j].Text, this.Font, Brushes.Black, rectangleF);
							}
						}
						solidBrush.Dispose();
						solidBrush2.Dispose();
					}
				}
			}
			else if (base.View == System.Windows.Forms.View.LargeIcon || base.View == System.Windows.Forms.View.SmallIcon || base.View == System.Windows.Forms.View.List)
			{
				for (int i = 0; i < base.Items.Count; i++)
				{
					Rectangle bounds = base.Items[i].GetBounds(System.Windows.Forms.ItemBoundsPortion.Icon);
					if (bounds.Bottom >= 0 && bounds.Top <= base.ClientRectangle.Height)
					{
						if (base.Items[i].Tag != null)
						{
							Graphics graphics = paintEventArgs_0.Graphics;
							System.IntPtr hdc = graphics.GetHdc();
							if (base.Items[i].Tag is ISymbol)
							{
								this.DrawSymbol(hdc.ToInt32(), bounds, base.Items[i].Tag);
							}
							else if (base.Items[i].Tag is IStyleGalleryItem)
							{
								this.DrawSymbol(hdc.ToInt32(), bounds, (base.Items[i].Tag as IStyleGalleryItem).Item);
							}
							graphics.ReleaseHdc(hdc);
						}
						SolidBrush solidBrush;
						SolidBrush solidBrush2;
						if (base.Items[i].Selected)
						{
							solidBrush = new SolidBrush(base.Items[i].ForeColor);
							solidBrush2 = new SolidBrush(base.Items[i].BackColor);
						}
						else
						{
							solidBrush = new SolidBrush(base.Items[i].BackColor);
							solidBrush2 = new SolidBrush(base.Items[i].ForeColor);
						}
						Rectangle bounds2 = base.Items[i].GetBounds(System.Windows.Forms.ItemBoundsPortion.Label);
						RectangleF rectangleF = new RectangleF((float)bounds2.X, (float)bounds2.Y, (float)bounds2.Width, (float)bounds2.Height);
						paintEventArgs_0.Graphics.FillRectangle(solidBrush, rectangleF);
						paintEventArgs_0.Graphics.DrawString(base.Items[i].Text, this.Font, solidBrush2, rectangleF, stringFormat);
						solidBrush.Dispose();
						solidBrush2.Dispose();
					}
				}
			}
		}

		public void RemoveAll()
		{
			base.Items.Clear();
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
				IStyleGalleryItem styleGalleryItem = (IStyleGalleryItem)base.SelectedItems[0].Tag;
				result = styleGalleryItem;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public void DeleteSelectItem()
		{
			if (base.SelectedItems.Count != 0)
			{
				for (int i = base.SelectedItems.Count - 1; i >= 0; i--)
				{
					base.Items.Remove(base.SelectedItems[i]);
				}
			}
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

		public void Add(IStyleGalleryItem istyleGalleryItem_0)
		{
			System.Windows.Forms.ListViewItem listViewItem = new System.Windows.Forms.ListViewItem(new string[]
			{
				istyleGalleryItem_0.Name,
				istyleGalleryItem_0.Category
			});
			listViewItem.Tag = istyleGalleryItem_0;
			base.Items.Add(listViewItem);
		}

		protected void DrawSymbol(int int_3, Rectangle rectangle_0, object object_0)
		{
			if (object_0 != null)
			{
				if (object_0 is IMapGrid)
				{
					tagRECT tagRECT = default(tagRECT);
					tagRECT.left = rectangle_0.Left;
					tagRECT.right = rectangle_0.Right;
					tagRECT.top = rectangle_0.Top;
					tagRECT.bottom = rectangle_0.Bottom;
					IMapFrame mapFrame = new MapFrame() as IMapFrame;
					IMap map = new Map();
					mapFrame.Map = map;
					(object_0 as IMapGrid).PrepareForOutput(int_3, 96, ref tagRECT, mapFrame);
				}
				else
				{
					IStyleDraw styleDraw = StyleDrawFactory.CreateStyleDraw(object_0);
					if (styleDraw != null)
					{
						styleDraw.Draw(int_3, rectangle_0, 1.0, 1.0);
					}
				}
			}
		}

		private void SymbolListView_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			base.Invalidate();
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
					if (this.int_2 != -1 && this.listViewItem_0.SubItems[this.int_2].Text != this.textBox_0.Text)
					{
						this.listViewItem_0.SubItems[this.int_2].Text = this.textBox_0.Text;
						if (this.int_2 == 0)
						{
							(this.listViewItem_0.Tag as IStyleGalleryItem).Name = this.textBox_0.Text;
						}
						else
						{
							(this.listViewItem_0.Tag as IStyleGalleryItem).Category = this.textBox_0.Text;
						}
						if (this.onValueChangedHandler_0 != null)
						{
							this.onValueChangedHandler_0(this.listViewItem_0.Tag);
						}
						this.int_2 = -1;
					}
					this.listViewItem_0 = null;
				}
				this.bool_0 = true;
				this.textBox_0.Hide();
			}
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

		private void SymbolListView_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (this.bool_1)
			{
				this.int_0 = e.X;
				this.int_1 = e.Y;
			}
		}

		private void SymbolListView_DoubleClick(object sender, System.EventArgs e)
		{
			if (this.bool_1 && base.SelectedItems.Count > 0)
			{
				Rectangle itemRect = base.GetItemRect(base.SelectedIndices[0], System.Windows.Forms.ItemBoundsPortion.Icon);
				if (base.View == System.Windows.Forms.View.LargeIcon)
				{
					if (this.int_1 > itemRect.Bottom)
					{
						this.int_2 = -1;
						Rectangle itemRect2 = base.GetItemRect(base.SelectedIndices[0], System.Windows.Forms.ItemBoundsPortion.Label);
						if (this.int_0 >= itemRect2.Left && this.int_0 <= itemRect2.Right && this.int_1 >= itemRect2.Top && this.int_1 <= itemRect2.Bottom)
						{
							this.int_2 = 0;
							this.bool_0 = false;
							this.listViewItem_0 = base.SelectedItems[0];
							this.textBox_0.Size = new Size(itemRect.Width, itemRect2.Height);
							this.textBox_0.Location = new Point(itemRect.X, itemRect2.Y);
							this.textBox_0.Show();
							this.textBox_0.Text = this.listViewItem_0.SubItems[this.int_2].Text;
							this.textBox_0.SelectAll();
							this.textBox_0.Focus();
						}
					}
				}
				else if (this.int_0 > itemRect.Right)
				{
					Rectangle itemRect2 = base.GetItemRect(base.SelectedIndices[0]);
					int num = this.int_0;
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
					this.int_2 = -1;
					if (base.SelectedItems.Count == 1)
					{
						this.bool_0 = false;
						this.int_2 = i;
						this.listViewItem_0 = base.SelectedItems[0];
						Rectangle itemRect3 = base.GetItemRect(base.Items.IndexOf(this.listViewItem_0), System.Windows.Forms.ItemBoundsPortion.Label);
						this.textBox_0.Size = new Size(num3 - num2, this.listViewItem_0.Bounds.Height);
						this.textBox_0.Location = new Point(num2, itemRect3.Y);
						this.textBox_0.Show();
						this.textBox_0.Text = this.listViewItem_0.SubItems[this.int_2].Text;
						this.textBox_0.SelectAll();
						this.textBox_0.Focus();
					}
				}
			}
		}

		private void SymbolListView_StyleChanged(object sender, System.EventArgs e)
		{
		}
	}
}
