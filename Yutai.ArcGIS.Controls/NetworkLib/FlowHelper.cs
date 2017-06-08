using System;
using System.Collections;
using System.Reflection;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.NetworkAnalysis;

namespace Yutai.ArcGIS.Controls.NetworkLib
{
    public class FlowHelper
    {
        private IList m_angle = new ArrayList();
        private IList m_eFlowDirection = new ArrayList();
        private IPointCollection m_pPointcol = null;

        public void ClearFlow()
        {
            this.m_eFlowDirection.Clear();
            this.m_angle.Clear();
            this.m_pPointcol.RemovePoints(0, this.m_pPointcol.PointCount);
        }

        public void GetFlow(IMap pMap)
        {
            int num4;
            this.m_pPointcol = new MultipointClass();
            this.m_eFlowDirection.Clear();
            this.m_angle.Clear();
            INetwork network = NetworkAnalyst.m_pAnalystGN.Network;
            IUtilityNetwork network2 = network as IUtilityNetwork;
            IEnumNetEID netEIDs = network.CreateNetBrowser(esriElementType.esriETEdge);
            IEnumNetEID teid2 = network.CreateNetBrowser(esriElementType.esriETJunction);
            netEIDs.Reset();
            int count = netEIDs.Count;
            int num2 = teid2.Count;
            IEIDHelper helper = new EIDHelperClass {
                GeometricNetwork = NetworkAnalyst.m_pAnalystGN,
                OutputSpatialReference = pMap.SpatialReference,
                ReturnGeometries = true,
                ReturnFeatures = true
            };
            IEnumEIDInfo info = helper.CreateEnumEIDInfo(teid2);
            int num3 = info.Count;
            teid2.Reset();
            info.Reset();
            IPointCollection points = new MultipointClass();
            object before = Missing.Value;
            for (num4 = 0; num4 < num2; num4++)
            {
                IPoint inPoint = info.Next().Geometry as IPoint;
                points.AddPoint(inPoint, ref before, ref before);
            }
            info = null;
            info = helper.CreateEnumEIDInfo(netEIDs);
            num3 = info.Count;
            netEIDs.Reset();
            info.Reset();
            IList list = new ArrayList();
            ILine line = new LineClass();
            IList list2 = new ArrayList();
            int num5 = 0;
            for (num4 = 0; num4 < count; num4++)
            {
                int edgeEID = netEIDs.Next();
                this.m_eFlowDirection.Add(network2.GetFlowDirection(edgeEID));
                IEIDInfo info2 = info.Next();
                IGeometry geometry = info2.Geometry;
                IEdgeFeature feature = info2.Feature as IEdgeFeature;
                IPointCollection points2 = geometry as IPointCollection;
                int pointCount = points2.PointCount;
                IPoint other = new PointClass();
                IPoint[] pointArray = new IPoint[pointCount];
                IRelationalOperator @operator = points as IRelationalOperator;
                IPointCollection points3 = new MultipointClass();
                int num8 = 0;
                for (int i = 0; i < pointCount; i++)
                {
                    other = points2.get_Point(i);
                    if (@operator.Contains(other))
                    {
                        IPoint point3 = points2.get_Point(i);
                        points3.AddPoint(point3, ref before, ref before);
                        pointArray[i] = point3;
                    }
                    else
                    {
                        pointArray[i] = null;
                    }
                }
                IPoint[] pointArray2 = new IPoint[pointCount];
                num8 = points3.PointCount;
                int index = -1;
                if (num5 < (points3.PointCount - 1))
                {
                    for (int j = 0; j < pointCount; j++)
                    {
                        if (pointArray[j] != null)
                        {
                            index++;
                        }
                        if (index == num5)
                        {
                            index = j;
                            break;
                        }
                    }
                    IPoint point4 = new PointClass();
                    pointArray2[index] = pointArray[index];
                    pointArray2[index + 1] = points2.get_Point(index + 1);
                    point4.X = (pointArray2[index].X + pointArray2[index + 1].X) / 2.0;
                    point4.Y = (pointArray2[index].Y + pointArray2[index + 1].Y) / 2.0;
                    this.m_pPointcol.AddPoint(point4, ref before, ref before);
                    line.FromPoint = pointArray2[index];
                    line.ToPoint = pointArray2[index + 1];
                    this.m_angle.Add(line.Angle);
                    num5++;
                    if (num5 == (num8 - 1))
                    {
                        num5 = 0;
                    }
                }
            }
            this.ShowFlow(pMap as IActiveView);
        }

        public void ShowFlow(IActiveView ipAV)
        {
            if (this.m_pPointcol != null)
            {
                double num = 4.0 * Math.Atan(1.0);
                IScreenDisplay screenDisplay = ipAV.ScreenDisplay;
                screenDisplay.StartDrawing(0, 0);
                IMarkerSymbol determinateFolwArrow = NetworkAnalyst.m_pFlowSymbol.DeterminateFolwArrow as IMarkerSymbol;
                for (int i = 0; i < this.m_pPointcol.PointCount; i++)
                {
                    esriFlowDirection direction = (esriFlowDirection) this.m_eFlowDirection[i];
                    if (direction == esriFlowDirection.esriFDWithFlow)
                    {
                        determinateFolwArrow.Angle = (180.0 * ((double) this.m_angle[i])) / num;
                        screenDisplay.SetSymbol(determinateFolwArrow as ISymbol);
                    }
                    else if (direction == esriFlowDirection.esriFDAgainstFlow)
                    {
                        determinateFolwArrow.Angle = ((180.0 * ((double) this.m_angle[i])) / num) + 180.0;
                        screenDisplay.SetSymbol(determinateFolwArrow as ISymbol);
                    }
                    else if (direction == esriFlowDirection.esriFDIndeterminate)
                    {
                        screenDisplay.SetSymbol(NetworkAnalyst.m_pFlowSymbol.IndeterminateFolwArrow);
                    }
                    else
                    {
                        screenDisplay.SetSymbol(NetworkAnalyst.m_pFlowSymbol.UninitializedFolwArrow);
                    }
                    IPoint point = this.m_pPointcol.get_Point(i);
                    screenDisplay.DrawPoint(point);
                }
                screenDisplay.FinishDrawing();
            }
        }
    }
}

