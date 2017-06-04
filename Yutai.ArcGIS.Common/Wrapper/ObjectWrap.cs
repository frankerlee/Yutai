using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.Wrapper
{
	public class ObjectWrap
	{
		protected object m_pobject = null;

		public object Object
		{
			get
			{
				return this.m_pobject;
			}
			set
			{
				this.m_pobject = null;
			}
		}

		public ObjectWrap(object object_0)
		{
			this.m_pobject = object_0;
		}

		public override string ToString()
		{
			string result;
			if (this.m_pobject is ILayer)
			{
				result = (this.m_pobject as ILayer).Name;
			}
			else if (this.m_pobject is IDataset)
			{
				result = (this.m_pobject as IDataset).BrowseName;
			}
			else if (this.m_pobject is ITable)
			{
				result = (this.m_pobject as IDataset).Name;
			}
			else if (this.m_pobject is IField)
			{
				result = (this.m_pobject as IField).AliasName;
			}
			else if (this.m_pobject is ISpatialReference)
			{
				result = (this.m_pobject as ISpatialReference).Name;
			}
			else
			{
				result = "";
			}
			return result;
		}
	}
}
