namespace Yutai.ArcGIS.Common.Geodatabase
{
	public class Field
	{
		public readonly string Name;

		public FieldType Type;

		public readonly int Index;

		public readonly int Width;

		public readonly short Precision;

		public readonly Layer Layer;

		protected internal Field(Layer layer_0, int int_0)
		{
			this.Layer = layer_0;
			this.Index = int_0;
			this.Name = MiApi.mitab_c_get_field_name(layer_0.Handle, int_0);
			this.Type = MiApi.mitab_c_get_field_type(layer_0.Handle, int_0);
			this.Precision = (short)MiApi.mitab_c_get_field_precision(layer_0.Handle, int_0);
			this.Width = MiApi.mitab_c_get_field_width(layer_0.Handle, int_0);
		}

		public double GetValueAsDouble(Feature feature_0)
		{
			return MiApi.mitab_c_get_field_as_double(feature_0.Handle, this.Index);
		}

		public string GetValueAsString(Feature feature_0)
		{
			return MiApi.mitab_c_get_field_as_string(feature_0.Handle, this.Index);
		}

		public override string ToString()
		{
			return string.Concat(this.Name, ", ", this.Type);
		}
	}
}