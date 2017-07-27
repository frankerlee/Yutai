using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Pipeline.Editor.Classes
{
    public class LayerItem : object
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public LayerItem(string text, object value)
        {
            Text = text;
            Value = value;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
