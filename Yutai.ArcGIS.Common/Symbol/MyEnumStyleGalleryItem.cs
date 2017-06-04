using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Common.Symbol
{
	public class MyEnumStyleGalleryItem : IEnumStyleGalleryItem
	{
		public System.Collections.IEnumerator m_pEnumerator = null;

		public void Reset()
		{
			if (this.m_pEnumerator != null)
			{
				this.m_pEnumerator.Reset();
			}
		}

		public IStyleGalleryItem Next()
		{
			IStyleGalleryItem result;
			if (this.m_pEnumerator != null && this.m_pEnumerator.MoveNext())
			{
				result = (IStyleGalleryItem)this.m_pEnumerator.Current;
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
