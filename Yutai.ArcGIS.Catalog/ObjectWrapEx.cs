using Yutai.ArcGIS.Common.Wrapper;

namespace Yutai.ArcGIS.Catalog
{
    public class ObjectWrapEx : ObjectWrap
    {
        public ObjectWrapEx(object object_0) : base(object_0)
        {
        }

        public override string ToString()
        {
            if (base.m_pobject is IGxObject)
            {
                return (base.m_pobject as IGxObject).FullName;
            }
            return base.ToString();
        }
    }
}