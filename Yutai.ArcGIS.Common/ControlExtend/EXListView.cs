using System;
using System.Collections;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Common.ControlExtend
{
    public class EXListView : ListView
    {
        private Brush brush_0 = SystemBrushes.ControlLight;
        private Brush brush_1 = SystemBrushes.Highlight;
        private EXListViewItem exlistViewItem_0 = null;
        private int int_0;
        private int int_1 = -1;
        private int int_2 = -1;
        private ListViewItem listViewItem_0;
        private ListViewItem.ListViewSubItem listViewSubItem_0;
        private const uint LVM_FIRST = 0x1000;
        private const uint LVM_SCROLL = 0x1014;
        private TextBox textBox_0;
        private const int WM_HSCROLL = 0x114;
        private const int WM_MOUSEWHEEL = 0x20a;
        private const int WM_VSCROLL = 0x115;

        public event ValueChangedHandler ValueChanged;

        public EXListView()
        {
            base.OwnerDraw = true;
            base.FullRowSelect = true;
            base.View = View.Details;
            base.MouseDown += new MouseEventHandler(this.EXListView_MouseDown);
            base.MouseDoubleClick += new MouseEventHandler(this.EXListView_MouseDoubleClick);
            base.DrawColumnHeader += new DrawListViewColumnHeaderEventHandler(this.EXListView_DrawColumnHeader);
            base.DrawSubItem += new DrawListViewSubItemEventHandler(this.EXListView_DrawSubItem);
            base.MouseMove += new MouseEventHandler(this.EXListView_MouseMove);
            base.ColumnClick += new ColumnClickEventHandler(this.EXListView_ColumnClick);
            this.textBox_0 = new TextBox();
            this.textBox_0.Visible = false;
            base.Controls.Add(this.textBox_0);
            this.textBox_0.Leave += new EventHandler(this.textBox_0_Leave);
            this.textBox_0.KeyPress += new KeyPressEventHandler(this.textBox_0_KeyPress);
        }

        private void EXListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (base.Items.Count != 0)
            {
                int num;
                for (num = 0; num < base.Columns.Count; num++)
                {
                    base.Columns[num].ImageKey = null;
                }
                for (num = 0; num < base.Items.Count; num++)
                {
                    base.Items[num].Tag = null;
                }
                if (e.Column != this.int_1)
                {
                    this.int_1 = e.Column;
                    base.Sorting = SortOrder.Ascending;
                    base.Columns[e.Column].ImageKey = "up";
                }
                else if (base.Sorting == SortOrder.Ascending)
                {
                    base.Sorting = SortOrder.Descending;
                    base.Columns[e.Column].ImageKey = "down";
                }
                else
                {
                    base.Sorting = SortOrder.Ascending;
                    base.Columns[e.Column].ImageKey = "up";
                }
                if (this.int_1 == 0)
                {
                    if (base.Items[0].GetType() == typeof(EXListViewItem))
                    {
                        base.ListViewItemSorter = new Class2(e.Column, base.Sorting);
                    }
                    else
                    {
                        base.ListViewItemSorter = new Class3(e.Column, base.Sorting);
                    }
                }
                else if (base.Items[0].SubItems[this.int_1].GetType() == typeof(EXListViewSubItemAB))
                {
                    base.ListViewItemSorter = new Class0(e.Column, base.Sorting);
                }
                else
                {
                    base.ListViewItemSorter = new Class1(e.Column, base.Sorting);
                }
            }
        }

        private void EXListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void EXListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawBackground();
            if (e.ColumnIndex == this.int_1)
            {
                e.Graphics.FillRectangle(this.brush_0, e.Bounds);
            }
            if ((e.ItemState & ListViewItemStates.Selected) != 0)
            {
                e.Graphics.FillRectangle(this.brush_1, e.Bounds);
            }
            int num = (e.Bounds.Y + (e.Bounds.Height / 2)) - (e.SubItem.Font.Height / 2);
            int x = e.Bounds.X + 2;
            if (e.ColumnIndex == 0)
            {
                EXListViewItem item = (EXListViewItem) e.Item;
                if (item.GetType() == typeof(EXImageListViewItem))
                {
                    EXImageListViewItem item2 = (EXImageListViewItem) item;
                    if (item2.MyImage != null)
                    {
                        Image myImage = item2.MyImage;
                        int y = (e.Bounds.Y + (e.Bounds.Height / 2)) - (myImage.Height / 2);
                        e.Graphics.DrawImage(myImage, x, y, myImage.Width, myImage.Height);
                        x += myImage.Width + 2;
                    }
                }
                e.Graphics.DrawString(e.SubItem.Text, e.SubItem.Font, new SolidBrush(e.SubItem.ForeColor), (float) x, (float) num);
            }
            else
            {
                EXListViewSubItemAB subItem = e.SubItem as EXListViewSubItemAB;
                if (subItem == null)
                {
                    e.DrawDefault = true;
                }
                else
                {
                    x = subItem.DoDraw(e, x, base.Columns[e.ColumnIndex] as EXColumnHeader);
                    e.Graphics.DrawString(e.SubItem.Text, e.SubItem.Font, new SolidBrush(e.SubItem.ForeColor), (float) x, (float) num);
                }
            }
        }

        private void EXListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            EXListViewItem itemAt = base.GetItemAt(e.X, e.Y) as EXListViewItem;
            if (itemAt != null)
            {
                this.listViewItem_0 = itemAt;
                int left = itemAt.Bounds.Left;
                int num2 = 0;
                while (num2 < base.Columns.Count)
                {
                    left += base.Columns[num2].Width;
                    if (left > e.X)
                    {
                        left -= base.Columns[num2].Width;
                        this.listViewSubItem_0 = itemAt.SubItems[num2];
                        this.int_0 = num2;
                        break;
                    }
                    num2++;
                }
                if (base.Columns[num2] is EXColumnHeader)
                {
                    EXColumnHeader header = (EXColumnHeader) base.Columns[num2];
                    if (header.GetType() == typeof(EXEditableColumnHeader))
                    {
                        EXEditableColumnHeader header2 = (EXEditableColumnHeader) header;
                        if (header2.MyControl != null)
                        {
                            Control myControl = header2.MyControl;
                            if (myControl.Tag != null)
                            {
                                base.Controls.Add(myControl);
                                myControl.Tag = null;
                                if (myControl is ComboBox)
                                {
                                    ((ComboBox) myControl).SelectedValueChanged += new EventHandler(this.method_1);
                                }
                                myControl.Leave += new EventHandler(this.textBox_0_Leave);
                            }
                            myControl.Location = new Point(left, base.GetItemRect(base.Items.IndexOf(itemAt)).Y);
                            myControl.Width = base.Columns[num2].Width;
                            if (myControl.Width > base.Width)
                            {
                                myControl.Width = base.ClientRectangle.Width;
                            }
                            myControl.Text = this.listViewSubItem_0.Text;
                            myControl.Visible = true;
                            myControl.BringToFront();
                            myControl.Focus();
                        }
                        else
                        {
                            this.textBox_0.Location = new Point(left, base.GetItemRect(base.Items.IndexOf(itemAt)).Y);
                            this.textBox_0.Width = base.Columns[num2].Width;
                            if (this.textBox_0.Width > base.Width)
                            {
                                this.textBox_0.Width = base.ClientRectangle.Width;
                            }
                            this.textBox_0.Text = this.listViewSubItem_0.Text;
                            this.textBox_0.Visible = true;
                            this.textBox_0.BringToFront();
                            this.textBox_0.Focus();
                        }
                    }
                    else if (header.GetType() == typeof(EXBoolColumnHeader))
                    {
                        EXBoolColumnHeader header3 = (EXBoolColumnHeader) header;
                        if (header3.Editable)
                        {
                            EXBoolListViewSubItem item2 = (EXBoolListViewSubItem) this.listViewSubItem_0;
                            if (item2.BoolValue)
                            {
                                item2.BoolValue = false;
                            }
                            else
                            {
                                item2.BoolValue = true;
                            }
                            if (this.ValueChanged != null)
                            {
                                this.ValueChanged(this, new ExListViewEventArgs(itemAt, num2, !item2.BoolValue));
                            }
                            base.Invalidate(item2.Bounds);
                        }
                    }
                }
            }
        }

        private void EXListView_MouseDown(object sender, MouseEventArgs e)
        {
            ListViewItem.ListViewSubItem subItem = base.HitTest(e.X, e.Y).SubItem;
            if (subItem != null)
            {
                int left = subItem.Bounds.Left;
                if (left < 0)
                {
                    this.method_0(left, 0);
                }
            }
        }

        private void EXListView_MouseMove(object sender, MouseEventArgs e)
        {
            ListViewItem itemAt = base.GetItemAt(e.X, e.Y);
            if ((itemAt != null) && (itemAt.Tag == null))
            {
                base.Invalidate(itemAt.Bounds);
                itemAt.Tag = "t";
            }
        }

        private void method_0(int int_3, int int_4)
        {
            SendMessage(base.Handle, 0x1014, int_3, int_4);
        }

        private void method_1(object sender, EventArgs e)
        {
            if (((Control) sender).Visible && (this.listViewSubItem_0 != null))
            {
                if (sender.GetType() == typeof(EXComboBox))
                {
                    EXMultipleImagesListViewItem item2;
                    EXImageListViewSubItem item3;
                    EXMultipleImagesListViewSubItem item4;
                    EXComboBox box = (EXComboBox) sender;
                    object selectedItem = box.SelectedItem;
                    if (selectedItem.GetType() == typeof(EXComboBox.EXImageItem))
                    {
                        EXComboBox.EXImageItem item = (EXComboBox.EXImageItem) selectedItem;
                        if (this.int_0 == 0)
                        {
                            if (this.listViewItem_0.GetType() == typeof(EXImageListViewItem))
                            {
                                ((EXImageListViewItem) this.listViewItem_0).MyImage = item.MyImage;
                            }
                            else if (this.listViewItem_0.GetType() == typeof(EXMultipleImagesListViewItem))
                            {
                                item2 = (EXMultipleImagesListViewItem) this.listViewItem_0;
                                item2.MyImages.Clear();
                                item2.MyImages.AddRange(new object[] { item.MyImage });
                            }
                        }
                        else if (this.listViewSubItem_0.GetType() == typeof(EXImageListViewSubItem))
                        {
                            item3 = (EXImageListViewSubItem) this.listViewSubItem_0;
                            item3.MyImage = item.MyImage;
                        }
                        else if (this.listViewSubItem_0.GetType() == typeof(EXMultipleImagesListViewSubItem))
                        {
                            item4 = (EXMultipleImagesListViewSubItem) this.listViewSubItem_0;
                            item4.MyImages.Clear();
                            item4.MyImages.Add(item.MyImage);
                            item4.MyValue = item.MyValue;
                        }
                    }
                    else if (selectedItem.GetType() == typeof(EXComboBox.EXMultipleImagesItem))
                    {
                        EXComboBox.EXMultipleImagesItem item5 = (EXComboBox.EXMultipleImagesItem) selectedItem;
                        if (this.int_0 == 0)
                        {
                            if (this.listViewItem_0.GetType() == typeof(EXImageListViewItem))
                            {
                                ((EXImageListViewItem) this.listViewItem_0).MyImage = (Image) item5.MyImages[0];
                            }
                            else if (this.listViewItem_0.GetType() == typeof(EXMultipleImagesListViewItem))
                            {
                                item2 = (EXMultipleImagesListViewItem) this.listViewItem_0;
                                item2.MyImages.Clear();
                                item2.MyImages.AddRange(item5.MyImages);
                            }
                        }
                        else if (this.listViewSubItem_0.GetType() == typeof(EXImageListViewSubItem))
                        {
                            item3 = (EXImageListViewSubItem) this.listViewSubItem_0;
                            if (item5.MyImages != null)
                            {
                                item3.MyImage = (Image) item5.MyImages[0];
                            }
                        }
                        else if (this.listViewSubItem_0.GetType() == typeof(EXMultipleImagesListViewSubItem))
                        {
                            item4 = (EXMultipleImagesListViewSubItem) this.listViewSubItem_0;
                            item4.MyImages.Clear();
                            item4.MyImages.AddRange(item5.MyImages);
                            item4.MyValue = item5.MyValue;
                        }
                    }
                }
                ComboBox box2 = (ComboBox) sender;
                this.listViewSubItem_0.Text = box2.Text;
                box2.Visible = false;
                this.listViewItem_0.Tag = null;
            }
        }

        [DllImport("user32.dll")]
        private static extern bool SendMessage(IntPtr intptr_0, uint uint_0, int int_3, int int_4);
        private void textBox_0_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                string text = this.listViewSubItem_0.Text;
                this.listViewSubItem_0.Text = this.textBox_0.Text;
                this.textBox_0.Visible = false;
                this.listViewItem_0.Tag = null;
                if (this.ValueChanged != null)
                {
                    this.ValueChanged(this, new ExListViewEventArgs(this.exlistViewItem_0, this.int_2, text));
                }
                this.exlistViewItem_0 = null;
                this.int_2 = -1;
            }
        }

        private void textBox_0_Leave(object sender, EventArgs e)
        {
            Control control = (Control) sender;
            string text = this.listViewSubItem_0.Text;
            this.listViewSubItem_0.Text = control.Text;
            control.Visible = false;
            this.listViewItem_0.Tag = null;
            if (this.ValueChanged != null)
            {
                this.ValueChanged(this, new ExListViewEventArgs(this.exlistViewItem_0, this.int_2, text));
            }
            this.exlistViewItem_0 = null;
            this.int_2 = -1;
        }

        protected override void WndProc(ref Message message_0)
        {
            if (((message_0.Msg == 0x114) || (message_0.Msg == 0x115)) || (message_0.Msg == 0x20a))
            {
                base.Focus();
            }
            base.WndProc(ref message_0);
        }

        public Brush MyHighlightBrush
        {
            get
            {
                return this.brush_1;
            }
            set
            {
                this.brush_1 = value;
            }
        }

        public Brush MySortBrush
        {
            get
            {
                return this.brush_0;
            }
            set
            {
                this.brush_0 = value;
            }
        }

        private class Class0 : IComparer
        {
            private int int_0;
            private SortOrder sortOrder_0;

            public Class0()
            {
                this.int_0 = 0;
                this.sortOrder_0 = SortOrder.Ascending;
            }

            public Class0(int int_1, SortOrder sortOrder_1)
            {
                this.int_0 = int_1;
                this.sortOrder_0 = sortOrder_1;
            }

            public int Compare(object object_0, object object_1)
            {
                decimal num2;
                decimal num3;
                int num = -1;
                string text = ((ListViewItem) object_0).SubItems[this.int_0].Text;
                string s = ((ListViewItem) object_1).SubItems[this.int_0].Text;
                if (decimal.TryParse(text, out num2) && decimal.TryParse(s, out num3))
                {
                    num = decimal.Compare(num2, num3);
                }
                else
                {
                    DateTime time;
                    DateTime time2;
                    if (DateTime.TryParse(text, out time) && DateTime.TryParse(s, out time2))
                    {
                        num = DateTime.Compare(time, time2);
                    }
                    else
                    {
                        num = string.Compare(text, s);
                    }
                }
                if (this.sortOrder_0 == SortOrder.Descending)
                {
                    num *= -1;
                }
                return num;
            }
        }

        private class Class1 : IComparer
        {
            private int int_0;
            private SortOrder sortOrder_0;

            public Class1()
            {
                this.int_0 = 0;
                this.sortOrder_0 = SortOrder.Ascending;
            }

            public Class1(int int_1, SortOrder sortOrder_1)
            {
                this.int_0 = int_1;
                this.sortOrder_0 = sortOrder_1;
            }

            public int Compare(object object_0, object object_1)
            {
                decimal num2;
                decimal num3;
                int num = -1;
                string myValue = ((EXListViewSubItemAB) ((ListViewItem) object_0).SubItems[this.int_0]).MyValue;
                string s = ((EXListViewSubItemAB) ((ListViewItem) object_1).SubItems[this.int_0]).MyValue;
                if (decimal.TryParse(myValue, out num2) && decimal.TryParse(s, out num3))
                {
                    num = decimal.Compare(num2, num3);
                }
                else
                {
                    DateTime time;
                    DateTime time2;
                    if (DateTime.TryParse(myValue, out time) && DateTime.TryParse(s, out time2))
                    {
                        num = DateTime.Compare(time, time2);
                    }
                    else
                    {
                        num = string.Compare(myValue, s);
                    }
                }
                if (this.sortOrder_0 == SortOrder.Descending)
                {
                    num *= -1;
                }
                return num;
            }
        }

        private class Class2 : IComparer
        {
            private int int_0;
            private SortOrder sortOrder_0;

            public Class2()
            {
                this.int_0 = 0;
                this.sortOrder_0 = SortOrder.Ascending;
            }

            public Class2(int int_1, SortOrder sortOrder_1)
            {
                this.int_0 = int_1;
                this.sortOrder_0 = sortOrder_1;
            }

            public int Compare(object object_0, object object_1)
            {
                decimal num2;
                decimal num3;
                int num = -1;
                string text = ((ListViewItem) object_0).Text;
                string s = ((ListViewItem) object_1).Text;
                if (decimal.TryParse(text, out num2) && decimal.TryParse(s, out num3))
                {
                    num = decimal.Compare(num2, num3);
                }
                else
                {
                    DateTime time;
                    DateTime time2;
                    if (DateTime.TryParse(text, out time) && DateTime.TryParse(s, out time2))
                    {
                        num = DateTime.Compare(time, time2);
                    }
                    else
                    {
                        num = string.Compare(text, s);
                    }
                }
                if (this.sortOrder_0 == SortOrder.Descending)
                {
                    num *= -1;
                }
                return num;
            }
        }

        private class Class3 : IComparer
        {
            private int int_0;
            private SortOrder sortOrder_0;

            public Class3()
            {
                this.int_0 = 0;
                this.sortOrder_0 = SortOrder.Ascending;
            }

            public Class3(int int_1, SortOrder sortOrder_1)
            {
                this.int_0 = int_1;
                this.sortOrder_0 = sortOrder_1;
            }

            public int Compare(object object_0, object object_1)
            {
                decimal num2;
                decimal num3;
                int num = -1;
                string myValue = ((EXListViewItem) object_0).MyValue;
                string s = ((EXListViewItem) object_1).MyValue;
                if (decimal.TryParse(myValue, out num2) && decimal.TryParse(s, out num3))
                {
                    num = decimal.Compare(num2, num3);
                }
                else
                {
                    DateTime time;
                    DateTime time2;
                    if (DateTime.TryParse(myValue, out time) && DateTime.TryParse(s, out time2))
                    {
                        num = DateTime.Compare(time, time2);
                    }
                    else
                    {
                        num = string.Compare(myValue, s);
                    }
                }
                if (this.sortOrder_0 == SortOrder.Descending)
                {
                    num *= -1;
                }
                return num;
            }
        }

        public delegate void ValueChangedHandler(object sender, ExListViewEventArgs e);
    }
}

