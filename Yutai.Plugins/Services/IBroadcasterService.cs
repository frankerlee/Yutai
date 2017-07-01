using System;
using System.Linq.Expressions;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Events;

namespace Yutai.Plugins.Services
{
    public interface IBroadcasterService
    {
        /// <summary>
        /// Broadcasts map event to all the listening plugins.
        /// </summary>
        /* void BroadcastEvent<T>(Expression<Func<BasePlugin, MapEventHandler<T>>> eventHandler, IMuteMap sender, T args)
             where T : EventArgs;
 
         void BroadcastEvent<T>(Expression<Func<BasePlugin, LegendEventHandler<T>>> eventHandler, IMuteLegend sender, T args)
             where T : EventArgs;*/
        void BroadcastEvent<T>(Expression<Func<BasePlugin, EventHandler<T>>> eventHandler, object sender, T args)
            where T : EventArgs;

        void BroadcastEvent<T>(Expression<Func<BasePlugin, EventHandler<T>>> eventHandler, object sender, T args,
            PluginIdentity identity)
            where T : EventArgs;

        event EventHandler<EventArgs> ItemClicked;
        event EventHandler<EventArgs> ItemCheckedChanged;
        event EventHandler<MenuItemEventArgs> StatusItemClicked;

        void FireItemClicked(object sender, EventArgs args);
        void FireItemCheckedChanged(object sender, EventArgs args);
        void FireStatusItemClicked(object sender, MenuItemEventArgs args);
    }
}