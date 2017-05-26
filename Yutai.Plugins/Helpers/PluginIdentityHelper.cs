using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Mef;
using Yutai.Shared;

namespace Yutai.Plugins.Helpers
{
    public static class PluginIdentityHelper
    {
        private static readonly Dictionary<string, PluginIdentity> _cache = new Dictionary<string, PluginIdentity>();

        public static PluginIdentity GetIdentity(Type pluginType, IPluginMetadata metadata = null)
        {
            if (pluginType == null) throw new ArgumentNullException("pluginType");

            PluginIdentity identity;
            if (_cache.TryGetValue(pluginType.FullName, out identity))
            {
                return identity;
            }

            if (metadata == null)
            {
                metadata = GetPluginMetadata(pluginType);
            }

            identity = GetIdentityCore(pluginType, metadata);

            _cache[pluginType.FullName] = identity;

            return identity;
        }

        private static IPluginMetadata GetPluginMetadata(Type pluginType)
        {
            var metadata = AttributeHelper.GetAttribute<YutaiPluginAttribute>(pluginType) as IPluginMetadata;

            if (metadata == null)
            {
                throw new ApplicationException("Plugin type must be decorated with MapWindowPluginAttribute.");
            }

            return metadata;
        }

        private static PluginIdentity GetIdentityCore(Type pluginType, IPluginMetadata metadata)
        {
            if (metadata.Empty)
            {
                try
                {
                    var info = pluginType.GetAssemblyInfo();
                    var attr = pluginType.Assembly.GetAttribute<GuidAttribute>();
                    var guid = new Guid(attr.Value);
                    return new PluginIdentity(info.ProductName, info.CompanyName, guid);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Failed to load plugin identity from assembly.", ex);
                }
            }

            return new PluginIdentity(metadata.Name, metadata.Author, new Guid(metadata.Guid));
        }
    }
}