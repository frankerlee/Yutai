using System;
using System.Collections.Generic;
using System.Reflection;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Carto.MapCartoTemplateLib;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Services;

namespace Yutai.Plugins.Printing.Views
{
    public class AutoLayoutHelper
    {
        private List<IPrintPageInfo> _pageInfos;
        private MapTemplate _mapTemplate;
        private IIndexMap _indexMap;
        private IGeometry _fence;
        private double _scale;
        private AutoLayoutType _layoutType;
        private string _searchText;

        public AutoLayoutHelper()
        {
            _pageInfos=new List<IPrintPageInfo>();
        }

        public AutoLayoutType LayoutType
        {
            get { return _layoutType; }
            set { _layoutType = value; }
        }

        public MapTemplate MapTemplate
        {
            get { return _mapTemplate; }
            set { _mapTemplate = value; }
        }

        public Double Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }

        public IIndexMap IndexMap
        {
            get { return _indexMap; }
            set { _indexMap = value; }
        }

        public string SearchText
        {
            get { return _searchText; }
            set { _searchText = value; }
        }

        public IGeometry Fence
        {
            get { return _fence; }
            set { _fence = value; }
        }

        public List<IPrintPageInfo> PageInfos
        {
            get { return _pageInfos; }
            set { _pageInfos = value; }
        }

        private bool IsValid()
        {
            if (_mapTemplate == null) return false;
            if (_scale <= 0) return false;
            if (_layoutType == AutoLayoutType.Index)
            {
                if (string.IsNullOrEmpty(_searchText) && (_fence == null || _fence.IsEmpty)) return false;
                if (_indexMap == null) return false;
            }
            else if (_layoutType == AutoLayoutType.FenceOnly)
            {
                if(_fence==null || _fence.IsEmpty) return false;
            }
            return true;
        }
        public void Execute()
        {
            if (!IsValid())
            {
                MessageService.Current.Warn("你选择的制图方式有参数未填写，请检查!");
                return;
            }
            //! 有索引图的时候比例尺就没有作用了，因为是按照索引图和模板进行的匹配

            double _realWidth = _mapTemplate.Width/100.0;
            double _realHeight = _mapTemplate.Height / 100.0;
            
            if (_layoutType == AutoLayoutType.Index)
            {
                _pageInfos=_indexMap.QueryPageInfo(_fence, _searchText);
            }
            else
            {
                if (_fence.GeometryType == esriGeometryType.esriGeometryPolyline)
                {
                    _pageInfos = CreateStripMapPageInfos(_fence as IPolyline);
                }
                else
                {
                    _pageInfos = CreateIndexPageInfos(_fence as IPolygon);
                }
               
            }
        }

        public List<IPrintPageInfo> CreateIndexPageInfos(IPolygon polygon)
        {
            List<IPrintPageInfo> pages=new List<IPrintPageInfo>();
            IEnvelope pEnvelope = polygon.Envelope;
            double width = _mapTemplate.Width*_scale/100;
            double height = _mapTemplate.Height * _scale / 100;

            double startX = Math.Floor(polygon.Envelope.XMin/width)*width;
            double startY = Math.Floor(polygon.Envelope.YMin / height) * height;

            double endX = Math.Ceiling(polygon.Envelope.XMax / width) * width;
            double endY = Math.Ceiling(polygon.Envelope.YMax / height) * height;

            double X1 = startX;
            double Y1 = startY;
            double X2 = 0;
            double Y2 = 0;
            int count = 0;

            int col = 0;
            int row = 0;

            while (Y1 <= endY - height)
            {
                col++;
                row = 0;
                X1 = startX;
                while (X1 < endX - width)
                {
                    row++;
                    X2 = X1 + width;
                    Y2 = Y1 + height;
                    IEnvelope pPageEnv=new Envelope() as IEnvelope;
                    pPageEnv.PutCoords(X1,Y1,X2,Y2);

                    //! 开始检查页面方框和多边形是否有相交，如果没有相交，该多边形就略过去了
                    ITopologicalOperator topo=polygon as ITopologicalOperator;
                    IGeometryCollection pGeoCol =
                        topo.Intersect(pPageEnv, esriGeometryDimension.esriGeometry0Dimension) as IGeometryCollection;
                    if (pGeoCol == null || pGeoCol.GeometryCount == 0)
                    {
                        X1 = X1 + width;
                        continue;
                    }
                    count++;
                    IPrintPageInfo onePage=new PrintPageInfo();
                    onePage.Boundary = pPageEnv;
                    onePage.PageID = count;
                    onePage.PageName = col.ToString() + "-" + row.ToString();
                    pages.Add(onePage);
                }
                Y1 = Y1 + height;
            }

            foreach (IPrintPageInfo pageInfo in pages)
            {
                pageInfo.TotalCount = pages.Count;
            }
            return pages;

        }


        public List<IPrintPageInfo> CreateStripMapPageInfos(IPolyline stripLine)
        {
            IPolyline pPolyline;
            IPoint pCenterPoint;
            IPolygon pCirclePoly;
            IPolygon pGridPoly;
            IConstructCircularArc pCircularArc;
            ISegmentCollection pSegmentCollection;
            ITopologicalOperator pTopoOpt;
            IGeometryCollection pGeoCol;
            IPoint pIntersectPoint;
            ICurve pArc;
            IPoint pIntersectPointPrev;
            bool bFirstRun;
            int lLoop2;
            double dHighest=0;
            int lHighestRef;
            double dHighestPrev;
            ICurve pCurve;
            ILine pLine;
            IPolyline pPLine;
            bool bContinue;
            bool bReducedRadius;
            double dGridAngle;
            double dHighestThisTurn;
            int lCounter=0;

            List<IPrintPageInfo> pages=new List<IPrintPageInfo>(); 
            pPolyline = stripLine;
            pCenterPoint = pPolyline.FromPoint;
            double m_GridWidth = _mapTemplate.Width*_scale/100;
            double m_GridHeight = _mapTemplate.Height * _scale / 100;

            double dCircleRadius;
            List<double> colIntersects=new List<double>();
            double dIntersect;

            dHighestPrev = -1;
            bFirstRun = true;
            pArc = pPolyline;
            bContinue = false;

            bool isLoop1 = true;
            object missing = Missing.Value;

            do
            {
                if (bFirstRun)
                {
                    dCircleRadius = m_GridWidth/2;
                }
                else
                {
                    dCircleRadius = m_GridWidth;
                }
                bReducedRadius = false;
                do
                {
                    //! 创建搜索圆
                    pCircularArc=new CircularArc() as IConstructCircularArc;
                    pCircularArc.ConstructCircle(pCenterPoint,dCircleRadius,false);
                    pCirclePoly=new Polygon() as IPolygon;
                    pSegmentCollection = pCirclePoly as ISegmentCollection;
                    pSegmentCollection.AddSegment(pCircularArc as ISegment,ref missing, ref missing);

                    pTopoOpt = pPolyline as ITopologicalOperator;
                    pGeoCol=new GeometryBag() as IGeometryCollection;
                    pGeoCol = pTopoOpt.Intersect(pCirclePoly, esriGeometryDimension.esriGeometry0Dimension) as IGeometryCollection;
                    if (pGeoCol.GeometryCount == 0)
                    {
                        //! 需要检查，没有交点的的时候的处理
                        return null;
                    }

                    pArc = pPolyline;
                    lHighestRef = -1;
                    dHighestThisTurn = 102;
                    for (lLoop2 = 0; lLoop2 < pGeoCol.GeometryCount; lLoop2++)
                    {
                        pIntersectPoint = pGeoCol.Geometry[lLoop2] as IPoint;
                        dIntersect = ReturnPercentageAlong(pArc, pIntersectPoint);
                        if (dIntersect > (dHighestPrev*1.001) && dIntersect < dHighestThisTurn)
                        {
                            dHighest = dIntersect;
                            dHighestThisTurn = dIntersect;
                            lHighestRef = lLoop2;
                        }
                    }

                    if (lHighestRef < 0)
                    {
                        dHighest = 101;
                        pIntersectPoint = IntersectPointExtendedTo(pPolyline, pCirclePoly);
                        pIntersectPointPrev = pCenterPoint;
                    }
                    else
                    {
                        pIntersectPoint=pGeoCol.Geometry[lHighestRef] as IPoint;
                        if (bFirstRun)
                        {
                            pIntersectPointPrev = new Point();
                            pIntersectPointPrev.PutCoords(pCenterPoint.X - (pIntersectPoint.X - pCenterPoint.X),
                                pCenterPoint.Y - (pIntersectPoint.Y - pCenterPoint.Y));
                        }
                        else
                        {
                            pIntersectPointPrev = pCenterPoint;
                        }
                    }

                    if (bReducedRadius)
                    {
                        IPolyline pTmpPLine;
                        IPolygon pTmpCPoly;
                        IPoint pTmpIntPoint;
                        pCircularArc = new CircularArc() as IConstructCircularArc;
                        if (bFirstRun)
                        {
                            pCircularArc.ConstructCircle(pCenterPoint, m_GridWidth/2.0, false);
                        }
                        else
                        {
                            pCircularArc.ConstructCircle(pCenterPoint, m_GridWidth, false);
                        }
                        pTmpCPoly = new Polygon() as IPolygon;
                        pSegmentCollection = pTmpCPoly as ISegmentCollection;
                        pSegmentCollection.AddSegment((ISegment) pCircularArc, ref missing, ref missing);
                        pTmpPLine = new Polyline() as IPolyline;
                        pTmpPLine.FromPoint = pIntersectPointPrev;
                        pTmpPLine.ToPoint = pIntersectPoint;
                        pTmpIntPoint = IntersectPointExtendedTo(pTmpPLine, pTmpCPoly);
                        CreateAngledGridPolygon(pIntersectPointPrev, pTmpIntPoint, out pGridPoly, out dGridAngle);
                    }
                    else
                    {
                        CreateAngledGridPolygon(pIntersectPointPrev, pIntersectPoint,out pGridPoly, out dGridAngle);
                    }

                    pTopoOpt = pGridPoly as ITopologicalOperator;
                    pGeoCol=new GeometryBag() as IGeometryCollection;
                    pGeoCol = pTopoOpt.Intersect(pPolyline, esriGeometryDimension.esriGeometry0Dimension) as IGeometryCollection;
                    bContinue = true;
                    if (pGeoCol.GeometryCount > 2)
                    {
                        colIntersects=new List<double>();
                        for (lLoop2 = 2; lLoop2 < pGeoCol.GeometryCount; lLoop2++)
                        {
                            colIntersects.Add(ReturnPercentageAlong(pArc, pGeoCol.Geometry[lLoop2] as IPoint));
                        }
                        for (lLoop2 = 0; lLoop2 < colIntersects.Count; lLoop2++)
                        {
                            if (colIntersects[lLoop2] > (dHighestPrev*1.001) &&
                                colIntersects[lLoop2] < (dHighestPrev*0.999))
                            {
                                bContinue = false;
                                dHighest = dHighestPrev;
                                dCircleRadius = dCircleRadius - m_GridWidth*0.1;
                                bReducedRadius = true;
                                if (dCircleRadius < 0)
                                {
                                    bContinue = true;
                                    break;
                                }
                            }
                            
                        }
                    }

                    if (bContinue && bReducedRadius)
                    {
                        double dTmpHighest;
                        pArc = pPolyline;
                        lHighestRef = -1;
                        dTmpHighest = -1;
                        for (lLoop2 = 0; lLoop2 < pGeoCol.GeometryCount; lLoop2++)
                        {
                            pIntersectPoint = pGeoCol.Geometry[lLoop2] as IPoint ;
                            dIntersect = ReturnPercentageAlong(pArc, pIntersectPoint);
                            if (dIntersect > dTmpHighest)
                            {
                                dTmpHighest = dIntersect;
                                lHighestRef = lLoop2;
                            }
                        }
                        if (lHighestRef >= 0)
                        {
                            pIntersectPoint = pGeoCol.Geometry[lHighestRef] as IPoint;
                            
                        }
                        dHighest = dTmpHighest;
                    }

                } while (bContinue);
                bFirstRun = false;
                dHighestPrev = dHighest;
                lCounter++;

                IPrintPageInfo page=new PrintPageInfo();
                page.Boundary = pGridPoly;
                page.PageID = lCounter;
                page.Angle = dGridAngle;
                page.PageName = "带状图(" + lCounter.ToString() + ")";
                page.Scale = _scale;
                pages.Add(page);

                pCenterPoint = pIntersectPoint;
            } while (dHighest < 100);

            foreach (IPrintPageInfo pageInfo in pages)
            {
                pageInfo.TotalCount = pages.Count;
            }
            return pages;
           

        }

        private void CreateAngledGridPolygon(IPoint p1, IPoint p2, out IPolygon ReturnedGrid, out double ReturnedAngleRadians)
        {
            IPointCollection pPointColl;
            IPoint pPointStart;
            IPoint pPoint;
            double dAngleInRadians;
            ILine pLine;

            double width = _mapTemplate.Width*_scale/100.0;
            double height = _mapTemplate.Height*_scale/100.0;
            pLine = new Line();
            pLine.FromPoint = p1;
            pLine.ToPoint = p2;
            dAngleInRadians = pLine.Angle;
            if (dAngleInRadians == 0)
            {
                ReturnedAngleRadians = 0;
            }
            else if (dAngleInRadians>0)
            {
                ReturnedAngleRadians = 360 - ((dAngleInRadians / Math.PI) * 180.0);
            }
            else
            {
                ReturnedAngleRadians = Math.Abs((dAngleInRadians/Math.PI)*180);
            }

            ReturnedGrid=new Polygon() as IPolygon;
            pPointColl = ReturnedGrid as IPointCollection;

            pPoint=new Point();
            pPoint.PutCoords(p1.X + (Math.Sin(dAngleInRadians)*(height/2)),
                p1.Y - (Math.Cos(dAngleInRadians)*(height/2)));
            pPointColl.AddPoint(pPoint);
            pPointStart = pPoint;
            pPoint = new Point();
            pPoint.PutCoords(p1.X - (Math.Sin(dAngleInRadians) * (height / 2)),
                     p1.Y + (Math.Cos(dAngleInRadians) * (height / 2)));
            pPointColl.AddPoint(pPoint);

            pPoint = new Point();
            pPoint.PutCoords(p2.X - Math.Sin(dAngleInRadians) * height / 2, 
                     p2.Y + Math.Cos(dAngleInRadians) * height / 2);
            pPointColl.AddPoint(pPoint);

            pPoint = new Point();
            pPoint.PutCoords(p2.X + Math.Sin(dAngleInRadians) * height / 2, 
                     p2.Y - Math.Cos(dAngleInRadians) * height / 2);
            pPointColl.AddPoint(pPoint);
            pPointColl.AddPoint(pPointStart);

        }

        IPoint IntersectPointExtendedTo(IPolyline pPolyline, IPolygon pCirclePoly)
        {
            ICurve pCurve;
            ILine pLine;
            IPolyline pPLine;
            ITopologicalOperator pTopoOpt;
            IGeometryCollection pGeoCol;

            pCurve = pPolyline;
            pLine=new Line();
            double gridWidth = _mapTemplate.Width*_scale/100.0;
            pCurve.QueryTangent(esriSegmentExtension.esriExtendTangentAtTo,1,true, gridWidth*1.1,pLine);
            pPLine=new Polyline() as IPolyline;
            pPLine.FromPoint = pLine.FromPoint;
            pPLine.ToPoint = pLine.ToPoint;
            pTopoOpt = pPLine as ITopologicalOperator;
            pGeoCol=new GeometryBag() as IGeometryCollection;
            pGeoCol =
                pTopoOpt.Intersect(pCirclePoly, esriGeometryDimension.esriGeometry0Dimension) as IGeometryCollection;
            return pGeoCol.Geometry[0] as IPoint;


        }
        private double ReturnPercentageAlong(ICurve pArc, IPoint pPoint)
        {
            int geoCount;
            double pDistAlong = 0;
            double pDist = 0;
            bool pRightSide = false;
            IPoint pOutPt;
            double compareDist;

            pOutPt=new Point();
            pArc.QueryPointAndDistance(esriSegmentExtension.esriNoExtension,pPoint,true,pOutPt, pDistAlong, pDist, pRightSide);
            return (pDistAlong*100);

        }
    }
}