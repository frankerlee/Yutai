using System;
using System.IO;

namespace Yutai.ArcGIS.Common.Geodatabase
{
    public class Layer : IDisposable
    {
        public readonly IntPtr Handle;

        public readonly Fields Fields;

        public readonly Features Features;

        public readonly string FileName;

        public Layer(string string_0)
        {
            this.Handle = MiApi.mitab_c_open(string_0);
            if (this.Handle == IntPtr.Zero)
            {
                throw new FileNotFoundException(string.Concat("File ", string_0, " not found"), string_0);
            }
            this.Fields = this.CreateFields();
            this.Features = this.CreateFeatures();
            this.FileName = string_0;
        }

        public virtual Features CreateFeatures()
        {
            return new Features(this);
        }

        public virtual Fields CreateFields()
        {
            return new Fields(this);
        }

        public void Dispose()
        {
            MiApi.mitab_c_close(this.Handle);
        }

        public static Layer GetByName(string string_0)
        {
            return new Layer(string_0);
        }

        public override string ToString()
        {
            return string.Concat("Layer: ", this.FileName);
        }

        public void ToText(TextWriter textWriter_0)
        {
            textWriter_0.WriteLine(this);
            textWriter_0.WriteLine(string.Concat(this.Fields, "\n"));
            textWriter_0.WriteLine(this.Features);
        }

        public void ToText(string string_0)
        {
            this.ToText(new StreamWriter(string_0));
        }
    }
}