namespace Yutai.ArcGIS.Common.Priviliges
{
    public class MenuInfo
    {
        public string MenuID { get; set; }

        public string CAPTION { get; set; }

        public string NAME { get; set; }

        public string COMPONENTDLLNAME { get; set; }

        public string CLASSNAME { get; set; }

        public int? SUBTYPE { get; set; }

        public bool? ISPOPMENUITEM { get; set; }

        public int? ORDERBY { get; set; }

        public bool? BEGINGROUP { get; set; }

        public string PROGID { get; set; }

        public bool? VISIBLE { get; set; }

        public string SHORTCUT { get; set; }

        public string PARENTIDS { get; set; }

        public int? ItemCol { get; set; }

        public override string ToString()
        {
            string text = this.CAPTION.Trim();
            if (text.Length == 0)
            {
                text = this.NAME.Trim();
            }
            return text;
        }
    }
}