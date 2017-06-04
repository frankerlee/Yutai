using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Shared;

namespace Yutai.ArcGIS.Common.Editor.Helpers
{
	public class TopologyError
	{
		private ITopologyLayer itopologyLayer_0 = null;

		private ITopologyErrorFeature itopologyErrorFeature_0 = null;

		public ITopologyErrorFeature TopologyErrorFeature
		{
			get
			{
				return this.itopologyErrorFeature_0;
			}
		}

		public ITopologyLayer TopologyLayer
		{
			get
			{
				return this.itopologyLayer_0;
			}
		}

		public TopologyError(ITopologyLayer itopologyLayer_1, ITopologyErrorFeature itopologyErrorFeature_1)
		{
			this.itopologyLayer_0 = itopologyLayer_1;
			this.itopologyErrorFeature_0 = itopologyErrorFeature_1;
		}

		public void Draw(IScreenDisplay iscreenDisplay_0)
		{
			iscreenDisplay_0.StartDrawing(0, -1);
			switch ((this.itopologyErrorFeature_0 as IFeature).Shape.GeometryType)
			{
				case esriGeometryType.esriGeometryPoint:
				{
					this.method_0(iscreenDisplay_0, (this.itopologyErrorFeature_0 as IFeature).Shape);
					break;
				}
				case esriGeometryType.esriGeometryMultipoint:
				{
					this.method_1(iscreenDisplay_0, (this.itopologyErrorFeature_0 as IFeature).Shape);
					break;
				}
				case esriGeometryType.esriGeometryPolyline:
				{
					this.method_2(iscreenDisplay_0, (this.itopologyErrorFeature_0 as IFeature).Shape);
					break;
				}
				case esriGeometryType.esriGeometryPolygon:
				{
					this.method_3(iscreenDisplay_0, (this.itopologyErrorFeature_0 as IFeature).Shape);
					break;
				}
			}
			iscreenDisplay_0.FinishDrawing();
		}

		private void method_0(IScreenDisplay iscreenDisplay_0, IGeometry igeometry_0)
		{
			iscreenDisplay_0.SetSymbol((ISymbol)(new SimpleMarkerSymbol()
			{
				Style = esriSimpleMarkerStyle.esriSMSCircle
			}));
			iscreenDisplay_0.DrawPoint(igeometry_0);
		}

		private void method_1(IScreenDisplay iscreenDisplay_0, IGeometry igeometry_0)
		{
			try
			{
				ISimpleMarkerSymbol simpleMarkerSymbolClass = new SimpleMarkerSymbol()
				{
					Style = esriSimpleMarkerStyle.esriSMSCircle
				};
				(new RgbColor()).Green = 128;
				ISymbol symbol = (ISymbol)simpleMarkerSymbolClass;
				for (int i = 0; i < (igeometry_0 as IPointCollection).PointCount; i++)
				{
					iscreenDisplay_0.SetSymbol(symbol);
					iscreenDisplay_0.DrawPoint((igeometry_0 as IPointCollection).Point[i]);
				}
			}
			catch (Exception exception)
			{
				Logger.Current.Error("",exception, "");
			}
		}

		private void method_2(IScreenDisplay iscreenDisplay_0, IGeometry igeometry_0)
		{
			iscreenDisplay_0.SetSymbol((ISymbol)(new SimpleLineSymbol()
			{
				Width = 4
			}));
			iscreenDisplay_0.DrawPolyline(igeometry_0);
		}

		private void method_3(IScreenDisplay iscreenDisplay_0, IGeometry igeometry_0)
		{
			ISimpleFillSymbol simpleFillSymbolClass = new SimpleFillSymbol()
			{
				Style = esriSimpleFillStyle.esriSFSNull,
				Outline = new SimpleLineSymbol()
				{
					Width = 2
				}
			};
			iscreenDisplay_0.SetSymbol((ISymbol)simpleFillSymbolClass);
			iscreenDisplay_0.DrawPolygon(igeometry_0);
		}
	}
}