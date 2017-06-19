using System;
using System.Reflection;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Editor;
using Yutai.ArcGIS.Common.Geodatabase;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Events;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    public class CmdVerticsEditTool : YutaiTool
    {
        private ISymbol isymbol_0;

        private IActiveViewEvents_Event iactiveViewEvents_Event_0 = null;

        private int int_0 = 0;

        private IFeature ifeature_0 = null;

        private IPointCollection ipointCollection_0 = null;

        private int int_1 = 0;

        private int int_2 = 0;

       

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
                else if (Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace != null)
                {
                    if ((Yutai.ArcGIS.Common.Editor.Editor.EditMap == null ? true : Yutai.ArcGIS.Common.Editor.Editor.EditMap == _context.FocusMap))
                    {
                        flag = true;
                        return flag;
                    }
                    flag = false;
                    return flag;
                }
                else
                {
                    flag = false;
                }
                return flag;
            }
        }

        public CmdVerticsEditTool(IAppContext context)
        {
            OnCreate(context);
        }

        public void ActiveCommand()
        {
            if (!this.Enabled)
            {
                _context.ShowCommandString("", CommandTipsType.CTTCommandTip);
               
                if (_context.Config.IsInEdit)
                {
                    _context.ShowCommandString("节点编辑工具", CommandTipsType.CTTInput);
                    _context.ShowCommandString("请选择线或面要素", CommandTipsType.CTTCommandTip);
                }
                else
                {
                    _context.ShowCommandString("还未启动编辑，请先启动编辑", CommandTipsType.CTTUnKnown);
                }
                
            }
            else
            {
                _context.ShowCommandString("", CommandTipsType.CTTCommandTip);
                _context.ShowCommandString("节点编辑工具", CommandTipsType.CTTInput);
                if (_context.FocusMap.SelectionCount == 1)
                {
                    IEnumFeature featureSelection = _context.FocusMap.FeatureSelection as IEnumFeature;
                    featureSelection.Reset();
                    IFeature feature = featureSelection.Next();
                    esriGeometryType geometryType = feature.Shape.GeometryType;
                    if ((geometryType == esriGeometryType.esriGeometryPolyline ? false : geometryType != esriGeometryType.esriGeometryPolygon))
                    {
                        this.ifeature_0 = null;
                        this.ipointCollection_0 = null;
                    }
                    else
                    {
                        this.ifeature_0 = feature;
                        this.ipointCollection_0 = this.ifeature_0.Shape as IPointCollection;
                    }
                }
                this.ShowCommand();
            }
        }

        public void Cancel()
        {
        }

        public void HandleCommandParameter(string string_0)
        {
            if (string_0.Length != 0)
            {
                if (string_0.Length == 1)
                {
                    string text = string_0.ToUpper();
                    if (text != null)
                    {
                        if (!(text == "N"))
                        {
                            if (!(text == "P"))
                            {
                                if (!(text == "B"))
                                {
                                    if (text == "I")
                                    {
                                        _context.ShowCommandString("输入点:", CommandTipsType.CTTCommandTip);
                                        this.int_0 = 1;
                                        return;
                                    }
                                    if (!(text == "D"))
                                    {
                                        if (text == "E")
                                        {
                                            this.ifeature_0 = null;
                                            this.ipointCollection_0 = null;
                                            this.int_2 = 0;
                                            _context.CurrentTool = null;
                                            (_context.FocusMap as IActiveView).Refresh();
                                        }
                                    }
                                    else
                                    {
                                        this.ipointCollection_0.RemovePoints(this.int_2, 1);
                                        if (this.int_2 == this.ipointCollection_0.PointCount)
                                        {
                                            this.int_2--;
                                        }
                                        Yutai.ArcGIS.Common.Editor.Editor.StartEditOperation(this.ifeature_0.Class as IDataset);
                                        this.ifeature_0.Shape = (this.ipointCollection_0 as IGeometry);
                                        this.ifeature_0.Store();
                                        Yutai.ArcGIS.Common.Editor.Editor.StopEditOperation(this.ifeature_0.Class as IDataset);
                                        this.ZoomToCenter();
                                    }
                                }
                                else
                                {
                                    if (this.ipointCollection_0 is IPolyline && this.int_2 > 0 && this.int_2 < this.ipointCollection_0.PointCount - 1)
                                    {
                                        Yutai.ArcGIS.Common.Editor.Editor.StartEditOperation(this.ifeature_0.Class as IDataset);
                                        try
                                        {
                                            IPoint point = this.ipointCollection_0.get_Point(this.int_2);
                                            GeometryOperator geometryOperator = GeometryOperatorFectory.CreateGeometryOperator(this.ipointCollection_0 as IGeometry);
                                            IGeometryBag geometryBag;
                                            geometryOperator.SplitAtPoint(point, out geometryBag);
                                            if (geometryBag != null)
                                            {
                                                try
                                                {
                                                    this.ifeature_0.Shape = (geometryBag as IGeometryCollection).get_Geometry(0);
                                                    this.ifeature_0.Store();
                                                    this.ipointCollection_0 = (this.ifeature_0.Shape as IPointCollection);
                                                    this.int_2 = 0;
                                                    this.ShowCommand();
                                                    for (int i = 1; i < (geometryBag as IGeometryCollection).GeometryCount; i++)
                                                    {
                                                        IRow row = RowOperator.CreatRowByRow(this.ifeature_0);
                                                        (row as IFeature).Shape = (geometryBag as IGeometryCollection).get_Geometry(i);
                                                        row.Store();
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    CErrorLog.writeErrorLog(this, ex, "");
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            System.Windows.Forms.MessageBox.Show(ex.Message);
                                        }
                                        Yutai.ArcGIS.Common.Editor.Editor.StopEditOperation(this.ifeature_0.Class as IDataset);
                                    }
                                    this.ZoomToCenter();
                                }
                            }
                            else
                            {
                                if (this.int_2 > 0)
                                {
                                    this.int_2--;
                                }
                                this.ZoomToCenter();
                            }
                        }
                        else
                        {
                            if (this.int_2 < this.ipointCollection_0.PointCount - 1)
                            {
                                this.int_2++;
                            }
                            this.ZoomToCenter();
                        }
                    }
                }
                if (this.int_0 == 1)
                {
                    try
                    {
                        this.int_0 = 0;
                        string[] array = string_0.Split(new char[]
                        {
                            ','
                        });
                        IPoint point = new ESRI.ArcGIS.Geometry.Point();
                        if (array.Length >= 2)
                        {
                            double x = Convert.ToDouble(array[0]);
                            double y = Convert.ToDouble(array[1]);
                            point.PutCoords(x, y);
                            if (array.Length >= 3)
                            {
                                double z = Convert.ToDouble(array[2]);
                                point.Z = z;
                            }
                            object obj = 0;
                            object value = Missing.Value;
                            if (this.int_2 == this.ipointCollection_0.PointCount - 1)
                            {
                                obj = this.int_2 - 1;
                            }
                            else
                            {
                                obj = this.int_2;
                            }
                            this.ipointCollection_0.AddPoint(point, ref value, ref obj);
                            Yutai.ArcGIS.Common.Editor.Editor.StartEditOperation(this.ifeature_0.Class as IDataset);
                            this.ifeature_0.Shape = (this.ipointCollection_0 as IGeometry);
                            this.ifeature_0.Store();
                            Yutai.ArcGIS.Common.Editor.Editor.StopEditOperation(this.ifeature_0.Class as IDataset);
                            this.ZoomToCenter();
                        }
                        else
                        {
                            _context.ShowCommandString("输入不正确", CommandTipsType.CTTLog);
                        }
                    }
                    catch
                    {
                        _context.ShowCommandString("输入不正确", CommandTipsType.CTTLog);
                        return;
                    }
                }
                this.ShowCommand();
            }
        }

        private void ActiveView_AfterDraw(IDisplay idisplay_0, esriViewDrawPhase esriViewDrawPhase_0)
        {
            if (this.ifeature_0 != null)
            {
                if ((this.int_2 < 0 ? false : this.int_2 < this.ipointCollection_0.PointCount))
                {
                    IPoint point = this.ipointCollection_0.Point[this.int_2];
                    idisplay_0.StartDrawing(0, -1);
                    idisplay_0.SetSymbol(this.isymbol_0);
                    idisplay_0.DrawPoint(point);
                    idisplay_0.FinishDrawing();
                }
            }
        }

        private void App_ActiveHookChanged(object object_0)
        {
            if (this.iactiveViewEvents_Event_0 != null)
            {
                try
                {
                    this.iactiveViewEvents_Event_0.AfterDraw -= new IActiveViewEvents_AfterDrawEventHandler(this.ActiveView_AfterDraw);
                }
                catch
                {
                }
                this.iactiveViewEvents_Event_0 = null;
            }
            try
            {
                this.iactiveViewEvents_Event_0 = _context.ActiveView as IActiveViewEvents_Event;
                if (this.iactiveViewEvents_Event_0 != null)
                {
                    this.iactiveViewEvents_Event_0.AfterDraw += new IActiveViewEvents_AfterDrawEventHandler(this.ActiveView_AfterDraw);
                }
            }
            catch (Exception exception)
            {
                CErrorLog.writeErrorLog(this, exception, "");
            }
        }

        private void ZoomToCenter()
        {
            IEnvelope extent = (_context.FocusMap as IActiveView).Extent;
            IPoint point = this.ipointCollection_0.Point[this.int_2];
            if ((point.X > extent.XMax || point.Y > extent.YMax || point.X < extent.XMin ? false : point.Y >= extent.YMin))
            {
                (_context.FocusMap as IActiveView).Refresh();
            }
            else
            {
                extent.CenterAt(point);
                (_context.FocusMap as IActiveView).Extent = extent;
                (_context.FocusMap as IActiveView).Refresh();
            }
        }

        private void ShowCommand()
        {
            if (this.ifeature_0 != null)
            {
                _context.ShowCommandString("下一点(N)或[上一点(P)/插入点(I)/打断(B)/删除节点(D)/退出(E)]:", CommandTipsType.CTTCommandTip);
            }
            else
            {
                _context.ShowCommandString("选择编辑的线或面要素:", CommandTipsType.CTTCommandTip);
            }
        }

        public override void OnClick()
        {
            this.ActiveCommand();
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            this.m_caption = "节点编辑工具";
            this.m_category = "编辑";
            this.m_toolTip = "节点编辑工具";
            this.m_name = "Edit_VerticsEdit";
            _context = hook as IAppContext;
            this._key = "Edit_VerticsEdit";
            base._itemType = RibbonItemType.Tool;
            this.m_cursor = new System.Windows.Forms.Cursor(base.GetType().Assembly.GetManifestResourceStream("Yutai.Plugins.Editor.Resources.Cursor.DeleteVertex.cur"));
            this.isymbol_0 = new SimpleMarkerSymbol() as ISymbol;
            (this.isymbol_0 as ISimpleMarkerSymbol).Style = esriSimpleMarkerStyle.esriSMSSquare;
            (this.isymbol_0 as ISimpleMarkerSymbol).Size = 6;
            (this.isymbol_0 as ISimpleMarkerSymbol).Outline = true;
            IRgbColor rgbColorClass = new RgbColor()
            {
                Red = 255,
                Green = 0,
                Blue = 0
            };
            (this.isymbol_0 as ISimpleMarkerSymbol).OutlineColor = rgbColorClass;
            (this.isymbol_0 as ISimpleMarkerSymbol).OutlineSize = 2;
            IRgbColor rgbColor = new RgbColor()
            {
                NullColor = true
            };
            (this.isymbol_0 as ISimpleMarkerSymbol).Color = rgbColor;
            
            (_context as IApplicationEvents).OnActiveHookChanged += new OnActiveHookChangedHandler(this.App_ActiveHookChanged);
            
            if (this.iactiveViewEvents_Event_0 != null)
            {
                try
                {
                    this.iactiveViewEvents_Event_0.AfterDraw -= new IActiveViewEvents_AfterDrawEventHandler(this.ActiveView_AfterDraw);
                }
                catch
                {
                }
            }
            if (_context.FocusMap != null)
            {
                this.iactiveViewEvents_Event_0 = _context.ActiveView as IActiveViewEvents_Event;
                this.iactiveViewEvents_Event_0.AfterDraw += new IActiveViewEvents_AfterDrawEventHandler(this.ActiveView_AfterDraw);
            }
        }

        public override void OnDblClick()
        {
            this.ifeature_0 = null;
            base.OnDblClick();
        }

        public override void OnMouseDown(int Button, int int_4, int int_5, int int_6)
        {
            IFeatureLayer featureLayer;
            if (Button == 1)
            {
                IActiveView focusMap = (IActiveView)_context.FocusMap;
                IPoint mapPoint = focusMap.ScreenDisplay.DisplayTransformation.ToMapPoint(int_5, int_6);
                if (this.int_0 != 1)
                {
                    double mapUnits = Common.ConvertPixelsToMapUnits(focusMap, 6);
                    this.ifeature_0 = Yutai.ArcGIS.Common.Editor.Editor.GetHitLineOrAreaFeature(_context.FocusMap, mapPoint, mapUnits, out featureLayer);
                    if (this.ifeature_0 == null)
                    {
                        _context.ShowCommandString("没选中线或面要素", CommandTipsType.CTTLog);
                        this.ShowCommand();
                    }
                    else
                    {
                        (_context.FocusMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                        _context.FocusMap.ClearSelection();
                        _context.FocusMap.SelectFeature(featureLayer, this.ifeature_0);
                        this.ipointCollection_0 = this.ifeature_0.Shape as IPointCollection;
                        (_context.FocusMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                        this.ShowCommand();
                    }
                }
                else
                {
                    string str = mapPoint.X.ToString();
                    double y = mapPoint.Y;
                    string str1 = string.Concat(str, ",", y.ToString());
                    _context.ShowCommandString(str1, CommandTipsType.CTTInput);
                    this.HandleCommandParameter(str1);
                }
            }
        }
    }
}