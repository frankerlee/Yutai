using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Framework;
using Yutai.ArcGIS.Common.Geodatabase;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Cursor = System.Windows.Forms.Cursor;
using IToolContextMenu = Yutai.Plugins.Interfaces.IToolContextMenu;


namespace Yutai.Plugins.Editor.Commands
{
    public class CmdCopyFeaturesTool : YutaiTool, IToolContextMenu
    {
        private bool bool_0 = false;

        private IPoint ipoint_0 = null;

        private INewEnvelopeFeedback inewEnvelopeFeedback_0 = null;

        private IPopuMenuWrap ipopuMenuWrap_0 = null;

        public override bool Enabled
        {
            get
            {
                bool result;
                if (_context.FocusMap == null)
                {
                    result = false;
                }
                else if (Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace == null)
                {
                    result = false;
                }
                else if (Yutai.ArcGIS.Common.Editor.Editor.EditMap != null && Yutai.ArcGIS.Common.Editor.Editor.EditMap != _context.FocusMap)
                {
                    result = false;
                }
                else if (_context.FocusMap.SelectionCount == 0)
                {
                    result = false;
                }
                else if (Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate == null)
                {
                    result = false;
                }
                else if (Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer.FeatureClass == null)
                {
                    result = false;
                }
                else if (Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPolyline && Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPolygon)
                {
                    result = false;
                }
                else
                {
                    IEnumFeature enumFeature = _context.FocusMap.FeatureSelection as IEnumFeature;
                    enumFeature.Reset();
                    IFeature feature = enumFeature.Next();
                    esriGeometryType geometryType = feature.Shape.GeometryType;
                    if (geometryType != Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer.FeatureClass.ShapeType)
                    {
                        result = false;
                    }
                    else
                    {
                        for (feature = enumFeature.Next(); feature != null; feature = enumFeature.Next())
                        {
                            if (feature.Shape.GeometryType != geometryType)
                            {
                                result = false;
                                return result;
                            }
                        }
                        result = true;
                    }
                }
                return result;
            }
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

      

     

        public CmdCopyFeaturesTool(IAppContext context)
        {
          OnCreate(context);
        }
        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            this.m_caption = "复制对象工具";
            this.m_category = "编辑器";
            this.m_name = "Edit_CopyFeatureTools";
            this._key = "Edit_CopyFeatureTools";
            this.m_message = "复制对象工具";
            this.m_toolTip = "复制对象工具";
            this.m_bitmap = Properties.Resources.icon_edit_copyfeatures;
            this.m_cursor = new Cursor(
                            base.GetType()
                                .Assembly.GetManifestResourceStream(
                                    "Yutai.Plugins.Editor.Resources.Cursor.CopyFeaturesTool.cur"));
            
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageBeforeText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.Tool;
        }


        public override void OnMouseDown(int int_0, int int_1, int int_2, int int_3)
        {
            if (int_0 != 2)
            {
                if (_context.ActiveView is IPageLayout)
                {
                    IPoint location = _context.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(int_2, int_3);
                    IMap map = _context.ActiveView.HitTestMap(location);
                    if (map == null)
                    {
                        return;
                    }
                    if (map != _context.FocusMap)
                    {
                        _context.ActiveView.FocusMap = map;
                        _context.ActiveView.Refresh();
                    }
                }
                this.ipoint_0 = ((IActiveView)_context.FocusMap).ScreenDisplay.DisplayTransformation.ToMapPoint(int_2, int_3);
                this.bool_0 = true;
            }
        }

        public override void OnMouseMove(int int_0, int int_1, int int_2, int int_3)
        {
            if (this.bool_0)
            {
                IActiveView activeView = (IActiveView)_context.FocusMap;
                if (this.inewEnvelopeFeedback_0 == null)
                {
                    this.inewEnvelopeFeedback_0 = new NewEnvelopeFeedback();
                    this.inewEnvelopeFeedback_0.Display = activeView.ScreenDisplay;
                    this.inewEnvelopeFeedback_0.Start(this.ipoint_0);
                }
                this.inewEnvelopeFeedback_0.MoveTo(activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(int_2, int_3));
            }
        }

        public override void OnMouseUp(int int_0, int int_1, int int_2, int int_3)
        {
            if (int_0 != 2 && this.bool_0)
            {
                this.bool_0 = false;
                IActiveView activeView = (IActiveView)_context.FocusMap;
                IEnvelope envelope = null;
                IPoint point = null;
                if (this.inewEnvelopeFeedback_0 == null)
                {
                    point = this.ipoint_0;
                }
                else
                {
                    envelope = this.inewEnvelopeFeedback_0.Stop();
                    this.inewEnvelopeFeedback_0 = null;
                    if (envelope.Width == 0.0 || envelope.Height == 0.0)
                    {
                        point = this.ipoint_0;
                        envelope = null;
                    }
                }
                IList list = new ArrayList();
                IEnumFeature enumFeature = _context.FocusMap.FeatureSelection as IEnumFeature;
                enumFeature.Reset();
                IFeature feature = enumFeature.Next();
                IGeometryCollection geometryCollection = new GeometryBag() as IGeometryCollection;
                object value = Missing.Value;
                while (feature != null)
                {
                    list.Add(feature);
                    geometryCollection.AddGeometry(feature.ShapeCopy, ref value, ref value);
                    feature = enumFeature.Next();
                }
                IEnvelope envelope2 = (geometryCollection as IGeometryBag).Envelope;
                IPoint point2 = new Point
                {
                    X = (envelope2.XMax + envelope2.XMin) / 2.0,
                    Y = (envelope2.YMax + envelope2.YMin) / 2.0
                };
                double num;
                double num2;
                if (envelope != null)
                {
                    IPoint point3 = new Point
                    {
                        X = (envelope.XMax + envelope.XMin) / 2.0,
                        Y = (envelope.YMax + envelope.YMin) / 2.0
                    };
                    num = point2.X - point3.X;
                    num2 = point2.Y - point3.Y;
                    double num3 = envelope.Width / envelope2.Width;
                    double num4 = envelope.Height / envelope2.Height;
                    num3 = ((num3 > num4) ? num4 : num3);
                    (geometryCollection as ITransform2D).Scale(point2, num3, num3);
                }
                else
                {
                    num = point2.X - point.X;
                    num2 = point2.Y - point.Y;
                }
                (geometryCollection as ITransform2D).Move(-num, -num2);
                Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace.StartEditOperation();
                for (int i = 0; i < list.Count; i++)
                {
                    feature = (list[i] as IFeature);
                    try
                    {
                        IFeature feature2 = RowOperator.CreatRowByRow(feature) as IFeature;
                        feature2.Shape = geometryCollection.get_Geometry(i);
                        feature2.Store();
                    }
                    catch (Exception exception_)
                    {
                        CErrorLog.writeErrorLog(this, exception_, "");
                    }
                }
                Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace.StopEditOperation();
                activeView.Refresh();
            }
        }

      

        //public void Init()
        //{
        //    this.ipopuMenuWrap_0.Clear();
        //    this.ipopuMenuWrap_0.AddItem("CopyFeaturesInPoint", false);
        //}

        public string[] ContextMenuKeys { get {return new string[] {"Edit_CopyFeatures_InPoint"};} }
    }
}
