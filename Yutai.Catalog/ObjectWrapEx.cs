using System;

namespace Yutai.Catalog
{
    public class ObjectWrapEx : ObjectWrap
	{
		public ObjectWrapEx(object object_0) : base(object_0)
		{
		}

		public override string ToString()
		{
			string str;
			str = (!(this.m_pobject is IGxObject) ? base.ToString() : (this.m_pobject as IGxObject).FullName);
			return str;
		}
	}
}