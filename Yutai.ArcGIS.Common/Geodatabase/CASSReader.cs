using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Shared;

namespace Yutai.ArcGIS.Common.Geodatabase
{
	public class CASSReader : ESRI.ArcGIS.Geodatabase.IFeatureProgress_Event
	{
		private IFeatureWorkspace ifeatureWorkspace_0 = null;

		private IFeatureClass ifeatureClass_0 = null;

		private IFeatureClass ifeatureClass_1 = null;

		private IFeatureClass ifeatureClass_2 = null;

		private IFeatureClass ifeatureClass_3 = null;

		private IFeatureClass ifeatureClass_4 = null;

		private IFeatureClass ifeatureClass_5 = null;

		private IFeatureClass ifeatureClass_6 = null;

		private IFeatureClass ifeatureClass_7 = null;

		private IFeatureClass ifeatureClass_8 = null;

		private double double_0 = 500;

		private IEnvelope ienvelope_0 = null;

		private bool bool_0 = false;

		private string string_0 = "";

		private string string_1 = "";

		private int int_0 = 0;

		private string string_2 = "";

		private IList ilist_0 = new ArrayList();

		private IList ilist_1 = new ArrayList();

		private IList ilist_2 = new ArrayList();

		private IFeatureDataset ifeatureDataset_0 = null;

		private IList ilist_3 = new ArrayList();

		private ESRI.ArcGIS.Geodatabase.IFeatureProgress_StepEventHandler ifeatureProgress_StepEventHandler_0;

		public IFeatureDataset FeatureDataset
		{
			get
			{
				return this.ifeatureDataset_0;
			}
			set
			{
				this.ifeatureDataset_0 = value;
				if (this.ifeatureDataset_0 != null)
				{
					this.ifeatureWorkspace_0 = this.ifeatureDataset_0.Workspace as IFeatureWorkspace;
				}
			}
		}

		public bool IsInFeatureDataset
		{
			get;
			set;
		}

		public IWorkspace Workspace
		{
			set
			{
				this.ifeatureWorkspace_0 = value as IFeatureWorkspace;
				if (value.Type != esriWorkspaceType.esriRemoteDatabaseWorkspace)
				{
					this.bool_0 = false;
				}
				else
				{
					IPropertySet connectionProperties = value.ConnectionProperties;
					this.bool_0 = true;
					try
					{
						this.string_0 = connectionProperties.GetProperty("User").ToString();
					}
					catch
					{
						this.string_0 = "sde";
					}
					try
					{
						this.string_1 = connectionProperties.GetProperty("DATABASE").ToString();
					}
					catch
					{
						this.string_1 = "sde";
					}
				}
			}
		}

		public CASSReader()
		{
		}

		internal ITextElement MakeTextElement(string string_3, IPoint ipoint_0)
		{
		    ITextElement element = new TextElement() as ITextElement;
		    element.ScaleText = true;
		    element.Text = string_3;
		    ITextElement textElementClass = element as ITextElement;
			(textElementClass as IGroupSymbolElement).SymbolID = 0;
			(textElementClass as IElement).Geometry = ipoint_0;
			return textElementClass;
		}

		private IFeatureClass method_0(string string_3, string string_4)
		{
			IFeatureClass item;
			IFeatureClass featureClass = null;
			int num = 0;
			while (true)
			{
				if (num >= this.ilist_2.Count)
				{
					featureClass = this.method_3(string_3, string_4);
					if (featureClass != null)
					{
						this.ilist_2.Add(string_3);
						this.ilist_1.Add(featureClass);
					}
					item = featureClass;
					break;
				}
				else if (string_3 == this.ilist_2[num].ToString())
				{
					item = this.ilist_1[num] as IFeatureClass;
					break;
				}
				else
				{
					num++;
				}
			}
			return item;
		}

		private IFeatureClass method_1(string string_3, double double_1, ITextSymbol itextSymbol_0)
		{
			IObjectClassDescription annotationFeatureClassDescriptionClass = new AnnotationFeatureClassDescription();
			IFeatureClassDescription featureClassDescription = annotationFeatureClassDescriptionClass as IFeatureClassDescription;
			IFields field = (annotationFeatureClassDescriptionClass.RequiredFields as IClone).Clone() as IFields;
			IField field1 = field.Field[field.FindField(featureClassDescription.ShapeFieldName)];
			IGeometryDefEdit geometryDef = (field1 as IFieldEdit).GeometryDef as IGeometryDefEdit;
			ISpatialReference spatialReference = geometryDef.SpatialReference;
			spatialReference.SetDomain(this.ienvelope_0.XMin, this.ienvelope_0.XMax, this.ienvelope_0.YMin, this.ienvelope_0.YMax);
			SpatialReferenctOperator.ChangeCoordinateSystem(this.ifeatureWorkspace_0 as IGeodatabaseRelease, spatialReference, false);
			geometryDef.SpatialReference_2 = spatialReference;
			(field1 as IFieldEdit).GeometryDef_2 = geometryDef;
			IFeatureWorkspaceAnno ifeatureWorkspace0 = this.ifeatureWorkspace_0 as IFeatureWorkspaceAnno;
			IGraphicsLayerScale graphicsLayerScaleClass = new GraphicsLayerScale()
			{
				ReferenceScale = double_1,
				Units = esriUnits.esriMeters
			};
			UID instanceCLSID = annotationFeatureClassDescriptionClass.InstanceCLSID;
			UID classExtensionCLSID = annotationFeatureClassDescriptionClass.ClassExtensionCLSID;
			ISymbolCollection symbolCollectionClass = new SymbolCollection();
			symbolCollectionClass.Symbol[0] = itextSymbol_0 as ISymbol;
			IFeatureClass featureClass = null;
			featureClass = ifeatureWorkspace0.CreateAnnotationClass(string_3, field, instanceCLSID, classExtensionCLSID, featureClassDescription.ShapeFieldName, "", this.FeatureDataset, null, null, graphicsLayerScaleClass, symbolCollectionClass, true);
			return featureClass;
		}

		private void method_10(StreamReader streamReader_0, IFeatureClass ifeatureClass_9, string string_3, string string_4)
		{
			object value = Missing.Value;
			int string3 = ifeatureClass_9.Fields.FindField("Layer");
			int num = ifeatureClass_9.Fields.FindField("Code");
			int num1 = ifeatureClass_9.Fields.FindField("LineType");
			while (streamReader_0.Peek() >= 0)
			{
				string str = streamReader_0.ReadLine();
				if ((str.ToUpper() == "NIL" ? true : str.ToUpper() == "END"))
				{
					break;
				}
				if (str == "e" || !this.method_13(str, ","))
				{
					continue;
				}
				IFeature feature = ifeatureClass_9.CreateFeature();
				feature.Value[string3] = string_3;
				str = string.Concat(str, ",Continuous,Continuous");
				string str1 = this.method_7(str, 0, ",");
				string str2 = this.method_7(str, 1, ",");
				feature.Value[num] = str1;
				feature.Value[num1] = str2;
				double num2 = double.Parse(this.method_7(str, 2, ","));
				double num3 = double.Parse(this.method_7(str, 3, ","));
				double num4 = double.Parse(this.method_7(str, 4, ","));
				str = streamReader_0.ReadLine();
				if ((str.ToUpper() == "NIL" ? true : str.ToUpper() == "END"))
				{
					break;
				}
				IGeometryCollection polylineClass = new Polyline()  as IGeometryCollection;
				IPoint pointClass = new Point()
				{
					X = double.Parse(this.method_7(str, 0, ",")),
					Y = double.Parse(this.method_7(str, 1, ","))
				};
				IPoint point = new Point()
				{
					X = pointClass.X + num2 * Math.Cos(1.5707963 - num3),
					Y = pointClass.Y + num2 * Math.Sin(1.5707963 - num3)
				};
				IPoint pointClass1 = new Point()
				{
					X = pointClass.X + num2 * Math.Cos(1.5707963 - num4),
					Y = pointClass.Y + num2 * Math.Sin(1.5707963 - num4)
				};
				IConstructCircularArc circularArcClass = new CircularArc() as IConstructCircularArc;
				circularArcClass.ConstructArcDistance(pointClass, point, true, (num4 - num3) * num2);
				(polylineClass as ISegmentCollection).AddSegment(circularArcClass as ISegment, ref value, ref value);
				feature.Shape = polylineClass as IGeometry;
				while (streamReader_0.Peek() >= 0)
				{
					str = streamReader_0.ReadLine();
					if (str == "e")
					{
						break;
					}
					if ((str.ToUpper() == "NIL" ? true : str.ToUpper() == "END"))
					{
						return;
					}
				}
				feature.Store();
				if (this.ifeatureProgress_StepEventHandler_0 == null)
				{
					continue;
				}
				this.ifeatureProgress_StepEventHandler_0();
			}
		}

		private void method_11(StreamReader streamReader_0, IFeatureClass ifeatureClass_9, string string_3, string string_4)
		{
			string str;
			if (this.ifeatureClass_2 == null)
			{
				this.ifeatureClass_2 = this.method_3(string.Concat(this.string_2, "_Polygong"), "POLYGON");
			}
			object value = Missing.Value;
			int string3 = ifeatureClass_9.Fields.FindField("Layer");
			int num = ifeatureClass_9.Fields.FindField("Code");
			int num1 = ifeatureClass_9.Fields.FindField("LineType");
			while (streamReader_0.Peek() >= 0)
			{
				string str1 = streamReader_0.ReadLine();
				if ((str1.ToUpper() == "NIL" ? true : str1.ToUpper() == "END"))
				{
					break;
				}
				if (str1 == "e" || !this.method_13(str1, ","))
				{
					continue;
				}
				IFeature feature = ifeatureClass_9.CreateFeature();
				IFeature feature1 = this.ifeatureClass_2.CreateFeature();
				feature.Value[string3] = string_3;
				feature1.Value[string3] = string_3;
				str1 = string.Concat(str1, ",Continuous,Continuous");
				string str2 = this.method_7(str1, 0, ",");
				double num2 = 0;
				if (this.int_0 != 61)
				{
					str = this.method_7(str1, 1, ",");
					num2 = double.Parse(this.method_7(str1, 2, ","));
				}
				else
				{
					num2 = double.Parse(this.method_7(str1, 1, ","));
					str = this.method_7(str1, 2, ",");
				}
				feature.Value[num] = str2;
				feature.Value[num1] = str;
				feature1.Value[num] = str2;
				feature1.Value[num1] = str;
				str1 = streamReader_0.ReadLine();
				if ((str1.ToUpper() == "NIL" ? true : str1.ToUpper() == "END"))
				{
					break;
				}
				IGeometryCollection polylineClass = new Polyline() as IGeometryCollection;
				IPoint pointClass = new Point()
				{
					X = double.Parse(this.method_7(str1, 0, ",")),
					Y = double.Parse(this.method_7(str1, 1, ","))
				};
				IConstructCircularArc circularArcClass = new CircularArc() as IConstructCircularArc;
				circularArcClass.ConstructCircle(pointClass, num2, true);
				(polylineClass as ISegmentCollection).AddSegment(circularArcClass as ISegment, ref value, ref value);
				feature.Shape = polylineClass as IGeometry;
				IPolygon polygonClass = new Polygon() as IPolygon;
				(polygonClass as ISegmentCollection).AddSegment(circularArcClass as ISegment, ref value, ref value);
				if (!(polygonClass as ITopologicalOperator).IsSimple)
				{
					(polygonClass as ITopologicalOperator).Simplify();
				}
				try
				{
					feature1.Shape = polygonClass;
				}
				catch (Exception exception)
				{
					Logger.Current.Error(null, exception, this);
				}
				do
				{
					if (streamReader_0.Peek() >= 0)
					{
						str1 = streamReader_0.ReadLine();
					}
					else
					{
						break;
					}
				}
				while (str1 != "e");
				feature.Store();
				feature1.Store();
				if (this.ifeatureProgress_StepEventHandler_0 == null)
				{
					continue;
				}
				this.ifeatureProgress_StepEventHandler_0();
			}
		}

		private void method_12(StreamReader streamReader_0, IFeatureClass ifeatureClass_9, string string_3, string string_4)
		{
			string str;
			string str1;
			double num;
			string str2;
			string str3;
			IPoint pointClass;
			object value = Missing.Value;
			int string3 = ifeatureClass_9.Fields.FindField("Layer");
			int num1 = ifeatureClass_9.Fields.FindField("Code");
			int num2 = ifeatureClass_9.Fields.FindField("LineType");
			int num3 = ifeatureClass_9.Fields.FindField("Line_Width");
			int num4 = ifeatureClass_9.Fields.FindField("FitType");
			int num5 = ifeatureClass_9.Fields.FindField("Other");
			while (streamReader_0.Peek() >= 0)
			{
				string str4 = streamReader_0.ReadLine();
				if ((str4.ToUpper() == "NIL" ? true : str4.ToUpper() == "END"))
				{
					break;
				}
				if (str4 == "e")
				{
					continue;
				}
				IGeometryCollection polylineClass = new Polyline() as IGeometryCollection;
				str4 = string.Concat(str4, ",");
				if (this.int_0 != 5)
				{
					str = this.method_7(str4, 0, ",");
					num = double.Parse(this.method_7(str4, 1, ","));
					str2 = this.method_7(str4, 2, ",");
					str3 = this.method_7(str4, 3, ",");
					str1 = "Continuous";
				}
				else
				{
					str = this.method_7(str4, 0, ",");
					str1 = this.method_7(str4, 1, ",");
					num = double.Parse(this.method_7(str4, 2, ","));
					str2 = this.method_7(str4, 3, ",");
					str3 = this.method_7(str4, 4, ",");
				}
				do
				{
					if (streamReader_0.Peek() < 0)
					{
						break;
					}
					IPointCollection pathClass = new ESRI.ArcGIS.Geometry.Path();
					while (streamReader_0.Peek() >= 0)
					{
						str4 = streamReader_0.ReadLine();
						if (str4 == "e")
						{
						    break;
						}
						if ((str4.ToUpper() == "NIL" ? true : str4.ToUpper() == "END"))
						{
							return;
						}
						if ((str4 == "E" || str4 == "C" ? false : str4.ToUpper() != "nil"))
						{
							pointClass = new Point()
							{
								X = double.Parse(this.method_7(str4, 0, ",")),
								Y = double.Parse(this.method_7(str4, 1, ","))
							};
							pathClass.AddPoint(pointClass, ref value, ref value);
						}
						if (str4 == "C" && pathClass.PointCount > 2)
						{
							pointClass = pathClass.Point[0];
							pathClass.AddPoint(pointClass, ref value, ref value);
							try
							{
								if (this.ifeatureClass_2 == null)
								{
									this.ifeatureClass_2 = this.method_3(string.Concat(this.string_2, "_Polygong"), "POLYGON");
								}
								IFeature feature = this.ifeatureClass_2.CreateFeature();
								feature.Value[num1] = str;
								feature.Value[num2] = str1;
								feature.Value[num3] = num;
								feature.Value[num4] = str2;
								feature.Value[num5] = str3;
								IPolygon polygonClass = new Polygon() as IPolygon;
								(polygonClass as ISegmentCollection).AddSegmentCollection(pathClass as ISegmentCollection);
								if (!(polygonClass as ITopologicalOperator).IsSimple)
								{
									(polygonClass as ITopologicalOperator).Simplify();
								}
								feature.Shape = polygonClass;
								feature.Store();
							}
							catch (Exception exception)
							{
								Logger.Current.Error("", exception, "");
							}
						}
						if ((str4 == "E" ? false : !(str4 == "C")) || pathClass.PointCount <= 1)
						{
							continue;
						}
						polylineClass.AddGeometry(pathClass as IGeometry, ref value, ref value);
						pathClass = new ESRI.ArcGIS.Geometry.Path();
					}
				//Label1:
				}
				while (str4 != "e");
				if ((polylineClass as IPointCollection).PointCount > 0)
				{
					if (!(polylineClass as ITopologicalOperator).IsSimple)
					{
						(polylineClass as ITopologicalOperator).Simplify();
					}
					IFeature feature1 = ifeatureClass_9.CreateFeature();
					feature1.Value[string3] = string_3;
					feature1.Value[num1] = str;
					feature1.Value[num2] = str1;
					feature1.Value[num3] = num;
					feature1.Value[num4] = str2;
					feature1.Value[num5] = str3;
					feature1.Shape = polylineClass as IGeometry;
					try
					{
						feature1.Store();
					}
					catch (Exception exception1)
					{
						Logger.Current.Error("", exception1, "");
					}
				}
				if (this.ifeatureProgress_StepEventHandler_0 == null)
				{
					continue;
				}
				this.ifeatureProgress_StepEventHandler_0();
			}
		}

		private bool method_13(string string_3, string string_4)
		{
			bool flag;
			string[] strArrays = string_3.Split(string_4.ToCharArray());
			int num = 0;
			while (true)
			{
				if (num >= (int)strArrays.Length)
				{
					flag = false;
					break;
				}
				else if (strArrays[num] != "0")
				{
					flag = true;
					break;
				}
				else
				{
					num++;
				}
			}
			return flag;
		}

		private void method_14(StreamReader streamReader_0, IFeatureClass ifeatureClass_9, string string_3, string string_4)
		{
			while (streamReader_0.Peek() >= 0)
			{
				string str = streamReader_0.ReadLine();
				if ((str.ToUpper() == "NIL" ? true : str.ToUpper() == "END"))
				{
					break;
				}
				if (str == "e" || !this.method_13(str, ","))
				{
					continue;
				}
				string str1 = this.method_7(str, 0, ",");
				double num = double.Parse(this.method_7(str, 1, ","));
				double num1 = double.Parse(this.method_7(str, 2, ","));
				string str2 = streamReader_0.ReadLine();
				str = streamReader_0.ReadLine();
				if (this.ifeatureProgress_StepEventHandler_0 != null)
				{
					this.ifeatureProgress_StepEventHandler_0();
				}
				try
				{
					IPoint pointClass = new Point()
					{
						X = double.Parse(this.method_7(str, 0, ",")),
						Y = double.Parse(this.method_7(str, 1, ","))
					};
					IFeature string3 = ifeatureClass_9.CreateFeature();
					if ((this.ifeatureWorkspace_0 as IWorkspace).Type != esriWorkspaceType.esriFileSystemWorkspace)
					{
						ITextElement textElement = this.MakeTextElement(str2, pointClass);
						textElement.Symbol.Angle = num1;
						textElement.Symbol.Size = num;
						textElement.Text = str2;
						IAnnotationFeature2 annotationFeature2 = string3 as IAnnotationFeature2;
						annotationFeature2.LinkedFeatureID = -1;
						annotationFeature2.AnnotationClassID = 0;
						annotationFeature2.Annotation = textElement as IElement;
						string3.Store();
					}
					else
					{
						int num2 = ifeatureClass_9.Fields.FindField("Layer");
						int num3 = ifeatureClass_9.Fields.FindField("Code");
						string3.Value[num2] = string_3;
						string3.Value[num3] = str1;
						num2 = ifeatureClass_9.Fields.FindField("Height");
						string3.Value[num2] = num;
						num2 = ifeatureClass_9.Fields.FindField("rotate");
						string3.Value[num2] = num1;
						string3.Value[ifeatureClass_9.Fields.FindField("TextString")] = str2;
					}
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					this.ilist_0.Add(str);
					Logger.Current.Error("", exception, "");
				}
			}
		}

		private int method_15(string string_3, string string_4)
		{
			return (int)string_3.Split(string_4.ToCharArray()).Length;
		}

		private void method_16(StreamReader streamReader_0, IFeatureClass ifeatureClass_9, string string_3, string string_4, string string_5)
		{
			IGeometry polylineClass;
			IPointCollection pathClass;
			IPoint point;
			int string4;
			IFeature feature;
			while (streamReader_0.Peek() >= 0)
			{
				string str = streamReader_0.ReadLine();
				if ((str.ToUpper() == "NIL" ? true : str.ToUpper() == "END"))
				{
					break;
				}
				string str1 = this.method_7(str, 0, ",");
				int num = this.method_15(str, ",");
				object value = Missing.Value;
				if (num == 4)
				{
					if (str1 != "300000")
					{
						if (this.ifeatureClass_7 == null)
						{
							this.ifeatureClass_7 = this.method_3(string.Concat(this.string_2, "_LINE"), "LINE");
						}
						polylineClass = new Polyline() as IGeometry;
						pathClass = new ESRI.ArcGIS.Geometry.Path();
						do
						{
							str = streamReader_0.ReadLine();
							if ((str.ToUpper() == "NIL" ? true : str.ToUpper() == "END"))
							{
								return;
							}
							if ((str == "E" || str == "C" ? true : str == "e"))
							{
								if (pathClass.PointCount > 1)
								{
									if (str == "C" && !(pathClass as ICurve).IsClosed)
									{
										point = pathClass.Point[0];
										pathClass.AddPoint(point, ref value, ref value);
									}
									(polylineClass as IGeometryCollection).AddGeometry(pathClass as IGeometry, ref value, ref value);
								}
								pathClass = new ESRI.ArcGIS.Geometry.Path();
							}
							else
							{
								point = new Point()
								{
									X = double.Parse(this.method_7(str, 0, ",")),
									Y = double.Parse(this.method_7(str, 1, ","))
								};
								pathClass.AddPoint(point, ref value, ref value);
							}
						}
						while (str != "e");
						pathClass = new ESRI.ArcGIS.Geometry.Path();
						do
						{
							str = streamReader_0.ReadLine();
							if ((str.ToUpper() == "NIL" ? true : str.ToUpper() == "END"))
							{
								return;
							}
							if ((str == "E" || str == "C" ? true : str == "e"))
							{
								if (pathClass.PointCount > 1)
								{
									if (str == "C" && !(pathClass as ICurve).IsClosed)
									{
										point = pathClass.Point[0];
										pathClass.AddPoint(point, ref value, ref value);
									}
									(polylineClass as IGeometryCollection).AddGeometry(pathClass as IGeometry, ref value, ref value);
								}
								pathClass = new ESRI.ArcGIS.Geometry.Path();
							}
							else
							{
								point = new Point()
								{
									X = double.Parse(this.method_7(str, 0, ",")),
									Y = double.Parse(this.method_7(str, 1, ","))
								};
								pathClass.AddPoint(point, ref value, ref value);
							}
						}
						while (str != "e");
						if (str != "e")
						{
							continue;
						}
						if (this.ifeatureProgress_StepEventHandler_0 != null)
						{
							this.ifeatureProgress_StepEventHandler_0();
						}
						feature = this.ifeatureClass_7.CreateFeature();
						string4 = this.ifeatureClass_7.Fields.FindField("Layer");
						feature.Value[string4] = string_4;
						string4 = this.ifeatureClass_7.Fields.FindField("Code");
						feature.Value[string4] = str1;
						if (!(polylineClass as ITopologicalOperator).IsSimple)
						{
							(polylineClass as ITopologicalOperator).Simplify();
						}
						try
						{
							feature.Shape = polylineClass;
							feature.Store();
						}
						catch (Exception exception)
						{
							Logger.Current.Error("", exception, "");
						}
					}
					else
					{
						if (this.ifeatureClass_4 == null)
						{
							this.ifeatureClass_4 = this.method_4(string.Concat(this.string_2, "_JZXLINE"), esriGeometryType.esriGeometryPolyline);
						}
						string str2 = this.method_7(str, 1, ",");
						string str3 = this.method_7(str, 2, ",");
						string str4 = this.method_7(str, 3, ",");
						polylineClass = new Polyline() as IGeometry;
						pathClass = new ESRI.ArcGIS.Geometry.Path();
						do
						{
							str = streamReader_0.ReadLine();
							if ((str.ToUpper() != "NIL" ? str.ToUpper() == "END" : true))
							{
								return;
							}
							if ((str == "E" || str == "C" ? true : str == "e"))
							{
								if (pathClass.PointCount > 1)
								{
									if (str == "C")
									{
										if (!(pathClass as ICurve).IsClosed)
										{
											point = pathClass.Point[0];
											pathClass.AddPoint(point, ref value, ref value);
										}
										IPolygon polygonClass = new Polygon() as IPolygon;
										(polygonClass as IPointCollection).AddPointCollection(pathClass);
										if (this.ifeatureClass_5 == null)
										{
											this.ifeatureClass_5 = this.method_4(string.Concat(this.string_2, "_JZXPolygong"), esriGeometryType.esriGeometryPolygon);
										}
										string4 = this.ifeatureClass_5.Fields.FindField("Layer");
										IFeature feature1 = this.ifeatureClass_5.CreateFeature();
										feature1.Value[string4] = string_4;
										string4 = this.ifeatureClass_5.Fields.FindField("Code");
										feature1.Value[string4] = str1;
										string4 = this.ifeatureClass_5.Fields.FindField("ZTH");
										feature1.Value[string4] = str2;
										string4 = this.ifeatureClass_5.Fields.FindField("QLR");
										feature1.Value[string4] = str3;
										string4 = this.ifeatureClass_5.Fields.FindField("DLDM");
										feature1.Value[string4] = str4;
										if (!(feature1 as ITopologicalOperator).IsSimple)
										{
											(feature1 as ITopologicalOperator).Simplify();
										}
										try
										{
											feature1.Shape = polygonClass;
											feature1.Store();
										}
										catch (Exception exception1)
										{
											Logger.Current.Error("", exception1, "");
										}
									}
									(polylineClass as IGeometryCollection).AddGeometry(pathClass as IGeometry, ref value, ref value);
								}
								pathClass = new ESRI.ArcGIS.Geometry.Path();
							}
							else
							{
								point = new Point()
								{
									X = double.Parse(this.method_7(str, 0, ",")),
									Y = double.Parse(this.method_7(str, 1, ","))
								};
								pathClass.AddPoint(point, ref value, ref value);
							}
						}
						while (str != "e");
						if (str == "e")
						{
							feature = this.ifeatureClass_4.CreateFeature();
							string4 = this.ifeatureClass_4.Fields.FindField("Layer");
							feature.Value[string4] = string_4;
							string4 = this.ifeatureClass_4.Fields.FindField("Code");
							feature.Value[string4] = str1;
							string4 = this.ifeatureClass_4.Fields.FindField("ZTH");
							feature.Value[string4] = str2;
							string4 = this.ifeatureClass_4.Fields.FindField("QLR");
							feature.Value[string4] = str3;
							string4 = this.ifeatureClass_4.Fields.FindField("DLDM");
							feature.Value[string4] = str4;
							if (!(polylineClass as ITopologicalOperator).IsSimple)
							{
								(polylineClass as ITopologicalOperator).Simplify();
							}
							try
							{
								feature.Shape = polylineClass;
								feature.Store();
							}
							catch (Exception exception2)
							{
								Logger.Current.Error("", exception2, "");
							}
							if (this.ifeatureProgress_StepEventHandler_0 != null)
							{
								this.ifeatureProgress_StepEventHandler_0();
							}
						}
						if (this.int_0 == 5)
						{
							continue;
						}
					}
				}
				else if (num != 2)
				{
					if (num != 1)
					{
						continue;
					}
					if (string_4.ToUpper() != "ASSIST")
					{
						if (this.ifeatureClass_6 == null)
						{
							this.ifeatureClass_6 = this.method_3(string.Concat(this.string_2, "_SPECIALPoint"), "POINT");
						}
						str = streamReader_0.ReadLine();
						if ((str.ToUpper() == "NIL" ? true : str.ToUpper() == "END"))
						{
							break;
						}
						if (str == "e")
						{
							continue;
						}
						point = new Point()
						{
							X = double.Parse(this.method_7(str, 0, ",")),
							Y = double.Parse(this.method_7(str, 1, ","))
						};
						if (this.ifeatureProgress_StepEventHandler_0 != null)
						{
							this.ifeatureProgress_StepEventHandler_0();
						}
						feature = this.ifeatureClass_6.CreateFeature();
						string4 = this.ifeatureClass_6.Fields.FindField("Layer");
						feature.Value[string4] = string_4;
						string4 = this.ifeatureClass_6.Fields.FindField("Code");
						feature.Value[string4] = str1;
						feature.Shape = point;
						feature.Store();
					}
					else
					{
						if (this.ifeatureClass_8 == null)
						{
							this.ifeatureClass_8 = this.method_3(string.Concat(this.string_2, "_LINE"), "LINE");
						}
						polylineClass = new Polyline() as IGeometry;
						pathClass = new ESRI.ArcGIS.Geometry.Path();
						do
						{
							if (streamReader_0.Peek() < 0)
							{
								break;
							}
							str = streamReader_0.ReadLine();
							if ((str.ToUpper() == "NIL" ? true : str.ToUpper() == "END"))
							{
								return;
							}
							if ((str == "E" || str == "C" ? true : str == "e"))
							{
								if (pathClass.PointCount > 1)
								{
									if (str == "C" && !(pathClass as ICurve).IsClosed)
									{
										point = pathClass.Point[0];
										pathClass.AddPoint(point, ref value, ref value);
									}
									(polylineClass as IGeometryCollection).AddGeometry(pathClass as IGeometry, ref value, ref value);
								}
								pathClass = new ESRI.ArcGIS.Geometry.Path();
							}
							else
							{
								point = new Point()
								{
									X = double.Parse(this.method_7(str, 0, ",")),
									Y = double.Parse(this.method_7(str, 1, ","))
								};
								pathClass.AddPoint(point, ref value, ref value);
							}
						}
						while (str != "e");
						if (str != "e")
						{
							continue;
						}
						if (this.ifeatureProgress_StepEventHandler_0 != null)
						{
							this.ifeatureProgress_StepEventHandler_0();
						}
						feature = this.ifeatureClass_8.CreateFeature();
						string4 = this.ifeatureClass_8.Fields.FindField("Layer");
						feature.Value[string4] = string_4;
						string4 = this.ifeatureClass_8.Fields.FindField("Code");
						feature.Value[string4] = str1;
						if (!(polylineClass as ITopologicalOperator).IsSimple)
						{
							(polylineClass as ITopologicalOperator).Simplify();
						}
						try
						{
							feature.Shape = polylineClass;
							feature.Store();
						}
						catch (Exception exception3)
						{
							Logger.Current.Error("", exception3, "");
						}
					}
				}
				else
				{
					if (this.ifeatureClass_6 == null)
					{
						this.ifeatureClass_6 = this.method_3(string.Concat(this.string_2, "_SPECIALPoint"), "POINT");
					}
					this.method_7(str, 1, ",");
					str = streamReader_0.ReadLine();
					if ((str.ToUpper() == "NIL" ? true : str.ToUpper() == "END"))
					{
						break;
					}
					if (str == "e")
					{
						continue;
					}
					point = new Point()
					{
						X = double.Parse(this.method_7(str, 0, ",")),
						Y = double.Parse(this.method_7(str, 1, ","))
					};
					if (this.ifeatureProgress_StepEventHandler_0 != null)
					{
						this.ifeatureProgress_StepEventHandler_0();
					}
					feature = this.ifeatureClass_6.CreateFeature();
					try
					{
						feature.Shape = point;
						feature.Store();
					}
					catch (Exception exception4)
					{
						Logger.Current.Error("", exception4, "");
					}
				}
			}
		}

		private string method_17(StreamReader streamReader_0, string string_3)
		{
			string str;
			bool flag;
			string str1 = "";
			string str2 = "";
			string str3 = "";
			string str4 = "";
			string str5 = "";
			while (true)
			{
				if (streamReader_0.Peek() >= 0)
				{
					str5 = streamReader_0.ReadLine();
					if (str5.ToUpper() == "NIL")
					{
						str = str1;
						break;
					}
					else if (str5.ToUpper() == "END")
					{
						str1 = "";
						str = str1;
						break;
					}
					else if ((str5[0] != '[' ? false : str5[str5.Length - 1] == ']'))
					{
						str1 = str5.Substring(1, str5.Length - 2);
						str = str1;
						break;
					}
					else
					{
						str2 = str5;
						if (string_3[0] == '.' || string_3[0] == '\u005F')
						{
							flag = false;
						}
						else
						{
							flag = (string_3[0] < '0' ? true : string_3[0] > '9');
						}
						if (!flag)
						{
							string_3 = string.Concat("A", string_3);
						}
						if (str2 == "POINT")
						{
							str4 = string.Concat(string_3, "_Point");
						}
						else if (!(str2 == "LINE" || str2 == "ARC" || str2 == "PLINE" || str2 == "SPLINE" ? false : !(str2 == "CIRCLE")))
						{
							str4 = string.Concat(string_3, "_Line");
						}
						else if (str2 != "TEXT")
						{
							str = "";
							break;
						}
						else
						{
							str4 = "Anno_Text";
						}
						CassLayer item = null;
						if (str3 != str4)
						{
							int num = 0;
							while (true)
							{
								if (num >= this.ilist_3.Count)
								{
									break;
								}
								else if ((this.ilist_3[num] as CassLayer).Name == str4)
								{
									item = this.ilist_3[num] as CassLayer;
									break;
								}
								else
								{
									num++;
								}
							}
							if (item == null)
							{
								item = new CassLayer()
								{
									FeatureType = str2,
									Name = str4
								};
								this.ilist_3.Add(item);
							}
							str3 = str4;
						}
						if (item != null)
						{
							this.method_18(streamReader_0, str2, item);
						}
					}
				}
				else
				{
					str = "";
					break;
				}
			}
			return str;
		}

		private void method_18(StreamReader streamReader_0, string string_3, CassLayer cassLayer_0)
		{
			string string3 = string_3;
			if (string3 != null)
			{
				switch (string3)
				{
					case "POINT":
					{
						this.method_19(streamReader_0, cassLayer_0);
						break;
					}
					case "LINE":
					{
						this.method_20(streamReader_0, cassLayer_0);
						break;
					}
					case "ARC":
					{
						this.method_21(streamReader_0, cassLayer_0);
						break;
					}
					case "CIRCLE":
					{
						this.method_22(streamReader_0, cassLayer_0);
						break;
					}
					case "PLINE":
					{
						this.method_23(streamReader_0, cassLayer_0);
						break;
					}
					case "SPLINE":
					{
						this.method_23(streamReader_0, cassLayer_0);
						break;
					}
				}
			}
		}

		private void method_19(StreamReader streamReader_0, CassLayer cassLayer_0)
		{
		Label0:
			while (streamReader_0.Peek() >= 0)
			{
				string str = streamReader_0.ReadLine();
				if ((str.ToUpper() == "NIL" ? true : str.ToUpper() == "END"))
				{
					break;
				}
				if (str == "e")
				{
					continue;
				}
				str = streamReader_0.ReadLine();
				if ((str.ToUpper() == "NIL" ? false : !(str.ToUpper() == "END")))
				{
					while (streamReader_0.Peek() >= 0)
					{
						str = streamReader_0.ReadLine();
						if (str == "e")
						{
							goto Label0;
						}
						if ((str.ToUpper() == "NIL" ? true : str.ToUpper() == "END"))
						{
							return;
						}
						if (str == "")
						{
							continue;
						}
						string str1 = this.method_7(str, 0, ",");
						cassLayer_0.AttributeList.Add(str1);
					}
				}
				else
				{
					break;
				}
			}
		}

		private IFeatureClass method_2(string string_3)
		{
			IFeatureClass featureClass;
			if ((this.ifeatureWorkspace_0 as IWorkspace).Type != esriWorkspaceType.esriFileSystemWorkspace)
			{
				try
				{
					featureClass = this.method_1(string_3, 500, new TextSymbol());
				}
				catch (Exception exception)
				{
					Logger.Current.Error("", exception, "");
					featureClass = null;
				}
			}
			else
			{
				IFeatureClass featureClass1 = null;
				IObjectClassDescription featureClassDescriptionClass = new FeatureClassDescription();
				IFieldsEdit requiredFields = featureClassDescriptionClass.RequiredFields as IFieldsEdit;
				IFieldEdit field = null;
				int num = requiredFields.FindField((featureClassDescriptionClass as IFeatureClassDescription).ShapeFieldName);
				field = requiredFields.Field[num] as IFieldEdit;
				IGeometryDefEdit geometryDef = field.GeometryDef as IGeometryDefEdit;
				ISpatialReference spatialReference = geometryDef.SpatialReference;
				spatialReference.SetDomain(this.ienvelope_0.XMin, this.ienvelope_0.XMax, this.ienvelope_0.YMin, this.ienvelope_0.YMax);
				geometryDef.SpatialReference_2 = spatialReference;
				geometryDef.GeometryType_2 = esriGeometryType.esriGeometryPoint;
				field.GeometryDef_2 = geometryDef;
				IFieldEdit fieldClass = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
			    fieldClass.Name_2 = "Layer";
			    fieldClass.AliasName_2 = "层名";
			    fieldClass.Type_2 = esriFieldType.esriFieldTypeString;
			    fieldClass.Length_2 = 60;
			    requiredFields.AddField(fieldClass);
                fieldClass = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                fieldClass.Name_2 = "Code";
			    fieldClass.AliasName_2 = "要素编码";
			    fieldClass.Type_2 = esriFieldType.esriFieldTypeString;
			    fieldClass.Length_2 = 60;
			    requiredFields.AddField(fieldClass);
                fieldClass = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                fieldClass.Name_2 = "Height";
			    fieldClass.AliasName_2 = "字高";
			    fieldClass.Type_2 = esriFieldType.esriFieldTypeDouble;
			    requiredFields.AddField(fieldClass);
                fieldClass = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                fieldClass.Name_2 = "Rotate";
			    fieldClass.AliasName_2 = "旋转角";
			    fieldClass.Type_2 = esriFieldType.esriFieldTypeDouble;
			    requiredFields.AddField(fieldClass);
                fieldClass = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                fieldClass.Name_2 = "TextString";
			    fieldClass.AliasName_2 = "文本";
			    fieldClass.Type_2 = esriFieldType.esriFieldTypeString;
			    fieldClass.Length_2 = 260;
			    requiredFields.AddField(fieldClass);
				featureClass1 = this.ifeatureWorkspace_0.CreateFeatureClass(string_3, requiredFields, null, null, esriFeatureType.esriFTSimple, "Shape", "");
				featureClass = featureClass1;
			}
			return featureClass;
		}

		private void method_20(StreamReader streamReader_0, CassLayer cassLayer_0)
		{
			cassLayer_0.HasUnClosedLine = false;
		Label0:
			while (streamReader_0.Peek() >= 0)
			{
				string str = streamReader_0.ReadLine();
				if ((str.ToUpper() == "NIL" ? true : str.ToUpper() == "END"))
				{
					break;
				}
				if (str == "e")
				{
					continue;
				}
				str = streamReader_0.ReadLine();
				if ((str.ToUpper() == "NIL" ? true : str.ToUpper() == "END"))
				{
					break;
				}
				str = streamReader_0.ReadLine();
				if ((str.ToUpper() == "NIL" ? false : !(str.ToUpper() == "END")))
				{
					while (streamReader_0.Peek() >= 0)
					{
						str = streamReader_0.ReadLine();
						if (str == "e")
						{
							goto Label0;
						}
						if ((str.ToUpper() == "NIL" ? true : str.ToUpper() == "END"))
						{
							return;
						}
						if (str == "")
						{
							continue;
						}
						string str1 = this.method_7(str, 0, ",");
						cassLayer_0.AttributeList.Add(str1);
					}
				}
				else
				{
					break;
				}
			}
		}

		private void method_21(StreamReader streamReader_0, CassLayer cassLayer_0)
		{
			cassLayer_0.HasUnClosedLine = false;
		Label0:
			while (streamReader_0.Peek() >= 0)
			{
				string str = streamReader_0.ReadLine();
				if ((str.ToUpper() == "NIL" ? true : str.ToUpper() == "END"))
				{
					break;
				}
				if (str == "e")
				{
					continue;
				}
				str = streamReader_0.ReadLine();
				if ((str.ToUpper() == "NIL" ? false : !(str.ToUpper() == "END")))
				{
					while (streamReader_0.Peek() >= 0)
					{
						str = streamReader_0.ReadLine();
						if (str == "e")
						{
							goto Label0;
						}
						if ((str.ToUpper() == "NIL" ? true : str.ToUpper() == "END"))
						{
							return;
						}
						if (str == "")
						{
							continue;
						}
						string str1 = this.method_7(str, 0, ",");
						cassLayer_0.AttributeList.Add(str1);
					}
				}
				else
				{
					break;
				}
			}
		}

		private void method_22(StreamReader streamReader_0, CassLayer cassLayer_0)
		{
			cassLayer_0.HasClosedLine = true;
		Label0:
			while (streamReader_0.Peek() >= 0)
			{
				string str = streamReader_0.ReadLine();
				if ((str.ToUpper() == "NIL" ? true : str.ToUpper() == "END"))
				{
					break;
				}
				if (str == "e")
				{
					continue;
				}
				str = streamReader_0.ReadLine();
				if ((str.ToUpper() == "NIL" ? false : !(str.ToUpper() == "END")))
				{
					while (streamReader_0.Peek() >= 0)
					{
						str = streamReader_0.ReadLine();
						if (str == "e")
						{
							goto Label0;
						}
						if ((str.ToUpper() == "NIL" ? true : str.ToUpper() == "END"))
						{
							return;
						}
						if (str == "")
						{
							continue;
						}
						string str1 = this.method_7(str, 0, ",");
						cassLayer_0.AttributeList.Add(str1);
					}
				}
				else
				{
					break;
				}
			}
		}

		private void method_23(StreamReader streamReader_0, CassLayer cassLayer_0)
		{
		Label0:
			while (streamReader_0.Peek() >= 0)
			{
				string str = streamReader_0.ReadLine();
				if ((str.ToUpper() == "NIL" ? true : str.ToUpper() == "END"))
				{
					break;
				}
				if (str != "e")
				{
					do
					{
						if (streamReader_0.Peek() < 0)
						{
						    break;
						}
						while (streamReader_0.Peek() >= 0)
						{
							str = streamReader_0.ReadLine();
							if (str == "e")
							{
							    break;
							}
							if ((str.ToUpper() == "NIL" ? true : str.ToUpper() == "END"))
							{
								return;
							}
							if (str == "C")
							{
								cassLayer_0.HasClosedLine = true;
							}
							if (str != "E")
							{
								continue;
							}
							cassLayer_0.HasUnClosedLine = true;
						}
					
					}
					while (str != "e");
				}
			}
		}

       
        private IFeatureClass method_3(string string_3, string string_4)
        {
            IFeatureClass featureClass = null;
            IFeatureClass result;
            if ((this.ifeatureWorkspace_0 as IWorkspace2).get_NameExists(esriDatasetType.esriDTFeatureClass, string_3))
            {
                string name = (this.ifeatureWorkspace_0 as ISQLSyntax).QualifyTableName(this.string_1, this.string_0, string_3);
                featureClass = this.ifeatureWorkspace_0.OpenFeatureClass(name);
                result = featureClass;
            }
            else if (string_4 == "TEXT")
            {
                result = this.method_2(string_3);
            }
            else
            {
                IFieldEdit field;
                IObjectClassDescription objectClassDescription = new FeatureClassDescription();
                IFieldsEdit fieldsEdit = objectClassDescription.RequiredFields as IFieldsEdit;
                int index = fieldsEdit.FindField((objectClassDescription as IFeatureClassDescription).ShapeFieldName);
                IFieldEdit fieldEdit = fieldsEdit.get_Field(index) as IFieldEdit;
                IGeometryDefEdit geometryDefEdit = fieldEdit.GeometryDef as IGeometryDefEdit;
                ISpatialReference spatialReference = geometryDefEdit.SpatialReference;
                spatialReference.SetDomain(this.ienvelope_0.XMin, this.ienvelope_0.XMax, this.ienvelope_0.YMin, this.ienvelope_0.YMax);
                SpatialReferenctOperator.ChangeCoordinateSystem(this.ifeatureWorkspace_0 as IGeodatabaseRelease, spatialReference, false);
                geometryDefEdit.SpatialReference_2 = spatialReference;
                esriFeatureType featureType = esriFeatureType.esriFTSimple;
                if (string_4 == "POINT")
                {
                    geometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;
                    field = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                    field.Name_2 = "Layer";
                    field.AliasName_2 = "层名";
                    field.Type_2 = esriFieldType.esriFieldTypeString;
                    field.Length_2 = 60;
                    fieldsEdit.AddField(field);
                    field = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                    field.Name_2 = "Code";
                    field.AliasName_2 = "要素编码";
                    field.Type_2 = esriFieldType.esriFieldTypeString;
                    field.Length_2 = 60;
                    fieldsEdit.AddField(field);
                    field = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                    field.Name_2 = "Rotate";
                    field.AliasName_2 = "旋转";
                    field.Type_2 = esriFieldType.esriFieldTypeDouble;
                    fieldsEdit.AddField(field);
                    field = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                    field.Name_2 = "Scale";
                    field.AliasName_2 = "缩放比例";
                    field.Type_2 = esriFieldType.esriFieldTypeDouble;
                    fieldsEdit.AddField(field);
                    field = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                    field.Name_2 = "KZD_Name";
                    field.AliasName_2 = "点名";
                    field.Type_2 = esriFieldType.esriFieldTypeString;
                    field.Length_2 = 60;
                    fieldsEdit.AddField(field);
                }
                else if (string_4 == "LINE" || string_4 == "ARC" || string_4 == "PLINE" || string_4 == "SPLINE" || string_4 == "CIRCLE")
                {
                    geometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolyline;
                    field = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                    field.Name_2 = "Layer";
                    field.AliasName_2 = "层名";
                    field.Type_2 = esriFieldType.esriFieldTypeString;
                    field.Length_2 = 60;
                    fieldsEdit.AddField(field);
                    field = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                    field.Name_2 = "Code";
                    field.AliasName_2 = "要素编码";
                    field.Type_2 = esriFieldType.esriFieldTypeString;
                    field.Length_2 = 60;
                    fieldsEdit.AddField(field);
                    field = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                    field.Name_2 = "LineType";
                    field.AliasName_2 = "线类型";
                    field.Type_2 = esriFieldType.esriFieldTypeString;
                    field.Length_2 = 60;
                    fieldsEdit.AddField(field);
                    field = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                    field.Name_2 = "Line_Width";
                    field.AliasName_2 = "线宽";
                    field.Type_2 = esriFieldType.esriFieldTypeDouble;
                    fieldsEdit.AddField(field);
                    field = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                    field.Name_2 = "FitType";
                    field.AliasName_2 = "拟合样式";
                    field.Type_2 = esriFieldType.esriFieldTypeString;
                    field.Length_2 = 60;
                    fieldsEdit.AddField(field);
                    field = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                    field.Name_2 = "Other";
                    field.AliasName_2 = "附加值";
                    field.Type_2 = esriFieldType.esriFieldTypeString;
                    field.Length_2 = 60;
                    fieldsEdit.AddField(field);
                }
                else if (string_4 == "TEXT")
                {
                    geometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolygon;
                    field = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                    field.Name_2 = "Layer";
                    field.AliasName_2 = "层名";
                    field.Type_2 = esriFieldType.esriFieldTypeString;
                    field.Length_2 = 60;
                    fieldsEdit.AddField(field);
                    field = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                    field.Name_2 = "Code";
                    field.AliasName_2 = "要素编码";
                    field.Type_2 = esriFieldType.esriFieldTypeString;
                    field.Length_2 = 60;
                    fieldsEdit.AddField(field);
                    featureType = esriFeatureType.esriFTAnnotation;
                }
                else
                {
                    if (!(string_4 == "POLYGON"))
                    {
                        result = null;
                        return result;
                    }
                    geometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolygon;
                    field = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                    field.Name_2 = "Layer";
                    field.AliasName_2 = "层名";
                    field.Type_2 = esriFieldType.esriFieldTypeString;
                    field.Length_2 = 60;
                    fieldsEdit.AddField(field);
                    field = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                    field.Name_2 = "Code";
                    field.AliasName_2 = "要素编码";
                    field.Type_2 = esriFieldType.esriFieldTypeString;
                    field.Length_2 = 60;
                    fieldsEdit.AddField(field);
                    field = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                    field.Name_2 = "LineType";
                    field.AliasName_2 = "线类型";
                    field.Type_2 = esriFieldType.esriFieldTypeString;
                    field.Length_2 = 60;
                    fieldsEdit.AddField(field);
                    field = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                    field.Name_2 = "Line_Width";
                    field.AliasName_2 = "线宽";
                    field.Type_2 = esriFieldType.esriFieldTypeDouble;
                    fieldsEdit.AddField(field);
                    field = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                    field.Name_2 = "FitType";
                    field.AliasName_2 = "拟合样式";
                    field.Type_2 = esriFieldType.esriFieldTypeString;
                    field.Length_2 = 60;
                    fieldsEdit.AddField(field);
                    field = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                    field.Name_2 = "Other";
                    field.AliasName_2 = "附加值";
                    field.Type_2 = esriFieldType.esriFieldTypeString;
                    field.Length_2 = 60;
                    fieldsEdit.AddField(field);
                }
                fieldEdit.GeometryDef_2 = geometryDefEdit;
                try
                {
                    if (this.ifeatureDataset_0 != null)
                    {
                        featureClass = this.ifeatureDataset_0.CreateFeatureClass(string_3, fieldsEdit, null, null, featureType, "Shape", "");
                    }
                    else
                    {
                        featureClass = this.ifeatureWorkspace_0.CreateFeatureClass(string_3, fieldsEdit, null, null, featureType, "Shape", "");
                    }
                }
                catch (System.Exception exception_)
                {
                    Logger.Current.Error("", exception_, "");
                }
                result = featureClass;
            }
            return result;
        }


        private IFeatureClass method_4(string string_3, esriGeometryType esriGeometryType_0)
		{
			IFeatureClass featureClass = null;
			IObjectClassDescription featureClassDescriptionClass = null;
			featureClassDescriptionClass = new FeatureClassDescription();
			IFieldsEdit requiredFields = featureClassDescriptionClass.RequiredFields as IFieldsEdit;
			IFieldEdit fieldClass = null;
			IFieldEdit field = null;
			int num = requiredFields.FindField((featureClassDescriptionClass as IFeatureClassDescription).ShapeFieldName);
			field = requiredFields.Field[num] as IFieldEdit;
			IGeometryDefEdit geometryDef = field.GeometryDef as IGeometryDefEdit;
			ISpatialReference spatialReference = geometryDef.SpatialReference;
			spatialReference.SetDomain(this.ienvelope_0.XMin, this.ienvelope_0.XMax, this.ienvelope_0.YMin, this.ienvelope_0.YMax);
			SpatialReferenctOperator.ChangeCoordinateSystem(this.ifeatureWorkspace_0 as IGeodatabaseRelease, spatialReference, false);
			geometryDef.SpatialReference_2 = spatialReference;
			geometryDef.GeometryType_2 = esriGeometryType_0;
            fieldClass = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
            fieldClass.Name_2 = "Layer";
		    fieldClass.AliasName_2 = "层名";
		    fieldClass.Type_2 = esriFieldType.esriFieldTypeString;
		    fieldClass.Length_2 = 60;
		    requiredFields.AddField(fieldClass);
            fieldClass = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
            fieldClass.Name_2 = "Code";
		    fieldClass.AliasName_2 = "要素编码";
		    fieldClass.Type_2 = esriFieldType.esriFieldTypeString;
		    fieldClass.Length_2 = 60;
		    requiredFields.AddField(fieldClass);
            fieldClass = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
            fieldClass.Name_2 = "ZTH";
		    fieldClass.AliasName_2 = "宗地号";
		    fieldClass.Type_2 = esriFieldType.esriFieldTypeString;
		    fieldClass.Length_2 = 60;
		    requiredFields.AddField(fieldClass);
            fieldClass = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
            fieldClass.Name_2 = "QLR";
		    fieldClass.AliasName_2 = "权利人";
		    fieldClass.Type_2 = esriFieldType.esriFieldTypeString;
		    fieldClass.Length_2 = 260;
		    requiredFields.AddField(fieldClass);
            fieldClass = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
            fieldClass.Name_2 = "DLDM";
		    fieldClass.AliasName_2 = "地类";
		    fieldClass.Type_2 = esriFieldType.esriFieldTypeString;
		    fieldClass.Length_2 = 60;
		    requiredFields.AddField(fieldClass);
			field.GeometryDef_2 = geometryDef;
			featureClass = this.ifeatureWorkspace_0.CreateFeatureClass(string_3, requiredFields, null, null, esriFeatureType.esriFTSimple, "Shape", "");
			return featureClass;
		}

		private string method_5(StreamReader streamReader_0, string string_3)
		{
			string str;
			string str1 = "";
			string str2 = "";
			string str3 = "";
			string str4 = "";
			string str5 = "";
			IFeatureClass featureClass = null;
			while (true)
			{
				if (streamReader_0.Peek() >= 0)
				{
					str5 = streamReader_0.ReadLine();
					if (str5.ToUpper() == "NIL")
					{
						str = str1;
						break;
					}
					else if (str5.ToUpper() == "END")
					{
						str1 = "";
						str = str1;
						break;
					}
					else if ((str5[0] != '[' ? false : str5[str5.Length - 1] == ']'))
					{
						str1 = str5.Substring(1, str5.Length - 2);
						str = str1;
						break;
					}
					else
					{
						str2 = str5;
						if (str2 == "POINT")
						{
							str4 = string.Concat(this.string_2, "_Point");
						}
						else if (!(str2 == "LINE" || str2 == "ARC" || str2 == "PLINE" || str2 == "SPLINE" ? false : !(str2 == "CIRCLE")))
						{
							str4 = string.Concat(this.string_2, "_Line");
						}
						else if (str2 == "TEXT")
						{
							str4 = string.Concat(this.string_2, string_3, "_Anno");
						}
						else if (str2 != "SPECIAL")
						{
							str = "";
							break;
						}
						else
						{
							this.method_6(streamReader_0, featureClass, string_3, str2);
							str = "";
							break;
						}
						if (str3 != str4)
						{
							featureClass = this.method_0(str4, str2);
							str3 = str4;
						}
						try
						{
							this.method_6(streamReader_0, featureClass, string_3, str2);
						}
						catch (Exception exception)
						{
							Logger.Current.Error("", exception, "");
						}
					}
				}
				else
				{
					str = "";
					break;
				}
			}
			return str;
		}

		private void method_6(StreamReader streamReader_0, IFeatureClass ifeatureClass_9, string string_3, string string_4)
		{
			string string4 = string_4;
			if (string4 != null)
			{
				switch (string4)
				{
					case "POINT":
					{
						this.method_8(streamReader_0, ifeatureClass_9, string_3, string_4);
						break;
					}
					case "LINE":
					{
						this.method_9(streamReader_0, ifeatureClass_9, string_3, string_4);
						break;
					}
					case "ARC":
					{
						this.method_10(streamReader_0, ifeatureClass_9, string_3, string_4);
						break;
					}
					case "CIRCLE":
					{
						this.method_11(streamReader_0, ifeatureClass_9, string_3, string_4);
						break;
					}
					case "PLINE":
					{
						this.method_12(streamReader_0, ifeatureClass_9, string_3, string_4);
						break;
					}
					case "SPLINE":
					{
						this.method_12(streamReader_0, ifeatureClass_9, string_3, string_4);
						break;
					}
					case "TEXT":
					{
						this.method_14(streamReader_0, ifeatureClass_9, string_3, string_4);
						break;
					}
					case "SPECIAL":
					{
						this.method_16(streamReader_0, ifeatureClass_9, "", string_3, "");
						break;
					}
				}
			}
		}

		private string method_7(string string_3, int int_1, string string_4)
		{
			string str;
			string[] strArrays = string_3.Split(string_4.ToCharArray());
			str = (int_1 >= (int)strArrays.Length ? "" : strArrays[int_1]);
			return str;
		}

		private void method_8(StreamReader streamReader_0, IFeatureClass ifeatureClass_9, string string_3, string string_4)
		{
			string str;
			int string3 = ifeatureClass_9.Fields.FindField("Layer");
			int num = ifeatureClass_9.Fields.FindField("Code");
			int num1 = ifeatureClass_9.Fields.FindField("Rotate");
			int num2 = ifeatureClass_9.Fields.FindField("Scale");
			int num3 = ifeatureClass_9.Fields.FindField("KZD_Name");
			while (streamReader_0.Peek() >= 0)
			{
				string str1 = streamReader_0.ReadLine();
				if ((str1.ToUpper() == "NIL" ? true : str1.ToUpper() == "END"))
				{
					break;
				}
				if (str1 == "e" || !this.method_13(str1, ","))
				{
					continue;
				}
				IFeature feature = ifeatureClass_9.CreateFeature();
				feature.Value[string3] = string_3;
				string str2 = "";
				if (string_3 == "KZD")
				{
					str1 = string.Concat(str1, ",0,0,0,0,0");
					str = this.method_7(str1, 0, ",");
					str2 = this.method_7(str1, 1, ",");
					feature.Value[num] = str;
					feature.Value[num3] = str2;
				}
				else if (string_3 == "GCD")
				{
					str1 = string.Concat(str1, ",0,0,0,0,0");
					str = this.method_7(str1, 0, ",");
					double num4 = double.Parse(this.method_7(str1, 1, ","));
					double num5 = double.Parse(this.method_7(str1, 2, ","));
					feature.Value[num] = str;
					feature.Value[num1] = num4;
					feature.Value[num2] = num5;
				}
				str1 = streamReader_0.ReadLine();
				if ((str1.ToUpper() == "NIL" ? true : str1.ToUpper() == "END"))
				{
					break;
				}
				IPoint pointClass = new Point()
				{
					X = double.Parse(this.method_7(str1, 0, ",")),
					Y = double.Parse(this.method_7(str1, 1, ","))
				};
				feature.Shape = pointClass;
				while (streamReader_0.Peek() >= 0)
				{
					str1 = streamReader_0.ReadLine();
					if (str1 == "e")
					{
						break;
					}
					if ((str1.ToUpper() == "NIL" ? true : str1.ToUpper() == "END"))
					{
						return;
					}
				}
				feature.Store();
				if (this.ifeatureProgress_StepEventHandler_0 == null)
				{
					continue;
				}
				this.ifeatureProgress_StepEventHandler_0();
			}
		}

		private void method_9(StreamReader streamReader_0, IFeatureClass ifeatureClass_9, string string_3, string string_4)
		{
			object value = Missing.Value;
			int string3 = ifeatureClass_9.Fields.FindField("Layer");
			int num = ifeatureClass_9.Fields.FindField("Code");
			int num1 = ifeatureClass_9.Fields.FindField("LineType");
			while (streamReader_0.Peek() >= 0)
			{
				string str = streamReader_0.ReadLine();
				if ((str.ToUpper() == "NIL" ? true : str.ToUpper() == "END"))
				{
					break;
				}
				if (str == "e" || !this.method_13(str, ","))
				{
					continue;
				}
				str = string.Concat(str, ",Continuous,Continuous");
				string str1 = this.method_7(str, 0, ",");
				string str2 = this.method_7(str, 1, ",");
				str = streamReader_0.ReadLine();
				if ((str.ToUpper() == "NIL" ? true : str.ToUpper() == "END"))
				{
					break;
				}
				IPointCollection polylineClass = new Polyline();
				IPoint pointClass = new Point()
				{
					X = double.Parse(this.method_7(str, 0, ",")),
					Y = double.Parse(this.method_7(str, 1, ","))
				};
				polylineClass.AddPoint(pointClass, ref value, ref value);
				str = streamReader_0.ReadLine();
				if ((str.ToUpper() == "NIL" ? true : str.ToUpper() == "END"))
				{
					break;
				}
				pointClass = new Point()
				{
					X = double.Parse(this.method_7(str, 0, ",")),
					Y = double.Parse(this.method_7(str, 1, ","))
				};
				polylineClass.AddPoint(pointClass, ref value, ref value);
				IFeature feature = ifeatureClass_9.CreateFeature();
				feature.Value[string3] = string_3;
				feature.Value[num] = str1;
				feature.Value[num1] = str2;
				feature.Shape = polylineClass as IGeometry;
				while (true)
				{
					if (streamReader_0.Peek() >= 0)
					{
						str = streamReader_0.ReadLine();
						if (str == "e")
						{
							break;
						}
						else if ((str.ToUpper() == "NIL" ? true : str.ToUpper() == "END"))
						{
							return;
						}
					}
					else
					{
						break;
					}
				}
				try
				{
					feature.Store();
				}
				catch (Exception exception)
				{
					Logger.Current.Error("", exception, "");
				}
				if (this.ifeatureProgress_StepEventHandler_0 == null)
				{
					continue;
				}
				this.ifeatureProgress_StepEventHandler_0();
			}
		}

		public void Read(string string_3)
		{
			bool flag;
			if (File.Exists(string_3))
			{
				this.string_2 = System.IO.Path.GetFileNameWithoutExtension(string_3);
				this.string_2.Replace('.', '\u005F');
				if (this.string_2[0] == '\u005F')
				{
					flag = false;
				}
				else
				{
					flag = (this.string_2[0] < '0' ? true : this.string_2[0] > '9');
				}
				if (!flag)
				{
					this.string_2 = string.Concat("A", this.string_2);
				}
				if (this.IsInFeatureDataset)
				{
					this.ifeatureDataset_0 = this.ifeatureWorkspace_0.CreateFeatureDataset(this.string_2, new UnknownCoordinateSystem() as ISpatialReference);
				}
				StreamReader streamReader = new StreamReader(string_3, Encoding.Default, true);
				this.method_15(streamReader.ReadToEnd(), "e");
				streamReader.Close();
				StreamReader streamReader1 = new StreamReader(string_3, Encoding.Default, true);
				string str = streamReader1.ReadLine();
				if (str.ToUpper() == "START")
				{
					this.int_0 = 5;
				}
				else if (str.ToUpper() == "CASS6")
				{
					this.int_0 = 6;
				}
				else if (str.ToUpper() == "CASS6_CD")
				{
					this.int_0 = 61;
				}
				str = streamReader1.ReadLine();
				double num = double.Parse(this.method_7(str, 0, ","));
				double num1 = double.Parse(this.method_7(str, 1, ","));
				str = streamReader1.ReadLine();
				double num2 = double.Parse(this.method_7(str, 0, ","));
				double num3 = double.Parse(this.method_7(str, 1, ","));
				IEnvelope envelopeClass = new Envelope() as IEnvelope;
				envelopeClass.PutCoords(num, num1, num2, num3);
				this.ienvelope_0 = envelopeClass;
				str = streamReader1.ReadLine();
				string str1 = "";
				if (str.ToUpper() != "END")
				{
					if ((str.IndexOf("[") < 0 ? false : str.IndexOf("]") > 0))
					{
						str1 = str.Substring(1, str.Length - 2);
					}
					while (str1.Length > 0)
					{
						str1 = this.method_5(streamReader1, str1);
					}
				}
				streamReader1.Close();
			}
		}

		internal void Read1(string string_3)
		{
			StreamReader streamReader = new StreamReader(string_3, Encoding.Default, true);
			string str = streamReader.ReadLine();
			if (str.ToUpper() == "START")
			{
				this.int_0 = 5;
			}
			else if (str.ToUpper() == "CASS6")
			{
				this.int_0 = 6;
			}
			str = streamReader.ReadLine();
			str = streamReader.ReadLine();
			str = streamReader.ReadLine();
			string str1 = "";
			if (str.ToUpper() != "END")
			{
				if ((str.IndexOf("[") < 0 ? false : str.IndexOf("]") > 0))
				{
					str1 = str.Substring(1, str.Length - 2);
				}
				while (str1.Length > 0)
				{
					str1 = this.method_17(streamReader, str1);
				}
			}
		}

		public event ESRI.ArcGIS.Geodatabase.IFeatureProgress_StepEventHandler Step
		{
			add
			{
				ESRI.ArcGIS.Geodatabase.IFeatureProgress_StepEventHandler featureProgressStepEventHandler;
				ESRI.ArcGIS.Geodatabase.IFeatureProgress_StepEventHandler ifeatureProgressStepEventHandler0 = this.ifeatureProgress_StepEventHandler_0;
				do
				{
					featureProgressStepEventHandler = ifeatureProgressStepEventHandler0;
					ESRI.ArcGIS.Geodatabase.IFeatureProgress_StepEventHandler featureProgressStepEventHandler1 = (ESRI.ArcGIS.Geodatabase.IFeatureProgress_StepEventHandler)Delegate.Combine(featureProgressStepEventHandler, value);
					ifeatureProgressStepEventHandler0 = Interlocked.CompareExchange<ESRI.ArcGIS.Geodatabase.IFeatureProgress_StepEventHandler>(ref this.ifeatureProgress_StepEventHandler_0, featureProgressStepEventHandler1, featureProgressStepEventHandler);
				}
				while ((object)ifeatureProgressStepEventHandler0 != (object)featureProgressStepEventHandler);
			}
			remove
			{
				ESRI.ArcGIS.Geodatabase.IFeatureProgress_StepEventHandler featureProgressStepEventHandler;
				ESRI.ArcGIS.Geodatabase.IFeatureProgress_StepEventHandler ifeatureProgressStepEventHandler0 = this.ifeatureProgress_StepEventHandler_0;
				do
				{
					featureProgressStepEventHandler = ifeatureProgressStepEventHandler0;
					ESRI.ArcGIS.Geodatabase.IFeatureProgress_StepEventHandler featureProgressStepEventHandler1 = (ESRI.ArcGIS.Geodatabase.IFeatureProgress_StepEventHandler)Delegate.Remove(featureProgressStepEventHandler, value);
					ifeatureProgressStepEventHandler0 = Interlocked.CompareExchange<ESRI.ArcGIS.Geodatabase.IFeatureProgress_StepEventHandler>(ref this.ifeatureProgress_StepEventHandler_0, featureProgressStepEventHandler1, featureProgressStepEventHandler);
				}
				while ((object)ifeatureProgressStepEventHandler0 != (object)featureProgressStepEventHandler);
			}
		}
	}
}