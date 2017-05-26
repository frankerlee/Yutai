﻿using System;
using System.ComponentModel;
using System.Windows.Forms;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Events;

namespace Yutai.Plugins.Interfaces
{
    public interface IMenuItem
    {
        /// <summary>
        /// Gets/Sets the Text shown for the MenuItem
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// Gets/Sets the icon for the menu item
        /// </summary>
        IMenuIcon Icon { get; set; }

        /// <summary>
        /// Gets/Sets the category for this item (used when the user customizes the menu)
        /// </summary>
        string Category { get; set; }

        /// <summary>
        /// Gets/Sets the checked state of the item
        /// </summary>
        bool Checked { get; set; }

        /// <summary>
        /// Gets/Sets the description of this menu item, used in customization of menu by the user
        /// </summary>
        string Description { get; set; }

        /// <summary>
        ///	Gets/Sets the enabled state of this item
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        /// Gets the key of this item
        /// </summary>
        string Key { get; }

        /// <summary>
        /// Gets a value indicating whether this menu item has key.
        /// </summary>
        bool HasKey { get; }

        /// <summary>
        /// Gets/Sets the visibility state of this item
        /// </summary>
        bool Visible { get; set; }

        /// <summary>
        /// Gets or sets any custom information to be stored with the item.
        /// </summary>
        object Tag { get; set; }

        /// <summary>
        /// Gets the internal object.
        /// </summary>
        object GetInternalObject();

        /// <summary>
        /// Gets the plugin identity.
        /// </summary>
        PluginIdentity PluginIdentity { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this item should be preceded by separator. 
        /// Note: call IMenuItemCollection.Update() after changing this property.
        /// </summary>
        bool BeginGroup { get; set; }

        /// <summary>
        /// Gets the unique key of menu item, consisting of the original key assigned by developer and 
        /// a postfix with GUID of plugin.
        /// </summary>
        string UniqueKey { get; }


        /// <summary>
        /// Occurs when certain properties of the menu item change.
        /// </summary>
        event EventHandler<PropertyChangedEventArgs> ItemChanged;

        /// <summary>
        /// Occurs when user clicks the item with mouse.
        /// </summary>
        event EventHandler<MenuItemEventArgs> ItemClicked;

        /// <summary>
        /// Gets a value indicating whether the item should be skipped during processing (e.g. separator).
        /// </summary>
        bool Skip { get; }

        /// <summary>
        /// Gets or sets the shortcut keys.
        /// </summary>
        Keys ShortcutKeys { get; set; }
    }
}