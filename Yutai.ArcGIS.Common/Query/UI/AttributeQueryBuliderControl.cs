using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Helpers;

namespace Yutai.ArcGIS.Common.Query.UI
{
    public partial class AttributeQueryBuliderControl : UserControl
    {
        private bool bool_0 = false;
        private Container container_0 = null;
        private ILayer ilayer_0 = null;
        private ITable itable_0 = null;
        private string string_0 = "";
        private string string_1 = "";

        public AttributeQueryBuliderControl()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.ilayer_0 != null)
            {
                this.string_0 = this.memEditWhereCaluse.Text;
            }
        }

        private void AttributeQueryBuliderControl_Load(object sender, EventArgs e)
        {
            this.method_2();
            this.bool_0 = true;
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
            this.memEditWhereCaluse.Text = text.Insert(selectionStart, " " + str + " ");
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = (selectionStart + str.Length) + 2;
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

        private void btnGetUniqueValue_Click(object sender, EventArgs e)
        {
            if ((this.itable_0 != null) && (this.Fieldlist.SelectedItem != null))
            {
                this.UniqueValuelist.Enabled = true;
                this.UniqueValuelist.Items.Clear();
                this.Fieldlist.SelectedItem.ToString();
                this.GetUniqueValues(this.itable_0, this.Fieldlist.SelectedItem.ToString(), this.UniqueValuelist.Items);
            }
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

        private void btnIs_Click(object sender, EventArgs e)
        {
            string str = "IS";
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

        private void btnLike_Click(object sender, EventArgs e)
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

        private void btnNot_Click(object sender, EventArgs e)
        {
            string str = "NOT";
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

        private void btnOr_Click(object sender, EventArgs e)
        {
            string str = "OR";
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
            this.memEditWhereCaluse.Text = text.Insert(selectionStart, " " + selectedItem + " ");
            try
            {
                this.memEditWhereCaluse.Focus();
            }
            catch
            {
            }
            this.memEditWhereCaluse.SelectionStart = (selectionStart + selectedItem.Length) + 1;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void Fieldlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.UniqueValuelist.Items.Clear();
            this.UniqueValuelist.Enabled = false;
        }

        public void GetUniqueValues(ITable itable_1, string string_2, ListBoxItemCollection listBoxItemCollection_0)
        {
            CommonHelper.GetUniqueValuesEx(itable_1, string_2, listBoxItemCollection_0);
        }

 private ITable method_0()
        {
            if (this.ilayer_0 == null)
            {
                return null;
            }
            if (this.ilayer_0 is IDisplayTable)
            {
                return (this.ilayer_0 as IDisplayTable).DisplayTable;
            }
            if (this.ilayer_0 is IAttributeTable)
            {
                return (this.ilayer_0 as IAttributeTable).AttributeTable;
            }
            if (this.ilayer_0 is IFeatureLayer)
            {
                return ((this.ilayer_0 as IFeatureLayer).FeatureClass as ITable);
            }
            return (this.ilayer_0 as ITable);
        }

        private void method_1(object sender, EventArgs e)
        {
            this.memEditWhereCaluse.Text = "";
        }

        private void method_2()
        {
            this.textEdit1.Text = "Select * From " + this.string_1 + " Where ";
            if (this.itable_0 != null)
            {
                this.method_3(this.itable_0, this.Fieldlist.Items);
                this.memEditWhereCaluse.Text = this.string_0;
                try
                {
                    this.btnMatchOneChar.Text = "_";
                    this.btnMatchString.Text = "%";
                    IWorkspace workspace = (this.itable_0 as IDataset).Workspace;
                    if (((workspace.Type == esriWorkspaceType.esriLocalDatabaseWorkspace) && (workspace.PathName != null)) && (Path.GetExtension(workspace.PathName).ToLower() == ".mdb"))
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

        private void method_3(ITable itable_1, ListBoxItemCollection listBoxItemCollection_0)
        {
            listBoxItemCollection_0.Clear();
            string str = "";
            string str2 = "";
            try
            {
                if ((itable_1 as IDataset).Workspace.Type == esriWorkspaceType.esriLocalDatabaseWorkspace)
                {
                    IWorkspace workspace = (itable_1 as IDataset).Workspace;
                    if ((workspace.PathName != null) && (Path.GetExtension(workspace.PathName).ToLower() == ".mdb"))
                    {
                        str = "[";
                        str2 = "]";
                    }
                }
                IFields fields = itable_1.Fields;
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    IField field = fields.get_Field(i);
                    if ((field.Type != esriFieldType.esriFieldTypeGeometry) && (field.Type != esriFieldType.esriFieldTypeBlob))
                    {
                        listBoxItemCollection_0.Add(str + field.Name + str2);
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
            this.memEditWhereCaluse.Text = text.Insert(selectionStart, " " + selectedItem + " ");
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = (selectionStart + selectedItem.Length) + 2;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        public ILayer CurrentLayer
        {
            set
            {
                this.ilayer_0 = value;
                this.itable_0 = this.method_0();
                this.string_1 = this.ilayer_0.Name;
                if (this.bool_0)
                {
                    this.method_2();
                }
            }
        }

        public ITable Table
        {
            set
            {
                this.itable_0 = value;
                this.string_1 = (this.itable_0 as IDataset).Name;
                if (this.bool_0)
                {
                    this.method_2();
                }
            }
        }

        public string WhereCaluse
        {
            get
            {
                return this.memEditWhereCaluse.Text;
            }
            set
            {
                this.string_0 = value;
            }
        }
    }
}

