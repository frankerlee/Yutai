using System;
using System.Reflection;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    public class CmdConstructParallel : YutaiCommand
    {
        public CmdConstructParallel(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_edit_constructparallel;
            this.m_caption = "平行线";
            this.m_category = "Edit";
            this.m_message = "平行线";
            this.m_name = "Edit_ConstructParallel";
            this._key = "Edit_ConstructParallel";
            this.m_toolTip = "平行线";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageBeforeText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.Button;
        }

        public double m_offset = 1;

        public int m_ConstructOffset = 5;

        public override bool Enabled
        {
            get
            {
                bool flag;
                try
                {
                    if (_context.FocusMap == null)
                    {
                        flag = false;
                    }
                    else if (
                        !(Yutai.ArcGIS.Common.Editor.Editor.EditMap == null
                            ? true
                            : Yutai.ArcGIS.Common.Editor.Editor.EditMap == _context.FocusMap))
                    {
                        flag = false;
                    }
                    else if (_context.FocusMap.SelectionCount == 0)
                    {
                        flag = false;
                    }
                    else if (Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate == null)
                    {
                        flag = false;
                    }
                    else if (Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer.FeatureClass == null)
                    {
                        flag = false;
                    }
                    else if (Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer.FeatureClass.ShapeType ==
                             esriGeometryType.esriGeometryPolyline)
                    {
                        IEnumFeature featureSelection = _context.FocusMap.FeatureSelection as IEnumFeature;
                        featureSelection.Reset();
                        IFeature feature = featureSelection.Next();
                        while (feature != null)
                        {
                            if (feature.Shape.GeometryType != esriGeometryType.esriGeometryPolyline)
                            {
                                flag = false;
                                return flag;
                            }
                            else
                            {
                                feature = featureSelection.Next();
                            }
                        }
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                }
                catch
                {
                    flag = false;
                }
                return flag;
            }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        private void CreateParallel(IGeometry pGeometry, IFeatureClass pFeatureClass)
        {
            double num;
            double num1;
            try
            {
                if ((pGeometry == null ? false : pFeatureClass != null))
                {
                    int num2 = pFeatureClass.FindField(pFeatureClass.ShapeFieldName);
                    IGeometryDef geometryDef = pFeatureClass.Fields.Field[num2].GeometryDef;
                    if (geometryDef.HasZ)
                    {
                        ((IZAware) pGeometry).ZAware = true;
                        if (pGeometry is IZ)
                        {
                            IZ igeometry0 = (IZ) pGeometry;
                            geometryDef.SpatialReference.GetZDomain(out num, out num1);
                            igeometry0.SetConstantZ(num);
                        }
                        else if (pGeometry is IPoint)
                        {
                            geometryDef.SpatialReference.GetZDomain(out num, out num1);
                            (pGeometry as IPoint).Z = num;
                        }
                    }
                    if (geometryDef.HasM)
                    {
                        ((IMAware) pGeometry).MAware = true;
                    }
                    IFeature feature = pFeatureClass.CreateFeature();
                    feature.Shape = pGeometry;
                    try
                    {
                        ((IRowSubtypes) feature).InitDefaultValues();
                    }
                    catch (Exception exception)
                    {
                        // CErrorLog.writeErrorLog(this, exception, "");
                    }
                    feature.Store();
                }
            }
            catch (Exception exception1)
            {
                //CErrorLog.writeErrorLog(null, exception1, "");
            }
        }

        public override void OnClick()
        {
            this.m_offset = SketchShareEx.m_offset;
            this.m_ConstructOffset = SketchShareEx.ConstructOffset;
            IEnumFeature featureSelection = _context.FocusMap.FeatureSelection as IEnumFeature;
            featureSelection.Reset();
            object value = Missing.Value;
            object mConstructOffset = this.m_ConstructOffset;
            IFeature feature = featureSelection.Next();
            IActiveView focusMap = _context.FocusMap as IActiveView;
            IFeatureClass featureClass = Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer.FeatureClass;
            Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace.StartEditOperation();
            while (feature != null)
            {
                if (feature.Shape.GeometryType == esriGeometryType.esriGeometryPolyline)
                {
                    IConstructCurve polylineClass = new Polyline() as IConstructCurve;
                    polylineClass.ConstructOffset(feature.Shape as IPolycurve, this.m_offset, ref mConstructOffset,
                        ref value);
                    this.CreateParallel(polylineClass as IGeometry, featureClass);
                }
                feature = featureSelection.Next();
            }
            Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace.StopEditOperation();
            focusMap.Refresh();
        }
    }
}