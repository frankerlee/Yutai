using System.Runtime.CompilerServices;
using System.Xml;
using DevExpress.XtraBars;

namespace Yutai.ArcGIS.Framework
{
    internal class Creator
    {
        protected BarManager barManager1;
        protected PopupMenu m_pPopupMenu;
        [CompilerGenerated]
        private object object_0;

        public virtual void Create(XmlNode xmlNode_0)
        {
        }

        public object Parent
        {
            [CompilerGenerated]
            get
            {
                return this.object_0;
            }
            [CompilerGenerated]
            set
            {
                this.object_0 = value;
            }
        }
    }
}

