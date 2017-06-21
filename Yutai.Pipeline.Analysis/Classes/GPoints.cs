using System;
using System.Collections;

namespace Yutai.Pipeline.Analysis.Classes
{
    public class GPoints : ApplyConst
    {
        public ArrayList m_vtPoints;

        public double Length
        {
            get
            {
                int count = this.m_vtPoints.Count;
                double num = 0.0;
                for (int i = 0; i < count - 1; i++)
                {
                    GPoint gPoint = (GPoint)this.m_vtPoints[i];
                    GPoint toPoint = (GPoint)this.m_vtPoints[i + 1];
                    float num2 = (float)gPoint.DistanceToPt(toPoint);
                    num += (double)num2;
                }
                return num;
            }
        }

        public GPoint this[int index]
        {
            get
            {
                if (index < 0)
                {
                }
                return (GPoint)this.m_vtPoints[index];
            }
            set
            {
                if (index < 0)
                {
                }
                this.m_vtPoints[index] = value;
            }
        }

        public GPoints()
        {
            this.m_vtPoints = new ArrayList();
        }

        public bool Empty()
        {
            return this.m_vtPoints.Count == 0;
        }

        public void PushBack(double dx, double dy)
        {
            this.m_vtPoints.Add(new GPoint(dx, dy));
        }

        public void PushBack(double dx, double dy, double dz, double dm)
        {
            this.m_vtPoints.Add(new GPoint(dx, dy, dz, dm));
        }

        public void PushBack(GPoint tPoint)
        {
            this.m_vtPoints.Add(tPoint);
        }

        public void Swap(int m, int n)
        {
            GPoint value = (GPoint)this.m_vtPoints[m];
            this.m_vtPoints[m] = this.m_vtPoints[n];
            this.m_vtPoints[n] = value;
        }

        public void Clear()
        {
            this.m_vtPoints.Clear();
        }

        public int Size()
        {
            return this.m_vtPoints.Count;
        }

        public GPoint Back()
        {
            return (GPoint)this.m_vtPoints[this.m_vtPoints.Count - 1];
        }

        public GPoint Front()
        {
            return (GPoint)this.m_vtPoints[0];
        }

        public void PopBack()
        {
            if (!this.Empty())
            {
                this.m_vtPoints.RemoveAt(this.m_vtPoints.Count - 1);
            }
        }

        public GPoints GetDeepCopy()
        {
            GPoints gPoints = new GPoints();
            int count = this.m_vtPoints.Count;
            gPoints.Clear();
            for (int i = 0; i < count; i++)
            {
                GPoint deepCopy = ((GPoint)this.m_vtPoints[i]).GetDeepCopy();
                gPoints.m_vtPoints.Add(deepCopy);
            }
            return gPoints;
        }

        public ArrayList GetDeepCopyOfVePoints()
        {
            ArrayList arrayList = new ArrayList();
            int count = this.m_vtPoints.Count;
            arrayList.Clear();
            for (int i = 0; i < count; i++)
            {
                GPoint deepCopy = ((GPoint)this.m_vtPoints[i]).GetDeepCopy();
                arrayList.Add(deepCopy);
            }
            return arrayList;
        }

        public double GetMinDistanceToPt(GPoint ptDst)
        {
            int count = this.m_vtPoints.Count;
            double num = 1.7976931348623157E+308;
            for (int i = 1; i < count; i++)
            {
                CLinesect cLinesect = new CLinesect((GPoint)this.m_vtPoints[i - 1], (GPoint)this.m_vtPoints[i]);
                double lenToPt = cLinesect.GetLenToPt(ptDst);
                num = Math.Min(num, lenToPt);
            }
            return num;
        }

        public bool IsPtOn(GPoint ptObj)
        {
            int count = this.m_vtPoints.Count;
            CLinesect cLinesect = new CLinesect();
            bool result;
            if (count == 1)
            {
                result = (ptObj == this.Front());
            }
            else
            {
                for (int i = 0; i < count - 1; i++)
                {
                    cLinesect.SetPoints((GPoint)this.m_vtPoints[i], (GPoint)this.m_vtPoints[i + 1]);
                    if (cLinesect.IsPtOnLine(ptObj))
                    {
                        result = true;
                        return result;
                    }
                }
                result = false;
            }
            return result;
        }

        public void SortByDistToPt(GPoint ptDst)
        {
            for (int i = this.m_vtPoints.Count - 1; i > 0; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    if (((GPoint)this.m_vtPoints[j]).DistanceToPt(ptDst) < ((GPoint)this.m_vtPoints[j + 1]).DistanceToPt(ptDst))
                    {
                        this.Swap(j, j + 1);
                    }
                }
            }
        }
    }
}