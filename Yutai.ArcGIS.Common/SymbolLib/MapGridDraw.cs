using System.Drawing;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Common.SymbolLib
{
    public class MapGridDraw : StyleDraw
    {
        public MapGridDraw(IMapGrid imapGrid_0) : base(imapGrid_0)
        {
        }

        public override void Draw(int int_0, Rectangle rectangle_0, double double_0, double double_1)
        {
            tagRECT tagRECT = default(tagRECT);
            tagRECT.left = rectangle_0.Left;
            tagRECT.right = rectangle_0.Right;
            tagRECT.top = rectangle_0.Top;
            tagRECT.bottom = rectangle_0.Bottom;
            IMapFrame mapFrame = new MapFrame() as IMapFrame;
            IMap map = new Map();
            mapFrame.Map = map;
            (this.m_pStyle as IMapGrid).PrepareForOutput(int_0, 96, ref tagRECT, mapFrame);
        }
    }
}