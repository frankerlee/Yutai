using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.ControlExtend;
using Yutai.ArcGIS.Common.Editor;
using Yutai.ArcGIS.Common.Geodatabase;
using Yutai.ArcGIS.Controls.Controls.EditorUI;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    public partial class PropertyControls : UserControl
    {
        private bool m_CanDo = false;
        private SortedList<string, object> m_changes = new SortedList<string, object>();
        private bool m_IsShowAllField = false;
        private bool m_IsUseAlias = false;
        private List<FieldInfo> m_listInfo = new List<FieldInfo>();
        private VertXtraGrid m_pVertXtraGrid = null;
        private SortedList<string, FieldInfo> m_sortAliaslistInfo = new SortedList<string, FieldInfo>();
        private SortedList<string, FieldInfo> m_sortnamelistInfo = new SortedList<string, FieldInfo>();
        private SortType m_sortype = SortType.LOrder;

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

        public YTEditTemplate EditTemplate { get; set; }

        public IFeatureLayer FeatureLayer { protected get; set; }

        public ITable Table { protected get; set; }

        private partial class FieldInfo
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

