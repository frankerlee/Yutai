using System;
using System.Text;

namespace Yutai.ArcGIS.Common.Geodatabase
{
    public class Feature : IDisposable
    {
        public readonly IntPtr Handle;

        public readonly int Id;

        public readonly FeatureType Type;

        public readonly Layer Layer;

        public readonly Parts Parts;

        private bool bool_0 = false;

        public string Text
        {
            get { return (this.Type == FeatureType.TABFC_Text ? MiApi.mitab_c_get_text(this.Handle) : ""); }
        }

        protected internal Feature(Layer layer_0, int int_0)
        {
            this.Id = int_0;
            this.Layer = layer_0;
            this.Handle = MiApi.mitab_c_read_feature(layer_0.Handle, int_0);
            this.Type = MiApi.mitab_c_get_type(this.Handle);
            this.Parts = this.CreateParts(this);
        }

        protected internal virtual Parts CreateParts(Feature feature_0)
        {
            return new Parts(this);
        }

        public void Dispose(bool bool_1)
        {
            if ((!bool_1 ? false : !this.bool_0))
            {
                MiApi.mitab_c_destroy_feature(this.Handle);
                this.bool_0 = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        ~Feature()
        {
            this.Dispose(false);
        }

        public Feature GetNext()
        {
            Feature feature = new Feature(this.Layer, MiApi.mitab_c_next_feature_id(this.Layer.Handle, this.Id));
            return feature;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(string.Concat("Feature: ", this.Id, "\nFields:\n"));
            foreach (Field field in this.Layer.Fields)
            {
                stringBuilder.Append(string.Concat(field.GetValueAsString(this).Trim(), "\t"));
            }
            stringBuilder.Append(string.Concat("\n", this.Parts.ToString()));
            return stringBuilder.ToString();
        }
    }
}