using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yutai.Plugins.Mvp;

namespace Yutai.Views.Abstract
{
    public interface IConfigView : IView<ConfigViewModel>, IMenuProvider
    {
        event Action PageShown;
    }
}