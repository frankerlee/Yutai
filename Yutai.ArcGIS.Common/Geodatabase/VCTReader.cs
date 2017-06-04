using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Shared;
using WorkspaceOperator = Yutai.ArcGIS.Common.BaseClasses.WorkspaceOperator;

namespace Yutai.ArcGIS.Common.Geodatabase
{
	public class VCTReader : IProgressMessage
	{
		private string string_0 = "1.0";

		private esriUnits esriUnits_0 = esriUnits.esriMeters;

		private string string_1 = "";

		private int int_0 = 2;

		private double double_0 = 0;

		private double double_1 = 0;

		private double double_2 = 0;

		private double double_3 = 1;

		private double double_4 = 1;

		private double double_5 = 1;

		private double double_6 = 0;

		private bool bool_0 = false;

		private bool bool_1 = false;

		private double double_7 = 500;

		private string string_2 = "M";

		private string string_3 = "";

		private string string_4 = "";

		private string string_5 = "";

		private string string_6 = ",";

		private string string_7 = "1";

		private IFeatureWorkspace ifeatureWorkspace_0 = null;

		private IFeatureDataset ifeatureDataset_0 = null;

		private IList ilist_0 = new ArrayList();

		private IList ilist_1 = new ArrayList();

		private IList ilist_2 = new ArrayList();

		private IList ilist_3 = new ArrayList();

		private IList ilist_4 = new ArrayList();

		private IList ilist_5 = new ArrayList();

		private string string_8 = "";

		private long long_0 = (long)0;

		private ProgressMessageHandle progressMessageHandle_0;

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
			}
		}

		public VCTReader()
		{
		}

		~VCTReader()
		{
			this.ifeatureWorkspace_0 = null;
			GC.Collect();
		}

		internal ITextElement MakeTextElement(string string_9, IPoint ipoint_0)
		{
		    ITextElement element = new TextElement() as ITextElement;
		    element.ScaleText = true;
		    element.Text = string_9;
		    ITextElement textElementClass = element as ITextElement;
			(textElementClass as IGroupSymbolElement).SymbolID = 0;
			(textElementClass as IElement).Geometry = ipoint_0;
			return textElementClass;
		}

        private ISpatialReference method_0()
        {
            ISpatialReference spatialReference = null;
            if (!(this.string_2 == "G"))
            {
            }
            SpatialReferenctOperator.ChangeCoordinateSystem(this.ifeatureWorkspace_0 as IGeodatabaseRelease, spatialReference, false);
            spatialReference.SetDomain(this.double_0, this.double_3, this.double_1, this.double_4);
            return spatialReference;
        }


        private string method_1(string string_9)
		{
			return WorkspaceOperator.GetFinalName(this.ifeatureWorkspace_0 as IWorkspace, esriDatasetType.esriDTFeatureClass, string_9);
		}

		private IFeatureClass method_10(string string_9, int int_1)
		{
			int i;
			IFeatureClass featureClass;
			if (int_1 == 1)
			{
				for (i = 0; i < this.ilist_0.Count; i++)
				{
					VCTReader.FeatureCode item = this.ilist_0[i] as VCTReader.FeatureCode;
					if (item.CodeName.ToUpper() == string_9.ToUpper())
					{
						string_9 = item.AttributeTable.ToUpper();
					}
				}
			}
			i = 0;
			while (true)
			{
				if (i < this.ilist_2.Count)
				{
					if (((string)this.ilist_2[i]).ToUpper() == string_9)
					{
						try
						{
							string str = this.ilist_5[i] as string;
							featureClass = this.ifeatureWorkspace_0.OpenFeatureClass(str);
							break;
						}
						catch
						{
						}
					}
					i++;
				}
				else
				{
					featureClass = null;
					break;
				}
			}
			return featureClass;
		}

		private IFeatureClass method_11(string string_9, out IFields ifields_0)
		{
			IFeatureClass featureClass;
			ifields_0 = null;
			int num = 0;
			while (true)
			{
				if (num >= this.ilist_2.Count)
				{
					featureClass = null;
					break;
				}
				else if (this.ilist_2[num].ToString().Trim().ToUpper() == string_9.Trim().ToUpper())
				{
					ifields_0 = this.ilist_3[num] as IFields;
					featureClass = this.ifeatureWorkspace_0.OpenFeatureClass(this.ilist_5[num] as string);
					break;
				}
				else
				{
					num++;
				}
			}
			return featureClass;
		}

		private void method_12(string string_9, out esriGeometryType esriGeometryType_0, out esriFeatureType esriFeatureType_0)
		{
			esriFeatureType_0 = esriFeatureType.esriFTSimple;
			esriGeometryType_0 = esriGeometryType.esriGeometryPoint;
			for (int i = 0; i < this.ilist_0.Count; i++)
			{
				VCTReader.FeatureCode item = this.ilist_0[i] as VCTReader.FeatureCode;
				if (item.AttributeTable.ToUpper() == string_9.ToUpper())
				{
					string upper = item.GeometryType.ToUpper();
					if (upper != null)
					{
						if (upper == "POLYGON")
						{
							esriFeatureType_0 = esriFeatureType.esriFTSimple;
							esriGeometryType_0 = esriGeometryType.esriGeometryPolygon;
						}
						else if (upper == "LINE")
						{
							esriFeatureType_0 = esriFeatureType.esriFTSimple;
							esriGeometryType_0 = esriGeometryType.esriGeometryPolyline;
						}
						else if (upper == "POINT")
						{
							esriFeatureType_0 = esriFeatureType.esriFTSimple;
							esriGeometryType_0 = esriGeometryType.esriGeometryPoint;
						}
						else if (upper == "ANNOTATION")
						{
							esriFeatureType_0 = esriFeatureType.esriFTAnnotation;
							esriGeometryType_0 = esriGeometryType.esriGeometryPolygon;
						}
					}
				}
			}
		}

		private IFeatureClass method_13(string string_9, IFeatureDataset ifeatureDataset_1, double double_8, ITextSymbol itextSymbol_0, IFields ifields_0)
		{
			int num;
			IGeometryDefEdit geometryDef;
			ISpatialReference spatialReference;
			int i;
			IFeatureClass featureClass;
			if ((this.ifeatureWorkspace_0 as IWorkspace).Type != esriWorkspaceType.esriFileSystemWorkspace)
			{
				IObjectClassDescription annotationFeatureClassDescriptionClass = new AnnotationFeatureClassDescription();
				IFeatureClassDescription featureClassDescription = annotationFeatureClassDescriptionClass as IFeatureClassDescription;
				IFields field = (annotationFeatureClassDescriptionClass.RequiredFields as IClone).Clone() as IFields;
				IFieldEdit fieldEdit = null;
				num = field.FindField(featureClassDescription.ShapeFieldName);
				fieldEdit = field.Field[num] as IFieldEdit;
				geometryDef = fieldEdit.GeometryDef as IGeometryDefEdit;
				if (ifeatureDataset_1 == null)
				{
					spatialReference = geometryDef.SpatialReference;
					SpatialReferenctOperator.ChangeCoordinateSystem(this.ifeatureWorkspace_0 as IGeodatabaseRelease, spatialReference, false);
					spatialReference.SetDomain(this.double_0, this.double_3, this.double_1, this.double_4);
				}
				else
				{
					spatialReference = (ifeatureDataset_1 as IGeoDataset).SpatialReference;
				}
				geometryDef.SpatialReference_2 = spatialReference;
				fieldEdit.GeometryDef_2 = geometryDef;
				IFeatureWorkspaceAnno ifeatureWorkspace0 = this.ifeatureWorkspace_0 as IFeatureWorkspaceAnno;
				IGraphicsLayerScale graphicsLayerScaleClass = new GraphicsLayerScale()
				{
					ReferenceScale = double_8,
					Units = esriUnits.esriMeters
				};
				UID instanceCLSID = annotationFeatureClassDescriptionClass.InstanceCLSID;
				UID classExtensionCLSID = annotationFeatureClassDescriptionClass.ClassExtensionCLSID;
				ISymbolCollection symbolCollectionClass = new SymbolCollection();
				symbolCollectionClass.Symbol[0] = itextSymbol_0 as ISymbol;
				IAnnotateLayerPropertiesCollection2 annotateLayerPropertiesCollectionClass = new AnnotateLayerPropertiesCollection() as IAnnotateLayerPropertiesCollection2;
				IAnnotateLayerProperties labelEngineLayerPropertiesClass = new LabelEngineLayerProperties() as IAnnotateLayerProperties;
			    labelEngineLayerPropertiesClass.Class = "要素类 1";
			    labelEngineLayerPropertiesClass.FeatureLinked = false;
			    labelEngineLayerPropertiesClass.AddUnplacedToGraphicsContainer = false;
			    labelEngineLayerPropertiesClass.CreateUnplacedElements = true;
			    labelEngineLayerPropertiesClass.DisplayAnnotation = true;
			    labelEngineLayerPropertiesClass.UseOutput = true;
			    ILabelEngineLayerProperties itextSymbol0 = labelEngineLayerPropertiesClass as ILabelEngineLayerProperties;
				itextSymbol0.Offset = 0;
				itextSymbol0.SymbolID = 0;
				itextSymbol0.Symbol = itextSymbol_0;
				annotateLayerPropertiesCollectionClass.Add(labelEngineLayerPropertiesClass);
				for (i = 0; i < ifields_0.FieldCount; i++)
				{
					(field as IFieldsEdit).AddField(ifields_0.Field[i]);
				}
				try
				{
					IFeatureClass featureClass1 = ifeatureWorkspace0.CreateAnnotationClass(string_9, field, instanceCLSID, classExtensionCLSID, featureClassDescription.ShapeFieldName, "", ifeatureDataset_1, null, annotateLayerPropertiesCollectionClass, graphicsLayerScaleClass, symbolCollectionClass, true);
					featureClass = featureClass1;
					return featureClass;
				}
				catch (Exception exception)
				{
					Logger.Current.Error("", exception, "");
				}
				featureClass = null;
			}
			else
			{
				IFeatureClass featureClass2 = null;
				IObjectClassDescription featureClassDescriptionClass = new FeatureClassDescription();
				string shapeFieldName = (featureClassDescriptionClass as IFeatureClassDescription).ShapeFieldName;
				IFieldsEdit requiredFields = featureClassDescriptionClass.RequiredFields as IFieldsEdit;
				IFieldEdit field1 = null;
				num = requiredFields.FindField((featureClassDescriptionClass as IFeatureClassDescription).ShapeFieldName);
				field1 = requiredFields.Field[num] as IFieldEdit;
				geometryDef = field1.GeometryDef as IGeometryDefEdit;
				if (this.ifeatureDataset_0 == null)
				{
					spatialReference = geometryDef.SpatialReference;
					SpatialReferenctOperator.ChangeCoordinateSystem(this.ifeatureWorkspace_0 as IGeodatabaseRelease, spatialReference, false);
					spatialReference.SetDomain(this.double_0, this.double_3, this.double_1, this.double_4);
				}
				else
				{
					spatialReference = (this.ifeatureDataset_0 as IGeoDataset).SpatialReference;
				}
				geometryDef.GeometryType_2 = esriGeometryType.esriGeometryPoint;
				field1.GeometryDef_2 = geometryDef;
				for (i = 0; i < ifields_0.FieldCount; i++)
				{
					requiredFields.AddField(ifields_0.Field[i]);
				}
				try
				{
					featureClass2 = (this.ifeatureDataset_0 == null ? this.ifeatureWorkspace_0.CreateFeatureClass(string_9, requiredFields, null, null, esriFeatureType.esriFTSimple, shapeFieldName, "") : this.ifeatureDataset_0.CreateFeatureClass(string_9, requiredFields, null, null, esriFeatureType.esriFTSimple, shapeFieldName, ""));
				}
				catch (Exception exception1)
				{
					Logger.Current.Error("", exception1, "");
				}
				featureClass = featureClass2;
			}
			return featureClass;
		}

		private void method_14(StreamReader streamReader_0)
		{
			esriGeometryType _esriGeometryType;
			esriFeatureType _esriFeatureType;
			while (streamReader_0.Peek() >= 0)
			{
				string str = streamReader_0.ReadLine().Trim();
				VCTReader long0 = this;
				long0.long_0 = long0.long_0 + (long)1;
				if (str.Length == 0)
				{
					continue;
				}
				if (str.ToUpper() == "TABLESTRUCTUREEND")
				{
					break;
				}
				string[] strArrays = str.Split(this.string_6.ToCharArray());
				string str1 = strArrays[0].Trim();
				int num = int.Parse(strArrays[1].Trim());
				IFieldsEdit fieldsClass = null;
                fieldsClass = new ESRI.ArcGIS.Geodatabase.Fields() as IFieldsEdit;
                IFieldEdit fieldClass = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
			    fieldClass.Name_2 = "MBBSM_VCT";
			    fieldClass.Type_2 = esriFieldType.esriFieldTypeInteger;
			    fieldsClass.AddField(fieldClass);
				for (int i = 0; i < num; i++)
				{
                    fieldClass = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                    str = streamReader_0.ReadLine().Trim();
					VCTReader vCTReader = this;
					vCTReader.long_0 = vCTReader.long_0 + (long)1;
					strArrays = str.Split(this.string_6.ToCharArray());
					fieldClass.Name_2 = strArrays[0].Trim();
					string upper = strArrays[1].Trim().ToUpper();
					if (upper != null)
					{
						switch (upper)
						{
							case "INTEGER":
							{
								fieldClass.Type_2 = esriFieldType.esriFieldTypeInteger;
								break;
							}
							case "FLOAT":
							{
								fieldClass.Type_2 = esriFieldType.esriFieldTypeSingle;
								try
								{
									if ((int)strArrays.Length >= 3)
									{
										fieldClass.Precision_2 = int.Parse(strArrays[2].Trim());
									}
									if ((int)strArrays.Length >= 4)
									{
										fieldClass.Scale_2 = int.Parse(strArrays[3].Trim());
									}
									break;
								}
								catch
								{
									break;
								}
								break;
							}
							case "CHAR":
							{
								fieldClass.Type_2 = esriFieldType.esriFieldTypeString;
								fieldClass.Length_2 = 10;
								try
								{
									if ((int)strArrays.Length >= 3)
									{
										fieldClass.Length_2 = int.Parse(strArrays[2].Trim());
									}
									break;
								}
								catch
								{
									break;
								}
								break;
							}
							case "DATE":
							{
								fieldClass.Type_2 = esriFieldType.esriFieldTypeDate;
								break;
							}
							case "TIME":
							{
								fieldClass.Type_2 = esriFieldType.esriFieldTypeString;
								fieldClass.Length_2 = 100;
								break;
							}
							case "VARCHAR":
							{
								fieldClass.Type_2 = esriFieldType.esriFieldTypeString;
								fieldClass.Length_2 = 100;
								try
								{
									if ((int)strArrays.Length >= 3)
									{
										fieldClass.Length_2 = int.Parse(strArrays[2].Trim());
									}
									break;
								}
								catch
								{
									break;
								}
								break;
							}
							case "VARBIN":
							{
								fieldClass.Type_2 = esriFieldType.esriFieldTypeString;
								fieldClass.Length_2 = 150;
								break;
							}
						}
					}
					fieldsClass.AddField(fieldClass);
				}
				this.method_12(str1, out _esriGeometryType, out _esriFeatureType);
				this.ilist_3.Add(fieldsClass);
				this.ilist_2.Add(str1);
				IFeatureClass featureClass = this.method_2(str1, _esriFeatureType, _esriGeometryType, fieldsClass);
				if (featureClass == null)
				{
					this.ilist_5.Add("");
				}
				else
				{
					this.ilist_5.Add((featureClass as IDataset).Name);
				}
				featureClass = null;
			}
		}

		private void method_15(StreamReader streamReader_0, string string_9)
		{
			IFields field;
			string str;
			int i;
			int item;
			Exception exception;
			IFeatureClass featureClass = this.method_11(string_9, out field);
			if (featureClass != null)
			{
				List<int> nums = new List<int>();
				for (i = 0; i < field.FieldCount; i++)
				{
					item = featureClass.FindField(field.Field[i].Name);
					nums.Add(item);
				}
				int num = 0;
				int num1 = 0;
				int num2 = 1;
				while (streamReader_0.Peek() >= 0)
				{
					str = streamReader_0.ReadLine().Trim();
					VCTReader long0 = this;
					long0.long_0 = long0.long_0 + (long)1;
					if (str.Length == 0)
					{
						continue;
					}
					if (str.ToUpper() == "TABLEEND")
					{
						featureClass = null;
						return;
					}
					else
					{
						num++;
						string[] strArrays = str.Split(this.string_6.ToCharArray());
						IQueryFilter queryFilterClass = new QueryFilter();
						if ((int)strArrays.Length != field.FieldCount - 1)
						{
							num1 = 1;
							queryFilterClass.WhereClause = string.Concat("MBBSM_VCT = ", strArrays[0].Trim());
						}
						else
						{
							queryFilterClass.WhereClause = string.Concat("MBBSM_VCT = ", num.ToString());
							num1 = 0;
						}
						try
						{
							IFeatureCursor featureCursor = featureClass.Update(queryFilterClass, false);
							IFeature feature = featureCursor.NextFeature();
							Marshal.ReleaseComObject(featureCursor);
							featureCursor = null;
							if (feature != null)
							{
								num2 = 1;
								i = num1;
								while (i < (int)strArrays.Length)
								{
									item = nums[num2];
									if (item != -1 && strArrays[i].Trim() != "")
									{
										feature.Value[item] = strArrays[i].Trim();
									}
									i++;
									num2++;
								}
								try
								{
									feature.Store();
								}
								catch (Exception exception1)
								{
									exception = exception1;
									IList ilist4 = this.ilist_4;
									string[] string9 = new string[] { "[", string_9, "] 中对象编码为[", null, null, null };
									string9[3] = feature.OID.ToString();
									string9[4] = "]的属性数据错误。原因:";
									string9[5] = exception.Message;
									ilist4.Add(string.Concat(string9));
									Logger.Current.Error("", exception, string.Concat(featureClass.AliasName, "_", str));
								}
								Marshal.ReleaseComObject(feature);
								feature = null;
							}
						}
						catch (Exception exception2)
						{
							exception = exception2;
							Logger.Current.Error("", exception, string.Concat(featureClass.AliasName, "_", str));
						}
					}
				}
				Marshal.ReleaseComObject(featureClass);
				featureClass = null;
			}
			else
			{
				while (streamReader_0.Peek() >= 0)
				{
					str = streamReader_0.ReadLine().Trim();
					VCTReader vCTReader = this;
					vCTReader.long_0 = vCTReader.long_0 + (long)1;
					if (str.Length != 0 && str.ToUpper() == "TABLEEND")
					{
						return;
					}
				}
			}
		}

		private IFeatureClass method_2(string string_9, esriFeatureType esriFeatureType_0, esriGeometryType esriGeometryType_0, IFields ifields_0)
		{
			string str;
			IFeatureClass featureClass;
			ISpatialReference spatialReference;
			IFeatureClass featureClass1 = null;
			string_9 = string.Concat(this.string_8, "_", string_9);
			IFieldChecker fieldCheckerClass = new FieldChecker()
			{
				ValidateWorkspace = this.ifeatureWorkspace_0 as IWorkspace
			};
			fieldCheckerClass.ValidateTableName(string_9, out str);
			string_9 = this.method_1(str);
			IObjectClassDescription featureClassDescriptionClass = null;
			if (esriFeatureType_0 != esriFeatureType.esriFTAnnotation)
			{
				featureClassDescriptionClass = new FeatureClassDescription();
				IFieldsEdit requiredFields = featureClassDescriptionClass.RequiredFields as IFieldsEdit;
				IFieldEdit field = null;
				int num = requiredFields.FindField((featureClassDescriptionClass as IFeatureClassDescription).ShapeFieldName);
				field = requiredFields.Field[num] as IFieldEdit;
				IGeometryDefEdit geometryDef = field.GeometryDef as IGeometryDefEdit;
				if (this.int_0 == 3)
				{
					geometryDef.HasZ_2 = true;
				}
				geometryDef.GeometryType_2 = esriGeometryType_0;
				string shapeFieldName = (featureClassDescriptionClass as IFeatureClassDescription).ShapeFieldName;
				if (this.ifeatureDataset_0 == null)
				{
					spatialReference = geometryDef.SpatialReference;
					SpatialReferenctOperator.ChangeCoordinateSystem(this.ifeatureWorkspace_0 as IGeodatabaseRelease, spatialReference, false);
					spatialReference.SetDomain(this.double_0, this.double_3, this.double_1, this.double_4);
				}
				else
				{
					spatialReference = (this.ifeatureDataset_0 as IGeoDataset).SpatialReference;
				}
				geometryDef.SpatialReference_2 = spatialReference;
				esriFeatureType _esriFeatureType = esriFeatureType.esriFTSimple;
				field.GeometryDef_2 = geometryDef;
				for (int i = 0; i < ifields_0.FieldCount; i++)
				{
					requiredFields.AddField(ifields_0.Field[i]);
				}
				try
				{
					featureClass1 = (this.ifeatureDataset_0 == null ? this.ifeatureWorkspace_0.CreateFeatureClass(string_9, requiredFields, null, null, _esriFeatureType, shapeFieldName, "") : this.ifeatureDataset_0.CreateFeatureClass(string_9, requiredFields, null, null, _esriFeatureType, shapeFieldName, ""));
				}
				catch (Exception exception)
				{
					Logger.Current.Error("", exception, "");
				}
				featureClass = featureClass1;
			}
			else
			{
				featureClass = this.method_13(string_9, this.ifeatureDataset_0, this.double_7, new TextSymbol(), ifields_0);
			}
			return featureClass;
		}

		private void method_3(StreamReader streamReader_0)
		{
			string str;
			while (streamReader_0.Peek() >= 0)
			{
				string str1 = streamReader_0.ReadLine().ToUpper().Trim();
				VCTReader long0 = this;
				long0.long_0 = long0.long_0 + (long)1;
				if (str1.Length == 0)
				{
					continue;
				}
				if (str1 == "HEADEND")
				{
					break;
				}
				if (str1.IndexOf("VERSION") == 0)
				{
					this.string_0 = str1.Substring(str1.IndexOf(":") + 1).Trim();
				}
				else if (str1.IndexOf("UNIT") == 0)
				{
					str = str1.Substring(str1.IndexOf(":") + 1).Trim();
					string str2 = str;
					if (str2 == null)
					{
						continue;
					}
					if (str2 == "M")
					{
						this.esriUnits_0 = esriUnits.esriMeters;
					}
					else if (str2 == "K")
					{
						this.esriUnits_0 = esriUnits.esriKilometers;
					}
					else if (str2 == "D")
					{
						this.esriUnits_0 = esriUnits.esriDecimalDegrees;
					}
					else
					{
						if (str2 != "S")
						{
							continue;
						}
						this.esriUnits_0 = esriUnits.esriUnknownUnits;
						this.string_1 = "S";
					}
				}
				else if (str1.IndexOf("DIM") == 0)
				{
					str = str1.Substring(str1.IndexOf(":") + 1).Trim();
					if (str != "2")
					{
						if (str != "3")
						{
							continue;
						}
						this.int_0 = 3;
					}
					else
					{
						this.int_0 = 2;
					}
				}
				else if (str1.IndexOf("TOPO") == 0)
				{
					this.string_7 = str1.Substring(str1.IndexOf(":") + 1).Trim();
				}
				else if (str1.IndexOf("COORDINATE") == 0)
				{
					str = str1.Substring(str1.IndexOf(":") + 1).Trim();
					this.string_2 = str;
				}
				else if (str1.IndexOf("PROJECTION") == 0)
				{
					str = str1.Substring(str1.IndexOf(":") + 1).Trim();
					this.string_3 = str;
				}
				else if (str1.IndexOf("SPHEROID") == 0)
				{
					str = str1.Substring(str1.IndexOf(":") + 1).Trim();
					this.string_4 = str;
				}
				else if (str1.IndexOf("SEPARATOR") == 0)
				{
					str = str1.Substring(str1.IndexOf(":") + 1).Trim();
					this.string_6 = str;
				}
				else if (str1.IndexOf("MINX") == 0)
				{
					this.double_0 = double.Parse(str1.Substring(str1.IndexOf(":") + 1).Trim()) - 100;
					this.bool_1 = true;
				}
				else if (str1.IndexOf("MINY") == 0)
				{
					this.double_1 = double.Parse(str1.Substring(str1.IndexOf(":") + 1).Trim()) - 100;
					this.bool_1 = true;
				}
				else if (str1.IndexOf("MINZ") == 0)
				{
					this.double_2 = double.Parse(str1.Substring(str1.IndexOf(":") + 1).Trim()) - 100;
					this.bool_0 = true;
				}
				else if (str1.IndexOf("MAXX") == 0)
				{
					this.double_3 = double.Parse(str1.Substring(str1.IndexOf(":") + 1).Trim()) + 100;
					this.bool_1 = true;
				}
				else if (str1.IndexOf("MAXY") == 0)
				{
					this.double_4 = double.Parse(str1.Substring(str1.IndexOf(":") + 1).Trim()) + 100;
					this.bool_1 = true;
				}
				else if (str1.IndexOf("MAXZ") != 0)
				{
					if (str1.IndexOf("MERIDIAN") != 0)
					{
						continue;
					}
					this.double_6 = double.Parse(str1.Substring(str1.IndexOf(":") + 1).Trim());
				}
				else
				{
					this.double_5 = double.Parse(str1.Substring(str1.IndexOf(":") + 1).Trim()) + 100;
					this.bool_0 = true;
				}
			}
		}

		private void method_4(StreamReader streamReader_0)
		{
			IFeatureClass featureClass = null;
			string str = "";
			int num = -1;
			while (true)
			{
				if (streamReader_0.Peek() >= 0)
				{
					string str1 = streamReader_0.ReadLine().Trim();
					VCTReader long0 = this;
					long0.long_0 = long0.long_0 + (long)1;
					if (str1.Length != 0)
					{
						if (str1.ToUpper() == "ANNOTATIONEND")
						{
							if (featureClass == null)
							{
								break;
							}
							Marshal.ReleaseComObject(featureClass);
							featureClass = null;
							break;
						}
						else
						{
							string str2 = str1;
							string str3 = streamReader_0.ReadLine().Trim();
							VCTReader vCTReader = this;
							vCTReader.long_0 = vCTReader.long_0 + (long)1;
							string str4 = streamReader_0.ReadLine().Trim();
							VCTReader long01 = this;
							long01.long_0 = long01.long_0 + (long)1;
							if (str != str4)
							{
								if (featureClass != null)
								{
									Marshal.ReleaseComObject(featureClass);
									featureClass = null;
								}
								featureClass = this.method_10(str3, 1);
								if (featureClass != null)
								{
									num = featureClass.FindField("MBBSM_VCT");
								}
								str = str4;
							}
							if (featureClass != null)
							{
								IFeature feature = featureClass.CreateFeature();
								feature.Value[num] = str2;
								if (featureClass.FeatureType != esriFeatureType.esriFTSimple)
								{
									string str5 = streamReader_0.ReadLine().Trim();
									VCTReader vCTReader1 = this;
									vCTReader1.long_0 = vCTReader1.long_0 + (long)1;
									int num1 = int.Parse(streamReader_0.ReadLine().Trim());
									VCTReader long02 = this;
									long02.long_0 = long02.long_0 + (long)1;
									str1 = streamReader_0.ReadLine().Trim();
									VCTReader vCTReader2 = this;
									vCTReader2.long_0 = vCTReader2.long_0 + (long)1;
									string[] strArrays = str1.Split(this.string_6.ToCharArray());
									int num2 = int.Parse(strArrays[0]);
									int num3 = int.Parse(strArrays[1]);
									string str6 = strArrays[2].Trim();
									str1 = streamReader_0.ReadLine().Trim();
									VCTReader long03 = this;
									long03.long_0 = long03.long_0 + (long)1;
									strArrays = str1.Split(this.string_6.ToCharArray());
									double num4 = 0;
									double num5 = 0;
									bool flag = false;
									if ((int)strArrays.Length != 1)
									{
										num5 = double.Parse(strArrays[0]);
										double.Parse(strArrays[1]);
									}
									else
									{
										num4 = double.Parse(strArrays[0]);
										flag = true;
									}
									str1 = streamReader_0.ReadLine().Trim();
									VCTReader vCTReader3 = this;
									vCTReader3.long_0 = vCTReader3.long_0 + (long)1;
									double.Parse(str1);
									string str7 = streamReader_0.ReadLine().Trim();
									VCTReader long04 = this;
									long04.long_0 = long04.long_0 + (long)1;
									str1 = streamReader_0.ReadLine().Trim();
									VCTReader vCTReader4 = this;
									vCTReader4.long_0 = vCTReader4.long_0 + (long)1;
									int num6 = int.Parse(str1);
									IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
									for (int i = 0; i < num6; i++)
									{
										str1 = streamReader_0.ReadLine();
										VCTReader long05 = this;
										long05.long_0 = long05.long_0 + (long)1;
										string[] strArrays1 = str1.Split(this.string_6.ToCharArray());
										pointClass.X = double.Parse(strArrays1[0]);
										pointClass.Y = double.Parse(strArrays1[1]);
									}
									ITextElement textElement = this.MakeTextElement(str7, pointClass);
									IRgbColor rgbColorClass = new RgbColor()
									{
										RGB = num1
									};
									textElement.Symbol.Color = rgbColorClass;
									try
									{
										textElement.Symbol.Font.Name = str5;
										textElement.Symbol.Font.Weight = (short)num2;
										if ((str6 == "T" ? true : str6 == "Y"))
										{
											textElement.Symbol.Font.Underline = true;
										}
										if ((num3 == 1 ? true : num3 == 2))
										{
											textElement.Symbol.Font.Italic = true;
										}
										if (flag)
										{
											if (num4 > 0)
											{
												textElement.Symbol.Font.Size = (decimal)((double)num4);
											}
										}
										else if (num5 > 0)
										{
											textElement.Symbol.Font.Size = (decimal)((double)num5);
										}
									}
									catch (Exception exception)
									{
									}
									try
									{
										IAnnotationFeature2 annotationFeature2 = feature as IAnnotationFeature2;
										annotationFeature2.LinkedFeatureID = -1;
										annotationFeature2.AnnotationClassID = 0;
										annotationFeature2.Annotation = textElement as IElement;
										feature.Store();
									}
									catch (Exception exception2)
									{
										Exception exception1 = exception2;
										IList ilist4 = this.ilist_4;
										string[] message = new string[] { "[", str3, "] 中对象编码为[", null, null, null };
										message[3] = feature.OID.ToString();
										message[4] = "]的几何数据错误。原因:";
										message[5] = exception1.Message;
										ilist4.Add(string.Concat(message));
										feature.Delete();
									}
								}
							}
						}
					}
				}
				else
				{
					break;
				}
			}
		}

		private void method_5(StreamReader streamReader_0)
		{
			while (streamReader_0.Peek() >= 0)
			{
				string str = streamReader_0.ReadLine().Trim();
				VCTReader long0 = this;
				long0.long_0 = long0.long_0 + (long)1;
				if (str.Length == 0)
				{
					continue;
				}
				if (str.ToUpper() == "ATTRIBUTEEND")
				{
					break;
				}
				this.method_15(streamReader_0, str);
			}
		}

		private void method_6(StreamReader streamReader_0)
		{
			while (streamReader_0.Peek() >= 0)
			{
				string str = streamReader_0.ReadLine().Trim();
				VCTReader long0 = this;
				long0.long_0 = long0.long_0 + (long)1;
				if (str.Length == 0)
				{
					continue;
				}
				if (str.ToUpper() == "FEATURECODEEND")
				{
					break;
				}
				VCTReader.FeatureCode featureCode = new VCTReader.FeatureCode();
				string[] strArrays = str.Split(this.string_6.ToCharArray());
				featureCode.CodeName = strArrays[0].Trim();
				featureCode.FeatureName = strArrays[1].Trim();
				featureCode.GeometryType = strArrays[2].Trim();
				featureCode.Color = int.Parse(strArrays[3].Trim());
				featureCode.AttributeTable = strArrays[4].Trim();
				if ((int)strArrays.Length >= 6)
				{
					featureCode.Tag = strArrays[5].Trim();
				}
				this.ilist_0.Add(featureCode);
			}
		}

		private void method_7(StreamReader streamReader_0)
		{
			string[] strArrays;
			IFeatureClass featureClass = null;
			string str = "";
			object value = Missing.Value;
			int num = -1;
			while (streamReader_0.Peek() >= 0)
			{
				string str1 = streamReader_0.ReadLine().Trim();
				VCTReader long0 = this;
				long0.long_0 = long0.long_0 + (long)1;
				if (str1.Length == 0)
				{
					continue;
				}
				if (str1.ToUpper() == "LINEEND")
				{
					if (featureClass == null)
					{
						break;
					}
					Marshal.ReleaseComObject(featureClass);
					featureClass = null;
					break;
				}
				else
				{
					string str2 = str1;
					string str3 = streamReader_0.ReadLine().Trim();
					VCTReader vCTReader = this;
					vCTReader.long_0 = vCTReader.long_0 + (long)1;
					string str4 = streamReader_0.ReadLine().Trim();
					VCTReader long01 = this;
					long01.long_0 = long01.long_0 + (long)1;
					if (str != str4)
					{
						if (featureClass != null)
						{
							Marshal.ReleaseComObject(featureClass);
							featureClass = null;
						}
						featureClass = this.method_10(str3, 1);
						if (featureClass == null)
						{
							this.ilist_4.Add(string.Concat("[", str3, "] 错误，无法找到对应的要素类！"));
						}
						else
						{
							num = featureClass.FindField("MBBSM_VCT");
						}
						str = str4;
					}
					if (featureClass == null)
					{
						continue;
					}
					IFeature feature = featureClass.CreateFeature();
					feature.Value[num] = str2;
					string str5 = streamReader_0.ReadLine().Trim();
					VCTReader vCTReader1 = this;
					vCTReader1.long_0 = vCTReader1.long_0 + (long)1;
					if (str5 != "100")
					{
						int num1 = int.Parse(streamReader_0.ReadLine().Trim());
						VCTReader long02 = this;
						long02.long_0 = long02.long_0 + (long)1;
						IPointCollection polylineClass = new Polyline();
						if (this.int_0 == 3)
						{
							(polylineClass as IZAware).ZAware = true;
						}
						if (!(str5 == "1" || str5 == "5" ? false : !(str5 == "6")))
						{
							for (int i = 0; i < num1; i++)
							{
								str1 = streamReader_0.ReadLine().Trim();
								VCTReader vCTReader2 = this;
								vCTReader2.long_0 = vCTReader2.long_0 + (long)1;
								IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
								strArrays = str1.Split(this.string_6.ToCharArray());
								pointClass.X = double.Parse(strArrays[0]);
								pointClass.Y = double.Parse(strArrays[1]);
								if (this.int_0 == 3 && (int)strArrays.Length >= 3)
								{
									pointClass.Z = double.Parse(strArrays[2]);
								}
								polylineClass.AddPoint(pointClass, ref value, ref value);
							}
							if ((str5 == "5" ? true : str5 == "6"))
							{
								(polylineClass as IPolycurve).Smooth(0);
							}
						}
						else if (str5 == "2")
						{
							IConstructCircularArc circularArcClass = new CircularArc() as IConstructCircularArc;
							str1 = streamReader_0.ReadLine().Trim();
							VCTReader long03 = this;
							long03.long_0 = long03.long_0 + (long)1;
							strArrays = str1.Split(this.string_6.ToCharArray());
							IPoint point = new Point()
							{
								X = double.Parse(strArrays[0]),
								Y = double.Parse(strArrays[1])
							};
							if (this.int_0 == 3 && (int)strArrays.Length >= 3)
							{
								point.Z = double.Parse(strArrays[2]);
							}
							str1 = streamReader_0.ReadLine().Trim();
							VCTReader vCTReader3 = this;
							vCTReader3.long_0 = vCTReader3.long_0 + (long)1;
							strArrays = str1.Split(this.string_6.ToCharArray());
							IPoint pointClass1 = new Point()
							{
								X = double.Parse(strArrays[0]),
								Y = double.Parse(strArrays[1])
							};
							if (this.int_0 == 3 && (int)strArrays.Length >= 3)
							{
								pointClass1.Z = double.Parse(strArrays[2]);
							}
							str1 = streamReader_0.ReadLine().Trim();
							VCTReader long04 = this;
							long04.long_0 = long04.long_0 + (long)1;
							strArrays = str1.Split(this.string_6.ToCharArray());
							IPoint point1 = new Point()
							{
								X = double.Parse(strArrays[0]),
								Y = double.Parse(strArrays[1])
							};
							if (this.int_0 == 3 && (int)strArrays.Length >= 3)
							{
								point1.Z = double.Parse(strArrays[2]);
							}
							circularArcClass.ConstructThreePoints(point, pointClass1, point1, false);
							(polylineClass as ISegmentCollection).AddSegment(circularArcClass as ISegment, ref value, ref value);
						}
						else if (str5 == "3")
						{
						}
						try
						{
							feature.Shape = polylineClass as IGeometry;
							feature.Store();
						}
						catch (Exception exception1)
						{
							Exception exception = exception1;
							IList ilist4 = this.ilist_4;
							string[] message = new string[] { "[", str3, "] 中对象编码为[", null, null, null };
							int oID = feature.OID;
							message[3] = oID.ToString();
							message[4] = "]的几何数据错误。原因:";
							message[5] = exception.Message;
							ilist4.Add(string.Concat(message));
							oID = feature.OID;
							Logger.Current.Error("", exception, oID.ToString());
							feature.Delete();
						}
					}
					if (this.string_7 != "2")
					{
						continue;
					}
					str1 = streamReader_0.ReadLine().Trim();
					VCTReader vCTReader4 = this;
					vCTReader4.long_0 = vCTReader4.long_0 + (long)1;
					str1 = streamReader_0.ReadLine().Trim();
					VCTReader long05 = this;
					long05.long_0 = long05.long_0 + (long)1;
					str1 = streamReader_0.ReadLine().Trim();
					VCTReader vCTReader5 = this;
					vCTReader5.long_0 = vCTReader5.long_0 + (long)1;
					str1 = streamReader_0.ReadLine().Trim();
					VCTReader long06 = this;
					long06.long_0 = long06.long_0 + (long)1;
				}
			}
		}

		private void method_8(StreamReader streamReader_0)
		{
			IFeatureClass featureClass = null;
			string str = "";
			int num = -1;
			while (true)
			{
				if (streamReader_0.Peek() >= 0)
				{
					string str1 = streamReader_0.ReadLine().Trim();
					VCTReader long0 = this;
					long0.long_0 = long0.long_0 + (long)1;
					if (str1.Length != 0)
					{
						if (str1.ToUpper() == "POINTEND")
						{
							if (featureClass == null)
							{
								break;
							}
							Marshal.ReleaseComObject(featureClass);
							featureClass = null;
							break;
						}
						else
						{
							string str2 = str1;
							string str3 = streamReader_0.ReadLine().Trim();
							VCTReader vCTReader = this;
							vCTReader.long_0 = vCTReader.long_0 + (long)1;
							string str4 = streamReader_0.ReadLine().Trim();
							VCTReader long01 = this;
							long01.long_0 = long01.long_0 + (long)1;
							if (str != str4)
							{
								if (featureClass != null)
								{
									Marshal.ReleaseComObject(featureClass);
									featureClass = null;
								}
								featureClass = this.method_10(str3, 1);
								if (featureClass == null)
								{
									this.ilist_4.Add(string.Concat("[", str3, "] 错误，无法找到对应的要素类！"));
								}
								else
								{
									num = featureClass.FindField("MBBSM_VCT");
								}
								str = str4;
							}
							if (featureClass != null)
							{
								IFeature feature = featureClass.CreateFeature();
								feature.Value[num] = str2;
								string str5 = streamReader_0.ReadLine().Trim();
								VCTReader vCTReader1 = this;
								vCTReader1.long_0 = vCTReader1.long_0 + (long)1;
								str1 = streamReader_0.ReadLine().Trim();
								VCTReader long02 = this;
								long02.long_0 = long02.long_0 + (long)1;
								IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
								string[] strArrays = str1.Split(this.string_6.ToCharArray());
								pointClass.X = double.Parse(strArrays[0]);
								pointClass.Y = double.Parse(strArrays[1]);
								if (this.int_0 == 3)
								{
									(pointClass as IZAware).ZAware = true;
									if ((int)strArrays.Length >= 3)
									{
										pointClass.Z = double.Parse(strArrays[2]);
									}
								}
								if (str5 == "3")
								{
									str1 = streamReader_0.ReadLine().Trim();
								}
								VCTReader vCTReader2 = this;
								vCTReader2.long_0 = vCTReader2.long_0 + (long)1;
								if (this.string_7 == "2")
								{
									str1 = streamReader_0.ReadLine().Trim();
									VCTReader long03 = this;
									long03.long_0 = long03.long_0 + (long)1;
								}
								try
								{
									feature.Shape = pointClass;
									feature.Store();
								}
								catch (Exception exception1)
								{
									Exception exception = exception1;
									IList ilist4 = this.ilist_4;
									string[] message = new string[] { "[", str3, "] 中对象编码为[", null, null, null };
									message[3] = feature.OID.ToString();
									message[4] = "]的几何数据错误。原因:";
									message[5] = exception.Message;
									ilist4.Add(string.Concat(message));
									feature.Delete();
								}
							}
						}
					}
				}
				else
				{
					break;
				}
			}
		}

		private void method_9(StreamReader streamReader_0)
		{
			IFeatureClass featureClass = null;
			string str = "";
			int num = -1;
			while (true)
			{
				if (streamReader_0.Peek() >= 0)
				{
					string str1 = streamReader_0.ReadLine().Trim();
					VCTReader long0 = this;
					long0.long_0 = long0.long_0 + (long)1;
					if (str1.Length != 0)
					{
						if (str1.ToUpper() == "POLYGONEND")
						{
							if (featureClass == null)
							{
								break;
							}
							Marshal.ReleaseComObject(featureClass);
							featureClass = null;
							break;
						}
						else
						{
							string str2 = str1;
							string str3 = streamReader_0.ReadLine().Trim();
							VCTReader vCTReader = this;
							vCTReader.long_0 = vCTReader.long_0 + (long)1;
							string str4 = streamReader_0.ReadLine().Trim();
							VCTReader long01 = this;
							long01.long_0 = long01.long_0 + (long)1;
							if (str != str4)
							{
								if (featureClass != null)
								{
									Marshal.ReleaseComObject(featureClass);
									featureClass = null;
								}
								featureClass = this.method_10(str3, 1);
								if (featureClass == null)
								{
									this.ilist_4.Add(string.Concat("[", str3, "] 错误，无法找到对应的要素类！"));
								}
								else
								{
									num = featureClass.FindField("MBBSM_VCT");
								}
								str = str4;
							}
							if (featureClass != null)
							{
								IFeature feature = featureClass.CreateFeature();
								feature.Value[num] = str2;
								str1 = streamReader_0.ReadLine().Trim();
								VCTReader vCTReader1 = this;
								vCTReader1.long_0 = vCTReader1.long_0 + (long)1;
								IPointCollection ringClass = null;
								IGeometryCollection polygonClass = new Polygon() as IGeometryCollection;
								object value = Missing.Value;
								while (true)
								{
									if (streamReader_0.Peek() >= 0)
									{
										str1 = streamReader_0.ReadLine().Trim();
										VCTReader long02 = this;
										long02.long_0 = long02.long_0 + (long)1;
										if (str1 == "0")
										{
											if (ringClass == null || ringClass.PointCount <= 2)
											{
												break;
											}
											polygonClass.AddGeometry(ringClass as IGeometry, ref value, ref value);
											break;
										}
										else
										{
											string[] strArrays = str1.Split(this.string_6.ToCharArray());
											if ((int)strArrays.Length != 1)
											{
												IPoint pointClass = new Point()
												{
													X = double.Parse(strArrays[0]),
													Y = double.Parse(strArrays[1])
												};
												if ((int)strArrays.Length >= 3)
												{
													pointClass.Z = double.Parse(strArrays[2]);
												}
												ringClass.AddPoint(pointClass, ref value, ref value);
											}
											else
											{
												if (ringClass != null)
												{
													(ringClass as IRing).Close();
													if (ringClass.PointCount > 2)
													{
														polygonClass.AddGeometry(ringClass as IGeometry, ref value, ref value);
													}
												}
												ringClass = new Ring();
											}
										}
									}
									else
									{
										break;
									}
								}
								if (!(polygonClass as ITopologicalOperator).IsSimple)
								{
									(polygonClass as ITopologicalOperator).Simplify();
								}
								if (this.int_0 == 3)
								{
									(polygonClass as IZAware).ZAware = true;
								}
								try
								{
									feature.Shape = polygonClass as IGeometry;
									feature.Store();
								}
								catch (Exception exception1)
								{
									Exception exception = exception1;
									IList ilist4 = this.ilist_4;
									string[] message = new string[] { "[", str3, "] 中对象编码为[", null, null, null };
									message[3] = feature.OID.ToString();
									message[4] = "]的几何数据错误。原因:";
									message[5] = exception.Message;
									ilist4.Add(string.Concat(message));
									feature.Delete();
								}
							}
						}
					}
				}
				else
				{
					break;
				}
			}
		}

		public void Read(string string_9)
		{
			this.ilist_4.Clear();
			StreamReader streamReader = new StreamReader(string_9, Encoding.Default, true);
			this.long_0 = (long)0;
			this.string_8 = System.IO.Path.GetFileNameWithoutExtension(string_9);
			char string8 = this.string_8[0];
			if ((string8 < '0' ? false : string8 <= '9'))
			{
				this.string_8 = string.Concat("V", this.string_8);
			}
			ISpatialReference spatialReference = SpatialReferenctOperator.ConstructCoordinateSystem(this.ifeatureWorkspace_0 as IGeodatabaseRelease);
			if (this.IsInFeatureDataset)
			{
				this.ifeatureDataset_0 = this.ifeatureWorkspace_0.CreateFeatureDataset(this.string_8, spatialReference);
			}
			while (streamReader.Peek() >= 0)
			{
				string upper = streamReader.ReadLine().Trim().ToUpper();
				VCTReader long0 = this;
				long0.long_0 = long0.long_0 + (long)1;
				if (upper.Length == 0)
				{
					continue;
				}
				if (upper == "HEADBEGIN")
				{
					if (this.progressMessageHandle_0 != null)
					{
						this.progressMessageHandle_0(this, "开始读取文件头...");
					}
					this.method_3(streamReader);
				}
				else if (upper == "ANNOTATIONBEGIN")
				{
					if (this.progressMessageHandle_0 != null)
					{
						this.progressMessageHandle_0(this, "开始读取注记...");
					}
					this.method_4(streamReader);
				}
				else if (upper == "ATTRIBUTEBEGIN")
				{
					if (this.progressMessageHandle_0 != null)
					{
						this.progressMessageHandle_0(this, "开始读取属性...");
					}
					this.method_5(streamReader);
				}
				else if (upper == "FEATURECODEBEGIN")
				{
					if (this.progressMessageHandle_0 != null)
					{
						this.progressMessageHandle_0(this, "开始读取要素类型编码...");
					}
					this.method_6(streamReader);
				}
				else if (upper == "LINEBEGIN")
				{
					if (this.progressMessageHandle_0 != null)
					{
						this.progressMessageHandle_0(this, "开始读取线要素图形...");
					}
					this.method_7(streamReader);
				}
				else if (upper == "POINTBEGIN")
				{
					if (this.progressMessageHandle_0 != null)
					{
						this.progressMessageHandle_0(this, "开始读取点要素图形...");
					}
					this.method_8(streamReader);
				}
				else if (upper != "POLYGONBEGIN")
				{
					if (upper != "TABLESTRUCTUREBEGIN")
					{
						continue;
					}
					if (this.progressMessageHandle_0 != null)
					{
						this.progressMessageHandle_0(this, "开始读取要素属性结构...");
					}
					this.method_14(streamReader);
				}
				else
				{
					if (this.progressMessageHandle_0 != null)
					{
						this.progressMessageHandle_0(this, "开始读取面要素图形...");
					}
					this.method_9(streamReader);
				}
			}
			if (this.ilist_4.Count > 0)
			{
				string str = string.Concat(Application.StartupPath, "\\VCTDataConvert.log");
				
				Logger.Current.Write(str,LogLevel.Info, this.ilist_4);
				MessageBox.Show("VCT数据中有错误，请查看日志文件——VCTDataConvert.log");
				this.ilist_4.Clear();
			}
		}

		public event ProgressMessageHandle ProgressMessage
		{
			add
			{
				ProgressMessageHandle progressMessageHandle;
				ProgressMessageHandle progressMessageHandle0 = this.progressMessageHandle_0;
				do
				{
					progressMessageHandle = progressMessageHandle0;
					ProgressMessageHandle progressMessageHandle1 = (ProgressMessageHandle)Delegate.Combine(progressMessageHandle, value);
					progressMessageHandle0 = Interlocked.CompareExchange<ProgressMessageHandle>(ref this.progressMessageHandle_0, progressMessageHandle1, progressMessageHandle);
				}
				while ((object)progressMessageHandle0 != (object)progressMessageHandle);
			}
			remove
			{
				ProgressMessageHandle progressMessageHandle;
				ProgressMessageHandle progressMessageHandle0 = this.progressMessageHandle_0;
				do
				{
					progressMessageHandle = progressMessageHandle0;
					ProgressMessageHandle progressMessageHandle1 = (ProgressMessageHandle)Delegate.Remove(progressMessageHandle, value);
					progressMessageHandle0 = Interlocked.CompareExchange<ProgressMessageHandle>(ref this.progressMessageHandle_0, progressMessageHandle1, progressMessageHandle);
				}
				while ((object)progressMessageHandle0 != (object)progressMessageHandle);
			}
		}

		internal class FeatureCode
		{
			public string CodeName;

			public string FeatureName;

			public string GeometryType;

			public int Color;

			public string AttributeTable;

			public string Tag;

			public FeatureCode()
			{
			}
		}
	}
}