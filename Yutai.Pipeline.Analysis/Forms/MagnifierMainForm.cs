using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Yutai.Pipeline.Analysis.Classes;
using Yutai.Pipeline.Analysis.Forms;
using Yutai.Pipeline.Analysis.Properties;

namespace Yutai.Pipeline.Analysis.Forms
{
    internal class MagnifierEventClass
    {
        public delegate void Delegate2(object sender);

        private Rectangle rectangle_0;

        private MagnifierEventClass.Delegate2 delegate2_0;

        private MagnifierEventClass.Delegate2 TyaPcGjAdg;

        private MagnifierEventClass.Delegate2 delegate2_1;

        public event MagnifierEventClass.Delegate2 Event_0
        {
            add
            {
                MagnifierEventClass.Delegate2 @delegate = this.delegate2_0;
                MagnifierEventClass.Delegate2 delegate2;
                do
                {
                    delegate2 = @delegate;
                    MagnifierEventClass.Delegate2 value2 =
                        (MagnifierEventClass.Delegate2) Delegate.Combine(delegate2, value);
                    @delegate = Interlocked.CompareExchange<MagnifierEventClass.Delegate2>(ref this.delegate2_0, value2,
                        delegate2);
                } while (@delegate != delegate2);
            }
            remove
            {
                MagnifierEventClass.Delegate2 @delegate = this.delegate2_0;
                MagnifierEventClass.Delegate2 delegate2;
                do
                {
                    delegate2 = @delegate;
                    MagnifierEventClass.Delegate2 value2 =
                        (MagnifierEventClass.Delegate2) Delegate.Remove(delegate2, value);
                    @delegate = Interlocked.CompareExchange<MagnifierEventClass.Delegate2>(ref this.delegate2_0, value2,
                        delegate2);
                } while (@delegate != delegate2);
            }
        }

        public event MagnifierEventClass.Delegate2 Event_1
        {
            add
            {
                MagnifierEventClass.Delegate2 @delegate = this.TyaPcGjAdg;
                MagnifierEventClass.Delegate2 delegate2;
                do
                {
                    delegate2 = @delegate;
                    MagnifierEventClass.Delegate2 value2 =
                        (MagnifierEventClass.Delegate2) Delegate.Combine(delegate2, value);
                    @delegate = Interlocked.CompareExchange<MagnifierEventClass.Delegate2>(ref this.TyaPcGjAdg, value2,
                        delegate2);
                } while (@delegate != delegate2);
            }
            remove
            {
                MagnifierEventClass.Delegate2 @delegate = this.TyaPcGjAdg;
                MagnifierEventClass.Delegate2 delegate2;
                do
                {
                    delegate2 = @delegate;
                    MagnifierEventClass.Delegate2 value2 =
                        (MagnifierEventClass.Delegate2) Delegate.Remove(delegate2, value);
                    @delegate = Interlocked.CompareExchange<MagnifierEventClass.Delegate2>(ref this.TyaPcGjAdg, value2,
                        delegate2);
                } while (@delegate != delegate2);
            }
        }

        public event MagnifierEventClass.Delegate2 Event_2
        {
            add
            {
                MagnifierEventClass.Delegate2 @delegate = this.delegate2_1;
                MagnifierEventClass.Delegate2 delegate2;
                do
                {
                    delegate2 = @delegate;
                    MagnifierEventClass.Delegate2 value2 =
                        (MagnifierEventClass.Delegate2) Delegate.Combine(delegate2, value);
                    @delegate = Interlocked.CompareExchange<MagnifierEventClass.Delegate2>(ref this.delegate2_1, value2,
                        delegate2);
                } while (@delegate != delegate2);
            }
            remove
            {
                MagnifierEventClass.Delegate2 @delegate = this.delegate2_1;
                MagnifierEventClass.Delegate2 delegate2;
                do
                {
                    delegate2 = @delegate;
                    MagnifierEventClass.Delegate2 value2 =
                        (MagnifierEventClass.Delegate2) Delegate.Remove(delegate2, value);
                    @delegate = Interlocked.CompareExchange<MagnifierEventClass.Delegate2>(ref this.delegate2_1, value2,
                        delegate2);
                } while (@delegate != delegate2);
            }
        }

        public MagnifierEventClass(Rectangle rectangle)
        {
            this.rectangle_0 = rectangle;
        }

        public bool method_0(MouseEventArgs mouseEventArgs)
        {
            bool result;
            if (!this.rectangle_0.Contains(mouseEventArgs.X, mouseEventArgs.Y))
            {
                result = false;
            }
            else
            {
                if (this.delegate2_0 != null)
                {
                    this.delegate2_0(this);
                }
                result = true;
            }
            return result;
        }

        public bool method_1(MouseEventArgs mouseEventArgs)
        {
            bool result;
            if (!this.rectangle_0.Contains(mouseEventArgs.X, mouseEventArgs.Y))
            {
                result = false;
            }
            else
            {
                if (this.TyaPcGjAdg != null)
                {
                    this.TyaPcGjAdg(this);
                }
                result = true;
            }
            return result;
        }

        public bool method_2(MouseEventArgs mouseEventArgs)
        {
            bool result;
            if (!this.rectangle_0.Contains(mouseEventArgs.X, mouseEventArgs.Y))
            {
                result = false;
            }
            else
            {
                if (this.delegate2_1 != null)
                {
                    this.delegate2_1(this);
                }
                result = true;
            }
            return result;
        }
    }

    public partial class MagnifierMainForm : XtraForm
    {
        private string string_0 = "configData.xml";


        private List<MagnifierEventClass> list_0 = new List<MagnifierEventClass>();


        private Configuration configuration_0 = new Configuration();

        private IContainer icontainer_0 = null;

        public MagnifierMainForm()
        {
            this.InitializeComponent();
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            base.TopMost = true;
            base.StartPosition = FormStartPosition.CenterScreen;
            //this.image_0 = Resources.magControlPanel;
            if (this.image_0 == null)
            {
                throw new Exception("Resource cannot be found!");
            }
            base.Width = this.image_0.Width;
            base.Height = this.image_0.Height;
            MagnifierEventClass class2 = new MagnifierEventClass(new Rectangle(10, 15, 30, 30));
            class2.Event_1 += new MagnifierEventClass.Delegate2(this.method_7);
            class2.Event_0 += new MagnifierEventClass.Delegate2(this.method_6);
            class2.Event_2 += new MagnifierEventClass.Delegate2(this.method_5);
            MagnifierEventClass class21 = new MagnifierEventClass(new Rectangle(50, 15, 35, 30));
            class21.Event_2 += new MagnifierEventClass.Delegate2(this.method_8);
            this.list_0.Add(class2);
            this.list_0.Add(class21);
            base.ShowInTaskbar = false;
        }

        private void method_0()
        {
            try
            {
                this.configuration_0 =
                    (Configuration) XmlUtility.Deserialize(this.configuration_0.GetType(), this.string_0);
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
            MagnifierEventClass current;
            this.point_0 = new Point(e.X, e.Y);
            this.point_1 = System.Windows.Forms.Cursor.Position;
            List<MagnifierEventClass>.Enumerator enumerator = this.list_0.GetEnumerator();
            try
            {
                do
                {
                    if (!enumerator.MoveNext())
                    {
                        break;
                    }
                    current = enumerator.Current;
                } while (!current.method_0(e));
            }
            finally
            {
                ((IDisposable) enumerator).Dispose();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            foreach (MagnifierEventClass list0 in this.list_0)
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
            MagnifierEventClass current;
            List<MagnifierEventClass>.Enumerator enumerator = this.list_0.GetEnumerator();
            try
            {
                do
                {
                    if (!enumerator.MoveNext())
                    {
                        break;
                    }
                    current = enumerator.Current;
                } while (!current.method_2(e));
            }
            finally
            {
                ((IDisposable) enumerator).Dispose();
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