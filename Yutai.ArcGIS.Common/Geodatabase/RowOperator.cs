using System;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Common.Geodatabase
{
	public class RowOperator
	{
		public RowOperator()
		{
		}

		public static void CopyFeatureAttributeToFeature(IFeature ifeature_0, IFeature ifeature_1)
		{
			IFields fields = ifeature_0.Fields;
			for (int i = 0; i < fields.FieldCount; i++)
			{
				IField field = fields.Field[i];
				if ((field.Type == esriFieldType.esriFieldTypeOID || field.Type == esriFieldType.esriFieldTypeGeometry ? false : field.Editable))
				{
					ifeature_1.Value[i] = ifeature_0.Value[i];
				}
			}
			ifeature_1.Store();
		}

		public static void CopyFeatureToFeature(IFeature ifeature_0, IFeature ifeature_1)
		{
			IFields fields = ifeature_0.Fields;
			for (int i = 0; i < fields.FieldCount; i++)
			{
				IField field = fields.Field[i];
				if ((field.Type == esriFieldType.esriFieldTypeOID || field.Type == esriFieldType.esriFieldTypeGeometry ? false : field.Editable))
				{
					ifeature_1.Value[i] = ifeature_0.Value[i];
				}
			}
			ifeature_1.Shape = ifeature_0.ShapeCopy;
			ifeature_1.Store();
		}

		public static void CopyFeatureToFeatureEx(IFeature ifeature_0, IFeature ifeature_1)
		{
			IFields fields = ifeature_0.Fields;
			for (int i = 0; i < fields.FieldCount; i++)
			{
				IField field = fields.Field[i];
				if ((field.Type == esriFieldType.esriFieldTypeOID || field.Type == esriFieldType.esriFieldTypeGeometry ? false : field.Editable))
				{
					int value = ifeature_1.Fields.FindField(field.Name);
					if (value != -1)
					{
						ifeature_1.Value[value] = ifeature_0.Value[i];
					}
				}
			}
			ifeature_1.Shape = ifeature_0.ShapeCopy;
			ifeature_1.Store();
		}

		public static IRow CopyRowToRow(IRow irow_0, IRow irow_1)
		{
			IFields fields = irow_0.Fields;
			try
			{
				for (int i = 0; i < fields.FieldCount; i++)
				{
					IField field = fields.Field[i];
					if ((field.Type == esriFieldType.esriFieldTypeOID || field.Type == esriFieldType.esriFieldTypeGeometry ? false : field.Editable))
					{
						string name = fields.Field[i].Name;
						int value = irow_1.Fields.FindField(name);
						if (value != -1)
						{
							irow_1.Value[value] = irow_0.Value[i];
						}
					}
				}
			}
			catch (Exception exception)
			{
			}
			irow_1.Store();
			return irow_1;
		}

		public static IRow CreatRowByRow(IRow irow_0)
		{
			IRow value = irow_0.Table.CreateRow();
			IFields fields = irow_0.Fields;
			for (int i = 0; i < fields.FieldCount; i++)
			{
				IField field = fields.Field[i];
				if ((field.Type == esriFieldType.esriFieldTypeOID ? false : field.Editable))
				{
					value.Value[i] = irow_0.Value[i];
				}
			}
			value.Store();
			return value;
		}

		public static string GetFieldTypeString(esriFieldType esriFieldType_0)
		{
			string str;
			switch (esriFieldType_0)
			{
				case esriFieldType.esriFieldTypeSmallInteger:
				{
					str = "短整型";
					break;
				}
				case esriFieldType.esriFieldTypeInteger:
				{
					str = "长整型";
					break;
				}
				case esriFieldType.esriFieldTypeSingle:
				{
					str = "单精度";
					break;
				}
				case esriFieldType.esriFieldTypeDouble:
				{
					str = "双精度";
					break;
				}
				case esriFieldType.esriFieldTypeString:
				{
					str = "文本";
					break;
				}
				case esriFieldType.esriFieldTypeDate:
				{
					str = "日期类型";
					break;
				}
				case esriFieldType.esriFieldTypeOID:
				{
					str = "OID";
					break;
				}
				case esriFieldType.esriFieldTypeGeometry:
				{
					str = "几何数据";
					break;
				}
				case esriFieldType.esriFieldTypeBlob:
				{
					str = "二进制串";
					break;
				}
				case esriFieldType.esriFieldTypeRaster:
				{
					str = "栅格数据类型";
					break;
				}
				case esriFieldType.esriFieldTypeGUID:
				{
					str = "GUID";
					break;
				}
				case esriFieldType.esriFieldTypeGlobalID:
				{
					str = "GlobalID";
					break;
				}
				default:
				{
					str = "";
					break;
				}
			}
			return str;
		}

		public static object GetFieldValue(IRow irow_0, string string_0)
		{
			object value = null;
			int num = irow_0.Fields.FindField(string_0);
			if (num != -1)
			{
				value = irow_0.Value[num];
			}
			return value;
		}

		public static void SetFieldValue(IRow irow_0, string string_0, object object_0)
		{
			int object0 = irow_0.Fields.FindField(string_0);
			if (object0 != -1)
			{
				irow_0.Value[object0] = object_0;
			}
		}
	}
}