using System;
using Yutai.Plugins.Events;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Interfaces
{
    public interface IMenuEx : IMenuBase
    {
        event EventHandler<MenuItemEventArgs> ItemClicked;
    }
}