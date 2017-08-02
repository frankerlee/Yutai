using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.Pipeline.Editor.Controls
{
    public partial class UcSelectFields : UserControl
    {
        private IFields _fields;
        public UcSelectFields()
        {
            InitializeComponent();
        }

        public IFields Fields
        {
            private get { return _fields; }
            set
            {
                _fields = value;
                LoadFields();
            }
        }

        public List<IField> SelectedFieldList
        {
            get
            {
                List<IField> list = new List<IField>();
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (checkedListBox1.GetItemChecked(i))
                    {
                        string fieldName = checkedListBox1.Items[i].ToString().Split('(')[0];
                        int idx = _fields.FindField(fieldName);
                        if (idx < 0)
                            continue;
                        list.Add(_fields.Field[idx]);
                    }
                }
                return list;
            }
            set
            {
                if (value == null)
                    value = new List<IField>();
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    string fieldName = checkedListBox1.Items[i].ToString().Split('(')[0];
                    if (value.FirstOrDefault(c => c.Name == fieldName) != null)
                        checkedListBox1.SetItemChecked(i, true);
                    else
                        checkedListBox1.SetItemChecked(i, false);
                }
            }
        }

        public IDictionary<int, string> SelectedFieldDictionary
        {
            get
            {
                IDictionary<int, string> dictionary = new Dictionary<int, string>();

                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (checkedListBox1.GetItemChecked(i))
                    {
                        string fieldName = checkedListBox1.Items[i].ToString().Split('(')[0];
                        int idx = _fields.FindField(fieldName);
                        if (idx < 0)
                            continue;
                        dictionary.Add(idx, _fields.Field[idx].Name);
                    }
                }
                return dictionary;
            }
        }

        public string SelectedFieldNames
        {
            get
            {
                string selectedFields = null;
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (checkedListBox1.GetItemChecked(i))
                    {
                        string fieldName = checkedListBox1.Items[i].ToString().Split('(')[0];
                        if (string.IsNullOrEmpty(selectedFields))
                            selectedFields = $"{fieldName}";
                        else
                            selectedFields = $"{selectedFields};{fieldName}";
                    }
                }
                return selectedFields;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    value = "";
                string[] selectedFields = value.Split(';');
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    string fieldName = checkedListBox1.Items[i].ToString().Split('(')[0];
                    if (selectedFields.Contains(fieldName))
                        checkedListBox1.SetItemChecked(i, true);
                    else
                        checkedListBox1.SetItemChecked(i, false);
                }
            }
        }

        private void LoadFields()
        {
            checkedListBox1.Items.Clear();
            if (_fields == null)
            {
                return;
            }
            for (int i = 0; i < _fields.FieldCount; i++)
            {
                IField pField = _fields.Field[i];
                checkedListBox1.Items.Add($"{pField.Name}({pField.AliasName})");
            }
        }

        public void SelectAll()
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, true);
            }
        }

        public void SelectClear()
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
        }
    }
}
