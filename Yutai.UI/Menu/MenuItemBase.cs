﻿using System;
using System.ComponentModel;
using Yutai.Plugins.Concrete;

namespace Yutai.UI.Menu
{
    internal abstract class MenuItemBase
    {
        public abstract bool HasKey { get; }

        public string Key
        {
            get { return Metadata.Key; }
        }

        public object Tag
        {
            get { return Metadata.Tag; }
            set { Metadata.Tag = value; }
        }

        public PluginIdentity PluginIdentity
        {
            get
            {
                return Metadata.PluginIdentity;
            }
        }

        public bool BeginGroup
        {
            get { return Metadata.BeginGroup; }
            set { Metadata.BeginGroup = value; }
        }

        public string Description
        {
            get { return Metadata.Description; }
            set
            {
                Metadata.Description = value;
                FireItemChanged("Description");
            }
        }

        public string UniqueKey
        {
            get { return Key + PluginIdentity.Guid; }
        }

        protected abstract MenuItemMetadata Metadata { get; }

        protected void FireItemChanged(string propertyName)
        {
            var handler = ItemChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event EventHandler<PropertyChangedEventArgs> ItemChanged;
    }
}