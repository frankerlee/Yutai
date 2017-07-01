using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using Yutai.Plugins.Mvp;
using Yutai.UI.Style;

namespace Yutai.UI
{
    internal static class CompositionRoot
    {
        public static void Compose(IApplicationContainer container)
        {
            container.RegisterService<IStyleService, DevExpressStyleService>()
                .RegisterSingleton<ControlStyleSettings>();
        }
    }
}