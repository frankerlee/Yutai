using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.ControlExtend;
using Yutai.ArcGIS.Common.Wrapper;
using BaseView = DevExpress.XtraBars.Docking2010.Views.BaseView;

namespace Yutai.ArcGIS.Catalog.UI
{
    public partial class FieldMatchCtrl : UserControl
    {
        private IContainer icontainer_0 = null;
        private VertXtraGrid vertXtraGrid_0 = null;

        public FieldMatchCtrl()
        {
            this.InitializeComponent();
            this.vertXtraGrid_0 = new VertXtraGrid(this.gridControl1);
            this.gridView1.CellValueChanged += new CellValueChangedEventHandler(this.gridView1_CellValueChanged);
        }

 private void FieldMatchCtrl_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        private void gridView1_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
        }

        public void Init()
        {
            if ((this.TargertFields != null) && (this.SourceFields != null))
            {
                this.vertXtraGrid_0.Clear();
                for (int i = 0; i < this.TargertFields.FieldCount; i++)
                {
                    IField field = this.TargertFields.get_Field(i);
                    if (field.Editable && ((field.Type != esriFieldType.esriFieldTypeOID) && (field.Type != esriFieldType.esriFieldTypeGeometry)))
                    {
                        FieldObject obj3;
                        FieldObject obj2 = new FieldObject(field);
                        List<object> list = this.method_0(field, out obj3);
                        this.vertXtraGrid_0.AddComBoBox(obj2.ToString(), (obj3 != null) ? ((object) obj3) : ((object) "<空>"), list, false, obj2);
                    }
                }
            }
        }

 private List<object> method_0(IField ifield_0, out FieldObject fieldObject_0)
        {
            List<object> list = new List<object> { "<空>" };
            fieldObject_0 = null;
            for (int i = 0; i < this.SourceFields.FieldCount; i++)
            {
                IField field = this.SourceFields.get_Field(i);
                if ((field.Type != esriFieldType.esriFieldTypeOID) && (field.Type != esriFieldType.esriFieldTypeGeometry))
                {
                    FieldObject obj2;
                    if (ifield_0.Type == esriFieldType.esriFieldTypeGlobalID)
                    {
                        if (((field.Type == esriFieldType.esriFieldTypeGlobalID) || (field.Type == esriFieldType.esriFieldTypeGUID)) || (field.Type == esriFieldType.esriFieldTypeString))
                        {
                            obj2 = new FieldObject(field);
                            list.Add(obj2);
                            if ((fieldObject_0 == null) && (string.Compare(ifield_0.Name, field.Name, true) == 0))
                            {
                                fieldObject_0 = obj2;
                            }
                        }
                    }
                    else if (ifield_0.Type == esriFieldType.esriFieldTypeGUID)
                    {
                        if (((field.Type == esriFieldType.esriFieldTypeGlobalID) || (field.Type == esriFieldType.esriFieldTypeGUID)) || (field.Type == esriFieldType.esriFieldTypeString))
                        {
                            obj2 = new FieldObject(field);
                            list.Add(obj2);
                            if ((fieldObject_0 == null) && (string.Compare(ifield_0.Name, field.Name, true) == 0))
                            {
                                fieldObject_0 = obj2;
                            }
                        }
                    }
                    else if (ifield_0.Type == esriFieldType.esriFieldTypeString)
                    {
                        if ((field.Type != esriFieldType.esriFieldTypeRaster) && (field.Type != esriFieldType.esriFieldTypeBlob))
                        {
                            obj2 = new FieldObject(field);
                            list.Add(obj2);
                            if ((fieldObject_0 == null) && (string.Compare(ifield_0.Name, field.Name, true) == 0))
                            {
                                fieldObject_0 = obj2;
                            }
                        }
                    }
                    else if (ifield_0.Type == esriFieldType.esriFieldTypeRaster)
                    {
                        if (field.Type == esriFieldType.esriFieldTypeRaster)
                        {
                            obj2 = new FieldObject(field);
                            list.Add(obj2);
                            if ((fieldObject_0 == null) && (string.Compare(ifield_0.Name, field.Name, true) == 0))
                            {
                                fieldObject_0 = obj2;
                            }
                        }
                    }
                    else if (ifield_0.Type == esriFieldType.esriFieldTypeBlob)
                    {
                        if (field.Type == esriFieldType.esriFieldTypeBlob)
                        {
                            obj2 = new FieldObject(field);
                            list.Add(obj2);
                            if ((fieldObject_0 == null) && (string.Compare(ifield_0.Name, field.Name, true) == 0))
                            {
                                fieldObject_0 = obj2;
                            }
                        }
                    }
                    else if (ifield_0.Type == esriFieldType.esriFieldTypeXML)
                    {
                        if (field.Type == esriFieldType.esriFieldTypeXML)
                        {
                            obj2 = new FieldObject(field);
                            list.Add(obj2);
                            if ((fieldObject_0 == null) && (string.Compare(ifield_0.Name, field.Name, true) == 0))
                            {
                                fieldObject_0 = obj2;
                            }
                        }
                    }
                    else if (ifield_0.Type == esriFieldType.esriFieldTypeDate)
                    {
                        if ((field.Type == esriFieldType.esriFieldTypeDate) || (field.Type == esriFieldType.esriFieldTypeString))
                        {
                            obj2 = new FieldObject(field);
                            list.Add(obj2);
                            if ((fieldObject_0 == null) && (string.Compare(ifield_0.Name, field.Name, true) == 0))
                            {
                                fieldObject_0 = obj2;
                            }
                        }
                    }
                    else if (((((field.Type != esriFieldType.esriFieldTypeRaster) && (field.Type != esriFieldType.esriFieldTypeBlob)) && ((field.Type != esriFieldType.esriFieldTypeXML) && (field.Type != esriFieldType.esriFieldTypeGUID))) && (field.Type != esriFieldType.esriFieldTypeGlobalID)) && (field.Type != esriFieldType.esriFieldTypeDate))
                    {
                        obj2 = new FieldObject(field);
                        list.Add(obj2);
                        if ((fieldObject_0 == null) && (string.Compare(ifield_0.Name, field.Name, true) == 0))
                        {
                            fieldObject_0 = obj2;
                        }
                    }
                }
            }
            return list;
        }

        public SortedList<string, string> FieldMaps
        {
            get
            {
                SortedList<string, string> list = new SortedList<string, string>();
                for (int i = 0; i < this.gridView1.RowCount; i++)
                {
                    GridEditorItem row = this.gridView1.GetRow(i) as GridEditorItem;
                    FieldObject tag = row.Tag as FieldObject;
                    if (row.Value != "<空>")
                    {
                        string key = tag.Field.Name.ToLower();
                        string str2 = row.Value.ToString();
                        int index = str2.IndexOf("(");
                        str2 = str2.Substring(0, index).Trim();
                        list.Add(key, str2);
                    }
                }
                return list;
            }
        }

        public IFields SourceFields
        {
            [CompilerGenerated]
            get
            {
                return this.ifields_0;
            }
            [CompilerGenerated]
            set
            {
                this.ifields_0 = value;
            }
        }

        public IFields TargertFields
        {
            [CompilerGenerated]
            get
            {
                return this.ifields_1;
            }
            [CompilerGenerated]
            set
            {
                this.ifields_1 = value;
            }
        }
    }
}

