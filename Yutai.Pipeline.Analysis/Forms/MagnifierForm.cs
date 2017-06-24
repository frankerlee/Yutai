
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Yutai.Pipeline.Analysis.Classes;
using Yutai.Pipeline.Analysis.Properties;

namespace Yutai.Pipeline.Analysis.Forms
{
	public partial class MagnifierForm : XtraForm
	{
		private IContainer icontainer_0 = null;


        
		private bool bool_0 = true;

		private static Point point_2;

		static MagnifierForm()
		{
			MagnifierForm.point_2 = System.Windows.Forms.Cursor.Position;
		}

		public MagnifierForm(Yutai.Pipeline.Analysis.Classes.Configuration configuration, Point startPoint)
		{
			this.InitializeComponent();
			this.configuration_0 = configuration;
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.ShowInTaskbar = this.configuration_0.ShowInTaskbar;
			base.TopMost = this.configuration_0.TopMostWindow;
			base.Width = this.configuration_0.MagnifierWidth;
			base.Height = this.configuration_0.MagnifierHeight;
			GraphicsPath graphicsPath = new GraphicsPath();
			graphicsPath.AddEllipse(base.ClientRectangle);
			base.Region = new System.Drawing.Region(graphicsPath);
			//this.image_0 = Resources.magnifierGlass;
			this.timer_0 = new Timer()
			{
				Enabled = true,
				Interval = 20
			};
			this.timer_0.Tick += new EventHandler(this.timer_0_Tick);
			Rectangle bounds = Screen.PrimaryScreen.Bounds;
			int width = bounds.Width;
			bounds = Screen.PrimaryScreen.Bounds;
			this.image_2 = new Bitmap(width, bounds.Height);
			this.point_0 = startPoint;
			this.pointF_0 = startPoint;
			if (!this.configuration_0.ShowInTaskbar)
			{
				base.ShowInTaskbar = false;
			}
			else
			{
				base.ShowInTaskbar = true;
			}
		}

	private void method_0()
		{
			if (!base.InvokeRequired)
			{
				Graphics graphic = Graphics.FromImage(this.image_2);
				graphic.CopyFromScreen(0, 0, 0, 0, new System.Drawing.Size(this.image_2.Width, this.image_2.Height));
				graphic.Dispose();
				if (!this.configuration_0.HideMouseCursor)
				{
					this.Cursor = Cursors.Cross;
				}
				else
				{
					System.Windows.Forms.Cursor.Hide();
				}
				base.Capture = true;
				if (!this.configuration_0.RememberLastPoint)
				{
					this.pointF_1 = System.Windows.Forms.Cursor.Position;
				}
				else
				{
					this.pointF_1 = MagnifierForm.point_2;
					System.Windows.Forms.Cursor.Position = MagnifierForm.point_2;
					base.Left = (int)this.pointF_1.X - base.Width / 2;
					base.Top = (int)this.pointF_1.Y - base.Height / 2;
				}
				base.Show();
			}
			else
			{
				base.Invoke(new MagnifierForm.Delegate0(this.method_0));
			}
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			this.point_1 = new Point(base.Width / 2 - e.X, base.Height / 2 - e.Y);
			this.pointF_1 = base.PointToScreen(new Point(e.X + this.point_1.X, e.Y + this.point_1.Y));
			this.pointF_0 = this.pointF_1;
			this.timer_0.Enabled = true;
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				this.pointF_0 = base.PointToScreen(new Point(e.X + this.point_1.X, e.Y + this.point_1.Y));
				this.timer_0.Enabled = true;
			}
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (this.configuration_0.CloseOnMouseUp)
			{
				base.Close();
				this.image_2.Dispose();
			}
			System.Windows.Forms.Cursor.Show();
			System.Windows.Forms.Cursor.Position = this.point_0;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics graphic;
			if (this.image_1 == null)
			{
				this.image_1 = new Bitmap(base.Width, base.Height);
			}
			Graphics graphic1 = Graphics.FromImage(this.image_1);
			graphic = (!this.configuration_0.DoubleBuffered ? e.Graphics : graphic1);
			if (this.image_2 != null)
			{
				Rectangle rectangle = new Rectangle(0, 0, base.Width, base.Height);
				int width = (int)((float)base.Width / this.configuration_0.ZoomFactor);
				int height = (int)((float)base.Height / this.configuration_0.ZoomFactor);
				int left = base.Left - width / 2 + base.Width / 2;
				int top = base.Top - height / 2 + base.Height / 2;
				graphic.DrawImage(this.image_2, rectangle, left, top, width, height, GraphicsUnit.Pixel);
			}
			if (this.image_0 != null)
			{
				graphic.DrawImage(this.image_0, 0, 0, base.Width, base.Height);
			}
			if (this.configuration_0.DoubleBuffered)
			{
				e.Graphics.DrawImage(this.image_1, 0, 0, base.Width, base.Height);
			}
		}

		protected override void OnPaintBackground(PaintEventArgs e)
		{
			if (!this.configuration_0.DoubleBuffered)
			{
				base.OnPaintBackground(e);
			}
		}

		protected override void OnShown(EventArgs e)
		{
			this.method_0();
		}

		private void timer_0_Tick(object obj, EventArgs eventArg)
		{
			float speedFactor = this.configuration_0.SpeedFactor * (this.pointF_0.X - this.pointF_1.X);
			float single = this.configuration_0.SpeedFactor * (this.pointF_0.Y - this.pointF_1.Y);
			if (!this.bool_0)
			{
				this.pointF_1.X = this.pointF_1.X + speedFactor;
				this.pointF_1.Y = this.pointF_1.Y + single;
				if ((Math.Abs(speedFactor) >= 1f ? true : Math.Abs(single) >= 1f))
				{
					base.Left = (int)this.pointF_1.X - base.Width / 2;
					base.Top = (int)this.pointF_1.Y - base.Height / 2;
					MagnifierForm.point_2 = new Point((int)this.pointF_1.X, (int)this.pointF_1.Y);
				}
				else
				{
					this.timer_0.Enabled = false;
				}
				this.Refresh();
			}
			else
			{
				this.bool_0 = false;
				this.pointF_1.X = this.pointF_0.X;
				this.pointF_1.Y = this.pointF_0.Y;
				base.Left = (int)this.pointF_1.X - base.Width / 2;
				base.Top = (int)this.pointF_1.Y - base.Height / 2;
			}
		}

		private delegate void Delegate0();
	}
}