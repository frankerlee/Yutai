using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public class frmSetTabelCell : Form
    {
        private Button btnExpress;
        private Button btnOK;
        private Button button1;
        private Button button2;
        private ComboBox cboCol;
        private ComboBox cboRow;
        private IContainer icontainer_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        internal SortedList<int, SortedList<int, string>> m_tabcell = new SortedList<int, SortedList<int, string>>();
        [CompilerGenerated]
        private MapCartoTemplateLib.MapTemplateTableElement mapTemplateTableElement_0;
        private TextBox txtExpress;

        public frmSetTabelCell()
        {
            this.InitializeComponent();
        }

        private void btnExpress_Click(object sender, EventArgs e)
        {
            if ((this.cboCol.SelectedIndex != -1) && (this.cboRow.SelectedIndex != -1))
            {
                frmExpressBulider bulider2 = new frmExpressBulider {
                    MapTemplate = this.MapTemplateTableElement.MapTemplate,
                    Expression = this.txtExpress.Text
                };
                if (bulider2.ShowDialog() == DialogResult.OK)
                {
                    this.txtExpress.Text = bulider2.Expression;
                    this.method_0(this.cboRow.SelectedIndex, this.cboCol.SelectedIndex, this.txtExpress.Text);
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((this.cboCol.SelectedIndex != -1) && (this.cboRow.SelectedIndex != -1))
            {
                this.method_0(this.cboRow.SelectedIndex, this.cboCol.SelectedIndex, this.txtExpress.Text);
            }
        }

        private void cboCol_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtExpress.Text = "";
            if ((this.cboCol.SelectedIndex != -1) && (this.cboRow.SelectedIndex != -1))
            {
                this.txtExpress.Text = this.method_1(this.cboRow.SelectedIndex, this.cboCol.SelectedIndex);
            }
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmSetTabelCell_Load(object sender, EventArgs e)
        {
            int num3;
            int rowNumber = this.MapTemplateTableElement.RowNumber;
            int columnNumber = this.MapTemplateTableElement.ColumnNumber;
            for (num3 = 1; num3 <= rowNumber; num3++)
            {
                this.cboRow.Items.Add(num3);
            }
            for (num3 = 1; num3 <= columnNumber; num3++)
            {
                this.cboCol.Items.Add(num3);
            }
            this.cboRow.SelectedIndex = 0;
            this.cboCol.SelectedIndex = 0;
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.cboRow = new ComboBox();
            this.cboCol = new ComboBox();
            this.label2 = new Label();
            this.btnExpress = new Button();
            this.label3 = new Label();
            this.txtExpress = new TextBox();
            this.btnOK = new Button();
            this.button1 = new Button();
            this.button2 = new Button();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 0x1a);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x11, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "行";
            this.cboRow.FormattingEnabled = true;
            this.cboRow.Location = new Point(0x4b, 0x17);
            this.cboRow.Name = "cboRow";
            this.cboRow.Size = new Size(0x79, 20);
            this.cboRow.TabIndex = 1;
            this.cboRow.SelectedIndexChanged += new EventHandler(this.cboCol_SelectedIndexChanged);
            this.cboCol.FormattingEnabled = true;
            this.cboCol.Location = new Point(0x4b, 50);
            this.cboCol.Name = "cboCol";
            this.cboCol.Size = new Size(0x79, 20);
            this.cboCol.TabIndex = 3;
            this.cboCol.SelectedIndexChanged += new EventHandler(this.cboCol_SelectedIndexChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 0x35);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x11, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "列";
            this.btnExpress.Location = new Point(0xca, 0x51);
            this.btnExpress.Name = "btnExpress";
            this.btnExpress.Size = new Size(0x49, 0x17);
            this.btnExpress.TabIndex = 4;
            this.btnExpress.Text = "设置表达式";
            this.btnExpress.UseVisualStyleBackColor = true;
            this.btnExpress.Click += new EventHandler(this.btnExpress_Click);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(12, 0x56);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x29, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "表达式";
            this.txtExpress.Location = new Point(0x4b, 0x51);
            this.txtExpress.Multiline = true;
            this.txtExpress.Name = "txtExpress";
            this.txtExpress.Size = new Size(0x79, 0x5d);
            this.txtExpress.TabIndex = 6;
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0x4b, 0xbf);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.button1.Location = new Point(200, 0x97);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 9;
            this.button1.Text = "应用";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.button2.DialogResult = DialogResult.Cancel;
            this.button2.Location = new Point(0xab, 0xbf);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x4b, 0x17);
            this.button2.TabIndex = 10;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x124, 0xe2);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.txtExpress);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.btnExpress);
            base.Controls.Add(this.cboCol);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.cboRow);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmSetTabelCell";
            this.Text = "设置单元格文本";
            base.Load += new EventHandler(this.frmSetTabelCell_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void method_0(int int_0, int int_1, string string_0)
        {
            SortedList<int, string> list;
            if (this.m_tabcell.ContainsKey(int_0))
            {
                list = this.m_tabcell[int_0];
                if (list.ContainsKey(int_1))
                {
                    list[int_1] = string_0;
                }
                else
                {
                    list.Add(int_1, string_0);
                }
            }
            else
            {
                list = new SortedList<int, string>();
                list.Add(int_1, string_0);
                this.m_tabcell.Add(int_0, list);
            }
        }

        private string method_1(int int_0, int int_1)
        {
            if (this.m_tabcell.ContainsKey(int_0))
            {
                SortedList<int, string> list = this.m_tabcell[int_0];
                if (list.ContainsKey(int_1))
                {
                    return list[int_1];
                }
            }
            return "";
        }

        public MapCartoTemplateLib.MapTemplateTableElement MapTemplateTableElement
        {
            [CompilerGenerated]
            get
            {
                return this.mapTemplateTableElement_0;
            }
            [CompilerGenerated]
            set
            {
                this.mapTemplateTableElement_0 = value;
            }
        }
    }
}

