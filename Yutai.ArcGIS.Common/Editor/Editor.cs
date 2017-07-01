using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.ArcGIS.Common.Geodatabase;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.Plugins.Interfaces;
using Yutai.Shared;

namespace Yutai.ArcGIS.Common.Editor
{
    public class Editor
    {
        private static bool m_IsAdvanceControlTool;

        private static bool m_EnableSketch;

        private static bool _DrawNode;

        private static YTEditTemplate m_CurrentEditTemplate;

        public static bool UseOldSnap;

        public static SysGrants m_SysGrants;

        private static bool m_EnableUndoRedo;

        private static IMap m_pEditMap;

        private static bool m_IsCheckRegisterVeision;

        private static IFeatureLayer m_CurrentSkectchLayer;

        private static IWorkspaceEdit m_pEditWorkspace;

        private static IAppContext m_pContext;

        public static YTEditTemplate CurrentEditTemplate
        {
            get { return Editor.m_CurrentEditTemplate; }
            set
            {
                Editor.m_CurrentEditTemplate = value;
                EditorEvent.EditTempalateChange(Editor.m_CurrentEditTemplate);
            }
        }

        public static IFeatureLayer CurrentLayer
        {
            get { return Editor.m_CurrentSkectchLayer; }
            set
            {
                Editor.m_CurrentSkectchLayer = value;
                EditorEvent.EditLayerChange(Editor.m_CurrentSkectchLayer);
            }
        }

        public static bool DrawNode
        {
            get { return Editor._DrawNode; }
            set { Editor._DrawNode = value; }
        }

        public static IFeature EditFeature { get; set; }

        public static IMap EditMap
        {
            get { return Editor.m_pEditMap; }
            set
            {
                Editor.m_pEditMap = value;
                SketchToolAssist.SetActiveMap(Editor.m_pEditMap);
            }
        }

        public static IWorkspaceEdit EditWorkspace
        {
            get { return Editor.m_pEditWorkspace; }
            set { Editor.m_pEditWorkspace = value; }
        }

        public static bool EnableSketch
        {
            get { return Editor.m_EnableSketch; }
            set { Editor.m_EnableSketch = value; }
        }

        public static bool EnableUndoRedo
        {
            get { return Editor.m_EnableUndoRedo; }
            set { Editor.m_EnableUndoRedo = value; }
        }

        public static bool IsAdvanceControlTool
        {
            get { return Editor.m_IsAdvanceControlTool; }
        }

        public static bool IsCheckRegisterVeision
        {
            get { return Editor.m_IsCheckRegisterVeision; }
            set { Editor.m_IsCheckRegisterVeision = value; }
        }

        public static bool IsRemoveUnEditLayerSelect { get; set; }

        public static bool IsSnapEndPoint { get; set; }

        public static bool IsSnapLine { get; set; }

        public static bool IsSnapPoint { get; set; }

        public static bool IsSnapVertexPoint { get; set; }

        public static IWorkspaceEdit SecondEditWorkspace { get; set; }

        public static SysGrants SysGrants
        {
            get { return Editor.m_SysGrants; }
        }

        public static bool UseSnap { get; set; }

        static Editor()
        {
            Editor.old_acctor_mc();
        }

        public Editor()
        {
        }

        private static IAnchorPoint AddNewAnchorPt(IPoint pPoint, IActiveView iactiveView_0, ISymbol isymbol_0)
        {
            IAnchorPoint anchorPointClass = new AnchorPoint()
            {
                Symbol = isymbol_0
            };
            anchorPointClass.MoveTo(pPoint, iactiveView_0.ScreenDisplay);
            return anchorPointClass;
        }

        private static IPoint CalculateJunction(IPoint pPoint, IPoint ipoint_1, IPoint ipoint_2, IPoint ipoint_3)
        {
            double num;
            double y;
            IPoint point;
            double x;
            double num1;
            IPoint pointClass;
            double x1 = ipoint_1.X - pPoint.X;
            double y1 = ipoint_1.Y - pPoint.Y;
            double x2 = ipoint_3.X - ipoint_2.X;
            double y2 = ipoint_3.Y - ipoint_2.Y;
            if (x1 != 0)
            {
                double num2 = y1/x1;
                double y3 = pPoint.Y - num2*pPoint.X;
                if (x2 == 0)
                {
                    x = ipoint_1.X;
                    num1 = x*num2 + y3;
                    pointClass = new ESRI.ArcGIS.Geometry.Point();
                    pointClass.PutCoords(x, num1);
                    point = pointClass;
                }
                else
                {
                    num = y2/x2;
                    y = ipoint_2.Y - num*ipoint_2.X;
                    if (num2 != num)
                    {
                        x = (y - y3)/(num2 - num);
                        num1 = x*num2 + y3;
                        pointClass = new ESRI.ArcGIS.Geometry.Point();
                        pointClass.PutCoords(x, num1);
                        point = pointClass;
                    }
                    else
                    {
                        point = null;
                    }
                }
            }
            else if (x2 == 0)
            {
                point = null;
            }
            else
            {
                num = y2/x2;
                y = ipoint_2.Y - num*ipoint_2.X;
                x = pPoint.X;
                num1 = x*num + y;
                pointClass = new ESRI.ArcGIS.Geometry.Point();
                pointClass.PutCoords(x, num1);
                point = pointClass;
            }
            return point;
        }

        public static bool CanEdit(IMap imap_0, bool bool_0)
        {
            bool flag;
            if (imap_0 == null)
            {
                flag = false;
            }
            else if (imap_0.LayerCount == 0)
            {
                flag = false;
            }
            else if (!(Editor.EditMap == null ? true : Editor.EditMap == imap_0))
            {
                flag = false;
            }
            else if (Editor.EditWorkspace == null)
            {
                flag = false;
            }
            else if (!bool_0)
            {
                flag = true;
            }
            else
            {
                flag = (Editor.CurrentEditTemplate != null ? Editor.CurrentEditTemplate.FeatureLayer != null : false);
            }
            return flag;
        }

        private static void CheckGroupLayerEdit(IArray pArray, ICompositeLayer icompositeLayer_0)
        {
            for (int i = 0; i < icompositeLayer_0.Count; i++)
            {
                ILayer layer = icompositeLayer_0.Layer[i];
                if (layer is IGroupLayer)
                {
                    Editor.CheckGroupLayerEdit(pArray, layer as ICompositeLayer);
                }
                else if (layer is IFeatureLayer)
                {
                    IFeatureLayer featureLayer = layer as IFeatureLayer;
                    IDataset featureClass = featureLayer.FeatureClass as IDataset;
                    if (featureClass != null && !(featureClass is ICoverageFeatureClass) &&
                        Editor.LayerIsHasEditprivilege(layer as IFeatureLayer))
                    {
                        IWorkspace workspace = featureClass.Workspace;
                        if (workspace is IWorkspaceEdit)
                        {
                            EditWorkspaceInfo editWorkspaceInfo = Editor.FindWorkspce(pArray, workspace);
                            if (workspace.Type != esriWorkspaceType.esriRemoteDatabaseWorkspace)
                            {
                                editWorkspaceInfo.LayerArray.Add(featureLayer);
                            }
                            else if (!Editor.IsCheckRegisterVeision)
                            {
                                editWorkspaceInfo.LayerArray.Add(featureLayer);
                            }
                            else if (!(workspace is IVersionedWorkspace))
                            {
                                editWorkspaceInfo.LayerArray.Add(featureLayer);
                            }
                            else if ((featureLayer.FeatureClass as IVersionedObject).IsRegisteredAsVersioned)
                            {
                                editWorkspaceInfo.LayerArray.Add(featureLayer);
                            }
                        }
                    }
                }
            }
        }

        public static bool CheckIsEdit(IFeatureLayer ifeatureLayer_0, IWorkspace pWorkspace)
        {
            return Editor.CheckIsEdit(ifeatureLayer_0.FeatureClass as IDataset, pWorkspace);
        }

        public static bool CheckIsEdit(IDataset idataset_0, IWorkspace pWorkspace)
        {
            bool flag;
            IWorkspace workspace = idataset_0.Workspace;
            if (workspace.ConnectionProperties.IsEqual(pWorkspace.ConnectionProperties))
            {
                if (workspace.Type != esriWorkspaceType.esriRemoteDatabaseWorkspace)
                {
                    flag = true;
                    return flag;
                }
                else
                {
                    if (!(workspace is IVersionedWorkspace) || !(idataset_0 as IVersionedObject).IsRegisteredAsVersioned)
                    {
                        flag = false;
                        return flag;
                    }
                    flag = true;
                    return flag;
                }
            }
            flag = false;
            return flag;
        }

        public static bool CheckIsEdit(IDataset idataset_0, SysGrants sysGrants_0, string string_0)
        {
            bool flag;
            IWorkspace workspace = idataset_0.Workspace;
            if ((workspace as IWorkspaceEdit).IsBeingEdited())
            {
                if (workspace.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
                {
                    if (!(workspace is IVersionedWorkspace))
                    {
                        flag = true;
                        return flag;
                    }
                    else if ((idataset_0 as IVersionedObject).IsRegisteredAsVersioned)
                    {
                        if ((string_0.Length == 0 ? false : !(string_0.ToLower() == "admin")))
                        {
                            if (!sysGrants_0.GetStaffAndRolesLayerPri(string_0.ToLower(), 2, idataset_0.Name))
                            {
                                goto Label1;
                            }
                            flag = true;
                            return flag;
                        }
                        else
                        {
                            flag = true;
                            return flag;
                        }
                    }
                }
                Label1:
                flag = false;
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        public static bool CheckIsEdit(IFeatureLayer ifeatureLayer_0)
        {
            return Editor.CheckIsEdit(ifeatureLayer_0.FeatureClass as IDataset);
        }

        public static bool CheckIsEdit(IDataset idataset_0)
        {
            bool flag;
            if (!(idataset_0 is ICoverageFeatureClass))
            {
                if (Editor.EditWorkspace != null)
                {
                    IWorkspace workspace = idataset_0.Workspace;
                    if (workspace.ConnectionProperties.IsEqual((Editor.EditWorkspace as IWorkspace).ConnectionProperties))
                    {
                        if (workspace.Type != esriWorkspaceType.esriRemoteDatabaseWorkspace)
                        {
                            flag = true;
                            return flag;
                        }
                        else if (workspace is IVersionedWorkspace && (idataset_0 as IVersionedObject).IsRegisteredAsVersioned)
                        {
                            if ((AppConfigInfo.UserID.Length == 0 ? false : !(AppConfigInfo.UserID.ToLower() == "admin")))
                            {
                                if (
                                    !Editor.m_SysGrants.GetStaffAndRolesLayerPri(AppConfigInfo.UserID, 2,
                                        idataset_0.Name))
                                {
                                    goto Label1;
                                }
                                flag = true;
                                return flag;
                            }
                            else
                            {
                                flag = true;
                                return flag;
                            }
                        }
                    }
                }
                Label1:
                flag = false;
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        public static bool CheckLayerCanEdit(IFeatureLayer ifeatureLayer_0)
        {
            bool flag;
            if (!(ifeatureLayer_0.FeatureClass is ICoverageFeatureClass))
            {
                IWorkspaceEdit workspace = (ifeatureLayer_0.FeatureClass as IDataset).Workspace as IWorkspaceEdit;
                if (workspace != null)
                {
                    bool flag1 = false;
                    if (workspace.IsBeingEdited())
                    {
                        if ((workspace as IWorkspace).Type != esriWorkspaceType.esriRemoteDatabaseWorkspace)
                        {
                            flag1 = true;
                        }
                        else if (workspace is IVersionedWorkspace &&
                                 (ifeatureLayer_0.FeatureClass as IVersionedObject).IsRegisteredAsVersioned)
                        {
                            if (
                                !(AppConfigInfo.UserID.Length == 0
                                    ? false
                                    : !(AppConfigInfo.UserID.ToLower() == "admin")))
                            {
                                flag1 = true;
                            }
                            else if (Editor.m_SysGrants.GetStaffAndRolesLayerPri(AppConfigInfo.UserID, 2,
                                (ifeatureLayer_0.FeatureClass as IDataset).Name))
                            {
                                flag1 = true;
                            }
                        }
                    }
                    workspace = null;
                    GC.Collect();
                    flag = flag1;
                }
                else
                {
                    flag = false;
                }
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        public static bool CheckLayerCanEdit(IFeatureClass ifeatureClass_0)
        {
            bool flag;
            if (Editor.EditWorkspace == null)
            {
                flag = false;
            }
            else if (ifeatureClass_0 == null)
            {
                flag = false;
            }
            else if (!(ifeatureClass_0 is ICoverageFeatureClass))
            {
                IWorkspace workspace = (ifeatureClass_0 as IDataset).Workspace;
                if (workspace.ConnectionProperties.IsEqual((Editor.EditWorkspace as IWorkspace).ConnectionProperties))
                {
                    if (workspace.Type != esriWorkspaceType.esriRemoteDatabaseWorkspace)
                    {
                        flag = true;
                        return flag;
                    }
                    else if (workspace is IVersionedWorkspace &&
                             (ifeatureClass_0 as IVersionedObject).IsRegisteredAsVersioned)
                    {
                        if ((AppConfigInfo.UserID.Length == 0 ? false : !(AppConfigInfo.UserID.ToLower() == "admin")))
                        {
                            if (
                                !Editor.SysGrants.GetStaffAndRolesLayerPri(AppConfigInfo.UserID, 2,
                                    (ifeatureClass_0 as IDataset).Name))
                            {
                                goto Label1;
                            }
                            flag = true;
                            return flag;
                        }
                        else
                        {
                            flag = true;
                            return flag;
                        }
                    }
                }
                Label1:
                flag = false;
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        private static void CheckStartEditing(IWorkspace pWorkspace)
        {
        }

        private static int CheckStartEditing(ILayer ilayer_0, ref string string_0)
        {
            int num;
            int num1 = 0;
            if (ilayer_0 is IGroupLayer)
            {
                ICompositeLayer ilayer0 = (ICompositeLayer) ilayer_0;
                for (int i = 0; i < ilayer0.Count; i++)
                {
                    num1 = num1 + Editor.CheckStartEditing(ilayer0.Layer[i], ref string_0);
                }
            }
            else if (ilayer_0 is IFeatureLayer)
            {
                IFeatureLayer featureLayer = (IFeatureLayer) ilayer_0;
                if (featureLayer.FeatureClass == null)
                {
                    num = num1;
                    return num;
                }
                IDataset featureClass = (IDataset) featureLayer.FeatureClass;
                if ((featureClass.Type == esriDatasetType.esriDTFeatureClass
                    ? false
                    : featureClass.Type != esriDatasetType.esriDTFeatureDataset))
                {
                    string_0 = string.Concat(string_0, "\\r\\n", featureLayer.Name);
                }
                else
                {
                    IWorkspaceEdit workspace = (IWorkspaceEdit) featureClass.Workspace;
                    if (featureClass.Workspace.Type != esriWorkspaceType.esriRemoteDatabaseWorkspace)
                    {
                        if (!workspace.IsBeingEdited())
                        {
                            try
                            {
                                workspace.StartEditing(true);
                                workspace.EnableUndoRedo();
                                num1++;
                            }
                            catch (Exception exception)
                            {
                                MessageBox.Show(exception.Message);
                            }
                        }
                    }
                    else if (!(featureClass.Workspace is IVersionedWorkspace))
                    {
                        string_0 = string.Concat(string_0, "\\r\\n", featureLayer.Name);
                    }
                    else if ((featureClass as IVersionedObject).IsRegisteredAsVersioned && !workspace.IsBeingEdited())
                    {
                        try
                        {
                            workspace.StartEditing(true);
                            workspace.EnableUndoRedo();
                            num1++;
                        }
                        catch (Exception exception1)
                        {
                            MessageBox.Show(exception1.Message);
                        }
                    }
                }
            }
            num = num1;
            return num;
        }

        private static bool CheckStopEdits(IDataset idataset_0, bool bool_0)
        {
            bool flag;
            bool flag1 = false;
            try
            {
                IWorkspaceEdit workspace = (IWorkspaceEdit) idataset_0.Workspace;
                if (workspace.IsBeingEdited())
                {
                    if (bool_0)
                    {
                        workspace.HasEdits(ref bool_0);
                    }
                    workspace.StopEditing(bool_0);
                    flag = true;
                    return flag;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            flag = flag1;
            return flag;
        }

        private static bool CheckStopEdits(ILayer ilayer_0, bool bool_0)
        {
            bool flag;
            bool flag1 = false;
            try
            {
                if (ilayer_0 is IGroupLayer)
                {
                    ICompositeLayer ilayer0 = (ICompositeLayer) ilayer_0;
                    for (int i = 0; i < ilayer0.Count; i++)
                    {
                        if (Editor.CheckStopEdits(ilayer0.Layer[i], bool_0))
                        {
                            flag1 = true;
                        }
                    }
                }
                else if (ilayer_0 is IFeatureLayer)
                {
                    IFeatureLayer featureLayer = (IFeatureLayer) ilayer_0;
                    if (featureLayer.FeatureClass != null)
                    {
                        IDataset featureClass = (IDataset) featureLayer.FeatureClass;
                        if ((featureClass.Type == esriDatasetType.esriDTFeatureClass
                            ? true
                            : featureClass.Type == esriDatasetType.esriDTFeatureDataset))
                        {
                            IWorkspaceEdit workspace = (IWorkspaceEdit) featureClass.Workspace;
                            if (workspace.IsBeingEdited())
                            {
                                if (bool_0)
                                {
                                    workspace.HasEdits(ref bool_0);
                                }
                                workspace.StopEditing(bool_0);
                            }
                        }
                    }
                    else
                    {
                        flag = false;
                        return flag;
                    }
                }
            }
            catch
            {
            }
            flag = flag1;
            return flag;
        }

        private static bool CheckUndoReDo(ILayer ilayer_0, bool bool_0)
        {
            bool flag;
            bool flag1 = false;
            bool flag2 = false;
            if (ilayer_0 is IGroupLayer)
            {
                ICompositeLayer ilayer0 = (ICompositeLayer) ilayer_0;
                int num = 0;
                while (num < ilayer0.Count)
                {
                    if (Editor.CheckUndoReDo(ilayer0.Layer[num], bool_0))
                    {
                        flag = true;
                        return flag;
                    }
                    else
                    {
                        num++;
                    }
                }
            }
            else if (ilayer_0 is IFeatureLayer)
            {
                IFeatureLayer featureLayer = (IFeatureLayer) ilayer_0;
                if (featureLayer.FeatureClass != null)
                {
                    IDataset featureClass = (IDataset) featureLayer.FeatureClass;
                    if (featureClass != null)
                    {
                        if ((featureClass.Type == esriDatasetType.esriDTFeatureClass
                            ? true
                            : featureClass.Type == esriDatasetType.esriDTFeatureDataset))
                        {
                            IWorkspaceEdit workspace = (IWorkspaceEdit) featureClass.Workspace;
                            if (!workspace.IsBeingEdited())
                            {
                                flag = false;
                                return flag;
                            }
                            if (!bool_0)
                            {
                                workspace.HasRedos(ref flag1);
                                if (flag1)
                                {
                                    workspace.RedoEditOperation();
                                }
                            }
                            else
                            {
                                workspace.HasUndos(ref flag2);
                                if (flag2)
                                {
                                    workspace.UndoEditOperation();
                                }
                            }
                            flag = true;
                            return flag;
                        }
                    }
                }
                else
                {
                    flag = false;
                    return flag;
                }
            }
            flag = false;
            return flag;
        }

        public static bool CheckWorkspaceEdit(IDataset idataset_0, string string_0)
        {
            bool flag;
            IWorkspaceEdit workspace = idataset_0.Workspace as IWorkspaceEdit;
            bool flag1 = false;
            if (workspace != null)
            {
                string string0 = string_0;
                if (string0 != null)
                {
                    if (string0 == "IsBeingEdited")
                    {
                        if (!workspace.IsBeingEdited())
                        {
                            goto Label1;
                        }
                        flag = true;
                        return flag;
                    }
                    else if (string0 == "hasEdits")
                    {
                        workspace.HasEdits(ref flag1);
                        flag = flag1;
                        return flag;
                    }
                    else if (string0 == "hasUndos")
                    {
                        workspace.HasUndos(ref flag1);
                        flag = flag1;
                        return flag;
                    }
                    else
                    {
                        if (string0 != "hasRedos")
                        {
                            goto Label1;
                        }
                        workspace.HasRedos(ref flag1);
                        flag = flag1;
                        return flag;
                    }
                }
                Label1:
                flag = false;
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        public static bool CheckWorkspaceEdit(ILayer ilayer_0, string string_0)
        {
            bool flag;
            bool flag1 = false;
            if (ilayer_0 is IGroupLayer)
            {
                ICompositeLayer ilayer0 = (ICompositeLayer) ilayer_0;
                int num = 0;
                while (num < ilayer0.Count)
                {
                    if (Editor.CheckWorkspaceEdit(ilayer0.Layer[num], string_0))
                    {
                        flag = true;
                        return flag;
                    }
                    else
                    {
                        num++;
                    }
                }
            }
            else if (ilayer_0 is IFeatureLayer)
            {
                IFeatureLayer featureLayer = (IFeatureLayer) ilayer_0;
                if (featureLayer.FeatureClass != null)
                {
                    IDataset featureClass = (IDataset) featureLayer.FeatureClass;
                    if ((featureClass.Type == esriDatasetType.esriDTFeatureClass
                        ? true
                        : featureClass.Type == esriDatasetType.esriDTFeatureDataset))
                    {
                        IWorkspaceEdit workspace = featureClass.Workspace as IWorkspaceEdit;
                        if (workspace != null)
                        {
                            string string0 = string_0;
                            if (string0 != null)
                            {
                                if (string0 == "IsBeingEdited")
                                {
                                    if (!workspace.IsBeingEdited())
                                    {
                                        flag = false;
                                        return flag;
                                    }
                                    flag = true;
                                    return flag;
                                }
                                else if (string0 == "hasEdits")
                                {
                                    workspace.HasEdits(ref flag1);
                                    flag = flag1;
                                    return flag;
                                }
                                else if (string0 == "hasUndos")
                                {
                                    workspace.HasUndos(ref flag1);
                                    flag = flag1;
                                    return flag;
                                }
                                else
                                {
                                    if (string0 != "hasRedos")
                                    {
                                        flag = false;
                                        return flag;
                                    }
                                    workspace.HasRedos(ref flag1);
                                    flag = flag1;
                                    return flag;
                                }
                            }
                        }
                        else
                        {
                            flag = false;
                            return flag;
                        }
                    }
                }
            }
            flag = false;
            return flag;
        }

        public static bool ComposeGeometry(IEnumFeature ienumFeature_0)
        {
            bool flag;
            try
            {
                ienumFeature_0.Reset();
                IFeature i = ienumFeature_0.Next();
                if (i != null)
                {
                    IClass @class = null;
                    ITopologicalOperator shapeCopy = i.ShapeCopy as ITopologicalOperator;
                    if (shapeCopy is IPolygon)
                    {
                        (shapeCopy as IPolygon).SimplifyPreserveFromTo();
                    }
                    @class = i.Class;
                    i = ienumFeature_0.Next();
                    while (i != null)
                    {
                        if (@class != i.Class)
                        {
                            MessageBox.Show("只能合并同层线或面要素!");
                            flag = false;
                            return flag;
                        }
                        else
                        {
                            shapeCopy.Simplify();
                            IGeometry geometry = i.ShapeCopy;
                            if (geometry is IPolygon)
                            {
                                (geometry as IPolygon).SimplifyPreserveFromTo();
                            }
                            shapeCopy = shapeCopy.Union(geometry) as ITopologicalOperator;
                            i = ienumFeature_0.Next();
                        }
                    }
                    ienumFeature_0.Reset();
                    i = ienumFeature_0.Next();
                    shapeCopy.Simplify();
                    i.Shape = shapeCopy as IGeometry;
                    i.Store();
                    for (i = ienumFeature_0.Next(); i != null; i = ienumFeature_0.Next())
                    {
                        i.Delete();
                    }
                    flag = true;
                    return flag;
                }
                else
                {
                    flag = false;
                    return flag;
                }
            }
            catch (COMException cOMException1)
            {
                COMException cOMException = cOMException1;
                if (cOMException.ErrorCode != -2147216063)
                {
                    MessageBox.Show("无法合并要素!");
                }
                else
                {
                    string message = cOMException.Message;
                    int num = message.IndexOf("[");
                    string str = "空间索引格网太小";
                    if (num != -1)
                    {
                        str = string.Concat(str, message.Substring(num));
                    }
                    MessageBox.Show(str, "合并要素", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                MessageBox.Show("无法合并要素!");
                Logger.Current.Error("", exception, "");
            }
            flag = false;
            return flag;
        }

        public static bool ConstructJunction(IMap imap_0, IFeature ifeature_0, IPoint pPoint, int int_0, int int_1,
            IFeature ifeature_1, IPoint ipoint_1, int int_2, int int_3)
        {
            bool flag;
            IPoint point;
            IPoint point1;
            bool flag1;
            bool flag2;
            object value;
            object pointCount;
            if (UtilityLicenseProviderCheck.Check())
            {
                IGeometryCollection shapeCopy = ifeature_0.ShapeCopy as IGeometryCollection;
                ISegmentCollection geometry = shapeCopy.Geometry[int_0] as ISegmentCollection;
                ISegment segment = null;
                if ((geometry as IPointCollection).PointCount == 2)
                {
                    point = (geometry as IPointCollection).Point[0];
                    point1 = (geometry as IPointCollection).Point[1];
                    if ((pPoint as IProximityOperator).ReturnDistance(point) <=
                        (pPoint as IProximityOperator).ReturnDistance(point1))
                    {
                        segment = geometry.Segment[0];
                        flag1 = true;
                    }
                    else
                    {
                        segment = geometry.Segment[geometry.SegmentCount - 1];
                        flag1 = false;
                    }
                }
                else if (int_1 >= geometry.SegmentCount/2)
                {
                    segment = geometry.Segment[geometry.SegmentCount - 1];
                    flag1 = false;
                }
                else
                {
                    segment = geometry.Segment[0];
                    flag1 = true;
                }
                IGeometryCollection geometryCollection = ifeature_1.ShapeCopy as IGeometryCollection;
                ISegmentCollection segmentCollection = geometryCollection.Geometry[int_2] as ISegmentCollection;
                ISegment segment1 = null;
                if ((segmentCollection as IPointCollection).PointCount == 2)
                {
                    point = (segmentCollection as IPointCollection).Point[0];
                    point1 = (segmentCollection as IPointCollection).Point[1];
                    if ((ipoint_1 as IProximityOperator).ReturnDistance(point) <=
                        (ipoint_1 as IProximityOperator).ReturnDistance(point1))
                    {
                        segment1 = segmentCollection.Segment[0];
                        flag2 = true;
                    }
                    else
                    {
                        segment1 = segmentCollection.Segment[segmentCollection.SegmentCount - 1];
                        flag2 = false;
                    }
                }
                else if (int_3 >= segmentCollection.SegmentCount/2)
                {
                    segment1 = segmentCollection.Segment[segmentCollection.SegmentCount - 1];
                    flag2 = false;
                }
                else
                {
                    segment1 = segmentCollection.Segment[0];
                    flag2 = true;
                }
                IPoint point2 = Editor.CalculateJunction(segment.FromPoint, segment.ToPoint, segment1.FromPoint,
                    segment1.ToPoint);
                if (point2 == null)
                {
                    flag = false;
                }
                else
                {
                    if ((geometry as IZAware).ZAware)
                    {
                        point2.Z = 0;
                    }
                    if (!flag1)
                    {
                        pointCount = (geometry as IPointCollection).PointCount - 1;
                        value = Missing.Value;
                        (geometry as IPointCollection).AddPoint(point2, ref value, ref pointCount);
                    }
                    else
                    {
                        value = 0;
                        pointCount = Missing.Value;
                        (geometry as IPointCollection).AddPoint(point2, ref value, ref pointCount);
                    }
                    shapeCopy.GeometriesChanged();
                    try
                    {
                        ifeature_0.Shape = shapeCopy as IGeometry;
                        ifeature_0.Store();
                    }
                    catch (Exception exception)
                    {
                    }
                    if (!flag2)
                    {
                        pointCount = (segmentCollection as IPointCollection).PointCount - 1;
                        value = Missing.Value;
                        (segmentCollection as IPointCollection).AddPoint(point2, ref value, ref pointCount);
                    }
                    else
                    {
                        value = 0;
                        pointCount = Missing.Value;
                        (segmentCollection as IPointCollection).AddPoint(point2, ref value, ref pointCount);
                    }
                    geometryCollection.GeometriesChanged();
                    ifeature_1.Shape = geometryCollection as IGeometry;
                    ifeature_1.Store();
                    flag = true;
                }
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        public static void CopyRow(IRow irow_0, IRow irow_1)
        {
            IFields fields = irow_1.Fields;
            for (int i = 0; i < fields.FieldCount; i++)
            {
                IField field = fields.Field[i];
                if ((field.Type == esriFieldType.esriFieldTypeOID || !field.Editable
                    ? false
                    : field.Type != esriFieldType.esriFieldTypeGeometry))
                {
                    try
                    {
                        irow_1.Value[i] = irow_0.Value[i];
                    }
                    catch
                    {
                    }
                }
            }
        }

        public static void CopyRowEx(IRow irow_0, IRow irow_1)
        {
            bool flag;
            if ((!CopyEnvironment.HasCopyMap ? false : CopyEnvironment.CopyMaps != null))
            {
                LayerMap layerMap = CopyEnvironment.CopyMaps.GetLayerMap((irow_0.Table as IDataset).Name,
                    (irow_1.Table as IDataset).Name);
                if (layerMap == null)
                {
                    goto Label1;
                }
                Editor.CopyRowEx(irow_0, irow_1, layerMap);
                return;
            }
            Label1:
            flag = (!(irow_0 is IFeature) ? true : !(irow_1 is IFeature));
            if (!flag)
            {
                (irow_1 as IFeature).Shape = (irow_0 as IFeature).Shape;
            }
            IFields fields = irow_0.Fields;
            IFields field = irow_1.Fields;
            for (int i = 0; i < fields.FieldCount; i++)
            {
                IField field1 = fields.Field[i];
                if ((field1.Type == esriFieldType.esriFieldTypeOID || field1.Type == esriFieldType.esriFieldTypeGeometry ||
                     field1.Type == esriFieldType.esriFieldTypeGUID || field1.Type == esriFieldType.esriFieldTypeGUID ||
                     field1.Type == esriFieldType.esriFieldTypeRaster || field1.Type == esriFieldType.esriFieldTypeBlob
                    ? false
                    : field1.Editable))
                {
                    int value = field.FindField(field1.Name);
                    if (value != -1)
                    {
                        IField field2 = field.Field[value];
                        if (field1.Type == field2.Type)
                        {
                            irow_1.Value[value] = irow_0.Value[i];
                        }
                    }
                }
            }
        }

        public static void CopyRowEx(IRow irow_0, IRow irow_1, LayerMap layerMap_0)
        {
            if ((!(irow_0 is IFeature) ? false : irow_1 is IFeature))
            {
                (irow_1 as IFeature).Shape = (irow_0 as IFeature).Shape;
            }
            layerMap_0.Copy(irow_0, irow_1);
        }

        public static void CopySelectFeatureToTargetFeatureLayer(IFeatureLayer ifeatureLayer_0,
            IFeatureLayer ifeatureLayer_1)
        {
        }

        public static void CreateFeature(IFeatureClass ifeatureClass_0, IGeometry igeometry_0)
        {
            double num;
            double num1;
            if (igeometry_0 != null)
            {
                IWorkspaceEdit workspace = (ifeatureClass_0 as IDataset).Workspace as IWorkspaceEdit;
                int num2 = ifeatureClass_0.FindField(ifeatureClass_0.ShapeFieldName);
                IGeometryDef geometryDef = ifeatureClass_0.Fields.Field[num2].GeometryDef;
                if (geometryDef.HasZ)
                {
                    ((IZAware) igeometry_0).ZAware = true;
                    geometryDef.SpatialReference.GetZDomain(out num, out num1);
                    IZ igeometry0 = igeometry_0 as IZ;
                    if (igeometry0 != null)
                    {
                        igeometry0.SetConstantZ(num);
                    }
                }
                if (geometryDef.HasM)
                {
                    ((IMAware) igeometry_0).MAware = true;
                }
                workspace.StartEditOperation();
                IFeature feature = ifeatureClass_0.CreateFeature();
                ((IRowSubtypes) feature).InitDefaultValues();
                feature.Shape = igeometry_0;
                feature.Store();
                workspace.StopEditOperation();
            }
        }

        private static void CreateFeature(IFeature ifeature_0, IGeometry igeometry_0)
        {
            try
            {
                IFeature igeometry0 = (ifeature_0.Class as IFeatureClass).CreateFeature();
                Editor.FeatureAttributreCopy(igeometry0, ifeature_0);
                igeometry0.Shape = igeometry_0;
                igeometry0.Store();
            }
            catch (Exception exception)
            {
                Logger.Current.Error("分解几何对象", exception, null);
            }
        }

        private static void CreateMultiPoint(string string_0, IMap imap_0, IFeatureLayer ifeatureLayer_0)
        {
            string str;
            try
            {
                StreamReader streamReader = new StreamReader(string_0);
                try
                {
                    int num = 0;
                    IPointCollection multipointClass = new Multipoint();
                    object value = Missing.Value;
                    while (true)
                    {
                        string str1 = streamReader.ReadLine();
                        string str2 = str1;
                        if (str1 == null)
                        {
                            break;
                        }
                        IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
                        num++;
                        string[] strArrays = str2.Split(new char[] {','});
                        try
                        {
                            if ((int) strArrays.Length >= 2)
                            {
                                pointClass.X = double.Parse(strArrays[0]);
                                pointClass.Y = double.Parse(strArrays[1]);
                                if ((int) strArrays.Length > 2)
                                {
                                    pointClass.Z = double.Parse(strArrays[2]);
                                }
                                if ((int) strArrays.Length > 3)
                                {
                                    pointClass.M = double.Parse(strArrays[3]);
                                }
                                multipointClass.AddPoint(pointClass, ref value, ref value);
                            }
                            else
                            {
                                str = string.Concat(string_0, " 中第 ", num.ToString(), "行数据有错误");

                                Logger.Current.Error(str);
                            }
                        }
                        catch
                        {
                            str = string.Concat(string_0, " 中第 ", num.ToString(), "行数据有错误");
                            Logger.Current.Error(str);
                        }
                    }
                    Editor.SetZMProperty(ifeatureLayer_0.FeatureClass, multipointClass as IGeometry);
                    CreateFeatureTool.CreateFeature(multipointClass as IGeometry, imap_0 as IActiveView, ifeatureLayer_0);
                }
                finally
                {
                    if (streamReader != null)
                    {
                        ((IDisposable) streamReader).Dispose();
                    }
                }
            }
            catch (Exception exception)
            {
                Logger.Current.Error("", exception, null);
            }
        }

        private static void CreatePoint(string string_0, IMap imap_0, IFeatureLayer ifeatureLayer_0)
        {
            string str;
            double num;
            double num1;
            double num2;
            double num3;
            try
            {
                int num4 = ifeatureLayer_0.FeatureClass.FindField(ifeatureLayer_0.FeatureClass.ShapeFieldName);
                IGeometryDef geometryDef = ifeatureLayer_0.FeatureClass.Fields.Field[num4].GeometryDef;
                StreamReader streamReader = new StreamReader(string_0);
                try
                {
                    IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
                    int num5 = 0;
                    while (true)
                    {
                        string str1 = streamReader.ReadLine();
                        string str2 = str1;
                        if (str1 == null)
                        {
                            break;
                        }
                        num5++;
                        string[] strArrays = str2.Split(new char[] {','});
                        try
                        {
                            if ((int) strArrays.Length >= 2)
                            {
                                pointClass.X = double.Parse(strArrays[0]);
                                pointClass.Y = double.Parse(strArrays[1]);
                                if ((int) strArrays.Length > 2)
                                {
                                    pointClass.Z = double.Parse(strArrays[2]);
                                }
                                else if (geometryDef.HasZ)
                                {
                                    geometryDef.SpatialReference.GetZDomain(out num, out num1);
                                    pointClass.Z = num;
                                }
                                if ((int) strArrays.Length > 3)
                                {
                                    pointClass.M = double.Parse(strArrays[3]);
                                }
                                else if (geometryDef.HasM)
                                {
                                    geometryDef.SpatialReference.GetMDomain(out num2, out num3);
                                    pointClass.M = num2;
                                }
                                try
                                {
                                    Editor.SetZMProperty(ifeatureLayer_0.FeatureClass, pointClass);
                                    CreateFeatureTool.CreateFeature(pointClass, imap_0 as IActiveView, ifeatureLayer_0);
                                }
                                catch (Exception exception)
                                {
                                    Logger.Current.Error("", exception, null);
                                }
                            }
                            else
                            {
                                str = string.Format("{0} 中第 {1}行数据有错误", string_0, num5);
                                Logger.Current.Error(str);
                            }
                        }
                        catch
                        {
                            str = string.Concat(string_0, " 中第 ", num5.ToString(), "行数据有错误");
                            Logger.Current.Error(str);
                        }
                    }
                }
                finally
                {
                    if (streamReader != null)
                    {
                        ((IDisposable) streamReader).Dispose();
                    }
                }
            }
            catch (Exception exception1)
            {
                Logger.Current.Error("", exception1, null);
            }
        }

        private static IPoint CreatePoint(string string_0, int int_0, IFeatureClass ifeatureClass_0)
        {
            IPoint point;
            double num;
            double num1;
            double num2;
            double num3;
            int num4 = ifeatureClass_0.FindField(ifeatureClass_0.ShapeFieldName);
            IGeometryDef geometryDef = ifeatureClass_0.Fields.Field[num4].GeometryDef;
            IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
            string[] strArrays = string_0.Split(new char[] {','});
            try
            {
                if ((int) strArrays.Length >= 2)
                {
                    pointClass.X = double.Parse(strArrays[0]);
                    pointClass.Y = double.Parse(strArrays[1]);
                    if ((int) strArrays.Length > 2)
                    {
                        pointClass.Z = double.Parse(strArrays[2]);
                    }
                    else if (geometryDef.HasZ)
                    {
                        geometryDef.SpatialReference.GetZDomain(out num, out num1);
                        pointClass.Z = num;
                    }
                    if ((int) strArrays.Length > 3)
                    {
                        pointClass.M = double.Parse(strArrays[3]);
                    }
                    else if (geometryDef.HasM)
                    {
                        geometryDef.SpatialReference.GetMDomain(out num2, out num3);
                        pointClass.M = num2;
                    }
                    point = pointClass;
                    return point;
                }
                else
                {
                    point = null;
                    return point;
                }
            }
            catch
            {
            }
            point = null;
            return point;
        }

        private static void CreatePolygon(string string_0, IMap imap_0, IFeatureLayer ifeatureLayer_0)
        {
            try
            {
                StreamReader streamReader = new StreamReader(string_0);
                try
                {
                    int num = 1;
                    object value = Missing.Value;
                    IGeometryCollection polygonClass = null;
                    polygonClass = new Polygon() as IGeometryCollection;
                    IPointCollection ringClass = null;
                    while (true)
                    {
                        string str = streamReader.ReadLine();
                        string str1 = str;
                        if (str == null)
                        {
                            break;
                        }
                        if (str1.Trim().Length != 0)
                        {
                            if (str1.ToLower() != "part")
                            {
                                IPoint point = Editor.CreatePoint(str1, num, ifeatureLayer_0.FeatureClass);
                                if (point != null)
                                {
                                    if (ringClass == null)
                                    {
                                        ringClass = new Ring();
                                    }
                                    ringClass.AddPoint(point, ref value, ref value);
                                }
                                else
                                {
                                    string str2 = string.Concat(string_0, " 中第 ", num.ToString(), "行数据有错误");
                                    Logger.Current.Error(str2);
                                }
                            }
                            else
                            {
                                if (ringClass != null)
                                {
                                    if ((ringClass.PointCount <= 2 ? false : polygonClass != null))
                                    {
                                        if (!(ringClass as IRing).IsClosed)
                                        {
                                            (ringClass as IRing).Close();
                                        }
                                        polygonClass.AddGeometry(ringClass as IGeometry, ref value, ref value);
                                    }
                                    ringClass = null;
                                }
                                ringClass = new Ring();
                            }
                        }
                    }
                    if (polygonClass != null)
                    {
                        try
                        {
                            if ((ringClass == null ? false : ringClass.PointCount > 2))
                            {
                                polygonClass.AddGeometry(ringClass as IGeometry, ref value, ref value);
                            }
                            if (polygonClass.GeometryCount <= 0)
                            {
                                MessageBox.Show("无法创建多边形!");
                            }
                            else
                            {
                                Editor.SetZMProperty(ifeatureLayer_0.FeatureClass, polygonClass as IGeometry);
                                CreateFeatureTool.CreateFeature(polygonClass as IGeometry, imap_0 as IActiveView,
                                    ifeatureLayer_0);
                            }
                        }
                        catch (Exception exception)
                        {
                            Logger.Current.Error("", exception, null);
                        }
                        polygonClass = null;
                    }
                }
                finally
                {
                    if (streamReader != null)
                    {
                        ((IDisposable) streamReader).Dispose();
                    }
                }
            }
            catch (Exception exception1)
            {
                Logger.Current.Error("", exception1, null);
            }
        }

        private static void CreatePolyline(string string_0, IMap imap_0, IFeatureLayer ifeatureLayer_0)
        {
            try
            {
                StreamReader streamReader = new StreamReader(string_0);
                try
                {
                    int num = 1;
                    object value = Missing.Value;
                    IGeometryCollection polylineClass = null;
                    polylineClass = new Polyline() as IGeometryCollection;
                    IPointCollection pathClass = null;
                    while (true)
                    {
                        string str = streamReader.ReadLine();
                        string str1 = str;
                        if (str == null)
                        {
                            break;
                        }
                        if (str1.Trim().Length != 0)
                        {
                            if (str1.ToLower() != "part")
                            {
                                IPoint point = Editor.CreatePoint(str1, num, ifeatureLayer_0.FeatureClass);
                                if (point != null)
                                {
                                    if (pathClass == null)
                                    {
                                        pathClass = new ESRI.ArcGIS.Geometry.Path();
                                    }
                                    pathClass.AddPoint(point, ref value, ref value);
                                }
                                else
                                {
                                    string str2 = string.Concat(string_0, " 中第 ", num.ToString(), "行数据有错误");
                                    Logger.Current.Error(str2);
                                }
                            }
                            else
                            {
                                if (pathClass != null)
                                {
                                    if ((pathClass.PointCount < 2 ? false : polylineClass != null))
                                    {
                                        polylineClass.AddGeometry(pathClass as IGeometry, ref value, ref value);
                                    }
                                    pathClass = null;
                                }
                                pathClass = new ESRI.ArcGIS.Geometry.Path();
                            }
                        }
                    }
                    if (polylineClass != null)
                    {
                        try
                        {
                            if ((pathClass == null ? false : pathClass.PointCount >= 2))
                            {
                                polylineClass.AddGeometry(pathClass as IGeometry, ref value, ref value);
                            }
                            if (polylineClass.GeometryCount <= 0)
                            {
                                MessageBox.Show("无法创建多边形!");
                            }
                            else
                            {
                                Editor.SetZMProperty(ifeatureLayer_0.FeatureClass, polylineClass as IGeometry);
                                CreateFeatureTool.CreateFeature(polylineClass as IGeometry, imap_0 as IActiveView,
                                    ifeatureLayer_0);
                            }
                        }
                        catch (Exception exception)
                        {
                            Logger.Current.Error("", exception, null);
                        }
                        polylineClass = null;
                    }
                }
                finally
                {
                    if (streamReader != null)
                    {
                        ((IDisposable) streamReader).Dispose();
                    }
                }
            }
            catch (Exception exception1)
            {
                Logger.Current.Error("", exception1, null);
            }
        }

        public static bool DeComposeGeometry(IFeature ifeature_0, IGeometryCollection igeometryCollection_0)
        {
            bool flag = false;
            if (igeometryCollection_0 != null && igeometryCollection_0.GeometryCount > 1)
            {
                flag = true;
                if (ifeature_0.Shape.GeometryType == esriGeometryType.esriGeometryMultipoint)
                {
                    Editor.DeComposeMultiPoint(ifeature_0, igeometryCollection_0);
                }
                else if (ifeature_0.Shape.GeometryType == esriGeometryType.esriGeometryPolygon)
                {
                    Editor.DeComposePolygon(ifeature_0, igeometryCollection_0);
                }
                else if (ifeature_0.Shape.GeometryType == esriGeometryType.esriGeometryPolyline)
                {
                    Editor.DeComposePolyline(ifeature_0, igeometryCollection_0);
                }
            }
            return flag;
        }

        private static void DeComposeMultiPoint(IFeature ifeature_0, IGeometryCollection igeometryCollection_0)
        {
            object value = Missing.Value;
            for (int i = 0; i < igeometryCollection_0.GeometryCount; i++)
            {
                IGeometry geometry = igeometryCollection_0.Geometry[i];
                IPointCollection multipointClass = new Multipoint();
                multipointClass.AddPoint(geometry as IPoint, ref value, ref value);
                Editor.CreateFeature(ifeature_0, multipointClass as IGeometry);
            }
            ifeature_0.Delete();
        }

        private static void DeComposePolygon(IFeature ifeature_0, IGeometryCollection igeometryCollection_0)
        {
            object value = Missing.Value;
            bool zAware = false;
            bool mAware = false;
            double zMin = 0;
            try
            {
                zAware = (igeometryCollection_0 as IZAware).ZAware;
                zMin = (igeometryCollection_0 as IZ).ZMin;
            }
            catch
            {
            }
            try
            {
                mAware = (igeometryCollection_0 as IMAware).MAware;
            }
            catch
            {
            }
            for (int i = 0; i < igeometryCollection_0.GeometryCount; i++)
            {
                IGeometry geometry = igeometryCollection_0.Geometry[i];
                IGeometryCollection polygonClass = new Polygon() as IGeometryCollection;
                (polygonClass as IZAware).ZAware = zAware;
                (polygonClass as IMAware).MAware = mAware;
                polygonClass.AddGeometry(geometry, ref value, ref value);
                if (zAware)
                {
                    (polygonClass as IZ).SetConstantZ(zMin);
                }
                (polygonClass as ITopologicalOperator).Simplify();
                Editor.CreateFeature(ifeature_0, polygonClass as IGeometry);
            }
            ifeature_0.Delete();
        }

        private static void DeComposePolyline(IFeature ifeature_0, IGeometryCollection igeometryCollection_0)
        {
            object value = Missing.Value;
            bool zAware = false;
            bool mAware = false;
            double zMin = 0;
            try
            {
                zAware = (igeometryCollection_0 as IZAware).ZAware;
                zMin = (igeometryCollection_0 as IZ).ZMin;
            }
            catch
            {
            }
            try
            {
                mAware = (igeometryCollection_0 as IMAware).MAware;
            }
            catch
            {
            }
            for (int i = 0; i < igeometryCollection_0.GeometryCount; i++)
            {
                IGeometry geometry = igeometryCollection_0.Geometry[i];
                IGeometryCollection polylineClass = new Polyline() as IGeometryCollection;
                (polylineClass as IZAware).ZAware = zAware;
                (polylineClass as IMAware).MAware = mAware;
                polylineClass.AddGeometry(geometry, ref value, ref value);
                if (zAware)
                {
                    (polylineClass as IZ).SetConstantZ(zMin);
                }
                (polylineClass as ITopologicalOperator).Simplify();
                Editor.CreateFeature(ifeature_0, polylineClass as IGeometry);
            }
            ifeature_0.Delete();
        }

        public static void DeletedSelectedFeatures(IMap imap_0)
        {
            IEnumFeature featureSelection = (IEnumFeature) imap_0.FeatureSelection;
            featureSelection.Reset();
            IFeature feature = featureSelection.Next();
            IWorkspaceEdit workspaceEdit = null;
            while (true)
            {
                if (feature != null)
                {
                    IWorkspaceEdit workspace = (IWorkspaceEdit) ((IDataset) feature.Class).Workspace;
                    if (workspace.IsBeingEdited())
                    {
                        workspaceEdit = workspace;
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            if (workspaceEdit != null)
            {
                Editor.DeletedSelectedFeatures(imap_0, workspaceEdit as IWorkspace);
            }
        }

        public static void DeletedSelectedFeatures(IMap imap_0, IWorkspace pWorkspace)
        {
            if (pWorkspace != null)
            {
                bool flag = false;
                IEnumFeature featureSelection = (IEnumFeature) imap_0.FeatureSelection;
                featureSelection.Reset();
                IFeature item = featureSelection.Next();
                IList<IFeature> features = new List<IFeature>();
                while (item != null)
                {
                    if (Editor.CheckIsEdit(item.Class as IDataset, pWorkspace))
                    {
                        features.Add(item);
                    }
                    item = featureSelection.Next();
                }
                if (features.Count > 0)
                {
                    (pWorkspace as IWorkspaceEdit).StartEditOperation();
                    for (int i = 0; i < features.Count; i++)
                    {
                        item = features[i];
                        ILayer layer = Editor.FindLayerByFeature(imap_0, item);
                        int oID = item.OID;
                        item.Delete();
                        flag = true;
                        EditorEvent.DeleteFeature(layer, oID);
                    }
                    (pWorkspace as IWorkspaceEdit).StopEditOperation();
                }
                imap_0.ClearSelection();
                if (flag)
                {
                    (imap_0 as IActiveView).Refresh();
                }
            }
        }

        public static bool DeleteFeature(IFeature ifeature_0, ref bool bool_0)
        {
            bool flag;
            IWorkspaceEdit workspace = null;
            bool flag1 = false;
            IDataset @class = (IDataset) ifeature_0.Class;
            if ((@class.Type == esriDatasetType.esriDTFeatureClass
                ? false
                : @class.Type != esriDatasetType.esriDTFeatureDataset))
            {
                flag1 = false;
            }
            else
            {
                workspace = (IWorkspaceEdit) @class.Workspace;
                if (!workspace.IsBeingEdited())
                {
                    flag1 = true;
                }
            }
            if (!flag1)
            {
                workspace.StartEditOperation();
                ifeature_0.Delete();
                workspace.StopEditOperation();
                bool_0 = true;
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        public static bool DeleteMultiVert(IFeature ifeature_0, int int_0, int int_1, int int_2, int int_3)
        {
            if (int_2 < int_0)
            {
                Editor.SplitGeometry2(ifeature_0, int_0, int_1, int_2, int_3);
            }
            else if (int_3 >= int_1)
            {
                Editor.SplitGeometry1(ifeature_0, int_0, int_1, int_2, int_3);
            }
            else
            {
                Editor.SplitGeometry2(ifeature_0, int_0, int_1, int_2, int_3);
            }
            return true;
        }

        public static void ExportGeometryCollection(StreamWriter streamWriter_0,
            IGeometryCollection igeometryCollection_0)
        {
            for (int i = 0; i < igeometryCollection_0.GeometryCount; i++)
            {
                streamWriter_0.WriteLine("partbegin");
                Editor.WritePointCollection(streamWriter_0, igeometryCollection_0.Geometry[i] as IPointCollection);
                streamWriter_0.WriteLine("partend");
            }
        }

        public static void ExportGeometryToTexe(string string_0, IFeatureLayer ifeatureLayer_0)
        {
            StreamWriter streamWriter = new StreamWriter(string_0);
            try
            {
                switch (ifeatureLayer_0.FeatureClass.ShapeType)
                {
                    case esriGeometryType.esriGeometryPoint:
                    case esriGeometryType.esriGeometryMultipoint:
                    {
                        break;
                    }
                    case esriGeometryType.esriGeometryPolyline:
                    {
                        Editor.ExportPolyline(streamWriter, ifeatureLayer_0);
                        goto case esriGeometryType.esriGeometryMultipoint;
                    }
                    case esriGeometryType.esriGeometryPolygon:
                    {
                        Editor.ExportPolygon(streamWriter, ifeatureLayer_0);
                        goto case esriGeometryType.esriGeometryMultipoint;
                    }
                    default:
                    {
                        goto case esriGeometryType.esriGeometryMultipoint;
                    }
                }
            }
            finally
            {
                if (streamWriter != null)
                {
                    ((IDisposable) streamWriter).Dispose();
                }
            }
        }

        public static void ExportGeometryToText(string string_0, IMap imap_0)
        {
            UID uIDClass = new UID()
            {
                Value = "{6CA416B1-E160-11D2-9F4E-00C04F6BC78E}"
            };
            IEnumLayer layers = imap_0.Layers[uIDClass, true];
            layers.Reset();
            ILayer layer = layers.Next();
            int num = 0;
            while (layer != null)
            {
                IFeatureLayer featureLayer = layer as IFeatureLayer;
                if (featureLayer != null)
                {
                    if ((featureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon
                            ? true
                            : featureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline) &&
                        (featureLayer as IFeatureSelection).SelectionSet.Count > 0)
                    {
                        string string0 = string_0;
                        if (num != 0)
                        {
                            string0 = string.Concat(string0, num.ToString());
                            num++;
                        }
                        Editor.ExportGeometryToTexe(string0, featureLayer);
                    }
                }
                layer = layers.Next();
            }
        }

        public static void ExportPolygon(StreamWriter streamWriter_0, IFeatureLayer ifeatureLayer_0)
        {
            ICursor cursor;
            IFeatureSelection ifeatureLayer0 = ifeatureLayer_0 as IFeatureSelection;
            if (ifeatureLayer0.SelectionSet.Count > 0)
            {
                ifeatureLayer0.SelectionSet.Search(null, false, out cursor);
                IFeatureCursor featureCursor = cursor as IFeatureCursor;
                for (IFeature i = featureCursor.NextFeature(); i != null; i = featureCursor.NextFeature())
                {
                    Editor.ExportPolygon(streamWriter_0, i.Shape as IPolygon);
                }
                ComReleaser.ReleaseCOMObject(featureCursor);
            }
        }

        public static void ExportPolygon(StreamWriter streamWriter_0, IPolygon ipolygon_0)
        {
            streamWriter_0.WriteLine("begin");
            Editor.ExportGeometryCollection(streamWriter_0, ipolygon_0 as IGeometryCollection);
            streamWriter_0.WriteLine("end");
        }

        public static void ExportPolyline(StreamWriter streamWriter_0, IFeatureLayer ifeatureLayer_0)
        {
            ICursor cursor;
            IFeatureSelection ifeatureLayer0 = ifeatureLayer_0 as IFeatureSelection;
            if (ifeatureLayer0.SelectionSet.Count > 0)
            {
                ifeatureLayer0.SelectionSet.Search(null, false, out cursor);
                IFeatureCursor featureCursor = cursor as IFeatureCursor;
                for (IFeature i = featureCursor.NextFeature(); i != null; i = featureCursor.NextFeature())
                {
                    Editor.ExportPolyline(streamWriter_0, i.Shape as IPolyline);
                }
                ComReleaser.ReleaseCOMObject(featureCursor);
            }
        }

        public static void ExportPolyline(StreamWriter streamWriter_0, IPolyline ipolyline_0)
        {
            streamWriter_0.WriteLine("begin");
            Editor.ExportGeometryCollection(streamWriter_0, ipolyline_0 as IGeometryCollection);
            streamWriter_0.WriteLine("end");
        }

        public static IPolyline ExtendPolyLine(IMap imap_0, IGeometry igeometry_0)
        {
            IPolyline polyline;
            IEnumFeature featureSelection = imap_0.FeatureSelection as IEnumFeature;
            if (featureSelection != null)
            {
                featureSelection.Reset();
                IFeature feature = featureSelection.Next();
                IConstructCurve polylineClass = new Polyline() as IConstructCurve;
                ICurve igeometry0 = igeometry_0 as ICurve;
                if (igeometry0 != null)
                {
                    bool flag = true;
                    while (feature != null)
                    {
                        ICurve shape = feature.Shape as ICurve;
                        if (shape != null)
                        {
                            polylineClass.ConstructExtended(igeometry0, shape, 0, ref flag);
                            if (!(polylineClass as IGeometry).IsEmpty)
                            {
                                polyline = polylineClass as IPolyline;
                                return polyline;
                            }
                        }
                        feature = featureSelection.Next();
                    }
                    polyline = null;
                }
                else
                {
                    polyline = null;
                }
            }
            else
            {
                polyline = null;
            }
            return polyline;
        }

        public static IPolyline ExtendPolyLineEx(IMap imap_0, IGeometry igeometry_0)
        {
            IPolyline polyline;
            bool flag;
            IPointCollection geometry;
            IEnumFeature featureSelection = imap_0.FeatureSelection as IEnumFeature;
            if (featureSelection != null)
            {
                featureSelection.Reset();
                IFeature feature = featureSelection.Next();
                IConstructCurve polylineClass = new Polyline() as IConstructCurve;
                ICurve igeometry0 = igeometry_0 as ICurve;
                if (igeometry0 != null)
                {
                    bool flag1 = true;
                    while (feature != null)
                    {
                        ICurve shape = feature.Shape as ICurve;
                        if (shape != null)
                        {
                            polylineClass.ConstructExtended(igeometry0, shape, 0, ref flag1);
                            if (!(polylineClass as IGeometry).IsEmpty)
                            {
                                if (((feature.Class as IDataset).Workspace as IWorkspaceEdit).IsBeingEdited())
                                {
                                    IPoint point = (polylineClass as IPointCollection).Point[0];
                                    double mapUnits = CommonHelper.ConvertPixelsToMapUnits(imap_0 as IActiveView, 8);
                                    IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
                                    double num = 0;
                                    int num1 = -1;
                                    int num2 = -1;
                                    if (
                                        !GeometryOperator.TestGeometryHit(mapUnits, point, shape, out pointClass,
                                            ref num, ref num1, ref num2, out flag))
                                    {
                                        point =
                                            (polylineClass as IPointCollection).Point[
                                                (polylineClass as IPointCollection).PointCount - 1];
                                        GeometryOperator.TestGeometryHit(mapUnits, point, shape, out pointClass, ref num,
                                            ref num1, ref num2, out flag);
                                        if (!flag)
                                        {
                                            pointClass.Z = 0;
                                            geometry = (shape as IGeometryCollection).Geometry[num1] as IPointCollection;
                                            geometry.InsertPoints(num2 + 1, 1, ref pointClass);
                                            (shape as IGeometryCollection).GeometriesChanged();
                                            try
                                            {
                                                feature.Shape = shape;
                                                feature.Store();
                                            }
                                            catch
                                            {
                                            }
                                        }
                                    }
                                    else if (!flag)
                                    {
                                        pointClass.Z = 0;
                                        geometry = (shape as IGeometryCollection).Geometry[num1] as IPointCollection;
                                        geometry.InsertPoints(num2 + 1, 1, ref pointClass);
                                        (shape as IGeometryCollection).GeometriesChanged();
                                        try
                                        {
                                            feature.Shape = shape;
                                            feature.Store();
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                                polyline = polylineClass as IPolyline;
                                return polyline;
                            }
                        }
                        feature = featureSelection.Next();
                    }
                    polyline = null;
                }
                else
                {
                    polyline = null;
                }
            }
            else
            {
                polyline = null;
            }
            return polyline;
        }

        public static void FeatureAttributreCopy(IFeature ifeature_0, IFeature ifeature_1)
        {
            string shapeFieldName = (ifeature_0.Class as IFeatureClass).ShapeFieldName;
            IFields fields = ifeature_0.Fields;
            IFields field = ifeature_1.Fields;
            for (int i = 0; i < fields.FieldCount; i++)
            {
                IField field1 = fields.Field[i];
                if ((!(field1.Name != shapeFieldName) || field1.Type == esriFieldType.esriFieldTypeOID
                    ? false
                    : field1.Editable))
                {
                    int num = field.FindField(field1.Name);
                    if (num != -1 && (field.Field[num] as IClone).IsEqual(field1 as IClone))
                    {
                        ifeature_0.Value[i] = ifeature_1.Value[num];
                    }
                }
            }
        }

        private static IFeatureLayer FindLayerByFeature(ICompositeLayer icompositeLayer_0, IFeature ifeature_0)
        {
            IFeatureLayer featureLayer;
            int num = 0;
            while (true)
            {
                if (num < icompositeLayer_0.Count)
                {
                    ILayer layer = icompositeLayer_0.Layer[num];
                    if (layer is IFeatureLayer)
                    {
                        if (ifeature_0.Class == (layer as IFeatureLayer).FeatureClass)
                        {
                            featureLayer = layer as IFeatureLayer;
                            break;
                        }
                    }
                    else if (layer is IGroupLayer)
                    {
                        IFeatureLayer featureLayer1 = Editor.FindLayerByFeature(layer as ICompositeLayer, ifeature_0);
                        if (featureLayer1 != null)
                        {
                            featureLayer = featureLayer1;
                            break;
                        }
                    }
                    num++;
                }
                else
                {
                    featureLayer = null;
                    break;
                }
            }
            return featureLayer;
        }

        public static IFeatureLayer FindLayerByFeature(IMap imap_0, IFeature ifeature_0)
        {
            IFeatureLayer featureLayer;
            int num = 0;
            while (true)
            {
                if (num < imap_0.LayerCount)
                {
                    ILayer layer = imap_0.Layer[num];
                    if (layer is IFeatureLayer)
                    {
                        if (ifeature_0.Class == (layer as IFeatureLayer).FeatureClass)
                        {
                            featureLayer = layer as IFeatureLayer;
                            break;
                        }
                    }
                    else if (layer is IGroupLayer)
                    {
                        IFeatureLayer featureLayer1 = Editor.FindLayerByFeature(layer as ICompositeLayer, ifeature_0);
                        if (featureLayer1 != null)
                        {
                            featureLayer = featureLayer1;
                            break;
                        }
                    }
                    num++;
                }
                else
                {
                    featureLayer = null;
                    break;
                }
            }
            return featureLayer;
        }

        private static IPoint FindPoint(IPointCollection pPointCollection, IPoint pPoint, double pDistance)
        {
            IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
            double num = -100;
            for (int i = 0; i < pPointCollection.PointCount; i++)
            {
                IPoint point = pPointCollection.Point[i];
                double num1 = CommonHelper.distance(pPoint, point);
                if (num1 <= pDistance)
                {
                    if (num < 0)
                    {
                        num = num1;
                        pointClass.X = point.X;
                        pointClass.Y = point.Y;
                        pointClass.Z = point.Z;
                    }
                    else if (num > num1)
                    {
                        num = num1;
                        pointClass.X = point.X;
                        pointClass.Y = point.Y;
                        pointClass.Z = point.Z;
                    }
                }
            }
            return pointClass;
        }

        private static int FindWorkspce(IArray pArray, IWorkspace pWorkspace, out bool isNew)
        {
            int count;
            IPropertySet connectionProperties = pWorkspace.ConnectionProperties;
            int num = 0;
            while (true)
            {
                if (num >= pArray.Count)
                {
                    isNew = true;
                    pArray.Add(pWorkspace);
                    count = pArray.Count - 1;
                    break;
                }
                else if (connectionProperties.IsEqual((pArray.Element[num] as IWorkspace).ConnectionProperties))
                {
                    isNew = false;
                    count = num;
                    break;
                }
                else
                {
                    num++;
                }
            }
            return count;
        }

        private static EditWorkspaceInfo FindWorkspce(IArray pArray, IWorkspace pWorkspace)
        {
            EditWorkspaceInfo element;
            EditWorkspaceInfo editWorkspaceInfo;
            IPropertySet connectionProperties = pWorkspace.ConnectionProperties;
            int num = 0;
            while (true)
            {
                if (num < pArray.Count)
                {
                    element = pArray.Element[num] as EditWorkspaceInfo;
                    if (connectionProperties.IsEqual(element.Workspace.ConnectionProperties))
                    {
                        editWorkspaceInfo = element;
                        break;
                    }
                    else
                    {
                        num++;
                    }
                }
                else
                {
                    element = new EditWorkspaceInfo(pWorkspace);
                    pArray.Add(element);
                    editWorkspaceInfo = element;
                    break;
                }
            }
            return editWorkspaceInfo;
        }

        public static void GetClosesFeature(IMap imap_0, IPoint pPoint, double pDistance, out IFeature ifeature_0)
        {
            IEnvelope envelope = pPoint.Envelope;
            envelope.Width = pDistance;
            envelope.Height = pDistance;
            envelope.CenterAt(pPoint);
            UID uIDClass = new UID()
            {
                Value = "{6CA416B1-E160-11D2-9F4E-00C04F6BC78E}"
            };
            IEnumLayer layers = imap_0.Layers[uIDClass, true];
            ISpatialFilter spatialFilterClass = new SpatialFilter()
            {
                Geometry = envelope,
                SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects
            };
            ifeature_0 = null;
            double num = -10;
            double num1 = 0;
            IProximityOperator ipoint0 = (IProximityOperator) pPoint;
            layers.Reset();
            for (ILayer i = layers.Next(); i != null; i = layers.Next())
            {
                if (i is IFeatureLayer)
                {
                    IFeatureCursor featureCursor = (i as IFeatureLayer).FeatureClass.Search(spatialFilterClass, false);
                    for (IFeature j = featureCursor.NextFeature(); j != null; j = featureCursor.NextFeature())
                    {
                        IGeometry shapeCopy = j.ShapeCopy;
                        if (num >= 0)
                        {
                            try
                            {
                                if (num < ipoint0.ReturnDistance(shapeCopy))
                                {
                                    num1 = num;
                                    ifeature_0 = j;
                                }
                            }
                            catch
                            {
                            }
                        }
                        else
                        {
                            try
                            {
                                num = ipoint0.ReturnDistance(shapeCopy);
                                num1 = num;
                                ifeature_0 = j;
                            }
                            catch
                            {
                            }
                        }
                    }
                    ComReleaser.ReleaseCOMObject(featureCursor);
                }
            }
        }

        public static void GetClosesFeature(IMap imap_0, IPoint pPoint, double pDistance,
            esriGeometryType esriGeometryType_0, out IFeature ifeature_0)
        {
            IEnvelope envelope = pPoint.Envelope;
            double double0 = pDistance;
            envelope.Width = double0;
            envelope.Height = double0;
            envelope.CenterAt(pPoint);
            UID uIDClass = new UID()
            {
                Value = "{6CA416B1-E160-11D2-9F4E-00C04F6BC78E}"
            };
            IEnumLayer layers = imap_0.Layers[uIDClass, true];
            ISpatialFilter spatialFilterClass = new SpatialFilter()
            {
                Geometry = envelope,
                SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects
            };
            ifeature_0 = null;
            double num = -10;
            double num1 = 0;
            IProximityOperator ipoint0 = (IProximityOperator) pPoint;
            layers.Reset();
            ILayer layer = layers.Next();
            while (layer != null)
            {
                if (layer is IFeatureLayer)
                {
                    if ((layer as IFeatureLayer).FeatureClass.FeatureType != esriFeatureType.esriFTSimple)
                    {
                        layer = layers.Next();
                        continue;
                    }
                    else if (
                        !(esriGeometryType_0 == esriGeometryType.esriGeometryAny
                            ? true
                            : esriGeometryType_0 == (layer as IFeatureLayer).FeatureClass.ShapeType))
                    {
                        layer = layers.Next();
                        continue;
                    }
                    else if (Editor.CheckLayerCanEdit(layer as IFeatureLayer))
                    {
                        IFeatureCursor featureCursor = (layer as IFeatureLayer).FeatureClass.Search(spatialFilterClass,
                            false);
                        for (IFeature i = featureCursor.NextFeature(); i != null; i = featureCursor.NextFeature())
                        {
                            IGeometry shapeCopy = i.ShapeCopy;
                            if (num < 0)
                            {
                                num = ipoint0.ReturnDistance(shapeCopy);
                                num1 = num;
                                ifeature_0 = i;
                            }
                            else if (num < ipoint0.ReturnDistance(shapeCopy))
                            {
                                num1 = num;
                                ifeature_0 = i;
                            }
                        }
                        ComReleaser.ReleaseCOMObject(featureCursor);
                    }
                    else
                    {
                        layer = layers.Next();
                        continue;
                    }
                }
                layer = layers.Next();
            }
        }

        public static void GetClosestSelectedFeature(IFeatureCache ifeatureCache_0, IPoint pPoint,
            out IFeature ifeature_0)
        {
            if (ifeatureCache_0.Count == 1)
            {
                ifeature_0 = ifeatureCache_0.Feature[0];
            }
            else if (ifeatureCache_0.Count <= 1)
            {
                ifeature_0 = null;
            }
            else
            {
                IProximityOperator ipoint0 = (IProximityOperator) pPoint;
                IFeature feature = ifeatureCache_0.Feature[0];
                double num = ipoint0.ReturnDistance(feature.ShapeCopy);
                ifeature_0 = feature;
                for (int i = 1; i < ifeatureCache_0.Count; i++)
                {
                    feature = ifeatureCache_0.Feature[i];
                    double num1 = ipoint0.ReturnDistance(feature.Shape);
                    if (num1 < num)
                    {
                        num = num1;
                        ifeature_0 = feature;
                    }
                }
            }
        }

        private static double GetClosestSelectedFeature(IPoint pPoint, IFeatureCursor ifeatureCursor_0,
            ref IFeature ifeature_0)
        {
            double num;
            IProximityOperator ipoint0 = (IProximityOperator) pPoint;
            IFeature feature = ifeatureCursor_0.NextFeature();
            if (feature != null)
            {
                if (ifeature_0 == null)
                {
                    ifeature_0 = feature;
                    feature = ifeatureCursor_0.NextFeature();
                }
                double num1 = ipoint0.ReturnDistance(ifeature_0.ShapeCopy);
                while (feature != null)
                {
                    double num2 = ipoint0.ReturnDistance(feature.Shape);
                    if (num2 < num1)
                    {
                        num1 = num2;
                        ifeature_0 = feature;
                    }
                    feature = ifeatureCursor_0.NextFeature();
                }
                num = num1;
            }
            else
            {
                num = 0;
            }
            return num;
        }

        public static void GetClosestSelectedFeature(IMap imap_0, IPoint pPoint, out IFeature ifeature_0)
        {
            ifeature_0 = null;
            double num = 0;
            double num1 = 0;
            IProximityOperator ipoint0 = (IProximityOperator) pPoint;
            IEnumFeature featureSelection = (IEnumFeature) imap_0.FeatureSelection;
            featureSelection.Reset();
            for (IFeature i = featureSelection.Next(); i != null; i = featureSelection.Next())
            {
                if (Editor.CheckIsEdit(i.Class as IDataset))
                {
                    IGeometry shapeCopy = i.ShapeCopy;
                    if (ifeature_0 != null)
                    {
                        try
                        {
                            num1 = ipoint0.ReturnDistance(shapeCopy);
                        }
                        catch
                        {
                        }
                        if (num1 < num)
                        {
                            num = num1;
                            ifeature_0 = i;
                        }
                    }
                    else
                    {
                        try
                        {
                            num = ipoint0.ReturnDistance(shapeCopy);
                        }
                        catch
                        {
                        }
                        ifeature_0 = i;
                    }
                }
            }
        }

        private static IWorkspace GetEditWorkspace(IMap imap_0)
        {
            IWorkspace editWorkspace;
            IArray arrayClass = new ESRI.ArcGIS.esriSystem.Array();
            IPropertySet propertySetClass = new PropertySet();
            if (imap_0 != null)
            {
                for (int i = 0; i < imap_0.LayerCount; i++)
                {
                    Editor.GetWorkspce(arrayClass, propertySetClass, imap_0.Layer[i]);
                }
                if (arrayClass.Count != 1)
                {
                    frmSelectEditDatasource _frmSelectEditDatasource = new frmSelectEditDatasource()
                    {
                        EditWorkspaceInfo = arrayClass
                    };
                    if (_frmSelectEditDatasource.ShowDialog() != DialogResult.OK)
                    {
                        editWorkspace = null;
                        return editWorkspace;
                    }
                    editWorkspace = Editor.EditWorkspace as IWorkspace;
                    return editWorkspace;
                }
                else
                {
                    editWorkspace = arrayClass.Element[0] as IWorkspace;
                    return editWorkspace;
                }
            }
            editWorkspace = null;
            return editWorkspace;
        }

        public static IEnvelope GetEnvelope(IGeometry igeometry_0)
        {
            IEnvelope envelope;
            if (igeometry_0.GeometryType != esriGeometryType.esriGeometryPoint)
            {
                envelope = igeometry_0.Envelope;
            }
            else
            {
                IEnvelope envelope1 = igeometry_0.Envelope;
                envelope1.CenterAt(igeometry_0 as IPoint);
                envelope1.Expand(20, 20, false);
                envelope = envelope1;
            }
            return envelope;
        }

        public static IFeature GetHitFeature(IMap imap_0, IPoint pPoint, double pDistance,
            out IFeatureLayer ifeatureLayer_0)
        {
            ifeatureLayer_0 = null;
            IFeature feature = null;
            double num = 0;
            IEnvelope envelope = pPoint.Envelope;
            envelope.Height = pDistance;
            envelope.Width = pDistance;
            envelope.CenterAt(pPoint);
            ISpatialFilter spatialFilterClass = new SpatialFilter()
            {
                Geometry = envelope,
                SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects
            };
            for (int i = 0; i < imap_0.LayerCount; i++)
            {
                IFeatureLayer layer = imap_0.Layer[i] as IFeatureLayer;
                if (layer != null && layer.Visible)
                {
                    spatialFilterClass.GeometryField = layer.FeatureClass.ShapeFieldName;
                    IFeatureCursor featureCursor = layer.Search(spatialFilterClass, false);
                    IFeature feature1 = null;
                    double closestSelectedFeature = Editor.GetClosestSelectedFeature(pPoint, featureCursor, ref feature1);
                    if (feature1 != null)
                    {
                        if ((feature == null ? true : num > closestSelectedFeature))
                        {
                            num = closestSelectedFeature;
                            feature = feature1;
                            ifeatureLayer_0 = layer;
                        }
                    }
                    ComReleaser.ReleaseCOMObject(featureCursor);
                    featureCursor = null;
                }
            }
            return feature;
        }

        public static IFeature GetHitFeature(IFeatureLayer ifeatureLayer_0, IPoint pPoint, double pDistance)
        {
            IFeature feature = null;
            IEnvelope envelope = pPoint.Envelope;
            envelope.Height = pDistance;
            envelope.Width = pDistance;
            envelope.CenterAt(pPoint);
            ISpatialFilter spatialFilterClass = new SpatialFilter()
            {
                Geometry = envelope,
                SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects
            };
            if (ifeatureLayer_0.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
            {
                spatialFilterClass.GeometryField = ifeatureLayer_0.FeatureClass.ShapeFieldName;
                IFeatureCursor featureCursor = ifeatureLayer_0.Search(spatialFilterClass, false);
                Editor.GetClosestSelectedFeature(pPoint, featureCursor, ref feature);
                ComReleaser.ReleaseCOMObject(featureCursor);
            }
            return feature;
        }

        public static IFeature GetHitLineFeature(IMap imap_0, IPoint pPoint, double pDistance)
        {
            IFeature feature = null;
            double num = 0;
            IEnvelope envelope = pPoint.Envelope;
            envelope.Height = pDistance;
            envelope.Width = pDistance;
            envelope.CenterAt(pPoint);
            ISpatialFilter spatialFilterClass = new SpatialFilter()
            {
                Geometry = envelope,
                SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects
            };
            for (int i = 0; i < imap_0.LayerCount; i++)
            {
                IFeatureLayer layer = imap_0.Layer[i] as IFeatureLayer;
                if (layer != null && layer.Visible &&
                    layer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                {
                    spatialFilterClass.GeometryField = layer.FeatureClass.ShapeFieldName;
                    IFeatureCursor featureCursor = layer.Search(spatialFilterClass, false);
                    IFeature feature1 = null;
                    double closestSelectedFeature = Editor.GetClosestSelectedFeature(pPoint, featureCursor, ref feature1);
                    if (feature1 != null)
                    {
                        if ((feature == null ? true : num > closestSelectedFeature))
                        {
                            num = closestSelectedFeature;
                            feature = feature1;
                        }
                    }
                    ComReleaser.ReleaseCOMObject(featureCursor);
                    featureCursor = null;
                }
            }
            return feature;
        }

        public static IFeature GetHitLineFeature(IMap imap_0, IPoint pPoint, double pDistance,
            out IFeatureLayer ifeatureLayer_0)
        {
            ifeatureLayer_0 = null;
            IFeature feature = null;
            double num = 0;
            IEnvelope envelope = pPoint.Envelope;
            envelope.Height = pDistance;
            envelope.Width = pDistance;
            envelope.CenterAt(pPoint);
            ISpatialFilter spatialFilterClass = new SpatialFilter()
            {
                Geometry = envelope,
                SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects
            };
            for (int i = 0; i < imap_0.LayerCount; i++)
            {
                IFeatureLayer layer = imap_0.Layer[i] as IFeatureLayer;
                if (layer != null && layer.Visible &&
                    layer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                {
                    spatialFilterClass.GeometryField = layer.FeatureClass.ShapeFieldName;
                    IFeatureCursor featureCursor = layer.Search(spatialFilterClass, false);
                    IFeature feature1 = null;
                    double closestSelectedFeature = Editor.GetClosestSelectedFeature(pPoint, featureCursor, ref feature1);
                    if (feature1 != null)
                    {
                        if ((feature == null ? true : num > closestSelectedFeature))
                        {
                            num = closestSelectedFeature;
                            feature = feature1;
                            ifeatureLayer_0 = layer;
                        }
                    }
                    ComReleaser.ReleaseCOMObject(featureCursor);
                    featureCursor = null;
                }
            }
            return feature;
        }

        public static IFeature GetHitLineOrAreaFeature(IMap imap_0, IPoint pPoint, double pDistance,
            out IFeatureLayer ifeatureLayer_0)
        {
            ifeatureLayer_0 = null;
            IFeature feature = null;
            double num = 0;
            IEnvelope envelope = pPoint.Envelope;
            envelope.Height = pDistance;
            envelope.Width = pDistance;
            envelope.CenterAt(pPoint);
            ISpatialFilter spatialFilterClass = new SpatialFilter()
            {
                Geometry = envelope,
                SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects
            };
            for (int i = 0; i < imap_0.LayerCount; i++)
            {
                IFeatureLayer layer = imap_0.Layer[i] as IFeatureLayer;
                if (layer != null && layer.Visible)
                {
                    if ((layer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline
                        ? true
                        : layer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon))
                    {
                        spatialFilterClass.GeometryField = layer.FeatureClass.ShapeFieldName;
                        IFeatureCursor featureCursor = layer.Search(spatialFilterClass, false);
                        IFeature feature1 = null;
                        double closestSelectedFeature = Editor.GetClosestSelectedFeature(pPoint, featureCursor,
                            ref feature1);
                        if (feature1 != null)
                        {
                            if ((feature == null ? true : num > closestSelectedFeature))
                            {
                                num = closestSelectedFeature;
                                feature = feature1;
                                ifeatureLayer_0 = layer;
                            }
                        }
                        ComReleaser.ReleaseCOMObject(featureCursor);
                        featureCursor = null;
                    }
                }
            }
            return feature;
        }

        public static IList<IFeature> GetIntersectsLineFeatures(IMap imap_0, IEnvelope ienvelope_0)
        {
            IList<IFeature> features = new List<IFeature>();
            ISpatialFilter spatialFilterClass = new SpatialFilter()
            {
                Geometry = ienvelope_0,
                SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects
            };
            for (int i = 0; i < imap_0.LayerCount; i++)
            {
                IFeatureLayer layer = imap_0.Layer[i] as IFeatureLayer;
                if (layer != null && layer.Visible &&
                    layer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                {
                    spatialFilterClass.GeometryField = layer.FeatureClass.ShapeFieldName;
                    IFeatureCursor featureCursor = layer.Search(spatialFilterClass, false);
                    for (IFeature j = featureCursor.NextFeature(); j != null; j = featureCursor.NextFeature())
                    {
                        features.Add(j);
                    }
                    ComReleaser.ReleaseCOMObject(featureCursor);
                    featureCursor = null;
                }
            }
            return features;
        }

        private static void GetWorkspce(IArray pArray, IPropertySet ipropertySet_0, ILayer ilayer_0)
        {
            bool flag;
            if (ilayer_0 is IGroupLayer)
            {
                ICompositeLayer ilayer0 = ilayer_0 as ICompositeLayer;
                for (int i = 0; i < ilayer0.Count; i++)
                {
                    Editor.GetWorkspce(pArray, ipropertySet_0, ilayer0.Layer[i]);
                }
            }
            else if (ilayer_0 is IFeatureLayer)
            {
                IDataset featureClass = (ilayer_0 as IFeatureLayer).FeatureClass as IDataset;
                if (featureClass != null)
                {
                    IWorkspace workspace = featureClass.Workspace;
                    int num = Editor.FindWorkspce(pArray, workspace, out flag);
                    IArray property = null;
                    if (!flag)
                    {
                        property = ipropertySet_0.GetProperty(num.ToString()) as IArray;
                    }
                    else
                    {
                        property = new ESRI.ArcGIS.esriSystem.Array();
                        ipropertySet_0.SetProperty(num.ToString(), property);
                    }
                    property.Add(ilayer_0);
                }
            }
        }

        public static void ImportGeometryData(string string_0, IMap imap_0, IFeatureLayer ifeatureLayer_0)
        {
            IWorkspaceEdit workspace = (ifeatureLayer_0.FeatureClass as IDataset).Workspace as IWorkspaceEdit;
            workspace.StartEditOperation();
            switch (ifeatureLayer_0.FeatureClass.ShapeType)
            {
                case esriGeometryType.esriGeometryPoint:
                {
                    Editor.CreatePoint(string_0, imap_0, ifeatureLayer_0);
                    break;
                }
                case esriGeometryType.esriGeometryMultipoint:
                {
                    Editor.CreateMultiPoint(string_0, imap_0, ifeatureLayer_0);
                    break;
                }
                case esriGeometryType.esriGeometryPolyline:
                {
                    Editor.CreatePolyline(string_0, imap_0, ifeatureLayer_0);
                    break;
                }
                case esriGeometryType.esriGeometryPolygon:
                {
                    Editor.CreatePolygon(string_0, imap_0, ifeatureLayer_0);
                    break;
                }
            }
            workspace.StopEditOperation();
        }

        public static bool LayerCanEdit(IFeatureLayer ifeatureLayer_0)
        {
            bool flag;
            if (Editor.EditWorkspace != null)
            {
                IDataset featureClass = ifeatureLayer_0.FeatureClass as IDataset;
                if (featureClass == null)
                {
                    flag = false;
                }
                else if (!(featureClass is ICoverageFeatureClass))
                {
                    try
                    {
                        if (ApplicationRef.AppContext.Config.IsSupportZD &&
                            ZD.ZDRegister.IsZDFeatureClass(ifeatureLayer_0.FeatureClass))
                        {
                            flag = false;
                            return flag;
                        }
                    }
                    catch
                    {
                    }
                    IPropertySet connectionProperties = featureClass.Workspace.ConnectionProperties;
                    IWorkspace editWorkspace = Editor.EditWorkspace as IWorkspace;
                    if (connectionProperties.IsEqual(editWorkspace.ConnectionProperties))
                    {
                        if (editWorkspace.Type != esriWorkspaceType.esriRemoteDatabaseWorkspace)
                        {
                            if ((AppConfigInfo.UserID.Length == 0 ? false : !(AppConfigInfo.UserID.ToLower() == "admin")))
                            {
                                if (!Editor.LayerIsHasEditprivilege(ifeatureLayer_0))
                                {
                                    goto Label1;
                                }
                                flag = true;
                                return flag;
                            }
                            else
                            {
                                flag = true;
                                return flag;
                            }
                        }
                        else if (editWorkspace is IVersionedWorkspace)
                        {
                            if ((featureClass as IVersionedObject).IsRegisteredAsVersioned)
                            {
                                if ((AppConfigInfo.UserID.Length == 0
                                    ? false
                                    : !(AppConfigInfo.UserID.ToLower() == "admin")))
                                {
                                    if (
                                        !Editor.m_SysGrants.GetStaffAndRolesDatasetPri(AppConfigInfo.UserID, 2,
                                            featureClass.Name))
                                    {
                                        goto Label1;
                                    }
                                    flag = true;
                                    return flag;
                                }
                                else
                                {
                                    flag = true;
                                    return flag;
                                }
                            }
                        }
                        else if ((AppConfigInfo.UserID.Length == 0 ? false : !(AppConfigInfo.UserID.ToLower() == "admin")))
                        {
                            if (!Editor.LayerIsHasEditprivilege(ifeatureLayer_0))
                            {
                                goto Label1;
                            }
                            flag = true;
                            return flag;
                        }
                        else
                        {
                            flag = true;
                            return flag;
                        }
                    }
                    Label1:
                    flag = false;
                }
                else
                {
                    flag = false;
                }
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        public static bool LayerIsHasEditprivilege(IFeatureLayer ifeatureLayer_0)
        {
            bool flag;
            if ((AppConfigInfo.UserID.Length == 0 ? false : !(AppConfigInfo.UserID.ToLower() == "admin")))
            {
                IDataset featureClass = ifeatureLayer_0.FeatureClass as IDataset;
                if (featureClass == null)
                {
                    flag = false;
                }
                else if (!(featureClass is ICoverageFeatureClass))
                {
                    IFeatureDataset featureDataset = (featureClass as IFeatureClass).FeatureDataset;
                    if (featureDataset == null)
                    {
                        if (!Editor.m_SysGrants.GetStaffAndRolesDatasetPri(AppConfigInfo.UserID, 2, featureClass.Name))
                        {
                            goto Label1;
                        }
                        flag = true;
                        return flag;
                    }
                    else
                    {
                        int num = 0;
                        Editor.m_SysGrants.GetStaffAndRolesDatasetPri(AppConfigInfo.UserID, featureDataset.Name, out num);
                        if (num != 0)
                        {
                            if (num != 2)
                            {
                                goto Label1;
                            }
                            flag = true;
                            return flag;
                        }
                        else
                        {
                            if (
                                !Editor.m_SysGrants.GetStaffAndRolesDatasetPri(AppConfigInfo.UserID, 2,
                                    featureClass.Name))
                            {
                                goto Label1;
                            }
                            flag = true;
                            return flag;
                        }
                    }
                    Label1:
                    flag = false;
                }
                else
                {
                    flag = false;
                }
            }
            else
            {
                flag = true;
            }
            return flag;
        }

        public static IPointCollection LineIntersect(IPolyline ipolyline_0, IEnvelope ienvelope_0)
        {
            IPointCollection polylineClass = new Polyline() as IPointCollection;
            object value = Missing.Value;
            polylineClass.AddPoint(ienvelope_0.LowerLeft, ref value, ref value);
            polylineClass.AddPoint(ienvelope_0.LowerRight, ref value, ref value);
            polylineClass.AddPoint(ienvelope_0.UpperRight, ref value, ref value);
            polylineClass.AddPoint(ienvelope_0.UpperLeft, ref value, ref value);
            polylineClass.AddPoint(ienvelope_0.LowerLeft, ref value, ref value);
            ITopologicalOperator ipolyline0 = ipolyline_0 as ITopologicalOperator;
            IRelationalOperator relationalOperator = ipolyline_0 as IRelationalOperator;
            IPointCollection multipointClass = new Multipoint();
            if (relationalOperator.Crosses(polylineClass as IGeometry))
            {
                IGeometry geometry = ipolyline0.Intersect(polylineClass as IGeometry,
                    esriGeometryDimension.esriGeometry0Dimension);
                if (geometry is IPoint)
                {
                    multipointClass.AddPoint(geometry as IPoint, ref value, ref value);
                }
                else if (geometry is IPointCollection)
                {
                    multipointClass.AddPointCollection(geometry as IPointCollection);
                }
            }
            return multipointClass;
        }

        public static void MoveSelectedFeatures(IMap imap_0, IWorkspace pWorkspace, double pDistance, double double_1)
        {
            if (pWorkspace != null)
            {
                if ((pDistance != 0 ? true : double_1 != 0))
                {
                    bool flag = false;
                    IEnumFeature featureSelection = (IEnumFeature) imap_0.FeatureSelection;
                    featureSelection.Reset();
                    IFeature feature = featureSelection.Next();
                    (pWorkspace as IWorkspaceEdit).StartEditOperation();
                    while (feature != null)
                    {
                        if (Editor.CheckIsEdit(feature.Class as IDataset, pWorkspace))
                        {
                            ITransform2D shapeCopy = feature.ShapeCopy as ITransform2D;
                            if (shapeCopy != null)
                            {
                                try
                                {
                                    shapeCopy.Move(pDistance, double_1);
                                    feature.Shape = shapeCopy as IGeometry;
                                    feature.Store();
                                    flag = true;
                                }
                                catch (Exception exception)
                                {
                                    Logger.Current.Error("", exception, "");
                                }
                            }
                        }
                        feature = featureSelection.Next();
                    }
                    (pWorkspace as IWorkspaceEdit).StopEditOperation();
                    if (flag)
                    {
                        (imap_0 as IActiveView).Refresh();
                    }
                }
            }
        }

        private static void old_acctor_mc()
        {
            Editor.m_IsAdvanceControlTool = false;
            Editor.m_EnableSketch = true;
            Editor._DrawNode = false;
            Editor.m_CurrentEditTemplate = null;
            Editor.UseOldSnap = false;
            Editor.m_SysGrants = new SysGrants(AppConfigInfo.UserID);
            Editor.m_EnableUndoRedo = true;
            Editor.m_pEditMap = null;
            Editor.m_IsCheckRegisterVeision = true;
            Editor.m_CurrentSkectchLayer = null;
            Editor.m_pEditWorkspace = null;
            Editor.IsRemoveUnEditLayerSelect = true;
        }

        public static void RefreshLayerWithSelection(IMap imap_0, IEnvelope ienvelope_0,
            esriViewDrawPhase esriViewDrawPhase_0)
        {
            if (imap_0 != null)
            {
                IActiveView imap0 = (IActiveView) imap_0;
                if (ienvelope_0 == null)
                {
                    IEnumFeature featureSelection = (IEnumFeature) imap_0.FeatureSelection;
                    featureSelection.Reset();
                    IFeature feature = featureSelection.Next();
                    ienvelope_0 = Editor.GetEnvelope(feature.Shape);
                    while (feature != null)
                    {
                        ienvelope_0.Union(feature.Extent);
                        feature = featureSelection.Next();
                    }
                }
                if (ienvelope_0 != null)
                {
                    double mapUnits = CommonHelper.ConvertPixelsToMapUnits(imap0, 1);
                    ienvelope_0.Expand(mapUnits, mapUnits, false);
                }
                imap0.PartialRefresh(esriViewDrawPhase_0, null, ienvelope_0);
                for (int i = 0; i < imap_0.LayerCount; i++)
                {
                    ILayer layer = imap_0.Layer[i];
                    if (!(layer is IFeatureLayer))
                    {
                        imap0.PartialRefresh(esriViewDrawPhase_0, layer, ienvelope_0);
                    }
                    else
                    {
                        IFeatureLayer featureLayer = (IFeatureLayer) imap_0.Layer[i];
                        if (((IFeatureSelection) featureLayer).SelectionSet.Count > 0)
                        {
                            imap0.PartialRefresh(esriViewDrawPhase_0, featureLayer, ienvelope_0);
                        }
                    }
                }
            }
        }

        internal static void ReleaseComObject(object object_0)
        {
            ComReleaser.ReleaseCOMObject(object_0);
            object_0 = null;
        }

        public static void SaveEditing(IMap imap_0)
        {
            Editor.StopEditing(imap_0, true, false);
            (imap_0 as IActiveView).Refresh();
            Editor.StartEditing(imap_0, m_pContext);
        }

        public static void SaveEditing()
        {
            if (Editor.EditMap != null)
            {
                Editor.SaveEditing(Editor.EditMap);
            }
        }

        public static void SaveEditing(IDataset idataset_0)
        {
            Editor.StopEditing(idataset_0, true, false);
            Editor.StartEditing(idataset_0);
        }

        public static void SetGeometryZM(IGeometry igeometry_0, IFeatureClass ifeatureClass_0)
        {
            double num;
            double num1;
            int num2 = ifeatureClass_0.FindField(ifeatureClass_0.ShapeFieldName);
            IGeometryDef geometryDef = ifeatureClass_0.Fields.Field[num2].GeometryDef;
            if (geometryDef.HasZ)
            {
                ((IZAware) igeometry_0).ZAware = true;
                if (igeometry_0 is IZ)
                {
                    IZ igeometry0 = (IZ) igeometry_0;
                    geometryDef.SpatialReference.GetZDomain(out num, out num1);
                    igeometry0.SetConstantZ(num);
                }
                else if (igeometry_0 is IPoint)
                {
                    geometryDef.SpatialReference.GetZDomain(out num, out num1);
                    (igeometry_0 as IPoint).Z = num;
                }
            }
            if (geometryDef.HasM)
            {
                ((IMAware) igeometry_0).MAware = true;
            }
        }

        public static void SetGeometryZM(IGeometry igeometry_0, IGeometry igeometry_1)
        {
            if ((igeometry_1 as IZAware).ZAware)
            {
                ((IZAware) igeometry_0).ZAware = true;
                if (igeometry_0 is IZ)
                {
                    IZ igeometry0 = (IZ) igeometry_0;
                    if (igeometry_1 is IZ)
                    {
                        igeometry0.SetConstantZ((igeometry_1 as IZ).ZMin);
                    }
                    else if (igeometry_1 is IPoint)
                    {
                        igeometry0.SetConstantZ((igeometry_1 as IPoint).Z);
                    }
                }
                else if (igeometry_0 is IPoint)
                {
                    if (igeometry_1 is IZ)
                    {
                        (igeometry_0 as IPoint).Z = (igeometry_1 as IZ).ZMin;
                    }
                    else if (igeometry_1 is IPoint)
                    {
                        (igeometry_0 as IPoint).Z = (igeometry_1 as IPoint).Z;
                    }
                }
            }
            if ((igeometry_1 as IMAware).MAware)
            {
                ((IMAware) igeometry_0).MAware = true;
            }
        }

        public static void SetZMProperty(IFeatureClass ifeatureClass_0, IGeometry igeometry_0)
        {
            int num = ifeatureClass_0.FindField(ifeatureClass_0.ShapeFieldName);
            IGeometryDef geometryDef = ifeatureClass_0.Fields.Field[num].GeometryDef;
            if (geometryDef.HasZ)
            {
                ((IZAware) igeometry_0).ZAware = true;
            }
            if (geometryDef.HasM)
            {
                ((IMAware) igeometry_0).MAware = true;
            }
        }

        public static bool Snap2Point(IPoint pPoint, IPoint ipoint_1, IPoint ipoint_2,
            ISnapEnvironment isnapEnvironment_0, IActiveView iactiveView_0, ISymbol isymbol_0,
            ref IAnchorPoint ianchorPoint_0)
        {
            bool flag = false;
            if ((isnapEnvironment_0 == null ? true : !isnapEnvironment_0.SnapPoint(pPoint, ipoint_1)))
            {
                flag = false;
                if (ianchorPoint_0 != null)
                {
                    ianchorPoint_0.Symbol = isymbol_0;
                    ianchorPoint_0.MoveTo(ipoint_1, iactiveView_0.ScreenDisplay);
                }
                else
                {
                    ianchorPoint_0 = Editor.AddNewAnchorPt(ipoint_1, iactiveView_0, isymbol_0);
                }
            }
            else
            {
                flag = true;
                if (ianchorPoint_0 != null)
                {
                    ianchorPoint_0.Symbol = isymbol_0;
                    ianchorPoint_0.MoveTo(ipoint_2, iactiveView_0.ScreenDisplay);
                }
                else
                {
                    ianchorPoint_0 = Editor.AddNewAnchorPt(ipoint_2, iactiveView_0, isymbol_0);
                }
            }
            return flag;
        }

        public static bool SnapToPoint(IPoint pPoint, IMap imap_0, double pDistance, out IPoint ipoint_1)
        {
            bool flag;
            int i;
            IFeature feature;
            IPoint shape;
            double num;
            IPoint pointClass;
            ipoint_1 = null;
            if (Editor.UseSnap)
            {
                UID uIDClass = new UID()
                {
                    Value = "{6CA416B1-E160-11D2-9F4E-00C04F6BC78E}"
                };
                IEnumLayer layers = imap_0.Layers[uIDClass, true];
                IFeatureCache2 featureCacheClass = new FeatureCache() as IFeatureCache2;
                featureCacheClass.Initialize(pPoint, pDistance);
                featureCacheClass.AddLayers(layers, null);
                if (featureCacheClass.Count != 0)
                {
                    double num1 = -10;
                    if (Editor.IsSnapPoint)
                    {
                        for (i = 0; i < featureCacheClass.Count; i++)
                        {
                            feature = featureCacheClass.Feature[i];
                            if (feature.Shape.GeometryType == esriGeometryType.esriGeometryPoint)
                            {
                                shape = feature.Shape as IPoint;
                                num = CommonHelper.distance(pPoint, shape);
                                if (num1 < 0)
                                {
                                    num1 = num;
                                    ipoint_1 = feature.ShapeCopy as IPoint;
                                }
                                else if (num1 > num)
                                {
                                    num1 = num;
                                    ipoint_1.X = shape.X;
                                    ipoint_1.Y = shape.Y;
                                    ipoint_1.Z = shape.Z;
                                }
                            }
                            else if (feature.Shape.GeometryType == esriGeometryType.esriGeometryMultipoint)
                            {
                                shape = Editor.FindPoint(feature.Shape as IPointCollection, pPoint, pDistance);
                                num = CommonHelper.distance(pPoint, shape);
                                if (num1 < 0)
                                {
                                    num1 = num;
                                    ipoint_1 = shape;
                                }
                                else if (num1 > num)
                                {
                                    num1 = num;
                                    ipoint_1.X = shape.X;
                                    ipoint_1.Y = shape.Y;
                                    ipoint_1.Z = shape.Z;
                                }
                            }
                        }
                        if (ipoint_1 == null)
                        {
                            goto Label1;
                        }
                        flag = true;
                        return flag;
                    }
                    Label1:
                    double num2 = 0;
                    int num3 = 0;
                    int num4 = 0;
                    bool flag1 = true;
                    if (Editor.IsSnapEndPoint)
                    {
                        for (i = 0; i < featureCacheClass.Count; i++)
                        {
                            feature = featureCacheClass.Feature[i];
                            if (feature.Shape.GeometryType == esriGeometryType.esriGeometryPolyline)
                            {
                                pointClass = new ESRI.ArcGIS.Geometry.Point();
                                if (((IHitTest) feature.Shape).HitTest(pPoint, pDistance,
                                    esriGeometryHitPartType.esriGeometryPartEndpoint, pointClass, ref num2, ref num3,
                                    ref num4, ref flag1))
                                {
                                    if (num1 < 0)
                                    {
                                        num1 = num2;
                                        ipoint_1 = pointClass;
                                    }
                                    else if (num1 > num2)
                                    {
                                        num1 = num2;
                                        ipoint_1.X = pointClass.X;
                                        ipoint_1.Y = pointClass.Y;
                                        ipoint_1.Z = pointClass.Z;
                                    }
                                }
                            }
                        }
                        if (ipoint_1 == null)
                        {
                            goto Label2;
                        }
                        flag = true;
                        return flag;
                    }
                    Label2:
                    if (Editor.IsSnapVertexPoint)
                    {
                        for (i = 0; i < featureCacheClass.Count; i++)
                        {
                            feature = featureCacheClass.Feature[i];
                            if ((feature.Shape.GeometryType == esriGeometryType.esriGeometryPoint
                                ? false
                                : feature.Shape.GeometryType != esriGeometryType.esriGeometryMultipoint))
                            {
                                pointClass = new ESRI.ArcGIS.Geometry.Point();
                                if (((IHitTest) feature.Shape).HitTest(pPoint, pDistance,
                                    esriGeometryHitPartType.esriGeometryPartVertex, pointClass, ref num2, ref num3,
                                    ref num4, ref flag1))
                                {
                                    if (num1 < 0)
                                    {
                                        num1 = num2;
                                        ipoint_1 = pointClass;
                                    }
                                    else if (num1 > num2)
                                    {
                                        num1 = num2;
                                        ipoint_1.X = pointClass.X;
                                        ipoint_1.Y = pointClass.Y;
                                        ipoint_1.Z = pointClass.Z;
                                    }
                                }
                            }
                        }
                    }
                    flag = false;
                }
                else
                {
                    flag = false;
                }
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        private static void SplitGeometry1(IFeature ifeature_0, int int_0, int int_1, int int_2, int int_3)
        {
            IPointCollection geometry;
            IGeometryCollection polygonClass;
            int i;
            IGeometryCollection shapeCopy = ifeature_0.ShapeCopy as IGeometryCollection;
            object value = Missing.Value;
            if (int_2 != int_0)
            {
                if (int_2 - int_0 - 1 > 0)
                {
                    shapeCopy.RemoveGeometries(int_0, int_2 - int_0 - 1);
                }
                geometry = shapeCopy.Geometry[int_0 + 1] as IPointCollection;
                if (int_3 != geometry.PointCount - 1)
                {
                    geometry.RemovePoints(0, int_3 + 1);
                    shapeCopy.GeometriesChanged();
                }
                else
                {
                    shapeCopy.RemoveGeometries(int_0 + 1, 1);
                }
                geometry = shapeCopy.Geometry[int_0] as IPointCollection;
                if (int_1 != 0)
                {
                    geometry.RemovePoints(int_0, geometry.PointCount - int_0);
                    shapeCopy.GeometriesChanged();
                }
                else
                {
                    shapeCopy.RemoveGeometries(int_0, 1);
                }
                if ((shapeCopy as IGeometry).IsEmpty)
                {
                    ifeature_0.Delete();
                }
                else
                {
                    ifeature_0.Shape = shapeCopy as IGeometry;
                    ifeature_0.Store();
                }
            }
            else
            {
                geometry = shapeCopy.get_Geometry(int_0) as IPointCollection;
                if (int_3 - int_1 + 1 != geometry.PointCount)
                {
                    if (ifeature_0.Shape.GeometryType != esriGeometryType.esriGeometryPolyline)
                    {
                        polygonClass = new Polygon() as IGeometryCollection;
                    }
                    else
                    {
                        polygonClass = new Polyline() as IGeometryCollection;
                    }
                    for (i = 0; i < int_0 - 1; i++)
                    {
                        polygonClass.AddGeometry((shapeCopy.Geometry[i] as IClone).Clone() as IGeometry, ref value,
                            ref value);
                    }
                    IPointCollection ringClass = null;
                    if (int_1 != 0)
                    {
                        if (ifeature_0.Shape.GeometryType != esriGeometryType.esriGeometryPolyline)
                        {
                            ringClass = new Ring();
                        }
                        else
                        {
                            ringClass = new ESRI.ArcGIS.Geometry.Path();
                        }
                        for (i = 0; i <= int_1; i++)
                        {
                            ringClass.AddPoint(geometry.Point[i], ref value, ref value);
                        }
                        if (ifeature_0.Shape.GeometryType == esriGeometryType.esriGeometryPolyline)
                        {
                            polygonClass.AddGeometry(ringClass as IGeometry, ref value, ref value);
                        }
                    }
                    if (int_3 != geometry.PointCount - 1)
                    {
                        if (ifeature_0.Shape.GeometryType == esriGeometryType.esriGeometryPolyline)
                        {
                            ringClass = new ESRI.ArcGIS.Geometry.Path();
                        }
                        else if (ringClass == null)
                        {
                            ringClass = new Ring();
                        }
                        for (i = int_3; i < geometry.PointCount; i++)
                        {
                            ringClass.AddPoint(geometry.Point[i], ref value, ref value);
                        }
                        if (ringClass is IRing)
                        {
                            (ringClass as IRing).Close();
                        }
                        polygonClass.AddGeometry(ringClass as IGeometry, ref value, ref value);
                    }
                    else if (ifeature_0.Shape.GeometryType == esriGeometryType.esriGeometryPolygon && ringClass == null)
                    {
                        (ringClass as IRing).Close();
                        polygonClass.AddGeometry(ringClass as IGeometry, ref value, ref value);
                    }
                    for (i = int_0 + 1; i < shapeCopy.GeometryCount; i++)
                    {
                        polygonClass.AddGeometry((shapeCopy.Geometry[i] as IClone).Clone() as IGeometry, ref value,
                            ref value);
                    }
                }
                else
                {
                    shapeCopy.RemoveGeometries(int_0, 1);
                    polygonClass = shapeCopy;
                }
                if ((polygonClass as IGeometry).IsEmpty)
                {
                    ifeature_0.Delete();
                }
                else
                {
                    if (polygonClass is IPolygon)
                    {
                        (polygonClass as IPolygon).Close();
                    }
                    ifeature_0.Shape = polygonClass as IGeometry;
                    ifeature_0.Store();
                }
            }
        }

        private static void SplitGeometry2(IFeature ifeature_0, int int_0, int int_1, int int_2, int int_3)
        {
            IPointCollection geometry;
            IGeometryCollection polygonClass;
            int i;
            IPointCollection ringClass;
            IGeometryCollection shapeCopy = ifeature_0.ShapeCopy as IGeometryCollection;
            object value = Missing.Value;
            if (int_2 != int_0)
            {
                if (int_0 != shapeCopy.GeometryCount - 1)
                {
                    shapeCopy.RemoveGeometries(int_0 + 1, shapeCopy.GeometryCount - int_0 - 1);
                }
                if (int_2 != 0)
                {
                    shapeCopy.RemoveGeometries(0, int_2);
                }
                geometry = shapeCopy.Geometry[0] as IPointCollection;
                if (int_3 != 0)
                {
                    geometry.RemovePoints(0, int_3);
                }
                if (geometry is IRing)
                {
                    (geometry as IRing).Close();
                }
                shapeCopy.GeometriesChanged();
                geometry = shapeCopy.Geometry[int_0 - int_2] as IPointCollection;
                if (int_1 != geometry.PointCount - 1)
                {
                    geometry.RemovePoints(int_1 + 1, geometry.PointCount - int_1 - 1);
                }
                if (geometry is IRing)
                {
                    (geometry as IRing).Close();
                }
                shapeCopy.GeometriesChanged();
                if ((shapeCopy as IGeometry).IsEmpty)
                {
                    ifeature_0.Delete();
                }
                else
                {
                    ifeature_0.Shape = shapeCopy as IGeometry;
                    ifeature_0.Store();
                }
            }
            else
            {
                geometry = shapeCopy.Geometry[int_0] as IPointCollection;
                if (int_3 - int_1 + 1 != geometry.PointCount)
                {
                    if (ifeature_0.Shape.GeometryType != esriGeometryType.esriGeometryPolyline)
                    {
                        polygonClass = new Polygon() as IGeometryCollection;
                    }
                    else
                    {
                        polygonClass = new Polyline() as IGeometryCollection;
                    }
                    for (i = 0; i < int_0 - 1; i++)
                    {
                        polygonClass.AddGeometry((shapeCopy.Geometry[i] as IClone).Clone() as IGeometry, ref value,
                            ref value);
                    }
                    if (ifeature_0.Shape.GeometryType != esriGeometryType.esriGeometryPolyline)
                    {
                        ringClass = new Ring();
                    }
                    else
                    {
                        ringClass = new ESRI.ArcGIS.Geometry.Path();
                    }
                    for (i = int_3; i < int_1 + 1; i++)
                    {
                        ringClass.AddPoint(geometry.Point[i], ref value, ref value);
                    }
                    if (!(ringClass is IRing))
                    {
                        polygonClass.AddGeometry(ringClass as IGeometry, ref value, ref value);
                    }
                    else if (ringClass.PointCount > 2)
                    {
                        (ringClass as IRing).Close();
                        polygonClass.AddGeometry(ringClass as IGeometry, ref value, ref value);
                    }
                    for (i = int_0 + 1; i < shapeCopy.GeometryCount; i++)
                    {
                        polygonClass.AddGeometry((shapeCopy.Geometry[i] as IClone).Clone() as IGeometry, ref value,
                            ref value);
                    }
                }
                else
                {
                    shapeCopy.RemoveGeometries(int_0, 1);
                    polygonClass = shapeCopy;
                }
                if ((polygonClass as IGeometry).IsEmpty)
                {
                    ifeature_0.Delete();
                }
                else
                {
                    ifeature_0.Shape = polygonClass as IGeometry;
                    try
                    {
                        ifeature_0.Store();
                    }
                    catch (Exception exception)
                    {
                    }
                }
            }
        }

        public static bool SplitPolylineAtPoint(IFeature ifeature_0, IPoint pPoint, bool bool_0)
        {
            bool flag;
            int num;
            int num1;
            int i;
            bool flag1;
            IPolycurve shapeCopy = ifeature_0.ShapeCopy as IPolycurve;
            IEnvelope envelope = shapeCopy.Envelope;
            shapeCopy.SplitAtPoint(pPoint, true, true, out flag, out num, out num1);
            if ((!flag ? true : !bool_0))
            {
                ifeature_0.Store();
            }
            else
            {
                object value = Missing.Value;
                try
                {
                    IGeometryCollection polylineClass = new Polyline() as IGeometryCollection;
                    for (i = 0; i < num; i++)
                    {
                        polylineClass.AddGeometry((shapeCopy as IGeometryCollection).Geometry[i], ref value, ref value);
                    }
                    if ((polylineClass as IPointCollection).PointCount > 1)
                    {
                        ifeature_0.Shape = polylineClass as IGeometry;
                        ifeature_0.Store();
                    }
                    polylineClass = new Polyline() as IGeometryCollection;
                    for (i = num; i < (shapeCopy as IGeometryCollection).GeometryCount; i++)
                    {
                        polylineClass.AddGeometry((shapeCopy as IGeometryCollection).Geometry[i], ref value, ref value);
                    }
                    if ((polylineClass as IPointCollection).PointCount > 1)
                    {
                        IFeature feature = RowOperator.CreatRowByRow(ifeature_0 as Row) as IFeature;
                        feature.Shape = polylineClass as IGeometry;
                        feature.Store();
                    }
                }
                catch (Exception exception)
                {
                    Logger.Current.Error("", exception, "");
                    flag1 = false;
                    return flag1;
                }
            }
            flag1 = true;
            return flag1;
        }

        public static bool SplitPolylineAtPoints(IFeature ifeature_0, IPointCollection pPointCollection, bool bool_0)
        {
            int i;
            bool flag;
            int num;
            int num1;
            int j;
            IFeature feature;
            bool flag1;
            IPolycurve shapeCopy = ifeature_0.ShapeCopy as IPolycurve;
            IList<int> nums = new List<int>();
            IList<int> nums1 = new List<int>();
            for (i = 0; i < pPointCollection.PointCount; i++)
            {
                shapeCopy.SplitAtPoint(pPointCollection.Point[i], true, true, out flag, out num, out num1);
                if ((!flag || !bool_0))
                {
                }
                else
                {
                    int num2 = -1;
                    for (j = 0; j < nums.Count; j++)
                    {
                        if (nums[j] >= num)
                        {
                            if (num2 == -1)
                            {
                                num2 = j;
                            }
                            nums[j] = nums[j] + 1;
                        }
                    }
                    if (num2 != -1)
                    {
                        nums.Insert(num2, num);
                    }
                    else
                    {
                        nums.Add(num);
                    }
                    nums1.Add(num1);
                }
            }
            if (!bool_0)
            {
                ifeature_0.Store();
            }
            else if (nums.Count > 0)
            {
                object value = Missing.Value;
                IGeometryCollection polylineClass = new Polyline() as IGeometryCollection;
                for (i = 0; i < nums[0]; i++)
                {
                    polylineClass.AddGeometry((shapeCopy as IGeometryCollection).Geometry[i], ref value, ref value);
                }
                if ((polylineClass as IPointCollection).PointCount > 1)
                {
                    ifeature_0.Shape = polylineClass as IGeometry;
                    ifeature_0.Store();
                }
                for (i = 0; i < nums.Count - 1; i++)
                {
                    try
                    {
                        polylineClass = new Polyline() as IGeometryCollection;
                        for (j = nums[i]; j < nums[i + 1]; j++)
                        {
                            polylineClass.AddGeometry((shapeCopy as IGeometryCollection).Geometry[j], ref value,
                                ref value);
                        }
                        if ((polylineClass as IPointCollection).PointCount > 1)
                        {
                            feature = RowOperator.CreatRowByRow(ifeature_0 as Row) as IFeature;
                            feature.Shape = polylineClass as IGeometry;
                            feature.Store();
                        }
                    }
                    catch (Exception exception)
                    {
                        Logger.Current.Error("", exception, "");
                        flag1 = false;
                        return flag1;
                    }
                }
                polylineClass = new Polyline() as IGeometryCollection;
                for (j = nums[nums.Count - 1]; j < (shapeCopy as IGeometryCollection).GeometryCount; j++)
                {
                    polylineClass.AddGeometry((shapeCopy as IGeometryCollection).Geometry[j], ref value, ref value);
                }
                if ((polylineClass as IPointCollection).PointCount > 1)
                {
                    feature = RowOperator.CreatRowByRow(ifeature_0 as Row) as IFeature;
                    feature.Shape = polylineClass as IGeometry;
                    feature.Store();
                }
            }
            flag1 = true;
            return flag1;
        }

        public static bool StartEditing(IDataset idataset_0)
        {
            bool flag;
            if (idataset_0.Workspace is IWorkspaceEdit)
            {
                IWorkspaceEdit workspace = idataset_0.Workspace as IWorkspaceEdit;
                if (!workspace.IsBeingEdited())
                {
                    try
                    {
                        workspace.StartEditing(true);
                        workspace.EnableUndoRedo();
                        flag = true;
                        return flag;
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                }
            }
            flag = false;
            return flag;
        }

        public static bool StartEditing(IMap pMap, IAppContext context)
        {
            bool editWorkspace;
            object obj;
            object obj1;
            IArray arrayClass = new ESRI.ArcGIS.esriSystem.Array();
            IPropertySet propertySetClass = new PropertySet();
            IFeatureLayer featureLayer = null;
            EditWorkspaceInfo element = null;
            m_pContext = context;
            for (int i = 0; i < pMap.LayerCount; i++)
            {
                ILayer layer = pMap.Layer[i];
                if (layer is IGroupLayer)
                {
                    Editor.CheckGroupLayerEdit(arrayClass, layer as ICompositeLayer);
                }
                else if (layer is IFeatureLayer)
                {
                    featureLayer = layer as IFeatureLayer;
                    IDataset featureClass = featureLayer.FeatureClass as IDataset;
                    if (featureClass != null && !(featureClass is ICoverageFeatureClass) &&
                        Editor.LayerIsHasEditprivilege(layer as IFeatureLayer))
                    {
                        IWorkspace workspace = featureClass.Workspace;
                        if (workspace is IWorkspaceEdit)
                        {
                            element = Editor.FindWorkspce(arrayClass, workspace);
                            if (workspace.Type != esriWorkspaceType.esriRemoteDatabaseWorkspace)
                            {
                                element.LayerArray.Add(featureLayer);
                            }
                            else if (!Editor.IsCheckRegisterVeision)
                            {
                                element.LayerArray.Add(featureLayer);
                            }
                            else if (!(workspace is IVersionedWorkspace))
                            {
                                element.LayerArray.Add(featureLayer);
                            }
                            else if ((featureLayer.FeatureClass as IVersionedObject).IsRegisteredAsVersioned)
                            {
                                element.LayerArray.Add(featureLayer);
                            }
                        }
                    }
                }
            }
            if (arrayClass.Count != 0)
            {
                if (arrayClass.Count != 1)
                {
                    frmSelectEditDatasource _frmSelectEditDatasource = new frmSelectEditDatasource()
                    {
                        Map = pMap,
                        EditWorkspaceInfo = arrayClass
                    };
                    _frmSelectEditDatasource.ShowDialog();
                }
                else
                {
                    element = arrayClass.Element[0] as EditWorkspaceInfo;
                    if (element.LayerArray.Count == 0)
                    {
                        MessageBox.Show("不能编辑任何图层，请检查是否加载了要素图层，加载的要素图层是否是否已经进行了版本注册或是否有更新权限！", "开始编辑",
                            MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        editWorkspace = false;
                        return editWorkspace;
                    }
                    IWorkspaceEdit workspaceEdit = element.Workspace as IWorkspaceEdit;
                    element.Workspace.ConnectionProperties.GetAllProperties(out obj, out obj1);
                    if (!workspaceEdit.IsBeingEdited())
                    {
                        try
                        {
                            if (context.Config.EngineSnapEnvironment != null)
                            {
                                (context.Config.EngineSnapEnvironment as IEngineEditor).StartEditing(
                                    workspaceEdit as IWorkspace, pMap);
                            }
                            else
                            {
                                workspaceEdit.StartEditing(true);
                            }
                            Editor.EditWorkspace = workspaceEdit;
                            Editor.EditMap = pMap;
                        }
                        catch (COMException cOMException1)
                        {
                            COMException cOMException = cOMException1;
                            if (cOMException.ErrorCode == -2147217069)
                            {
                                MessageBox.Show("不能编辑数据，其他应用程序正在使用该数据源!");
                            }
                            else if (cOMException.ErrorCode != -2147217071)
                            {
                                Logger.Current.Error("", cOMException, "");
                            }
                            else
                            {
                                MessageBox.Show(cOMException.Message);
                            }
                        }
                        catch (Exception exception)
                        {
                            Logger.Current.Error("", exception, "");
                        }
                    }
                }
                editWorkspace = Editor.EditWorkspace != null;
            }
            else
            {
                MessageBox.Show("不能编辑任何图层，请检查是否加载了要素图层，加载的要素图层是否是否已经进行了版本注册或是否有更新权限！", "开始编辑", MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);
                editWorkspace = false;
            }
            return editWorkspace;
        }

        private static bool StartEditing1(IMap pMap)
        {
            bool flag;
            if (!UtilityLicenseProviderCheck.Check())
            {
                flag = false;
            }
            else if (pMap.LayerCount < 1)
            {
                flag = false;
            }
            else if (Editor.GetEditWorkspace(pMap) != null)
            {
                int num = 0;
                string str = "";
                for (int i = 0; i < pMap.LayerCount; i++)
                {
                    num = num + Editor.CheckStartEditing(pMap.Layer[i], ref str);
                }
                if (num != 0)
                {
                    flag = true;
                }
                else
                {
                    MessageBox.Show("不能编辑任何图层，请检查是否加载了要素图层，加载的要素图层是否已经进行了版本注册或是否有更新权限！", "开始编辑", MessageBoxButtons.OK,
                        MessageBoxIcon.Asterisk);
                    flag = false;
                }
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        public static bool StartEditOperation(IDataset idataset_0)
        {
            (idataset_0.Workspace as IWorkspaceEdit).StartEditOperation();
            return true;
        }

        public static bool StartEditOperation()
        {
            if (Editor.EditWorkspace != null)
            {
                Editor.EditWorkspace.StartEditOperation();
            }
            return true;
        }

        public static bool StopEditing()
        {
            bool flag;
            if (Editor.EditWorkspace != null)
            {
                bool flag1 = false;
                Editor.EditWorkspace.HasEdits(ref flag1);
                if (!flag1)
                {
                    Editor.EditWorkspace.StopEditing(false);
                    Editor.EditWorkspace = null;
                    Editor.EditMap = null;
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("数据已经被修改过，保存修改吗?", "更改提示", MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question);
                    if (dialogResult != DialogResult.Yes)
                    {
                        if (dialogResult == DialogResult.Cancel)
                        {
                            flag = false;
                            return flag;
                        }
                        Editor.EditWorkspace.StopEditing(false);
                        Editor.EditWorkspace = null;
                        Editor.EditMap = null;
                    }
                    else
                    {
                        Editor.EditWorkspace.StopEditing(true);
                        Editor.EditWorkspace = null;
                        Editor.EditMap = null;
                    }
                }
                EditorEvent.StopEditing();
                flag = true;
            }
            else
            {
                flag = true;
            }
            return flag;
        }

        public static bool StopEditing(IDataset idataset_0, bool bool_0, bool bool_1)
        {
            DialogResult dialogResult;
            bool flag;
            bool flag1;
            bool flag2 = false;
            if (!bool_1)
            {
                dialogResult = (!bool_0 ? DialogResult.No : DialogResult.Yes);
            }
            else
            {
                if (Editor.CheckWorkspaceEdit(idataset_0, "hasEdits"))
                {
                    flag2 = true;
                }
                dialogResult = (flag2
                    ? MessageBox.Show("数据已经被修改过，保存修改吗?", "更改提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                    : DialogResult.No);
                if (dialogResult != DialogResult.Cancel)
                {
                    flag1 = Editor.CheckStopEdits(idataset_0, dialogResult == DialogResult.Yes);
                    flag = true;
                    return flag;
                }
                flag = false;
                return flag;
            }
            flag1 = Editor.CheckStopEdits(idataset_0, dialogResult == DialogResult.Yes);
            flag = true;
            return flag;
        }

        public static bool StopEditing(IMap imap_0, bool bool_0, bool bool_1)
        {
            int num;
            DialogResult yes;
            if (imap_0.LayerCount < 1)
            {
                return false;
            }
            bool flag2 = false;
            if (!bool_1)
            {
                if (bool_0)
                {
                    yes = DialogResult.Yes;
                }
                else
                {
                    yes = DialogResult.No;
                }
            }
            else
            {
                for (num = 0; num < imap_0.LayerCount; num++)
                {
                    if (CheckWorkspaceEdit(imap_0.get_Layer(num), "hasEdits"))
                    {
                        flag2 = true;
                        break;
                    }
                }
                if (!flag2)
                {
                    yes = DialogResult.No;
                }
                else
                {
                    yes = MessageBox.Show("数据已经被修改过，保存修改吗?", "更改提示", MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question);
                }
                if (yes == DialogResult.Cancel)
                {
                    return false;
                }
            }
            for (num = 0; num < imap_0.LayerCount; num++)
            {
                CheckStopEdits(imap_0.get_Layer(num), yes == DialogResult.Yes);
            }
            imap_0.ClearSelection();
            ((IActiveView) imap_0).Refresh();
            return true;
        }


        public static bool StopEditOperation()
        {
            if (Editor.EditWorkspace != null)
            {
                Editor.EditWorkspace.StopEditOperation();
            }
            return true;
        }

        public static bool StopEditOperation(IDataset idataset_0)
        {
            (idataset_0.Workspace as IWorkspaceEdit).StopEditOperation();
            return true;
        }


        public static IPolyline TrimPolyLine(IMap imap_0, IGeometry igeometry_0, IEnvelope ienvelope_0, double double_0)
        {
            if ((igeometry_0 as IGeometryCollection).GeometryCount <= 1)
            {
                IEnumFeature featureSelection = imap_0.FeatureSelection as IEnumFeature;
                if (featureSelection == null)
                {
                    return null;
                }
                IPointCollection points = LineIntersect(igeometry_0 as IPolyline, ienvelope_0);
                if (points.PointCount == 0)
                {
                    return null;
                }
                IPoint point = points.get_Point(0);
                featureSelection.Reset();
                IFeature feature2 = featureSelection.Next();
                ITopologicalOperator @operator = igeometry_0 as ITopologicalOperator;
                IRelationalOperator operator2 = igeometry_0 as IRelationalOperator;
                IPointCollection points2 = new Multipoint();
                object before = Missing.Value;
                while (feature2 != null)
                {
                    IPolyline shape = feature2.Shape as IPolyline;
                    if (operator2.Crosses(shape))
                    {
                        IGeometry geometry = @operator.Intersect(shape, esriGeometryDimension.esriGeometry0Dimension);
                        if (geometry is IPoint)
                        {
                            points2.AddPoint(geometry as IPoint, ref before, ref before);
                        }
                        else if (geometry is IPointCollection)
                        {
                            points2.AddPointCollection(geometry as IPointCollection);
                        }
                    }
                    feature2 = featureSelection.Next();
                }
                if (points2.PointCount > 0)
                {
                    IPoint point2;
                    int num13;
                    IPoint point3;
                    IPoint point4;
                    IPointCollection points3;
                    IPointCollection points4;
                    IGeometryCollection geometrys;
                    IPolycurve2 polycurve = (IPolycurve2) (igeometry_0 as IClone).Clone();
                    polycurve.SplitAtPoints(points2.EnumVertices, true, false, -1.0);
                    double num = 0.0;
                    int num2 = 0;
                    int num3 = 0;
                    bool flag = false;
                    double num4 = 0.0;
                    int num5 = 0;
                    int num6 = 0;
                    bool flag2 = false;
                    int num7 = -1;
                    int num8 = -1;
                    int i = -1;
                    int num10 = -1;
                    int num11 = -1;
                    int num12 = -1;
                    if (
                        !GeometryOperator.TestGeometryHit(double_0, point, polycurve, out point2, ref num, ref num2,
                            ref num3, out flag))
                    {
                        return null;
                    }
                    for (num13 = 0; num13 < points2.PointCount; num13++)
                    {
                        point3 = points2.get_Point(num13);
                        GeometryOperator.TestGeometryHit(double_0, point3, polycurve, out point2, ref num4, ref num5,
                            ref num6, out flag2);
                        if (num5 > num2)
                        {
                            if (num11 == -1)
                            {
                                num11 = num5;
                                num12 = num6;
                                num10 = num13;
                            }
                            else if (num11 > num5)
                            {
                                num11 = num5;
                                num12 = num6;
                                num10 = num13;
                            }
                            else if ((num11 == num5) && (num12 > num6))
                            {
                                num12 = num6;
                                num10 = num13;
                            }
                        }
                        else if (num5 < num2)
                        {
                            if (num7 == -1)
                            {
                                num7 = num5;
                                num8 = num6;
                                i = num13;
                            }
                            else if (num7 < num5)
                            {
                                num7 = num5;
                                num8 = num6;
                                i = num13;
                            }
                            else if ((num7 == num5) && (num8 < num6))
                            {
                                num8 = num6;
                                i = num13;
                            }
                        }
                        else if (num6 > num3)
                        {
                            if (num11 == -1)
                            {
                                num11 = num5;
                                num12 = num6;
                                num10 = num13;
                            }
                            else if (num11 > num5)
                            {
                                num11 = num5;
                                num12 = num6;
                                num10 = num13;
                            }
                            else if ((num11 == num5) && (num12 > num6))
                            {
                                num12 = num6;
                                num10 = num13;
                            }
                        }
                        else if (num6 <= num3)
                        {
                            if (num7 == -1)
                            {
                                num7 = num5;
                                num8 = num6;
                                i = num13;
                            }
                            else if (num7 < num5)
                            {
                                num7 = num5;
                                num8 = num6;
                                i = num13;
                            }
                            else if ((num7 == num5) && (num8 < num6))
                            {
                                num8 = num6;
                                i = num13;
                            }
                        }
                    }
                    if ((num7 == -1) && (num11 == -1))
                    {
                        return null;
                    }
                    if (num7 == -1)
                    {
                        point3 = points2.get_Point(num10);
                        GeometryOperator.TestGeometryHit(double_0/10.0, point3, igeometry_0, out point4, ref num,
                            ref num2, ref num3, out flag);
                        points3 = new ESRI.ArcGIS.Geometry.Path();
                        SetGeometryZM(point4, igeometry_0);
                        SetGeometryZM(points3 as IGeometry, igeometry_0);
                        points3.AddPoint(point4, ref before, ref before);
                        points4 = (igeometry_0 as IGeometryCollection).get_Geometry(num2) as IPointCollection;
                        for (num13 = num3 + 1; num13 < points4.PointCount; num13++)
                        {
                            points3.AddPoint(points4.get_Point(num13), ref before, ref before);
                        }
                        geometrys = new Polyline() as IGeometryCollection;
                        SetGeometryZM(geometrys as IGeometry, igeometry_0);
                        geometrys.AddGeometry(points3 as IGeometry, ref before, ref before);
                        for (num13 = num2 + 1; num13 < (igeometry_0 as IGeometryCollection).GeometryCount; num13++)
                        {
                            geometrys.AddGeometry((igeometry_0 as IGeometryCollection).get_Geometry(num13), ref before,
                                ref before);
                        }
                        return (geometrys as IPolyline);
                    }
                    if (num11 == -1)
                    {
                        point3 = points2.get_Point(i);
                        GeometryOperator.TestGeometryHit(double_0/10.0, point3, igeometry_0, out point4, ref num,
                            ref num2, ref num3, out flag);
                        SetGeometryZM(point4, igeometry_0);
                        geometrys = new Polyline() as IGeometryCollection;
                        SetGeometryZM(geometrys as IGeometry, igeometry_0);
                        for (num13 = 0; num13 < num2; num13++)
                        {
                            geometrys.AddGeometry((igeometry_0 as IGeometryCollection).get_Geometry(num13), ref before,
                                ref before);
                        }
                        points3 = new ESRI.ArcGIS.Geometry.Path();
                        SetGeometryZM(points3 as IGeometry, igeometry_0);
                        points4 = (igeometry_0 as IGeometryCollection).get_Geometry(num2) as IPointCollection;
                        for (num13 = 0; num13 < (num3 + 1); num13++)
                        {
                            points3.AddPoint(points4.get_Point(num13), ref before, ref before);
                        }
                        if (!flag)
                        {
                            points3.AddPoint(point4, ref before, ref before);
                        }
                        geometrys.AddGeometry(points3 as IGeometry, ref before, ref before);
                        return (geometrys as IPolyline);
                    }
                    if ((igeometry_0 as IGeometryCollection).GeometryCount == 1)
                    {
                        int num14 = (polycurve as IPointCollection).PointCount - num12;
                        if (num14 > num8)
                        {
                            point3 = points2.get_Point(num10);
                            GeometryOperator.TestGeometryHit(double_0/10.0, point3, igeometry_0, out point4, ref num,
                                ref num2, ref num3, out flag);
                            SetGeometryZM(point4, igeometry_0);
                            points3 = new ESRI.ArcGIS.Geometry.Path();
                            SetGeometryZM(points3 as IGeometry, igeometry_0);
                            points3.AddPoint(point4, ref before, ref before);
                            points4 = (igeometry_0 as IGeometryCollection).get_Geometry(num2) as IPointCollection;
                            for (num13 = num3 + 1; num13 < points4.PointCount; num13++)
                            {
                                points3.AddPoint(points4.get_Point(num13), ref before, ref before);
                            }
                            geometrys = new Polyline() as IGeometryCollection;
                            SetGeometryZM(geometrys as IGeometry, igeometry_0);
                            geometrys.AddGeometry(points3 as IGeometry, ref before, ref before);
                            for (num13 = num2 + 1; num13 < (polycurve as IGeometryCollection).GeometryCount; num13++)
                            {
                                geometrys.AddGeometry((igeometry_0 as IGeometryCollection).get_Geometry(num13),
                                    ref before, ref before);
                            }
                            return (geometrys as IPolyline);
                        }
                        point3 = points2.get_Point(i);
                        GeometryOperator.TestGeometryHit(double_0/10.0, point3, igeometry_0, out point4, ref num,
                            ref num2, ref num3, out flag);
                        SetGeometryZM(point4, igeometry_0);
                        geometrys = new Polyline() as IGeometryCollection;
                        SetGeometryZM(geometrys as IGeometry, igeometry_0);
                        for (num13 = 0; num13 < num2; num13++)
                        {
                            geometrys.AddGeometry((igeometry_0 as IGeometryCollection).get_Geometry(num13), ref before,
                                ref before);
                        }
                        points3 = new ESRI.ArcGIS.Geometry.Path();
                        SetGeometryZM(points3 as IGeometry, igeometry_0);
                        points4 = (igeometry_0 as IGeometryCollection).get_Geometry(num2) as IPointCollection;
                        for (num13 = 0; num13 < (num3 + 1); num13++)
                        {
                            points3.AddPoint(points4.get_Point(num13), ref before, ref before);
                        }
                        if (!flag)
                        {
                            points3.AddPoint(point4, ref before, ref before);
                        }
                        geometrys.AddGeometry(points3 as IGeometry, ref before, ref before);
                        return (geometrys as IPolyline);
                    }
                    if ((igeometry_0 as IGeometryCollection).GeometryCount <= 1)
                    {
                    }
                }
            }
            return null;
        }

        public static IPolyline TrimPolyLine(IMap imap_0, IGeometry igeometry_0, IPoint ipoint_0, double double_0)
        {
            if ((igeometry_0 as IGeometryCollection).GeometryCount <= 1)
            {
                IEnumFeature featureSelection = imap_0.FeatureSelection as IEnumFeature;
                if (featureSelection == null)
                {
                    return null;
                }
                featureSelection.Reset();
                IFeature feature2 = featureSelection.Next();
                ITopologicalOperator @operator = igeometry_0 as ITopologicalOperator;
                IRelationalOperator operator2 = igeometry_0 as IRelationalOperator;
                IPointCollection points = new Multipoint();
                object before = Missing.Value;
                while (feature2 != null)
                {
                    IPolyline shape = feature2.Shape as IPolyline;
                    if (operator2.Crosses(shape))
                    {
                        IGeometry geometry = @operator.Intersect(shape, esriGeometryDimension.esriGeometry0Dimension);
                        if (geometry is IPoint)
                        {
                            points.AddPoint(geometry as IPoint, ref before, ref before);
                        }
                        else if (geometry is IPointCollection)
                        {
                            points.AddPointCollection(geometry as IPointCollection);
                        }
                    }
                    feature2 = featureSelection.Next();
                }
                if (points.PointCount > 0)
                {
                    IPoint point;
                    int num13;
                    IPoint point2;
                    IPoint point3;
                    IPointCollection points2;
                    IPointCollection points3;
                    IGeometryCollection geometrys;
                    IPolycurve2 polycurve = (IPolycurve2) (igeometry_0 as IClone).Clone();
                    polycurve.SplitAtPoints(points.EnumVertices, true, false, -1.0);
                    double num = 0.0;
                    int num2 = 0;
                    int num3 = 0;
                    bool flag = false;
                    double num4 = 0.0;
                    int num5 = 0;
                    int num6 = 0;
                    bool flag2 = false;
                    int num7 = -1;
                    int num8 = -1;
                    int i = -1;
                    int num10 = -1;
                    int num11 = -1;
                    int num12 = -1;
                    if (
                        !GeometryOperator.TestGeometryHit(double_0, ipoint_0, polycurve, out point, ref num, ref num2,
                            ref num3, out flag))
                    {
                        return null;
                    }
                    for (num13 = 0; num13 < points.PointCount; num13++)
                    {
                        point2 = points.get_Point(num13);
                        GeometryOperator.TestGeometryHit(double_0, point2, polycurve, out point, ref num4, ref num5,
                            ref num6, out flag2);
                        if (num5 > num2)
                        {
                            if (num11 == -1)
                            {
                                num11 = num5;
                                num12 = num6;
                                num10 = num13;
                            }
                            else if (num11 > num5)
                            {
                                num11 = num5;
                                num12 = num6;
                                num10 = num13;
                            }
                            else if ((num11 == num5) && (num12 > num6))
                            {
                                num12 = num6;
                                num10 = num13;
                            }
                        }
                        else if (num5 < num2)
                        {
                            if (num7 == -1)
                            {
                                num7 = num5;
                                num8 = num6;
                                i = num13;
                            }
                            else if (num7 < num5)
                            {
                                num7 = num5;
                                num8 = num6;
                                i = num13;
                            }
                            else if ((num7 == num5) && (num8 < num6))
                            {
                                num8 = num6;
                                i = num13;
                            }
                        }
                        else if (num6 > num3)
                        {
                            if (num11 == -1)
                            {
                                num11 = num5;
                                num12 = num6;
                                num10 = num13;
                            }
                            else if (num11 > num5)
                            {
                                num11 = num5;
                                num12 = num6;
                                num10 = num13;
                            }
                            else if ((num11 == num5) && (num12 > num6))
                            {
                                num12 = num6;
                                num10 = num13;
                            }
                        }
                        else if (num6 <= num3)
                        {
                            if (num7 == -1)
                            {
                                num7 = num5;
                                num8 = num6;
                                i = num13;
                            }
                            else if (num7 < num5)
                            {
                                num7 = num5;
                                num8 = num6;
                                i = num13;
                            }
                            else if ((num7 == num5) && (num8 < num6))
                            {
                                num8 = num6;
                                i = num13;
                            }
                        }
                    }
                    if ((num7 == -1) && (num11 == -1))
                    {
                        return null;
                    }
                    if (num7 == -1)
                    {
                        point2 = points.get_Point(num10);
                        GeometryOperator.TestGeometryHit(double_0/10.0, point2, igeometry_0, out point3, ref num,
                            ref num2, ref num3, out flag);
                        points2 = new ESRI.ArcGIS.Geometry.Path();
                        SetGeometryZM(point3, igeometry_0);
                        SetGeometryZM(points2 as IGeometry, igeometry_0);
                        points2.AddPoint(point3, ref before, ref before);
                        points3 = (igeometry_0 as IGeometryCollection).get_Geometry(num2) as IPointCollection;
                        for (num13 = num3 + 1; num13 < points3.PointCount; num13++)
                        {
                            points2.AddPoint(points3.get_Point(num13), ref before, ref before);
                        }
                        geometrys = new Polyline() as IGeometryCollection;
                        SetGeometryZM(geometrys as IGeometry, igeometry_0);
                        geometrys.AddGeometry(points2 as IGeometry, ref before, ref before);
                        for (num13 = num2 + 1; num13 < (igeometry_0 as IGeometryCollection).GeometryCount; num13++)
                        {
                            geometrys.AddGeometry((igeometry_0 as IGeometryCollection).get_Geometry(num13), ref before,
                                ref before);
                        }
                        return (geometrys as IPolyline);
                    }
                    if (num11 == -1)
                    {
                        point2 = points.get_Point(i);
                        GeometryOperator.TestGeometryHit(double_0/10.0, point2, igeometry_0, out point3, ref num,
                            ref num2, ref num3, out flag);
                        SetGeometryZM(point3, igeometry_0);
                        geometrys = new Polyline() as IGeometryCollection;
                        SetGeometryZM(geometrys as IGeometry, igeometry_0);
                        for (num13 = 0; num13 < num2; num13++)
                        {
                            geometrys.AddGeometry((igeometry_0 as IGeometryCollection).get_Geometry(num13), ref before,
                                ref before);
                        }
                        points2 = new ESRI.ArcGIS.Geometry.Path();
                        SetGeometryZM(points2 as IGeometry, igeometry_0);
                        points3 = (igeometry_0 as IGeometryCollection).get_Geometry(num2) as IPointCollection;
                        for (num13 = 0; num13 < (num3 + 1); num13++)
                        {
                            points2.AddPoint(points3.get_Point(num13), ref before, ref before);
                        }
                        if (!flag)
                        {
                            points2.AddPoint(point3, ref before, ref before);
                        }
                        geometrys.AddGeometry(points2 as IGeometry, ref before, ref before);
                        return (geometrys as IPolyline);
                    }
                    if ((igeometry_0 as IGeometryCollection).GeometryCount == 1)
                    {
                        int num14 = (polycurve as IPointCollection).PointCount - num12;
                        if (num14 > num8)
                        {
                            point2 = points.get_Point(num10);
                            GeometryOperator.TestGeometryHit(double_0/10.0, point2, igeometry_0, out point3, ref num,
                                ref num2, ref num3, out flag);
                            SetGeometryZM(point3, igeometry_0);
                            points2 = new ESRI.ArcGIS.Geometry.Path();
                            SetGeometryZM(points2 as IGeometry, igeometry_0);
                            points2.AddPoint(point3, ref before, ref before);
                            points3 = (igeometry_0 as IGeometryCollection).get_Geometry(num2) as IPointCollection;
                            for (num13 = num3 + 1; num13 < points3.PointCount; num13++)
                            {
                                points2.AddPoint(points3.get_Point(num13), ref before, ref before);
                            }
                            geometrys = new Polyline() as IGeometryCollection;
                            SetGeometryZM(geometrys as IGeometry, igeometry_0);
                            geometrys.AddGeometry(points2 as IGeometry, ref before, ref before);
                            for (num13 = num2 + 1; num13 < (polycurve as IGeometryCollection).GeometryCount; num13++)
                            {
                                geometrys.AddGeometry((igeometry_0 as IGeometryCollection).get_Geometry(num13),
                                    ref before, ref before);
                            }
                            return (geometrys as IPolyline);
                        }
                        point2 = points.get_Point(i);
                        GeometryOperator.TestGeometryHit(double_0/10.0, point2, igeometry_0, out point3, ref num,
                            ref num2, ref num3, out flag);
                        SetGeometryZM(point3, igeometry_0);
                        geometrys = new Polyline() as IGeometryCollection;
                        SetGeometryZM(geometrys as IGeometry, igeometry_0);
                        for (num13 = 0; num13 < num2; num13++)
                        {
                            geometrys.AddGeometry((igeometry_0 as IGeometryCollection).get_Geometry(num13), ref before,
                                ref before);
                        }
                        points2 = new ESRI.ArcGIS.Geometry.Path();
                        SetGeometryZM(points2 as IGeometry, igeometry_0);
                        points3 = (igeometry_0 as IGeometryCollection).get_Geometry(num2) as IPointCollection;
                        for (num13 = 0; num13 < (num3 + 1); num13++)
                        {
                            points2.AddPoint(points3.get_Point(num13), ref before, ref before);
                        }
                        if (!flag)
                        {
                            points2.AddPoint(point3, ref before, ref before);
                        }
                        geometrys.AddGeometry(points2 as IGeometry, ref before, ref before);
                        return (geometrys as IPolyline);
                    }
                    if ((igeometry_0 as IGeometryCollection).GeometryCount > 1)
                    {
                    }
                }
                else
                {
                    MessageBox.Show("没有击中要裁剪的线!");
                }
            }
            return null;
        }


        public static void UndoRedoEdit(IMap imap_0, bool bool_0)
        {
            if (imap_0.LayerCount >= 1)
            {
                int num = 0;
                while (num < imap_0.LayerCount && !Editor.CheckUndoReDo(imap_0.Layer[num], bool_0))
                {
                    num++;
                }
                ((IActiveView) imap_0).Refresh();
            }
        }

        public static bool UpdateFeature(IFeature ifeature_0, IGeometry igeometry_0, ref bool bool_0)
        {
            bool flag;
            IWorkspaceEdit workspace = null;
            bool flag1 = false;
            IDataset @class = (IDataset) ifeature_0.Class;
            if ((@class.Type == esriDatasetType.esriDTFeatureClass
                ? false
                : @class.Type != esriDatasetType.esriDTFeatureDataset))
            {
                flag1 = false;
            }
            else
            {
                workspace = (IWorkspaceEdit) @class.Workspace;
                if (!workspace.IsBeingEdited())
                {
                    flag1 = true;
                }
            }
            if (!flag1)
            {
                workspace.StartEditOperation();
                try
                {
                    if (!(ifeature_0 is IAnnotationFeature2))
                    {
                        if (ifeature_0.Fields.Field[ifeature_0.Fields.FindField("Shape")].GeometryDef.HasZ)
                        {
                            if (!(igeometry_0 as IZAware).ZAware)
                            {
                                (igeometry_0 as IZAware).ZAware = true;
                                if (igeometry_0 is IZ)
                                {
                                    (igeometry_0 as IZ).SetConstantZ(0);
                                }
                            }
                            if (igeometry_0 is IZ)
                            {
                                (igeometry_0 as IZ).CalculateNonSimpleZs();
                            }
                        }
                        ifeature_0.Shape = igeometry_0;
                    }
                    ifeature_0.Store();
                }
                catch (COMException cOMException1)
                {
                    COMException cOMException = cOMException1;
                    if (cOMException.ErrorCode != -2147220936)
                    {
                        Logger.Current.Error("", cOMException, "");
                    }
                    else
                    {
                        MessageBox.Show("坐标值或量测值超出范围!", "编辑要素", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                    MessageBox.Show(exception.Message);
                    Logger.Current.Error("", exception, "");
                }
                workspace.StopEditOperation();
                EditorEvent.FeatureGeometryChanged(ifeature_0);
                if (!bool_0)
                {
                    bool_0 = true;
                }
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        public static bool UpdateFeature(IMap imap_0, IFeature ifeature_0, ITransform2D itransform2D_0, ref bool bool_0)
        {
            bool flag;
            IWorkspaceEdit workspace = null;
            bool flag1 = false;
            IDataset @class = (IDataset) ifeature_0.Class;
            if ((@class.Type == esriDatasetType.esriDTFeatureClass
                ? false
                : @class.Type != esriDatasetType.esriDTFeatureDataset))
            {
                flag1 = false;
            }
            else
            {
                workspace = (IWorkspaceEdit) @class.Workspace;
                if (!workspace.IsBeingEdited())
                {
                    flag1 = true;
                }
            }
            if (!flag1)
            {
                workspace.StartEditOperation();
                try
                {
                    if (!(ifeature_0 is IAnnotationFeature2))
                    {
                        ifeature_0.Shape = (IGeometry) itransform2D_0;
                    }
                    else
                    {
                        int num = 0;
                        while (num < imap_0.LayerCount)
                        {
                            IFeatureLayer layer = imap_0.Layer[num] as IFeatureLayer;
                            if (layer == null || ifeature_0.Class.ObjectClassID != layer.FeatureClass.ObjectClassID)
                            {
                                num++;
                            }
                            else
                            {
                                ((IGraphicsContainer) layer).UpdateElement((IElement) itransform2D_0);
                                goto Label0;
                            }
                        }
                    }
                    Label0:
                    ifeature_0.Store();
                    if (!bool_0)
                    {
                        bool_0 = true;
                    }
                }
                catch (COMException cOMException1)
                {
                    COMException cOMException = cOMException1;
                    if (cOMException.ErrorCode != -2147220936)
                    {
                        Logger.Current.Error("", cOMException, "");
                    }
                    else
                    {
                        MessageBox.Show("坐标值或量测值超出范围!", "编辑要素", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    (imap_0 as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewForeground, null, null);
                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                    MessageBox.Show(exception.Message);
                    Logger.Current.Error("", exception, "");
                    (imap_0 as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewForeground, null, null);
                }
                workspace.StopEditOperation();
                EditorEvent.FeatureGeometryChanged(ifeature_0);
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        private static void WritePointCollection(StreamWriter streamWriter_0, IPointCollection pPointCollection)
        {
            for (int i = 0; i < pPointCollection.PointCount; i++)
            {
                streamWriter_0.WriteLine(Editor.WritePointToString(pPointCollection.Point[i]));
            }
        }

        private static string WritePointToString(IPoint pPoint)
        {
            string str = "";
            str = pPoint.X.ToString("0.#####");
            double y = pPoint.Y;
            str = string.Concat(str, ",", y.ToString("0.#####"));
            return str;
        }

        public enum WorkspaceEditStatus
        {
            IsBeginEdited,
            HasEdits,
            HasUndos
        }
    }
}