using System.Text;

namespace Yutai.ArcGIS.Common.Geodatabase
{
	public class Fields : EnumImpl
	{
		private Field[] field_0;

		public virtual Field this[int int_1]
		{
			get
			{
				Field field0;
				if (int_1 < base.Count)
				{
					field0 = this.field_0[int_1];
				}
				else
				{
					field0 = null;
				}
				return field0;
			}
		}

		protected internal Fields(Layer layer_0) : base(MiApi.mitab_c_get_field_count(layer_0.Handle))
		{
			this.field_0 = new Field[base.Count];
			for (int i = 0; i < base.Count; i++)
			{
				this.field_0[i] = this.CreateField(layer_0, i);
			}
		}

		protected internal virtual Field CreateField(Layer layer_0, int int_1)
		{
			return new Field(layer_0, int_1);
		}

		public override object GetObj(int int_1)
		{
			return this[int_1];
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Columns:\n");
			foreach (Field field in this)
			{
				stringBuilder.Append(string.Concat(field.ToString(), "\t"));
			}
			return stringBuilder.ToString();
		}
	}
}