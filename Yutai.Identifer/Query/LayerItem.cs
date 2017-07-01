namespace Yutai.Plugins.Identifer.Query
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