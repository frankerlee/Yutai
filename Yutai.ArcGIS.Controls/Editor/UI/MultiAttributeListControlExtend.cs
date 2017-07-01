using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.CodeDomainEx;
using Yutai.ArcGIS.Common.ControlExtend;
using Yutai.ArcGIS.Common.Display;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.ArcGIS.Common.Wrapper;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    public partial class MultiAttributeListControlExtend : UserControl
    {
        private bool m_CanDo = false;
        private IList m_LayerList = null;
        private IActiveView m_pActiveView = null;
        private IFeatureLayer m_pFeatLayer = null;
        private VertXtraGrid m_pVertXtraGrid = null;

        public MultiAttributeListControlExtend()
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

        private void cboLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.m_pVertXtraGrid.Clear();
            if (this.cboLayer.SelectedIndex != -1)
            {
                LayerObject selectedItem = this.cboLayer.SelectedItem as LayerObject;
                IFeatureLayer pLayer = selectedItem.Layer as IFeatureLayer;
                this.m_pFeatLayer = pLayer;
                if (pLayer != null)
                {
                    ICursor cursor;
                    (pLayer as IFeatureSelection).SelectionSet.Search(null, false, out cursor);
                    IList<IRow> pLists = new List<IRow>();
                    for (IRow row = cursor.NextRow(); row != null; row = cursor.NextRow())
                    {
                        pLists.Add(row);
                    }
                    ComReleaser.ReleaseCOMObject(cursor);
                    IFields fields = pLayer.FeatureClass.Fields;
                    string[] strArray = new string[2];
                    ISubtypes featureClass = pLayer.FeatureClass as ISubtypes;
                    IDomain domain = null;
                    for (int i = 0; i < fields.FieldCount; i++)
                    {
                        IField pField = fields.get_Field(i);
                        if (this.CheckFieldIsVisible(pLayer, pField))
                        {
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
                            else if ((pField.Type != esriFieldType.esriFieldTypeOID) && pField.Editable)
                            {
                                int num3;
                                double minValue;
                                double maxValue;
                                ICodedValueDomain domain2 = null;
                                IList list2 = new ArrayList();
                                if ((featureClass != null) && featureClass.HasSubtype)
                                {
                                    if (featureClass.SubtypeFieldName == pField.Name)
                                    {
                                        int num2;
                                        if (this.CheckValueIsEqual(pLists, i))
                                        {
                                            object obj4 = pLists[0].get_Value(i);
                                            if (obj4 is DBNull)
                                            {
                                                strArray[1] = "<空>";
                                            }
                                            else
                                            {
                                                try
                                                {
                                                    strArray[1] =
                                                        featureClass.get_SubtypeName(
                                                            (pLists[0] as IRowSubtypes).SubtypeCode);
                                                }
                                                catch
                                                {
                                                    strArray[1] = obj4.ToString();
                                                }
                                            }
                                        }
                                        else
                                        {
                                            strArray[1] = "<空>";
                                        }
                                        IEnumSubtype subtypes = featureClass.Subtypes;
                                        subtypes.Reset();
                                        for (string str = subtypes.Next(out num2);
                                            str != null;
                                            str = subtypes.Next(out num2))
                                        {
                                            list2.Add(str);
                                        }
                                        this.m_pVertXtraGrid.AddComBoBox(strArray[0], strArray[1], list2,
                                            !pField.Editable);
                                    }
                                    else
                                    {
                                        domain = featureClass.get_Domain((pLists[0] as IRowSubtypes).SubtypeCode,
                                            pField.Name);
                                        if (domain is ICodedValueDomain)
                                        {
                                            domain2 = domain as ICodedValueDomain;
                                            if (pField.IsNullable)
                                            {
                                                list2.Add("<空>");
                                            }
                                            if (this.CheckValueIsEqual(pLists, i))
                                            {
                                                strArray[1] = pLists[0].get_Value(i).ToString();
                                            }
                                            else
                                            {
                                                strArray[1] = "<空>";
                                            }
                                            num3 = 0;
                                            while (num3 < domain2.CodeCount)
                                            {
                                                list2.Add(domain2.get_Name(num3));
                                                if (strArray[1] == domain2.get_Value(num3).ToString())
                                                {
                                                    strArray[1] = domain2.get_Name(num3);
                                                }
                                                num3++;
                                            }
                                            this.m_pVertXtraGrid.AddComBoBox(strArray[0], strArray[1], list2,
                                                !pField.Editable);
                                        }
                                        else
                                        {
                                            if (this.CheckValueIsEqual(pLists, i))
                                            {
                                                strArray[1] = pLists[0].get_Value(i).ToString();
                                            }
                                            else
                                            {
                                                strArray[1] = "<空>";
                                            }
                                            if ((((pField.Type == esriFieldType.esriFieldTypeSmallInteger) ||
                                                  (pField.Type == esriFieldType.esriFieldTypeSingle)) ||
                                                 (pField.Type == esriFieldType.esriFieldTypeDouble)) ||
                                                (pField.Type == esriFieldType.esriFieldTypeInteger))
                                            {
                                                minValue = 0.0;
                                                maxValue = 0.0;
                                                if (domain is IRangeDomain)
                                                {
                                                    minValue = (double) (domain as IRangeDomain).MinValue;
                                                    maxValue = (double) (domain as IRangeDomain).MaxValue;
                                                }
                                                if (pField.Editable)
                                                {
                                                    this.m_pVertXtraGrid.AddSpinEdit(strArray[0], strArray[1], false,
                                                        minValue, maxValue);
                                                }
                                                else
                                                {
                                                    this.m_pVertXtraGrid.AddTextEdit(strArray[0], strArray[1], true);
                                                }
                                            }
                                            else if (pField.Type == esriFieldType.esriFieldTypeDate)
                                            {
                                                this.m_pVertXtraGrid.AddDateEdit(strArray[0], strArray[1],
                                                    !pField.Editable);
                                            }
                                            else
                                            {
                                                if (this.CheckValueIsEqual(pLists, i))
                                                {
                                                    strArray[1] = pLists[0].get_Value(i).ToString();
                                                }
                                                else
                                                {
                                                    strArray[1] = "<空>";
                                                }
                                                this.m_pVertXtraGrid.AddTextEdit(strArray[0], strArray[1],
                                                    !pField.Editable);
                                            }
                                        }
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
                                                list2.Add("<空>");
                                            }
                                            if (this.CheckValueIsEqual(pLists, i))
                                            {
                                                strArray[1] = pLists[0].get_Value(i).ToString();
                                            }
                                            else
                                            {
                                                strArray[1] = "<空>";
                                            }
                                            num3 = 0;
                                            while (num3 < domain2.CodeCount)
                                            {
                                                list2.Add(domain2.get_Name(num3));
                                                if (strArray[1] == domain2.get_Value(num3).ToString())
                                                {
                                                    strArray[1] = domain2.get_Name(num3);
                                                }
                                                num3++;
                                            }
                                            this.m_pVertXtraGrid.AddComBoBox(strArray[0], strArray[1], list2,
                                                !pField.Editable);
                                        }
                                        else
                                        {
                                            if (this.CheckValueIsEqual(pLists, i))
                                            {
                                                strArray[1] = pLists[0].get_Value(i).ToString();
                                            }
                                            else
                                            {
                                                strArray[1] = "<空>";
                                            }
                                            if ((((pField.Type == esriFieldType.esriFieldTypeSmallInteger) ||
                                                  (pField.Type == esriFieldType.esriFieldTypeSingle)) ||
                                                 (pField.Type == esriFieldType.esriFieldTypeDouble)) ||
                                                (pField.Type == esriFieldType.esriFieldTypeInteger))
                                            {
                                                minValue = 0.0;
                                                maxValue = 0.0;
                                                if (domain is IRangeDomain)
                                                {
                                                    minValue = (double) (domain as IRangeDomain).MinValue;
                                                    maxValue = (double) (domain as IRangeDomain).MaxValue;
                                                }
                                                if (pField.Editable)
                                                {
                                                    this.m_pVertXtraGrid.AddSpinEdit(strArray[0], strArray[1], false,
                                                        minValue, maxValue);
                                                }
                                                else
                                                {
                                                    this.m_pVertXtraGrid.AddTextEdit(strArray[0], strArray[1], true);
                                                }
                                            }
                                            else if (pField.Type == esriFieldType.esriFieldTypeDate)
                                            {
                                                this.m_pVertXtraGrid.AddDateEdit(strArray[0], strArray[1],
                                                    !pField.Editable);
                                            }
                                            else
                                            {
                                                this.m_pVertXtraGrid.AddTextEdit(strArray[0], strArray[1],
                                                    !pField.Editable);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (this.CheckValueIsEqual(pLists, i))
                                        {
                                            strArray[1] = pLists[0].get_Value(i).ToString();
                                        }
                                        else
                                        {
                                            strArray[1] = "<空>";
                                        }
                                        string name = (pLayer.FeatureClass as IDataset).Name;
                                        NameValueCollection codeDomain = CodeDomainManage.GetCodeDomain(pField.Name,
                                            name);
                                        if (codeDomain.Count > 0)
                                        {
                                            if (pField.IsNullable)
                                            {
                                                list2.Add("<空>");
                                            }
                                            for (num3 = 0; num3 < codeDomain.Count; num3++)
                                            {
                                                string str3 = codeDomain.Keys[num3];
                                                list2.Add(str3);
                                                if (strArray[1] == codeDomain[str3])
                                                {
                                                    strArray[1] = str3;
                                                }
                                            }
                                            this.m_pVertXtraGrid.AddComBoBox(strArray[0], strArray[1], list2,
                                                !pField.Editable);
                                        }
                                        else if ((((pField.Type == esriFieldType.esriFieldTypeSmallInteger) ||
                                                   (pField.Type == esriFieldType.esriFieldTypeSingle)) ||
                                                  (pField.Type == esriFieldType.esriFieldTypeDouble)) ||
                                                 (pField.Type == esriFieldType.esriFieldTypeInteger))
                                        {
                                            if (pField.Editable)
                                            {
                                                this.m_pVertXtraGrid.AddSpinEdit(strArray[0], strArray[1], false, 0.0,
                                                    0.0);
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
                    }
                }
            }
        }

        private bool CheckFieldIsVisible(ILayer pLayer, IField pField)
        {
            if (pLayer is ILayerFields)
            {
                int index = (pLayer as ILayerFields).FindField(pField.Name);
                if (index != -1)
                {
                    return (pLayer as ILayerFields).get_FieldInfo(index).Visible;
                }
            }
            return true;
        }

        private bool CheckValueIsEqual(IList<IRow> pLists, int nIndex)
        {
            IRow row = pLists[0];
            object obj2 = row.get_Value(nIndex);
            for (int i = 1; i < pLists.Count; i++)
            {
                object obj3 = pLists[i].get_Value(nIndex);
                if (!(!(obj2 is DBNull) || (obj3 is DBNull)))
                {
                    return false;
                }
                if (!(!(obj3 is DBNull) || (obj2 is DBNull)))
                {
                    return false;
                }
                if ((obj3 is DBNull) && (obj2 is DBNull))
                {
                    return false;
                }
                if (obj2 is string)
                {
                    if (obj2.ToString() != obj3.ToString())
                    {
                        return false;
                    }
                }
                else
                {
                    double num2 = double.Parse(obj2.ToString());
                    double num3 = double.Parse(obj3.ToString());
                    if (num2 != num3)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void FlashObject()
        {
            try
            {
                LayerObject selectedItem = this.cboLayer.SelectedItem as LayerObject;
                IFeatureLayer layer = selectedItem.Layer as IFeatureLayer;
                if (layer != null)
                {
                    Flash.FlashSelectedFeature(this.m_pActiveView.ScreenDisplay, layer);
                }
            }
            catch
            {
            }
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
                if (this.m_pFeatLayer == null)
                {
                    LayerObject selectedItem = this.cboLayer.SelectedItem as LayerObject;
                    IFeatureLayer layer = selectedItem.Layer as IFeatureLayer;
                    this.m_pFeatLayer = layer;
                }
                if (this.m_pFeatLayer != null)
                {
                    object obj3;
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
                            for (string str = subtypes.Next(out subtypeCode);
                                str != null;
                                str = subtypes.Next(out subtypeCode))
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
                            obj3 = DBNull.Value;
                            this.UpdateFieldValue(pField, obj3);
                        }
                        else
                        {
                            this.UpdateFieldValue(pField, e.Value);
                        }
                    }
                    else if (e.Value.ToString() == "<空>")
                    {
                        obj3 = DBNull.Value;
                        this.UpdateFieldValue(pField, obj3);
                    }
                    else
                    {
                        int num3;
                        string name = (this.m_pFeatLayer.FeatureClass as IDataset).Name;
                        NameValueCollection codeDomain = CodeDomainManage.GetCodeDomain(pField.Name, name);
                        if (codeDomain.Count > 0)
                        {
                            for (num3 = 0; num3 < codeDomain.Count; num3++)
                            {
                                string str3 = codeDomain.Keys[num3];
                                if (str3 == e.Value.ToString())
                                {
                                    this.UpdateFieldValue(pField, codeDomain[str3]);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            IDomain domain = pField.Domain;
                            if (domain is ICodedValueDomain)
                            {
                                ICodedValueDomain domain2 = domain as ICodedValueDomain;
                                for (num3 = 0; num3 < domain2.CodeCount; num3++)
                                {
                                    if (domain2.get_Name(num3) == e.Value.ToString())
                                    {
                                        this.UpdateFieldValue(pField, domain2.get_Value(num3));
                                        break;
                                    }
                                }
                            }
                            else if (this.UpdateFieldValue(pField, e.Value))
                            {
                                this.m_CanDo = false;
                                this.m_CanDo = true;
                            }
                        }
                    }
                }
            }
        }

        private void Init()
        {
            this.cboLayer.Properties.Items.Clear();
            this.m_pVertXtraGrid.Clear();
            this.m_CanDo = true;
            if ((this.m_LayerList != null) && (this.m_LayerList.Count != 0))
            {
                for (int i = 0; i < this.m_LayerList.Count; i++)
                {
                    this.cboLayer.Properties.Items.Add(new LayerObject(this.m_LayerList[i] as ILayer));
                }
                this.cboLayer.SelectedIndex = 0;
            }
        }

        private void panel3_Resize(object sender, EventArgs e)
        {
        }

        private void panel3_SizeChanged(object sender, EventArgs e)
        {
            this.cboLayer.Width = this.panel3.Width - this.cboLayer.Left;
        }

        private bool UpdateFieldValue(IField pField, object str)
        {
            if (this.cboLayer.SelectedIndex == -1)
            {
                return false;
            }
            LayerObject selectedItem = this.cboLayer.SelectedItem as LayerObject;
            IFeatureLayer layer = selectedItem.Layer as IFeatureLayer;
            if (layer == null)
            {
                return false;
            }
            IWorkspaceEdit editWorkspace = Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace;
            bool flag = false;
            try
            {
                ICursor cursor;
                editWorkspace.StartEditOperation();
                int index = layer.FeatureClass.Fields.FindField(pField.Name);
                (layer as IFeatureSelection).SelectionSet.Search(null, false, out cursor);
                for (IRow row = cursor.NextRow(); row != null; row = cursor.NextRow())
                {
                    row.set_Value(index, str);
                    row.Store();
                }
                ComReleaser.ReleaseCOMObject(cursor);
                editWorkspace.StopEditOperation();
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
            if (this.cboLayer.SelectedIndex != -1)
            {
                LayerObject selectedItem = this.cboLayer.SelectedItem as LayerObject;
                IFeatureLayer layer = selectedItem.Layer as IFeatureLayer;
                if (layer != null)
                {
                    IFields fields = layer.FeatureClass.Fields;
                    ISubtypes featureClass = layer.FeatureClass as ISubtypes;
                    for (int i = 0; i < fields.FieldCount; i++)
                    {
                        IField field = fields.get_Field(i);
                        if (((((field.Type != esriFieldType.esriFieldTypeGeometry) &&
                               (field.Type != esriFieldType.esriFieldTypeOID)) &&
                              (field.Type != esriFieldType.esriFieldTypeRaster)) && field.Editable) &&
                            !(featureClass.SubtypeFieldName == field.Name))
                        {
                        }
                    }
                }
            }
        }

        public void ZoomToSelectObject()
        {
            try
            {
                LayerObject selectedItem = this.cboLayer.SelectedItem as LayerObject;
                IFeatureLayer layer = selectedItem.Layer as IFeatureLayer;
                if (layer != null)
                {
                    if (this.m_pActiveView is IScene)
                    {
                        CommonHelper.Zoom2SelectedFeature(this.m_pActiveView as IScene);
                    }
                    else
                    {
                        CommonHelper.Zoom2SelectedFeature(this.m_pActiveView);
                    }
                }
            }
            catch
            {
            }
        }

        public IActiveView ActiveView
        {
            set { this.m_pActiveView = value; }
        }

        public IList LayerList
        {
            set
            {
                this.m_LayerList = value;
                if ((this.m_LayerList == null) || (this.m_LayerList.Count == 0))
                {
                    this.cboLayer.Properties.Items.Clear();
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