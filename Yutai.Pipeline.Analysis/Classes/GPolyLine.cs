namespace Yutai.Pipeline.Analysis.Classes
{
    public class GPolyLine : GPoints
    {
        public GPoints GetInterPtsToPolyLine(GPolyLine plObj)
        {
            GPoints gPoints = new GPoints();
            long num = (long) this.m_vtPoints.Count;
            long num2 = (long) plObj.Size();
            GPoints result;
            if (num < 2L || num2 < 2L)
            {
                result = gPoints;
            }
            else
            {
                CLinesect cLinesect = new CLinesect();
                CLinesect cLinesect2 = new CLinesect();
                int num3 = 1;
                while ((long) num3 < num)
                {
                    cLinesect.SetFirstPoint((GPoint) this.m_vtPoints[num3 - 1]);
                    cLinesect.SetSecondPoint((GPoint) this.m_vtPoints[num3]);
                    int num4 = 1;
                    while ((long) num4 < num2)
                    {
                        cLinesect2.SetFirstPoint(plObj[num4 - 1]);
                        cLinesect2.SetSecondPoint(plObj[num4]);
                        GPoint gPoint = new GPoint();
                        if (cLinesect.GetInterSectPointofTwoLinesect(cLinesect2, ref gPoint) != 0L)
                        {
                            double lenToPt = cLinesect.GetLenToPt(gPoint);
                            double lenToPt2 = cLinesect2.GetLenToPt(gPoint);
                            if (lenToPt + lenToPt2 < 0.001)
                            {
                                gPoints.PushBack(gPoint);
                            }
                        }
                        num4++;
                    }
                    num3++;
                }
                gPoints.Size();
                result = gPoints;
            }
            return result;
        }

        public GPoints GetInterPtsToPolyLineWithHeight(GPolyLine plObj)
        {
            GPoints gPoints = new GPoints();
            long num = (long) this.m_vtPoints.Count;
            long num2 = (long) plObj.Size();
            GPoints result;
            if (num < 2L || num2 < 2L)
            {
                result = gPoints;
            }
            else
            {
                CLinesect cLinesect = new CLinesect();
                CLinesect cLinesect2 = new CLinesect();
                int num3 = 1;
                while ((long) num3 < num)
                {
                    cLinesect.SetFirstPoint((GPoint) this.m_vtPoints[num3 - 1]);
                    cLinesect.SetSecondPoint((GPoint) this.m_vtPoints[num3]);
                    int num4 = 1;
                    while ((long) num4 < num2)
                    {
                        cLinesect2.SetFirstPoint(plObj[num4 - 1]);
                        cLinesect2.SetSecondPoint(plObj[num4]);
                        GPoint gPoint = new GPoint();
                        if (cLinesect.GetInterSectPointofTwoLinesectWithHeight(cLinesect2, ref gPoint) != 0L)
                        {
                            double lenToPt = cLinesect.GetLenToPt(gPoint);
                            double lenToPt2 = cLinesect2.GetLenToPt(gPoint);
                            if (lenToPt + lenToPt2 < 0.001)
                            {
                                gPoints.PushBack(gPoint);
                            }
                        }
                        num4++;
                    }
                    num3++;
                }
                gPoints.Size();
                result = gPoints;
            }
            return result;
        }

        public GPoints GetInterPtsToPolyLineWithHeightForTransect(GPolyLine plObj)
        {
            GPoints gPoints = new GPoints();
            long num = (long) this.m_vtPoints.Count;
            long num2 = (long) plObj.Size();
            GPoints result;
            if (num < 2L || num2 < 2L)
            {
                result = gPoints;
            }
            else
            {
                CLinesect cLinesect = new CLinesect();
                CLinesect cLinesect2 = new CLinesect();
                int num3 = 1;
                while ((long) num3 < num)
                {
                    cLinesect.SetFirstPoint((GPoint) this.m_vtPoints[num3 - 1]);
                    cLinesect.SetSecondPoint((GPoint) this.m_vtPoints[num3]);
                    int num4 = 1;
                    while ((long) num4 < num2)
                    {
                        cLinesect2.SetFirstPoint(plObj[num4 - 1]);
                        cLinesect2.SetSecondPoint(plObj[num4]);
                        GPoint gPoint = new GPoint();
                        if (cLinesect.GetInterSectPointofTwoLinesectWithHeightForTranSect(cLinesect2, ref gPoint) != 0L)
                        {
                            double lenToPt = cLinesect.GetLenToPt(gPoint);
                            double lenToPt2 = cLinesect2.GetLenToPt(gPoint);
                            if (lenToPt + lenToPt2 < 0.001)
                            {
                                gPoints.PushBack(gPoint);
                            }
                        }
                        num4++;
                    }
                    num3++;
                }
                gPoints.Size();
                result = gPoints;
            }
            return result;
        }

        public new GPolyLine GetDeepCopy()
        {
            GPolyLine gPolyLine = new GPolyLine();
            int count = this.m_vtPoints.Count;
            gPolyLine.Clear();
            for (int i = 0; i < count; i++)
            {
                GPoint deepCopy = ((GPoint) this.m_vtPoints[i]).GetDeepCopy();
                gPolyLine.m_vtPoints.Add(deepCopy);
            }
            return gPolyLine;
        }
    }
}