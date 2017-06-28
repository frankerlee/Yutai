using System.Collections;
using ESRI.ArcGIS.Geometry;

using Yutai.Pipeline.Config.Interfaces;
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

        private IPipelineConfig m_PipeConfig;

        private Section.Section_Action _sectionAction;

        public IPolyline m_pBaseLine;

        public Point m_PointDown;

        public Point m_PointUp;

        public int m_nSelectIndex;

        public SectionDisp m_pSectionDisp;

        private bool _isMUsing;

        public IPipelineConfig PipeConfig
        {
            get
            {
                return this.m_PipeConfig;
            }
            set
            {
                this.m_PipeConfig = value;
            }
        }

        public bool IsMUsing
        {
            get { return _isMUsing; }
            set { _isMUsing = value; }
        }
        public Section.Section_Action ActionType
        {
            get
            {
                return this._sectionAction;
            }
            set
            {
                this._sectionAction = value;
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

        public Section(object objFrom, IAppContext pApp,IPipelineConfig pipeConfig)
        {
            this.m_context = pApp;
            this.PipeConfig = pipeConfig;
            this.m_nSelectIndex = -1;
            this.m_pSectionDisp = new SectionDisp(objFrom);
            this.m_pSectionDisp.PipeConfig = pipeConfig;
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
            switch (this._sectionAction)
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
            this._sectionAction = Section.Section_Action.Section_ZoomIn;
        }

        public void ZoomOut()
        {
            this._sectionAction = Section.Section_Action.Section_ZoomOut;
        }

        public void Select()
        {
            this._sectionAction = Section.Section_Action.Section_Select;
        }

        public void Pan()
        {
            this._sectionAction = Section.Section_Action.Section_Pan;
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