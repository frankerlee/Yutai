using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    public class CmdFeatureReShapeTool : YutaiTool, ITask
    {
        private ISnappingEnvironment pSnapEnvironment = new Snapping();

        private ControlsEditingSketchTool pSketchTool = new ControlsEditingSketchTool();

        private IFeature pFeature;

        public override bool Enabled
        {
            get
            {
                bool result;
                if (_context.FocusMap == null)
                {
                    result = false;
                }
                else if (_context.FocusMap.SelectionCount != 1)
                {
                    result = false;
                }
                else
                {
                    this.pFeature = null;
                    IEnumFeature enumFeature = _context.FocusMap.FeatureSelection as IEnumFeature;
                    enumFeature.Reset();
                    IFeature feature = enumFeature.Next();
                    if (feature != null)
                    {
                        esriGeometryType geometryType = feature.Shape.GeometryType;
                        if (geometryType == esriGeometryType.esriGeometryPolygon ||
                            geometryType == esriGeometryType.esriGeometryPolyline)
                        {
                            this.pFeature = feature;
                            result = true;
                            return result;
                        }
                    }
                    result = false;
                }
                return result;
            }
        }

        public override bool Checked
        {
            get { return this.Enabled && SketchToolAssist.CurrentTask == this; }
        }

        public string DefaultTool
        {
            get { return "Editor_Sketch_Line"; }
            set { }
        }

        public CmdFeatureReShapeTool(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            this.m_caption = "要素整形工具";
            this.m_name = "Edit_FeatureReShapeTool";
            this._key = "Edit_FeatureReShapeTool";
            this._itemType = RibbonItemType.Tool;
            this.m_toolTip = "要素整形工具";
            this.m_message = "要素整形工具";
            this.m_category = "编辑";
            this.m_bitmap = Properties.Resources.icon_edit_reshape;
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageBeforeText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
        }

        public override void OnClick()
        {
            SketchToolAssist.IsDrawTempLine = DrawTempGeometry.Line;
            SketchToolAssist.CurrentTask = this;
            base.OnClick();
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public void Excute()
        {
            IGeometry shapeCopy = this.pFeature.ShapeCopy;
            IGeometryServer2 geometryServer = new GeometryServerImpl() as IGeometryServer2;
            IInvalidArea invalidArea = new InvalidArea();
            invalidArea.Display = (_context.FocusMap as IActiveView).ScreenDisplay;
            invalidArea.Add(SketchToolAssist.TempLine);
            IGeometry geometry = geometryServer.Reshape(shapeCopy.SpatialReference, shapeCopy,
                SketchToolAssist.TempLine as IPolyline);
            if (geometry != null && !geometry.IsEmpty)
            {
                Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace.StartEditOperation();
                this.pFeature.Shape = geometry;
                this.pFeature.Store();
                Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace.StopEditOperation();
                invalidArea.Add(this.pFeature);
            }
            invalidArea.Invalidate(-2);
        }

        public bool CheckTaskStatue(ITool pTool)
        {
            string name = (pTool as ICommand).Name;
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
    }
}