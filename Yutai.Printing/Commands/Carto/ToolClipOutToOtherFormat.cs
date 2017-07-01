using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Carto.Library;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Geodatabase;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Printing.Commands.Carto
{
    public sealed class ToolClipOutToOtherFormat : YutaiTool
    {
        private IPolygon ipolygon_0 = null;

        private IDisplayFeedback idisplayFeedback_0 = null;

        public override bool Enabled
        {
            get { return this._context.FocusMap.LayerCount > 0; }
        }


        public override void OnCreate(object hook)
        {
            this.m_category = "制图工具";
            this.m_caption = "交换";
            this.m_message = "裁剪输出工具";
            this.m_toolTip = "裁剪输出工具";

            base.m_bitmap = Properties.Resources.icon_clip_cut3;
            base.m_name = "Printing_RealClipOutToOtherFormat";
            _key = "Printing_RealClipOutToOtherFormat";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;
            _context = hook as IAppContext;
        }

        public ToolClipOutToOtherFormat(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            _context.SetCurrentTool(this);
        }

        private void method_0(IDisplay idisplay_0, esriViewDrawPhase esriViewDrawPhase_0)
        {
            if (this.ipolygon_0 != null)
            {
                idisplay_0.StartDrawing(idisplay_0.hDC, -1);
                IFillSymbol fillSymbol = new SimpleFillSymbol();
                (fillSymbol as ISimpleFillSymbol).Style = esriSimpleFillStyle.esriSFSNull;
                idisplay_0.SetSymbol(fillSymbol as ISymbol);
                idisplay_0.DrawPolygon(this.ipolygon_0);
                idisplay_0.FinishDrawing();
            }
        }

        private void method_1(IGroupLayer igroupLayer_0, List<IFeatureClass> list_0)
        {
            for (int i = 0; i < (igroupLayer_0 as ICompositeLayer).Count; i++)
            {
                ILayer layer = (igroupLayer_0 as ICompositeLayer).get_Layer(i);
                if (layer.Visible)
                {
                    if (layer is IFeatureLayer)
                    {
                        list_0.Add((layer as IFeatureLayer).FeatureClass);
                    }
                    else if (layer is IGroupLayer)
                    {
                        this.method_1(layer as IGroupLayer, list_0);
                    }
                }
            }
        }

        public override void OnDblClick()
        {
            if (this.idisplayFeedback_0 == null) return;
            this.ipolygon_0 = (this.idisplayFeedback_0 as INewPolygonFeedback).Stop();
            this.idisplayFeedback_0 = null;
            if ((this.ipolygon_0 as IArea).Area == 0.0)
            {
                this.ipolygon_0 = null;
                return;
            }

            (this._context.FocusMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewForeground, null, null);
            if ((this.ipolygon_0 as ITopologicalOperator).IsSimple)
            {
                this.ipolygon_0.SpatialReference = this._context.FocusMap.SpatialReference;
                frmClipOutSet frmClipOutSet = new frmClipOutSet();
                if (frmClipOutSet.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    List<IFeatureClass> list = new List<IFeatureClass>();
                    for (int i = 0; i < this._context.FocusMap.LayerCount; i++)
                    {
                        ILayer layer = this._context.FocusMap.get_Layer(i);
                        if (layer.Visible)
                        {
                            if (layer is IFeatureLayer)
                            {
                                list.Add((layer as IFeatureLayer).FeatureClass);
                            }
                            else if (layer is IGroupLayer)
                            {
                                this.method_1(layer as IGroupLayer, list);
                            }
                        }
                    }
                    if (frmClipOutSet.Type == 0)
                    {
                        SDEToShapefile sde1 = new SDEToShapefile();
                        sde1.AddFeatureClasses(list);
                        sde1.ClipGeometry = this.ipolygon_0;
                        sde1.IsClip = true;
                        sde1.Convert(frmClipOutSet.OutWorspace);
                        ComReleaser.ReleaseCOMObject(frmClipOutSet.OutWorspace);
                    }
                    else if (frmClipOutSet.Type == 1)
                    {
                        new ExportToMiTab
                        {
                            InputFeatureClasses = list,
                            OutPath = frmClipOutSet.OutPath,
                            ClipGeometry = this.ipolygon_0,
                            IsClip = true
                        }.Export();
                    }
                    else if (frmClipOutSet.Type == 2)
                    {
                        VCTWrite vCTWrite = new VCTWrite();
                        for (int i = 0; i < list.Count; i++)
                        {
                            vCTWrite.AddDataset(list[i] as IDataset);
                        }
                        vCTWrite.ClipGeometry = this.ipolygon_0;
                        vCTWrite.IsClip = true;
                        vCTWrite.Write(frmClipOutSet.OutPath);
                    }
                }
            }
            this.ipolygon_0 = null;
            (this._context.FocusMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewForeground, null, null);
        }

        public override void OnMouseDown(int Button, int int_1, int int_2, int int_3)
        {
            if (Button == 1)
            {
                IActiveView activeView = (IActiveView) this._context.FocusMap;
                IPoint point = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(int_2, int_3);
                if (this.idisplayFeedback_0 == null)
                {
                    this.idisplayFeedback_0 = new NewPolygonFeedback();
                    this.idisplayFeedback_0.Display = activeView.ScreenDisplay;
                    (this.idisplayFeedback_0 as INewPolygonFeedback).Start(point);
                }
                else
                {
                    (this.idisplayFeedback_0 as INewPolygonFeedback).AddPoint(point);
                }
            }
        }

        public override void OnMouseMove(int Button, int int_1, int int_2, int int_3)
        {
            if (this.idisplayFeedback_0 != null)
            {
                IActiveView activeView = (IActiveView) this._context.FocusMap;
                IPoint point = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(int_2, int_3);
                this.idisplayFeedback_0.MoveTo(point);
            }
        }

        public override void OnMouseUp(int Button, int int_1, int int_2, int int_3)
        {
            if (Button == 2)
            {
                IActiveView activeView = (IActiveView) this._context.FocusMap;
                this.idisplayFeedback_0 = null;
                activeView.Refresh();
            }
        }
    }
}