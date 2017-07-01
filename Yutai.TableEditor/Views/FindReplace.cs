using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.TableEditor.Editor;
using Yutai.Plugins.TableEditor.Enums;

namespace Yutai.Plugins.TableEditor.Views
{
    public partial class FindReplace : Form, IFindReplaceView
    {
        private IAppContext _context;
        private IVirtualGridView _view;
        private int _searchCount = 0;
        private int _firstRow = -1;
        private int _firstColumn = -1;

        public FindReplace(IAppContext context, IVirtualGridView view)
        {
            _context = context;
            _view = view;
            InitializeComponent();
            InitControls();
        }

        private void InitControls()
        {
            cboMatch.DataSource = Enum.GetNames(typeof(MatchType));
            cboFields.Items.Add("<all>");
            int count = _view.GridView.ColumnCount;
            for (int i = 0; i < count; i++)
            {
                DataGridViewColumn pColumn = _view.GridView.Columns[i];
                cboFields.Items.Add(pColumn.HeaderText);
            }
            cboFields.SelectedIndex = 0;
        }

        public string SearchContent
        {
            get { return txtFind.Text; }
        }

        public MatchType MatchType
        {
            get { return (MatchType) Enum.Parse(typeof(MatchType), cboMatch.SelectedItem.ToString(), false); }
        }

        public string FieldName
        {
            get { return cboFields.SelectedItem.ToString(); }
        }

        public DataGridViewCell CurrentCell
        {
            get { return _view.GridView.CurrentCell; }
        }

        public DataGridViewCell MoveToNextCell(int columnIndex = -1)
        {
            DataGridViewCell pCell = GetNextCell(CurrentCell, columnIndex);
            _view.GridView.CurrentCell = pCell;
            pCell.Selected = true;
            return pCell;
        }

        public DataGridViewCell GetNextCell(DataGridViewCell cell, int columnIndex = -1)
        {
            int rowIdx;
            int colIdx = cell.ColumnIndex;
            if (columnIndex > -1)
            {
                if (colIdx == columnIndex)
                {
                    rowIdx = cell.RowIndex + 1;
                }
                else
                {
                    rowIdx = cell.RowIndex;
                    colIdx = columnIndex;
                }
            }
            else
            {
                rowIdx = cell.RowIndex;
                colIdx = cell.ColumnIndex + 1;
            }
            if (colIdx >= _view.GridView.ColumnCount)
            {
                colIdx = 0;
                rowIdx += 1;
            }
            if (rowIdx >= _view.GridView.RowCount)
            {
                rowIdx = 0;
            }
            return _view.GridView.Rows[rowIdx].Cells[colIdx];
        }

        public void Find(DataGridViewCell cell, bool isClick = false)
        {
            int columnIndex = -1;
            if (FieldName != "<all>")
            {
                columnIndex = GetColumnIndex(FieldName);
            }
            DataGridViewCell pCell = GetNextCell(cell ?? CurrentCell, columnIndex);
            if (isClick)
            {
                _firstRow = pCell.RowIndex;
                _firstColumn = pCell.ColumnIndex;
            }
            if (pCell.RowIndex == _firstRow && pCell.ColumnIndex == _firstColumn)
            {
                if (_searchCount == 0 && isClick == false)
                {
                    _firstRow = -1;
                    _firstColumn = -1;
                    MessageBox.Show(@"没有找到符合条件的记录！", null, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            if ((MatchType == MatchType.Contains && pCell.Value.ToString().Contains(SearchContent)) ||
                (MatchType == MatchType.Match && pCell.Value.ToString().Equals(SearchContent)) ||
                (MatchType == MatchType.Start && pCell.Value.ToString().StartsWith(SearchContent)))
            {
                _searchCount += 1;
                _view.GridView.CurrentCell = pCell;
                pCell.Selected = true;
                _firstRow = pCell.RowIndex;
                _firstColumn = pCell.ColumnIndex;
            }
            else
            {
                Find(pCell, false);
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            Find(null, true);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private int GetColumnIndex(string header)
        {
            int count = _view.GridView.ColumnCount;
            for (int i = 0; i < count; i++)
            {
                DataGridViewColumn pColumn = _view.GridView.Columns[i];
                if (pColumn.HeaderText == header)
                    return pColumn.Index;
            }
            return -1;
        }
    }
}