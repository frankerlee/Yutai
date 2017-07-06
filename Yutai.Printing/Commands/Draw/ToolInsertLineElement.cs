using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Carto;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Printing.Commands
{
    public class ToolInsertLineElement : YutaiTool
    {
        private INewLineFeedback _lineFeedback = null;

        private IPoint _Point = null;

        private esriLineConstraints lineConstraints = esriLineConstraints.esriLineConstraintsNone;

        public override bool Enabled
        {
            get { return _context.ActiveView != null; }
        }

        public ToolInsertLineElement(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "";
            base.m_toolTip = "新建线";
            base.m_message = "新建线";
            base.m_category = "Printing";
            base.m_bitmap = Properties.Resources.icon_line;
            base.m_name = "Printing_NewLine";
            _key = "Printing_NewLine";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;
            m_cursor =
                new Cursor(
                    base.GetType()
                        .Assembly.GetManifestResourceStream("Yutai.Plugins.Printing.Resources.Cursor.Digitise.cur"));
        }

        public override void OnClick()
        {
            _context.SetCurrentTool(this);
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }


        private IActiveView GetActiveView()
        {
            IActiveView focusMap = null;


            if (this._context.MainView.ControlType == GISControlType.PageLayout)
            {
                focusMap =
                    (this._context.MainView.ActiveGISControl as IPageLayoutControl).ActiveView.FocusMap as IActiveView;
            }

            return focusMap;
        }

        public override void OnMouseDown(int button, int Shift, int x, int y)
        {
            IActiveView activeView = this._context.ActiveView;
            IPoint mapPoint = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
            if (this._lineFeedback != null)
            {
                if (this._Point != null)
                {
                    if (this.lineConstraints == esriLineConstraints.esriLineConstraintsVertical)
                    {
                        mapPoint.X = this._Point.X;
                    }
                    else if (this.lineConstraints == esriLineConstraints.esriLineConstraintsHorizontal)
                    {
                        mapPoint.Y = this._Point.Y;
                    }
                }
                this._lineFeedback.AddPoint(mapPoint);
                this._Point = mapPoint;
            }
            else
            {
                this._lineFeedback = new NewLineFeedback()
                {
                    Constraint = this.lineConstraints,
                    Display = activeView.ScreenDisplay
                };
                this._lineFeedback.Start(mapPoint);
                this._Point = mapPoint;
            }
        }

        public override void OnMouseMove(int Button, int Shift, int x, int y)
        {
            if (this._lineFeedback != null)
            {
                IActiveView activeView = this._context.ActiveView;
                IPoint mapPoint = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
                if (this._Point != null)
                {
                    if (this.lineConstraints == esriLineConstraints.esriLineConstraintsVertical)
                    {
                        mapPoint.X = this._Point.X;
                    }
                    else if (this.lineConstraints == esriLineConstraints.esriLineConstraintsHorizontal)
                    {
                        mapPoint.Y = this._Point.Y;
                    }
                }
                this._lineFeedback.MoveTo(mapPoint);
            }
        }


        public override void OnDblClick()
        {
            if (this._lineFeedback != null)
            {
                this._Point = null;
                IPolyline polyline = this._lineFeedback.Stop();
                this._lineFeedback = null;
                if (!polyline.IsEmpty)
                {
                    IElement lineElementClass = new LineElement()
                    {
                        Geometry = polyline
                    };
                    (lineElementClass as ILineElement).Symbol = new SimpleLineSymbol();
                    NewElementOperation newElementOperation = new NewElementOperation()
                    {
                        ActiveView = this._context.ActiveView,
                        ContainHook = this.GetActiveView(),
                        Element = lineElementClass
                    };
                    this._context.OperationStack.Do(newElementOperation);
                }
            }
        }

        public override void OnKeyDown(int button, int shift)
        {
            if (button == 27)
            {
                this._lineFeedback = null;
                this._context.ActiveView.Refresh();
                this.lineConstraints = esriLineConstraints.esriLineConstraintsNone;
                this._Point = null;
            }
            else if (button == 16)
            {
                this.lineConstraints = esriLineConstraints.esriLineConstraintsHorizontal;
                if (this._lineFeedback != null)
                {
                    this._lineFeedback.Constraint = esriLineConstraints.esriLineConstraintsHorizontal;
                }
            }
            else if (button == 17)
            {
                this.lineConstraints = esriLineConstraints.esriLineConstraintsVertical;
                if (this._lineFeedback != null)
                {
                    this._lineFeedback.Constraint = esriLineConstraints.esriLineConstraintsHorizontal;
                }
            }
        }

        public override void OnKeyUp(int button, int shift)
        {
            this.lineConstraints = esriLineConstraints.esriLineConstraintsNone;
            if (this._lineFeedback != null)
            {
                this._lineFeedback.Constraint = esriLineConstraints.esriLineConstraintsNone;
            }
        }
    }
}