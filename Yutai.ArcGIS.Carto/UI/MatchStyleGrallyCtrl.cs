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
using Yutai.ArcGIS.Common.Display;
using Yutai.Shared;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class MatchStyleGrallyCtrl : UserControl, IUserControl
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private SimpleButton btnAddAllValues;
        private SimpleButton btnAddValue;
        private SimpleButton btnBrowStyle;
        private SimpleButton btnDelete;
        private SimpleButton btnDeleteAll;
        private SimpleButton btnMoveDown;
        private SimpleButton btnMoveUp;
        private ComboBoxEdit cboFields;
        private ComboBoxEdit cboLookupStyleset;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ColumnHeader columnHeader_2;
        private ColumnHeader columnHeader_3;
        private ContextMenuStrip contextMenuStrip1;
        private IColorRamp icolorRamp_0 = null;
        private IContainer icontainer_0;
        private IEnumColors ienumColors_0 = null;
        private IFields ifields_0 = null;
        private IGeoFeatureLayer igeoFeatureLayer_0 = null;
        private IList ilist_0 = new ArrayList();
        private int int_0 = 10;
        private IUniqueValueRenderer iuniqueValueRenderer_0 = null;
        private Label label1;
        private Label label2;
        private RenderInfoListView listView1;
        public IStyleGallery m_pSG = null;
        private ToolStripMenuItem menuitemGroup;
        private ToolStripMenuItem menuitemUnGroup;

        public MatchStyleGrallyCtrl()
        {
            this.InitializeComponent();
            this.listView1.OnValueChanged += new RenderInfoListView.OnValueChangedHandler(this.method_5);
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
                    string[] strArray = ex.SubItems[1].Text.Split(new char[] { ';' });
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
            this.igeoFeatureLayer_0.Renderer = renderer as IFeatureRenderer;
        }

        private void btnAddAllValues_Click(object sender, EventArgs e)
        {
            this.listView1.Items.Clear();
            this.ilist_0.Clear();
            this.iuniqueValueRenderer_0.RemoveAllValues();
            if (this.GetUniqueValues(this.igeoFeatureLayer_0, (this.cboFields.SelectedItem as FieldWrap).Name, this.ilist_0))
            {
                object[] objArray = new object[4];
                ISymbol defaultSymbol = this.iuniqueValueRenderer_0.DefaultSymbol;
                if (defaultSymbol == null)
                {
                    defaultSymbol = this.method_4(this.igeoFeatureLayer_0.FeatureClass.ShapeType);
                }
                string str = "Marker Symbols";
                if (this.igeoFeatureLayer_0.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                {
                    str = "Line Symbols";
                }
                else if (this.igeoFeatureLayer_0.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                {
                    str = "Fill Symbols";
                }
                ISymbol symbol = null;
                int num = 0;
                while (true)
                {
                    if (num >= this.ilist_0.Count)
                    {
                        break;
                    }
                    try
                    {
                        if (this.cboLookupStyleset.SelectedIndex != -1)
                        {
                            IStyleGalleryItem item = SymbolFind.FindStyleGalleryItem(this.ilist_0[num].ToString(), this.m_pSG, this.cboLookupStyleset.Text, str, "");
                            if (item != null)
                            {
                                symbol = item.Item as ISymbol;
                            }
                        }
                        if (symbol == null)
                        {
                            symbol = (defaultSymbol as IClone).Clone() as ISymbol;
                        }
                        objArray[0] = symbol;
                        objArray[1] = this.ilist_0[num].ToString();
                        objArray[2] = this.ilist_0[num].ToString();
                        objArray[3] = this.method_3(this.method_1(), (this.cboFields.SelectedItem as FieldWrap).Name, objArray[1].ToString()).ToString();
                        this.iuniqueValueRenderer_0.AddValue(objArray[1].ToString(), null, symbol);
                        this.listView1.Add(objArray);
                    }
                    catch
                    {
                    }
                    num++;
                }
                this.ilist_0.Clear();
            }
        }

        private void btnAddValue_Click(object sender, EventArgs e)
        {
            frmAddValues values = new frmAddValues {
                Layer = this.igeoFeatureLayer_0,
                FieldName = this.cboFields.Text,
                List = this.ilist_0 as ArrayList,
                UniqueValueRenderer = this.iuniqueValueRenderer_0,
                GetAllValues = this.bool_1
            };
            if (values.ShowDialog() == DialogResult.OK)
            {
                ISymbol defaultSymbol = this.iuniqueValueRenderer_0.DefaultSymbol;
                if (defaultSymbol == null)
                {
                    defaultSymbol = this.method_4(this.igeoFeatureLayer_0.FeatureClass.ShapeType);
                }
                ISymbol symbol = null;
                object[] objArray = new object[4];
                string str = "Marker Symbols";
                if (this.igeoFeatureLayer_0.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                {
                    str = "Line Symbols";
                }
                else if (this.igeoFeatureLayer_0.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                {
                    str = "Fill Symbols";
                }
                for (int i = 0; i < values.SelectedItems.Count; i++)
                {
                    if (this.cboLookupStyleset.SelectedIndex != -1)
                    {
                        IStyleGalleryItem item = SymbolFind.FindStyleGalleryItem(values.SelectedItems[i].ToString(), this.m_pSG, this.cboLookupStyleset.Text, str, "");
                        if (item != null)
                        {
                            symbol = item.Item as ISymbol;
                        }
                    }
                    if (symbol == null)
                    {
                        symbol = (defaultSymbol as IClone).Clone() as ISymbol;
                    }
                    objArray[0] = symbol;
                    objArray[1] = values.SelectedItems[i].ToString();
                    objArray[2] = values.SelectedItems[i].ToString();
                    objArray[3] = this.method_3(this.method_1(), (this.cboFields.SelectedItem as FieldWrap).Name, objArray[1].ToString()).ToString();
                    this.iuniqueValueRenderer_0.AddValue(objArray[1].ToString(), null, symbol);
                    this.listView1.Add(objArray);
                }
            }
            this.bool_1 = values.GetAllValues;
        }

        private void btnBrowStyle_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                Filter = "符号库 (*.Style)|*.Style",
                RestoreDirectory = true
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                IStyleGalleryStorage pSG = (IStyleGalleryStorage) this.m_pSG;
                int fileCount = pSG.FileCount;
                pSG.AddFile(dialog.FileName);
                if (fileCount == pSG.FileCount)
                {
                    pSG = null;
                }
                else
                {
                    pSG = null;
                    this.cboLookupStyleset.Properties.Items.Add(dialog.FileName);
                    this.cboLookupStyleset.Text = dialog.FileName;
                }
            }
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

        private void cboFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.listView1.Items.Clear();
                this.ilist_0.Clear();
                this.bool_1 = false;
                this.iuniqueValueRenderer_0.RemoveAllValues();
                if ((this.cboFields.SelectedIndex >= 0) && (this.ifields_0 != null))
                {
                    this.iuniqueValueRenderer_0.set_Field(0, (this.cboFields.SelectedItem as FieldWrap).Name);
                    esriFieldType type = (this.cboFields.SelectedItem as FieldWrap).Type;
                    this.iuniqueValueRenderer_0.set_FieldType(0, type == esriFieldType.esriFieldTypeString);
                }
            }
        }

        private void cboLookupStyleset_SelectedIndexChanged(object sender, EventArgs e)
        {
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

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_2);
        }

        public bool GetUniqueValues(ILayer ilayer_0, string string_0, IList ilist_1)
        {
            try
            {
                ITable displayTable = null;
                if (ilayer_0 is IDisplayTable)
                {
                    displayTable = (ilayer_0 as IDisplayTable).DisplayTable;
                }
                else
                {
                    IAttributeTable table2 = ilayer_0 as IAttributeTable;
                    if (table2 != null)
                    {
                        displayTable = table2.AttributeTable;
                    }
                    else
                    {
                        displayTable = ilayer_0 as ITable;
                    }
                }
                if (displayTable == null)
                {
                    return false;
                }
                ICursor cursor = displayTable.Search(null, false);
                IDataStatistics statistics = new DataStatisticsClass {
                    Field = string_0,
                    Cursor = cursor
                };
                if ((statistics.UniqueValueCount > 500) && (MessageBox.Show("唯一值数大于500，是否继续", "提示", MessageBoxButtons.YesNo) == DialogResult.No))
                {
                    return false;
                }
                this.bool_1 = true;
                IEnumerator uniqueValues = statistics.UniqueValues;
                uniqueValues.Reset();
                while (uniqueValues.MoveNext())
                {
                    this.ilist_0.Add(uniqueValues.Current);
                }
                (this.ilist_0 as ArrayList).Sort();
                return true;
            }
            catch (Exception exception)
            {
                Logger.Current.Error("", exception, "");
            }
            return false;
        }

        private void InitializeComponent()
        {
            this.icontainer_0 = new Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MatchStyleGrallyCtrl));
            this.label1 = new Label();
            this.label2 = new Label();
            this.btnAddAllValues = new SimpleButton();
            this.btnAddValue = new SimpleButton();
            this.btnDelete = new SimpleButton();
            this.btnDeleteAll = new SimpleButton();
            this.listView1 = new RenderInfoListView();
            this.columnHeader_3 = new ColumnHeader();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.columnHeader_2 = new ColumnHeader();
            this.btnBrowStyle = new SimpleButton();
            this.cboFields = new ComboBoxEdit();
            this.cboLookupStyleset = new ComboBoxEdit();
            this.btnMoveDown = new SimpleButton();
            this.btnMoveUp = new SimpleButton();
            this.contextMenuStrip1 = new ContextMenuStrip(this.icontainer_0);
            this.menuitemGroup = new ToolStripMenuItem();
            this.menuitemUnGroup = new ToolStripMenuItem();
            this.cboFields.Properties.BeginInit();
            this.cboLookupStyleset.Properties.BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "值字段";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0x10, 40);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x41, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "匹配符号库";
            this.btnAddAllValues.Location = new System.Drawing.Point(0x18, 0xe8);
            this.btnAddAllValues.Name = "btnAddAllValues";
            this.btnAddAllValues.Size = new Size(80, 0x18);
            this.btnAddAllValues.TabIndex = 8;
            this.btnAddAllValues.Text = "添加所有值";
            this.btnAddAllValues.Click += new EventHandler(this.btnAddAllValues_Click);
            this.btnAddValue.Location = new System.Drawing.Point(0x70, 0xe8);
            this.btnAddValue.Name = "btnAddValue";
            this.btnAddValue.Size = new Size(0x48, 0x18);
            this.btnAddValue.TabIndex = 9;
            this.btnAddValue.Text = "添加值...";
            this.btnAddValue.Click += new EventHandler(this.btnAddValue_Click);
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(0xc0, 0xe8);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(80, 0x18);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.btnDeleteAll.Location = new System.Drawing.Point(280, 0xe8);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new Size(80, 0x18);
            this.btnDeleteAll.TabIndex = 11;
            this.btnDeleteAll.Text = "删除全部";
            this.btnDeleteAll.Click += new EventHandler(this.btnDeleteAll_Click);
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_3, this.columnHeader_0, this.columnHeader_1, this.columnHeader_2 });
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(8, 0x48);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0x178, 0x98);
            this.listView1.TabIndex = 12;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.MouseUp += new MouseEventHandler(this.listView1_MouseUp);
            this.columnHeader_3.Text = "符号";
            this.columnHeader_0.Text = "值";
            this.columnHeader_0.Width = 110;
            this.columnHeader_1.Text = "标注";
            this.columnHeader_1.Width = 0x77;
            this.columnHeader_2.Text = "数目";
            this.columnHeader_2.Width = 0x3f;
            this.btnBrowStyle.Location = new System.Drawing.Point(0x158, 40);
            this.btnBrowStyle.Name = "btnBrowStyle";
            this.btnBrowStyle.Size = new Size(0x38, 0x18);
            this.btnBrowStyle.TabIndex = 15;
            this.btnBrowStyle.Text = "浏览...";
            this.btnBrowStyle.Click += new EventHandler(this.btnBrowStyle_Click);
            this.cboFields.EditValue = "";
            this.cboFields.Location = new System.Drawing.Point(0x5d, 6);
            this.cboFields.Name = "cboFields";
            this.cboFields.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboFields.Size = new Size(0xeb, 0x15);
            this.cboFields.TabIndex = 0x10;
            this.cboFields.SelectedIndexChanged += new EventHandler(this.cboFields_SelectedIndexChanged);
            this.cboLookupStyleset.EditValue = "";
            this.cboLookupStyleset.Location = new System.Drawing.Point(0x60, 40);
            this.cboLookupStyleset.Name = "cboLookupStyleset";
            this.cboLookupStyleset.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboLookupStyleset.Size = new Size(240, 0x15);
            this.cboLookupStyleset.TabIndex = 0x11;
            this.cboLookupStyleset.SelectedIndexChanged += new EventHandler(this.cboLookupStyleset_SelectedIndexChanged);
            this.btnMoveDown.Enabled = false;
            this.btnMoveDown.Image = (System.Drawing.Image)resources.GetObject("btnMoveDown.Image");
            this.btnMoveDown.Location = new System.Drawing.Point(0x188, 0x80);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new Size(0x18, 0x18);
            this.btnMoveDown.TabIndex = 0x34;
            this.btnMoveDown.Click += new EventHandler(this.btnMoveDown_Click);
            this.btnMoveUp.Enabled = false;
            this.btnMoveUp.Image = (System.Drawing.Image)resources.GetObject("btnMoveUp.Image");
            this.btnMoveUp.Location = new System.Drawing.Point(0x188, 0x58);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new Size(0x18, 0x18);
            this.btnMoveUp.TabIndex = 0x33;
            this.btnMoveUp.Click += new EventHandler(this.btnMoveUp_Click);
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.menuitemGroup, this.menuitemUnGroup });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new Size(0x99, 70);
            this.menuitemGroup.Name = "menuitemGroup";
            this.menuitemGroup.Size = new Size(0x98, 0x16);
            this.menuitemGroup.Text = "值分组";
            this.menuitemGroup.Click += new EventHandler(this.menuitemGroup_Click);
            this.menuitemUnGroup.Name = "menuitemUnGroup";
            this.menuitemUnGroup.Size = new Size(0x98, 0x16);
            this.menuitemUnGroup.Text = "取消分组";
            this.menuitemUnGroup.Click += new EventHandler(this.menuitemUnGroup_Click);
            base.Controls.Add(this.btnMoveDown);
            base.Controls.Add(this.btnMoveUp);
            base.Controls.Add(this.cboLookupStyleset);
            base.Controls.Add(this.cboFields);
            base.Controls.Add(this.btnBrowStyle);
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.btnDeleteAll);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.btnAddValue);
            base.Controls.Add(this.btnAddAllValues);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "MatchStyleGrallyCtrl";
            base.Size = new Size(0x1b0, 0x108);
            base.Load += new EventHandler(this.MatchStyleGrallyCtrl_Load);
            this.cboFields.Properties.EndInit();
            this.cboLookupStyleset.Properties.EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
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
                        if (this.listView1.Items.Count == 1)
                        {
                            this.btnMoveDown.Enabled = false;
                        }
                        else
                        {
                            this.btnMoveDown.Enabled = true;
                        }
                        this.btnMoveUp.Enabled = false;
                    }
                    else if (this.listView1.SelectedIndices[0] == (this.listView1.Items.Count - 1))
                    {
                        this.btnMoveDown.Enabled = false;
                        this.btnMoveUp.Enabled = true;
                    }
                    else
                    {
                        this.btnMoveDown.Enabled = true;
                        this.btnMoveUp.Enabled = true;
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

        private void MatchStyleGrallyCtrl_Load(object sender, EventArgs e)
        {
            this.method_0();
            if (this.m_pSG != null)
            {
                for (int i = 0; i < (this.m_pSG as IStyleGalleryStorage).FileCount; i++)
                {
                    this.cboLookupStyleset.Properties.Items.Add((this.m_pSG as IStyleGalleryStorage).get_File(i));
                }
            }
            if (this.cboLookupStyleset.Properties.Items.Count > 0)
            {
                this.cboLookupStyleset.SelectedIndex = 0;
            }
            this.method_2();
            this.bool_0 = true;
        }

        private void menuitemGroup_Click(object sender, EventArgs e)
        {
            ListViewItemEx ex = this.listView1.SelectedItems[0] as ListViewItemEx;
            string text = ex.SubItems[1].Text;
            text.Split(new char[] { ',' });
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
                string[] strArray = ex.SubItems[1].Text.Split(new char[] { ';' });
                bool flag = false;
                if (ex.SubItems[3].Text != "?")
                {
                    flag = true;
                    ex.SubItems[3].Text = this.method_3(this.method_1(), (this.cboFields.SelectedItem as FieldWrap).Name, strArray[0]).ToString();
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
                    objArray[3] = !flag ? "?" : this.method_3(this.method_1(), (this.cboFields.SelectedItem as FieldWrap).Name, str2).ToString();
                    this.listView1.Add(objArray);
                }
            }
            catch (Exception)
            {
            }
        }

        private void method_0()
        {
            if (this.igeoFeatureLayer_0 == null)
            {
                this.iuniqueValueRenderer_0 = null;
            }
            else
            {
                IAttributeTable table = this.igeoFeatureLayer_0 as IAttributeTable;
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
                IUniqueValueRenderer pInObject = this.igeoFeatureLayer_0.Renderer as IUniqueValueRenderer;
                if (pInObject == null)
                {
                    if (this.iuniqueValueRenderer_0 == null)
                    {
                        this.iuniqueValueRenderer_0 = new UniqueValueRendererClass();
                        this.iuniqueValueRenderer_0.FieldCount = 1;
                        this.iuniqueValueRenderer_0.DefaultLabel = "默认符号";
                        this.iuniqueValueRenderer_0.DefaultSymbol = this.method_4(this.igeoFeatureLayer_0.FeatureClass.ShapeType);
                        this.iuniqueValueRenderer_0.UseDefaultSymbol = true;
                    }
                }
                else
                {
                    IObjectCopy copy = new ObjectCopyClass();
                    this.iuniqueValueRenderer_0 = copy.Copy(pInObject) as IUniqueValueRenderer;
                }
            }
        }

        private ITable method_1()
        {
            if (this.igeoFeatureLayer_0 == null)
            {
                return null;
            }
            if (this.igeoFeatureLayer_0 is IDisplayTable)
            {
                return (this.igeoFeatureLayer_0 as IDisplayTable).DisplayTable;
            }
            if (this.igeoFeatureLayer_0 is IAttributeTable)
            {
                return (this.igeoFeatureLayer_0 as IAttributeTable).AttributeTable;
            }
            if (this.igeoFeatureLayer_0 != null)
            {
                return (this.igeoFeatureLayer_0.FeatureClass as ITable);
            }
            return (this.igeoFeatureLayer_0 as ITable);
        }

        private void method_2()
        {
            this.cboFields.Properties.Items.Clear();
            this.listView1.Items.Clear();
            this.btnDelete.Enabled = false;
            this.ifields_0 = null;
            if (this.igeoFeatureLayer_0 != null)
            {
                IFields fields;
                int num;
                if (this.igeoFeatureLayer_0 is IDisplayTable)
                {
                    fields = (this.igeoFeatureLayer_0 as IDisplayTable).DisplayTable.Fields;
                }
                else
                {
                    if (this.igeoFeatureLayer_0.FeatureClass == null)
                    {
                        return;
                    }
                    fields = this.igeoFeatureLayer_0.FeatureClass.Fields;
                }
                this.ifields_0 = fields;
                for (num = 0; num < fields.FieldCount; num++)
                {
                    IField field = fields.get_Field(num);
                    if ((((field.Type != esriFieldType.esriFieldTypeOID) && (field.Type != esriFieldType.esriFieldTypeGeometry)) && (field.Type != esriFieldType.esriFieldTypeBlob)) && (field.Type != esriFieldType.esriFieldTypeRaster))
                    {
                        this.cboFields.Properties.Items.Add(new FieldWrap(field));
                    }
                }
                string str = this.iuniqueValueRenderer_0.get_Field(0);
                if (str.Length == 0)
                {
                    if (this.cboFields.Properties.Items.Count > 0)
                    {
                        this.cboFields.SelectedIndex = 0;
                        this.iuniqueValueRenderer_0.set_Field(0, (this.cboFields.SelectedItem as FieldWrap).Name);
                        esriFieldType type = (this.cboFields.SelectedItem as FieldWrap).Type;
                        this.iuniqueValueRenderer_0.set_FieldType(0, type == esriFieldType.esriFieldTypeString);
                    }
                }
                else
                {
                    for (num = 0; num < this.cboFields.Properties.Items.Count; num++)
                    {
                        if ((this.cboFields.Properties.Items[num] as FieldWrap).Name == str)
                        {
                            this.cboFields.SelectedIndex = num;
                            break;
                        }
                    }
                }
                if (this.icolorRamp_0 != null)
                {
                    bool flag;
                    this.icolorRamp_0.Size = this.int_0;
                    this.icolorRamp_0.CreateRamp(out flag);
                    this.ienumColors_0 = this.icolorRamp_0.Colors;
                    this.ienumColors_0.Reset();
                }
                object[] objArray = new object[4];
                SortedList<string, ListViewItemEx> list = new SortedList<string, ListViewItemEx>();
                num = 0;
                while (true)
                {
                    ListViewItemEx ex;
                    if (num >= this.iuniqueValueRenderer_0.ValueCount)
                    {
                        break;
                    }
                    string str2 = this.iuniqueValueRenderer_0.get_Value(num);
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
                    num++;
                }
            }
        }

        private int method_3(ITable itable_0, string string_0, string string_1)
        {
            if (itable_0 == null)
            {
                return 0;
            }
            IFields fields = itable_0.Fields;
            IField field = fields.get_Field(fields.FindField(string_0));
            IQueryFilter queryFilter = new QueryFilterClass {
                WhereClause = field.Name + " = " + this.ConvertFieldValueToString(field.Type, string_1)
            };
            return itable_0.RowCount(queryFilter);
        }

        private ISymbol method_4(esriGeometryType esriGeometryType_0)
        {
            switch (esriGeometryType_0)
            {
                case esriGeometryType.esriGeometryPoint:
                case esriGeometryType.esriGeometryMultipoint:
                {
                    IMarkerSymbol symbol = new SimpleMarkerSymbolClass {
                        Size = 3.0
                    };
                    return (symbol as ISymbol);
                }
                case esriGeometryType.esriGeometryPolyline:
                {
                    ILineSymbol symbol2 = new SimpleLineSymbolClass {
                        Width = 0.2
                    };
                    return (symbol2 as ISymbol);
                }
                case esriGeometryType.esriGeometryPolygon:
                    return new SimpleFillSymbolClass();
            }
            return null;
        }

        private void method_5(int int_1, object object_0)
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

        private int method_6(int int_1)
        {
            return -1;
        }

        public ILayer CurrentLayer
        {
            set
            {
                this.igeoFeatureLayer_0 = value as IGeoFeatureLayer;
                if (this.bool_0)
                {
                    this.method_0();
                    this.bool_0 = false;
                    this.method_2();
                    this.bool_0 = true;
                }
            }
        }

        bool IUserControl.Visible
        {
            get
            {
                return base.Visible;
            }
            set
            {
                base.Visible = value;
            }
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

