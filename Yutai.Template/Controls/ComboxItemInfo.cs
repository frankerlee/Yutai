namespace Yutai.Plugins.Template.Controls
{
    public class ComboxItemInfo
    {
        public string Value { get; set; }
        public string Display { get; set; }

        public string Parent { get; set; }

        public ComboxItemInfo(string value, string description)
        {
            Value = value;
            Display = description;
            Parent = "";
        }
        public ComboxItemInfo(string value, string description,string parent)
        {
            Value = value;
            Display = description;
            Parent = parent;
        }
    }
}