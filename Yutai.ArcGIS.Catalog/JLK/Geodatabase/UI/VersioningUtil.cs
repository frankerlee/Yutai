namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.ADF;
    using ESRI.ArcGIS.Carto;
    using ESRI.ArcGIS.Display;
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.Geodatabase;
    using ESRI.ArcGIS.Geometry;
    using System;
    using System.Collections;
    using System.Windows.Forms;

    internal class VersioningUtil
    {
        private bool bool_0;
        private bool bool_1;
        private IFeatureClass ifeatureClass_0;
        private IFeatureClass ifeatureClass_1;

        public void CheckDatasetForDifferences(TreeNodeCollection treeNodeCollection_0, IFeatureWorkspace ifeatureWorkspace_0, IFeatureWorkspace ifeatureWorkspace_1, IFeatureWorkspace ifeatureWorkspace_2, string string_0, string string_1, IList ilist_0)
        {
            for (int i = 0; i < treeNodeCollection_0.Count; i++)
            {
                if (treeNodeCollection_0[i].Nodes.Count > 0)
                {
                    this.CheckDatasetForDifferences(treeNodeCollection_0[i].Nodes, ifeatureWorkspace_0, ifeatureWorkspace_1, ifeatureWorkspace_2, string_0, string_1, ilist_0);
                }
                else if ((treeNodeCollection_0[i].Nodes.Count == 0) && treeNodeCollection_0[i].Checked)
                {
                    IDataset dataset = ifeatureWorkspace_0.OpenFeatureClass(treeNodeCollection_0[i].Text) as IDataset;
                    if (dataset != null)
                    {
                        this.method_0(ifeatureWorkspace_1, ifeatureWorkspace_2, string_0, string_1, dataset, ilist_0);
                    }
                }
            }
        }

        public void DrawDifferenceGeometry(IGeometry igeometry_0, IScreenDisplay iscreenDisplay_0)
        {
            IRgbColor color = new RgbColorClass {
                Red = 0xff
            };
            iscreenDisplay_0.StartDrawing(iscreenDisplay_0.hDC, -1);
            switch (igeometry_0.GeometryType)
            {
                case esriGeometryType.esriGeometryPoint:
                {
                    ISimpleMarkerSymbol symbol = new SimpleMarkerSymbolClass {
                        Color = color,
                        Size = 10.0
                    };
                    iscreenDisplay_0.SetSymbol(symbol as ISymbol);
                    iscreenDisplay_0.DrawPoint(igeometry_0);
                    break;
                }
                case esriGeometryType.esriGeometryPolyline:
                {
                    ISimpleLineSymbol symbol2 = new SimpleLineSymbolClass {
                        Color = color,
                        Width = 2.0
                    };
                    iscreenDisplay_0.SetSymbol(symbol2 as ISymbol);
                    iscreenDisplay_0.DrawPolyline(igeometry_0);
                    break;
                }
                case esriGeometryType.esriGeometryPolygon:
                {
                    ISimpleFillSymbol symbol3 = new SimpleFillSymbolClass {
                        Outline = { Color = color, Width = 2.0 },
                        Style = esriSimpleFillStyle.esriSFSForwardDiagonal
                    };
                    iscreenDisplay_0.SetSymbol(symbol3 as ISymbol);
                    iscreenDisplay_0.DrawPolygon(igeometry_0);
                    break;
                }
            }
            iscreenDisplay_0.FinishDrawing();
        }

        public void GetDifferences1(IWorkspace iworkspace_0, string string_0, string string_1, TreeNodeCollection treeNodeCollection_0, IList ilist_0)
        {
            try
            {
                this.bool_0 = false;
                IFeatureWorkspace workspace = iworkspace_0 as IFeatureWorkspace;
                IVersionedWorkspace workspace2 = iworkspace_0 as IVersionedWorkspace;
                IFeatureWorkspace workspace3 = workspace2.FindVersion(string_0) as IFeatureWorkspace;
                IFeatureWorkspace workspace4 = workspace2.FindVersion(string_1) as IFeatureWorkspace;
                ilist_0.Clear();
                ilist_0.Add("Version Difference Report - " + DateTime.Now.ToLongDateString());
                ilist_0.Add("Parent/Grandparent/.. Version: " + string_0);
                ilist_0.Add("Child Version: " + string_1);
                ilist_0.Add("________________________________________________________");
                this.CheckDatasetForDifferences(treeNodeCollection_0, workspace, workspace3, workspace4, string_0, string_1, ilist_0);
                if (!this.bool_0)
                {
                    ilist_0.Add("No differences found.");
                }
            }
            catch
            {
            }
        }

        private void method_0(IFeatureWorkspace ifeatureWorkspace_0, IFeatureWorkspace ifeatureWorkspace_1, string string_0, string string_1, IDataset idataset_0, IList ilist_0)
        {
            IFeatureClass class2 = ifeatureWorkspace_0.OpenFeatureClass(idataset_0.Name);
            IFeatureClass class3 = ifeatureWorkspace_1.OpenFeatureClass(idataset_0.Name);
            this.ifeatureClass_1 = class2;
            this.ifeatureClass_0 = class3;
            this.method_5(class2 as IVersionedTable, class3 as IVersionedTable, string_0, string_1, idataset_0.Name, ilist_0);
        }

        private void method_1(IVersionedTable iversionedTable_0, IVersionedTable iversionedTable_1, string string_0, string string_1, esriDifferenceType esriDifferenceType_0, string string_2, bool bool_2, IList ilist_0)
        {
            try
            {
                int num;
                IRow row;
                IQueryFilter queryFilter = new QueryFilterClass();
                IObjectClass class2 = iversionedTable_0 as IObjectClass;
                queryFilter.SubFields = class2.OIDFieldName;
                IDifferenceCursor cursor = iversionedTable_0.Differences(iversionedTable_1 as ITable, esriDifferenceType_0, queryFilter);
                cursor.Next(out num, out row);
                while (num != -1)
                {
                    if (bool_2)
                    {
                        this.method_2(num, esriDifferenceType_0, string_1, string_0, string_2, ilist_0);
                    }
                    else
                    {
                        this.method_2(num, esriDifferenceType_0, string_0, string_1, string_2, ilist_0);
                    }
                    cursor.Next(out num, out row);
                }
            }
            catch
            {
            }
        }

        private void method_10(IWorkspaceEdit iworkspaceEdit_0, bool bool_2)
        {
            if (bool_2)
            {
                iworkspaceEdit_0.StopEditOperation();
                iworkspaceEdit_0.StopEditing(true);
            }
            else
            {
                iworkspaceEdit_0.AbortEditOperation();
                iworkspaceEdit_0.StopEditing(false);
            }
        }

        private void method_2(int int_0, esriDifferenceType esriDifferenceType_0, string string_0, string string_1, string string_2, IList ilist_0)
        {
            string str = "";
            this.bool_0 = true;
            switch (esriDifferenceType_0)
            {
                case esriDifferenceType.esriDifferenceTypeInsert:
                    str = "版本 " + string_0 + " 中插入行";
                    break;

                case esriDifferenceType.esriDifferenceTypeDeleteNoChange:
                    str = "版本 " + string_0 + " 中删除了行，而在版本 " + string_1 + " 没有变化";
                    break;

                case esriDifferenceType.esriDifferenceTypeUpdateNoChange:
                    str = "版本 " + string_0 + " 中更新了记录，而在版本 " + string_1 + " 没有变化";
                    break;

                case esriDifferenceType.esriDifferenceTypeUpdateUpdate:
                    str = "版本 " + string_0 + " 和 " + string_1 + "都更新了该行";
                    break;

                case esriDifferenceType.esriDifferenceTypeUpdateDelete:
                    str = "版本 " + string_0 + " 中更新了该行，而在版本 " + string_1 + "删除了该行";
                    break;

                case esriDifferenceType.esriDifferenceTypeDeleteUpdate:
                    str = "版本 " + string_0 + " 中删除了该行，而在版本 " + string_1 + " 中更新了该行";
                    break;
            }
            ilist_0.Add(string_2.ToUpper() + ": 对象OID " + int_0.ToString());
            ilist_0.Add(str);
            if (((esriDifferenceType_0 == esriDifferenceType.esriDifferenceTypeUpdateUpdate) || (esriDifferenceType_0 == esriDifferenceType.esriDifferenceTypeUpdateDelete)) || (esriDifferenceType_0 == esriDifferenceType.esriDifferenceTypeDeleteUpdate))
            {
                ilist_0.Add("发现冲突");
            }
            if ((esriDifferenceType_0 == esriDifferenceType.esriDifferenceTypeUpdateNoChange) || (esriDifferenceType_0 == esriDifferenceType.esriDifferenceTypeUpdateUpdate))
            {
                this.method_3(int_0, string_0, string_1, ilist_0);
            }
            ilist_0.Add("________________________________________________________");
        }

        private void method_3(int int_0, string string_0, string string_1, IList ilist_0)
        {
            try
            {
                IFeature feature = this.ifeatureClass_0.GetFeature(int_0);
                IFeature feature2 = this.ifeatureClass_1.GetFeature(int_0);
                string str = (feature.Class as IFeatureClass).ShapeFieldName.ToUpper();
                ilist_0.Add("");
                for (int i = 0; i < feature.Fields.FieldCount; i++)
                {
                    if (!this.method_4(feature.get_Value(i), feature2.get_Value(i)))
                    {
                        if (((feature.Fields.get_Field(i).Name.ToUpper() == str) || (feature.Fields.get_Field(i).Name.ToUpper() == "SHAPE.AREA")) || (feature.Fields.get_Field(i).Name.ToUpper() == "SHAPE.LEN"))
                        {
                            if (!this.bool_1)
                            {
                                this.bool_1 = true;
                            }
                        }
                        else
                        {
                            string str2 = this.method_6(feature, i);
                            string str3 = this.method_6(feature2, i);
                            if (str2.Length > 0)
                            {
                                ilist_0.Add("* 字段 " + feature.Fields.get_Field(i).Name.ToUpper() + " 变化: ");
                                ilist_0.Add("  - 版本 " + string_0 + ": " + str3);
                                ilist_0.Add("  - 版本 " + string_1 + ": " + str2);
                            }
                        }
                    }
                }
                if (this.bool_1)
                {
                    ilist_0.Add("* GEOMETRY变化");
                    this.bool_1 = false;
                }
            }
            catch
            {
            }
        }

        private bool method_4(object object_0, object object_1)
        {
            try
            {
                bool flag = false;
                bool flag2 = false;
                flag = object_0 is DBNull;
                flag2 = object_1 is DBNull;
                if (flag != flag2)
                {
                    return false;
                }
                if (flag && flag2)
                {
                    return true;
                }
                if ((object_0 is IClone) && (object_1 is IClone))
                {
                    IClone clone = object_0 as IClone;
                    return clone.IsEqual(object_1 as IClone);
                }
                if (!(!(object_0 is IClone) || (object_1 is IClone)))
                {
                    return false;
                }
                if (!(!(object_1 is IClone) || (object_0 is IClone)))
                {
                    return false;
                }
                bool flag4 = false;
                if (object_0.ToString() == object_1.ToString())
                {
                    flag4 = true;
                }
                return flag4;
            }
            catch
            {
            }
            return false;
        }

        private void method_5(IVersionedTable iversionedTable_0, IVersionedTable iversionedTable_1, string string_0, string string_1, string string_2, IList ilist_0)
        {
            this.bool_1 = false;
            this.method_1(iversionedTable_1, iversionedTable_0, string_0, string_1, esriDifferenceType.esriDifferenceTypeDeleteNoChange, string_2, true, ilist_0);
            this.method_1(iversionedTable_1, iversionedTable_0, string_0, string_1, esriDifferenceType.esriDifferenceTypeDeleteUpdate, string_2, true, ilist_0);
            this.method_1(iversionedTable_1, iversionedTable_0, string_0, string_1, esriDifferenceType.esriDifferenceTypeInsert, string_2, true, ilist_0);
            this.method_1(iversionedTable_1, iversionedTable_0, string_0, string_1, esriDifferenceType.esriDifferenceTypeUpdateDelete, string_2, true, ilist_0);
            this.method_1(iversionedTable_1, iversionedTable_0, string_0, string_1, esriDifferenceType.esriDifferenceTypeUpdateNoChange, string_2, true, ilist_0);
            this.method_1(iversionedTable_1, iversionedTable_0, string_0, string_1, esriDifferenceType.esriDifferenceTypeUpdateUpdate, string_2, true, ilist_0);
            this.method_1(iversionedTable_0, iversionedTable_1, string_0, string_1, esriDifferenceType.esriDifferenceTypeDeleteNoChange, string_2, false, ilist_0);
            this.method_1(iversionedTable_0, iversionedTable_1, string_0, string_1, esriDifferenceType.esriDifferenceTypeInsert, string_2, false, ilist_0);
            this.method_1(iversionedTable_0, iversionedTable_1, string_0, string_1, esriDifferenceType.esriDifferenceTypeUpdateNoChange, string_2, false, ilist_0);
        }

        private string method_6(IFeature ifeature_0, int int_0)
        {
            try
            {
                IDomain domain = ifeature_0.Fields.get_Field(int_0).Domain;
                if (domain == null)
                {
                    return ifeature_0.get_Value(int_0).ToString();
                }
                if (domain.Type != esriDomainType.esriDTCodedValue)
                {
                    return ifeature_0.get_Value(int_0).ToString();
                }
                ICodedValueDomain domain2 = domain as ICodedValueDomain;
                int codeCount = domain2.CodeCount;
                for (int i = 0; i < codeCount; i++)
                {
                    if (domain2.get_Value(i) == ifeature_0.get_Value(int_0))
                    {
                        return domain2.get_Name(i);
                    }
                }
            }
            catch
            {
            }
            return "";
        }

        private bool method_7(IVersionedWorkspace iversionedWorkspace_0, IEnumVersionInfo ienumVersionInfo_0, string string_0, bool bool_2, bool bool_3)
        {
            bool flag = true;
            try
            {
                for (IVersionInfo info = ienumVersionInfo_0.Next(); info != null; info = ienumVersionInfo_0.Next())
                {
                    IVersionEdit edit;
                    if (bool_2)
                    {
                        if (!this.method_7(iversionedWorkspace_0, info.Children, info.VersionName, bool_2, bool_3))
                        {
                            flag = false;
                        }
                        else
                        {
                            edit = iversionedWorkspace_0.FindVersion(info.VersionName) as IVersionEdit;
                            if (!this.method_8(edit, string_0, bool_2, bool_3))
                            {
                                flag = true;
                            }
                        }
                    }
                    else
                    {
                        edit = iversionedWorkspace_0.FindVersion(info.VersionName) as IVersionEdit;
                        if (this.method_8(edit, string_0, bool_2, bool_3))
                        {
                            this.method_7(iversionedWorkspace_0, info.Children, info.VersionName, bool_2, bool_3);
                        }
                    }
                }
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        private bool method_8(IVersionEdit iversionEdit_0, string string_0, bool bool_2, bool bool_3)
        {
            bool flag = true;
            try
            {
                if (iversionEdit_0.Reconcile(string_0))
                {
                    this.method_10(iversionEdit_0 as IWorkspaceEdit, false);
                    flag = false;
                }
                else if (iversionEdit_0.CanPost() && bool_2)
                {
                    iversionEdit_0.Post(string_0);
                    this.method_10(iversionEdit_0 as IWorkspaceEdit, true);
                    if (!bool_3)
                    {
                    }
                }
                else
                {
                    this.method_10(iversionEdit_0 as IWorkspaceEdit, true);
                }
                if (!flag)
                {
                }
            }
            catch
            {
                this.method_10(iversionEdit_0 as IWorkspaceEdit, false);
                flag = false;
            }
            return flag;
        }

        private void method_9(IWorkspaceEdit iworkspaceEdit_0)
        {
            iworkspaceEdit_0.StartEditing(false);
            iworkspaceEdit_0.StartEditOperation();
        }

        public void ShowDifferenceGeometry(IVersionedWorkspace iversionedWorkspace_0, int int_0, IMap imap_0, string string_0, string string_1)
        {
            try
            {
                IQueryFilter filter = new QueryFilterClass();
                IDataset featureClass = null;
                IFeatureWorkspace workspace = iversionedWorkspace_0.FindVersion(string_1) as IFeatureWorkspace;
                for (int i = 0; i < imap_0.LayerCount; i++)
                {
                    IFeatureLayer layer = imap_0.get_Layer(i) as IFeatureLayer;
                    if (layer != null)
                    {
                        featureClass = layer.FeatureClass as IDataset;
                        if (featureClass.Name.ToUpper() == string_0.ToUpper())
                        {
                            break;
                        }
                    }
                }
                if (featureClass != null)
                {
                    IFeatureClass class3 = workspace.OpenFeatureClass(featureClass.Name);
                    filter.WhereClause = "OBJECTID = " + int_0;
                    IFeatureCursor o = class3.Search(filter, false);
                    IFeature feature = o.NextFeature();
                    if (feature != null)
                    {
                        this.DrawDifferenceGeometry(feature.Shape, (imap_0 as IActiveView).ScreenDisplay);
                    }
                    ComReleaser.ReleaseCOMObject(o);
                }
            }
            catch
            {
            }
        }
    }
}

