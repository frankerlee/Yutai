using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.ArcGIS.Common.Geodatabase;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Services;

namespace Yutai.Plugins.Editor.Commands
{
    public class CmdPolygonCutHollow : YutaiCommand, ITask
    {
        private IDisplayFeedback idisplayFeedback_0 = null;

        private int int_0 = 0;

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
                else if ( Yutai.ArcGIS.Common.Editor.Editor.EditMap != null && Yutai.ArcGIS.Common.Editor.Editor.EditMap != _context.FocusMap)
                {
                    result = false;
                }
                else if (_context.FocusMap.LayerCount == 0)
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

        public CmdPolygonCutHollow(IAppContext context)
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
            tempLine.SimplifyPreserveFromTo();
            IEnumFeature featureSelection = _context.FocusMap.FeatureSelection as IEnumFeature;
            featureSelection.Reset();
            IFeature feature = featureSelection.Next();
            IPolygon shapeCopy = feature.ShapeCopy as IPolygon;
            if (!(shapeCopy as IRelationalOperator).Contains(tempLine))
            {
                MessageService.Current.Warn("挖空多边形必须包含原多边形!");
            }
            else
            {
                IGeometry geometry = (shapeCopy as ITopologicalOperator).Difference(tempLine);
                if (!geometry.IsEmpty)
                {
                    Yutai.ArcGIS.Common.Editor.Editor.SetGeometryZM(geometry, shapeCopy);
                    Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace.StartEditOperation();
                    try
                    {
                        feature.Shape = geometry;
                        feature.Store();
                        if (MessageService.Current.Ask("保留挖空面吗?"))
                        {
                            if (this.int_0 == 1)
                            {
                                IFeature feature1 = RowOperator.CreatRowByRow(feature) as IFeature;
                                if (feature1 != null)
                                {
                                    Yutai.ArcGIS.Common.Editor.Editor.SetGeometryZM(tempLine, shapeCopy);
                                    feature1.Shape = tempLine;
                                    feature1.Store();
                                }
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        //CErrorLog.writeErrorLog(this, exception, "");
                    }
                    Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace.StopEditOperation();
                    (_context.FocusMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, null, shapeCopy.Envelope);
                    (_context.FocusMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                    _context.UpdateUI();
                }
            }
        }

      

        public override void OnCreate(object hook)
        {
            this.m_caption = "挖空";
            this.m_toolTip = "挖空";
            this.m_name = "Edit_PolygonCutHollow0";
            this.m_category = "编辑器";

            this._key = "Edit_PolygonCutHollow0";
            this.m_message = "挖空";
            this.m_bitmap = Properties.Resources.icon_edit_cuthole;
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.Button;
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

        public void SetSubType(int int_1)
        {
            this.int_0 = int_1;
            if (this.int_0 == 0)
            {
                this.m_caption = "挖空";
                this.m_toolTip = "挖空";
                this.m_name = "PolygonCutHollow0";
            }
            else if (this.int_0 == 1)
            {
                this.m_caption = "挖空，并保留挖空面";
                this.m_toolTip = "挖空，并创建一个新面";
                this.m_name = "PolygonCutHollow1";
            }
        }
    }
}