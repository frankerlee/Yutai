using System;

namespace Yutai.Catalog
{
	public interface IGxShortcut
	{
		IGxObject Target
		{
			get;
			set;
		}

		string TargetLocation
		{
			get;
			set;
		}
	}
}