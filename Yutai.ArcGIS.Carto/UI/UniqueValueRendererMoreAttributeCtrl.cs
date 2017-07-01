using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.Shared;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class UniqueValueRendererMoreAttributeCtrl : UserControl, IUserControl
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private IColorRamp icolorRamp_0 = null;
        private IEnumColors ienumColors_0 = null;
        private IFields ifields_0 = null;
        private ILayer ilayer_0 = null;
        private IList ilist_0 = new ArrayList();
        private IList ilist_1 = new ArrayList();
        private int int_0 = 10;
        private IUniqueValueRenderer iuniqueValueRenderer_0 = null;
        public IStyleGallery m_pSG = null;

        public UniqueValueRendererMoreAttributeCtrl()
        {
            this.InitializeComponent();
            this.listView1.OnValueChanged += new RenderInfoListView.OnValueChangedHandler(this.method_8);
            this.listView1.SetColumnEditable(2, true);
        }

        public void Apply()
        {
            this.iuniqueValueRenderer_0.RemoveAllValues();
            for (int i = 0; i < this.listView1.Items.Count; i++)
            {
                ListViewItemEx ex = this.listView1.Items[i] as ListViewItemEx;
                if (ex != null)
                {
                    string[] strArray = ex.SubItems[1].Text.Split(new char[] {';'});
                    this.iuniqueValueRenderer_0.AddValue(strArray[0], null, ex.Style as ISymbol);
                    this.iuniqueValueRenderer_0.set_Label(strArray[0], ex.SubItems[2].Text);
                    for (int j = 1; j < strArray.Length; j++)
                    {
                        this.iuniqueValueRenderer_0.AddValue(strArray[j], null, ex.Style as ISymbol);
                        this.iuniqueValueRenderer_0.set_Label(strArray[j], ex.SubItems[2].Text);
                        this.iuniqueValueRenderer_0.AddReferenceValue(strArray[j], strArray[0]);
                    }
                }
            }
            IObjectCopy copy = new ObjectCopyClass();
            IUniqueValueRenderer renderer = copy.Copy(this.iuniqueValueRenderer_0) as IUniqueValueRenderer;
            (this.ilayer_0 as IGeoFeatureLayer).Renderer = renderer as IFeatureRenderer;
        }

        private void btnAddAllValues_Click(object sender, EventArgs e)
        {
            this.listView1.Items.Clear();
            this.ilist_0.Clear();
            this.ilist_1.Clear();
            this.iuniqueValueRenderer_0.RemoveAllValues();
            int num = 1;
            if (this.cboFields2.SelectedIndex > 0)
            {
                num++;
            }
            if (this.cboFields3.SelectedIndex > 0)
            {
                num++;
            }
            string[] strArray = new string[num];
            int index = 0;
            strArray[0] = (this.cboFields1.SelectedItem as FieldWrap).Name;
            index = 0 + 1;
            if (this.cboFields2.SelectedIndex > 0)
            {
                strArray[index] = (this.cboFields2.SelectedItem as FieldWrap).Name;
                index++;
            }
            if (this.cboFields3.SelectedIndex > 0)
            {
                strArray[index] = (this.cboFields3.SelectedItem as FieldWrap).Name;
            }
            if (this.GetUniqueValues(this.ilayer_0, strArray, this.ilist_0, this.ilist_1))
            {
                object[] objArray = new object[4];
                ISymbol defaultSymbol = this.iuniqueValueRenderer_0.DefaultSymbol;
                if (defaultSymbol == null)
                {
                    defaultSymbol = this.method_6((this.ilayer_0 as IGeoFeatureLayer).FeatureClass.ShapeType);
                }
                int num3 = 0;
                while (true)
                {
                    if (num3 >= this.ilist_0.Count)
                    {
                        break;
                    }
                    try
                    {
                        ISymbol symbol2 = (defaultSymbol as IClone).Clone() as ISymbol;
                        IColor color = this.ienumColors_0.Next();
                        if (color == null)
                        {
                            this.ienumColors_0.Reset();
                            color = this.ienumColors_0.Next();
                        }
                        this.method_5(symbol2, color);
                        objArray[0] = symbol2;
                        objArray[1] = this.ilist_0[num3].ToString();
                        objArray[2] = this.ilist_0[num3].ToString();
                        objArray[3] = this.ilist_1[num3].ToString();
                        this.iuniqueValueRenderer_0.AddValue(objArray[1].ToString(), null, symbol2);
                        this.listView1.Add(objArray);
                    }
                    catch
                    {
                    }
                    num3++;
                }
                this.ilist_0.Clear();
            }
        }

        private void btnAddValue_Click(object sender, EventArgs e)
        {
            frmAddValuesEx ex = new frmAddValuesEx
            {
                Layer = this.ilayer_0,
                FieldNames = this.method_4(),
                FieldDelimiter = this.iuniqueValueRenderer_0.FieldDelimiter,
                List = this.ilist_0 as ArrayList,
                UniqueValueRenderer = this.iuniqueValueRenderer_0,
                CountList = this.ilist_1 as ArrayList,
                GetAllValues = this.bool_1
            };
            if (ex.ShowDialog() == DialogResult.OK)
            {
                ISymbol defaultSymbol = this.iuniqueValueRenderer_0.DefaultSymbol;
                if (defaultSymbol == null)
                {
                    defaultSymbol = this.method_6((this.ilayer_0 as IGeoFeatureLayer).FeatureClass.ShapeType);
                }
                object[] objArray = new object[4];
                for (int i = 0; i < ex.SelectedItems.Count; i++)
                {
                    ISymbol symbol2 = (defaultSymbol as IClone).Clone() as ISymbol;
                    IColor color = this.ienumColors_0.Next();
                    if (color == null)
                    {
                        this.ienumColors_0.Reset();
                        color = this.ienumColors_0.Next();
                    }
                    this.method_5(symbol2, color);
                    objArray[0] = symbol2;
                    objArray[1] = ex.SelectedItems[i].ToString();
                    objArray[2] = ex.SelectedItems[i].ToString();
                    objArray[3] = "?";
                    this.iuniqueValueRenderer_0.AddValue(objArray[1].ToString(), null, symbol2);
                    this.listView1.Add(objArray);
                }
            }
            this.bool_1 = ex.GetAllValues;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int index = 0;
            for (int i = this.listView1.SelectedIndices.Count - 1; i >= 0; i--)
            {
                index = this.listView1.SelectedIndices[i];
                this.listView1.Items.RemoveAt(index);
                string str = this.iuniqueValueRenderer_0.get_Value(index);
                this.iuniqueValueRenderer_0.RemoveValue(str);
                this.ilist_0.Add(str);
            }
            index = this.listView1.SelectedIndices.Count;
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            this.listView1.Items.Clear();
            this.iuniqueValueRenderer_0.RemoveAllValues();
            this.ilist_0.Clear();
            this.bool_1 = false;
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            int index = this.listView1.SelectedIndices[0];
            ListViewItem item = this.listView1.Items[index];
            this.listView1.Items.RemoveAt(index);
            index++;
            if (index == this.listView1.Items.Count)
            {
                this.listView1.Items.Add(item);
            }
            else
            {
                this.listView1.Items.Insert(index, item);
            }
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            int index = this.listView1.SelectedIndices[0];
            ListViewItem item = this.listView1.Items[index];
            this.listView1.Items.RemoveAt(index);
            index--;
            this.listView1.Items.Insert(index, item);
        }

        private void cboColorRamp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.iuniqueValueRenderer_0.ColorScheme = this.cboColorRamp.Text;
                this.icolorRamp_0 = this.cboColorRamp.GetSelectStyleGalleryItem().Item as IColorRamp;
                if (this.icolorRamp_0 != null)
                {
                    bool flag;
                    this.icolorRamp_0.Size = this.int_0;
                    this.icolorRamp_0.CreateRamp(out flag);
                    this.ienumColors_0 = this.icolorRamp_0.Colors;
                    this.ienumColors_0.Reset();
                    IColor color = null;
                    for (int i = 0; i < this.iuniqueValueRenderer_0.ValueCount; i++)
                    {
                        ISymbol symbol = this.iuniqueValueRenderer_0.get_Symbol(this.iuniqueValueRenderer_0.get_Value(i));
                        color = this.ienumColors_0.Next();
                        if (color == null)
                        {
                            this.ienumColors_0.Reset();
                            color = this.ienumColors_0.Next();
                        }
                        this.method_5(symbol, color);
                        this.iuniqueValueRenderer_0.set_Symbol(this.iuniqueValueRenderer_0.get_Value(i), symbol);
                        this.listView1.Items[i].Tag = symbol;
                    }
                    this.listView1.Invalidate();
                }
            }
        }

        private void cboFields1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.listView1.Items.Clear();
                this.ilist_0.Clear();
                this.ilist_1.Clear();
                this.bool_1 = false;
                this.iuniqueValueRenderer_0.RemoveAllValues();
                this.method_7();
            }
        }

        private void cboFields2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.listView1.Items.Clear();
                this.ilist_0.Clear();
                this.ilist_1.Clear();
                this.bool_1 = false;
                this.iuniqueValueRenderer_0.RemoveAllValues();
                this.method_7();
            }
        }

        private void cboFields3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.listView1.Items.Clear();
                this.ilist_0.Clear();
                this.ilist_1.Clear();
                this.bool_1 = false;
                this.iuniqueValueRenderer_0.RemoveAllValues();
                this.method_7();
            }
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
            return object_0.ToString();
        }

        public bool GetUniqueValues(ILayer ilayer_1, string[] string_0, IList ilist_2, IList ilist_3)
        {
            try
            {
                ITable displayTable = null;
                if (ilayer_1 is IDisplayTable)
                {
                    displayTable = (ilayer_1 as IDisplayTable).DisplayTable;
                }
                else
                {
                    IAttributeTable table2 = ilayer_1 as IAttributeTable;
                    if (table2 != null)
                    {
                        displayTable = table2.AttributeTable;
                    }
                    else
                    {
                        displayTable = ilayer_1 as ITable;
                    }
                }
                if (displayTable == null)
                {
                    return false;
                }
                int[] numArray = new int[string_0.Length];
                int index = 0;
                while (index < string_0.Length)
                {
                    numArray[index] = displayTable.Fields.FindField(string_0[index]);
                    index++;
                }
                ICursor cursor = displayTable.Search(null, false);
                IRow row = cursor.NextRow();
                ilist_2.Clear();
                string fieldDelimiter = this.iuniqueValueRenderer_0.FieldDelimiter;
                while (row != null)
                {
                    string str2 = "";
                    for (index = 0; index < numArray.Length; index++)
                    {
                        object obj2 = row.get_Value(numArray[index]);
                        if (obj2 is DBNull)
                        {
                            if (index == 0)
                            {
                                str2 = "";
                            }
                            else
                            {
                                str2 = str2 + fieldDelimiter;
                            }
                        }
                        else if (index == 0)
                        {
                            str2 = obj2.ToString();
                        }
                        else
                        {
                            str2 = str2 + fieldDelimiter + obj2.ToString();
                        }
                    }
                    int num2 = ilist_2.IndexOf(str2);
                    if (num2 != -1)
                    {
                        int num3 = (int) ilist_3[num2];
                        num3++;
                        ilist_3[num2] = num3;
                    }
                    else
                    {
                        ilist_2.Add(str2);
                        ilist_3.Add(1);
                    }
                    row = cursor.NextRow();
                }
                this.bool_1 = true;
                (ilist_2 as ArrayList).Sort();
                return true;
            }
            catch (Exception exception)
            {
                Logger.Current.Error("", exception, "");
            }
            return false;
        }

        private void listView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.menuitemUnGroup.Enabled = false;
                this.menuitemGroup.Enabled = false;
                if (this.listView1.SelectedItems.Count > 1)
                {
                    this.menuitemGroup.Enabled = true;
                }
                else if (this.listView1.SelectedItems.Count == 1)
                {
                    ListViewItem item = this.listView1.SelectedItems[0];
                    if (item.SubItems[1].Text.IndexOf(";") != -1)
                    {
                        this.menuitemUnGroup.Enabled = true;
                    }
                }
                this.contextMenuStrip1.Show(this.listView1, e.Location);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView1.SelectedIndices.Count > 0)
            {
                this.btnDelete.Enabled = true;
                if (this.listView1.SelectedIndices.Count == 1)
                {
                    if (this.listView1.SelectedIndices[0] == 0)
                    {
                        this.btnMoveUp.Enabled = false;
                        if (this.listView1.Items.Count == 1)
                        {
                            this.btnMoveDown.Enabled = false;
                        }
                        else
                        {
                            this.btnMoveDown.Enabled = true;
                        }
                    }
                    else if (this.listView1.SelectedIndices[0] == (this.listView1.Items.Count - 1))
                    {
                        this.btnMoveUp.Enabled = true;
                        this.btnMoveDown.Enabled = false;
                    }
                }
                else
                {
                    this.btnMoveDown.Enabled = false;
                    this.btnMoveUp.Enabled = false;
                }
            }
            else
            {
                this.btnDelete.Enabled = false;
                this.btnMoveDown.Enabled = false;
                this.btnMoveUp.Enabled = false;
            }
        }

        private void menuitemGroup_Click(object sender, EventArgs e)
        {
            ListViewItemEx ex = this.listView1.SelectedItems[0] as ListViewItemEx;
            string text = ex.SubItems[1].Text;
            text.Split(new char[] {','});
            int num = -1;
            if (ex.SubItems[3].Text != "?")
            {
                num = Convert.ToInt32(ex.SubItems[3].Text);
            }
            List<ListViewItemEx> list = new List<ListViewItemEx>();
            for (int i = 1; i < this.listView1.SelectedItems.Count; i++)
            {
                ListViewItemEx item = this.listView1.SelectedItems[i] as ListViewItemEx;
                string str2 = item.SubItems[1].Text;
                if (num != -1)
                {
                    if (ex.SubItems[3].Text != "?")
                    {
                        num += Convert.ToInt32(item.SubItems[3].Text);
                    }
                    else
                    {
                        num = -1;
                    }
                }
                text = text + ";" + str2;
                list.Add(item);
            }
            ex.SubItems[1].Text = text;
            if (num == -1)
            {
                ex.SubItems[3].Text = "?";
            }
            else
            {
                ex.SubItems[3].Text = num.ToString();
            }
            foreach (ListViewItemEx ex3 in list)
            {
                this.listView1.Items.Remove(ex3);
            }
        }

        private void menuitemUnGroup_Click(object sender, EventArgs e)
        {
            try
            {
                ListViewItemEx ex = this.listView1.SelectedItems[0] as ListViewItemEx;
                string[] strArray = ex.SubItems[1].Text.Split(new char[] {';'});
                if (ex.SubItems[3].Text != "?")
                {
                    ex.SubItems[3].Text = "?";
                }
                ISymbol symbol = this.iuniqueValueRenderer_0.get_Symbol(strArray[0]);
                ex.SubItems[1].Text = strArray[0];
                ex.SubItems[2].Text = strArray[0];
                object[] objArray = new object[4];
                for (int i = 1; i < strArray.Length; i++)
                {
                    string str2 = strArray[i];
                    objArray[0] = symbol;
                    objArray[1] = str2;
                    objArray[2] = str2;
                    objArray[3] = "?";
                    this.listView1.Add(objArray);
                }
            }
            catch (Exception)
            {
            }
        }

        private void method_0()
        {
            if (this.ilayer_0 == null)
            {
                this.iuniqueValueRenderer_0 = null;
            }
            else
            {
                IAttributeTable table = this.ilayer_0 as IAttributeTable;
                if (table == null)
                {
                    this.int_0 = 50;
                }
                else
                {
                    int num = table.AttributeTable.RowCount(null);
                    if (num > 100)
                    {
                        this.int_0 = 100;
                    }
                    else
                    {
                        this.int_0 = (int) num;
                    }
                }
                if (this.ilayer_0 is IGeoFeatureLayer)
                {
                    IUniqueValueRenderer pInObject =
                        (this.ilayer_0 as IGeoFeatureLayer).Renderer as IUniqueValueRenderer;
                    if (pInObject == null)
                    {
                        if (this.iuniqueValueRenderer_0 == null)
                        {
                            this.iuniqueValueRenderer_0 = new UniqueValueRendererClass();
                            this.iuniqueValueRenderer_0.FieldCount = 1;
                            this.iuniqueValueRenderer_0.DefaultLabel = "默认符号";
                            this.iuniqueValueRenderer_0.DefaultSymbol =
                                this.method_6((this.ilayer_0 as IGeoFeatureLayer).FeatureClass.ShapeType);
                            this.iuniqueValueRenderer_0.UseDefaultSymbol = true;
                            this.iuniqueValueRenderer_0.FieldDelimiter = ",";
                        }
                    }
                    else
                    {
                        IObjectCopy copy = new ObjectCopyClass();
                        this.iuniqueValueRenderer_0 = copy.Copy(pInObject) as IUniqueValueRenderer;
                        if (this.iuniqueValueRenderer_0.FieldDelimiter == "")
                        {
                            this.iuniqueValueRenderer_0.FieldDelimiter = ",";
                        }
                    }
                }
            }
        }

        private void method_1()
        {
            this.cboFields1.Properties.Items.Clear();
            this.listView1.Items.Clear();
            this.cboFields2.Properties.Items.Clear();
            this.cboFields2.Properties.Items.Add("<无>");
            this.cboFields3.Properties.Items.Clear();
            this.cboFields3.Properties.Items.Add("<无>");
            this.btnDelete.Enabled = false;
            this.ifields_0 = null;
            if (this.ilayer_0 != null)
            {
                if (this.ilayer_0 is IDisplayTable)
                {
                    this.ifields_0 = (this.ilayer_0 as IDisplayTable).DisplayTable.Fields;
                }
                else if (this.ilayer_0 is IGeoFeatureLayer)
                {
                    if ((this.ilayer_0 as IGeoFeatureLayer).FeatureClass == null)
                    {
                        return;
                    }
                    this.ifields_0 = (this.ilayer_0 as IGeoFeatureLayer).FeatureClass.Fields;
                }
                else
                {
                    this.ifields_0 = (this.ilayer_0 as IAttributeTable).AttributeTable.Fields;
                }
                int index = 0;
                while (index < this.ifields_0.FieldCount)
                {
                    IField field = this.ifields_0.get_Field(index);
                    if ((((field.Type != esriFieldType.esriFieldTypeOID) &&
                          (field.Type != esriFieldType.esriFieldTypeGeometry)) &&
                         (field.Type != esriFieldType.esriFieldTypeBlob)) &&
                        (field.Type != esriFieldType.esriFieldTypeRaster))
                    {
                        this.cboFields1.Properties.Items.Add(new FieldWrap(field));
                        this.cboFields2.Properties.Items.Add(new FieldWrap(field));
                        this.cboFields3.Properties.Items.Add(new FieldWrap(field));
                    }
                    index++;
                }
                string str = this.iuniqueValueRenderer_0.get_Field(0);
                if (str.Length == 0)
                {
                    if (this.cboFields1.Properties.Items.Count > 0)
                    {
                        this.cboFields1.SelectedIndex = 0;
                        this.iuniqueValueRenderer_0.set_Field(0, (this.cboFields1.SelectedItem as FieldWrap).Name);
                        esriFieldType type = (this.cboFields1.SelectedItem as FieldWrap).Type;
                        this.iuniqueValueRenderer_0.set_FieldType(0, type == esriFieldType.esriFieldTypeString);
                    }
                }
                else
                {
                    index = 0;
                    while (index < this.cboFields1.Properties.Items.Count)
                    {
                        if ((this.cboFields1.Properties.Items[index] as FieldWrap).Name == str)
                        {
                            this.cboFields1.SelectedIndex = index;
                            break;
                        }
                        index++;
                    }
                }
                this.cboFields2.SelectedIndex = 0;
                this.cboFields3.SelectedIndex = 0;
                if (this.iuniqueValueRenderer_0.FieldCount > 1)
                {
                    str = this.iuniqueValueRenderer_0.get_Field(1);
                    if (str.Length > 0)
                    {
                        index = 1;
                        while (index < this.cboFields2.Properties.Items.Count)
                        {
                            if ((this.cboFields2.Properties.Items[index] as FieldWrap).Name == str)
                            {
                                this.cboFields2.SelectedIndex = index;
                                break;
                            }
                            index++;
                        }
                    }
                }
                if (this.iuniqueValueRenderer_0.FieldCount > 2)
                {
                    str = this.iuniqueValueRenderer_0.get_Field(2);
                    if (str.Length > 0)
                    {
                        for (index = 1; index < this.cboFields3.Properties.Items.Count; index++)
                        {
                            if ((this.cboFields3.Properties.Items[index] as FieldWrap).Name == str)
                            {
                                this.cboFields3.SelectedIndex = index;
                                break;
                            }
                        }
                    }
                }
                if (this.icolorRamp_0 != null)
                {
                    try
                    {
                        bool flag;
                        this.icolorRamp_0.Size = (this.int_0 == 0) ? 10 : this.int_0;
                        this.icolorRamp_0.CreateRamp(out flag);
                        this.ienumColors_0 = this.icolorRamp_0.Colors;
                        this.ienumColors_0.Reset();
                    }
                    catch
                    {
                    }
                }
                object[] objArray = new object[4];
                SortedList<string, ListViewItemEx> list = new SortedList<string, ListViewItemEx>();
                index = 0;
                while (true)
                {
                    ListViewItemEx ex;
                    if (index >= this.iuniqueValueRenderer_0.ValueCount)
                    {
                        break;
                    }
                    string str2 = this.iuniqueValueRenderer_0.get_Value(index);
                    string key = "";
                    try
                    {
                        key = this.iuniqueValueRenderer_0.get_ReferenceValue(str2);
                        if (list.ContainsKey(key))
                        {
                            ex = list[key];
                            ListViewItem.ListViewSubItem item1 = ex.SubItems[1];
                            item1.Text = item1.Text + ";" + str2;
                        }
                    }
                    catch (Exception)
                    {
                        objArray[0] = this.iuniqueValueRenderer_0.get_Symbol(str2);
                        objArray[1] = str2;
                        objArray[2] = this.iuniqueValueRenderer_0.get_Label(str2);
                        objArray[3] = "?";
                        ex = this.listView1.Add(objArray);
                        list.Add(str2, ex);
                    }
                    index++;
                }
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

        private int method_3(ITable itable_0, string string_0, string string_1)
        {
            IFields fields = itable_0.Fields;
            IField field = fields.get_Field(fields.FindField(string_0));
            IQueryFilter queryFilter = new QueryFilterClass
            {
                WhereClause = field.Name + " = " + this.ConvertFieldValueToString(field.Type, string_1)
            };
            return itable_0.RowCount(queryFilter);
        }

        private string[] method_4()
        {
            int num = 1;
            if (this.cboFields2.SelectedIndex > 0)
            {
                num++;
            }
            if (this.cboFields3.SelectedIndex > 0)
            {
                num++;
            }
            string[] strArray = new string[num];
            int index = 0;
            strArray[0] = this.cboFields1.Text;
            index = 0 + 1;
            if (this.cboFields2.SelectedIndex > 0)
            {
                strArray[index] = this.cboFields2.Text;
                index++;
            }
            if (this.cboFields3.SelectedIndex > 0)
            {
                strArray[index] = this.cboFields3.Text;
            }
            return strArray;
        }

        private void method_5(ISymbol isymbol_0, IColor icolor_0)
        {
            if (isymbol_0 is IMarkerSymbol)
            {
                (isymbol_0 as IMarkerSymbol).Color = icolor_0;
            }
            else if (isymbol_0 is ILineSymbol)
            {
                (isymbol_0 as ILineSymbol).Color = icolor_0;
            }
            else if (isymbol_0 is IFillSymbol)
            {
                (isymbol_0 as IFillSymbol).Color = icolor_0;
            }
        }

        private ISymbol method_6(esriGeometryType esriGeometryType_0)
        {
            switch (esriGeometryType_0)
            {
                case esriGeometryType.esriGeometryPoint:
                case esriGeometryType.esriGeometryMultipoint:
                {
                    IMarkerSymbol symbol = new SimpleMarkerSymbolClass
                    {
                        Size = 3.0
                    };
                    return (symbol as ISymbol);
                }
                case esriGeometryType.esriGeometryPolyline:
                {
                    ILineSymbol symbol2 = new SimpleLineSymbolClass
                    {
                        Width = 0.2
                    };
                    return (symbol2 as ISymbol);
                }
                case esriGeometryType.esriGeometryPolygon:
                    return new SimpleFillSymbolClass();
            }
            return null;
        }

        private void method_7()
        {
            int num = 1;
            if (this.cboFields2.SelectedIndex > 0)
            {
                num++;
            }
            if (this.cboFields3.SelectedIndex > 0)
            {
                num++;
            }
            this.iuniqueValueRenderer_0.FieldCount = num;
            if (this.cboFields1.SelectedIndex >= 0)
            {
                this.iuniqueValueRenderer_0.set_Field(0, (this.cboFields1.SelectedItem as FieldWrap).Name);
                esriFieldType type = (this.cboFields1.SelectedItem as FieldWrap).Type;
                this.iuniqueValueRenderer_0.set_FieldType(0, type == esriFieldType.esriFieldTypeString);
                int index = 1;
                if (this.cboFields2.SelectedIndex > 0)
                {
                    this.iuniqueValueRenderer_0.set_Field(index, (this.cboFields2.SelectedItem as FieldWrap).Name);
                    type = (this.cboFields1.SelectedItem as FieldWrap).Type;
                    this.iuniqueValueRenderer_0.set_FieldType(index, type == esriFieldType.esriFieldTypeString);
                    index++;
                }
                if (this.cboFields3.SelectedIndex > 0)
                {
                    this.iuniqueValueRenderer_0.set_Field(index, (this.cboFields3.SelectedItem as FieldWrap).Name);
                    type = (this.cboFields1.SelectedItem as FieldWrap).Type;
                    this.iuniqueValueRenderer_0.set_FieldType(index, type == esriFieldType.esriFieldTypeString);
                }
            }
        }

        private void method_8(int int_1, object object_0)
        {
            ListViewItem item = this.listView1.Items[int_1];
            string text = item.SubItems[1].Text;
            if (object_0 is ISymbol)
            {
                this.iuniqueValueRenderer_0.set_Symbol(text, object_0 as ISymbol);
            }
            else if (object_0 is string)
            {
                this.iuniqueValueRenderer_0.set_Label(text, object_0.ToString());
            }
        }

        private int method_9(int int_1)
        {
            return -1;
        }

        private void UniqueValueRendererMoreAttributeCtrl_Load(object sender, EventArgs e)
        {
            this.method_0();
            if (this.m_pSG != null)
            {
                IEnumStyleGalleryItem item = this.m_pSG.get_Items("Color Ramps", "", "");
                item.Reset();
                for (IStyleGalleryItem item2 = item.Next(); item2 != null; item2 = item.Next())
                {
                    this.cboColorRamp.Add(item2);
                }
                item = null;
                GC.Collect();
            }
            if (this.cboColorRamp.Items.Count == 0)
            {
                this.cboColorRamp.Enabled = false;
                IRandomColorRamp ramp = new RandomColorRampClass
                {
                    StartHue = 40,
                    EndHue = 120,
                    MinValue = 65,
                    MaxValue = 90,
                    MinSaturation = 25,
                    MaxSaturation = 45,
                    Size = 5,
                    Seed = 23
                };
                this.icolorRamp_0 = ramp;
            }
            else
            {
                this.cboColorRamp.Text = this.iuniqueValueRenderer_0.ColorScheme;
                if (this.cboColorRamp.SelectedIndex == -1)
                {
                    this.cboColorRamp.SelectedIndex = 0;
                    this.iuniqueValueRenderer_0.ColorScheme = this.cboColorRamp.Text;
                }
                this.icolorRamp_0 = this.cboColorRamp.GetSelectStyleGalleryItem().Item as IColorRamp;
            }
            this.method_1();
            this.bool_0 = true;
        }

        public ILayer CurrentLayer
        {
            set
            {
                this.ilayer_0 = value;
                if (this.bool_0)
                {
                    this.method_0();
                    this.bool_0 = false;
                    this.method_1();
                    this.bool_0 = true;
                }
            }
        }

        bool IUserControl.Visible
        {
            get { return base.Visible; }
            set { base.Visible = value; }
        }

        public IStyleGallery StyleGallery
        {
            set
            {
                this.m_pSG = value;
                this.listView1.StyleGallery = this.m_pSG;
            }
        }
    }
}