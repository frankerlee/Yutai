using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Yutai.Pipeline.Analysis.Classes;
using Yutai.Pipeline.Analysis.Forms;
using Yutai.Pipeline.Analysis.Properties;

namespace Yutai.Pipeline.Analysis.Forms
{
	public partial class MagnifierMainForm : XtraForm
	{
		private string string_0 = "configData.xml";


		private List<Class2> list_0 = new List<Class2>();



		private Configuration configuration_0 = new Configuration();

		private IContainer icontainer_0 = null;

		public MagnifierMainForm()
		{
			this.InitializeComponent();
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.TopMost = true;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.image_0 = Resources.magControlPanel;
			if (this.image_0 == null)
			{
				throw new Exception("Resource cannot be found!");
			}
			base.Width = this.image_0.Width;
			base.Height = this.image_0.Height;
			Class2 class2 = new Class2(new Rectangle(10, 15, 30, 30));
			class2.Event_1 += new Class2.Delegate2(this.method_7);
			class2.Event_0 += new Class2.Delegate2(this.method_6);
			class2.Event_2 += new Class2.Delegate2(this.method_5);
			Class2 class21 = new Class2(new Rectangle(50, 15, 35, 30));
			class21.Event_2 += new Class2.Delegate2(this.method_8);
			this.list_0.Add(class2);
			this.list_0.Add(class21);
			base.ShowInTaskbar = false;
		}

	private void method_0()
		{
			try
			{
				this.configuration_0 = (Configuration)XmlUtility.Deserialize(this.configuration_0.GetType(), this.string_0);
			}
			catch
			{
				this.configuration_0 = new Configuration();
			}
		}

		private void method_1()
		{
			try
			{
				XmlUtility.Serialize(this.configuration_0, this.string_0);
			}
			catch (Exception exception)
			{
				Console.WriteLine(string.Concat("Serialization problem: ", exception.Message));
			}
		}

		private void method_2(object obj)
		{
		}

		private void method_3(object obj)
		{
			(new ConfigurationForm(this.configuration_0)).ShowDialog(this);
		}

		private void method_4(object obj)
		{
		}

		private void method_5(object obj)
		{
		}

		private void method_6(object obj)
		{
			(new MagnifierForm(this.configuration_0, this.point_1)).Show();
		}

		private void method_7(object obj)
		{
		}

		private void method_8(object obj)
		{
			base.Close();
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			Class2 current;
			this.point_0 = new Point(e.X, e.Y);
			this.point_1 = System.Windows.Forms.Cursor.Position;
			List<Class2>.Enumerator enumerator = this.list_0.GetEnumerator();
			try
			{
				do
				{
					if (!enumerator.MoveNext())
					{
						break;
					}
					current = enumerator.Current;
				}
				while (!current.method_0(e));
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			foreach (Class2 list0 in this.list_0)
			{
				if (!list0.method_1(e))
				{
					continue;
				}
				this.Cursor = Cursors.Hand;
				return;
			}
			this.Cursor = Cursors.SizeAll;
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				int x = e.X - this.point_0.X;
				int y = e.Y - this.point_0.Y;
				MagnifierMainForm left = this;
				left.Left = left.Left + x;
				MagnifierMainForm top = this;
				top.Top = top.Top + y;
				this.configuration_0.LocationX = base.Left;
				this.configuration_0.LocationY = base.Top;
			}
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			Class2 current;
			List<Class2>.Enumerator enumerator = this.list_0.GetEnumerator();
			try
			{
				do
				{
					if (!enumerator.MoveNext())
					{
						break;
					}
					current = enumerator.Current;
				}
				while (!current.method_2(e));
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			if (this.image_0 != null)
			{
				graphics.DrawImage(this.image_0, 0, 0, base.Width, base.Height);
			}
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			if ((this.configuration_0.LocationX == -1 ? false : this.configuration_0.LocationY != -1))
			{
				base.Location = new System.Drawing.Point(this.configuration_0.LocationX, this.configuration_0.LocationY);
			}
		}
	}
}