using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Common.Wrapper
{
	public class FieldObject
	{
		private IField ifield_0 = null;

		public IField Field
		{
			get
			{
				return this.ifield_0;
			}
		}

		public FieldObject(IField ifield_1)
		{
			this.ifield_0 = ifield_1;
		}

		public override string ToString()
		{
			return string.Format("{0} ({1})", this.ifield_0.Name, this.method_0(this.ifield_0.Type));
		}

		private string method_0(esriFieldType esriFieldType_0)
		{
			string result;
			if (esriFieldType_0 == esriFieldType.esriFieldTypeBlob)
			{
				result = "二进制";
			}
			else if (esriFieldType_0 == esriFieldType.esriFieldTypeDate)
			{
				result = "日期";
			}
			else if (esriFieldType_0 == esriFieldType.esriFieldTypeDouble)
			{
				result = "双精度";
			}
			else if (esriFieldType_0 == esriFieldType.esriFieldTypeGeometry)
			{
				result = "几何对象";
			}
			else if (esriFieldType_0 == esriFieldType.esriFieldTypeGlobalID)
			{
				result = "Guid";
			}
			else if (esriFieldType_0 == esriFieldType.esriFieldTypeGUID)
			{
				result = "GUID";
			}
			else if (esriFieldType_0 == esriFieldType.esriFieldTypeInteger)
			{
				result = "整形";
			}
			else if (esriFieldType_0 == esriFieldType.esriFieldTypeOID)
			{
				result = "OID";
			}
			else if (esriFieldType_0 == esriFieldType.esriFieldTypeRaster)
			{
				result = "栅格";
			}
			else if (esriFieldType_0 == esriFieldType.esriFieldTypeSingle)
			{
				result = "单精度";
			}
			else if (esriFieldType_0 == esriFieldType.esriFieldTypeSmallInteger)
			{
				result = "短整形";
			}
			else if (esriFieldType_0 == esriFieldType.esriFieldTypeString)
			{
				result = "字符串";
			}
			else if (esriFieldType_0 == esriFieldType.esriFieldTypeXML)
			{
				result = "XML";
			}
			else
			{
				result = "";
			}
			return result;
		}
	}
}
