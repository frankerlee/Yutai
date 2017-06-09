using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal class EditObjectClassHelper
    {
        private IObjectClass iobjectClass_0 = null;

        public bool Apply()
        {
            return true;
        }

        public IObjectClass ObjectClass
        {
            get
            {
                return this.iobjectClass_0;
            }
            set
            {
                this.iobjectClass_0 = value;
            }
        }
    }
}

