using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.ArcGIS.Common.Geodatabase;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    public class CmdPolygonCutByPolygon : YutaiCommand,ITask
    {
        private IDisplayFeedback idisplayFeedback_0 = null;

        public override bool Checked
        {
            get
            {
                bool flag;
                flag = (!this.Enabled ? false : SketchToolAssist.CurrentTask == this);
                return flag;
            }
        }

        public string DefaultTool
        {
            get
            {
                return "Editor_Sketch_Line";
            }
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
                else if (_context.FocusMap.LayerCount == 0)
                {
                    result = false;
                }
                else if ( Yutai.ArcGIS.Common.Editor.Editor.EditMap != null && Yutai.ArcGIS.Common.Editor.Editor.EditMap != _context.FocusMap)
                {
                    result = false;
                }
                else
                {
                    if (Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace != null && _context.FocusMap.SelectionCount == 1)
                    {
                        IEnumFeature enumFeature = _context.FocusMap.FeatureSelection as IEnumFeature;
                        enumFeature.Reset();
                        IFeature feature = enumFeature.Next();
                        if (feature != null && feature.Shape != null && feature.Shape.GeometryType == esriGeometryType.esriGeometryPolygon)
                        {
                            result = Yutai.ArcGIS.Common.Editor.Editor.CheckLayerCanEdit(feature.Class as IFeatureClass);
                            return result;
                        }
                    }
                    result = false;
                }
                return result;
            }
        }
        public override void OnCreate(object hook)
        {
            this.m_caption = "打断多边形";
            this.m_toolTip = "多边形打断多边形";
            this.m_name = "Edit_PolygonCutByPolygon";
            this.m_category = "编辑器";



            this._key = "Edit_PolygonCutByPolygon";
            this.m_message = "多边形打断多边形";
            this.m_bitmap = Properties.Resources.icon_edit_cutpolygon;
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.Button;
        }

        public CmdPolygonCutByPolygon(IAppContext context)
        {
            OnCreate(context);
        }

        public bool CheckTaskStatue(ITool itool_0)
        {
            string name = (itool_0 as ICommand).Name;
            bool flag = false;
            if (string.Compare(name, "Editor_Sketch_Line", true) == 0)
            {
                flag = true;
            }
            if (!flag && string.Compare(name, "Editor_Sketch_ArcLine", true) == 0)
            {
                flag = true;
            }
            if (!flag && string.Compare(name, "Editor_Start_Sketch", true) == 0)
            {
                flag = true;
            }
            if (!flag)
            {
                SketchToolAssist.ReSet();
            }
            return flag;
        }

        public void Excute()
        {
            IPolygon tempLine = SketchToolAssist.TempLine as IPolygon;
            IInvalidArea invalidAreaClass = new InvalidArea()
            {
                Display = (_context.FocusMap as IActiveView).ScreenDisplay
            };
            invalidAreaClass.Add(tempLine);
            tempLine.SimplifyPreserveFromTo();
            IEnumFeature featureSelection = _context.FocusMap.FeatureSelection as IEnumFeature;
            featureSelection.Reset();
            IFeature feature = featureSelection.Next();
            if (feature != null)
            {
                IPolygon shapeCopy = feature.ShapeCopy as IPolygon;
                if (!(shapeCopy as IRelationalOperator).Disjoint(tempLine))
                {
                    IGeometry geometry = (shapeCopy as ITopologicalOperator).Difference(tempLine);
                    if (!geometry.IsEmpty)
                    {
                        invalidAreaClass.Add(feature);
                        Yutai.ArcGIS.Common.Editor.Editor.SetGeometryZM(geometry, shapeCopy);
                        Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace.StartEditOperation();
                        try
                        {
                            feature.Shape = geometry;
                            feature.Store();
                            IGeometry geometry1 = (shapeCopy as ITopologicalOperator).Difference(geometry);
                            if (!geometry1.IsEmpty)
                            {
                                Yutai.ArcGIS.Common.Editor.Editor.SetGeometryZM(geometry1, shapeCopy);
                                IFeature feature1 = RowOperator.CreatRowByRow(feature as Row) as IFeature;
                                feature1.Shape = geometry1;
                                feature1.Store();
                            }
                        }
                        catch (Exception exception)
                        {
                            CErrorLog.writeErrorLog(this, exception, "");
                        }
                        Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace.StopEditOperation();
                        _context.UpdateUI();
                    }
                }
            }
            invalidAreaClass.Invalidate(-2);
        }

      
        public override void OnClick()
        {
            SketchToolAssist.IsDrawTempLine = DrawTempGeometry.Fill;
            SketchToolAssist.CurrentTask = this;
            _context.SetCurrentTool("Editor_Sketch_Line");
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }
    }
}