using System;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Common.SymbolUI
{
    public class KDEditListView : ListView
    {
        // Fields
        private bool bool_0 = false;
        private Color color_0;
        private Color color_1;
        private ComboBox[] comboBox_0 = new ComboBox[20];
        private Font font_0;
        private Font font_1;
        private int int_0 = 0;
        private int int_1 = 0;
        private int int_2 = 0;
        private ListViewItem listViewItem_0;
        private ListViewItem listViewItem_1;
        private string string_0;
        private TextBox textBox_0 = new TextBox();

        // Events
        public event ValueChangedHandler ValueChanged;

        // Methods
        public KDEditListView()
        {
            this.ComboBoxFont = this.Font;
            this.EditFont = this.Font;
            this.EditBgColor = Color.LightBlue;
            this.color_0 = Color.LightBlue;
            base.MouseDown += new MouseEventHandler(this.SMKMouseDown);
            base.DoubleClick += new EventHandler(this.SMKDoubleClick);
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

        public void BoundListToColumn(int int_3, string[] string_1)
        {
            if ((int_3 < 0) || (int_3 > base.Columns.Count))
            {
                throw new Exception("Column index is out of range");
            }
            if (((KDColumnHeader)base.Columns[int_3]).ColumnStyle != KDListViewColumnStyle.ComboBox)
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
            this.comboBox_0[int_3] = box;
        }

        private void method_0(object sender, KeyPressEventArgs e)
        {
            ComboBox box = (ComboBox)sender;
            if ((e.KeyChar == '\r') || (e.KeyChar == '\x001b'))
            {
                box.Hide();
            }
        }

        private void method_1(object sender, EventArgs e)
        {
            ComboBox box = (ComboBox)sender;
            int selectedIndex = box.SelectedIndex;
            if (selectedIndex >= 0)
            {
                string str = box.Items[selectedIndex].ToString();
                this.listViewItem_0.SubItems[this.int_2].Text = str;
                if (this.ValueChanged != null)
                {
                    this.ValueChanged(sender, e);
                }
            }
        }

        private void method_2(object sender, EventArgs e)
        {
            ((ComboBox)sender).Hide();
        }

        public void SetColumn(int int_3, KDListViewColumnStyle kdlistViewColumnStyle_0)
        {
            if ((int_3 < 0) || (int_3 > base.Columns.Count))
            {
                throw new Exception("Column index is out of range");
            }
            ((KDColumnHeader)base.Columns[int_3]).ColumnStyle = kdlistViewColumnStyle_0;
        }

        public void SMKDoubleClick(object sender, EventArgs e)
        {
            Rectangle rectangle;
            int num = this.int_0;
            int x = 0;
            int width = 0;
            for (int i = 0; i < base.Columns.Count; i++)
            {
                x = width;
                width += base.Columns[i].Width;
                if ((num > x) && (num < width))
                {
                    this.int_2 = i;
                    break;
                }
            }
            this.string_0 = this.listViewItem_0.SubItems[this.int_2].Text;
            KDColumnHeader header = (KDColumnHeader)base.Columns[this.int_2];
            if (header.ColumnStyle == KDListViewColumnStyle.ComboBox)
            {
                ComboBox box = this.comboBox_0[this.int_2];
                if (box == null)
                {
                    throw new Exception("The ComboxBox control bind to current column is null");
                }
                rectangle = new Rectangle(x, this.listViewItem_0.Bounds.Y, width, this.listViewItem_0.Bounds.Bottom);
                box.Size = new Size(width - x, this.listViewItem_0.Bounds.Bottom - this.listViewItem_0.Bounds.Top);
                box.Location = new Point(x, this.listViewItem_0.Bounds.Y);
                box.Show();
                box.Text = this.string_0;
                box.SelectAll();
                box.Focus();
            }
            if (header.ColumnStyle == KDListViewColumnStyle.EditBox)
            {
                rectangle = new Rectangle(x, this.listViewItem_0.Bounds.Y, width, this.listViewItem_0.Bounds.Bottom);
                this.textBox_0.Size = new Size(width - x, this.listViewItem_0.Bounds.Height);
                this.textBox_0.Location = new Point(x, this.listViewItem_0.Bounds.Y);
                this.textBox_0.Show();
                this.textBox_0.Text = this.string_0;
                this.textBox_0.SelectAll();
                this.textBox_0.Focus();
            }
        }

        public void SMKMouseDown(object sender, MouseEventArgs e)
        {
            this.listViewItem_1 = this.listViewItem_0;
            this.listViewItem_0 = base.GetItemAt(e.X, e.Y);
            this.int_0 = e.X;
            this.int_1 = e.Y;
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
            }
            else
            {
                if ((this.listViewItem_1 != null) && (this.listViewItem_1.SubItems[this.int_2].Text != this.textBox_0.Text))
                {
                    this.listViewItem_1.SubItems[this.int_2].Text = this.textBox_0.Text;
                    if (this.ValueChanged != null)
                    {
                        this.ValueChanged(this, e);
                    }
                }
                this.textBox_0.Hide();
            }
        }

        // Properties
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
    }
}