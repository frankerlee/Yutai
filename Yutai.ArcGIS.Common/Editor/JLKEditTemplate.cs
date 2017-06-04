using System;
using System.Collections.Generic;
using System.Drawing;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Common.Editor
{
	public class YTEditTemplate
	{
		private SortedList<string, string> sortedList_0 = new SortedList<string, string>();

		private System.Drawing.Bitmap bitmap_0 = null;

		private string string_0 = "";

		public System.Drawing.Bitmap Bitmap
		{
			get
			{
				return this.bitmap_0;
			}
			set
			{
				this.bitmap_0 = value;
			}
		}

		public ITool DefaultTool
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		public EditTemplateSchems EditTemplateSchems
		{
			get;
			set;
		}

		public IFeatureLayer FeatureLayer
		{
			get;
			set;
		}

		public string ImageKey
		{
			get
			{
				return this.string_0;
			}
		}

		public string Label
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public ISymbol Symbol
		{
			get;
			set;
		}

		public YTEditTemplate()
		{
		}

		public void Add(string string_4, string string_5)
		{
			if (!this.sortedList_0.ContainsKey(string_4))
			{
				this.sortedList_0.Add(string_4, string_5);
			}
		}

		public YTEditTemplate Clone()
		{
			YTEditTemplate jLKEditTemplate = new YTEditTemplate()
			{
				FeatureLayer = this.FeatureLayer,
				Name = this.Name,
				Symbol = this.Symbol
			};
			jLKEditTemplate.InitBitmap();
			jLKEditTemplate.Label = this.Label;
			foreach (KeyValuePair<string, string> sortedList0 in this.sortedList_0)
			{
				jLKEditTemplate.Add(sortedList0.Key, sortedList0.Value);
			}
			jLKEditTemplate.EditTemplateSchems = this.EditTemplateSchems;
			return jLKEditTemplate;
		}

		public object GetFieldValue(string string_4)
		{
			object dateTime;
			if (this.sortedList_0.ContainsKey(string_4))
			{
				string item = this.sortedList_0[string_4];
				if (item != "<空>")
				{
					int num = this.FeatureLayer.FeatureClass.FindField(string_4);
					if (num == -1)
					{
						dateTime = null;
					}
					else
					{
						IField field = this.FeatureLayer.FeatureClass.Fields.Field[num];
						if (field.Type == esriFieldType.esriFieldTypeDate)
						{
							dateTime = Convert.ToDateTime(item);
						}
						else if (field.Type == esriFieldType.esriFieldTypeDouble)
						{
							dateTime = Convert.ToDouble(item);
						}
						else if (field.Type == esriFieldType.esriFieldTypeInteger)
						{
							dateTime = Convert.ToInt32(item);
						}
						else if (field.Type == esriFieldType.esriFieldTypeSingle)
						{
							dateTime = Convert.ToSingle(item);
						}
						else if (field.Type != esriFieldType.esriFieldTypeSmallInteger)
						{
							dateTime = item;
						}
						else
						{
							dateTime = Convert.ToInt32(item);
						}
					}
				}
				else
				{
					dateTime = null;
				}
			}
			else
			{
				dateTime = null;
			}
			return dateTime;
		}

		public bool HasSchema(string string_4)
		{
			bool flag;
			flag = (this.EditTemplateSchems == null ? false : this.EditTemplateSchems.HasField(string_4));
			return flag;
		}

		public void Init(IFeatureLayer ifeatureLayer_1)
		{
			this.FeatureLayer = ifeatureLayer_1;
			ILayerFields featureLayer = this.FeatureLayer as ILayerFields;
			for (int i = 0; i < featureLayer.FieldCount; i++)
			{
				IField field = featureLayer.Field[i];
				if (!field.Required)
				{
					esriFieldType type = field.Type;
					if (field.Editable)
					{
						if ((type == esriFieldType.esriFieldTypeGeometry || type == esriFieldType.esriFieldTypeBlob || type == esriFieldType.esriFieldTypeRaster ? false : type != esriFieldType.esriFieldTypeOID))
						{
							if (field.IsNullable)
							{
								this.sortedList_0.Add(field.Name, "<空>");
							}
							else if (!(type == esriFieldType.esriFieldTypeInteger || type == esriFieldType.esriFieldTypeSingle || type == esriFieldType.esriFieldTypeSmallInteger ? false : type != esriFieldType.esriFieldTypeDouble))
							{
								this.sortedList_0.Add(field.Name, "0");
							}
							else if (type == esriFieldType.esriFieldTypeString)
							{
								this.sortedList_0.Add(field.Name, "");
							}
							else if (type == esriFieldType.esriFieldTypeDate)
							{
								this.sortedList_0.Add(field.Name, DateTime.Now.ToShortDateString());
							}
						}
					}
				}
				else if (ifeatureLayer_1 is IFDOGraphicsLayer && string.Compare(field.Name, "SymbolID", true) == 0)
				{
					if (!field.IsNullable)
					{
						this.sortedList_0.Add("SymbolID", "0");
					}
					else
					{
						this.sortedList_0.Add("SymbolID", "<空>");
					}
				}
			}
			this.Name = this.FeatureLayer.Name;
			esriGeometryType shapeType = this.FeatureLayer.FeatureClass.ShapeType;
			if (this.FeatureLayer.FeatureClass.FeatureType == esriFeatureType.esriFTAnnotation)
			{
				this.Label = "注记";
			}
			else if (shapeType == esriGeometryType.esriGeometryPolyline)
			{
				this.Label = "线";
			}
			else if (shapeType == esriGeometryType.esriGeometryPolygon)
			{
				this.Label = "面";
			}
			else if ((shapeType == esriGeometryType.esriGeometryPoint ? true : shapeType == esriGeometryType.esriGeometryMultipoint))
			{
				this.Label = "点";
			}
		}

		public void InitBitmap()
		{
			this.bitmap_0 = null;
			if (this.Symbol != null)
			{
				IStyleDraw styleDraw = StyleDrawFactory.CreateStyleDraw(this.Symbol);
				if (styleDraw != null)
				{
					this.bitmap_0 = styleDraw.StyleGalleryItemToBmp(new Size(16, 16), 96, 1);
					this.string_0 = Guid.NewGuid().ToString();
				}
			}
			if (this.bitmap_0 == null)
			{
				esriGeometryType shapeType = this.FeatureLayer.FeatureClass.ShapeType;
				if (this.FeatureLayer.FeatureClass.FeatureType == esriFeatureType.esriFTAnnotation)
				{
					this.bitmap_0 = BitmapResources.Anno;
					this.string_0 = "A1DD7E5C-E48D-49D2-962C-AC98FF5BD25D";
				}
				else if (shapeType == esriGeometryType.esriGeometryPolyline)
				{
					this.bitmap_0 = BitmapResources.Line;
					this.string_0 = "BF5BE334-1BD8-49B4-BAF9-41292C0D74ED";
				}
				else if (shapeType == esriGeometryType.esriGeometryPolygon)
				{
					this.bitmap_0 = BitmapResources.Area;
					this.string_0 = "830C9A6D-AA74-49A8-9794-3B2AD01613AF";
				}
				else if ((shapeType == esriGeometryType.esriGeometryPoint ? true : shapeType == esriGeometryType.esriGeometryMultipoint))
				{
					this.bitmap_0 = BitmapResources.Point;
					this.string_0 = "6D20F51F-BBB0-4551-91CB-24B7F2216B40";
				}
			}
		}

		public void SetFeatureValue(IFeature ifeature_0)
		{
			foreach (KeyValuePair<string, string> sortedList0 in this.sortedList_0)
			{
				object fieldValue = this.GetFieldValue(sortedList0.Key);
				if (fieldValue == null)
				{
					continue;
				}
				int num = ifeature_0.Fields.FindField(sortedList0.Key);
				if (num == -1)
				{
					continue;
				}
				try
				{
					ifeature_0.Value[num] = fieldValue;
				}
				catch
				{
				}
			}
		}

		public void SetFieldValue(string string_4, string string_5)
		{
			this.sortedList_0[string_4] = string_5;
		}
	}
}