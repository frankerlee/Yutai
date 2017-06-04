using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Common.SymbolLib
{
	public class StyleDrawFactory
	{
		public static IStyleDraw CreateStyleDraw(object object_0)
		{
			IStyleDraw result;
			if (object_0 is IMarkerSymbol)
			{
				result = new MarkerSymbolDraw(object_0 as ISymbol);
			}
			else if (object_0 is ILineSymbol)
			{
				result = new LineSymbolDraw(object_0 as ISymbol);
			}
			else if (object_0 is IFillSymbol)
			{
				result = new FillSymbolDraw(object_0 as ISymbol);
			}
			else if (object_0 is IMapSurround)
			{
				result = new MapSurroundDraw(object_0 as IMapSurround);
			}
			else if (object_0 is IColorRamp)
			{
				result = new ColorRampDraw(object_0 as IColorRamp);
			}
			else if (object_0 is IColor)
			{
				result = new ColorDraw(object_0 as IColor);
			}
			else if (object_0 is IBorder)
			{
				result = new BorderDraw(object_0 as IBorder);
			}
			else if (object_0 is IBackground)
			{
				result = new BackgroundDraw(object_0 as IBackground);
			}
			else if (object_0 is IShadow)
			{
				result = new ShadowDraw(object_0 as IShadow);
			}
			else if (object_0 is ILinePatch)
			{
				result = new LinePatchDraw(object_0 as ILinePatch);
			}
			else if (object_0 is IAreaPatch)
			{
				result = new AreaPatchDraw(object_0 as IAreaPatch);
			}
			else if (object_0 is ITextSymbol)
			{
				result = new TextSymbolDraw(object_0 as ISymbol);
			}
			else if (object_0 is ILegendItem)
			{
				result = new LegendItemDraw(object_0 as ILegendItem);
			}
			else if (object_0 is ILabelStyle2)
			{
				result = new LabelStyleDraw(object_0 as ILabelStyle2);
			}
			else if (object_0 is IMapGrid)
			{
				result = new MapGridDraw(object_0 as IMapGrid);
			}
			else if (object_0 is IRepresentationMarker)
			{
				result = new RepresentationMarkerDraw(object_0 as IRepresentationMarker);
			}
			else if (object_0 is IRepresentationRuleItem)
			{
				result = new RepresentationRuleDraw(object_0);
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
