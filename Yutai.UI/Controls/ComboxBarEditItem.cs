namespace Yutai.UI.Controls
{
    public abstract class ComboxBarEditItem : RibbonItemEx
    {
        protected ComboxBarEditItem()
        {
        }

        public abstract void Add(object object_0);
        public abstract void Clear();
        public abstract void RemoveAt(int int_0);

        public abstract int Count { get; }

        public abstract object this[int int_0] { get; }

        public abstract string Text { get; set; }
    }
}