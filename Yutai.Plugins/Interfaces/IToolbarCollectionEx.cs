using System;
using Yutai.Plugins.Events;

namespace Yutai.Plugins.Interfaces
{
    public interface IToolbarCollectionEx : IToolbarCollectionBase
    {
        event EventHandler<MenuItemEventArgs> ItemClicked;
    }
}