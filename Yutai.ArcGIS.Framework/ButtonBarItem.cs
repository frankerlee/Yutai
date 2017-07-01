namespace Yutai.ArcGIS.Framework
{
    public abstract class ButtonBarItem : BarItemEx
    {
        protected ButtonBarItem()
        {
        }

        public abstract string Caption { set; }
    }
}