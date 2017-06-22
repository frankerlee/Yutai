using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public partial class frmSetTabelCell : Form
    {
        private IContainer icontainer_0 = null;
        internal SortedList<int, SortedList<int, string>> m_tabcell = new SortedList<int, SortedList<int, string>>();
        [CompilerGenerated]

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

