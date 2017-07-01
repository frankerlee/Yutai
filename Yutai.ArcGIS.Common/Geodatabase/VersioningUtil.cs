using System;
using System.Collections;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.Geodatabase
{
    internal class VersioningUtil
    {
        private bool bool_0;

        private IFeatureClass ifeatureClass_0;

        private IFeatureClass ifeatureClass_1;

        private bool bool_1;

        public VersioningUtil()
        {
        }

        public void CheckDatasetForDifferences(TreeNodeCollection treeNodeCollection_0,
            IFeatureWorkspace ifeatureWorkspace_0, IFeatureWorkspace ifeatureWorkspace_1,
            IFeatureWorkspace ifeatureWorkspace_2, string string_0, string string_1, IList ilist_0)
        {
            for (int i = 0; i < treeNodeCollection_0.Count; i++)
            {
                if (treeNodeCollection_0[i].Nodes.Count > 0)
                {
                    this.CheckDatasetForDifferences(treeNodeCollection_0[i].Nodes, ifeatureWorkspace_0,
                        ifeatureWorkspace_1, ifeatureWorkspace_2, string_0, string_1, ilist_0);
                }
                else if (treeNodeCollection_0[i].Nodes.Count == 0 && treeNodeCollection_0[i].Checked)
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
            IRgbColor rgbColorClass = new RgbColor()
            {
                Red = 255
            };
            iscreenDisplay_0.StartDrawing(iscreenDisplay_0.hDC, -1);
            switch (igeometry_0.GeometryType)
            {
                case esriGeometryType.esriGeometryPoint:
                {
                    ISimpleMarkerSymbol simpleMarkerSymbolClass = new SimpleMarkerSymbol()
                    {
                        Color = rgbColorClass,
                        Size = 10
                    };
                    iscreenDisplay_0.SetSymbol(simpleMarkerSymbolClass as ISymbol);
                    iscreenDisplay_0.DrawPoint(igeometry_0);
                    iscreenDisplay_0.FinishDrawing();
                    return;
                }
                case esriGeometryType.esriGeometryMultipoint:
                {
                    iscreenDisplay_0.FinishDrawing();
                    return;
                }
                case esriGeometryType.esriGeometryPolyline:
                {
                    ISimpleLineSymbol simpleLineSymbolClass = new SimpleLineSymbol()
                    {
                        Color = rgbColorClass,
                        Width = 2
                    };
                    iscreenDisplay_0.SetSymbol(simpleLineSymbolClass as ISymbol);
                    iscreenDisplay_0.DrawPolyline(igeometry_0);
                    iscreenDisplay_0.FinishDrawing();
                    return;
                }
                case esriGeometryType.esriGeometryPolygon:
                {
                    ISimpleFillSymbol simpleFillSymbolClass = new SimpleFillSymbol();
                    simpleFillSymbolClass.Outline.Color = rgbColorClass;
                    simpleFillSymbolClass.Outline.Width = 2;
                    simpleFillSymbolClass.Style = esriSimpleFillStyle.esriSFSForwardDiagonal;
                    iscreenDisplay_0.SetSymbol(simpleFillSymbolClass as ISymbol);
                    iscreenDisplay_0.DrawPolygon(igeometry_0);
                    iscreenDisplay_0.FinishDrawing();
                    return;
                }
                default:
                {
                    iscreenDisplay_0.FinishDrawing();
                    return;
                }
            }
        }

        public void GetDifferences1(IWorkspace iworkspace_0, string string_0, string string_1,
            TreeNodeCollection treeNodeCollection_0, IList ilist_0)
        {
            try
            {
                this.bool_0 = false;
                IFeatureWorkspace iworkspace0 = iworkspace_0 as IFeatureWorkspace;
                IVersionedWorkspace versionedWorkspace = iworkspace_0 as IVersionedWorkspace;
                IFeatureWorkspace featureWorkspace = versionedWorkspace.FindVersion(string_0) as IFeatureWorkspace;
                IFeatureWorkspace featureWorkspace1 = versionedWorkspace.FindVersion(string_1) as IFeatureWorkspace;
                ilist_0.Clear();
                DateTime now = DateTime.Now;
                ilist_0.Add(string.Concat("Version Difference Report - ", now.ToLongDateString()));
                ilist_0.Add(string.Concat("Parent/Grandparent/.. Version: ", string_0));
                ilist_0.Add(string.Concat("Child Version: ", string_1));
                ilist_0.Add("________________________________________________________");
                this.CheckDatasetForDifferences(treeNodeCollection_0, iworkspace0, featureWorkspace, featureWorkspace1,
                    string_0, string_1, ilist_0);
                if (!this.bool_0)
                {
                    ilist_0.Add("No differences found.");
                }
            }
            catch
            {
            }
        }

        private void method_0(IFeatureWorkspace ifeatureWorkspace_0, IFeatureWorkspace ifeatureWorkspace_1,
            string string_0, string string_1, IDataset idataset_0, IList ilist_0)
        {
            IFeatureClass featureClass = ifeatureWorkspace_0.OpenFeatureClass(idataset_0.Name);
            IFeatureClass featureClass1 = ifeatureWorkspace_1.OpenFeatureClass(idataset_0.Name);
            this.ifeatureClass_1 = featureClass;
            this.ifeatureClass_0 = featureClass1;
            this.method_5(featureClass as IVersionedTable, featureClass1 as IVersionedTable, string_0, string_1,
                idataset_0.Name, ilist_0);
        }

        private void method_1(IVersionedTable iversionedTable_0, IVersionedTable iversionedTable_1, string string_0,
            string string_1, esriDifferenceType esriDifferenceType_0, string string_2, bool bool_2, IList ilist_0)
        {
            int num;
            IRow row;
            try
            {
                IQueryFilter queryFilterClass = new QueryFilter()
                {
                    SubFields = (iversionedTable_0 as IObjectClass).OIDFieldName
                };
                IDifferenceCursor differenceCursor = iversionedTable_0.Differences(iversionedTable_1 as ITable,
                    esriDifferenceType_0, queryFilterClass);
                differenceCursor.Next(out num, out row);
                while (num != -1)
                {
                    if (!bool_2)
                    {
                        this.method_2(num, esriDifferenceType_0, string_0, string_1, string_2, ilist_0);
                    }
                    else
                    {
                        this.method_2(num, esriDifferenceType_0, string_1, string_0, string_2, ilist_0);
                    }
                    differenceCursor.Next(out num, out row);
                }
            }
            catch
            {
            }
        }

        private void method_10(IWorkspaceEdit iworkspaceEdit_0, bool bool_2)
        {
            if (!bool_2)
            {
                iworkspaceEdit_0.AbortEditOperation();
                iworkspaceEdit_0.StopEditing(false);
            }
            else
            {
                iworkspaceEdit_0.StopEditOperation();
                iworkspaceEdit_0.StopEditing(true);
            }
        }

        private void method_2(int int_0, esriDifferenceType esriDifferenceType_0, string string_0, string string_1,
            string string_2, IList ilist_0)
        {
            string[] string0;
            string str = "";
            this.bool_0 = true;
            switch (esriDifferenceType_0)
            {
                case esriDifferenceType.esriDifferenceTypeInsert:
                {
                    str = string.Concat("版本 ", string_0, " 中插入行");
                    break;
                }
                case esriDifferenceType.esriDifferenceTypeDeleteNoChange:
                {
                    string0 = new string[] {"版本 ", string_0, " 中删除了行，而在版本 ", string_1, " 没有变化"};
                    str = string.Concat(string0);
                    break;
                }
                case esriDifferenceType.esriDifferenceTypeUpdateNoChange:
                {
                    string0 = new string[] {"版本 ", string_0, " 中更新了记录，而在版本 ", string_1, " 没有变化"};
                    str = string.Concat(string0);
                    break;
                }
                case esriDifferenceType.esriDifferenceTypeUpdateUpdate:
                {
                    string0 = new string[] {"版本 ", string_0, " 和 ", string_1, "都更新了该行"};
                    str = string.Concat(string0);
                    break;
                }
                case esriDifferenceType.esriDifferenceTypeUpdateDelete:
                {
                    string0 = new string[] {"版本 ", string_0, " 中更新了该行，而在版本 ", string_1, "删除了该行"};
                    str = string.Concat(string0);
                    break;
                }
                case esriDifferenceType.esriDifferenceTypeDeleteUpdate:
                {
                    string0 = new string[] {"版本 ", string_0, " 中删除了该行，而在版本 ", string_1, " 中更新了该行"};
                    str = string.Concat(string0);
                    break;
                }
            }
            ilist_0.Add(string.Concat(string_2.ToUpper(), ": 对象OID ", int_0.ToString()));
            ilist_0.Add(str);
            if ((esriDifferenceType_0 == esriDifferenceType.esriDifferenceTypeUpdateUpdate ||
                 esriDifferenceType_0 == esriDifferenceType.esriDifferenceTypeUpdateDelete
                ? true
                : esriDifferenceType_0 == esriDifferenceType.esriDifferenceTypeDeleteUpdate))
            {
                ilist_0.Add("发现冲突");
            }
            if ((esriDifferenceType_0 == esriDifferenceType.esriDifferenceTypeUpdateNoChange
                ? true
                : esriDifferenceType_0 == esriDifferenceType.esriDifferenceTypeUpdateUpdate))
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
                IFeature feature1 = this.ifeatureClass_1.GetFeature(int_0);
                string upper = (feature.Class as IFeatureClass).ShapeFieldName.ToUpper();
                ilist_0.Add("");
                for (int i = 0; i < feature.Fields.FieldCount; i++)
                {
                    if (!this.method_4(feature.Value[i], feature1.Value[i]))
                    {
                        if ((feature.Fields.Field[i].Name.ToUpper() == upper ||
                             feature.Fields.Field[i].Name.ToUpper() == "SHAPE.AREA"
                            ? false
                            : !(feature.Fields.Field[i].Name.ToUpper() == "SHAPE.LEN")))
                        {
                            string str = this.method_6(feature, i);
                            string str1 = this.method_6(feature1, i);
                            if (str.Length > 0)
                            {
                                ilist_0.Add(string.Concat("* 字段 ", feature.Fields.Field[i].Name.ToUpper(), " 变化: "));
                                ilist_0.Add(string.Concat("  - 版本 ", string_0, ": ", str1));
                                ilist_0.Add(string.Concat("  - 版本 ", string_1, ": ", str));
                            }
                        }
                        else if (!this.bool_1)
                        {
                            this.bool_1 = true;
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
            bool flag;
            try
            {
                bool object0 = false;
                bool object1 = false;
                object0 = object_0 is DBNull;
                object1 = object_1 is DBNull;
                if (object0 != object1)
                {
                    flag = false;
                }
                else if (!(!object0 ? true : !object1))
                {
                    flag = true;
                }
                else if (!(!(object_0 is IClone) ? true : !(object_1 is IClone)))
                {
                    flag = (object_0 as IClone).IsEqual(object_1 as IClone);
                }
                else if (!(!(object_0 is IClone) ? true : object_1 is IClone))
                {
                    flag = false;
                }
                else if ((!(object_1 is IClone) ? true : object_0 is IClone))
                {
                    bool flag1 = false;
                    if (object_0.ToString() == object_1.ToString())
                    {
                        flag1 = true;
                    }
                    flag = flag1;
                }
                else
                {
                    flag = false;
                }
            }
            catch
            {
                flag = false;
                return flag;
            }
            return flag;
        }

        private void method_5(IVersionedTable iversionedTable_0, IVersionedTable iversionedTable_1, string string_0,
            string string_1, string string_2, IList ilist_0)
        {
            this.bool_1 = false;
            this.method_1(iversionedTable_1, iversionedTable_0, string_0, string_1,
                esriDifferenceType.esriDifferenceTypeDeleteNoChange, string_2, true, ilist_0);
            this.method_1(iversionedTable_1, iversionedTable_0, string_0, string_1,
                esriDifferenceType.esriDifferenceTypeDeleteUpdate, string_2, true, ilist_0);
            this.method_1(iversionedTable_1, iversionedTable_0, string_0, string_1,
                esriDifferenceType.esriDifferenceTypeInsert, string_2, true, ilist_0);
            this.method_1(iversionedTable_1, iversionedTable_0, string_0, string_1,
                esriDifferenceType.esriDifferenceTypeUpdateDelete, string_2, true, ilist_0);
            this.method_1(iversionedTable_1, iversionedTable_0, string_0, string_1,
                esriDifferenceType.esriDifferenceTypeUpdateNoChange, string_2, true, ilist_0);
            this.method_1(iversionedTable_1, iversionedTable_0, string_0, string_1,
                esriDifferenceType.esriDifferenceTypeUpdateUpdate, string_2, true, ilist_0);
            this.method_1(iversionedTable_0, iversionedTable_1, string_0, string_1,
                esriDifferenceType.esriDifferenceTypeDeleteNoChange, string_2, false, ilist_0);
            this.method_1(iversionedTable_0, iversionedTable_1, string_0, string_1,
                esriDifferenceType.esriDifferenceTypeInsert, string_2, false, ilist_0);
            this.method_1(iversionedTable_0, iversionedTable_1, string_0, string_1,
                esriDifferenceType.esriDifferenceTypeUpdateNoChange, string_2, false, ilist_0);
        }

        private string method_6(IFeature ifeature_0, int int_0)
        {
            string str;
            try
            {
                IDomain domain = ifeature_0.Fields.Field[int_0].Domain;
                if (domain == null)
                {
                    str = ifeature_0.Value[int_0].ToString();
                    return str;
                }
                else if (domain.Type != esriDomainType.esriDTCodedValue)
                {
                    str = ifeature_0.Value[int_0].ToString();
                    return str;
                }
                else
                {
                    ICodedValueDomain codedValueDomain = domain as ICodedValueDomain;
                    int codeCount = codedValueDomain.CodeCount;
                    int num = 0;
                    while (num < codeCount)
                    {
                        if (codedValueDomain.Value[num] == ifeature_0.Value[int_0])
                        {
                            str = codedValueDomain.Value[num].ToString();
                            return str;
                        }
                        else
                        {
                            num++;
                        }
                    }
                }
            }
            catch
            {
            }
            str = "";
            return str;
        }

        private bool method_7(IVersionedWorkspace iversionedWorkspace_0, IEnumVersionInfo ienumVersionInfo_0,
            string string_0, bool bool_2, bool bool_3)
        {
            bool flag = true;
            try
            {
                for (IVersionInfo i = ienumVersionInfo_0.Next(); i != null; i = ienumVersionInfo_0.Next())
                {
                    if (!bool_2)
                    {
                        if (this.method_8(iversionedWorkspace_0.FindVersion(i.VersionName) as IVersionEdit, string_0,
                            bool_2, bool_3))
                        {
                            this.method_7(iversionedWorkspace_0, i.Children, i.VersionName, bool_2, bool_3);
                        }
                    }
                    else if (!this.method_7(iversionedWorkspace_0, i.Children, i.VersionName, bool_2, bool_3))
                    {
                        flag = false;
                    }
                    else if (
                        !this.method_8(iversionedWorkspace_0.FindVersion(i.VersionName) as IVersionEdit, string_0,
                            bool_2, bool_3))
                    {
                        flag = true;
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
                    if (bool_3)
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

        public void ShowDifferenceGeometry(IVersionedWorkspace iversionedWorkspace_0, int int_0, IMap imap_0,
            string string_0, string string_1)
        {
            try
            {
                IQueryFilter queryFilterClass = new QueryFilter();
                IDataset featureClass = null;
                IFeatureWorkspace featureWorkspace = iversionedWorkspace_0.FindVersion(string_1) as IFeatureWorkspace;
                for (int i = 0; i < imap_0.LayerCount; i++)
                {
                    IFeatureLayer layer = imap_0.Layer[i] as IFeatureLayer;
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
                    IFeatureClass featureClass1 = featureWorkspace.OpenFeatureClass(featureClass.Name);
                    queryFilterClass.WhereClause = string.Concat("OBJECTID = ", int_0);
                    IFeatureCursor featureCursor = featureClass1.Search(queryFilterClass, false);
                    IFeature feature = featureCursor.NextFeature();
                    if (feature != null)
                    {
                        this.DrawDifferenceGeometry(feature.Shape, (imap_0 as IActiveView).ScreenDisplay);
                    }
                    ComReleaser.ReleaseCOMObject(featureCursor);
                }
            }
            catch
            {
            }
        }
    }
}