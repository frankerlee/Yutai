namespace Yutai.ArcGIS.Framework
{
    public abstract class TreeviewComboxBarItem : BarItemEx
    {
        protected TreeviewComboxBarItem()
        {
        }

        public abstract void Add(object object_0);
        public abstract void Clear();

        public abstract System.Windows.Forms.TreeView TreeView { get; }
    }
}