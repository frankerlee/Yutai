using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    public class CmdReverseLineOrientation : YutaiCommand
    {
        public override bool Enabled
        {
            get
            {
                bool result;
                try
                {
                    if (_context.FocusMap == null)
                    {
                        result = false;
                    }
                    else if (Yutai.ArcGIS.Common.Editor.Editor.EditMap != null &&
                             Yutai.ArcGIS.Common.Editor.Editor.EditMap != _context.FocusMap)
                    {
                        result = false;
                    }
                    else
                    {
                        if (_context.FocusMap.SelectionCount > 0)
                        {
                            IEnumFeature enumFeature = _context.FocusMap.FeatureSelection as IEnumFeature;
                            enumFeature.Reset();
                            for (IFeature feature = enumFeature.Next(); feature != null; feature = enumFeature.Next())
                            {
                                esriGeometryType geometryType = feature.Shape.GeometryType;
                                if ((geometryType == esriGeometryType.esriGeometryPolyline ||
                                     geometryType == esriGeometryType.esriGeometryPolygon) &&
                                    Yutai.ArcGIS.Common.Editor.Editor.CheckWorkspaceEdit(feature.Class as IDataset,
                                        "IsBeingEdited"))
                                {
                                    result = true;
                                    return result;
                                }
                            }
                        }
                        result = false;
                    }
                }
                catch
                {
                    result = false;
                }
                return result;
            }
        }

        public string CommandName
        {
            get { return "_Reverse"; }
        }


        public string CommandLines
        {
            set { }
        }

        public CmdReverseLineOrientation(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            this.m_name = "Editor_ReverseLineOrientation";
            this._key = "Editor_ReverseLineOrientation";

            this.m_caption = "线反转";
            this.m_toolTip = "线反转";
            this.m_message = "线反转";
        }

        public override void OnClick()
        {
            Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace.StartEditOperation();
            IEnumFeature enumFeature = _context.FocusMap.FeatureSelection as IEnumFeature;
            enumFeature.Reset();
            for (IFeature feature = enumFeature.Next(); feature != null; feature = enumFeature.Next())
            {
                esriGeometryType geometryType = feature.Shape.GeometryType;
                if ((geometryType == esriGeometryType.esriGeometryPolyline ||
                     geometryType == esriGeometryType.esriGeometryPolygon) &&
                    Yutai.ArcGIS.Common.Editor.Editor.CheckWorkspaceEdit(feature.Class as IDataset, "IsBeingEdited"))
                {
                    IPolycurve polycurve = feature.Shape as IPolycurve;
                    polycurve.ReverseOrientation();
                    feature.Shape = polycurve;
                    feature.Store();
                    (_context.FocusMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                }
            }
            Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace.StopEditOperation();
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public void HandleCommandParameter(string string_0)
        {
        }

        public void ActiveCommand()
        {
            if (this.Enabled)
            {
                _context.ShowCommandString("", CommandTipsType.CTTCommandTip);
                _context.ShowCommandString("线反向", CommandTipsType.CTTInput);
                this.OnClick();
            }
            else
            {
                if (Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace == null)
                {
                    _context.ShowCommandString("还未启动编辑，请先启动编辑", CommandTipsType.CTTUnKnown);
                }
                else if (Yutai.ArcGIS.Common.Editor.Editor.EditMap != _context.FocusMap)
                {
                    _context.ShowCommandString("当前地图不可编辑", CommandTipsType.CTTUnKnown);
                }
                else
                {
                    _context.ShowCommandString("请选选择线对象", CommandTipsType.CTTUnKnown);
                }
            }
        }

        public void Cancel()
        {
        }
    }
}