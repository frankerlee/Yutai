using System;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Common.ControlExtend
{
    public class EditListView : ListView
    {
        private bool bool_0 = false;
        private Color color_0;
        private Color color_1;
        private ComboBox[] comboBox_0 = new ComboBox[20];
        private Font font_0;
        private Font font_1;
        private int int_0 = 0;
        private int int_1 = 0;
        private int int_2 = 0;
        private int int_3 = 0;
        private ListViewItem listViewItem_0;
        private ListViewItem listViewItem_1;
        private string string_0;
        private TextBox textBox_0 = new TextBox();

        public event RowDeleteHandler RowDelete;

        public event ValueChangedHandler ValueChanged;

        public EditListView()
        {
            this.ComboBoxFont = this.Font;
            this.EditFont = this.Font;
            this.EditBgColor = Color.LightBlue;
            this.color_0 = Color.LightBlue;
            base.MouseDown += new MouseEventHandler(this.EditListView_MouseDown);
            base.DoubleClick += new EventHandler(this.EditListView_DoubleClick);
            base.GridLines = true;
            this.textBox_0.Size = new Size(0, 0);
            this.textBox_0.Location = new Point(0, 0);
            base.Controls.AddRange(new Control[] { this.textBox_0 });
            this.textBox_0.KeyPress += new KeyPressEventHandler(this.textBox_0_KeyPress);
            this.textBox_0.LostFocus += new EventHandler(this.textBox_0_LostFocus);
            this.textBox_0.AutoSize = false;
            this.textBox_0.Font = this.EditFont;
            this.textBox_0.BackColor = this.EditBgColor;
            this.textBox_0.BorderStyle = BorderStyle.FixedSingle;
            this.textBox_0.Hide();
            this.textBox_0.Text = "";
        }

        public void BoundListToColumn(int int_4, string[] string_1)
        {
            if ((int_4 < 0) || (int_4 > base.Columns.Count))
            {
                throw new Exception("Column index is out of range");
            }
            if (((LVColumnHeader) base.Columns[int_4]).ColumnStyle != ListViewColumnStyle.ComboBox)
            {
                throw new Exception("Column should be ComboBox style");
            }
            ComboBox box = new ComboBox();
            for (int i = 0; i < string_1.Length; i++)
            {
                box.Items.Add(string_1[i]);
            }
            box.Size = new Size(0, 0);
            box.Location = new Point(0, 0);
            base.Controls.AddRange(new Control[] { box });
            box.SelectedIndexChanged += new EventHandler(this.method_1);
            box.LostFocus += new EventHandler(this.method_2);
            box.KeyPress += new KeyPressEventHandler(this.method_0);
            box.Font = this.ComboBoxFont;
            box.BackColor = this.ComboBoxBgColor;
            box.DropDownStyle = ComboBoxStyle.DropDownList;
            box.Hide();
            this.comboBox_0[int_4] = box;
        }

        private void EditListView_DoubleClick(object sender, EventArgs e)
        {
            if (this.listViewItem_0 != null)
            {
                Rectangle itemRect = base.GetItemRect(base.Items.IndexOf(this.listViewItem_0));
                int num = this.int_1;
                int left = itemRect.Left;
                int width = itemRect.Left;
                for (int i = 0; i < base.Columns.Count; i++)
                {
                    left = width;
                    width += base.Columns[i].Width;
                    if ((num > left) && (num < width))
                    {
                        this.int_3 = i;
                        break;
                    }
                }
                if ((this.int_0 <= 0) || (base.Items.IndexOf(this.listViewItem_0) >= this.int_0))
                {
                    Rectangle rectangle2;
                    this.string_0 = this.listViewItem_0.SubItems[this.int_3].Text;
                    LVColumnHeader header = (LVColumnHeader) base.Columns[this.int_3];
                    if (header.ColumnStyle == ListViewColumnStyle.ComboBox)
                    {
                        ComboBox box = this.comboBox_0[this.int_3];
                        if (box == null)
                        {
                            throw new Exception("The ComboxBox control bind to current column is null");
                        }
                        rectangle2 = new Rectangle(left, this.listViewItem_0.Bounds.Y, width, this.listViewItem_0.Bounds.Bottom);
                        box.Size = new Size(width - left, this.listViewItem_0.Bounds.Bottom - this.listViewItem_0.Bounds.Top);
                        box.Location = new Point(left, this.listViewItem_0.Bounds.Y);
                        box.Show();
                        box.Text = this.string_0;
                        box.SelectAll();
                        box.Focus();
                    }
                    if (header.ColumnStyle == ListViewColumnStyle.EditBox)
                    {
                        rectangle2 = new Rectangle(left, this.listViewItem_0.Bounds.Y, width, this.listViewItem_0.Bounds.Bottom);
                        this.textBox_0.Size = new Size(width - left, this.listViewItem_0.Bounds.Height);
                        this.textBox_0.Location = new Point(left, this.listViewItem_0.Bounds.Y);
                        this.textBox_0.Show();
                        this.textBox_0.Text = this.string_0;
                        this.textBox_0.SelectAll();
                        this.textBox_0.Focus();
                    }
                }
            }
        }

        private void EditListView_MouseDown(object sender, MouseEventArgs e)
        {
            this.listViewItem_1 = this.listViewItem_0;
            this.listViewItem_0 = base.GetItemAt(e.X, e.Y);
            this.int_1 = e.X;
            this.int_2 = e.Y;
        }

        private void method_0(object sender, KeyPressEventArgs e)
        {
            ComboBox box = (ComboBox) sender;
            if ((e.KeyChar == '\r') || (e.KeyChar == '\x001b'))
            {
                box.Hide();
            }
        }

        private void method_1(object sender, EventArgs e)
        {
            ComboBox box = (ComboBox) sender;
            int selectedIndex = box.SelectedIndex;
            if (selectedIndex >= 0)
            {
                string str = box.Items[selectedIndex].ToString();
                this.listViewItem_0.SubItems[this.int_3].Text = str;
                if (this.ValueChanged != null)
                {
                    this.ValueChanged(this, new ValueChangedEventArgs(this.textBox_0.Text, base.Items.IndexOf(this.listViewItem_0), this.int_3));
                }
            }
        }

        private void method_2(object sender, EventArgs e)
        {
            ((ComboBox) sender).Hide();
        }

        private void method_3()
        {
        }

        protected override bool ProcessCmdKey(ref Message message_0, Keys keys_0)
        {
            if (keys_0 == Keys.Delete)
            {
                if (this.RowDelete != null)
                {
                    this.RowDelete(this, new RowDeleteEventArgs(base.Items.IndexOf(this.listViewItem_0)));
                }
                return true;
            }
            return base.ProcessCmdKey(ref message_0, keys_0);
        }

        public void SetColumn(int int_4, ListViewColumnStyle listViewColumnStyle_0)
        {
            if ((int_4 < 0) || (int_4 > base.Columns.Count))
            {
                throw new Exception("Column index is out of range");
            }
            ((LVColumnHeader) base.Columns[int_4]).ColumnStyle = listViewColumnStyle_0;
        }

        private void textBox_0_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.bool_0 = false;
                this.textBox_0.Hide();
            }
            else if (e.KeyChar == '\x001b')
            {
                this.bool_0 = true;
                this.textBox_0.Hide();
            }
        }

        private void textBox_0_LostFocus(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_0 = false;
                this.textBox_0.Hide();
            }
            else
            {
                if (this.listViewItem_1 != null)
                {
                    this.listViewItem_1.SubItems[this.int_3].Text = this.textBox_0.Text;
                    if (this.ValueChanged != null)
                    {
                        this.ValueChanged(this, new ValueChangedEventArgs(this.textBox_0.Text, base.Items.IndexOf(this.listViewItem_1), this.int_3));
                    }
                }
                this.textBox_0.Hide();
            }
        }

        public Color ComboBoxBgColor
        {
            get
            {
                return this.color_0;
            }
            set
            {
                this.color_0 = value;
                for (int i = 0; i < this.comboBox_0.Length; i++)
                {
                    if (this.comboBox_0[i] != null)
                    {
                        this.comboBox_0[i].BackColor = this.color_0;
                    }
                }
            }
        }

        public Font ComboBoxFont
        {
            get
            {
                return this.font_0;
            }
            set
            {
                this.font_0 = value;
            }
        }

        public Color EditBgColor
        {
            get
            {
                return this.color_1;
            }
            set
            {
                this.color_1 = value;
                this.textBox_0.BackColor = this.color_1;
            }
        }

        public Font EditFont
        {
            get
            {
                return this.font_1;
            }
            set
            {
                this.font_1 = value;
                this.textBox_0.Font = this.font_1;
            }
        }

        public int LockRowCount
        {
            get
            {
                return this.int_0;
            }
            set
            {
                this.int_0 = value;
            }
        }
    }
}

