using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Commands.Views
{
    public class CmdViewMeasureArea : YutaiTool
    {
        private double _length = 0;
        private IMap map;
        private IActiveView activeView;
        private IGraphicsContainer graphicsContainer;
        private IGroupElement pGroupElement;
        private int _pointnum = 0;
        private ISimpleLineSymbol pSimpleLineSymbol;
        private ITextSymbol pAngleSymbol;
        private ITextSymbol pDistSymbol;
        private ISimpleFillSymbol pSimpleFillSymbol;
        private INewPolygonFeedback _polygonFeedback = null;
        private IPointCollection _pointCollection = null;


        public CmdViewMeasureArea(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick()
        {
            map = _context.FocusMap as IMap;
            activeView = _context.ActiveView;
            graphicsContainer = map as IGraphicsContainer;
            _pointnum = 0;
            graphicsContainer.DeleteAllElements();
            ((IActiveView) this._context.FocusMap).PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            _context.SetCurrentTool(this);
        }

        public override bool Deactivate()
        {
            this.m_cursor = System.Windows.Forms.Cursors.Default;
            return base.Deactivate();
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "面积量测";
            base.m_category = "View";
            base.m_bitmap = Properties.Resources.icon_measure_area;
            base.m_cursor =
                new Cursor(base.GetType().Assembly.GetManifestResourceStream("Yutai.Resource.Cursor.Measure.cur"));
            base.m_name = "View_MeasureArea";
            base._key = "View_MeasureArea";
            base.m_toolTip = "面积量测";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;

            IRgbColor pRgb = new RgbColor() as IRgbColor;
            pRgb.Red = 200;
            pRgb.Green = 0;
            pRgb.Blue = 0;

            pSimpleLineSymbol = new SimpleLineSymbolClass()
                {Color = pRgb, Width = 1.5, Style = esriSimpleLineStyle.esriSLSSolid};
            IRgbColor pRgb2 = new RgbColor() as IRgbColor;
            pRgb2.Red = 255;
            pRgb2.Green = 0;
            pRgb2.Blue = 0;
            pAngleSymbol = new TextSymbolClass()
            {
                Color = pRgb2,
                Size = 10
            };


            pDistSymbol = new TextSymbolClass()
            {
                Color = pRgb2,
                Size = 10
            };

            pDistSymbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHACenter;
            pDistSymbol.VerticalAlignment = esriTextVerticalAlignment.esriTVACenter;
        }

        private string GetUnitDesc(esriUnits esriUnits_0)
        {
            string str;
            switch (esriUnits_0)
            {
                case esriUnits.esriUnknownUnits:
                {
                    str = "未知单位";
                    break;
                }
                case esriUnits.esriInches:
                {
                    str = "英寸";
                    break;
                }
                case esriUnits.esriPoints:
                {
                    str = "点";
                    break;
                }
                case esriUnits.esriFeet:
                {
                    str = "英尺";
                    break;
                }
                case esriUnits.esriYards:
                {
                    str = "码";
                    break;
                }
                case esriUnits.esriMiles:
                {
                    str = "英里";
                    break;
                }
                case esriUnits.esriNauticalMiles:
                {
                    str = "海里";
                    break;
                }
                case esriUnits.esriMillimeters:
                {
                    str = "毫米";
                    break;
                }
                case esriUnits.esriCentimeters:
                {
                    str = "厘米";
                    break;
                }
                case esriUnits.esriMeters:
                {
                    str = "米";
                    break;
                }
                case esriUnits.esriKilometers:
                {
                    str = "公里";
                    break;
                }
                case esriUnits.esriDecimalDegrees:
                {
                    str = "度";
                    break;
                }
                case esriUnits.esriDecimeters:
                {
                    str = "分米";
                    break;
                }
                default:
                {
                    str = "未知单位";
                    break;
                }
            }
            return str;
        }

        public override void OnMouseDown(int button, int Shift, int x, int y)
        {
            if (button != 2)
            {
                _pointnum++;
                this.CalculateArea(x, y, 1);
            }
            else
            {
                graphicsContainer.DeleteAllElements();
                ((IActiveView) this._context.FocusMap).PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            }
        }

        public override void OnMouseMove(int Button, int Shift, int x, int y)
        {
            this.CalculateArea(x, y, 2);
        }

        public override void OnDblClick()
        {
            this.CalculateArea(0, 0, 3);
        }

        private void CalculateArea(int x, int y, int pntnum)
        {
            double num;
            string str;
            string str1 = string.Concat(" 平方", this.GetUnitDesc(_context.FocusMap.MapUnits));
            object value = Missing.Value;
            IPoint mapPoint = ((IActiveView) this._context.FocusMap).ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
            if (!(pntnum != 1 ? true : this._polygonFeedback != null))
            {
                this._polygonFeedback = new NewPolygonFeedbackClass();
                this._polygonFeedback.Start(mapPoint);
                this._polygonFeedback.Display = ((IActiveView) this._context.FocusMap).ScreenDisplay;
                this._pointCollection = new Polygon() as IPointCollection;
                this._pointCollection.AddPoint(mapPoint, ref value, ref value);
                graphicsContainer.DeleteAllElements();
                ((IActiveView) this._context.FocusMap).PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            }
            else if (this._polygonFeedback != null)
            {
                switch (pntnum)
                {
                    case 1:
                    {
                        this._polygonFeedback.AddPoint(mapPoint);
                        this._pointCollection.AddPoint(mapPoint, ref value, ref value);
                        if (this._pointCollection.PointCount > 2)
                        {
                            ITextElement textElement = new ParagraphTextElementClass();
                            IPointCollection polygonClass = new Polygon() as IPointCollection;
                            polygonClass.AddPointCollection(this._pointCollection);
                            polygonClass.AddPoint(_pointCollection.Point[0]);
                            num = Math.Abs(((IArea) polygonClass).Area);
                            str = string.Concat("总面积 = ", num.ToString("#.##"), str1);
                            textElement.Text = str;
                            textElement.Symbol = pDistSymbol;
                            IElement element = textElement as IElement;
                            element.Geometry = polygonClass as IGeometry;
                            graphicsContainer.DeleteAllElements();
                            graphicsContainer.AddElement(element, 0);
                            ((IActiveView) this._context.FocusMap).PartialRefresh(esriViewDrawPhase.esriViewGraphics,
                                null, null);
                        }
                        break;
                    }
                    case 2:
                    {
                        this._polygonFeedback.MoveTo(mapPoint);
                        if (this._pointCollection.PointCount <= 1)
                        {
                            break;
                        }
                        IPointCollection polygonClass = new Polygon() as IPointCollection;
                        polygonClass.AddPointCollection(this._pointCollection);
                        polygonClass.AddPoint(mapPoint, ref value, ref value);
                        num = Math.Abs(((IArea) polygonClass).Area);
                        str = string.Concat("总面积 = ", num.ToString("#.##"), str1);
                        //ITextElement textElement = new ParagraphTextElementClass();
                        //textElement.Text = str;
                        //textElement.Symbol = pDistSymbol;
                        //IElement element = textElement as IElement;
                        //element.Geometry = polygonClass as IGeometry;
                        //graphicsContainer.DeleteAllElements();
                        //graphicsContainer.AddElement(element, 0);
                        //activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                        break;
                    }
                    case 3:
                    {
                        IPolygon polygon = this._polygonFeedback.Stop();
                        if (polygon.IsEmpty)
                        {
                            // this.m_HookHelper.SetStatus("");
                        }
                        else
                        {
                            num = Math.Abs(((IArea) polygon).Area);
                            str = string.Concat("总面积 = ", num.ToString("#.##"), str1);

                            ITextElement textElement = new ParagraphTextElementClass();
                            textElement.Text = str;
                            textElement.Symbol = pDistSymbol;
                            IElement element = textElement as IElement;
                            element.Geometry = polygon as IGeometry;
                            graphicsContainer.DeleteAllElements();
                            graphicsContainer.AddElement(element, 0);
                            ((IActiveView) this._context.FocusMap).PartialRefresh(esriViewDrawPhase.esriViewGraphics,
                                null, null);
                        }
                        this._polygonFeedback = null;
                        this._pointCollection = null;
                        ((IActiveView) this._context.FocusMap).Refresh();
                        break;
                    }
                }
            }
        }
    }
}