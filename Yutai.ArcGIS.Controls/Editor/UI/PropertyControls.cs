using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Controls.Controls.EditorUI;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    public class PropertyControls : UserControl
    {
        private IContainer components = null;
        private GridControl dataGrid1;
        private GridView gridView1;
        private bool m_CanDo = false;
        private SortedList<string, object> m_changes = new SortedList<string, object>();
        private bool m_IsShowAllField = false;
        private bool m_IsUseAlias = false;
        private List<FieldInfo> m_listInfo = new List<FieldInfo>();
        private VertXtraGrid m_pVertXtraGrid = null;
        private SortedList<string, FieldInfo> m_sortAliaslistInfo = new SortedList<string, FieldInfo>();
        private SortedList<string, FieldInfo> m_sortnamelistInfo = new SortedList<string, FieldInfo>();
        private SortType m_sortype = SortType.LOrder;
        private Panel panel1;
        private Panel panel2;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private TextBox textBox1;

        public PropertyControls()
        {
            this.InitializeComponent();
        }

        private void AddField(FieldInfo v)
        {
            string layerFieldAlias = "";
            string str2 = this.GetValue(v.FieldName);
            if (this.m_IsShowAllField || v.IsVisible)
            {
                if (this.m_IsUseAlias)
                {
                    layerFieldAlias = v.LayerFieldAlias;
                }
                else
                {
                    layerFieldAlias = v.FieldName;
                }
                if (this.EditTemplate.HasSchema(v.FieldName))
                {
                    this.m_pVertXtraGrid.AddButtonEdit(layerFieldAlias, str2, true, new ButtonPressedEventHandler(this.ButtonEditButtonClick), v.FieldName);
                }
                else if (v.CodeDomainList != null)
                {
                    this.m_pVertXtraGrid.AddComBoBox(layerFieldAlias, str2, v.CodeDomainList, true);
                }
                else
                {
                    this.m_pVertXtraGrid.AddTextEdit(layerFieldAlias, str2, false);
                }
            }
        }

        public void Apply()
        {
            foreach (KeyValuePair<string, object> pair in this.m_changes)
            {
                if (pair.Value != null)
                {
                    string str = pair.Value.ToString();
                    if (str.Length > 0)
                    {
                        this.EditTemplate.SetFieldValue(pair.Key, str);
                    }
                    else
                    {
                        this.EditTemplate.SetFieldValue(pair.Key, "<空>");
                    }
                }
                else
                {
                    this.EditTemplate.SetFieldValue(pair.Key, "<空>");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.gridView1.ClearSorting();
            this.m_sortype = SortType.LOrder;
            this.InitControl();
            this.textBox1.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.gridView1.ClearSorting();
            this.m_sortype = SortType.NameOrder;
            this.InitControl();
            this.textBox1.Text = "";
        }

        private void ButtonEditButtonClick(object sender, ButtonPressedEventArgs e)
        {
            frmSelectSymbolClass class2 = new frmSelectSymbolClass {
                EditTemplate = this.EditTemplate
            };
            if (class2.ShowDialog() == DialogResult.OK)
            {
                this.InitControl();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private string GetValue(string fn)
        {
            object fieldValue = null;
            if (this.EditTemplate != null)
            {
                fieldValue = this.EditTemplate.GetFieldValue(fn);
            }
            if (fieldValue == null)
            {
                return "<空>";
            }
            return fieldValue.ToString();
        }

        private void gridView1_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            GridEditorItem row = this.gridView1.GetRow(e.RowHandle) as GridEditorItem;
            string key = "";
            if (this.m_IsUseAlias)
            {
                FieldInfo info = this.m_sortAliaslistInfo[row.Name];
                key = info.FieldName;
            }
            else
            {
                key = row.Name;
            }
            if (this.m_changes.ContainsKey(key))
            {
                this.m_changes[key] = row.Value.ToString();
            }
            else
            {
                this.m_changes.Add(key, row.Value.ToString());
            }
        }

        private void gridView1_EndSorting(object sender, EventArgs e)
        {
            this.m_sortype = SortType.NameOrder;
        }

        private void gridView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int[] selectedRows = this.gridView1.GetSelectedRows();
            if (selectedRows.Length != 0)
            {
                FieldInfo info;
                GridEditorItem row = this.gridView1.GetRow(selectedRows[0]) as GridEditorItem;
                string name = row.Name;
                if (this.m_IsUseAlias)
                {
                    info = this.m_sortAliaslistInfo[name];
                }
                else
                {
                    info = this.m_sortnamelistInfo[name];
                }
                this.textBox1.Text = string.Format("{0}\r\n{1}\r\n{2}", info.FieldName, info.TypeDescription, info.IsNull ? "允许为空" : "不允许为空");
            }
        }

        private void InitControl()
        {
            this.m_pVertXtraGrid.Clear();
            if (this.m_sortype == SortType.LOrder)
            {
                foreach (FieldInfo info in this.m_listInfo)
                {
                    this.AddField(info);
                }
            }
            else if (this.m_sortype == SortType.NameOrder)
            {
                if (this.m_IsUseAlias)
                {
                    foreach (KeyValuePair<string, FieldInfo> pair in this.m_sortAliaslistInfo)
                    {
                        this.AddField(pair.Value);
                    }
                }
                else
                {
                    foreach (KeyValuePair<string, FieldInfo> pair in this.m_sortnamelistInfo)
                    {
                        this.AddField(pair.Value);
                    }
                }
            }
            this.gridView1.Focus();
            this.gridView1.RefreshData();
            for (int i = 0; i < this.gridView1.RowCount; i++)
            {
                object row = this.gridView1.GetRow(i);
            }
            this.m_CanDo = true;
            base.Parent.Focus();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PropertyControls));
            this.panel1 = new Panel();
            this.panel2 = new Panel();
            this.textBox1 = new TextBox();
            this.dataGrid1 = new GridControl();
            this.gridView1 = new GridView();
            this.radioButton1 = new RadioButton();
            this.radioButton2 = new RadioButton();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.dataGrid1.BeginInit();
            this.gridView1.BeginInit();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x174, 0x18);
            this.panel1.TabIndex = 0;
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Dock = DockStyle.Bottom;
            this.panel2.Location = new Point(0, 0x109);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x174, 0x3a);
            this.panel2.TabIndex = 1;
            this.textBox1.Dock = DockStyle.Fill;
            this.textBox1.Location = new Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new Size(0x174, 0x3a);
            this.textBox1.TabIndex = 0;
            this.dataGrid1.Dock = DockStyle.Fill;
            this.dataGrid1.EmbeddedNavigator.Buttons.Append.Enabled = false;
            this.dataGrid1.EmbeddedNavigator.Buttons.Append.Hint = "增加";
            this.dataGrid1.EmbeddedNavigator.Buttons.CancelEdit.Hint = "取消编辑";
            this.dataGrid1.EmbeddedNavigator.Buttons.Edit.Enabled = false;
            this.dataGrid1.EmbeddedNavigator.Buttons.Edit.Hint = "编辑";
            this.dataGrid1.EmbeddedNavigator.Buttons.EndEdit.Hint = "结束编辑";
            this.dataGrid1.EmbeddedNavigator.Buttons.First.Hint = "第一个";
            this.dataGrid1.EmbeddedNavigator.Buttons.Last.Hint = "上一个";
            this.dataGrid1.EmbeddedNavigator.Buttons.Next.Hint = "下一个";
            this.dataGrid1.EmbeddedNavigator.Buttons.NextPage.Hint = "下一页";
            this.dataGrid1.EmbeddedNavigator.Buttons.Prev.Hint = "前一个";
            this.dataGrid1.EmbeddedNavigator.Buttons.PrevPage.Hint = "前一页";
            this.dataGrid1.EmbeddedNavigator.Buttons.Remove.Enabled = false;
            this.dataGrid1.EmbeddedNavigator.Buttons.Remove.Hint = "删除";
            this.dataGrid1.EmbeddedNavigator.Name = "";
            this.dataGrid1.Location = new Point(0, 0x18);
            this.dataGrid1.MainView = this.gridView1;
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new Size(0x174, 0xf1);
            this.dataGrid1.TabIndex = 4;
            this.dataGrid1.ViewCollection.AddRange(new BaseView[] { this.gridView1 });
            this.gridView1.GridControl = this.dataGrid1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowIncrementalSearch = true;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.SelectionChanged += new SelectionChangedEventHandler(this.gridView1_SelectionChanged);
            this.gridView1.EndSorting += new EventHandler(this.gridView1_EndSorting);
            this.radioButton1.Appearance = Appearance.Button;
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.FlatAppearance.BorderSize = 0;
            this.radioButton1.FlatStyle = FlatStyle.Flat;
            this.radioButton1.Image = (Image) resources.GetObject("radioButton1.Image");
            this.radioButton1.Location = new Point(0, 0);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new Size(0x16, 0x16);
            this.radioButton1.TabIndex = 2;
            this.radioButton1.TabStop = true;
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new EventHandler(this.radioButton1_CheckedChanged);
            this.radioButton2.Appearance = Appearance.Button;
            this.radioButton2.AutoSize = true;
            this.radioButton2.FlatAppearance.BorderSize = 0;
            this.radioButton2.FlatStyle = FlatStyle.Flat;
            this.radioButton2.Image = (Image) resources.GetObject("radioButton2.Image");
            this.radioButton2.Location = new Point(0x18, 0);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new Size(0x16, 0x16);
            this.radioButton2.TabIndex = 3;
            this.radioButton2.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.dataGrid1);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Name = "PropertyControls";
            base.Size = new Size(0x174, 0x143);
            base.Load += new EventHandler(this.PropertyControls_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.dataGrid1.EndInit();
            this.gridView1.EndInit();
            base.ResumeLayout(false);
        }

        private void PropertyControls_Load(object sender, EventArgs e)
        {
            if (this.EditTemplate != null)
            {
                this.FeatureLayer = this.EditTemplate.FeatureLayer;
            }
            this.m_pVertXtraGrid = new VertXtraGrid(this.dataGrid1);
            this.ReadFieldInfo();
            this.InitControl();
            this.gridView1.CellValueChanged += new CellValueChangedEventHandler(this.gridView1_CellValueChanged);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.gridView1.ClearSorting();
            this.gridView1.ClearSelection();
            if (this.radioButton1.Checked)
            {
                this.m_sortype = SortType.LOrder;
                this.InitControl();
                this.textBox1.Text = "";
            }
            else
            {
                this.m_sortype = SortType.NameOrder;
                this.InitControl();
                this.textBox1.Text = "";
            }
        }

        private void ReadFieldInfo()
        {
            IDomain domain = null;
            ICodedValueDomain domain2 = null;
            ILayerFields featureLayer = this.FeatureLayer as ILayerFields;
            for (int i = 0; i < featureLayer.FieldCount; i++)
            {
                string str;
                IFieldInfo info;
                FieldInfo info2;
                IField field = featureLayer.get_Field(i);
                if (field.Required)
                {
                    if ((featureLayer is IFDOGraphicsLayer) && (string.Compare(field.Name, "SymbolID", true) == 0))
                    {
                        str = (field.Type == esriFieldType.esriFieldTypeString) ? string.Format("(长度={0})", field.Length) : "";
                        info = featureLayer.get_FieldInfo(i);
                        info2 = new FieldInfo {
                            FieldName = "SymbolID",
                            TypeDescription = RowOperator.GetFieldTypeString(field.Type) + str,
                            IsNull = field.IsNullable,
                            IsVisible = info.Visible,
                            LayerFieldAlias = info.Alias
                        };
                        this.m_listInfo.Add(info2);
                        this.m_sortAliaslistInfo.Add(info2.LayerFieldAlias, info2);
                        this.m_sortnamelistInfo.Add(info2.FieldName, info2);
                    }
                }
                else if (field.Editable && ((((field.Type != esriFieldType.esriFieldTypeGeometry) && (field.Type != esriFieldType.esriFieldTypeBlob)) && (field.Type != esriFieldType.esriFieldTypeRaster)) && (field.Type != esriFieldType.esriFieldTypeOID)))
                {
                    str = (field.Type == esriFieldType.esriFieldTypeString) ? string.Format("(长度={0})", field.Length) : "";
                    info = featureLayer.get_FieldInfo(i);
                    info2 = new FieldInfo {
                        FieldName = field.Name,
                        TypeDescription = RowOperator.GetFieldTypeString(field.Type) + str,
                        IsNull = field.IsNullable,
                        IsVisible = info.Visible,
                        LayerFieldAlias = info.Alias
                    };
                    domain = field.Domain;
                    if ((domain != null) && (domain is ICodedValueDomain))
                    {
                        domain2 = domain as ICodedValueDomain;
                        List<string> list = new List<string>();
                        if (field.IsNullable)
                        {
                            list.Add("<空>");
                        }
                        for (int j = 0; j < domain2.CodeCount; j++)
                        {
                            list.Add(domain2.get_Name(j));
                        }
                        info2.CodeDomainList = list;
                    }
                    this.m_listInfo.Add(info2);
                    this.m_sortAliaslistInfo.Add(info2.LayerFieldAlias, info2);
                    this.m_sortnamelistInfo.Add(info2.FieldName, info2);
                }
            }
        }

        public JLKEditTemplate EditTemplate { get; set; }

        public IFeatureLayer FeatureLayer { protected get; set; }

        public ITable Table { protected get; set; }

        private class FieldInfo
        {
            public List<string> CodeDomainList { get; set; }

            public string FieldName { get; set; }

            public bool IsNull { get; set; }

            public bool IsVisible { get; set; }

            public string LayerFieldAlias { get; set; }

            public string TypeDescription { get; set; }
        }

        private enum SortType
        {
            LOrder,
            NameOrder
        }
    }
}

