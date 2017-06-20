using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.Plugins.TableEditor.Controls
{
    public class SelectFields : CheckedListBox
    {
        private IFields _fields;
        private string _selectFields;

        public IFields Fields
        {
            get { return _fields; }
            set
            {
                _fields = value;
                LoadFields();
            }
        }

        private void LoadFields()
        {
            this.Items.Clear();
            if (_fields == null)
                return;
            for (int i = 0; i < _fields.FieldCount; i++)
            {
                IField field = _fields.Field[i];
                this.Items.Add($"{field.Name}({field.AliasName})");
            }
        }

        public void SelectAll()
        {
            for (int i = 0; i < this.Items.Count; i++)
            {
                this.SetItemChecked(i, true);
            }
        }

        public void SelectClear()
        {
            for (int i = 0; i < this.Items.Count; i++)
            {
                this.SetItemChecked(i, false);
            }
        }

        public string GetSelectedFields(char splitChar)
        {
            _selectFields = null;
            foreach (object checkedItem in this.CheckedItems)
            {
                _selectFields = string.IsNullOrWhiteSpace(_selectFields) ? $"{this.GetItemText(checkedItem).Split('(')[0]}" : $"{_selectFields}{splitChar}{this.GetItemText(checkedItem).Split('(')[0]}";
            }
            return _selectFields;
        }
    }
}
