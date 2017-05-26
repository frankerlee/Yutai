using System;
using Yutai.Plugins.Mvp;

namespace Yutai.Plugins.Interfaces
{
    public interface IDropDownMenuItem : IMenuItem
    {
        /// <summary>
        /// Gets the collection item in submenu for this item.
        /// </summary>
        IMenuItemCollection SubItems { get; }

        event EventHandler DropDownOpening;

        event EventHandler DropDownClosed;

        void Update();
    }
}