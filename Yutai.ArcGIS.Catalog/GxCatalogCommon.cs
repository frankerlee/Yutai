using System.Windows.Forms;

namespace Yutai.ArcGIS.Catalog
{
    public class GxCatalogCommon
    {
        public static void ConnectGDB(IGxDatabase igxDatabase_0)
        {
            if ((igxDatabase_0 != null) && !igxDatabase_0.IsConnected)
            {
                Cursor.Current = Cursors.WaitCursor;
                igxDatabase_0.Connect();
                IGxCatalog catalog = GetCatalog(igxDatabase_0 as IGxObject);
                catalog.ObjectChanged(igxDatabase_0 as IGxObject);
                catalog.ObjectRefreshed(igxDatabase_0 as IGxObject);
                Cursor.Current = Cursors.Default;
            }
        }

        public static IGxCatalog GetCatalog(IGxObject igxObject_0)
        {
            if (igxObject_0 is IGxCatalog)
            {
                return (igxObject_0 as IGxCatalog);
            }
            for (IGxObject obj2 = igxObject_0.Parent; obj2 != null; obj2 = obj2.Parent)
            {
                if (obj2 is IGxCatalog)
                {
                    return (obj2 as IGxCatalog);
                }
            }
            return null;
        }
    }
}