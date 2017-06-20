using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.Plugins.TableEditor.Controls
{
    public class JoinsGrid : DataGridView
    {
        private readonly List<JoinModel> _joinModels;

        public JoinsGrid()
        {
            _joinModels = new List<JoinModel>();
        }

        public void AddColumns()
        {
            this.Columns.Clear();
            this.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = @"表名",
                Name = @"TableName",
                ReadOnly = true,
            });
            this.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = @"当前字段",
                Name = @"FromField",
                ReadOnly = true,
            });
            this.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = @"外部字段",
                Name = @"ToField",
                ReadOnly = true,
            });
            this.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = @"要加入的字段",
                Name = @"Fields",
                ReadOnly = true,
            });
        }

        public List<JoinModel> JoinModels => _joinModels;

        public JoinModel CurrentJoinModel => this.CurrentRow?.Tag as JoinModel;

        public void AddItem(JoinModel model)
        {
            int index = this.Rows.Add();
            this.Rows[index].Cells[0].Value = model.Name;
            this.Rows[index].Cells[1].Value = model.FromField;
            this.Rows[index].Cells[2].Value = model.ToField;
            this.Rows[index].Cells[3].Value = model.Fields;
            this.Rows[index].Tag = model;
            _joinModels.Add(model);
        }

        public void StopJoin()
        {
            if (CurrentJoinModel == null || this.CurrentRow == null)
                return;
            _joinModels.Remove(CurrentJoinModel);
            this.Rows.Remove(this.CurrentRow);
        }

        public void StopAll()
        {
            _joinModels.Clear();
            this.Rows.Clear();
        }
    }

    public class JoinModel
    {
        public ITable Table { get; set; }
        public string Name { get; set; }
        public string FromField { get; set; }
        public string ToField { get; set; }
        public string Fields { get; set; }
    }
}
