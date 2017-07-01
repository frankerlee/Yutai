using System.Collections.Generic;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Common.Editor
{
    public class EditTemplateSchem
    {
        public SortedList<string, object> FieldValues = new SortedList<string, object>();

        public string Description { get; set; }

        public string Label { get; set; }

        public ISymbol Symbol { get; set; }

        public string Value { get; set; }

        public EditTemplateSchem()
        {
        }

        public void Add(string string_3, object object_0)
        {
            if (!this.FieldValues.ContainsKey(string_3))
            {
                this.FieldValues.Add(string_3, object_0);
            }
            else
            {
                this.FieldValues[string_3] = object_0;
            }
        }

        public object GetValues(string string_3)
        {
            object obj;
            object item = null;
            if (this.FieldValues.ContainsKey(string_3))
            {
                item = this.FieldValues[string_3];
            }
            if (item != null)
            {
                obj = item;
            }
            else
            {
                obj = "<空>";
            }
            return obj;
        }
    }
}