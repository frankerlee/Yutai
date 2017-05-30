using System;

namespace Yutai.Catalog
{
	public interface IGxVCTLayerObject
	{
		string LayerTypeName
		{
			get;
		}

		object VCTLayer
		{
			get;
			set;
		}
	}
}