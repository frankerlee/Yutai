using System;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Plugins.Identifer.Helpers;

namespace Yutai.Plugins.Identifer.Query
{
    public partial class UcAttributeQueryBuilder : UserControl
    {
        private bool _canUse = false;

        private ILayer _queryLayer = null;

        private string _whereClause = "";

        private string _datasetName = "";

        private ITable _queryTable = null;


        public ILayer CurrentLayer
        {
            set
            {
                this._queryLayer = value;
                this._queryTable = this.GetQueryTable();
                this._datasetName = this._queryLayer.Name;
                if (this._canUse)
                {
                    this.InitForm();
                }
            }
        }

        public ITable Table
        {
            set
            {
                this._queryTable = value;
                this._datasetName = (this._queryTable as IDataset).Name;
                if (this._canUse)
                {
                    this.InitForm();
                }
            }
        }

        public string WhereCaluse
        {
            get { return this.memEditWhereCaluse.Text; }
            set { this._whereClause = value; }
        }

        public UcAttributeQueryBuilder()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this._queryLayer != null)
            {
                this._whereClause = this.memEditWhereCaluse.Text;
            }
        }

        private void UcAttributeQueryBulider_Load(object sender, EventArgs e)
        {
            this.InitForm();
            this._canUse = true;
        }

        private void btnAnd_Click(object sender, EventArgs e)
        {
            string str = "AND";
            string text = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                text = text.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = text.Insert(selectionStart, string.Concat(" ", str, " "));
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = selectionStart + str.Length + 2;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void btnBracket_Click(object sender, EventArgs e)
        {
            string text = this.btnBracket.Text;
            string str = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                str = str.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = str.Insert(selectionStart, string.Concat(" ", text, " "));
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = selectionStart + text.Length + 2;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            string text = this.btnEqual.Text;
            string str = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                str = str.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = str.Insert(selectionStart, string.Concat(" ", text, " "));
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = selectionStart + text.Length + 2;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void btnGetUniqueValue_Click(object sender, EventArgs e)
        {
            if (this._queryTable != null && this.Fieldlist.SelectedItem != null)
            {
                this.UniqueValuelist.Enabled = true;
                this.UniqueValuelist.Items.Clear();
                this.Fieldlist.SelectedItem.ToString();
                this.GetUniqueValues(this._queryTable, this.Fieldlist.SelectedItem.ToString(),
                    this.UniqueValuelist.Items);
            }
        }

        private void btnGreat_Click(object sender, EventArgs e)
        {
            string text = this.btnGreat.Text;
            string str = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                str = str.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = str.Insert(selectionStart, string.Concat(" ", text, " "));
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = selectionStart + text.Length + 2;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void btnGreatEqual_Click(object sender, EventArgs e)
        {
            string text = this.btnGreatEqual.Text;
            string str = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                str = str.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = str.Insert(selectionStart, string.Concat(" ", text, " "));
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = selectionStart + text.Length + 2;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void btnIs_Click(object sender, EventArgs e)
        {
            string str = "IS";
            string text = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                text = text.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = text.Insert(selectionStart, string.Concat(" ", str, " "));
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = selectionStart + str.Length + 2;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void btnLike_Click(object sender, EventArgs e)
        {
            string str = "LIKE";
            string text = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                text = text.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = text.Insert(selectionStart, string.Concat(" ", str, " "));
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = selectionStart + str.Length + 2;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void btnLittle_Click(object sender, EventArgs e)
        {
            string text = this.btnLittle.Text;
            string str = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                str = str.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = str.Insert(selectionStart, string.Concat(" ", text, " "));
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = selectionStart + text.Length + 2;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void btnLittleEqual_Click(object sender, EventArgs e)
        {
            string text = this.btnLittleEqual.Text;
            string str = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                str = str.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = str.Insert(selectionStart, string.Concat(" ", text, " "));
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = selectionStart + text.Length + 2;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void btnMatchOneChar_Click(object sender, EventArgs e)
        {
            string text = this.btnMatchOneChar.Text;
            string str = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                str = str.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = str.Insert(selectionStart, text);
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = selectionStart + text.Length + 1;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void btnMatchString_Click(object sender, EventArgs e)
        {
            string text = this.btnMatchString.Text;
            string str = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            this.memEditWhereCaluse.Focus();
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                str = str.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = str.Insert(selectionStart, string.Concat(" ", text, " "));
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = selectionStart + text.Length + 2;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void btnNot_Click(object sender, EventArgs e)
        {
            string str = "NOT";
            string text = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                text = text.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = text.Insert(selectionStart, string.Concat(" ", str, " "));
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = selectionStart + str.Length + 2;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void btnNotEqual_Click(object sender, EventArgs e)
        {
            string text = this.btnNotEqual.Text;
            string str = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                str = str.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = str.Insert(selectionStart, string.Concat(" ", text, " "));
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = selectionStart + text.Length + 2;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void btnOr_Click(object sender, EventArgs e)
        {
            string str = "OR";
            string text = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                text = text.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = text.Insert(selectionStart, string.Concat(" ", str, " "));
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = selectionStart + str.Length + 2;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        public void ClearWhereCaluse()
        {
            this.memEditWhereCaluse.Text = "";
        }


        private void Fieldlist_DoubleClick(object sender, EventArgs e)
        {
            string selectedItem = this.Fieldlist.SelectedItem as string;
            string text = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                text = text.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = text.Insert(selectionStart, string.Concat(" ", selectedItem, " "));
            try
            {
                this.memEditWhereCaluse.Focus();
            }
            catch
            {
            }
            this.memEditWhereCaluse.SelectionStart = selectionStart + selectedItem.Length + 1;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void Fieldlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.UniqueValuelist.Items.Clear();
            this.UniqueValuelist.Enabled = false;
        }

        public void GetUniqueValues(ITable itable_1, string string_2, ListBox.ObjectCollection listBoxItemCollection_0)
        {
            CommonHelper.GetUniqueValuesEx(itable_1, string_2, listBoxItemCollection_0);
        }


        private ITable GetQueryTable()
        {
            ITable displayTable;
            if (this._queryLayer == null)
            {
                displayTable = null;
            }
            else if (this._queryLayer is IDisplayTable)
            {
                displayTable = (this._queryLayer as IDisplayTable).DisplayTable;
            }
            else if (!(this._queryLayer is IAttributeTable))
            {
                displayTable = (!(this._queryLayer is IFeatureLayer)
                    ? this._queryLayer as ITable
                    : (this._queryLayer as IFeatureLayer).FeatureClass as ITable);
            }
            else
            {
                displayTable = (this._queryLayer as IAttributeTable).AttributeTable;
            }
            return displayTable;
        }

        private void ClearWhereClause(object sender, EventArgs e)
        {
            this.memEditWhereCaluse.Text = "";
        }

        private void InitForm()
        {
            this.textEdit1.Text = string.Concat("Select * From ", this._datasetName, " Where ");
            if (this._queryTable != null)
            {
                this.LoadTableInfo(this._queryTable, this.Fieldlist.Items);
                this.memEditWhereCaluse.Text = this._whereClause;
                try
                {
                    this.btnMatchOneChar.Text = "_";
                    this.btnMatchString.Text = "%";
                    IWorkspace workspace = (this._queryTable as IDataset).Workspace;
                    if (workspace.Type == esriWorkspaceType.esriLocalDatabaseWorkspace && workspace.PathName != null &&
                        Path.GetExtension(workspace.PathName).ToLower() == ".mdb")
                    {
                        this.btnMatchOneChar.Text = "?";
                        this.btnMatchString.Text = "*";
                    }
                }
                catch
                {
                }
                this.UniqueValuelist.Items.Clear();
                this.UniqueValuelist.Enabled = false;
            }
        }

        private void LoadTableInfo(ITable itable_1, ListBox.ObjectCollection listBoxItemCollection_0)
        {
            listBoxItemCollection_0.Clear();
            string str = "";
            string str1 = "";
            try
            {
                if ((itable_1 as IDataset).Workspace.Type == esriWorkspaceType.esriLocalDatabaseWorkspace)
                {
                    IWorkspace workspace = (itable_1 as IDataset).Workspace;
                    if (workspace.PathName != null && Path.GetExtension(workspace.PathName).ToLower() == ".mdb")
                    {
                        str = "[";
                        str1 = "]";
                    }
                }
                IFields fields = itable_1.Fields;
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    IField field = fields.Field[i];
                    if ((field.Type == esriFieldType.esriFieldTypeGeometry
                        ? false
                        : field.Type != esriFieldType.esriFieldTypeBlob))
                    {
                        listBoxItemCollection_0.Add(string.Concat(str, field.Name, str1));
                    }
                }
            }
            catch
            {
            }
        }

        private void UniqueValuelist_DoubleClick(object sender, EventArgs e)
        {
            string selectedItem = this.UniqueValuelist.SelectedItem as string;
            string text = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                text = text.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = text.Insert(selectionStart, string.Concat(" ", selectedItem, " "));
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = selectionStart + selectedItem.Length + 2;
            this.memEditWhereCaluse.SelectionLength = 0;
        }
    }
}