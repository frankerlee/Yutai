using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Common.Wrapper
{
	public class FieldObject2
	{
		private IField ifield_0 = null;

		public IField Field
		{
			get
			{
				return this.ifield_0;
			}
		}

		public FieldObject2(IField ifield_1)
		{
			this.ifield_0 = ifield_1;
		}

		public override string ToString()
		{
			return this.ifield_0.AliasName;
		}
	}
}
