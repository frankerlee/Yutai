using System.Runtime.CompilerServices;

namespace Yutai.ArcGIS.Common
{
    public class MenuInfo
    {
       

        public override string ToString()
        {
            string str = this.CAPTION.Trim();
            if (str.Length == 0)
            {
                str = this.NAME.Trim();
            }
            return str;
        }

        public bool? BEGINGROUP { get; set; }

        public string CAPTION
        {
            get; set;
        }

        public string CLASSNAME
        {
            get; set;
        }

        public string COMPONENTDLLNAME
        {
            get; set;
        }

        public bool? ISPOPMENUITEM
        {
            get; set;
        }

        public int? ItemCol
        {
            get; set;
        }

        public string MenuID
        {
            get; set;
        }

        public string NAME
        {
            get; set;
        }

        public int? ORDERBY
        {
            get; set;
        }

        public string PARENTIDS
        {
            get; set;
        }

        public string PROGID
        {
            get; set;
        }

        public string SHORTCUT
        {
            get; set;
        }

        public int? SUBTYPE
        {
            get; set;
        }

        public bool? VISIBLE
        {
            get; set;
        }
    }
}

