namespace Yutai.ArcGIS.Common.DXF
{
	public struct Line
	{
		public int x1;

		public int y1;

		public int x2;

		public int y2;

		public Line(int X1, int Y1, int X2, int Y2)
		{
			this.x1 = X1;
			this.y1 = Y1;
			this.x2 = X2;
			this.y2 = Y2;
		}
	}
}