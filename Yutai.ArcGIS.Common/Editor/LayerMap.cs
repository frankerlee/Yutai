using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Common.Editor
{
	public class LayerMap
	{
		private List<FieldMaps> list_0 = new List<FieldMaps>();

		public string DestLayerAliasName
		{
			get;
			set;
		}

		public int FieldMapsCount
		{
			get
			{
				return this.list_0.Count;
			}
		}

		public FieldMaps this[int int_0]
		{
			get
			{
				return this.list_0[int_0];
			}
		}

		public string SourceFeatureClassName
		{
			get;
			set;
		}

		public string SourceLayerAliasName
		{
			get;
			set;
		}

		public string TargetFeatureClassName
		{
			get;
			set;
		}

		public LayerMap()
		{
		}

		public void AddFieldMap(FieldMaps fieldMaps_0)
		{
			this.list_0.Add(fieldMaps_0);
		}

		public void Copy(IRow irow_0, IRow irow_1)
		{
			foreach (FieldMaps list0 in this.list_0)
			{
				list0.CopyValue(irow_0, irow_1);
			}
		}

		public void Remove(FieldMaps fieldMaps_0)
		{
			this.list_0.Remove(fieldMaps_0);
		}

		public void RemoveAt(int int_0)
		{
			this.list_0.RemoveAt(int_0);
		}
	}
}