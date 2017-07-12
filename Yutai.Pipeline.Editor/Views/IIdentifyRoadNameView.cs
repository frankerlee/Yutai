// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  IIdentifyRoadNameView.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/07/12  10:58
// 更新时间 :  2017/07/12  10:58

using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mvp;

namespace Yutai.Pipeline.Editor.Views
{
    public interface IIdentifyRoadNameView : IMenuProvider
    {
        void Initialize(IAppContext context);
    }
}