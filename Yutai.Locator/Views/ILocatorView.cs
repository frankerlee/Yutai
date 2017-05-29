using System;
using System.Collections.Generic;
using Yutai.Plugins.Mvp;

namespace Yutai.Plugins.Locator.Views
{
   
    public interface ILocatorView : IMenuProvider
    {
        //定位器改变事件触发
        string LocatorName { get; }
        bool ZoomToShape { get; }
        void Clear();
        event Action LocatorChanged;
        event Action ItemSelected;
        void UpdateView();

        void LoadLocators();

        void TrySearch(bool allowKeyEmpty);
    }
}
