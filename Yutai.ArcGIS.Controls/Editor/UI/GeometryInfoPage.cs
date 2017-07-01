using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using DevExpress.XtraBars;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.ControlExtend;
using Yutai.ArcGIS.Common.Editor;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.Shared;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    public partial class GeometryInfoPage : UserControl, IDockContent
    {
        private bool m_CanDo = false;
        private bool m_HasLicense = false;
        private bool m_HasM = false;
        private bool m_HasZ = false;
        private IPoint m_LastPoint = null;
        private IApplication m_pApp = null;
        private IFeature m_pEditFeature = null;
        private IWorkspace m_pEditWorkspace = null;
        private IGeometry m_pGeometry = null;

        public GeometryInfoPage()
        {
            this.InitializeComponent();
            this.m_pSymbol = new SimpleMarkerSymbolClass();
            (this.m_pSymbol as ISimpleMarkerSymbol).Style = esriSimpleMarkerStyle.esriSMSSquare;
            (this.m_pSymbol as ISimpleMarkerSymbol).Size = 6.0;
            (this.m_pSymbol as ISimpleMarkerSymbol).Outline = true;
            IRgbColor color = new RgbColorClass
            {
                Red = 183,
                Green = 120,
                Blue = 245
            };
            (this.m_pSymbol as IMarkerSymbol).Color = color;
            color = new RgbColorClass
            {
                Red = 255,
                Green = 0,
                Blue = 0
            };
            (this.m_pSymbol as ISimpleMarkerSymbol).OutlineColor = color;
            (this.m_pSymbol as ISimpleMarkerSymbol).OutlineSize = 2.0;
            this.m_pSymbol.ROP2 = esriRasterOpCode.esriROPXOrPen;
            this.Text = "几何数据信息";
        }

        private bool CheckIsEdit(IFeatureLayer pFeatLayer)
        {
            return this.CheckIsEdit(pFeatLayer.FeatureClass as IDataset);
        }

        private bool CheckIsEdit(IDataset pDataset)
        {
            if (this.m_pEditWorkspace != null)
            {
                IWorkspace workspace = pDataset.Workspace;
                if (workspace.ConnectionProperties.IsEqual(this.m_pEditWorkspace.ConnectionProperties))
                {
                    if (workspace.Type != esriWorkspaceType.esriRemoteDatabaseWorkspace)
                    {
                        return true;
                    }
                    if (workspace is IVersionedWorkspace)
                    {
                        IVersionedObject obj2 = pDataset as IVersionedObject;
                        if (obj2.IsRegisteredAsVersioned)
                        {
                            SysGrants grants = new SysGrants();
                            return (((AppConfigInfo.UserID.Length == 0) || (AppConfigInfo.UserID.ToLower() == "admin")) ||
                                    grants.GetStaffAndRolesLayerPri(AppConfigInfo.UserID, 2, pDataset.Name));
                        }
                    }
                }
            }
            return false;
        }

        private void CreateMultiPoint(string filename)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filename))
                {
                    string str;
                    int num = 0;
                    IPointCollection points = new MultipointClass();
                    object before = Missing.Value;
                    while ((str = reader.ReadLine()) != null)
                    {
                        string str2;
                        IPoint inPoint = new PointClass();
                        num++;
                        string[] strArray = str.Split(new char[] {','});
                        try
                        {
                            if (strArray.Length < 2)
                            {
                                str2 = filename + " 中第 " + num.ToString() + "行数据有错误";
                                Logger.Current.Error("", null, str2);
                                continue;
                            }
                            inPoint.X = double.Parse(strArray[0]);
                            inPoint.Y = double.Parse(strArray[1]);
                            if (strArray.Length > 2)
                            {
                                inPoint.Z = double.Parse(strArray[2]);
                            }
                            if (strArray.Length > 3)
                            {
                                inPoint.M = double.Parse(strArray[3]);
                            }
                            points.AddPoint(inPoint, ref before, ref before);
                        }
                        catch
                        {
                            str2 = filename + " 中第 " + num.ToString() + "行数据有错误";
                            Logger.Current.Error("", null, str2);
                        }
                    }
                    this.SetZMProperty(points as IGeometry);
                    IFeature feature =
                        Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer.FeatureClass.CreateFeature();
                    try
                    {
                        feature.Shape = points as IGeometry;
                        feature.Store();
                    }
                    catch (Exception exception)
                    {
                        feature.Delete();
                        Logger.Current.Error("", exception, "");
                    }
                    IActiveView pMap = (IActiveView) this.m_pMap;
                    this.m_pMap.ClearSelection();
                    this.m_pMap.SelectFeature(Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer,
                        feature);
                    pMap.Refresh();
                }
            }
            catch (Exception exception2)
            {
                Logger.Current.Error("", exception2, "");
            }
        }

        private void CreatePoint(string filename)
        {
            try
            {
                IFeatureClass featureClass =
                    Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer.FeatureClass;
                int index = featureClass.FindField(featureClass.ShapeFieldName);
                IGeometryDef geometryDef = featureClass.Fields.get_Field(index).GeometryDef;
                using (StreamReader reader = new StreamReader(filename))
                {
                    string str;
                    IPoint pGeom = new PointClass();
                    int num2 = 0;
                    while ((str = reader.ReadLine()) != null)
                    {
                        string str2;
                        num2++;
                        string[] strArray = str.Split(new char[] {','});
                        try
                        {
                            if (strArray.Length < 2)
                            {
                                str2 = filename + " 中第 " + num2.ToString() + "行数据有错误";
                                Logger.Current.Error("", null, str2);
                                continue;
                            }
                            pGeom.X = double.Parse(strArray[0]);
                            pGeom.Y = double.Parse(strArray[1]);
                            if (strArray.Length > 2)
                            {
                                pGeom.Z = double.Parse(strArray[2]);
                            }
                            else if (geometryDef.HasZ)
                            {
                                double num3;
                                double num4;
                                geometryDef.SpatialReference.GetZDomain(out num3, out num4);
                                pGeom.Z = num3;
                            }
                            if (strArray.Length > 3)
                            {
                                pGeom.M = double.Parse(strArray[3]);
                            }
                            else if (geometryDef.HasM)
                            {
                                double num5;
                                double num6;
                                geometryDef.SpatialReference.GetMDomain(out num5, out num6);
                                pGeom.M = num5;
                            }
                            try
                            {
                                this.SetZMProperty(pGeom);
                                IFeature feature = featureClass.CreateFeature();
                                feature.Shape = pGeom;
                                feature.Store();
                            }
                            catch (Exception exception)
                            {
                                Logger.Current.Error("", exception, "");
                            }
                        }
                        catch
                        {
                            str2 = filename + " 中第 " + num2.ToString() + "行数据有错误";
                            Logger.Current.Error("", null, str2);
                        }
                    }
                }
            }
            catch (Exception exception2)
            {
                Logger.Current.Error("", exception2, "");
            }
        }

        private IPoint CreatePoint(string line, int nIndex)
        {
            IFeatureClass featureClass = Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer.FeatureClass;
            int index = featureClass.FindField(featureClass.ShapeFieldName);
            IGeometryDef geometryDef = featureClass.Fields.get_Field(index).GeometryDef;
            IPoint point = new PointClass();
            string[] strArray = line.Split(new char[] {','});
            try
            {
                if (strArray.Length < 2)
                {
                    return null;
                }
                point.X = double.Parse(strArray[0]);
                point.Y = double.Parse(strArray[1]);
                if (strArray.Length > 2)
                {
                    point.Z = double.Parse(strArray[2]);
                }
                else if (geometryDef.HasZ)
                {
                    double num2;
                    double num3;
                    geometryDef.SpatialReference.GetZDomain(out num2, out num3);
                    point.Z = num2;
                }
                if (strArray.Length > 3)
                {
                    point.M = double.Parse(strArray[3]);
                    return point;
                }
                if (geometryDef.HasM)
                {
                    double num4;
                    double num5;
                    geometryDef.SpatialReference.GetMDomain(out num4, out num5);
                    point.M = num4;
                }
                return point;
            }
            catch
            {
            }
            return null;
        }

        private void CreatePolygon(string filename)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filename))
                {
                    int nIndex = 1;
                    string line = reader.ReadLine();
                    if (line != null)
                    {
                        IPoint point;
                        string str2;
                        IGeometryCollection geometrys = new PolygonClass();
                        IPointCollection points = new RingClass();
                        object before = Missing.Value;
                        bool flag = false;
                        if (line.ToLower() == "part")
                        {
                            flag = true;
                        }
                        else
                        {
                            point = this.CreatePoint(line, nIndex);
                            if (point == null)
                            {
                                str2 = string.Format("{0} 中第 {1}行数据有错误", filename, nIndex);
                                Logger.Current.Error("", null, str2);
                            }
                            else
                            {
                                points.AddPoint(point, ref before, ref before);
                            }
                        }
                        while ((line = reader.ReadLine()) != null)
                        {
                            line.Trim();
                            if (line.Length != 0)
                            {
                                nIndex++;
                                if (line.ToLower() == "part")
                                {
                                    if (flag)
                                    {
                                        if (points.PointCount >= 3)
                                        {
                                            if (!(points as IRing).IsClosed)
                                            {
                                                (points as IRing).Close();
                                            }
                                            geometrys.AddGeometry(points as IGeometry, ref before, ref before);
                                        }
                                        points = new RingClass();
                                    }
                                    else
                                    {
                                        str2 = filename + " 中第 " + nIndex.ToString() + "行数据有错误";
                                        Logger.Current.Error("", null, str2);
                                    }
                                }
                                else
                                {
                                    point = this.CreatePoint(line, nIndex);
                                    if (point == null)
                                    {
                                        str2 = filename + " 中第 " + nIndex.ToString() + "行数据有错误";
                                        Logger.Current.Error("", null, str2);
                                    }
                                    else
                                    {
                                        points.AddPoint(point, ref before, ref before);
                                    }
                                }
                            }
                        }
                        if ((points != null) && (points.PointCount >= 3))
                        {
                            if (!(points as IRing).IsClosed)
                            {
                                (points as IRing).Close();
                            }
                            geometrys.AddGeometry(points as IGeometry, ref before, ref before);
                        }
                        try
                        {
                            if (geometrys.GeometryCount > 0)
                            {
                                IFeatureLayer featureLayer =
                                    Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer;
                                this.SetZMProperty(geometrys as IGeometry);
                                IFeature feature = featureLayer.FeatureClass.CreateFeature();
                                feature.Shape = geometrys as IGeometry;
                                feature.Store();
                                IActiveView pMap = (IActiveView) this.m_pMap;
                                this.m_pMap.ClearSelection();
                                this.m_pMap.SelectFeature(featureLayer, feature);
                                pMap.Refresh();
                            }
                            else
                            {
                                MessageBox.Show("无法创建多边形!");
                            }
                        }
                        catch (Exception exception)
                        {
                            Logger.Current.Error("", exception, "");
                        }
                    }
                }
            }
            catch (Exception exception2)
            {
                Logger.Current.Error("", exception2, "");
            }
        }

        private void CreatePolyline(string filename)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filename))
                {
                    int nIndex = 1;
                    string line = reader.ReadLine();
                    if (line != null)
                    {
                        IPoint point;
                        string str2;
                        IGeometryCollection geometrys = new PolylineClass();
                        IPointCollection points = new PathClass();
                        object before = Missing.Value;
                        bool flag = false;
                        if (line.ToLower() == "part")
                        {
                            flag = true;
                        }
                        else
                        {
                            point = this.CreatePoint(line, nIndex);
                            if (point == null)
                            {
                                str2 = string.Format("{0} 中第 {1}行数据有错误", filename, nIndex);
                                Logger.Current.Error("", null, str2);
                            }
                            else
                            {
                                points.AddPoint(point, ref before, ref before);
                            }
                        }
                        while ((line = reader.ReadLine()) != null)
                        {
                            line.Trim();
                            if (line.Length != 0)
                            {
                                nIndex++;
                                if (line.ToLower() == "end")
                                {
                                    break;
                                }
                                if (line.ToLower() == "part")
                                {
                                    if (flag)
                                    {
                                        geometrys.AddGeometry(points as IGeometry, ref before, ref before);
                                        points = new PathClass();
                                    }
                                    else
                                    {
                                        str2 = filename + " 中第 " + nIndex.ToString() + "行数据有错误";
                                        Logger.Current.Error("", null, str2);
                                    }
                                }
                                else
                                {
                                    point = this.CreatePoint(line, nIndex);
                                    if (point == null)
                                    {
                                        str2 = string.Concat(new object[] {filename, " 中第 ", nIndex, "行数据有错误"});
                                        Logger.Current.Error("", null, str2);
                                    }
                                    else
                                    {
                                        points.AddPoint(point, ref before, ref before);
                                    }
                                }
                            }
                        }
                        if (points != null)
                        {
                            geometrys.AddGeometry(points as IGeometry, ref before, ref before);
                        }
                        try
                        {
                            IFeatureLayer featureLayer =
                                Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer;
                            this.SetZMProperty(geometrys as IGeometry);
                            IFeature feature = featureLayer.FeatureClass.CreateFeature();
                            feature.Shape = geometrys as IGeometry;
                            feature.Store();
                            IActiveView pMap = (IActiveView) this.m_pMap;
                            this.m_pMap.ClearSelection();
                            this.m_pMap.SelectFeature(featureLayer, feature);
                            pMap.Refresh();
                        }
                        catch (Exception exception)
                        {
                            Logger.Current.Error("", exception, "");
                        }
                    }
                }
            }
            catch (Exception exception2)
            {
                Logger.Current.Error("", exception2, "");
            }
        }

        private void DeletePoints_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                IPointCollection pGeometry;
                IEnvelope envelope = this.m_pGeometry.Envelope;
                double dx = CommonHelper.ConvertPixelsToMapUnits(this.m_pMap as IActiveView, 6.0);
                envelope.Expand(dx, dx, false);
                if (this.m_pGeometry.GeometryType == esriGeometryType.esriGeometryMultipoint)
                {
                    pGeometry = this.m_pGeometry as IPointCollection;
                }
                else
                {
                    IGeometryCollection geometrys = this.m_pGeometry as IGeometryCollection;
                    pGeometry = geometrys.get_Geometry(this.listBox1.SelectedIndex) as IPointCollection;
                }
                int index = this.listView1.SelectedIndices[0];
                pGeometry.RemovePoints(index, 1);
                this.m_LastPoint = null;
                this.ResetListView();
                (this.m_pMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, envelope);
                IWorkspaceEdit workspace = (this.m_pEditFeature.Class as IDataset).Workspace as IWorkspaceEdit;
                workspace.StartEditOperation();
                this.m_pEditFeature.Shape = this.m_pGeometry;
                this.m_pEditFeature.Store();
                workspace.StopEditOperation();
                envelope = this.m_pGeometry.Envelope;
                envelope.Expand(dx, dx, false);
                (this.m_pMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, envelope);
                this.SetStatus();
            }
            catch (Exception exception)
            {
                Logger.Current.Error("", exception, "");
            }
        }

        private void EditorEvent_OnEditTemplateChange(YTEditTemplate template)
        {
            this.ImportGeometryData.Enabled = Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate != null;
        }

        private void EditorEvent_OnFeatureGeometryChanged(IFeature pFeat)
        {
            if (this.m_pEditFeature == pFeat)
            {
                this.m_pEditFeature = (pFeat.Class as IFeatureClass).GetFeature(pFeat.OID);
                Yutai.ArcGIS.Common.Editor.Editor.EditFeature = this.m_pEditFeature;
                this.RefreshCoordinate();
            }
        }

        private void EditorEvent_OnStopEditing()
        {
            ApplicationRef.Application.HideDockWindow(this);
        }

        private void ExportGeometry_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.m_pGeometry != null)
            {
                SaveFileDialog dialog = new SaveFileDialog
                {
                    Filter = "文本文件 (*.txt)|*.txt"
                };
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        int index =
                            this.m_pEditFeature.Fields.FindField(
                                (this.m_pEditFeature.Class as IFeatureClass).ShapeFieldName);
                        IGeometryDef geometryDef = this.m_pEditFeature.Fields.get_Field(index).GeometryDef;
                        this.m_HasZ = geometryDef.HasZ;
                        this.m_HasM = geometryDef.HasM;
                        using (StreamWriter writer = new StreamWriter(dialog.FileName))
                        {
                            if (this.m_pGeometry.GeometryType == esriGeometryType.esriGeometryPoint)
                            {
                                writer.WriteLine(this.WritePointToString(this.m_pGeometry as IPoint));
                            }
                            else if (this.m_pGeometry.GeometryType == esriGeometryType.esriGeometryMultipoint)
                            {
                                this.WritePointCollection(writer, this.m_pGeometry as IPointCollection);
                            }
                            else if ((this.m_pGeometry.GeometryType == esriGeometryType.esriGeometryPolyline) ||
                                     (this.m_pGeometry.GeometryType == esriGeometryType.esriGeometryPolygon))
                            {
                                IGeometryCollection pGeometry = this.m_pGeometry as IGeometryCollection;
                                if (pGeometry.GeometryCount > 1)
                                {
                                    for (int i = 0; i < pGeometry.GeometryCount; i++)
                                    {
                                        writer.WriteLine("part");
                                        this.WritePointCollection(writer, pGeometry.get_Geometry(i) as IPointCollection);
                                    }
                                }
                                else
                                {
                                    this.WritePointCollection(writer, pGeometry.get_Geometry(0) as IPointCollection);
                                }
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void frmGeometryInfo_Load(object sender, EventArgs e)
        {
            if (EditorLicenseProviderCheck.Check())
            {
                this.m_HasLicense = true;
                EditorEvent.OnEditTemplateChange +=
                    new EditorEvent.OnEditTemplateChangeHandler(this.EditorEvent_OnEditTemplateChange);
                EditorEvent.OnFeatureGeometryChanged +=
                    new EditorEvent.OnFeatureGeometryChangedHandler(this.EditorEvent_OnFeatureGeometryChanged);
                EditorEvent.OnStopEditing += new EditorEvent.OnStopEditingHandler(this.EditorEvent_OnStopEditing);
                this.Init();
                this.m_CanDo = true;
            }
        }


        private string GetUnitName(string s)
        {
            string str2 = s.ToLower();
            if ((str2 != null) && (str2 == "meter"))
            {
                return "米";
            }
            return s;
        }

        private void ImportGeometryData_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "文本文件 (*.txt)|*.txt"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Yutai.ArcGIS.Common.Editor.Editor.ImportGeometryData(dialog.FileName, this.m_pMap,
                    Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer);
                (this.m_pMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography,
                    Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer, null);
            }
        }

        public void Init()
        {
            if (Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate != null)
            {
                this.ImportGeometryData.Enabled = true;
            }
            else
            {
                this.ImportGeometryData.Enabled = false;
            }
            this.listBox1.Items.Clear();
            this.listView1.Clear();
            this.BarGeometryInfo.Caption = "";
            Yutai.ArcGIS.Common.Editor.Editor.EditFeature = null;
            if (this.m_pMap.SelectionCount != 1)
            {
                this.m_pEditFeature = null;
            }
            else
            {
                IEnumFeature featureSelection = this.m_pMap.FeatureSelection as IEnumFeature;
                featureSelection.Reset();
                this.m_pEditFeature = featureSelection.Next();
                if (!this.CheckIsEdit(this.m_pEditFeature.Class as IDataset))
                {
                    this.m_pEditFeature = null;
                }
                else if (this.m_pEditFeature is IAnnotationFeature2)
                {
                    this.m_pEditFeature = null;
                }
                else
                {
                    Yutai.ArcGIS.Common.Editor.Editor.EditFeature = this.m_pEditFeature;
                    this.RefreshCoordinate();
                }
            }
        }

        private void InsertPointAfter_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                IPointCollection pGeometry;
                IEnvelope envelope = this.m_pGeometry.Envelope;
                double dx = CommonHelper.ConvertPixelsToMapUnits(this.m_pMap as IActiveView, 6.0);
                envelope.Expand(dx, dx, false);
                if (this.m_pGeometry.GeometryType == esriGeometryType.esriGeometryMultipoint)
                {
                    pGeometry = this.m_pGeometry as IPointCollection;
                }
                else
                {
                    IGeometryCollection geometrys = this.m_pGeometry as IGeometryCollection;
                    pGeometry = geometrys.get_Geometry(this.listBox1.SelectedIndex) as IPointCollection;
                }
                int i = this.listView1.SelectedIndices[0];
                IPoint point = pGeometry.get_Point(i);
                IPoint point2 = pGeometry.get_Point(i + 1);
                IPoint inPoint = new PointClass();
                inPoint.PutCoords((point.X + point2.X)/2.0, (point.Y + point2.Y)/2.0);
                if (this.m_HasZ)
                {
                    inPoint.Z = (point.Z + point2.Z)/2.0;
                }
                if (this.m_HasM)
                {
                    inPoint.M = (point.M + point2.M)/2.0;
                }
                object after = i;
                object before = Missing.Value;
                pGeometry.AddPoint(inPoint, ref before, ref after);
                this.ResetListView();
                (this.m_pMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, envelope);
                IWorkspaceEdit workspace = (this.m_pEditFeature.Class as IDataset).Workspace as IWorkspaceEdit;
                workspace.StartEditOperation();
                this.m_pEditFeature.Shape = this.m_pGeometry;
                this.m_pEditFeature.Store();
                workspace.StopEditOperation();
                envelope = this.m_pGeometry.Envelope;
                envelope.Expand(dx, dx, false);
                (this.m_pMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, envelope);
                this.SetStatus();
            }
            catch (Exception exception)
            {
                Logger.Current.Error("", exception, "");
            }
        }

        private void InsertPointBefore_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                IPointCollection pGeometry;
                IEnvelope envelope = this.m_pGeometry.Envelope;
                double dx = CommonHelper.ConvertPixelsToMapUnits(this.m_pMap as IActiveView, 6.0);
                envelope.Expand(dx, dx, false);
                if (this.m_pGeometry.GeometryType == esriGeometryType.esriGeometryMultipoint)
                {
                    pGeometry = this.m_pGeometry as IPointCollection;
                }
                else
                {
                    IGeometryCollection geometrys = this.m_pGeometry as IGeometryCollection;
                    pGeometry = geometrys.get_Geometry(this.listBox1.SelectedIndex) as IPointCollection;
                }
                int i = this.listView1.SelectedIndices[0];
                IPoint point = pGeometry.get_Point(i - 1);
                IPoint point2 = pGeometry.get_Point(i);
                IPoint inPoint = new PointClass();
                inPoint.PutCoords((point.X + point2.X)/2.0, (point.Y + point2.Y)/2.0);
                if (this.m_HasZ)
                {
                    inPoint.Z = (point.Z + point2.Z)/2.0;
                }
                if (this.m_HasM)
                {
                    inPoint.M = (point.M + point2.M)/2.0;
                }
                object before = i;
                object after = Missing.Value;
                pGeometry.AddPoint(inPoint, ref before, ref after);
                this.ResetListView();
                (this.m_pMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, envelope);
                IWorkspaceEdit workspace = (this.m_pEditFeature.Class as IDataset).Workspace as IWorkspaceEdit;
                workspace.StartEditOperation();
                this.m_pEditFeature.Shape = this.m_pGeometry;
                this.m_pEditFeature.Store();
                workspace.StopEditOperation();
                envelope = this.m_pGeometry.Envelope;
                envelope.Expand(dx, dx, false);
                (this.m_pMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, envelope);
                this.SetStatus();
            }
            catch (Exception exception)
            {
                Logger.Current.Error("", exception, "");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ResetListView();
        }

        private void listView1_MouseUp(object sender, MouseEventArgs e)
        {
            if ((this.listView1.SelectedItems.Count > 0) && (e.Button == MouseButtons.Right))
            {
                this.popupMenu1.ShowPopup(Control.MousePosition);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.InsertPointAfter.Enabled = false;
            this.InsertPointBefore.Enabled = false;
            IEnvelope envelope = null;
            if ((this.listView1.SelectedItems.Count == 1) &&
                (this.m_pGeometry.GeometryType != esriGeometryType.esriGeometryPoint))
            {
                this.InsertPointBefore.Enabled = true;
                this.InsertPointAfter.Enabled = true;
                if (this.listView1.SelectedIndices[0] == 0)
                {
                    this.InsertPointBefore.Enabled = false;
                }
                if (this.listView1.SelectedIndices[0] == (this.listView1.Items.Count - 1))
                {
                    this.InsertPointAfter.Enabled = false;
                }
            }
            IRgbColor color = new RgbColorClass();
            envelope = this.m_pGeometry.Envelope;
            double dx = CommonHelper.ConvertPixelsToMapUnits(this.m_pMap as IActiveView, 6.0);
            envelope.Expand(dx, dx, false);
            if (this.m_LastPoint != null)
            {
                this.m_pSymbol.ROP2 = esriRasterOpCode.esriROPXOrPen;
                (this.m_pMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, envelope);
            }
            if (this.listView1.SelectedItems.Count == 1)
            {
                this.m_LastPoint = this.listView1.SelectedItems[0].Tag as IPoint;
                this.m_pSymbol.ROP2 = esriRasterOpCode.esriROPCopyPen;
                (this.m_pMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, envelope);
            }
        }

        private void listView1_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            try
            {
                IPoint tag = this.listView1.Items[e.Row].Tag as IPoint;
                if (tag != null)
                {
                    if (e.Column == 1)
                    {
                        tag.X = double.Parse((string) e.NewValue);
                    }
                    else if (e.Column == 2)
                    {
                        tag.Y = double.Parse((string) e.NewValue);
                    }
                    else if (e.Column == 3)
                    {
                        if (this.m_HasZ)
                        {
                            tag.Z = double.Parse((string) e.NewValue);
                        }
                        else
                        {
                            tag.M = double.Parse((string) e.NewValue);
                        }
                    }
                    else if (e.Column == 4)
                    {
                        tag.M = double.Parse((string) e.NewValue);
                    }
                    if ((this.m_pGeometry is IPolygon) || (this.m_pGeometry is IPolyline))
                    {
                        IPointCollection points =
                            (this.m_pGeometry as IGeometryCollection).get_Geometry(this.listBox1.SelectedIndex) as
                                IPointCollection;
                        points.UpdatePoint(e.Row, tag);
                        if ((points as ICurve).IsClosed && (e.Row == 0))
                        {
                            points.UpdatePoint(points.PointCount - 1, tag);
                        }
                    }
                    IWorkspaceEdit workspace = (this.m_pEditFeature.Class as IDataset).Workspace as IWorkspaceEdit;
                    (this.m_pMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, null,
                        this.m_pEditFeature.Extent);
                    workspace.StartEditOperation();
                    this.m_pEditFeature.Shape = this.m_pGeometry;
                    this.m_pEditFeature.Store();
                    workspace.StopEditOperation();
                    (this.m_pMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, null,
                        this.m_pGeometry.Envelope);
                    this.SetStatus();
                }
            }
            catch (Exception exception)
            {
                CErrorLog.writeErrorLog(null, exception, "");
            }
        }

        private void m_pActiveViewEvents_AfterDraw(IDisplay Display, esriViewDrawPhase phase)
        {
            if ((this.listView1.SelectedIndices.Count == 1) && base.Visible)
            {
                IEnvelope extent = this.m_pApp.ActiveView.Extent;
                if (this.m_LastPoint != null)
                {
                    int hDC = Graphics.FromHwnd(new IntPtr((Display as IScreenDisplay).hWnd)).GetHdc().ToInt32();
                    this.m_pSymbol.SetupDC(hDC, Display.DisplayTransformation);
                    this.m_pSymbol.Draw(this.m_LastPoint);
                    this.m_pSymbol.ResetDC();
                }
            }
        }

        private void m_pActiveViewEvents_SelectionChanged()
        {
            if (this.m_HasLicense && this.m_CanDo)
            {
                this.m_CanDo = false;
                this.Init();
                this.m_CanDo = true;
            }
        }

        private void RefreshCoordinate()
        {
            this.listBox1.Items.Clear();
            this.listView1.Clear();
            this.m_pGeometry = this.m_pEditFeature.ShapeCopy;
            string shapeFieldName = (this.m_pEditFeature.Class as IFeatureClass).ShapeFieldName;
            int index = this.m_pEditFeature.Fields.FindField(shapeFieldName);
            IGeometryDef geometryDef = this.m_pEditFeature.Fields.get_Field(index).GeometryDef;
            this.m_HasM = geometryDef.HasM;
            this.m_HasZ = geometryDef.HasZ;
            this.SetStatus();
            LVColumnHeader header = new LVColumnHeader
            {
                Text = "序号",
                Width = 80
            };
            this.listView1.Columns.Add(header);
            header = new LVColumnHeader
            {
                ColumnStyle = ListViewColumnStyle.EditBox,
                Text = "x",
                Width = 80
            };
            this.listView1.Columns.Add(header);
            header = new LVColumnHeader
            {
                ColumnStyle = ListViewColumnStyle.EditBox,
                Text = "y",
                Width = 80
            };
            this.listView1.Columns.Add(header);
            if (this.m_HasZ)
            {
                header = new LVColumnHeader
                {
                    ColumnStyle = ListViewColumnStyle.EditBox,
                    Text = "z",
                    Width = 80
                };
                this.listView1.Columns.Add(header);
            }
            if (this.m_HasM)
            {
                header = new LVColumnHeader
                {
                    ColumnStyle = ListViewColumnStyle.EditBox,
                    Text = "m",
                    Width = 80
                };
                this.listView1.Columns.Add(header);
            }
            if (this.m_pGeometry.GeometryType == esriGeometryType.esriGeometryPoint)
            {
                this.listBox1.Items.Add(0);
            }
            else if (this.m_pGeometry.GeometryType == esriGeometryType.esriGeometryMultipoint)
            {
                this.listBox1.Items.Add(0);
            }
            else
            {
                IGeometryCollection pGeometry = this.m_pGeometry as IGeometryCollection;
                for (int i = 0; i < pGeometry.GeometryCount; i++)
                {
                    this.listBox1.Items.Add(i);
                }
            }
            this.listBox1.SelectedIndex = 0;
        }

        private void ResetListView()
        {
            this.listView1.Items.Clear();
            if (this.listBox1.SelectedIndex != -1)
            {
                ListViewItem item;
                string[] items = new string[this.listView1.Columns.Count];
                if (this.m_pGeometry.GeometryType == esriGeometryType.esriGeometryPoint)
                {
                    items[0] = "0";
                    items[1] = (this.m_pGeometry as IPoint).X.ToString("0.###");
                    items[2] = (this.m_pGeometry as IPoint).Y.ToString("0.###");
                    if (this.m_HasZ)
                    {
                        items[3] = (this.m_pGeometry as IPoint).Z.ToString("0.###");
                    }
                    if (this.m_HasM)
                    {
                        items[4] = (this.m_pGeometry as IPoint).M.ToString("0.###");
                    }
                    item = new ListViewItem(items)
                    {
                        Tag = this.m_pGeometry
                    };
                    this.listView1.Items.Add(item);
                }
                else
                {
                    IPointCollection pGeometry;
                    IPoint point;
                    int num;
                    if (this.m_pGeometry.GeometryType == esriGeometryType.esriGeometryMultipoint)
                    {
                        pGeometry = this.m_pGeometry as IPointCollection;
                        for (num = 0; num < pGeometry.PointCount; num++)
                        {
                            point = pGeometry.get_Point(num);
                            items[0] = num.ToString();
                            items[1] = point.X.ToString("0.###");
                            items[2] = point.Y.ToString("0.###");
                            if (this.m_HasZ)
                            {
                                items[3] = point.Z.ToString("0.###");
                            }
                            if (this.m_HasM)
                            {
                                items[4] = point.M.ToString("0.###");
                            }
                            item = new ListViewItem(items)
                            {
                                Tag = point
                            };
                            this.listView1.Items.Add(item);
                        }
                    }
                    else
                    {
                        IGeometryCollection geometrys = this.m_pGeometry as IGeometryCollection;
                        pGeometry = geometrys.get_Geometry(this.listBox1.SelectedIndex) as IPointCollection;
                        int pointCount = pGeometry.PointCount;
                        if ((pGeometry as ICurve).IsClosed)
                        {
                            pointCount--;
                        }
                        for (num = 0; num < pointCount; num++)
                        {
                            point = pGeometry.get_Point(num);
                            items[0] = num.ToString();
                            items[1] = point.X.ToString("0.###");
                            items[2] = point.Y.ToString("0.###");
                            if (this.m_HasZ)
                            {
                                items[3] = point.Z.ToString("0.###");
                            }
                            if (this.m_HasM)
                            {
                                items[4] = point.M.ToString("0.###");
                            }
                            item = new ListViewItem(items)
                            {
                                Tag = point
                            };
                            this.listView1.Items.Add(item);
                        }
                    }
                }
            }
        }

        private void SetStatus()
        {
            string unitName = "";
            string str2 = "";
            ISpatialReference spatialReference = this.m_pGeometry.SpatialReference;
            if (spatialReference is IProjectedCoordinateSystem)
            {
                unitName = this.GetUnitName((spatialReference as IProjectedCoordinateSystem).CoordinateUnit.Name);
                str2 = "平方" + unitName;
            }
            string str3 = "";
            if (this.m_pGeometry.GeometryType == esriGeometryType.esriGeometryPolygon)
            {
                str3 = ("周长 = " + (this.m_pGeometry as ICurve).Length.ToString() + unitName) + ", 面积 = " +
                       (this.m_pGeometry as IArea).Area.ToString() + str2;
            }
            else if (this.m_pGeometry.GeometryType == esriGeometryType.esriGeometryPolyline)
            {
                str3 = "周长 = " + (this.m_pGeometry as ICurve).Length.ToString() + unitName;
            }
            this.BarGeometryInfo.Caption = str3;
        }

        private void SetZMProperty(IGeometry pGeom)
        {
            IFeatureClass featureClass = Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer.FeatureClass;
            int index = featureClass.FindField(featureClass.ShapeFieldName);
            IGeometryDef geometryDef = featureClass.Fields.get_Field(index).GeometryDef;
            if (geometryDef.HasZ)
            {
                IZAware aware = (IZAware) pGeom;
                aware.ZAware = true;
            }
            if (geometryDef.HasM)
            {
                IMAware aware2 = (IMAware) pGeom;
                aware2.MAware = true;
            }
        }

        private void WritePointCollection(StreamWriter wr, IPointCollection pts)
        {
            for (int i = 0; i < pts.PointCount; i++)
            {
                IPoint pt = pts.get_Point(i);
                wr.WriteLine(this.WritePointToString(pt));
            }
        }

        private string WritePointToString(IPoint pt)
        {
            string str = "";
            str = pt.X.ToString("0.#####") + "," + pt.Y.ToString("0.#####");
            if (this.m_HasZ)
            {
                str = str + "," + pt.Z.ToString("0.#####");
            }
            if (this.m_HasM)
            {
                str = str + "," + pt.M.ToString("0.#####");
            }
            return str;
        }

        public IApplication Application
        {
            set { this.m_pApp = value; }
        }

        public DockingStyle DefaultDockingStyle
        {
            get { return DockingStyle.Right; }
        }

        public IWorkspace EditWorkspace
        {
            set { this.m_pEditWorkspace = value; }
        }

        public IMap FocusMap
        {
            set
            {
                this.m_pMap = value;
                this.m_pActiveViewEvents = this.m_pMap as IActiveViewEvents_Event;
                this.m_pActiveViewEvents.SelectionChanged +=
                    (new IActiveViewEvents_SelectionChangedEventHandler(this.m_pActiveViewEvents_SelectionChanged));
                this.m_pActiveViewEvents.AfterDraw +=
                    (new IActiveViewEvents_AfterDrawEventHandler(this.m_pActiveViewEvents_AfterDraw));
            }
        }

        string IDockContent.Name
        {
            get { return base.Name; }
        }

        int IDockContent.Width
        {
            get { return base.Width; }
        }
    }
}