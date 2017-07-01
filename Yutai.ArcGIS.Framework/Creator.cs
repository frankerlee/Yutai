using System.Runtime.CompilerServices;
using System.Xml;
using DevExpress.XtraBars;

namespace Yutai.ArcGIS.Framework
{
    internal class Creator
    {
        protected BarManager barManager1;
        protected PopupMenu m_pPopupMenu;

        public virtual void Create(XmlNode xmlNode_0)
        {
        }

        public object Parent { get; set; }
    }
}