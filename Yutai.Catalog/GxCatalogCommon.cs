using System;
using System.Windows.Forms;

namespace Yutai.Catalog
{
	public class GxCatalogCommon
	{
		public GxCatalogCommon()
		{
		}

		public static void ConnectGDB(IGxDatabase igxDatabase_0)
		{
			if (igxDatabase_0 != null && !igxDatabase_0.IsConnected)
			{
				Cursor.Current = Cursors.WaitCursor;
				igxDatabase_0.Connect();
				IGxCatalog catalog = GxCatalogCommon.GetCatalog(igxDatabase_0 as IGxObject);
				catalog.ObjectChanged(igxDatabase_0 as IGxObject);
				catalog.ObjectRefreshed(igxDatabase_0 as IGxObject);
				Cursor.Current = Cursors.Default;
			}
		}

		public static IGxCatalog GetCatalog(IGxObject igxObject_0)
		{
			IGxCatalog igxObject0;
			if (!(igxObject_0 is IGxCatalog))
			{
				IGxObject parent = igxObject_0.Parent;
				while (parent != null)
				{
					if (parent is IGxCatalog)
					{
						igxObject0 = parent as IGxCatalog;
						return igxObject0;
					}
					else
					{
						parent = parent.Parent;
					}
				}
				igxObject0 = null;
			}
			else
			{
				igxObject0 = igxObject_0 as IGxCatalog;
			}
			return igxObject0;
		}
	}
}