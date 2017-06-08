using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public class frmExpressBulider : Form
    {
        private bool bool_0 = false;
        private Button btnAdd;
        private Button btnAnd;
        private Button btnBracket;
        private Button btnCancel;
        private Button btnDivide;
        private Button btnEqual;
        private Button btnGreat;
        private Button btnGreatEqual;
        private Button btnIsTrue;
        private Button btnLittle;
        private Button btnLittleEqual;
        private Button btnMatchOneChar;
        private Button btnMatchString;
        private Button btnNotEqual;
        private Button btnNum0;
        private Button btnNum1;
        private Button btnNum2;
        private Button btnNum3;
        private Button btnNum4;
        private Button btnNum5;
        private Button btnNum6;
        private Button btnNum7;
        private Button btnNum8;
        private Button btnNum9;
        private Button btnNumDot;
        private Button btnOK;
        private Button btnOr;
        private Button btnSelectOne;
        private Button btnSubstruct;
        private Container container_0 = null;
        private ListBox Fieldlist;
        private IList ilist_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        [CompilerGenerated]
        private MapCartoTemplateLib.MapTemplate mapTemplate_0;
        private TextBox memEditWhereCaluse;
        private string string_0 = "";
        private string string_1 = "";
        private ListBox UniqueValuelist;

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

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_1);
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

        private void InitializeComponent()
        {
            this.label2 = new Label();
            this.btnMatchString = new Button();
            this.btnAdd = new Button();
            this.btnOr = new Button();
            this.btnBracket = new Button();
            this.btnMatchOneChar = new Button();
            this.btnAnd = new Button();
            this.btnLittleEqual = new Button();
            this.btnLittle = new Button();
            this.btnSelectOne = new Button();
            this.btnIsTrue = new Button();
            this.btnGreat = new Button();
            this.btnNotEqual = new Button();
            this.btnGreatEqual = new Button();
            this.btnEqual = new Button();
            this.memEditWhereCaluse = new TextBox();
            this.Fieldlist = new ListBox();
            this.label4 = new Label();
            this.UniqueValuelist = new ListBox();
            this.btnOK = new Button();
            this.label1 = new Label();
            this.btnCancel = new Button();
            this.btnSubstruct = new Button();
            this.btnDivide = new Button();
            this.btnNum5 = new Button();
            this.btnNum2 = new Button();
            this.btnNum6 = new Button();
            this.btnNum1 = new Button();
            this.btnNum3 = new Button();
            this.btnNum4 = new Button();
            this.btnNum8 = new Button();
            this.btnNum7 = new Button();
            this.btnNum9 = new Button();
            this.btnNumDot = new Button();
            this.btnNum0 = new Button();
            this.label3 = new Label();
            base.SuspendLayout();
            this.label2.Location = new Point(0x10, 8);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x30, 0x10);
            this.label2.TabIndex = 0x2b;
            this.label2.Text = "参数";
            this.btnMatchString.Location = new Point(330, 0x36);
            this.btnMatchString.Name = "btnMatchString";
            this.btnMatchString.Size = new Size(0x20, 0x18);
            this.btnMatchString.TabIndex = 0x2a;
            this.btnMatchString.Text = "%";
            this.btnMatchString.Click += new EventHandler(this.btnMatchString_Click);
            this.btnAdd.Location = new Point(0xfe, 0x18);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(0x20, 0x18);
            this.btnAdd.TabIndex = 0x29;
            this.btnAdd.Text = "+";
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            this.btnOr.Location = new Point(0xfe, 0x90);
            this.btnOr.Name = "btnOr";
            this.btnOr.Size = new Size(0x20, 0x18);
            this.btnOr.TabIndex = 40;
            this.btnOr.Text = "||";
            this.btnOr.Click += new EventHandler(this.btnOr_Click);
            this.btnBracket.Location = new Point(330, 0x18);
            this.btnBracket.Name = "btnBracket";
            this.btnBracket.Size = new Size(0x20, 0x18);
            this.btnBracket.TabIndex = 0x27;
            this.btnBracket.Text = "()";
            this.btnBracket.Click += new EventHandler(this.btnBracket_Click);
            this.btnMatchOneChar.Location = new Point(0xfe, 0x36);
            this.btnMatchOneChar.Name = "btnMatchOneChar";
            this.btnMatchOneChar.Size = new Size(0x20, 0x18);
            this.btnMatchOneChar.TabIndex = 0x26;
            this.btnMatchOneChar.Text = "*";
            this.btnMatchOneChar.Click += new EventHandler(this.btnMatchOneChar_Click);
            this.btnAnd.Location = new Point(330, 0x90);
            this.btnAnd.Name = "btnAnd";
            this.btnAnd.Size = new Size(0x20, 0x18);
            this.btnAnd.TabIndex = 0x25;
            this.btnAnd.Tag = "&&";
            this.btnAnd.Text = "&&";
            this.btnAnd.Click += new EventHandler(this.btnAnd_Click);
            this.btnLittleEqual.Location = new Point(0x124, 0x72);
            this.btnLittleEqual.Name = "btnLittleEqual";
            this.btnLittleEqual.Size = new Size(0x20, 0x18);
            this.btnLittleEqual.TabIndex = 0x24;
            this.btnLittleEqual.Text = "<=";
            this.btnLittleEqual.Click += new EventHandler(this.btnLittleEqual_Click);
            this.btnLittle.Location = new Point(0x124, 0x54);
            this.btnLittle.Name = "btnLittle";
            this.btnLittle.Size = new Size(0x20, 0x18);
            this.btnLittle.TabIndex = 0x23;
            this.btnLittle.Text = "<";
            this.btnLittle.Click += new EventHandler(this.btnLittle_Click);
            this.btnSelectOne.Location = new Point(0xca, 0x90);
            this.btnSelectOne.Name = "btnSelectOne";
            this.btnSelectOne.Size = new Size(0x20, 0x18);
            this.btnSelectOne.TabIndex = 0x22;
            this.btnSelectOne.Text = ":";
            this.btnSelectOne.Click += new EventHandler(this.btnDivide_Click);
            this.btnIsTrue.Location = new Point(0x7e, 0x90);
            this.btnIsTrue.Name = "btnIsTrue";
            this.btnIsTrue.Size = new Size(0x20, 0x18);
            this.btnIsTrue.TabIndex = 0x21;
            this.btnIsTrue.Text = "?";
            this.btnIsTrue.Click += new EventHandler(this.btnDivide_Click);
            this.btnGreat.Location = new Point(0xfe, 0x54);
            this.btnGreat.Name = "btnGreat";
            this.btnGreat.Size = new Size(0x20, 0x18);
            this.btnGreat.TabIndex = 0x20;
            this.btnGreat.Text = ">";
            this.btnGreat.Click += new EventHandler(this.btnGreat_Click);
            this.btnNotEqual.Location = new Point(330, 0x72);
            this.btnNotEqual.Name = "btnNotEqual";
            this.btnNotEqual.Size = new Size(0x20, 0x18);
            this.btnNotEqual.TabIndex = 0x1f;
            this.btnNotEqual.Text = "!=";
            this.btnNotEqual.Click += new EventHandler(this.btnNotEqual_Click);
            this.btnGreatEqual.Location = new Point(0xfe, 0x72);
            this.btnGreatEqual.Name = "btnGreatEqual";
            this.btnGreatEqual.Size = new Size(0x20, 0x18);
            this.btnGreatEqual.TabIndex = 30;
            this.btnGreatEqual.Text = ">=";
            this.btnGreatEqual.Click += new EventHandler(this.btnGreatEqual_Click);
            this.btnEqual.Location = new Point(330, 0x54);
            this.btnEqual.Name = "btnEqual";
            this.btnEqual.Size = new Size(0x20, 0x18);
            this.btnEqual.TabIndex = 0x1d;
            this.btnEqual.Text = "==";
            this.btnEqual.Click += new EventHandler(this.btnEqual_Click);
            this.memEditWhereCaluse.Location = new Point(0x2d, 230);
            this.memEditWhereCaluse.Name = "memEditWhereCaluse";
            this.memEditWhereCaluse.Size = new Size(0x1c4, 0x15);
            this.memEditWhereCaluse.TabIndex = 0x1a;
            this.Fieldlist.ItemHeight = 12;
            this.Fieldlist.Location = new Point(0x10, 0x18);
            this.Fieldlist.Name = "Fieldlist";
            this.Fieldlist.Size = new Size(0x68, 160);
            this.Fieldlist.TabIndex = 0x18;
            this.Fieldlist.DoubleClick += new EventHandler(this.Fieldlist_DoubleClick);
            this.label4.Location = new Point(0x17b, 5);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x34, 0x10);
            this.label4.TabIndex = 0x30;
            this.label4.Text = "函数";
            this.UniqueValuelist.ItemHeight = 12;
            this.UniqueValuelist.Items.AddRange(new object[] { 
                "sin()", "cos()", "log10()", "tan()", "sqrt()", "exp()", "floor()", "pow(,)", "log(,)", "abs()", "acos()", "asin()", "atan()", "atn2(,)", "ceiling()", "cosh()", 
                "ieeeremainder(,)", "max(,)", "min(,)", "sinh()", "truncate()"
             });
            this.UniqueValuelist.Location = new Point(0x179, 0x18);
            this.UniqueValuelist.Name = "UniqueValuelist";
            this.UniqueValuelist.Size = new Size(120, 160);
            this.UniqueValuelist.TabIndex = 0x2f;
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0x159, 0x110);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x45, 0x17);
            this.btnOK.TabIndex = 50;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 0xcc);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x29, 12);
            this.label1.TabIndex = 0x33;
            this.label1.Text = "表达式";
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(430, 0x110);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x45, 0x17);
            this.btnCancel.TabIndex = 0x34;
            this.btnCancel.Text = "取消";
            this.btnSubstruct.Location = new Point(0x124, 0x18);
            this.btnSubstruct.Name = "btnSubstruct";
            this.btnSubstruct.Size = new Size(0x20, 0x18);
            this.btnSubstruct.TabIndex = 0x35;
            this.btnSubstruct.Text = "-";
            this.btnSubstruct.Click += new EventHandler(this.btnDivide_Click);
            this.btnDivide.Location = new Point(0x124, 0x36);
            this.btnDivide.Name = "btnDivide";
            this.btnDivide.Size = new Size(0x20, 0x18);
            this.btnDivide.TabIndex = 0x36;
            this.btnDivide.Text = "/";
            this.btnDivide.Click += new EventHandler(this.btnDivide_Click);
            this.btnNum5.Location = new Point(0xa4, 0x36);
            this.btnNum5.Name = "btnNum5";
            this.btnNum5.Size = new Size(0x20, 0x18);
            this.btnNum5.TabIndex = 0x3f;
            this.btnNum5.Text = "5";
            this.btnNum5.Click += new EventHandler(this.btnNum0_Click);
            this.btnNum2.Location = new Point(0xa4, 0x18);
            this.btnNum2.Name = "btnNum2";
            this.btnNum2.Size = new Size(0x20, 0x18);
            this.btnNum2.TabIndex = 0x3e;
            this.btnNum2.Text = "2";
            this.btnNum2.Click += new EventHandler(this.btnNum0_Click);
            this.btnNum6.Location = new Point(0xca, 0x36);
            this.btnNum6.Name = "btnNum6";
            this.btnNum6.Size = new Size(0x20, 0x18);
            this.btnNum6.TabIndex = 0x3d;
            this.btnNum6.Text = "6";
            this.btnNum6.Click += new EventHandler(this.btnNum0_Click);
            this.btnNum1.Location = new Point(0x7e, 0x18);
            this.btnNum1.Name = "btnNum1";
            this.btnNum1.Size = new Size(0x20, 0x18);
            this.btnNum1.TabIndex = 60;
            this.btnNum1.Text = "1";
            this.btnNum1.Click += new EventHandler(this.btnNum0_Click);
            this.btnNum3.Location = new Point(0xca, 0x18);
            this.btnNum3.Name = "btnNum3";
            this.btnNum3.Size = new Size(0x20, 0x18);
            this.btnNum3.TabIndex = 0x3b;
            this.btnNum3.Text = "3";
            this.btnNum3.Click += new EventHandler(this.btnNum0_Click);
            this.btnNum4.Location = new Point(0x7e, 0x36);
            this.btnNum4.Name = "btnNum4";
            this.btnNum4.Size = new Size(0x20, 0x18);
            this.btnNum4.TabIndex = 0x3a;
            this.btnNum4.Text = "4";
            this.btnNum4.Click += new EventHandler(this.btnNum0_Click);
            this.btnNum8.Location = new Point(0xa4, 0x54);
            this.btnNum8.Name = "btnNum8";
            this.btnNum8.Size = new Size(0x20, 0x18);
            this.btnNum8.TabIndex = 0x39;
            this.btnNum8.Text = "8";
            this.btnNum8.Click += new EventHandler(this.btnNum0_Click);
            this.btnNum7.Location = new Point(0x7e, 0x54);
            this.btnNum7.Name = "btnNum7";
            this.btnNum7.Size = new Size(0x20, 0x18);
            this.btnNum7.TabIndex = 0x38;
            this.btnNum7.Text = "7";
            this.btnNum7.Click += new EventHandler(this.btnNum0_Click);
            this.btnNum9.Location = new Point(0xca, 0x54);
            this.btnNum9.Name = "btnNum9";
            this.btnNum9.Size = new Size(0x20, 0x18);
            this.btnNum9.TabIndex = 0x37;
            this.btnNum9.Text = "9";
            this.btnNum9.Click += new EventHandler(this.btnNum0_Click);
            this.btnNumDot.Location = new Point(0xca, 0x72);
            this.btnNumDot.Name = "btnNumDot";
            this.btnNumDot.Size = new Size(0x20, 0x18);
            this.btnNumDot.TabIndex = 0x41;
            this.btnNumDot.Text = ".";
            this.btnNumDot.Click += new EventHandler(this.btnNum0_Click);
            this.btnNum0.Location = new Point(0x7e, 0x72);
            this.btnNum0.Name = "btnNum0";
            this.btnNum0.Size = new Size(0x20, 0x18);
            this.btnNum0.TabIndex = 0x40;
            this.btnNum0.Text = "0";
            this.btnNum0.Click += new EventHandler(this.btnNum0_Click);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x1c, 0xe9);
            this.label3.Name = "label3";
            this.label3.Size = new Size(11, 12);
            this.label3.TabIndex = 0x42;
            this.label3.Text = "=";
            base.ClientSize = new Size(0x209, 0x130);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.btnNumDot);
            base.Controls.Add(this.btnNum0);
            base.Controls.Add(this.btnNum5);
            base.Controls.Add(this.btnNum2);
            base.Controls.Add(this.btnNum6);
            base.Controls.Add(this.btnNum1);
            base.Controls.Add(this.btnNum3);
            base.Controls.Add(this.btnNum4);
            base.Controls.Add(this.btnNum8);
            base.Controls.Add(this.btnNum7);
            base.Controls.Add(this.btnNum9);
            base.Controls.Add(this.btnDivide);
            base.Controls.Add(this.btnSubstruct);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.UniqueValuelist);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.btnMatchString);
            base.Controls.Add(this.btnAdd);
            base.Controls.Add(this.btnOr);
            base.Controls.Add(this.btnBracket);
            base.Controls.Add(this.btnMatchOneChar);
            base.Controls.Add(this.btnAnd);
            base.Controls.Add(this.btnLittleEqual);
            base.Controls.Add(this.btnLittle);
            base.Controls.Add(this.btnSelectOne);
            base.Controls.Add(this.btnIsTrue);
            base.Controls.Add(this.btnGreat);
            base.Controls.Add(this.btnNotEqual);
            base.Controls.Add(this.btnGreatEqual);
            base.Controls.Add(this.btnEqual);
            base.Controls.Add(this.memEditWhereCaluse);
            base.Controls.Add(this.Fieldlist);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmExpressBulider";
            this.Text = "表达式构造器";
            base.Load += new EventHandler(this.frmExpressBulider_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
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
            get
            {
                return ("=" + this.memEditWhereCaluse.Text);
            }
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
            set
            {
                this.ilist_0 = value;
            }
        }

        public MapCartoTemplateLib.MapTemplate MapTemplate
        {
            [CompilerGenerated]
            get
            {
                return this.mapTemplate_0;
            }
            [CompilerGenerated]
            set
            {
                this.mapTemplate_0 = value;
            }
        }
    }
}

