using System;
using System.Collections.Generic;
using System.Linq;
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
    public class CmdViewMeasureLength : YutaiTool
    {
        private IPoint _iPoint0 = null;
        private IPoint _iPoint0_g = null;
        private IPoint _iPoint1 = null;
        private INewLineFeedback _lineFeedback;
        private double _length = 0;
        private IMap map;
        private IActiveView activeView;
        private IGraphicsContainer graphicsContainer;
        private IGroupElement pGroupElement;
        private int _pointnum = 0;
        private ISimpleLineSymbol pSimpleLineSymbol;
        private ITextSymbol pAngleSymbol;
        private ITextSymbol pDistSymbol;

        public CmdViewMeasureLength(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick()
        {
            map = _context.MapControl.ActiveView as IMap;
            activeView = _context.MapControl.ActiveView;
            graphicsContainer = map as IGraphicsContainer;
            _pointnum = 0;
            graphicsContainer.DeleteAllElements();
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
            base.m_caption = "距离量测";
            base.m_category = "View";
            base.m_bitmap = Properties.Resources.icon_measure_distance;
            base.m_cursor =
                new Cursor(base.GetType().Assembly.GetManifestResourceStream("Yutai.Resource.Cursor.Measure.cur"));
            base.m_name = "View_MeasureLength";
            base._key = "View_MeasureLength";
            base.m_toolTip = "距离量测";
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
            ITextPath textPath=new SimpleTextPathClass();
            ISimpleTextSymbol simpleTextPath = (ISimpleTextSymbol) pAngleSymbol;
            simpleTextPath.TextPath = textPath;
            simpleTextPath.HorizontalAlignment= esriTextHorizontalAlignment.esriTHACenter;
            simpleTextPath.VerticalAlignment= esriTextVerticalAlignment.esriTVABaseline;
            simpleTextPath.YOffset = 3;

            pDistSymbol = new TextSymbolClass()
            {
                Color = pRgb2,
                Size = 10
            };
            ITextPath textPath2 = new SimpleTextPathClass();
            simpleTextPath = (ISimpleTextSymbol)pDistSymbol;
            simpleTextPath.TextPath = textPath;
            simpleTextPath.HorizontalAlignment = esriTextHorizontalAlignment.esriTHACenter;
            simpleTextPath.VerticalAlignment = esriTextVerticalAlignment.esriTVATop;
            simpleTextPath.YOffset = -3;


        }


        public override void OnMouseDown(int button, int Shift, int x, int y)
        {
            if (button != 2)
            {
                _pointnum++;
                this.CalculateLength(x, y, 1);
            }
        }

        public override void OnMouseMove(int Button, int Shift, int x, int y)
        {
            this.CalculateLength(x, y, 2);
        }

        public override void OnDblClick()
        {
            this.CalculateLength(0, 0, 3);
        }

        private double GetAngle(ILine lineClass)
        {
            double ang = (Math.PI/2 - lineClass.Angle)*180/Math.PI;
            if (ang > 180)
                ang -= 180;
            if (ang < 0)
                ang += 180;
            return ang;
        }

        private void CalculateLength(int x, int y, int num)
        {
            string[] str;
            string str1 = String.Concat(" ", this.GetUnitDesc(_context.MapControl.ActiveView.FocusMap.MapUnits));
            ILine lineClass = new Line();
            double length = 0;
            string str2 = "";
            IPoint mapPoint = _context.MapControl.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
            if (mapPoint != null)
            {
                if (!(num != 1 ? true : this._lineFeedback != null))
                {
                    //第一个点
                    this._length = 0;
                    this._lineFeedback = new NewLineFeedbackClass()
                    {
                        Display = _context.MapControl.ActiveView.ScreenDisplay
                    };
                    _lineFeedback.Start(mapPoint);
                    _iPoint0 = mapPoint;
                    graphicsContainer.DeleteAllElements();
                    str2 = string.Concat("距离 = 0.0 ", str1, ", 总长度 = 0.0 ", str1);
                }
                else if (this._lineFeedback != null)
                {
                    switch (num)
                    {
                        case 1:
                        {
                            lineClass.PutCoords(this._iPoint0, mapPoint);
                            length = lineClass.Length;
                            _length = _length + length;
                            this._lineFeedback.AddPoint(mapPoint);
                            str = new string[]
                                {"距离 = ", length.ToString("0.####"), str1, ", 总长度 = ", _length.ToString("0.####"), str1};
                            str2 = string.Concat(str);
                            ILineElement pLineElement = new LineElementClass();
                            pLineElement.Symbol = pSimpleLineSymbol;
                            IElement pElement = pLineElement as IElement;
                            IPolyline pLine2 = new Polyline() as IPolyline;
                            IPointCollection pCol = pLine2 as IPointCollection;
                            pCol.AddPoint(_iPoint0);
                            pCol.AddPoint(mapPoint);
                            pElement.Geometry = (IGeometry) pLine2;
                            graphicsContainer.AddElement(pElement, 0);
                            double ang = GetAngle(lineClass);
                            ITextElement pTextElement = new TextElementClass();
                            pTextElement.Symbol = pAngleSymbol;
                            pTextElement.Text = ang.ToString("###.##");
                            IElement pTextElement2 = pTextElement as IElement;
                            pTextElement2.Geometry = (IGeometry) pLine2;
                            graphicsContainer.AddElement(pTextElement2, 0);

                            ITextElement pTextElement3 = new TextElementClass();

                            pTextElement3.Symbol = pDistSymbol;
                            pTextElement3.Text = string.Format("{0:###.##}({1:####.##}){2}", lineClass.Length, _length,
                                str1);
                            IElement pTextElement4 = pTextElement3 as IElement;
                            pTextElement4.Geometry = (IGeometry) pLine2;
                            graphicsContainer.AddElement(pTextElement4, 0);
                          
                            activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                            this._iPoint0 = mapPoint;

                            break;
                        }
                        case 2:
                        {
                            lineClass.PutCoords(this._iPoint0, mapPoint);
                            length = lineClass.Length;
                            double num2 = this._length + length;
                            str = new string[]
                                {"距离 = ", length.ToString("0.####"), str1, ", 总长度 = ", num2.ToString("0.####"), str1};
                            str2 = string.Concat(str);
                            //this.m_HookHelper.SetStatus(str2);
                            this._lineFeedback.MoveTo(mapPoint);
                            this._iPoint1 = mapPoint;
                            break;
                        }
                        case 3:
                        {
                            lineClass.PutCoords(this._iPoint0, this._iPoint1);
                            length = 0;

                            _length = _length + lineClass.Length;
                            str = new string[]
                            {
                                "距离 = ", length.ToString("0.####"), str1, ", 总长度 = ", this._length.ToString("0.####"),
                                str1
                            };
                            str2 = string.Concat(str);
                            //this.m_HookHelper.SetStatus(str2);
                            this._lineFeedback = null;
                            _context.MapControl.ActiveView.Refresh();
                            break;
                        }
                    }
                }
            }
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
    }
}