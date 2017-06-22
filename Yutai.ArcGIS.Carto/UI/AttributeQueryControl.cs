using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class AttributeQueryControl : UserControl
    {
        private Container container_0 = null;
        private ILayer ilayer_0 = null;
        private string string_0 = "";

        public AttributeQueryControl()
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

        private void AttributeQueryControl_Load(object sender, EventArgs e)
        {
            this.method_1();
        }

        private void btnAnd_Click(object sender, EventArgs e)
        {
            string str = "AND";
            string text = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.Text = text.Insert(selectionStart, " " + str + " ");
            this.memEditWhereCaluse.SelectedText = "";
            this.memEditWhereCaluse.SelectionStart = (selectionStart + str.Length) + 2;
        }

        private void btnBracket_Click(object sender, EventArgs e)
        {
            string text = this.btnBracket.Text;
            string str2 = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.Text = str2.Insert(selectionStart, text);
            this.memEditWhereCaluse.SelectedText = "";
            this.memEditWhereCaluse.SelectionStart = (selectionStart + text.Length) + 1;
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            string text = this.btnEqual.Text;
            string str2 = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.Text = str2.Insert(selectionStart, " " + text + " ");
            this.memEditWhereCaluse.SelectedText = "";
            this.memEditWhereCaluse.SelectionStart = (selectionStart + text.Length) + 2;
        }

        private void btnGetUniqueValue_Click(object sender, EventArgs e)
        {
            if (this.Fieldlist.SelectedItem != null)
            {
                this.UniqueValuelist.Enabled = true;
                this.Fieldlist.SelectedItem.ToString();
                this.GetUniqueValues(this.ilayer_0, this.Fieldlist.SelectedItem.ToString(), this.UniqueValuelist.Items);
            }
        }

        private void btnGreat_Click(object sender, EventArgs e)
        {
            string text = this.btnGreat.Text;
            string str2 = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.Text = str2.Insert(selectionStart, " " + text + " ");
            this.memEditWhereCaluse.SelectedText = "";
            this.memEditWhereCaluse.SelectionStart = (selectionStart + text.Length) + 2;
        }

        private void btnGreatEqual_Click(object sender, EventArgs e)
        {
            string text = this.btnGreatEqual.Text;
            string str2 = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.Text = str2.Insert(selectionStart, " " + text + " ");
            this.memEditWhereCaluse.SelectedText = "";
            this.memEditWhereCaluse.SelectionStart = (selectionStart + text.Length) + 2;
        }

        private void btnIs_Click(object sender, EventArgs e)
        {
            string str = "IS";
            string text = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.Text = text.Insert(selectionStart, " " + str + " ");
            this.memEditWhereCaluse.SelectedText = "";
            this.memEditWhereCaluse.SelectionStart = (selectionStart + str.Length) + 2;
        }

        private void btnLike_Click(object sender, EventArgs e)
        {
            string str = "LIKE";
            string text = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.Text = text.Insert(selectionStart, " " + str + " ");
            this.memEditWhereCaluse.SelectedText = "";
            this.memEditWhereCaluse.SelectionStart = (selectionStart + str.Length) + 2;
        }

        private void btnLittle_Click(object sender, EventArgs e)
        {
            string text = this.btnLittle.Text;
            string str2 = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.Text = str2.Insert(selectionStart, " " + text + " ");
            this.memEditWhereCaluse.SelectedText = "";
            this.memEditWhereCaluse.SelectionStart = (selectionStart + text.Length) + 2;
        }

        private void btnLittleEqual_Click(object sender, EventArgs e)
        {
            string text = this.btnLittleEqual.Text;
            string str2 = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.Text = str2.Insert(selectionStart, " " + text + " ");
            this.memEditWhereCaluse.SelectedText = "";
            this.memEditWhereCaluse.SelectionStart = (selectionStart + text.Length) + 2;
        }

        private void btnMatchOneChar_Click(object sender, EventArgs e)
        {
            string text = this.btnMatchOneChar.Text;
            string str2 = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.Text = str2.Insert(selectionStart, text);
            this.memEditWhereCaluse.SelectedText = "";
            this.memEditWhereCaluse.SelectionStart = (selectionStart + text.Length) + 1;
        }

        private void btnMatchString_Click(object sender, EventArgs e)
        {
            string text = this.btnMatchString.Text;
            string str2 = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.Text = str2.Insert(selectionStart, text);
            this.memEditWhereCaluse.SelectedText = "";
            this.memEditWhereCaluse.SelectionStart = (selectionStart + text.Length) + 1;
        }

        private void btnNot_Click(object sender, EventArgs e)
        {
            string str = "NOT";
            string text = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.Text = text.Insert(selectionStart, " " + str + " ");
            this.memEditWhereCaluse.SelectedText = "";
            this.memEditWhereCaluse.SelectionStart = (selectionStart + str.Length) + 2;
        }

        private void btnNotEqual_Click(object sender, EventArgs e)
        {
            string text = this.btnNotEqual.Text;
            string str2 = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.Text = str2.Insert(selectionStart, " " + text + " ");
            this.memEditWhereCaluse.SelectedText = "";
            this.memEditWhereCaluse.SelectionStart = (selectionStart + text.Length) + 2;
        }

        private void btnOr_Click(object sender, EventArgs e)
        {
            string str = "OR";
            string text = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.Text = text.Insert(selectionStart, " " + str + " ");
            this.memEditWhereCaluse.SelectedText = "";
            this.memEditWhereCaluse.SelectionStart = (selectionStart + str.Length) + 2;
        }

        public void ClearWhereCaluse()
        {
            this.memEditWhereCaluse.Text = "";
        }

        public string ConvertFieldValueToString(esriFieldType esriFieldType_0, object object_0)
        {
            switch (esriFieldType_0)
            {
                case esriFieldType.esriFieldTypeInteger:
                case esriFieldType.esriFieldTypeSingle:
                case esriFieldType.esriFieldTypeDouble:
                case esriFieldType.esriFieldTypeOID:
                    return object_0.ToString();

                case esriFieldType.esriFieldTypeString:
                    return ("'" + object_0.ToString() + "'");

                case esriFieldType.esriFieldTypeDate:
                    return object_0.ToString();

                case esriFieldType.esriFieldTypeGeometry:
                    return "几何数据";

                case esriFieldType.esriFieldTypeBlob:
                    return "长二进制串";
            }
            return "";
        }

 private void Fieldlist_DoubleClick(object sender, EventArgs e)
        {
            string selectedItem = this.Fieldlist.SelectedItem as string;
            string text = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.Text = text.Insert(selectionStart, " " + selectedItem + " ");
            this.memEditWhereCaluse.SelectedText = "";
            this.memEditWhereCaluse.SelectionStart = (selectionStart + selectedItem.Length) + 2;
        }

        private void Fieldlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.UniqueValuelist.Items.Clear();
            this.UniqueValuelist.Enabled = false;
        }

        public void GetUniqueValues(ILayer ilayer_1, string string_1, ListBoxItemCollection listBoxItemCollection_0)
        {
            try
            {
                string str;
                ITable table = this.method_2();
                if ((string_1[0] == '\'') || (string_1[0] == '['))
                {
                    str = string_1.Substring(1, string_1.Length - 2);
                }
                else
                {
                    str = string_1;
                }
                ICursor cursor = table.Search(null, false);
                IDataStatistics statistics = new DataStatisticsClass {
                    Field = str,
                    Cursor = cursor
                };
                IEnumerator uniqueValues = statistics.UniqueValues;
                uniqueValues.Reset();
                int index = cursor.Fields.FindField(str);
                esriFieldType type = cursor.Fields.get_Field(index).Type;
                while (uniqueValues.MoveNext())
                {
                    listBoxItemCollection_0.Add(this.ConvertFieldValueToString(type, uniqueValues.Current));
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

 private void method_0(object sender, EventArgs e)
        {
            this.memEditWhereCaluse.Text = "";
        }

        private void method_1()
        {
            if (this.ilayer_0 != null)
            {
                this.textEdit1.Text = "Select * From " + this.ilayer_0.Name + " Where ";
                this.method_3(this.ilayer_0, this.Fieldlist.Items);
                this.memEditWhereCaluse.Text = this.string_0;
                if (((this.ilayer_0 as IFeatureLayer).FeatureClass as IDataset).Workspace.Type == esriWorkspaceType.esriLocalDatabaseWorkspace)
                {
                    this.btnMatchOneChar.Text = "?";
                    this.btnMatchString.Text = "*";
                }
                else
                {
                    this.btnMatchOneChar.Text = "_";
                    this.btnMatchString.Text = "%";
                }
                this.UniqueValuelist.Items.Clear();
                this.UniqueValuelist.Enabled = false;
            }
        }

        private ITable method_2()
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

        private void method_3(ILayer ilayer_1, ListBoxItemCollection listBoxItemCollection_0)
        {
            listBoxItemCollection_0.Clear();
            ITable table = this.method_2();
            if (table != null)
            {
                string str = "";
                string str2 = "";
                if ((table as IDataset).Workspace.Type == esriWorkspaceType.esriLocalDatabaseWorkspace)
                {
                    str = "[";
                    str2 = "]";
                }
                IFields fields = table.Fields;
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    IField field = fields.get_Field(i);
                    if ((field.Type != esriFieldType.esriFieldTypeGeometry) && (field.Type != esriFieldType.esriFieldTypeBlob))
                    {
                        listBoxItemCollection_0.Add(str + field.Name + str2);
                    }
                }
            }
        }

        private ILayer method_4(IBasicMap ibasicMap_0, string string_1)
        {
            ILayer layer = null;
            for (int i = 0; i < ibasicMap_0.LayerCount; i++)
            {
                layer = ibasicMap_0.get_Layer(i);
                if (layer.Name == string_1)
                {
                    return layer;
                }
            }
            return null;
        }

        private void UniqueValuelist_DoubleClick(object sender, EventArgs e)
        {
            string selectedItem = this.UniqueValuelist.SelectedItem as string;
            string text = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.Text = text.Insert(selectionStart, " " + selectedItem + " ");
            this.memEditWhereCaluse.SelectedText = "";
            this.memEditWhereCaluse.SelectionStart = (selectionStart + selectedItem.Length) + 2;
        }

        public ILayer CurrentLayer
        {
            set
            {
                this.ilayer_0 = value;
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

