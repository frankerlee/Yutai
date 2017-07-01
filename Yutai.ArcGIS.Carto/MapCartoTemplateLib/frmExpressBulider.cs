using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public partial class frmExpressBulider : Form
    {
        private bool bool_0 = false;
        private Container container_0 = null;
        private IList ilist_0 = null;

        private string string_0 = "";
        private string string_1 = "";

        public frmExpressBulider()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            this.string_0 = this.memEditWhereCaluse.Text;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string str2;
            string text = (sender as Button).Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                str2 = this.memEditWhereCaluse.Text.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            else
            {
                str2 = this.memEditWhereCaluse.Text;
            }
            this.memEditWhereCaluse.Text = str2.Insert(selectionStart, string.Format(" {0} ", text));
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = (selectionStart + text.Length) + 2;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void btnAnd_Click(object sender, EventArgs e)
        {
            string text = (sender as Button).Text;
            string str2 = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                str2 = str2.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = str2.Insert(selectionStart, " " + text + " ");
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = (selectionStart + text.Length) + 2;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void btnBracket_Click(object sender, EventArgs e)
        {
            string text = this.btnBracket.Text;
            string str2 = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                str2 = str2.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = str2.Insert(selectionStart, " " + text + " ");
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = (selectionStart + text.Length) + 2;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void btnDivide_Click(object sender, EventArgs e)
        {
            string text = (sender as Button).Text;
            string str2 = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                str2 = str2.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = str2.Insert(selectionStart, text);
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = (selectionStart + text.Length) + 1;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            string text = this.btnEqual.Text;
            string str2 = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                str2 = str2.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = str2.Insert(selectionStart, " " + text + " ");
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = (selectionStart + text.Length) + 2;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void btnGreat_Click(object sender, EventArgs e)
        {
            string text = this.btnGreat.Text;
            string str2 = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                str2 = str2.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = str2.Insert(selectionStart, " " + text + " ");
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = (selectionStart + text.Length) + 2;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void btnGreatEqual_Click(object sender, EventArgs e)
        {
            string text = this.btnGreatEqual.Text;
            string str2 = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                str2 = str2.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = str2.Insert(selectionStart, " " + text + " ");
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = (selectionStart + text.Length) + 2;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void btnLittle_Click(object sender, EventArgs e)
        {
            string text = this.btnLittle.Text;
            string str2 = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                str2 = str2.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = str2.Insert(selectionStart, " " + text + " ");
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = (selectionStart + text.Length) + 2;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void btnLittleEqual_Click(object sender, EventArgs e)
        {
            string text = this.btnLittleEqual.Text;
            string str2 = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                str2 = str2.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = str2.Insert(selectionStart, " " + text + " ");
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = (selectionStart + text.Length) + 2;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void btnMatchOneChar_Click(object sender, EventArgs e)
        {
            string text = this.btnMatchOneChar.Text;
            string str2 = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                str2 = str2.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = str2.Insert(selectionStart, text);
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = (selectionStart + text.Length) + 1;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void btnMatchString_Click(object sender, EventArgs e)
        {
            string text = this.btnMatchString.Text;
            string str2 = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            this.memEditWhereCaluse.Focus();
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                str2 = str2.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = str2.Insert(selectionStart, " " + text + " ");
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = (selectionStart + text.Length) + 2;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void btnNotEqual_Click(object sender, EventArgs e)
        {
            string text = this.btnNotEqual.Text;
            string str2 = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                str2 = str2.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = str2.Insert(selectionStart, " " + text + " ");
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = (selectionStart + text.Length) + 2;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void btnNum0_Click(object sender, EventArgs e)
        {
            string text = (sender as Button).Text;
            string str2 = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                str2 = str2.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = str2.Insert(selectionStart, text);
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = selectionStart + text.Length;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
        }

        private void btnOr_Click(object sender, EventArgs e)
        {
            string str2;
            string text = (sender as Button).Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                str2 = this.memEditWhereCaluse.Text.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            else
            {
                str2 = this.memEditWhereCaluse.Text;
            }
            this.memEditWhereCaluse.Text = str2.Insert(selectionStart, string.Format(" {0} ", text));
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = (selectionStart + text.Length) + 2;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        public void ClearWhereCaluse()
        {
            this.memEditWhereCaluse.Text = "";
        }

        private void Fieldlist_DoubleClick(object sender, EventArgs e)
        {
            string str = "";
            if (this.Fieldlist.SelectedItem != null)
            {
                str = this.Fieldlist.SelectedItem.ToString();
                string text = this.memEditWhereCaluse.Text;
                int selectionStart = this.memEditWhereCaluse.SelectionStart;
                if (this.memEditWhereCaluse.SelectionLength > 0)
                {
                    text = text.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
                }
                this.memEditWhereCaluse.Text = text.Insert(selectionStart, " " + str + " ");
                try
                {
                    this.memEditWhereCaluse.Focus();
                }
                catch
                {
                }
                this.memEditWhereCaluse.SelectionStart = (selectionStart + str.Length) + 1;
                this.memEditWhereCaluse.SelectionLength = 0;
            }
        }

        private void frmExpressBulider_Load(object sender, EventArgs e)
        {
            this.method_3();
            this.bool_0 = true;
        }

        private void method_0(object sender, EventArgs e)
        {
            string str = "LIKE";
            string text = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                text = text.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = text.Insert(selectionStart, " " + str + " ");
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = (selectionStart + str.Length) + 2;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void method_1(object sender, EventArgs e)
        {
            string str = "AND";
            string text = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                text = text.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = text.Insert(selectionStart, " " + str + " ");
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = (selectionStart + str.Length) + 2;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void method_2(object sender, EventArgs e)
        {
            this.memEditWhereCaluse.Text = "";
        }

        private void method_3()
        {
            for (int i = 0; i < this.MapTemplate.MapTemplateParam.Count; i++)
            {
                MapTemplateParam item = this.MapTemplate.MapTemplateParam[i];
                this.Fieldlist.Items.Add(item);
            }
            this.memEditWhereCaluse.Text = this.string_0;
        }

        public string Expression
        {
            get { return ("=" + this.memEditWhereCaluse.Text); }
            set
            {
                if (value.Length > 0)
                {
                    if (value[0] == '=')
                    {
                        this.string_0 = value.Substring(1);
                    }
                    else
                    {
                        this.string_0 = value;
                    }
                }
                else
                {
                    this.string_0 = value;
                }
            }
        }

        public IList FieldList
        {
            set { this.ilist_0 = value; }
        }

        public MapCartoTemplateLib.MapTemplate MapTemplate { get; set; }
    }
}