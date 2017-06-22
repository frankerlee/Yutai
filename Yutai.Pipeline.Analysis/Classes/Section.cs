using System.Collections;
using ESRI.ArcGIS.Geometry;
using Yutai.PipeConfig;
using Yutai.Plugins.Interfaces;
using Point = System.Drawing.Point;

namespace Yutai.Pipeline.Analysis.Classes
{
    public class Section
    {
        public enum Section_Action
        {
            Section_Select,
            Section_Pan,
            Section_ZoomIn,
            Section_ZoomOut
        }

        public IAppContext m_context;

        public double m_dMinX;

        public double m_dMaxX;

        public double m_dMinY;

        public double m_dMaxY;

        private IPipeConfig ipipeConfig_0;

        private Section.Section_Action section_Action_0;

        public IPolyline m_pBaseLine;

        public Point m_PointDown;

        public Point m_PointUp;

        public int m_nSelectIndex;

        public SectionDisp m_pSectionDisp;

        public IPipeConfig PipeConfig
        {
            get
            {
                return this.ipipeConfig_0;
            }
            set
            {
                this.ipipeConfig_0 = value;
            }
        }

        public Section.Section_Action ActionType
        {
            get
            {
                return this.section_Action_0;
            }
            set
            {
                this.section_Action_0 = value;
            }
        }

        public IPolyline BaseLine
        {
            get
            {
                return this.m_pBaseLine;
            }
            set
            {
                this.m_pBaseLine = value;
                this.GetSelectedData();
            }
        }

        public Section(object objFrom, IAppContext pApp)
        {
            this.m_context = pApp;
            this.PipeConfig = pApp.PipeConfig;
            this.m_nSelectIndex = -1;
            this.m_pSectionDisp = new SectionDisp(objFrom);
            this.m_pSectionDisp.PipeConfig = pApp.PipeConfig;
        }

        public void OnResize(int nMenuH, int nToolBarH, float a, float b)
        {
            this.m_pSectionDisp.OnResize(nMenuH, nToolBarH, a, b);
        }

        public void MouseDown(Point ptVal)
        {
            this.m_PointDown = ptVal;
        }

        public virtual void SectionInfo(out ArrayList pArrInfo)
        {
            pArrInfo = new ArrayList();
        }

        public void MouseUp(Point ptVal)
        {
            this.m_PointUp = ptVal;
            switch (this.section_Action_0)
            {
                case Section.Section_Action.Section_Select:
                    this.m_nSelectIndex = this.m_pSectionDisp.GetSelectIndex(ptVal);
                    break;
                case Section.Section_Action.Section_Pan:
                    this.m_pSectionDisp.Pan(this.m_PointDown, this.m_PointUp);
                    break;
                case Section.Section_Action.Section_ZoomIn:
                    this.m_pSectionDisp.ZoomIn(ptVal);
                    break;
                case Section.Section_Action.Section_ZoomOut:
                    this.m_pSectionDisp.ZoomOut(ptVal);
                    break;
            }
        }

        public virtual void GetSelectedData()
        {
        }

        public void ZoomIn()
        {
            this.section_Action_0 = Section.Section_Action.Section_ZoomIn;
        }

        public void ZoomOut()
        {
            this.section_Action_0 = Section.Section_Action.Section_ZoomOut;
        }

        public void Select()
        {
            this.section_Action_0 = Section.Section_Action.Section_Select;
        }

        public void Pan()
        {
            this.section_Action_0 = Section.Section_Action.Section_Pan;
        }

        public void ViewEntire()
        {
            this.m_pSectionDisp.ViewEntire();
        }

        public virtual void Paint()
        {
            this.m_pSectionDisp.Paint();
        }

        public virtual void PrintPage(object objPrint)
        {
            this.m_pSectionDisp.PrintPage(objPrint);
        }

        public virtual void SaveSectionFile(string strFileName)
        {
        }

        public virtual void OpenSectionFile(string strFileName)
        {
        }

        public void method_0()
        {
            this.m_pSectionDisp.method_37();
        }
    }
}