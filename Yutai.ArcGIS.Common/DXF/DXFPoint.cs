using System;

namespace Yutai.ArcGIS.Common.DXF
{
	public class DXFPoint : ICloneable
	{
		public float X;

		public float Y;

		public float Z;

		public DXFPoint()
		{
		}

		public DXFPoint(DXFPoint dxfP)
		{
			this.X = dxfP.X;
			this.Y = dxfP.Y;
			this.Z = dxfP.Z;
		}

		public DXFPoint(float x, float y, float z)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		public object Clone()
		{
			return new DXFPoint(this);
		}
	}
}