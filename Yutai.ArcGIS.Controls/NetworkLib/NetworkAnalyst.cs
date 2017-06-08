using System.Collections;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.NetworkAnalysis;
using Yutai.ArcGIS.Common.Symbol;

namespace Yutai.ArcGIS.Controls.NetworkLib
{
    public class NetworkAnalyst
    {
        public static bool ApplyEdgeFilterWeight = false;
        public static bool ApplyJuncFilterWeight = false;
        public static object[] EdgefromValues = null;
        public static object[] EdgetoValues = null;
        public static INetWeight FromToEdgeFilterWeight = null;
        public static INetWeight FromToEdgeWeight = null;
        public static object[] JuncfromValues = null;
        public static INetWeight JunctionFilterWeight = null;
        public static INetWeight JunctionWeight = null;
        public static object[] JunctoValues = null;
        private static double m_dblPathCost = 0.0;
        public static IList m_EdgeBarriers = new ArrayList();
        public static IList m_EdgeFlags = new ArrayList();
        private static IEnumNetEID m_ipEnumNetEID_Edges = null;
        private static IEnumNetEID m_ipEnumNetEID_Junctions = null;
        public static IPolyline m_ipPolyline = null;
        public static IList m_JunBarriers = new ArrayList();
        public static IList m_JunFlags = new ArrayList();
        public static IGeometricNetwork m_pAnalystGN = null;
        public static FlowSymbol m_pFlowSymbol = new FlowSymbol();
        public static double SnapTolerance = 10.0;
        public static INetWeight ToFromEdgeFilterWeight = null;
        public static INetWeight ToFromEdgeWeight = null;
        public static bool TraceIndeterminateFlow = false;

        public static void ClearResult()
        {
            m_ipPolyline = null;
            m_ipEnumNetEID_Edges = null;
            m_ipEnumNetEID_Junctions = null;
        }

        private static ITraceFlowSolver CreateTraceFlowSolver()
        {
            int num;
            INetFlag netFlag;
            ITraceFlowSolver solver = (ITraceFlowSolver) new TraceFlowSolverClass();
            INetSolver solver2 = (INetSolver) solver;
            INetwork network = m_pAnalystGN.Network;
            solver2.SourceNetwork = network;
            INetElements elements = (INetElements) network;
            if (m_JunFlags.Count > 0)
            {
                IJunctionFlag[] junctionOrigins = new IJunctionFlag[m_JunFlags.Count];
                for (num = 0; num < m_JunFlags.Count; num++)
                {
                    netFlag = (m_JunFlags[num] as NetFlagWrap).NetFlag;
                    junctionOrigins[num] = (IJunctionFlag) netFlag;
                }
                (solver as ITraceFlowSolverGEN).PutJunctionOrigins(ref junctionOrigins);
            }
            if (m_EdgeFlags.Count > 0)
            {
                IEdgeFlag[] edgeOrigins = new IEdgeFlag[m_EdgeFlags.Count];
                for (num = 0; num < m_EdgeFlags.Count; num++)
                {
                    netFlag = (m_EdgeFlags[num] as NetFlagWrap).NetFlag;
                    edgeOrigins[num] = (IEdgeFlag) netFlag;
                }
                (solver as ITraceFlowSolverGEN).PutEdgeOrigins(ref edgeOrigins);
            }
            INetSolverWeightsGEN sgen = (INetSolverWeightsGEN) solver;
            if (JunctionWeight != null)
            {
                sgen.JunctionWeight = JunctionWeight;
            }
            if (FromToEdgeWeight != null)
            {
                sgen.JunctionWeight = FromToEdgeWeight;
            }
            if (ToFromEdgeWeight != null)
            {
                sgen.JunctionWeight = ToFromEdgeWeight;
            }
            if (JunctionFilterWeight != null)
            {
                sgen.JunctionFilterWeight = JunctionFilterWeight;
            }
            if (FromToEdgeFilterWeight != null)
            {
                sgen.FromToEdgeFilterWeight = FromToEdgeFilterWeight;
            }
            if (ToFromEdgeFilterWeight != null)
            {
                sgen.ToFromEdgeFilterWeight = ToFromEdgeFilterWeight;
            }
            if (JuncfromValues != null)
            {
                sgen.SetFilterRanges(esriElementType.esriETJunction, ref JuncfromValues, ref JunctoValues);
                sgen.SetFilterType(esriElementType.esriETJunction, esriWeightFilterType.esriWFRange, ApplyJuncFilterWeight);
            }
            else
            {
                sgen.SetFilterType(esriElementType.esriETJunction, esriWeightFilterType.esriWFNone, ApplyJuncFilterWeight);
            }
            if (EdgefromValues != null)
            {
                sgen.SetFilterRanges(esriElementType.esriETEdge, ref EdgefromValues, ref EdgetoValues);
                sgen.SetFilterType(esriElementType.esriETEdge, esriWeightFilterType.esriWFRange, ApplyJuncFilterWeight);
            }
            else
            {
                sgen.SetFilterType(esriElementType.esriETEdge, esriWeightFilterType.esriWFNone, ApplyEdgeFilterWeight);
            }
            INetElementBarriers netElementBarriers = GetNetElementBarriers(esriElementType.esriETJunction);
            if (netElementBarriers != null)
            {
                (solver as INetSolver).set_ElementBarriers(esriElementType.esriETJunction, netElementBarriers);
            }
            netElementBarriers = GetNetElementBarriers(esriElementType.esriETEdge);
            if (netElementBarriers != null)
            {
                (solver as INetSolver).set_ElementBarriers(esriElementType.esriETEdge, netElementBarriers);
            }
            return solver;
        }

        public static void DrawBarriers(IScreenDisplay pDisplay)
        {
            if ((m_JunBarriers.Count + m_EdgeBarriers.Count) != 0)
            {
                int num;
                ISimpleMarkerSymbol symbol = new SimpleMarkerSymbolClass {
                    Style = esriSimpleMarkerStyle.esriSMSX,
                    Size = 8.0,
                    Color = ColorManage.Red
                };
                pDisplay.StartDrawing(0, -1);
                pDisplay.SetSymbol(symbol as ISymbol);
                for (num = 0; num < m_JunBarriers.Count; num++)
                {
                    pDisplay.DrawPoint((m_JunBarriers[num] as NetFlagWrap).Location);
                }
                for (num = 0; num < m_EdgeBarriers.Count; num++)
                {
                    pDisplay.DrawPoint((m_EdgeBarriers[num] as NetFlagWrap).Location);
                }
                pDisplay.FinishDrawing();
            }
        }

        public static void DrawFlag(IScreenDisplay pDisplay)
        {
            if ((m_JunFlags.Count + m_EdgeFlags.Count) != 0)
            {
                int num;
                ISimpleMarkerSymbol symbol = new SimpleMarkerSymbolClass {
                    Style = esriSimpleMarkerStyle.esriSMSSquare,
                    Size = 8.0,
                    Color = ColorManage.Green
                };
                pDisplay.StartDrawing(0, -1);
                pDisplay.SetSymbol(symbol as ISymbol);
                for (num = 0; num < m_JunFlags.Count; num++)
                {
                    pDisplay.DrawPoint((m_JunFlags[num] as NetFlagWrap).Location);
                }
                for (num = 0; num < m_EdgeFlags.Count; num++)
                {
                    pDisplay.DrawPoint((m_EdgeFlags[num] as NetFlagWrap).Location);
                }
                pDisplay.FinishDrawing();
            }
        }

        public static void DrawPolyline(IScreenDisplay pDisplay)
        {
            if (m_ipPolyline == null)
            {
                if ((m_ipEnumNetEID_Junctions != null) && (m_ipEnumNetEID_Junctions.Count == 1))
                {
                    IEIDHelper helper = new EIDHelperClass {
                        GeometricNetwork = m_pAnalystGN,
                        ReturnGeometries = true
                    };
                    IEnumEIDInfo info = helper.CreateEnumEIDInfo(m_ipEnumNetEID_Junctions);
                    info.Reset();
                    IGeometry point = info.Next().Geometry;
                    ISimpleMarkerSymbol symbol = new SimpleMarkerSymbolClass {
                        Style = esriSimpleMarkerStyle.esriSMSCircle,
                        Size = 2.0,
                        Color = ColorManage.Red
                    };
                    pDisplay.StartDrawing(0, -1);
                    pDisplay.SetSymbol(symbol as ISymbol);
                    pDisplay.DrawPoint(point);
                    pDisplay.FinishDrawing();
                }
            }
            else
            {
                ISimpleLineSymbol symbol2 = new SimpleLineSymbolClass {
                    Style = esriSimpleLineStyle.esriSLSSolid,
                    Width = 2.0,
                    Color = ColorManage.Red
                };
                pDisplay.StartDrawing(0, -1);
                pDisplay.SetSymbol(symbol2 as ISymbol);
                pDisplay.DrawPolyline(m_ipPolyline);
                pDisplay.FinishDrawing();
            }
        }

        public static int EdgeFeatCount(ISimpleJunctionFeature pSJF)
        {
            IRow row = null;
            IRow row2 = null;
            int num = 0;
            int edgeFeatureCount = 0;
            edgeFeatureCount = pSJF.EdgeFeatureCount;
            num = edgeFeatureCount;
            for (int i = 0; i < (edgeFeatureCount - 1); i++)
            {
                for (int j = i + 1; j < edgeFeatureCount; j++)
                {
                    row = pSJF.get_EdgeFeature(i) as IRow;
                    row2 = pSJF.get_EdgeFeature(j) as IRow;
                    if (row.OID == row2.OID)
                    {
                        num--;
                    }
                }
            }
            return num;
        }

        public static bool FindCommonAncestors(out string str)
        {
            str = "";
            m_ipPolyline = null;
            (CreateTraceFlowSolver() as ITraceFlowSolverGEN).FindCommonAncestors(esriFlowElements.esriFEJunctionsAndEdges, out m_ipEnumNetEID_Junctions, out m_ipEnumNetEID_Edges);
            m_ipPolyline = PathPolyLine;
            return true;
        }

        public static bool FindConnectedFeature()
        {
            m_ipPolyline = null;
            (CreateTraceFlowSolver() as ITraceFlowSolverGEN).FindFlowElements(esriFlowMethod.esriFMConnected, esriFlowElements.esriFEJunctionsAndEdges, out m_ipEnumNetEID_Junctions, out m_ipEnumNetEID_Edges);
            m_ipPolyline = PathPolyLine;
            return true;
        }

        public static bool FindLoop()
        {
            m_ipPolyline = null;
            (CreateTraceFlowSolver() as ITraceFlowSolverGEN).FindCircuits(esriFlowElements.esriFEJunctionsAndEdges, out m_ipEnumNetEID_Junctions, out m_ipEnumNetEID_Edges);
            m_ipPolyline = PathPolyLine;
            return true;
        }

        public static bool FindUnConnectdeFeature()
        {
            m_ipPolyline = null;
            (CreateTraceFlowSolver() as ITraceFlowSolverGEN).FindFlowUnreachedElements(esriFlowMethod.esriFMConnected, esriFlowElements.esriFEJunctionsAndEdges, out m_ipEnumNetEID_Junctions, out m_ipEnumNetEID_Edges);
            m_ipPolyline = PathPolyLine;
            return true;
        }

        public static bool FindUpStreamAccumulation()
        {
            object obj2;
            m_ipPolyline = null;
            (CreateTraceFlowSolver() as ITraceFlowSolverGEN).FindAccumulation(esriFlowMethod.esriFMUpstream, esriFlowElements.esriFEJunctionsAndEdges, out m_ipEnumNetEID_Junctions, out m_ipEnumNetEID_Edges, out obj2);
            m_ipPolyline = PathPolyLine;
            return true;
        }

        public static bool FindUpstreamToSource()
        {
            object[] segmentCosts = new object[m_JunFlags.Count + m_EdgeFlags.Count];
            m_ipPolyline = null;
            (CreateTraceFlowSolver() as ITraceFlowSolverGEN).FindSource(esriFlowMethod.esriFMConnected, esriShortestPathObjFn.esriSPObjFnMinSum, out m_ipEnumNetEID_Junctions, out m_ipEnumNetEID_Edges, segmentCosts.Length, ref segmentCosts);
            m_dblPathCost = 0.0;
            for (int i = 0; i < segmentCosts.Length; i++)
            {
                m_dblPathCost += (double) segmentCosts[i];
            }
            m_ipPolyline = PathPolyLine;
            return true;
        }

        public static INetElementBarriers GetNetElementBarriers(esriElementType type)
        {
            INetElementBarriersGEN sgen = null;
            IList junBarriers = null;
            int num2;
            if (type == esriElementType.esriETJunction)
            {
                if (m_JunBarriers.Count == 0)
                {
                    return null;
                }
                junBarriers = m_JunBarriers;
            }
            else if (type == esriElementType.esriETEdge)
            {
                if (m_EdgeBarriers.Count == 0)
                {
                    return null;
                }
                junBarriers = m_EdgeBarriers;
            }
            if (junBarriers == null)
            {
                return null;
            }
            sgen = new NetElementBarriersClass {
                ElementType = type,
                Network = m_pAnalystGN.Network
            };
            IList list2 = new ArrayList();
            IList list3 = new ArrayList();
            IList list4 = null;
            for (num2 = 0; num2 < junBarriers.Count; num2++)
            {
                NetFlagWrap wrap = junBarriers[num2] as NetFlagWrap;
                int userClassID = wrap.NetFlag.UserClassID;
                int index = list2.IndexOf(userClassID);
                if (index == -1)
                {
                    list2.Add(userClassID);
                    list4 = new ArrayList();
                    list4.Add(wrap.NetFlag.UserID);
                    list3.Add(list4);
                }
                else
                {
                    list4 = list3[index] as IList;
                    list4.Add(wrap.NetFlag.UserID);
                }
            }
            for (num2 = 0; num2 < list2.Count; num2++)
            {
                list4 = list3[num2] as IList;
                int[] array = new int[list4.Count];
                list4.CopyTo(array, 0);
                sgen.SetBarriers((int) list2[num2], ref array);
            }
            return (sgen as INetElementBarriers);
        }

        public static bool HasResult()
        {
            return ((m_ipPolyline != null) || ((m_ipEnumNetEID_Edges != null) || (m_ipEnumNetEID_Junctions != null)));
        }

        public static bool TrackDownStream(out string str)
        {
            str = "";
            object[] segmentCosts = new object[m_JunFlags.Count + m_EdgeFlags.Count];
            m_ipPolyline = null;
            (CreateTraceFlowSolver() as ITraceFlowSolverGEN).FindSource(esriFlowMethod.esriFMDownstream, esriShortestPathObjFn.esriSPObjFnMinSum, out m_ipEnumNetEID_Junctions, out m_ipEnumNetEID_Edges, segmentCosts.Length, ref segmentCosts);
            m_dblPathCost = 0.0;
            for (int i = 0; i < segmentCosts.Length; i++)
            {
                m_dblPathCost += (double) segmentCosts[i];
            }
            m_ipPolyline = PathPolyLine;
            str = m_dblPathCost.ToString();
            return true;
        }

        public static bool TrackUpStream(out string str)
        {
            str = "";
            object[] segmentCosts = new object[m_JunFlags.Count + m_EdgeFlags.Count];
            m_ipPolyline = null;
            (CreateTraceFlowSolver() as ITraceFlowSolverGEN).FindSource(esriFlowMethod.esriFMUpstream, esriShortestPathObjFn.esriSPObjFnMinSum, out m_ipEnumNetEID_Junctions, out m_ipEnumNetEID_Edges, segmentCosts.Length, ref segmentCosts);
            m_dblPathCost = 0.0;
            for (int i = 0; i < segmentCosts.Length; i++)
            {
                m_dblPathCost += (double) segmentCosts[i];
            }
            m_ipPolyline = PathPolyLine;
            str = m_dblPathCost.ToString();
            return true;
        }

        public static IPolyline PathPolyLine
        {
            get
            {
                if ((m_ipEnumNetEID_Edges.Count > 0) && (m_ipPolyline == null))
                {
                    m_ipPolyline = new PolylineClass();
                    IGeometryCollection ipPolyline = (IGeometryCollection) m_ipPolyline;
                    if (m_ipEnumNetEID_Edges != null)
                    {
                        IEIDHelper helper = new EIDHelperClass {
                            GeometricNetwork = m_pAnalystGN,
                            ReturnGeometries = true
                        };
                        IEnumEIDInfo info = helper.CreateEnumEIDInfo(m_ipEnumNetEID_Edges);
                        info.Reset();
                        for (int i = 1; i <= info.Count; i++)
                        {
                            IGeometry geometry = info.Next().Geometry;
                            ipPolyline.AddGeometryCollection((IGeometryCollection) geometry);
                        }
                    }
                }
                return m_ipPolyline;
            }
        }

        internal class BarrierFlagWrap
        {
            private INetFlag m_NetFlag = null;
            private IPoint m_pt = null;

            public BarrierFlagWrap(INetFlag NetFlag, IPoint pt)
            {
                this.m_NetFlag = NetFlag;
                this.m_pt = pt;
            }

            public IPoint Location
            {
                get
                {
                    return this.m_pt;
                }
            }

            public INetFlag NetFlag
            {
                get
                {
                    return this.m_NetFlag;
                }
            }
        }

        public class NetFlagWrap
        {
            private INetFlag m_NetFlag = null;
            private IPoint m_pt = null;

            public NetFlagWrap(INetFlag NetFlag, IPoint pt)
            {
                this.m_NetFlag = NetFlag;
                this.m_pt = pt;
            }

            public IPoint Location
            {
                get
                {
                    return this.m_pt;
                }
            }

            public INetFlag NetFlag
            {
                get
                {
                    return this.m_NetFlag;
                }
            }
        }
    }
}

