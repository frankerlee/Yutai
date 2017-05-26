using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Concrete
{
    public class RibbonMenuItemCollectionMetadata
    {
        public IRibbonItem InsertBefore { get; set; }
        public bool AlignRight { get; set; }
    }
    internal class MenuItemCollectionMetadata
    {
        public IMenuItem InsertBefore { get; set; }
        public bool AlignRight { get; set; }
    }

    internal class ToolStripItemCollectionMetadata
    {
        public IToolStripItem InsertBefore { get; set; }
        public bool AlignRight { get; set; }
    }
}