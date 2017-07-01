using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Shared;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class frmExpressionBuild : Form
    {
        private bool bool_0 = false;
        private Container container_0 = null;
        private ILayer ilayer_0 = null;
        private ITable itable_0 = null;
        private string string_0 = "";
        private string string_1 = "";

        public frmExpressionBuild()
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

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.string_0 = this.memEditWhereCaluse.Text;
            base.DialogResult = DialogResult.OK;
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
        }

        private void frmExpressionBuild_Load(object sender, EventArgs e)
        {
            this.method_17();
            this.memEditWhereCaluse.Text = this.string_0;
            this.bool_0 = true;
        }

        public void GetUniqueValues(ITable itable_1, string string_2, ListBoxItemCollection listBoxItemCollection_0)
        {
            try
            {
                string str;
                if ((string_2[0] == '\'') || (string_2[0] == '['))
                {
                    str = string_2.Substring(1, string_2.Length - 2);
                }
                else
                {
                    str = string_2;
                }
                IQueryFilter queryFilter = new QueryFilterClass
                {
                    WhereClause = "1=1"
                };
                (queryFilter as IQueryFilterDefinition).PostfixClause = "Order by " + str;
                ICursor cursor = itable_1.Search(queryFilter, false);
                IDataStatistics statistics = new DataStatisticsClass
                {
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
                Logger.Current.Error("", exception, "");
            }
        }

        private void lstFunction_DoubleClick(object sender, EventArgs e)
        {
            string selectedItem = this.lstFunction.SelectedItem as string;
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

        private void method_0(object sender, EventArgs e)
        {
            string str = "";
            string text = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                text = text.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = text.Insert(selectionStart, str);
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = (selectionStart + str.Length) + 1;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private ITable method_1()
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

        private void method_10(object sender, EventArgs e)
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

        private void method_11(object sender, EventArgs e)
        {
            string str = "";
            string text = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            this.memEditWhereCaluse.Focus();
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                text = text.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = text.Insert(selectionStart, " " + str + " ");
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = (selectionStart + str.Length) + 2;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void method_12(object sender, EventArgs e)
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

        private void method_13(object sender, EventArgs e)
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

        private void method_14(object sender, EventArgs e)
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

        private void method_15(object sender, EventArgs e)
        {
            if ((this.itable_0 != null) && (this.Fieldlist.SelectedItem != null))
            {
                this.lstFunction.Enabled = true;
                this.lstFunction.Items.Clear();
                this.Fieldlist.SelectedItem.ToString();
                this.GetUniqueValues(this.itable_0, this.Fieldlist.SelectedItem.ToString(), this.lstFunction.Items);
            }
        }

        private void method_16(object sender, EventArgs e)
        {
            this.memEditWhereCaluse.Text = "";
        }

        private void method_17()
        {
            if (this.itable_0 != null)
            {
                this.method_18(this.itable_0, this.Fieldlist.Items);
            }
        }

        private void method_18(ITable itable_1, ListBoxItemCollection listBoxItemCollection_0)
        {
            listBoxItemCollection_0.Clear();
            string str = "";
            string str2 = "";
            try
            {
                if ((itable_1 as IDataset).Workspace.Type == esriWorkspaceType.esriLocalDatabaseWorkspace)
                {
                    str = "[";
                    str2 = "]";
                }
                IFields fields = itable_1.Fields;
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    IField field = fields.get_Field(i);
                    if ((((field.Type == esriFieldType.esriFieldTypeInteger) ||
                          (field.Type == esriFieldType.esriFieldTypeSingle)) ||
                         (field.Type == esriFieldType.esriFieldTypeDouble)) ||
                        (field.Type == esriFieldType.esriFieldTypeSmallInteger))
                    {
                        listBoxItemCollection_0.Add(str + field.Name + str2);
                    }
                }
            }
            catch
            {
            }
        }

        private ILayer method_19(IBasicMap ibasicMap_0, string string_2)
        {
            ILayer layer = null;
            for (int i = 0; i < ibasicMap_0.LayerCount; i++)
            {
                layer = ibasicMap_0.get_Layer(i);
                if (layer.Name == string_2)
                {
                    return layer;
                }
            }
            return null;
        }

        private void method_2(object sender, EventArgs e)
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

        private void method_3(object sender, EventArgs e)
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

        private void method_4(object sender, EventArgs e)
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

        private void method_5(object sender, EventArgs e)
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

        private void method_6(object sender, EventArgs e)
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

        private void method_7(object sender, EventArgs e)
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

        private void method_8(object sender, EventArgs e)
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

        private void method_9(object sender, EventArgs e)
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

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            string text = (sender as SimpleButton).Text;
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

        public ILayer CurrentLayer
        {
            set
            {
                this.ilayer_0 = value;
                this.itable_0 = this.method_1();
                this.string_1 = this.ilayer_0.Name;
                if (this.bool_0)
                {
                    this.method_17();
                }
            }
        }

        public string Expression
        {
            get { return this.string_0; }
            set { this.string_0 = value; }
        }

        public ITable Table
        {
            set
            {
                this.itable_0 = value;
                this.string_1 = (this.itable_0 as IDataset).Name;
            }
        }
    }
}