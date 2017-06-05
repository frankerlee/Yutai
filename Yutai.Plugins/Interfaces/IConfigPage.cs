using System.Drawing;
using Yutai.Plugins.Enums;

namespace Yutai.Plugins.Interfaces
{
    public interface IConfigPage
    {
        string Description { get; }

        Bitmap Icon { get; }

        string PageName { get; }

        ConfigPageType PageType { get; }

        ConfigPageType ParentPage { get; }

        object Tag { get; set; }

        /// <summary>
        /// Gets a value indicating whether the page height can be adjusted to fit the the parent.
        /// </summary>
        bool VariableHeight { get; }

        void Initialize();

        void Save();

        int ImageIndex { get; set; }

        Size OriginalSize { get; set; }

        //增加的属性，用来替代原来的PageType,
        string Key { get; }
        string ParentKey { get;  }
    }
}