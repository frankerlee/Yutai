using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.Library
{
    public class MxDocument : IMxDocument
    {
        private IMapAndPageLayoutCtrlForm imapAndPageLayoutCtrlForm_0 = null;

        public IActiveView ActiveView
        {
            get
            {
                return this.imapAndPageLayoutCtrlForm_0.ActiveView;
            }
            set
            {
                this.imapAndPageLayoutCtrlForm_0.ActiveView = value;
            }
        }

        public IMap FocusMap
        {
            get
            {
                return this.imapAndPageLayoutCtrlForm_0.FocusMap;
            }
            set
            {
            }
        }

        public IMapAndPageLayoutCtrlForm MapAndPageLayoutCtrlForm
        {
            get
            {
                return this.imapAndPageLayoutCtrlForm_0;
            }
            set
            {
                this.imapAndPageLayoutCtrlForm_0 = value;
            }
        }

        public IPageLayout PageLayout
        {
            get
            {
                return this.imapAndPageLayoutCtrlForm_0.PageLayout;
            }
            set
            {
                this.imapAndPageLayoutCtrlForm_0.PageLayout = value;
            }
        }
    }
}

