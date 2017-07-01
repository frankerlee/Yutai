using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.Interfaces;

namespace Yutai.ArcGIS.Common.Editor.Helpers
{
    public class SnapHelper
    {
        private bool bool_0;

        private IAnchorPoint ianchorPoint_0;

        private ISimpleMarkerSymbol isimpleMarkerSymbol_0 = new SimpleMarkerSymbol();

        private IActiveView iactiveView_0 = null;

        private IEngineSnapEnvironment iengineSnapEnvironment_0 = null;

        private IAppContext _appContext = null;

        public IPoint AnchorPoint { get; set; }

        public SnapHelper(IActiveView iactiveView_1, IEngineSnapEnvironment iengineSnapEnvironment_1)
        {
            this.iactiveView_0 = iactiveView_1;
            this.iengineSnapEnvironment_0 = iengineSnapEnvironment_1;
        }

        public SnapHelper(IAppContext ikhookHelper_0)
        {
            this._appContext = ikhookHelper_0;
            this.iengineSnapEnvironment_0 = ikhookHelper_0.Config.EngineSnapEnvironment;
        }

        private ISnappingResult method_0(IPoint ipoint_1)
        {
            IActiveView focusMap = this._appContext.MapControl.Map as IActiveView;
            IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
            pointClass.PutCoords(ipoint_1.X, ipoint_1.Y);
            IEngineSnapEnvironment engineSnapEnvironment = ApplicationRef.AppContext.Config.EngineSnapEnvironment;
            ISnappingResult snappingResult = null;
            if (engineSnapEnvironment is ISnapEnvironment)
            {
                ISnapEnvironment snapEnvironment = engineSnapEnvironment as ISnapEnvironment;
                if ((snapEnvironment == null || !ApplicationRef.AppContext.Config.UseSnap
                    ? false
                    : snapEnvironment.SnapPoint(pointClass, ipoint_1)))
                {
                    SnappingResult snappingResult1 = new SnappingResult()
                    {
                        X = ipoint_1.X,
                        Y = ipoint_1.Y
                    };
                    snappingResult = snappingResult1;
                }
            }
            else if ((engineSnapEnvironment == null || !ApplicationRef.AppContext.Config.UseSnap
                ? false
                : engineSnapEnvironment.SnapPoint(ipoint_1)))
            {
                SnappingResult snappingResult2 = new SnappingResult()
                {
                    X = ipoint_1.X,
                    Y = ipoint_1.Y
                };
                snappingResult = snappingResult2;
            }
            return snappingResult;
        }

        private void method_1(IPoint ipoint_1, esriSimpleMarkerStyle esriSimpleMarkerStyle_0)
        {
            this.ianchorPoint_0 = new AnchorPoint()
            {
                Symbol = this.isimpleMarkerSymbol_0 as ISymbol
            };
            this.ianchorPoint_0.MoveTo(ipoint_1, this.iactiveView_0.ScreenDisplay);
        }

        public void Snap2Point(IPoint ipoint_1, IPoint ipoint_2, esriSimpleMarkerStyle esriSimpleMarkerStyle_0)
        {
            ISnappingResult snappingResult;
            this.AnchorPoint = ipoint_1;
            if (!Editor.UseOldSnap)
            {
                snappingResult = SketchToolAssist.m_psnaper.Snap(this.AnchorPoint);
                if (snappingResult == null)
                {
                    if (this.ianchorPoint_0 == null)
                    {
                        this.ianchorPoint_0 = new AnchorPoint()
                        {
                            Symbol = this.isimpleMarkerSymbol_0 as ISymbol
                        };
                    }
                    this.ianchorPoint_0.MoveTo(this.AnchorPoint,
                        (this._appContext.MapControl.Map as IActiveView).ScreenDisplay);
                }
                else
                {
                    this.AnchorPoint = snappingResult.Location;
                    if (this.ianchorPoint_0 == null)
                    {
                        this.ianchorPoint_0 = new AnchorPoint()
                        {
                            Symbol = this.isimpleMarkerSymbol_0 as ISymbol
                        };
                    }
                    this.ianchorPoint_0.MoveTo(this.AnchorPoint,
                        (this._appContext.MapControl.Map as IActiveView).ScreenDisplay);
                }
            }
            else
            {
                IAppContext application = ApplicationRef.AppContext;
                snappingResult = this.method_0(this.AnchorPoint);
                if (snappingResult == null)
                {
                    if (this.ianchorPoint_0 == null)
                    {
                        this.ianchorPoint_0 = new AnchorPoint()
                        {
                            Symbol = this.isimpleMarkerSymbol_0 as ISymbol
                        };
                    }
                    this.ianchorPoint_0.MoveTo(this.AnchorPoint,
                        (this._appContext.MapControl.Map as IActiveView).ScreenDisplay);
                }
                else
                {
                    this.AnchorPoint = snappingResult.Location;
                    if (this.ianchorPoint_0 == null)
                    {
                        this.ianchorPoint_0 = new AnchorPoint()
                        {
                            Symbol = this.isimpleMarkerSymbol_0 as ISymbol
                        };
                    }
                    this.ianchorPoint_0.MoveTo(this.AnchorPoint,
                        (this._appContext.MapControl.Map as IActiveView).ScreenDisplay);
                }
            }
        }

        public bool Snap2Point2(IPoint ipoint_1, IPoint ipoint_2, esriSimpleMarkerStyle esriSimpleMarkerStyle_0)
        {
            if (!Editor.UseOldSnap)
            {
                ISnappingResult snappingResult = SketchToolAssist.m_psnaper.Snap(ipoint_1);
                if (snappingResult == null)
                {
                    if (this.ianchorPoint_0 == null)
                    {
                        this.ianchorPoint_0 = new AnchorPoint()
                        {
                            Symbol = this.isimpleMarkerSymbol_0 as ISymbol
                        };
                    }
                    this.ianchorPoint_0.MoveTo(ipoint_1, this.iactiveView_0.ScreenDisplay);
                    this.bool_0 = false;
                }
                else
                {
                    ipoint_2 = snappingResult.Location;
                    this.bool_0 = true;
                    if (this.ianchorPoint_0 != null)
                    {
                        this.ianchorPoint_0.MoveTo(snappingResult.Location, this.iactiveView_0.ScreenDisplay);
                    }
                    else
                    {
                        this.method_1(snappingResult.Location, esriSimpleMarkerStyle_0);
                    }
                }
            }
            else
            {
                ISimpleMarkerSymbol isimpleMarkerSymbol0 = this.isimpleMarkerSymbol_0;
                isimpleMarkerSymbol0.Style = esriSimpleMarkerStyle_0;
                if (ipoint_2 == null)
                {
                    ipoint_2 = new ESRI.ArcGIS.Geometry.Point();
                }
                ipoint_2.PutCoords(ipoint_1.X, ipoint_1.Y);
                if (this.iengineSnapEnvironment_0 is SnapEnvironment)
                {
                    if ((!ApplicationRef.AppContext.Config.UseSnap
                        ? true
                        : !(this.iengineSnapEnvironment_0 as SnapEnvironment).SnapPoint(ipoint_1, ipoint_2)))
                    {
                        this.bool_0 = false;
                        if (this.ianchorPoint_0 != null)
                        {
                            this.ianchorPoint_0.Symbol = (ISymbol) isimpleMarkerSymbol0;
                            this.ianchorPoint_0.MoveTo(ipoint_1, this.iactiveView_0.ScreenDisplay);
                        }
                        else
                        {
                            this.method_1(ipoint_1, esriSimpleMarkerStyle_0);
                        }
                    }
                    else
                    {
                        this.bool_0 = true;
                        if (this.ianchorPoint_0 != null)
                        {
                            this.ianchorPoint_0.Symbol = (ISymbol) isimpleMarkerSymbol0;
                            this.ianchorPoint_0.MoveTo(ipoint_2, this.iactiveView_0.ScreenDisplay);
                        }
                        else
                        {
                            this.method_1(ipoint_2, esriSimpleMarkerStyle_0);
                        }
                    }
                }
                else if ((this.iengineSnapEnvironment_0 == null || !ApplicationRef.AppContext.Config.UseSnap
                    ? true
                    : !this.iengineSnapEnvironment_0.SnapPoint(ipoint_2)))
                {
                    this.bool_0 = false;
                    if (this.ianchorPoint_0 != null)
                    {
                        this.ianchorPoint_0.Symbol = (ISymbol) isimpleMarkerSymbol0;
                        this.ianchorPoint_0.MoveTo(ipoint_1, this.iactiveView_0.ScreenDisplay);
                    }
                    else
                    {
                        this.method_1(ipoint_1, esriSimpleMarkerStyle_0);
                    }
                }
                else
                {
                    this.bool_0 = true;
                    if (this.ianchorPoint_0 != null)
                    {
                        this.ianchorPoint_0.Symbol = (ISymbol) isimpleMarkerSymbol0;
                        this.ianchorPoint_0.MoveTo(ipoint_2, this.iactiveView_0.ScreenDisplay);
                    }
                    else
                    {
                        this.method_1(ipoint_2, esriSimpleMarkerStyle_0);
                    }
                }
            }
            return this.bool_0;
        }
    }
}