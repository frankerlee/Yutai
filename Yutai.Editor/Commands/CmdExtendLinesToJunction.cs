using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using ICommandSubType = Yutai.Plugins.Interfaces.ICommandSubType;


namespace Yutai.Plugins.Editor.Commands
{
    public class CmdExtendLinesToJunction : YutaiTool
    {
        private double double_0 = 16;

        private IFeature ifeature_0 = null;

        private int int_0 = -1;

        private int int_1 = -1;

        private IPoint ipoint_0 = null;

        public override bool Enabled
        {
            get
            {
                bool flag;
                if (_context.FocusMap == null)
                {
                    flag = false;
                }
                else if (_context.FocusMap.LayerCount == 0)
                {
                    flag = false;
                }
                else if ((Yutai.ArcGIS.Common.Editor.Editor.EditMap == null ? true : Yutai.ArcGIS.Common.Editor.Editor.EditMap == _context.FocusMap))
                {
                    flag = (Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace == null ? false : true);
                }
                else
                {
                    flag = false;
                }
                return flag;
            }
        }

        public CmdExtendLinesToJunction(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            this.m_caption = "延长两线相交";
            this.m_category = "编辑器";
            this.m_name = "Edit_ConstructJunction";
            this._key = "Edit_ConstructJunction";
            this.m_message = "延长两线相交";
            this.m_toolTip = "延长两线相交";
            this.m_bitmap = Properties.Resources.icon_edit_extendjunction;
            this.m_cursor = new System.Windows.Forms.Cursor(GetType().Assembly.GetManifestResourceStream("Yutai.Plugins.Editor.Resources.Cursor.ExtendLine.cur"));
            base._itemType = RibbonItemType.Tool;
        }

        public override void OnClick()
        {
            base.OnClick();
            _context.SetCurrentTool(this);
        }

       

        public override void OnMouseDown(int int_2, int int_3, int int_4, int int_5)
        {
            if (int_2 == 1)
            {
                IPoint mapPoint = _context.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(int_4, int_5);
                double mapUnits = Common.ConvertPixelsToMapUnits((IActiveView)_context.FocusMap, _context.Config.EngineSnapEnvironment.SnapTolerance);
                IFeature hitLineFeature = Yutai.ArcGIS.Common.Editor.Editor.GetHitLineFeature(_context.FocusMap, mapPoint, mapUnits);
                if (hitLineFeature != null)
                {
                    double num = 0;
                    bool flag = false;
                    int num1 = -1;
                    int num2 = -1;
                    IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
                    IHitTest shapeCopy = hitLineFeature.ShapeCopy as IHitTest;
                    shapeCopy.HitTest(mapPoint, mapUnits, esriGeometryHitPartType.esriGeometryPartBoundary, pointClass, ref num, ref num1, ref num2, ref flag);
                    if (this.ifeature_0 != null)
                    {
                        IWorkspaceEdit workspace = (hitLineFeature.Class as IDataset).Workspace as IWorkspaceEdit;
                        workspace.StartEditOperation();
                        bool flag1 = Yutai.ArcGIS.Common.Editor.Editor.ConstructJunction(_context.FocusMap, this.ifeature_0, this.ipoint_0, this.int_0, this.int_1, hitLineFeature, pointClass, num1, num2);
                        workspace.StopEditOperation();
                        if (!flag1)
                        {
                            MessageBox.Show("两条线无法计算交点，线段可能不相交或交点在该图层坐标范围以外！");
                        }
                        else
                        {
                            _context.ActiveView.Refresh();
                        }
                        this.ifeature_0 = null;
                    }
                    else
                    {
                        this.ifeature_0 = hitLineFeature;
                        this.int_0 = num1;
                        this.int_1 = num2;
                        this.ipoint_0 = pointClass;
                    }
                }
            }
        }
    }
}