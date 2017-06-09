using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public class TableCellSetPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private Button btnSetCell;
        private IContainer icontainer_0 = null;
        private ListView listView1;
        private MapTemplateElement mapTemplateElement_0;
        private SortedList<int, SortedList<int, string>> sortedList_0 = new SortedList<int, SortedList<int, string>>();

        public event OnValueChangeEventHandler OnValueChange;

        public TableCellSetPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_0)
            {
                this.bool_0 = false;
                int rowNumber = (this.mapTemplateElement_0 as MapTemplateTableElement).RowNumber;
                int columnNumber = (this.mapTemplateElement_0 as MapTemplateTableElement).ColumnNumber;
                for (int i = 0; i < rowNumber; i++)
                {
                    for (int j = 0; j < columnNumber; j++)
                    {
                        (this.mapTemplateElement_0 as MapTemplateTableElement).SetTableCell(i, j, this.method_1(i, j));
                    }
                }
            }
        }

        private void btnSetCell_Click(object sender, EventArgs e)
        {
            frmSetTabelCell cell = new frmSetTabelCell {
                MapTemplateTableElement = this.mapTemplateElement_0 as MapTemplateTableElement,
                m_tabcell = this.sortedList_0
            };
            if (cell.ShowDialog() == DialogResult.OK)
            {
                MapTemplateTableElement element = this.mapTemplateElement_0 as MapTemplateTableElement;
                int rowNumber = element.RowNumber;
                int columnNumber = element.ColumnNumber;
                this.sortedList_0 = cell.m_tabcell;
                for (int i = 0; i < rowNumber; i++)
                {
                    ListViewItem item = this.listView1.Items[i];
                    int count = item.SubItems.Count;
                    for (int j = 0; j < columnNumber; j++)
                    {
                        item.SubItems[j + 1].Text = this.method_1(i, j);
                    }
                }
                this.bool_0 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        public void Cancel()
        {
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        public void Init()
        {
            int num3;
            MapTemplateTableElement element = this.mapTemplateElement_0 as MapTemplateTableElement;
            int rowNumber = element.RowNumber;
            int columnNumber = element.ColumnNumber;
            this.listView1.Clear();
            ColumnHeader header = new ColumnHeader {
                Text = "  "
            };
            this.listView1.Columns.Add(header);
            for (num3 = 0; num3 < columnNumber; num3++)
            {
                ColumnHeader header2 = new ColumnHeader();
                int num4 = num3 + 1;
                header2.Text = num4.ToString();
                this.listView1.Columns.Add(header2);
            }
            string[] items = new string[columnNumber + 1];
            for (num3 = 0; num3 < (columnNumber + 1); num3++)
            {
                items[num3] = "";
            }
            for (num3 = 0; num3 < rowNumber; num3++)
            {
                items[0] = (num3 + 1).ToString();
                for (int i = 1; i < (columnNumber + 1); i++)
                {
                    items[i] = this.method_1(num3, i - 1);
                }
                this.listView1.Items.Add(new ListViewItem(items));
            }
        }

        private void InitializeComponent()
        {
            this.listView1 = new ListView();
            this.btnSetCell = new Button();
            base.SuspendLayout();
            this.listView1.Location = new Point(13, 0x2e);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0x128, 0xb5);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.btnSetCell.Location = new Point(0x7a, 0xfb);
            this.btnSetCell.Name = "btnSetCell";
            this.btnSetCell.Size = new Size(0x56, 0x17);
            this.btnSetCell.TabIndex = 1;
            this.btnSetCell.Text = "设置单元格";
            this.btnSetCell.UseVisualStyleBackColor = true;
            this.btnSetCell.Click += new EventHandler(this.btnSetCell_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.btnSetCell);
            base.Controls.Add(this.listView1);
            base.Name = "TableCellSetPage";
            base.Size = new Size(0x147, 0x12d);
            base.Load += new EventHandler(this.TableCellSetPage_Load);
            base.VisibleChanged += new EventHandler(this.TableCellSetPage_VisibleChanged);
            base.ResumeLayout(false);
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

        private void method_0(int int_0, int int_1, string string_0)
        {
            SortedList<int, string> list;
            if (this.sortedList_0.ContainsKey(int_0))
            {
                list = this.sortedList_0[int_0];
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
                this.sortedList_0.Add(int_0, list);
            }
        }

        private string method_1(int int_0, int int_1)
        {
            if (this.sortedList_0.ContainsKey(int_0))
            {
                SortedList<int, string> list = this.sortedList_0[int_0];
                if (list.ContainsKey(int_1))
                {
                    return list[int_1];
                }
            }
            return "";
        }

        public void ResetControl()
        {
            this.Init();
        }

        public void SetObjects(object object_0)
        {
            this.mapTemplateElement_0 = object_0 as MapTemplateElement;
        }

        private void TableCellSetPage_Load(object sender, EventArgs e)
        {
            MapTemplateTableElement element = this.mapTemplateElement_0 as MapTemplateTableElement;
            int rowNumber = element.RowNumber;
            int columnNumber = element.ColumnNumber;
            for (int i = 0; i < rowNumber; i++)
            {
                for (int j = 0; j < columnNumber; j++)
                {
                    this.method_0(i, j, element.GetCellElement(i, j));
                }
            }
            this.Init();
        }

        private void TableCellSetPage_VisibleChanged(object sender, EventArgs e)
        {
            if (base.Visible)
            {
                this.Init();
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_0;
            }
        }

        int IPropertyPage.Height
        {
            get
            {
                return base.Height;
            }
        }

        int IPropertyPage.Width
        {
            get
            {
                return base.Width;
            }
        }

        public string Title
        {
            get
            {
                return "单元格";
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}

