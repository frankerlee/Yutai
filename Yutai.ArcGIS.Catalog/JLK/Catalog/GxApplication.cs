namespace JLK.Catalog
{
    using JLK.Utility;
    using System;

    public class GxApplication : ApplicationBase, IApplication, IGxApplication
    {
        private IGxCatalog igxCatalog_0 = null;
        private IGxObject igxObject_0;
        private IGxSelection igxSelection_0 = new JLK.Catalog.GxSelection();

        public GxApplication()
        {
            if (this.igxSelection_0 is IGxSelectionEvents)
            {
                (this.igxSelection_0 as IGxSelectionEvents).OnSelectionChanged += new OnSelectionChangedEventHandler(this.method_0);
            }
        }

        private void method_0(IGxSelection igxSelection_1, object object_0)
        {
            if (this.igxSelection_0 == igxSelection_1)
            {
                this.igxObject_0 = igxSelection_1.FirstObject;
            }
        }

        public IGxCatalog GxCatalog
        {
            get
            {
                return this.igxCatalog_0;
            }
            set
            {
                this.igxCatalog_0 = value;
                this.GxSelection = this.igxCatalog_0.Selection;
            }
        }

        public IGxObject GxObject
        {
            get
            {
                return this.igxObject_0;
            }
            set
            {
                this.igxObject_0 = value;
            }
        }

        public IGxSelection GxSelection
        {
            get
            {
                return this.igxSelection_0;
            }
            set
            {
                if (this.igxSelection_0 is IGxSelectionEvents)
                {
                    (this.igxSelection_0 as IGxSelectionEvents).OnSelectionChanged -= new OnSelectionChangedEventHandler(this.method_0);
                }
                this.igxSelection_0 = value;
                if (this.igxSelection_0 is IGxSelectionEvents)
                {
                    (this.igxSelection_0 as IGxSelectionEvents).OnSelectionChanged += new OnSelectionChangedEventHandler(this.method_0);
                }
            }
        }
    }
}

