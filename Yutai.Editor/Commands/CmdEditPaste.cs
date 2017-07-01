using System;
using System.Reflection;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    class CmdEditPaste : YutaiCommand
    {
        private bool bool_0 = false;

        public CmdEditPaste(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_edit_paste;
            this.m_caption = "粘贴";
            this.m_category = "Edit";
            this.m_message = "粘贴";
            this.m_name = "Edit_Paste";
            this._key = "Edit_Paste";
            this.m_toolTip = "粘贴";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.Button;
        }

        public override bool Enabled
        {
            get
            {
                bool result;
                if (_context.FocusMap == null)
                {
                    result = false;
                }
                else
                {
                    this.bool_0 = false;
                    if (_context.FocusMap.LayerCount > 0 && _context.FocusMap.SelectionCount > 0)
                    {
                        this.bool_0 = true;
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                return result;
            }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            int i;
            IFeature feature;
            if (Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate == null ||
                Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer == null) return;
            IFeatureLayer featureLayer = Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer;
            esriGeometryType shapeType = featureLayer.FeatureClass.ShapeType;
            CmdEditCopy.m_pEnumFeature.Reset();
            IFeature item = CmdEditCopy.m_pEnumFeature.Next();
            IFeatureClass featureClass = featureLayer.FeatureClass;
            IWorkspaceEdit workspace = (featureClass as IDataset).Workspace as IWorkspaceEdit;
            workspace.StartEditOperation();
            IArray arrayClass = new ESRI.ArcGIS.esriSystem.Array();
            object value = Missing.Value;
            for (i = 0; i < CmdEditCopy.m_pFeatureList.Count; i++)
            {
                item = CmdEditCopy.m_pFeatureList[i];
                if (item.Shape.GeometryType == shapeType)
                {
                    feature = featureClass.CreateFeature();
                    try
                    {
                        Yutai.ArcGIS.Common.Editor.Editor.CopyRowEx(item, feature);
                        feature.Store();
                        arrayClass.Add(feature);
                    }
                    catch (Exception exception)
                    {
                        CErrorLog.writeErrorLog(this, exception, "");
                    }
                }
                else if ((item.Shape.GeometryType != esriGeometryType.esriGeometryPolygon
                    ? false
                    : shapeType == esriGeometryType.esriGeometryPolyline))
                {
                    feature = featureClass.CreateFeature();
                    try
                    {
                        IPolyline polylineClass = new Polyline() as IPolyline;
                        IPolygon shape = item.Shape as IPolygon;
                        for (int j = 0; j < (shape as IGeometryCollection).GeometryCount; j++)
                        {
                            ISegmentCollection geometry =
                                (shape as IGeometryCollection).Geometry[j] as ISegmentCollection;
                            IPath pathClass = new Path() as IPath;
                            (pathClass as ISegmentCollection).AddSegmentCollection(geometry);
                            (polylineClass as IGeometryCollection).AddGeometry(pathClass, ref value, ref value);
                        }
                        feature.Shape = polylineClass;
                        Yutai.ArcGIS.Common.Editor.Editor.CopyRow(item, feature);
                        feature.Store();
                        arrayClass.Add(feature);
                    }
                    catch (Exception exception1)
                    {
                        CErrorLog.writeErrorLog(this, exception1, "");
                    }
                }
                item = CmdEditCopy.m_pEnumFeature.Next();
            }
            workspace.StopEditOperation();
            _context.FocusMap.ClearSelection();
            for (i = 0; i < arrayClass.Count; i++)
            {
                _context.FocusMap.SelectFeature(Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer,
                    arrayClass.Element[i] as IFeature);
            }
            _context.ActiveView.Refresh();
        }
    }
}