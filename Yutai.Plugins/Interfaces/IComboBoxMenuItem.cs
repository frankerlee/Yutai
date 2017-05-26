using System;
using System.Collections.Specialized;
using Yutai.Plugins.Events;
using Yutai.Plugins.Mvp;

namespace Yutai.Plugins.Interfaces
{
    public interface IComboBoxMenuItem : IMenuItem
    {
        StringCollection DataSource { get; }
        int Width { get; set; }
        event EventHandler<StringValueChangedEventArgs> ValueChanged;
        void Focus();
    }
}