using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yutai.Plugins.TableEditor.Enums;

namespace Yutai.Plugins.TableEditor.Editor
{
    public delegate void TableEditEventHandler(object sender, TableType args);
    public partial class NavigationBar : UserControl
    {
        public event TableEditEventHandler SwitchTableEventHandler;
        public NavigationBar()
        {
            InitializeComponent();
        }

        [Browsable(true)]
        [Description("表格"), Category("扩展"), DefaultValue(null)]
        public DataGridView View { get; set; }

        private void mnuFirst_Click(object sender, EventArgs e)
        {
            MoveToFirst();
        }

        private void mnuPrevious_Click(object sender, EventArgs e)
        {
            MoveToPrevious();
        }

        private void mnuNext_Click(object sender, EventArgs e)
        {
            MoveToNext();
        }

        private void mnuLast_Click(object sender, EventArgs e)
        {
            MoveToLast();
        }

        private void mnuAllRecords_Click(object sender, EventArgs e)
        {
            OnSwitchTableEventHandler(TableType.All);
        }

        private void mnuSelectedRecords_Click(object sender, EventArgs e)
        {
            OnSwitchTableEventHandler(TableType.Selected);
        }

        public void SwitchTable(TableType type)
        {
            switch (type)
            {
                case TableType.All:
                    this.mnuAllRecords.Checked = true;
                    this.mnuSelectedRecords.Checked = false;
                    break;
                case TableType.Selected:
                    this.mnuAllRecords.Checked = false;
                    this.mnuSelectedRecords.Checked = true;
                    break;
            }
        }

        private void mnuCurrentRecord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int i = View.CurrentRow?.Index ?? 1;
                bool b = Int32.TryParse(this.mnuCurrentRecord.Text, out i);
                if (b)
                {
                    if (i > this.View.RowCount)
                        i = this.View.RowCount;
                    else if (i < 1)
                        i = 1;
                    MoveTo(i - 1);
                }
                else
                {
                    this.mnuCurrentRecord.Text = i.ToString();
                }
            }
        }
        public void MoveToFirst()
        {
            MoveTo(0);
        }

        public void MoveToLast()
        {
            MoveTo(this.View.RowCount - 1);
        }

        public void MoveToPrevious()
        {
            if (this.View.CurrentRow == null)
                return;
            MoveTo(this.View.CurrentRow.Index - 1);
        }

        public void MoveToNext()
        {
            if (this.View.CurrentRow == null)
                return;
            MoveTo(this.View.CurrentRow.Index + 1);

        }

        public void MoveTo(int rowIndex)
        {
            if (rowIndex < 0)
                return;
            if (rowIndex >= this.View.RowCount)
                return;
            this.View.ClearSelection();
            this.View.CurrentCell = this.View.Rows[rowIndex].Cells[0];
            this.View.Rows[rowIndex].Selected = true;
        }

        public void ShowRecordInfo()
        {
            int selectedCount = this.View.SelectedRows.Count;
            int count = this.View.RowCount;
            this.mnuRecordInfo.Text = string.Format("({0} / {1} 已选择)", selectedCount, count);
        }

        public void ShowCurrentRowInfo()
        {
            if (this.View.CurrentRow == null)
                return;
            this.mnuCurrentRecord.Text = (this.View.CurrentRow.Index + 1).ToString();
        }

        protected virtual void OnSwitchTableEventHandler(TableType args)
        {
            SwitchTableEventHandler?.Invoke(this, args);
            SwitchTable(args);
        }
    }
}
