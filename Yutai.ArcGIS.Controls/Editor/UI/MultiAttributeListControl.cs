using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.ControlExtend;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    internal class MultiAttributeListControl : UserControl
    {
        private Container components = null;
        private GridControl gridControl1;
        private GridView gridView1;
        private bool m_CanDo = false;
        private int m_nX;
        private int m_nY;
        private IActiveView m_pActiveView = null;
        private IFeatureLayer m_pFeatLayer = null;
        private VertXtraGrid m_pVertXtraGrid = null;

        public MultiAttributeListControl()
        {
            this.InitializeComponent();
            this.m_pVertXtraGrid = new VertXtraGrid(this.gridControl1);
            this.gridView1.CellValueChanged += new CellValueChangedEventHandler(this.gridView1_CellValueChanged);
        }

        private void AttributeListControl_Load(object sender, EventArgs e)
        {
            this.Init();
            this.m_CanDo = true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private string GetShapeString(IField pField)
        {
            string str = "";
            IGeometryDef geometryDef = pField.GeometryDef;
            if (geometryDef != null)
            {
                switch (geometryDef.GeometryType)
                {
                    case esriGeometryType.esriGeometryPoint:
                        str = "点";
                        break;

                    case esriGeometryType.esriGeometryMultipoint:
                        str = "多点";
                        break;

                    case esriGeometryType.esriGeometryPolyline:
                        str = "线";
                        break;

                    case esriGeometryType.esriGeometryPolygon:
                        str = "多边形";
                        break;

                    case esriGeometryType.esriGeometryMultiPatch:
                        str = "多面";
                        break;
                }
                str = str + " ";
                if (geometryDef.HasZ)
                {
                    str = str + "Z";
                }
                if (geometryDef.HasM)
                {
                    str = str + "M";
                }
            }
            return str;
        }

        private void gridView1_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (this.m_CanDo)
            {
                object obj2;
                ISubtypes featureClass = this.m_pFeatLayer.FeatureClass as ISubtypes;
                GridEditorItem row = this.gridView1.GetRow(e.RowHandle) as GridEditorItem;
                int index = this.m_pFeatLayer.FeatureClass.Fields.FindFieldByAliasName(row.Name);
                IField pField = this.m_pFeatLayer.FeatureClass.Fields.get_Field(index);
                if ((featureClass != null) && featureClass.HasSubtype)
                {
                    if (featureClass.SubtypeFieldName == pField.Name)
                    {
                        IEnumSubtype subtypes = featureClass.Subtypes;
                        subtypes.Reset();
                        int subtypeCode = 0;
                        for (string str = subtypes.Next(out subtypeCode); str != null; str = subtypes.Next(out subtypeCode))
                        {
                            if (e.Value.ToString() == str)
                            {
                                this.UpdateFieldValue(pField, subtypeCode);
                                break;
                            }
                        }
                    }
                    else if (e.Value.ToString() == "<空>")
                    {
                        obj2 = DBNull.Value;
                        this.UpdateFieldValue(pField, obj2);
                    }
                    else
                    {
                        this.UpdateFieldValue(pField, e.Value);
                    }
                }
                else if (e.Value.ToString() == "<空>")
                {
                    obj2 = DBNull.Value;
                    this.UpdateFieldValue(pField, obj2);
                }
                else
                {
                    this.UpdateFieldValue(pField, e.Value);
                }
            }
        }

        private void Init()
        {
            this.m_pVertXtraGrid.Clear();
            if (this.m_pFeatLayer == null)
            {
                this.m_CanDo = true;
            }
            else
            {
                IFields fields = this.m_pFeatLayer.FeatureClass.Fields;
                string[] strArray = new string[2];
                ISubtypes featureClass = this.m_pFeatLayer.FeatureClass as ISubtypes;
                IDomain domain = null;
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    IField pField = fields.get_Field(i);
                    strArray[0] = pField.AliasName;
                    if (pField.Type == esriFieldType.esriFieldTypeGeometry)
                    {
                        strArray[1] = this.GetShapeString(pField);
                        this.m_pVertXtraGrid.AddTextEdit(strArray[0], strArray[1], true);
                    }
                    else if (pField.Type == esriFieldType.esriFieldTypeBlob)
                    {
                        strArray[1] = "<二进制数据>";
                        this.m_pVertXtraGrid.AddTextEdit(strArray[0], strArray[1], true);
                    }
                    else
                    {
                        ICodedValueDomain domain2 = null;
                        IList list = new ArrayList();
                        if ((featureClass != null) && featureClass.HasSubtype)
                        {
                            if (featureClass.SubtypeFieldName == pField.Name)
                            {
                                int num2;
                                strArray[1] = "<空>";
                                IEnumSubtype subtypes = featureClass.Subtypes;
                                subtypes.Reset();
                                for (string str = subtypes.Next(out num2); str != null; str = subtypes.Next(out num2))
                                {
                                    list.Add(str);
                                }
                                this.m_pVertXtraGrid.AddComBoBox(strArray[0], strArray[1], list, !pField.Editable);
                            }
                            else
                            {
                                strArray[1] = "<空>";
                                this.m_pVertXtraGrid.AddTextEdit(strArray[0], strArray[1], !pField.Editable);
                            }
                        }
                        else
                        {
                            domain = pField.Domain;
                            if (domain != null)
                            {
                                if (domain is ICodedValueDomain)
                                {
                                    domain2 = domain as ICodedValueDomain;
                                    if (pField.IsNullable)
                                    {
                                        list.Add("<空>");
                                    }
                                    strArray[1] = "<空>";
                                    for (int j = 0; j < domain2.CodeCount; j++)
                                    {
                                        list.Add(domain2.get_Name(j));
                                    }
                                    this.m_pVertXtraGrid.AddComBoBox(strArray[0], strArray[1], list, !pField.Editable);
                                }
                                else
                                {
                                    strArray[1] = "<空>";
                                    if ((((pField.Type == esriFieldType.esriFieldTypeSmallInteger) || (pField.Type == esriFieldType.esriFieldTypeSingle)) || (pField.Type == esriFieldType.esriFieldTypeDouble)) || (pField.Type == esriFieldType.esriFieldTypeInteger))
                                    {
                                        double minValue = 0.0;
                                        double maxValue = 0.0;
                                        if (domain is IRangeDomain)
                                        {
                                            minValue = (double) (domain as IRangeDomain).MinValue;
                                            maxValue = (double) (domain as IRangeDomain).MaxValue;
                                        }
                                        if (pField.Editable)
                                        {
                                            this.m_pVertXtraGrid.AddSpinEdit(strArray[0], strArray[1], false, minValue, maxValue);
                                        }
                                        else
                                        {
                                            this.m_pVertXtraGrid.AddTextEdit(strArray[0], strArray[1], true);
                                        }
                                    }
                                    else if (pField.Type == esriFieldType.esriFieldTypeDate)
                                    {
                                        this.m_pVertXtraGrid.AddDateEdit(strArray[0], strArray[1], !pField.Editable);
                                    }
                                    else
                                    {
                                        this.m_pVertXtraGrid.AddTextEdit(strArray[0], strArray[1], !pField.Editable);
                                    }
                                }
                            }
                            else
                            {
                                strArray[1] = "<空>";
                                if ((((pField.Type == esriFieldType.esriFieldTypeSmallInteger) || (pField.Type == esriFieldType.esriFieldTypeSingle)) || (pField.Type == esriFieldType.esriFieldTypeDouble)) || (pField.Type == esriFieldType.esriFieldTypeInteger))
                                {
                                    if (pField.Editable)
                                    {
                                        this.m_pVertXtraGrid.AddSpinEdit(strArray[0], strArray[1], false, 0.0, 0.0);
                                    }
                                    else
                                    {
                                        this.m_pVertXtraGrid.AddTextEdit(strArray[0], strArray[1], true);
                                    }
                                }
                                else if (pField.Type == esriFieldType.esriFieldTypeDate)
                                {
                                    this.m_pVertXtraGrid.AddDateEdit(strArray[0], strArray[1], !pField.Editable);
                                }
                                else
                                {
                                    this.m_pVertXtraGrid.AddTextEdit(strArray[0], strArray[1], !pField.Editable);
                                }
                            }
                        }
                    }
                }
                this.m_CanDo = true;
            }
        }

        private void InitializeComponent()
        {
            this.gridControl1 = new GridControl();
            this.gridView1 = new GridView();
            this.gridControl1.BeginInit();
            this.gridView1.BeginInit();
            base.SuspendLayout();
            this.gridControl1.Dock = DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Name = "";
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new Size(0x128, 0xe0);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new BaseView[] { this.gridView1 });
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            this.gridView1.ShowButtonMode = ShowButtonModeEnum.ShowOnlyInEditor;
            base.Controls.Add(this.gridControl1);
            base.Name = "MultiAttributeListControl";
            base.Size = new Size(0x128, 0xe0);
            base.Load += new EventHandler(this.AttributeListControl_Load);
            this.gridControl1.EndInit();
            this.gridView1.EndInit();
            base.ResumeLayout(false);
        }

        private bool UpdateFieldValue(IField pField, object str)
        {
            IWorkspaceEdit workspace = (this.m_pFeatLayer.FeatureClass as IDataset).Workspace as IWorkspaceEdit;
            bool flag = false;
            try
            {
                ICursor cursor;
                workspace.StartEditOperation();
                int index = this.m_pFeatLayer.FeatureClass.Fields.FindField(pField.Name);
                (this.m_pFeatLayer as IFeatureSelection).SelectionSet.Search(null, false, out cursor);
                for (IRow row = cursor.NextRow(); row != null; row = cursor.NextRow())
                {
                    row.set_Value(index, str);
                    row.Store();
                }
                ComReleaser.ReleaseCOMObject(cursor);
                workspace.StopEditOperation();
                try
                {
                }
                catch
                {
                }
                flag = true;
            }
            catch (Exception exception)
            {
                MessageBox.Show("输入数据格式错误");
                CErrorLog.writeErrorLog(null, exception, "");
            }
            return flag;
        }

        private void UpdateGrid()
        {
            IFields fields = this.m_pFeatLayer.FeatureClass.Fields;
            ISubtypes featureClass = this.m_pFeatLayer.FeatureClass as ISubtypes;
            for (int i = 0; i < fields.FieldCount; i++)
            {
                IField field = fields.get_Field(i);
                if (((((field.Type != esriFieldType.esriFieldTypeGeometry) && (field.Type != esriFieldType.esriFieldTypeOID)) && (field.Type != esriFieldType.esriFieldTypeRaster)) && field.Editable) && !(featureClass.SubtypeFieldName == field.Name))
                {
                }
            }
        }

        public IActiveView ActiveView
        {
            set
            {
                this.m_pActiveView = value;
            }
        }

        public IFeatureLayer SelectLayer
        {
            set
            {
                this.m_pFeatLayer = value;
                if (this.m_pFeatLayer == null)
                {
                    this.m_pVertXtraGrid.Clear();
                }
                else if (this.m_CanDo)
                {
                    this.m_CanDo = false;
                    this.Init();
                    this.m_CanDo = true;
                }
            }
        }
    }
}

