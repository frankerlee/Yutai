using System;
using System.Collections.Generic;
using System.Reflection;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.Plugins.Scene.Classes
{
	internal sealed class modFacades
	{
		public static IGraphicsContainer3D g_pGCon;

		public static double m_extrusionHeight;

		public static double m_limitAngle;

		public static bool g_bFeaturePropertiesApplied;

		public static bool CreateFacades(IGraphicsContainer3D igraphicsContainer3D_0, IFeatureCursor ifeatureCursor_0, IFeature3DProperties ifeature3DProperties_0, string string_0, int int_0, ISurface isurface_0, List<clsTextureGroup> list_0, bool bool_0)
		{
			bool flag = false;
			bool result;
			try
			{
				modFacades.m_limitAngle = 0.5235987666666666;
				IFeature feature = ifeatureCursor_0.NextFeature();
				List<IElement> list = new List<IElement>();
				while (feature != null)
				{
					IRow row = feature;
					if (!bool_0)
					{
						if (int_0 < 0)
						{
							int num = row.Fields.FindField(string_0);
							if (num < 0)
							{
								modFacades.m_extrusionHeight = (double)int.Parse(BuildingProperty.BuildingHieght);
							}
							else
							{
								try
								{
									modFacades.m_extrusionHeight = -1.0;
									modFacades.m_extrusionHeight = (double)row.get_Value(num);
									goto IL_1C7;
								}
								catch
								{
									goto IL_1C7;
								}
								IL_9A:
								modFacades.m_extrusionHeight = (double)int.Parse(BuildingProperty.BuildingHieght);
								goto IL_14A;
								IL_1C7:
								if (modFacades.m_extrusionHeight < 1.0)
								{
									goto IL_9A;
								}
							}
						}
						else
						{
							modFacades.m_extrusionHeight = (double)int_0;
						}
					}
					else
					{
						if (int_0 > 0)
						{
							modFacades.m_extrusionHeight = (double)int_0;
						}
						else
						{
							int num = row.Fields.FindField(string_0);
							if (num < 0)
							{
								modFacades.m_extrusionHeight = (double)int.Parse(BuildingProperty.BuildingHieght);
							}
							else
							{
								try
								{
									modFacades.m_extrusionHeight = -1.0;
									modFacades.m_extrusionHeight = (double)row.get_Value(num);
								}
								catch
								{
								}
								if (modFacades.m_extrusionHeight < 1.0)
								{
									modFacades.m_extrusionHeight = (double)int.Parse(BuildingProperty.BuildingHieght);
								}
							}
						}
						isurface_0 = null;
					}
					IL_14A:
					clsTextureGroup clsTextureGroup_;
					if (list_0 != null)
					{
						int count = list_0.Count;
						Random random = new Random();
						int index = random.Next(count);
						clsTextureGroup_ = list_0[index];
					}
					else
					{
						clsTextureGroup_ = null;
					}
					string text = "Bulid";
					IGroupElement groupElement = modFacades.CreateFacadesFromPolygon(feature, ifeature3DProperties_0, clsTextureGroup_, isurface_0, text, null, bool_0);
					if (groupElement != null)
					{
						IElementProperties elementProperties = groupElement as IElementProperties;
						elementProperties.Name = text;
						list.Add(groupElement as IElement);
					}
					feature = ifeatureCursor_0.NextFeature();
				}
				modFacades.AddGroupElements(igraphicsContainer3D_0, list);
				flag = true;
				result = true;
				return result;
			}
			catch
			{
			}
			result = flag;
			return result;
		}

		public static IGroupElement CreateFacadesFromPolygon(IFeature ifeature_0, IFeature3DProperties ifeature3DProperties_0, clsTextureGroup clsTextureGroup_0, ISurface isurface_0, string string_0, IPolygon ipolygon_0, bool bool_0)
		{
			IGroupElement groupElement = null;
			IGroupElement result;
			try
			{
				IEncode3DProperties encode3DProperties = new GeometryEnvironment() as IEncode3DProperties;
				IGroupElement groupElement2 = new GroupElement() as IGroupElement;
				if (modFacades.m_limitAngle == 0.0)
				{
					modFacades.m_limitAngle = 0.5235987666666666;
				}
				IGeometry geometry = null;
				if (ifeature_0 != null)
				{
					if (ifeature3DProperties_0 != null && bool_0)
					{
						ifeature3DProperties_0.ApplyFeatureProperties(ifeature_0, out geometry, false);
					}
					else
					{
						geometry = ifeature_0.Shape;
					}
				}
				else if (ipolygon_0 != null)
				{
					geometry = ipolygon_0;
				}
				if (geometry == null)
				{
					result = null;
					return result;
				}
				if (!(geometry is IMultiPatch))
				{
				}
				IEnvelope envelope = geometry.Envelope;
				double num = envelope.ZMin;
				ISpatialReference spatialReference = BuildingProperty.Scene.SpatialReference;
				geometry.Project(spatialReference);
				string a = num.ToString();
				if (a.Equals("非数字"))
				{
					num = 0.0;
				}
				double num2 = 0.0;
				if (ifeature3DProperties_0 != null)
				{
					I3DProperties i3DProperties = ifeature3DProperties_0 as I3DProperties;
					if (i3DProperties.OffsetExpressionString.Length > 0)
					{
						try
						{
							num2 = Convert.ToDouble(i3DProperties.OffsetExpressionString);
						}
						catch
						{
						}
					}
					if (geometry is IPolygon)
					{
						num += num2;
					}
				}
				IGeometryCollection geometryCollection = geometry as IGeometryCollection;
				int geometryCount = geometryCollection.GeometryCount;
				int num3;
				if (geometry is IMultiPatch)
				{
					geometryCount = geometryCollection.GeometryCount;
					num3 = geometryCollection.GeometryCount * 2 / 3;
					modFacades.m_extrusionHeight = geometry.Envelope.ZMax - geometry.Envelope.ZMin;
				}
				else
				{
					num3 = 0;
					geometryCount = geometryCollection.GeometryCount;
				}
				if (modFacades.m_extrusionHeight < 1.0)
				{
					modFacades.m_extrusionHeight = 1.0;
				}
				IPoint point = null;
				double num4 = 0.0;
				double num5 = 0.0;
				double num6 = 0.0;
				object value = Missing.Value;
				for (int i = num3; i < geometryCount; i++)
				{
					IGeometry geometry2 = geometryCollection.get_Geometry(i);
					esriGeometryType geometryType = geometry2.GeometryType;
					if (geometryType == esriGeometryType.esriGeometryRing || geometryType == esriGeometryType.esriGeometryPolyline)
					{
						IPointCollection pointCollection = geometry2 as IPointCollection;
						int pointCount = pointCollection.PointCount;
						if (pointCount >= 2)
						{
							int num7 = 0;
							int num8 = 1;
							bool flag = false;
							IPointCollection pointCollection2;
							double m;
							IMultiPatch multiPatch;
							IGeometryCollection geometryCollection2;
							IZAware iZAware;
							IMAware iMAware;
							IElement element;
							while (!flag)
							{
								bool flag2 = false;
								while (num8 < pointCount && !flag2)
								{
									if (num8 - num7 == 1)
									{
										IPoint point2 = pointCollection.get_Point(num7);
										point = pointCollection.get_Point(num8);
										num5 = point.X - point2.X;
										num6 = point.Y - point2.Y;
										num4 = Math.Sqrt(num5 * num5 + num6 * num6);
										if (isurface_0 != null)
										{
											num = isurface_0.get_Z(point.X, point.Y);
											num += num2;
										}
									}
									else
									{
										IPoint point2 = point;
										double num9 = num5;
										double num10 = num6;
										point = pointCollection.get_Point(num8);
										if (isurface_0 != null)
										{
											num = isurface_0.get_Z(point.X, point.Y);
											num += num2;
										}
										num5 = point.X - point2.X;
										num6 = point.Y - point2.Y;
										double num11 = Math.Sqrt(num9 * num9 + num10 * num10);
										double num12 = Math.Sqrt(num5 * num5 + num6 * num6);
										double num13 = (num9 * num5 + num10 * num6) / (num11 * num12);
										if (num13 < Math.Cos(modFacades.m_limitAngle))
										{
											flag2 = true;
											break;
										}
										num4 += num12;
									}
									num8++;
								}
								if (flag2)
								{
									num8--;
								}
								else
								{
									num8--;
								}
								pointCollection2 = new TriangleStrip();
								double num14 = 0.0;
								for (int j = num7; j <= num8; j++)
								{
									if (j > 0)
									{
										IPoint point2 = point;
										point = pointCollection.get_Point(j);
										num5 = point.X - point2.Y;
										num6 = point.Y - point2.Y;
										num14 += Math.Sqrt(num5 * num5 + num6 * num6);
									}
									else
									{
										point = pointCollection.get_Point(j);
									}
									point.Z = num;
									m = 0.0;
									encode3DProperties.PackTexture2D(num14 / num4, 0.0, out m);
									point.M = m;
									IClone clone = point as IClone;
									pointCollection2.AddPoint(clone.Clone() as IPoint, ref value, ref value);
									point.Z = num + modFacades.m_extrusionHeight;
									m = 0.0;
									encode3DProperties.PackTexture2D(num14 / num4, -1.0, out m);
									point.M = m;
									pointCollection2.AddPoint(clone.Clone() as IPoint, ref value, ref value);
								}
								multiPatch = new MultiPatch() as IMultiPatch;
								geometryCollection2 = (multiPatch as IGeometryCollection);
								iZAware = (multiPatch as IZAware);
								iZAware.ZAware = true;
								iMAware = (multiPatch as IMAware);
								iMAware.MAware = true;
								geometryCollection2.AddGeometry(pointCollection2 as IGeometry, ref value, ref value);
								if (clsTextureGroup_0 != null)
								{
									int index = modFacades.FindTextureByAspect(clsTextureGroup_0, num4 / modFacades.m_extrusionHeight);
									element = modFacades.CreateElement(multiPatch, clsTextureGroup_0.Symbols[index], string_0);
								}
								else
								{
									element = modFacades.CreateElement(multiPatch, null, string_0);
								}
								if (element != null)
								{
									groupElement2.AddElement(element);
								}
								num7 = num8;
								num8 = num7 + 1;
								if (num7 >= pointCount - 1)
								{
									flag = true;
								}
							}
							IVector3D vector3D = new Vector3D() as IVector3D;
							vector3D.XComponent = 0.0;
							vector3D.YComponent = 0.0;
							vector3D.ZComponent = 1.0;
							m = 0.0;
							encode3DProperties.PackNormal(vector3D, out m);
							pointCollection2 = new Ring();
							for (int j = 0; j <= pointCount - 1; j++)
							{
								IPoint point3 = pointCollection.get_Point(j);
								point3.Z = num + modFacades.m_extrusionHeight;
								point3.M = 0.0;
								IClone clone = point3 as IClone;
								pointCollection2.AddPoint(clone.Clone() as IPoint, ref value, ref value);
							}
							IRing ring = pointCollection2 as IRing;
							ring.Close();
							multiPatch = new MultiPatch() as IMultiPatch;
							geometryCollection2 = (multiPatch as IGeometryCollection);
							iZAware = (multiPatch as IZAware);
							iZAware.ZAware = true;
							iMAware = (multiPatch as IMAware);
							iMAware.MAware = true;
							geometryCollection2.AddGeometry(pointCollection2 as IGeometry, ref value, ref value);
							multiPatch.PutRingType(pointCollection2 as IRing, esriMultiPatchRingType.esriMultiPatchOuterRing);
							if (clsTextureGroup_0 != null)
							{
								element = modFacades.CreateElement(multiPatch, clsTextureGroup_0.RoofSymbol, string_0 + ";ROOFCOLOR=" + clsTextureGroup_0.RoofColorRGB.ToString());
							}
							else
							{
								element = modFacades.CreateElement(multiPatch, null, string_0 + ";ROOFCOLOR=1");
							}
							if (element != null)
							{
								groupElement2.AddElement(element);
							}
						}
					}
				}
				if (groupElement2 != null)
				{
					IElementProperties elementProperties = groupElement2 as IElementProperties;
					elementProperties.Name = string_0;
				}
				groupElement = groupElement2;
				result = groupElement;
				return result;
			}
			catch
			{
			}
			result = groupElement;
			return result;
		}

		public static int FindTextureByAspect(clsTextureGroup clsTextureGroup_0, double double_0)
		{
			int num = 0;
			int result;
			try
			{
				double num2 = -1.0;
				short num3 = 1;
				int count = clsTextureGroup_0.Symbols.Count;
				short num4 = 1;
				while ((int)num4 <= count - 1)
				{
					double num5 = clsTextureGroup_0.AspectRatios[(int)num4];
					double num6 = Math.Abs(double_0 / num5 - 1.0);
					if (num2 == -1.0)
					{
						num2 = num6;
						num3 = num4;
					}
					else if (num6 < num2)
					{
						num2 = num6;
						num3 = num4;
					}
					num4 += 1;
				}
				num = (int)num3;
				result = num;
				return result;
			}
			catch
			{
			}
			result = num;
			return result;
		}

		public static IElement CreateElement(IGeometry igeometry_0, ISymbol isymbol_0, string string_0)
		{
			IElement element = null;
			IElement result;
			try
			{
				if (igeometry_0.IsEmpty)
				{
					result = element;
					return result;
				}
				IElement element2 = null;
				esriGeometryType geometryType = igeometry_0.GeometryType;
				switch (geometryType)
				{
				case esriGeometryType.esriGeometryPoint:
					element2 = new MarkerElement();
					if (isymbol_0 != null)
					{
						IMarkerElement markerElement = element2 as IMarkerElement;
						markerElement.Symbol = (isymbol_0 as IMarkerSymbol);
					}
					break;
				case esriGeometryType.esriGeometryMultipoint:
					break;
				case esriGeometryType.esriGeometryPolyline:
					element2 = new LineElement();
					if (isymbol_0 != null)
					{
						ILineElement lineElement = element2 as ILineElement;
						lineElement.Symbol = (isymbol_0 as ILineSymbol);
					}
					break;
				case esriGeometryType.esriGeometryPolygon:
					element2 = new PolygonElement();
					if (isymbol_0 != null)
					{
						IFillShapeElement fillShapeElement = element2 as IFillShapeElement;
						fillShapeElement.Symbol = (isymbol_0 as IFillSymbol);
					}
					break;
				default:
					if (geometryType == esriGeometryType.esriGeometryMultiPatch)
					{
						element2 = new MultiPatchElement();
						if (isymbol_0 != null)
						{
							IFillShapeElement fillShapeElement = element2 as IFillShapeElement;
							fillShapeElement.Symbol = (isymbol_0 as IFillSymbol);
						}
					}
					break;
				}
				if (string_0.Length > 0)
				{
					IElementProperties elementProperties = element2 as IElementProperties;
					elementProperties.Name = string_0;
				}
				element2.Geometry = igeometry_0;
				element = element2;
				result = element;
				return result;
			}
			catch
			{
			}
			result = element;
			return result;
		}

		public static bool IsNaN(object object_0)
		{
			return object_0.ToString() == "1.#QNAN" || object_0.ToString() == "1,#QNAN";
		}

		public static void InitTextures(List<clsTextureGroup> list_0, bool bool_0)
		{
			try
			{
				foreach (clsTextureGroup current in list_0)
				{
					clsTextureGroup clsTextureGroup = current;
					short num = 0;
					while ((int)num < clsTextureGroup.TexturePaths.Count)
					{
						bool flag = clsTextureGroup.Symbols.Count < (int)(num + 1) || clsTextureGroup.AspectRatios.Count < (int)(num + 1) || clsTextureGroup.SymbolIsDirty.Count < (int)(num + 1) || (bool_0 && clsTextureGroup.SymbolIsDirty[(int)num]) || !bool_0 || clsTextureGroup.SymbolIsDirty[(int)num];
						if (flag)
						{
							IPictureFillSymbol pictureFillSymbol = new PictureFillSymbol();
							if (clsTextureGroup.TexturePaths[(int)num].IndexOf('[') == 0)
							{
								string text = clsTextureGroup.TexturePaths[(int)num].Substring(1, clsTextureGroup.TexturePaths[(int)num].Length - 2);
								text = System.Windows.Forms.Application.StartupPath + "\\" + text + ".bmp";
								pictureFillSymbol.CreateFillSymbolFromFile(esriIPictureType.esriIPictureBitmap, text);
							}
							else
							{
								pictureFillSymbol.CreateFillSymbolFromFile(esriIPictureType.esriIPictureBitmap, clsTextureGroup.TexturePaths[(int)num]);
							}
							clsTextureGroup.Symbols.Add(pictureFillSymbol as ISymbol);
							int height = pictureFillSymbol.Picture.Height;
							int width = pictureFillSymbol.Picture.Width;
							double item = (double)height / (double)width;
							clsTextureGroup.AspectRatios.Add(item);
						}
						num += 1;
					}
					clsTextureGroup.RoofSymbol = (new SimpleFillSymbol
					{
						Color = new RgbColor
						{
							RGB = clsTextureGroup.RoofColorRGB
						}
					} as ISymbol);
					short num2 = 0;
					while ((int)num2 < clsTextureGroup.Symbols.Count)
					{
						clsTextureGroup.SymbolIsDirty.Add(false);
						num2 += 1;
					}
				}
			}
			catch
			{
			}
		}

		public static void AddGroupElements(IGraphicsContainer3D igraphicsContainer3D_0, List<IElement> list_0)
		{
			try
			{
				int graphicsLayerIndex = BuildingProperty.GetGraphicsLayerIndex(igraphicsContainer3D_0);
				for (int i = 0; i < list_0.Count; i++)
				{
					IGroupElement groupElement = list_0[i] as IGroupElement;
					igraphicsContainer3D_0.AddElement(groupElement as IElement);
					BuildingProperty.AddElement(graphicsLayerIndex, groupElement as IElement);
				}
			}
			catch
			{
			}
		}

		public static void AddGroupElementsToBasicGraphicsLayer(List<IElement> list_0)
		{
			try
			{
				IGraphicsContainer3D graphicsContainer3D = null;
				for (int i = 0; i < list_0.Count; i++)
				{
					IGroupElement groupElement = list_0[i] as IGroupElement;
					graphicsContainer3D.AddElement(groupElement as IElement);
				}
			}
			catch
			{
			}
		}

		public static IElement AddGraphic(IGeometry igeometry_0, ISymbol isymbol_0, string string_0, IGraphicsContainer3D igraphicsContainer3D_0)
		{
			IElement element = null;
			IElement result;
			try
			{
				if (igeometry_0.IsEmpty)
				{
					result = element;
					return result;
				}
				if (modFacades.g_pGCon == null)
				{
					result = element;
					return result;
				}
				IElement element2 = null;
				esriGeometryType geometryType = igeometry_0.GeometryType;
				switch (geometryType)
				{
				case esriGeometryType.esriGeometryPoint:
					element2 = new MarkerElement();
					if (isymbol_0 != null)
					{
						IMarkerElement markerElement = element2 as IMarkerElement;
						markerElement.Symbol = (isymbol_0 as IMarkerSymbol);
					}
					break;
				case esriGeometryType.esriGeometryMultipoint:
					break;
				case esriGeometryType.esriGeometryPolyline:
					element2 = new LineElement();
					if (isymbol_0 != null)
					{
						ILineElement lineElement = element2 as ILineElement;
						lineElement.Symbol = (isymbol_0 as ILineSymbol);
					}
					break;
				case esriGeometryType.esriGeometryPolygon:
					element2 = new PolygonElement();
					if (isymbol_0 != null)
					{
						IFillShapeElement fillShapeElement = element2 as IFillShapeElement;
						fillShapeElement.Symbol = (isymbol_0 as IFillSymbol);
					}
					break;
				default:
					if (geometryType == esriGeometryType.esriGeometryMultiPatch)
					{
						element2 = new MultiPatchElement();
						if (isymbol_0 != null)
						{
							IFillShapeElement fillShapeElement = element2 as IFillShapeElement;
							fillShapeElement.Symbol = (isymbol_0 as IFillSymbol);
						}
					}
					break;
				}
				if (string_0.Length > 0)
				{
					IElementProperties elementProperties = element2 as IElementProperties;
					elementProperties.Name = string_0;
				}
				element2.Geometry = igeometry_0;
				if (igraphicsContainer3D_0 != null)
				{
					igraphicsContainer3D_0.AddElement(element2);
				}
				element = element2;
				result = element;
				return result;
			}
			catch
			{
			}
			result = element;
			return result;
		}

		public static IGroupElement CreateRoadSurfaceFromPolygon(IFeature ifeature_0, IFeature3DProperties ifeature3DProperties_0, clsTextureGroup clsTextureGroup_0, ISurface isurface_0, string string_0, IPolygon ipolygon_0, bool bool_0)
		{
			IGroupElement groupElement = null;
			IGroupElement result;
			try
			{
				IEncode3DProperties encode3DProperties = new GeometryEnvironment() as IEncode3DProperties;
				IGroupElement groupElement2 = new GroupElement() as IGroupElement;
				if (modFacades.m_limitAngle == 0.0)
				{
					modFacades.m_limitAngle = 0.5235987666666666;
				}
				IGeometry geometry = null;
				if (ifeature_0 != null)
				{
					if (ifeature3DProperties_0 != null && bool_0)
					{
						ifeature3DProperties_0.ApplyFeatureProperties(ifeature_0, out geometry, false);
						modFacades.g_bFeaturePropertiesApplied = true;
					}
					else
					{
						geometry = ifeature_0.Shape;
						modFacades.g_bFeaturePropertiesApplied = false;
					}
				}
				else if (ipolygon_0 != null)
				{
					geometry = ipolygon_0;
				}
				if (geometry == null)
				{
					result = groupElement;
					return result;
				}
				if (!(geometry is IMultiPatch))
				{
				}
				IEnvelope envelope = geometry.Envelope;
				double num = envelope.ZMin;
				if (modFacades.IsNaN(num))
				{
					num = 0.0;
				}
				double num2 = 0.0;
				I3DProperties i3DProperties = ifeature3DProperties_0 as I3DProperties;
				if (i3DProperties.OffsetExpressionString.Length > 0)
				{
					num2 = Convert.ToDouble(i3DProperties.OffsetExpressionString);
				}
				if (geometry is IPolygon)
				{
					num += num2;
				}
				IGeometryCollection geometryCollection = geometry as IGeometryCollection;
				int geometryCount = geometryCollection.GeometryCount;
				int num3;
				if (geometry is IMultiPatch)
				{
					geometryCount = geometryCollection.GeometryCount;
					num3 = geometryCollection.GeometryCount * 2 / 3;
					modFacades.m_extrusionHeight = geometry.Envelope.ZMax - geometry.Envelope.ZMin;
				}
				else
				{
					num3 = 0;
					geometryCount = geometryCollection.GeometryCount;
				}
				if (modFacades.m_extrusionHeight >= 1.0)
				{
				}
				for (int i = num3; i <= geometryCount - 1; i++)
				{
					IGeometry geometry2 = geometryCollection.get_Geometry(i);
					esriGeometryType geometryType = geometry2.GeometryType;
					if (geometryType == esriGeometryType.esriGeometryRing || geometryType == esriGeometryType.esriGeometryPolyline)
					{
					}
					IPointCollection pointCollection = geometry2 as IPointCollection;
					int pointCount = pointCollection.PointCount;
					if (pointCount >= 2)
					{
					}
					IVector3D vector3D = new Vector3D() as IVector3D;
					vector3D.XComponent = 0.0;
					vector3D.YComponent = 0.0;
					vector3D.ZComponent = 1.0;
					double m = 0.0;
					encode3DProperties.PackNormal(vector3D, out m);
					IPointCollection pointCollection2 = new Ring();
					object value = Missing.Value;
					short num4 = 0;
					while ((int)num4 <= pointCount - 1)
					{
						IPoint point = pointCollection.get_Point((int)num4);
						point.Z = num;
						point.M = m;
						IClone clone = point as IClone;
						pointCollection2.AddPoint(clone.Clone() as IPoint, ref value, ref value);
						num4 += 1;
					}
					IRing ring = pointCollection2 as IRing;
					ring.Close();
					IMultiPatch multiPatch = new MultiPatch() as IMultiPatch;
					IGeometryCollection geometryCollection2 = multiPatch as IGeometryCollection;
					IZAware iZAware = multiPatch as IZAware;
					iZAware.ZAware = true;
					IMAware iMAware = multiPatch as IMAware;
					iMAware.MAware = true;
					geometryCollection2.AddGeometry(pointCollection2 as IGeometry, ref value, ref value);
					multiPatch.PutRingType(pointCollection2 as IRing, esriMultiPatchRingType.esriMultiPatchOuterRing);
					IElement element = modFacades.CreateElement(multiPatch, clsTextureGroup_0.RoofSymbol, string_0 + ";ROOFCOLOR=" + clsTextureGroup_0.RoofColorRGB.ToString());
					if (element != null)
					{
						groupElement2.AddElement(element);
					}
				}
				if (groupElement2 != null)
				{
					IElementProperties elementProperties = groupElement2 as IElementProperties;
					elementProperties.Name = string_0;
				}
				groupElement = groupElement2;
				result = groupElement;
				return result;
			}
			catch
			{
			}
			result = groupElement;
			return result;
		}

		public static bool CreateRoadSurface(IFeatureCursor ifeatureCursor_0, IFeature3DProperties ifeature3DProperties_0, string string_0, int int_0, ISurface isurface_0, List<clsTextureGroup> list_0, bool bool_0)
		{
			bool flag = false;
			bool result;
			try
			{
				modFacades.InitTextures(list_0, true);
				modFacades.m_limitAngle = 0.5235987666666666;
				IFeature feature = ifeatureCursor_0.NextFeature();
				List<IElement> list = new List<IElement>();
				while (feature != null)
				{
					IRow row = feature;
					if (!bool_0)
					{
						if (int_0 < 0)
						{
							int num = row.Fields.FindField(string_0);
							if (num < 0)
							{
								modFacades.m_extrusionHeight = 100.0;
							}
							else
							{
								modFacades.m_extrusionHeight = (double)row.get_Value(num);
							}
						}
						else
						{
							modFacades.m_extrusionHeight = (double)int_0;
						}
					}
					else
					{
						if (int_0 > 0)
						{
							modFacades.m_extrusionHeight = (double)int_0;
						}
						isurface_0 = null;
					}
					int count = list_0.Count;
					Random random = new Random();
					int index = (int)((double)count * random.NextDouble() + 1.0);
					clsTextureGroup clsTextureGroup_ = list_0[index];
					string text = "2";
					IGroupElement groupElement = modFacades.CreateRoadSurfaceFromPolygon(feature, ifeature3DProperties_0, clsTextureGroup_, isurface_0, text, null, bool_0);
					if (groupElement != null)
					{
						IElementProperties elementProperties = groupElement as IElementProperties;
						elementProperties.Name = text;
						list.Add(groupElement as IElement);
					}
					feature = ifeatureCursor_0.NextFeature();
				}
				modFacades.AddGroupElements(null, list);
				flag = true;
				result = true;
				return result;
			}
			catch
			{
			}
			result = flag;
			return result;
		}
	}
}
