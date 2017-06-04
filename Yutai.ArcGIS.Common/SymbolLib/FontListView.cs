using System.ComponentModel;
using System.Drawing;

namespace Yutai.ArcGIS.Common.SymbolLib
{
	public class FontListView : System.Windows.Forms.UserControl
	{
		private Container container_0 = null;

		private Size size_0 = new Size(20, 20);

		private int int_0 = 1;

		private System.Collections.ArrayList arrayList_0 = new System.Collections.ArrayList();

		private int int_1 = -1;

		private SelectedIndexChangedHandler selectedIndexChangedHandler_0;

		[Category("Events"), Description("选择变化事件")]
		public event SelectedIndexChangedHandler SelectedIndexChanged
		{
			add
			{
				SelectedIndexChangedHandler selectedIndexChangedHandler = this.selectedIndexChangedHandler_0;
				SelectedIndexChangedHandler selectedIndexChangedHandler2;
				do
				{
					selectedIndexChangedHandler2 = selectedIndexChangedHandler;
					SelectedIndexChangedHandler value2 = (SelectedIndexChangedHandler)System.Delegate.Combine(selectedIndexChangedHandler2, value);
					selectedIndexChangedHandler = System.Threading.Interlocked.CompareExchange<SelectedIndexChangedHandler>(ref this.selectedIndexChangedHandler_0, value2, selectedIndexChangedHandler2);
				}
				while (selectedIndexChangedHandler != selectedIndexChangedHandler2);
			}
			remove
			{
				SelectedIndexChangedHandler selectedIndexChangedHandler = this.selectedIndexChangedHandler_0;
				SelectedIndexChangedHandler selectedIndexChangedHandler2;
				do
				{
					selectedIndexChangedHandler2 = selectedIndexChangedHandler;
					SelectedIndexChangedHandler value2 = (SelectedIndexChangedHandler)System.Delegate.Remove(selectedIndexChangedHandler2, value);
					selectedIndexChangedHandler = System.Threading.Interlocked.CompareExchange<SelectedIndexChangedHandler>(ref this.selectedIndexChangedHandler_0, value2, selectedIndexChangedHandler2);
				}
				while (selectedIndexChangedHandler != selectedIndexChangedHandler2);
			}
		}

		[Description("设置没个Item的大小")]
		public Size ItemSize
		{
			get
			{
				return this.size_0;
			}
			set
			{
				this.size_0 = value;
				this.int_0 = base.ClientRectangle.Width / this.size_0.Width;
				this.SetScroll();
			}
		}

		public System.Collections.ArrayList Items
		{
			get
			{
				return this.arrayList_0;
			}
		}

		[DefaultValue(0), Description("或取或设置选中的Item")]
		public int SelectedIndex
		{
			get
			{
				return this.int_1;
			}
			set
			{
				this.int_1 = value;
			}
		}

		public new Size Size
		{
			get
			{
				return base.Size;
			}
			set
			{
				base.Size = value;
				this.int_0 = this.Size.Width / this.size_0.Width;
				this.SetScroll();
			}
		}

		public FontListView()
		{
			this.InitializeComponent();
		}

		protected override void Dispose(bool bool_0)
		{
			if (bool_0 && this.container_0 != null)
			{
				this.container_0.Dispose();
			}
			base.Dispose(bool_0);
		}

		private void InitializeComponent()
		{
			this.AutoScroll = true;
			this.BackColor = SystemColors.ControlLightLight;
			base.Name = "FontListView";
			base.Paint += new System.Windows.Forms.PaintEventHandler(this.FontListView_Paint);
			base.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FontListView_MouseDown);
		}

		private void FontListView_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			int num = 0;
			int num2 = base.AutoScrollPosition.X;
			int num3 = base.AutoScrollPosition.Y;
			StringFormat stringFormat = new StringFormat();
			stringFormat.Alignment = StringAlignment.Center;
			for (int i = 0; i < this.arrayList_0.Count; i++)
			{
				if (num3 >= -this.size_0.Height)
				{
					Rectangle rectangle = new Rectangle(num2, num3, this.size_0.Width, this.size_0.Height);
					e.Graphics.DrawRectangle(Pens.Black, rectangle);
					Brush brush;
					if (this.int_1 == i)
					{
						brush = Brushes.White;
						e.Graphics.FillRectangle(Brushes.Blue, rectangle);
					}
					else
					{
						brush = Brushes.Black;
					}
					e.Graphics.DrawString((string)this.arrayList_0[i], this.Font, brush, rectangle, stringFormat);
					num2 += this.size_0.Width;
				}
				num++;
				if (num >= this.int_0)
				{
					num2 = base.AutoScrollPosition.X;
					num3 += this.size_0.Height;
					num = 0;
				}
				if (num3 > base.ClientRectangle.Height - 5)
				{
					break;
				}
			}
			System.Windows.Forms.ControlPaint.DrawBorder3D(e.Graphics, base.ClientRectangle, System.Windows.Forms.Border3DStyle.Adjust, (System.Windows.Forms.Border3DSide)0);
		}

		private void FontListView_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			int num = this.HitTest(e.X, e.Y);
			if (this.int_1 != num)
			{
				if (this.int_1 != -1)
				{
					this.method_0(this.int_1);
				}
				this.int_1 = num;
				if (this.int_1 != -1)
				{
					this.method_0(this.int_1);
				}
				if (this.selectedIndexChangedHandler_0 != null)
				{
					this.selectedIndexChangedHandler_0(sender, e);
				}
			}
		}

		public void MakeSelectItemVisible()
		{
			int num = this.int_1 / this.int_0;
			int num2 = System.Math.Abs(base.AutoScrollPosition.Y) / this.size_0.Height;
			num = num - num2 + 1;
			Point autoScrollPosition = base.AutoScrollPosition;
			if (base.ClientRectangle.Height - num * this.size_0.Height < 0)
			{
				autoScrollPosition.Y = autoScrollPosition.Y + base.ClientRectangle.Height - num * this.size_0.Height;
				base.AutoScrollPosition = autoScrollPosition;
			}
		}

		public void Add(object object_0)
		{
			this.arrayList_0.Add(object_0);
			this.SetScroll();
		}

		public void Clear()
		{
			this.arrayList_0.Clear();
			base.AutoScrollMinSize = base.ClientSize;
		}

		public void SetScroll()
		{
			if (this.arrayList_0.Count == 0)
			{
				base.AutoScrollMinSize = base.ClientSize;
			}
			else
			{
				int num = this.arrayList_0.Count / this.int_0;
				if (num * this.int_0 < this.arrayList_0.Count)
				{
					num++;
				}
				int width = 10 * this.size_0.Width;
				int height = num * this.size_0.Height - this.size_0.Height;
				base.AutoScrollMinSize = new Size(width, height);
			}
		}

		public int HitTest(int int_2, int int_3)
		{
			Point autoScrollPosition = base.AutoScrollPosition;
			int num = 0;
			int num2 = autoScrollPosition.X;
			int num3 = autoScrollPosition.Y;
			int i = 0;
			while (i < this.arrayList_0.Count)
			{
				num++;
				int result;
				if (int_3 < num3 || int_3 >= num3 + this.size_0.Height)
				{
					if (num >= this.int_0)
					{
						num3 += this.size_0.Height;
						num = 0;
					}
					if (num3 <= base.ClientRectangle.Height - 5)
					{
						i++;
						continue;
					}
					result = -1;
				}
				else
				{
					int num4 = i;
					while (num4 < i + this.int_0 && num4 < this.arrayList_0.Count)
					{
						if (int_2 >= num2 & int_2 < num2 + this.size_0.Width)
						{
							result = num4;
							return result;
						}
						num2 += this.size_0.Width;
						num4++;
					}
					result = -1;
				}
				return result;
			}
		    return -1;
		}

		private void method_0(int int_2)
		{
			int num = int_2 / this.int_0;
			int num2 = int_2 - this.int_0 * num;
			int num3 = num * this.size_0.Height + base.AutoScrollPosition.Y;
			if (num3 >= -this.size_0.Height && num3 < base.ClientRectangle.Height + 5)
			{
				int x = num2 * this.size_0.Width + base.AutoScrollPosition.X;
				Rectangle rc = new Rectangle(x, num3, this.size_0.Width, this.size_0.Height);
				base.Invalidate(rc);
			}
		}

		protected override bool ProcessCmdKey(ref System.Windows.Forms.Message message_0, System.Windows.Forms.Keys keys_0)
		{
			bool result;
			if (keys_0 == System.Windows.Forms.Keys.Down)
			{
				if (this.int_1 + this.int_0 < this.arrayList_0.Count)
				{
					this.method_0(this.int_1);
					this.int_1 += this.int_0;
					this.MakeSelectItemVisible();
					this.method_0(this.int_1);
					this.selectedIndexChangedHandler_0(this, new System.EventArgs());
				}
			}
			else if (keys_0 == System.Windows.Forms.Keys.Up)
			{
				if (this.int_1 - this.int_0 >= 0)
				{
					this.method_0(this.int_1);
					this.int_1 -= this.int_0;
					this.MakeSelectItemVisible();
					this.method_0(this.int_1);
					this.selectedIndexChangedHandler_0(this, new System.EventArgs());
				}
			}
			else if (keys_0 == System.Windows.Forms.Keys.Left)
			{
				if (this.int_1 > 0)
				{
					this.method_0(this.int_1);
					this.int_1--;
					this.MakeSelectItemVisible();
					this.method_0(this.int_1);
					this.selectedIndexChangedHandler_0(this, new System.EventArgs());
				}
			}
			else
			{
				if (keys_0 != System.Windows.Forms.Keys.Right)
				{
					result = base.ProcessCmdKey(ref message_0, keys_0);
					return result;
				}
				if (this.int_1 < this.arrayList_0.Count - 1)
				{
					this.method_0(this.int_1);
					this.int_1++;
					this.MakeSelectItemVisible();
					this.method_0(this.int_1);
					this.selectedIndexChangedHandler_0(this, new System.EventArgs());
				}
			}
			result = true;
			return result;
		}
	}
}
