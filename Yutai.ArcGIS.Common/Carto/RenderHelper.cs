using System;
using System.Diagnostics;
using System.IO;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.Carto
{
	public class RenderHelper
	{
		public RenderHelper()
		{
		}

		public static void ApplyRenderer(IGeoFeatureLayer igeoFeatureLayer_0, string string_0)
		{
			igeoFeatureLayer_0.Renderer = RenderHelper.ReadRender(string_0);
		}

		public static IColor CreateRandomColor()
		{
			Random random = new Random((int)DateTime.Now.Ticks);
			IRgbColor rgbColorClass = new RgbColor()
			{
				Red = (int)(255 * random.NextDouble()),
				Green = (int)(255 * random.NextDouble()),
				Blue = (int)(255 * random.NextDouble())
			};
			return rgbColorClass;
		}

		public static ISymbol CreateSymbol(esriGeometryType esriGeometryType_0)
		{
			ISymbol simpleMarkerSymbolClass = null;
			switch (esriGeometryType_0)
			{
				case esriGeometryType.esriGeometryPoint:
				case esriGeometryType.esriGeometryMultipoint:
				{
					simpleMarkerSymbolClass = new SimpleMarkerSymbol() as ISymbol;
					(simpleMarkerSymbolClass as IMarkerSymbol).Color = RenderHelper.CreateRandomColor();
					break;
				}
				case esriGeometryType.esriGeometryPolyline:
				{
					simpleMarkerSymbolClass = new SimpleLineSymbol() as ISymbol;
					(simpleMarkerSymbolClass as ILineSymbol).Color = RenderHelper.CreateRandomColor();
					break;
				}
				case esriGeometryType.esriGeometryPolygon:
				{
					simpleMarkerSymbolClass = new SimpleFillSymbol() as ISymbol;
					(simpleMarkerSymbolClass as IFillSymbol).Color = RenderHelper.CreateRandomColor();
					break;
				}
			}
			return simpleMarkerSymbolClass;
		}

		public static IRasterClassifyColorRampRenderer RasterClassifyRenderer(IRasterLayer irasterLayer_0)
		{
			bool flag;
			double @break;
			string str;
			IRasterClassifyColorRampRenderer rasterClassifyColorRampRenderer;
			try
			{
				IRasterClassifyColorRampRenderer rasterClassifyColorRampRendererClass = new RasterClassifyColorRampRenderer();
				IRasterRenderer raster = (IRasterRenderer)rasterClassifyColorRampRendererClass;
				rasterClassifyColorRampRendererClass.ClassField = "<VALUE>";
				rasterClassifyColorRampRendererClass.NormField = "<NONE>";
				raster.Raster = irasterLayer_0.Raster;
				rasterClassifyColorRampRendererClass.ClassCount = 3;
				raster.Update();
				IAlgorithmicColorRamp algorithmicColorRampClass = new AlgorithmicColorRamp()
				{
					Size = 3
				};
				algorithmicColorRampClass.CreateRamp(out flag);
				IFillSymbol simpleFillSymbolClass = new SimpleFillSymbol();
				for (int i = 0; i < rasterClassifyColorRampRendererClass.ClassCount; i++)
				{
					simpleFillSymbolClass.Color = algorithmicColorRampClass.Color[i];
					rasterClassifyColorRampRendererClass.Symbol[i] = (ISymbol)simpleFillSymbolClass;
					if (i != rasterClassifyColorRampRendererClass.ClassCount - 1)
					{
						@break = rasterClassifyColorRampRendererClass.Break[i];
						string str1 = @break.ToString("0.####");
						@break = rasterClassifyColorRampRendererClass.Break[i + 1];
						str = string.Concat(str1, " - ", @break.ToString("0.####"));
					}
					else
					{
						@break = rasterClassifyColorRampRendererClass.Break[i];
						str = @break.ToString("0.####");
					}
					rasterClassifyColorRampRendererClass.set_Label(i, str);
				}
				rasterClassifyColorRampRenderer = raster as IRasterClassifyColorRampRenderer;
			}
			catch (Exception exception)
			{
				Debug.WriteLine(exception.Message);
				rasterClassifyColorRampRenderer = null;
			}
			return rasterClassifyColorRampRenderer;
		}

		public static IRasterRGBRenderer RasterRGBRenderer(IRasterLayer irasterLayer_0)
		{
			IRasterRGBRenderer rasterRGBRenderer;
			try
			{
				IRasterRGBRenderer rasterRGBRendererClass = new RasterRGBRenderer();
				IRasterRenderer raster = (IRasterRenderer)rasterRGBRendererClass;
				raster.Raster = irasterLayer_0.Raster;
				rasterRGBRendererClass.RedBandIndex = 0;
				rasterRGBRendererClass.GreenBandIndex = 1;
				rasterRGBRendererClass.BlueBandIndex = 2;
				raster.Update();
				rasterRGBRenderer = raster as IRasterRGBRenderer;
			}
			catch (Exception exception)
			{
				Debug.WriteLine(exception.Message);
				rasterRGBRenderer = null;
			}
			return rasterRGBRenderer;
		}

		public static IRasterStretchColorRampRenderer RasterStretchRenderer(IRasterLayer irasterLayer_0)
		{
			bool flag;
			IRasterStretchColorRampRenderer rasterStretchColorRampRenderer;
			try
			{
				IRasterStretchColorRampRenderer rasterStretchColorRampRendererClass = new RasterStretchColorRampRenderer();
				IRasterRenderer raster = (IRasterRenderer)rasterStretchColorRampRendererClass;
				raster.Raster = irasterLayer_0.Raster;
				raster.Update();
				rasterStretchColorRampRendererClass.BandIndex = 0;
				IAlgorithmicColorRamp algorithmicColorRampClass = new AlgorithmicColorRamp();
				IColor rgbColorClass = new RgbColor();
				(rgbColorClass as IRgbColor).Red = 255;
				(rgbColorClass as IRgbColor).Red = 0;
				(rgbColorClass as IRgbColor).Green = 0;
				(rgbColorClass as IRgbColor).Blue = 0;
				IColor color = new RgbColor();
				(color as IRgbColor).Red = 255;
				(color as IRgbColor).Green = 255;
				(color as IRgbColor).Blue = 255;
				algorithmicColorRampClass.Size = 255;
				algorithmicColorRampClass.FromColor = rgbColorClass;
				algorithmicColorRampClass.ToColor = color;
				algorithmicColorRampClass.CreateRamp(out flag);
				rasterStretchColorRampRendererClass.ColorRamp = algorithmicColorRampClass;
				raster.Update();
				rasterStretchColorRampRenderer = raster as IRasterStretchColorRampRenderer;
			}
			catch (Exception exception)
			{
				Debug.WriteLine(exception.Message);
				rasterStretchColorRampRenderer = null;
			}
			return rasterStretchColorRampRenderer;
		}

		public static IFeatureRenderer ReadRender(string string_0)
		{
			BinaryReader binaryReader = new BinaryReader(new System.IO.FileStream(string_0, FileMode.Open));
			byte[] numArray = binaryReader.ReadBytes(binaryReader.ReadInt32());
			IMemoryBlobStream memoryBlobStreamClass = new MemoryBlobStream();
			IObjectStream objectStreamClass = new ObjectStream()
			{
				Stream = memoryBlobStreamClass
			};
			((IMemoryBlobStreamVariant)memoryBlobStreamClass).ImportFromVariant(numArray);
			IPropertySet propertySetClass = new PropertySet();
			(propertySetClass as IPersistStream).Load(objectStreamClass);
			return propertySetClass.GetProperty("Render") as IFeatureRenderer;
		}

		public static void SaveRender(IFeatureRenderer ifeatureRenderer_0, string string_0)
		{
			object obj;
			IMemoryBlobStream memoryBlobStreamClass = new MemoryBlobStream();
			IObjectStream objectStreamClass = new ObjectStream()
			{
				Stream = memoryBlobStreamClass
			};
			IPropertySet propertySetClass = new PropertySet();
			IPersistStream persistStream = propertySetClass as IPersistStream;
			propertySetClass.SetProperty("Render", ifeatureRenderer_0);
			persistStream.Save(objectStreamClass, 0);
			((IMemoryBlobStreamVariant)memoryBlobStreamClass).ExportToVariant(out obj);
			System.IO.FileStream fileStream = new System.IO.FileStream(string_0, FileMode.CreateNew);
			BinaryWriter binaryWriter = new BinaryWriter(fileStream);
			binaryWriter.Write((int)((byte[])obj).Length);
			binaryWriter.Write((byte[])obj);
			binaryWriter.Close();
			fileStream.Close();
		}
	}
}